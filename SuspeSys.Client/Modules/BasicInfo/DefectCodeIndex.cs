using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Utils.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using SuspeSys.Client.Action.BasicInfo;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class DefectCodeIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
  
        public DefectCodeIndex()
        {
            InitializeComponent();
        }

        private void BasicProcessLirbaryIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            Query(1);
            RegisterSusGridEvent();
            RegisterCommconEvent();
            DefaultSet();
        }
        void DefaultSet()
        {
            this.searchControl1.Properties.NullValuePrompt = "输入疵点代码及疵点名称搜索";
        }

        private void RegisterCommconEvent()
        {
            searchControl1.KeyDown += searchControl1_KeyDown;
        }
        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }

        private void RegisterSusGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
        BasicInfoAction basicInfoAction = new BasicInfoAction();
        private void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey = searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = new Dictionary<string, string>();
            //ordercondition.Add("StyleNo", "Asc");
            var list = basicInfoAction.SearchDefectCodeTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = susGrid1.DataGrid.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="DefectNo",Visible=true,Name="Seq"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="疵点代码",FieldName="DefectCode",Visible=true,Name="DefectCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="疵点名称",FieldName="DefectName",Visible=true,Name="DefectName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"}

            });

            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = true;



        }
        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var tsmItemCopyAndNew = new ToolStripMenuItem() { Text = "复制(新增)" };
            tsmItemCopyAndNew.Click += TsmItemCopyAndNew_Click;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { tsmItemCopyAndNew });
        }
        void RegisterTooButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        bool sucess=Save();
                        if(sucess)
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem(true);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    case ButtonName.Delete:
                        DeleteItem();
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void DeleteItem()
        {
            var selectData = (((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.DefectCodeTable);

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);// XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.DefectCodeTable>(selectData);
                action.DeleteLog<Domain.DefectCodeTable>(selectData.Id, "疵点代码", "DefectCodeIndex", selectData.GetType().Name);
                Query(1);
            }
        }
        //private void BindData()
        //{
        //    var defectCodeTableService = new CommonServiceImpl<SuspeSys.Domain.DefectCodeTable>();
        //    susGrid1.DataGrid.DataSource = defectCodeTableService.GetList();
        //}

        bool Save()
        {
            var action = new Client.Action.Common.CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<Domain.DefectCodeTable>;
            if (null != list)
            {
                var nullList = list.Where<Domain.DefectCodeTable>(f =>f.DefectCode==null).ToList<Domain.DefectCodeTable>();
                if (nullList.Count > 0)
                {
                    //XtraMessageBox.Show("疵点代码不能为空!");
                    XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("defectCode")));
                    return false;
                }
                foreach (var m in list)
                {
                    if (string.IsNullOrEmpty(m.DefectCode?.Trim()))
                    {
                        //XtraMessageBox.Show("疵点代码不能为空!");
                        XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("defectCode")));
                        return false;
                    }
                    var pCode = m.DefectCode?.Trim();
                    try
                    {
                        short pcodeMax = short.Parse(pCode);
                        if (pcodeMax > 255)
                        {
                            XtraMessageBox.Show("疵点代码必须为(0--255之间的)数字!");
                            return false;
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("疵点代码必须为(0--255之间的)数字!");
                        return false;
                    }
                    if (string.IsNullOrEmpty(m.Id))
                    {
                        var ht = new Hashtable();
                        ht.Add("DefectCode", m.DefectCode);
                        var ex = action.CheckIsExist<Domain.DefectCodeTable>(ht);
                        if (ex)
                        {
                            XtraMessageBox.Show(string.Format("疵点代码【{0}】已存在!", m.DefectCode.Trim()));
                            return false;
                        }
                    }
                    var exList = list.Where<Domain.DefectCodeTable>(f => null != f.Id && !f.Id.Equals(m.Id) && f.DefectCode.Equals(m.DefectCode)).ToList<Domain.DefectCodeTable>();
                    if (exList.Count > 0)
                    {
                        XtraMessageBox.Show(string.Format("疵点代码【{0}】已存在!", m.DefectCode.Trim()));
                        return false;
                    }
                }

                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<Domain.DefectCodeTable>(m);
                    }
                    else
                    {
                        action.Save<Domain.DefectCodeTable>(m);
                    }
                }
               
            }
            return true;
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem(true);
        }
        void AddItem(bool  isNewButton=false)
        {
            var dt = susGrid1.DataGrid.DataSource as IList<Domain.DefectCodeTable>;
            if (dt == null) dt = new List<Domain.DefectCodeTable>();
            var gv = (susGrid1.DataGrid.MainView as GridView);
            if (isNewButton) {
                var newModel =new Domain.DefectCodeTable();
                //newModel.DefectNo = dt.Count.ToString();
                newModel.Deleted = 0;
                dt.Add(newModel);
                susGrid1.DataGrid.MainView.RefreshData();
                return;
            }
            var selectRow = gv?.GetRow(gv.FocusedRowHandle) as Domain.DefectCodeTable;
            if (null != selectRow) {
                var newModel = BeanUitls<Domain.DefectCodeTable, Domain.DefectCodeTable>.Mapper(selectRow);
                newModel.DefectNo=dt.Count.ToString();
                dt.Add(newModel);
                susGrid1.DataGrid.MainView.RefreshData();
            }
          
        }
    }
}

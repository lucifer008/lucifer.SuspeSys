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
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Utils.Reflection;
using System.Collections;
using SuspeSys.Client.Action.BasicInfo;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class LackOfMaterialCodeIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public LackOfMaterialCodeIndex()
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
            this.searchControl1.Properties.NullValuePrompt = "输入缺料代码及缺料名称搜索";
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
            var list = basicInfoAction.SearchLackMaterialsTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new DevExpress.XtraGrid.Views.Grid.GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="缺料序号",FieldName="LackMaterialsNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="缺料代码",FieldName="LackMaterialsCode",Visible=true,Name="lackOfMaterialCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="缺料名称",FieldName="LackMaterialsName",Visible=true,Name="LackMaterialsName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;


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
                        var sucess = Save();
                        if (sucess)
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem(true);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.SaveAndClose:
                        sucess = Save();
                        if (sucess)
                        {
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                            ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        }
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
                XtraMessageBox.Show(ex.Message, "错误");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void DeleteItem()
        {
            var selectData = (((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.LackMaterialsTable);

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
                action.Update<Domain.LackMaterialsTable>(selectData);
                action.DeleteLog<Domain.LackMaterialsTable>(selectData.Id, "缺料代码", "LackMaterialsTableIndex", selectData.GetType().Name);
                Query(1);
            }
        }
        private void BindData()
        {

            var lackMaterialsTableService = new CommonServiceImpl<SuspeSys.Domain.LackMaterialsTable>();
            susGrid1.DataGrid.DataSource = lackMaterialsTableService.GetList();
        }

        bool Save()
        {
            var action = new Client.Action.Common.CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<Domain.LackMaterialsTable>;
            if (null != list)
            {
                var nullList = list.Where<Domain.LackMaterialsTable>(f => f.LackMaterialsCode == null).ToList<Domain.LackMaterialsTable>();
                if (nullList.Count > 0)
                {
                    //XtraMessageBox.Show("缺料代码不能为空!");
                    XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("lackOfMaterialCode")));
                    return false;
                }
                foreach (var m in list)
                {
                    if (string.IsNullOrEmpty(m.LackMaterialsCode?.Trim()))
                    {
                        //XtraMessageBox.Show("缺料代码不能为空!");
                        XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("lackOfMaterialCode")));
                        return false;
                    }
                    if (string.IsNullOrEmpty(m.Id))
                    {
                        var ht = new Hashtable();
                        ht.Add("LackMaterialsCode", m.LackMaterialsCode);
                        var ex = action.CheckIsExist<Domain.LackMaterialsTable>(ht);
                        if (ex)
                        {
                            XtraMessageBox.Show(string.Format("缺料代码【{0}】已存在!", m.LackMaterialsCode.Trim()));
                            return false;
                        }
                    }
                    var pCode = m.LackMaterialsCode?.Trim();
                    try
                    {
                        short pcodeMax = short.Parse(pCode);
                        if (pcodeMax > 255)
                        {
                            XtraMessageBox.Show("缺料代码必须为(0--255之间的)数字!");
                            return false;
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("缺料代码必须为(0--255之间的)数字!");
                        return false;
                    }
                    var exList = list.Where<Domain.LackMaterialsTable>(f => null != f.Id && !f.Id.Equals(m.Id) && f.LackMaterialsCode.Equals(m.LackMaterialsCode)).ToList<Domain.LackMaterialsTable>();
                    if (exList.Count > 0)
                    {
                        XtraMessageBox.Show(string.Format("缺料代码【{0}】已存在!", m.LackMaterialsCode.Trim()));
                        return false;
                    }
                }

                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<Domain.LackMaterialsTable>(m);
                    }
                    else
                    {
                        action.Save<Domain.LackMaterialsTable>(m);
                    }
                }
            }
            return true;
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem(true);
        }
        void AddItem(bool isNewButton = false)
        {
            var dt = susGrid1.DataGrid.DataSource as IList<Domain.LackMaterialsTable>;
            if (dt == null) dt = new List<Domain.LackMaterialsTable>();
            var gv = (susGrid1.DataGrid.MainView as GridView);
            if (isNewButton)
            {
                var newModel = new Domain.LackMaterialsTable();
                // newModel. = dt.Count.ToString();
                dt.Add(newModel);
                susGrid1.DataGrid.MainView.RefreshData();
                return;
            }
            var selectRow = gv?.GetRow(gv.FocusedRowHandle) as Domain.LackMaterialsTable;
            if (null != selectRow)
            {
                var newModel = BeanUitls<Domain.LackMaterialsTable, Domain.LackMaterialsTable>.Mapper(selectRow);
                // newModel.DefectNo = dt.Count.ToString();
                dt.Add(newModel);
                susGrid1.DataGrid.MainView.RefreshData();
            }

        }
    }
}

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
using SuspeSys.Client.Action.Query;
using SuspeSys.Client.Action.Common;
using System.Collections;
using SuspeSys.Client.Action.BasicInfo;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.BasicInfo
{
    /// <summary>
    /// 基本工序库
    /// </summary>
    public partial class BasicProcessLirbaryIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public BasicProcessLirbaryIndex()
        {
            InitializeComponent();
        }
        public BasicProcessLirbaryIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        private void BasicProcessLirbaryIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            Query(1);
            RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            RegisterSusGridEvent();
            RegisterCommconEvent();
            DefaultSet();
        }
        void DefaultSet()
        {
            this.searchControl1.Properties.NullValuePrompt = "输入工序代码及工序名称搜索";
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
            ordercondition.Add("ProcessCode", "Asc");
            var list = basicInfoAction.SearchBasicProcessLirbary(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }

        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true,Name="ProcessName"},
                                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(秒)",FieldName="StanardSecond",Visible=true,Name="StanardSecond"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="Sam",Visible=true,Name="SAM"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单价",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            gridView.CellValueChanged += GridView1_CellValueChanged;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private void GridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;

            var list = susGrid1.DataGrid.DataSource as List<Domain.BasicProcessFlow>;
            var gv = (susGrid1.DataGrid.MainView as SusGridView);
            int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);

            if (e.Column.FieldName.Equals("StanardSecond", StringComparison.OrdinalIgnoreCase))
            {
                decimal statnardS = Convert.ToDecimal(e.Value);
                list[dataIndex].Sam = Math.Round(statnardS / 60, 4).ToString();
                //list[dataIndex].SAM = Math.Round(statnardS / 3600, 2).ToString();
            }

            //if (e.Column.FieldName.Equals("StanardMinute", StringComparison.OrdinalIgnoreCase))
            //{
            //    decimal statnardM = Convert.ToDecimal(e.Value);
            //    list[dataIndex].StanardSecond = Math.Round(statnardM * 60, 2);
            //    list[dataIndex].SAM = Math.Round(statnardM / 60, 2).ToString();
            //}
            if (e.Column.FieldName.Equals("Sam", StringComparison.OrdinalIgnoreCase))
            {
                decimal statnardM = Convert.ToDecimal(e.Value);
                //list[dataIndex].StanardMinute = Math.Round(statnardH * 60, 4);
                list[dataIndex].StanardSecond = Convert.ToInt32( Math.Round(statnardM * 60, 0));
            }
        }

        private void BindData()
        {
            var action = new CommonQueryAction<Domain.BasicProcessFlow>();
            action.GetList();
            susGrid1.DataGrid.DataSource = action.Model.List;
            //var p=new CommonQueryAction<SuspeSys.Domain.ProcessOrder>();
            //p.GetList();

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
                        var sucess=Save();
                        if(sucess)
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem();
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
                //  XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DeleteItem()
        {
            var selectData = (((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.BasicProcessFlow);

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);//XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.BasicProcessFlow>(selectData);
                action.DeleteLog<Domain.BasicProcessFlow>(selectData.Id, "工序代码表", "BasicProcessFlowIndex", selectData.GetType().Name);
                Query(1);
            }
        }
        bool Save()
        {
            var action = new CommonAction();
            //var checkAction = new CommonServiceImpl<>();
            var list = susGrid1.DataGrid.DataSource as List<Domain.BasicProcessFlow>;
            var nullList = list.Where<Domain.BasicProcessFlow>(f => f.ProcessCode == null).ToList<Domain.BasicProcessFlow>();
            if (nullList.Count > 0)
            {
                //XtraMessageBox.Show("工序代码不能为空!");
                XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("ProcessCode")));
                return false;
            }
            nullList = list.Where<Domain.BasicProcessFlow>(f => f.DefaultFlowNo==null).ToList<Domain.BasicProcessFlow>();
            if (nullList.Count > 0)
            {
                //  XtraMessageBox.Show("工序号不能为空!");
                XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("DefaultFlowNo")));
                return false;
            }
            var isDisFlowNo= list.Where<Domain.BasicProcessFlow>(f => f.DefaultFlowNo != null && list.Where(ff=>ff.DefaultFlowNo.Equals(f.DefaultFlowNo)).Count()>1).ToList<Domain.BasicProcessFlow>();
            if (isDisFlowNo.Count > 0)
            {
                XtraMessageBox.Show("工序号不能重复!");
                return false;
            }
            foreach (var m in list)
            {
                if (string.IsNullOrEmpty(m.ProcessCode?.Trim()))
                {
                    //XtraMessageBox.Show("工序代码不能为空!");
                    XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("ProcessCode")));
                    return false;
                }
                var pCode = m.ProcessCode?.Trim();
                try
                {
                    short pcodeMax = short.Parse(pCode);
                    if (pcodeMax>255) {
                        XtraMessageBox.Show("工序代码必须为(0--255之间的)数字!");
                        return false;
                    }
                }
                catch {
                    XtraMessageBox.Show("工序代码必须为(0--255之间的)数字!");
                    return false;
                }
                if (string.IsNullOrEmpty(m.Id))
                {
                    var ht = new Hashtable();
                    ht.Add("ProcessCode", m.ProcessCode);
                    var ex = action.CheckIsExist<Domain.BasicProcessFlow>(ht);
                    if (ex)
                    {
                        XtraMessageBox.Show(string.Format("工序代码【{0}】已存在!",m.ProcessCode.Trim()));
                        return false;
                    }
                }
                var exList = list.Where<Domain.BasicProcessFlow>(f =>null!= f.Id && !f.Id.Equals(m.Id) && f.ProcessCode.Equals(m.ProcessCode)).ToList<Domain.BasicProcessFlow>();
                if (exList.Count>0) {
                    XtraMessageBox.Show(string.Format("工序代码【{0}】已存在!", m.ProcessCode.Trim()));
                    return false;
                }
            }
            if (null != list)
            {
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<Domain.BasicProcessFlow>(m);
                    }
                    else
                    {
                        action.Save<Domain.BasicProcessFlow>(m);
                    }
                }
            }
            return true;
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<Domain.BasicProcessFlow>;
            if (null != dt)
            {
                dt.Add(new Domain.BasicProcessFlow());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }
    }
}

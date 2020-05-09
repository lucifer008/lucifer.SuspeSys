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
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraTab;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Client.Action.BasicInfo;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class StyleProcessLirbaryIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public StyleProcessLirbaryIndex()
        {
            InitializeComponent();

            this.susToolBar1.ShowModifyButton = true;
        }
        public StyleProcessLirbaryIndex(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void BasicProcessLirbaryIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            // BindData();
            Query(1);
            RegisterTooButtonEvent();
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName= "StyleNo",Visible=true},
            new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式",FieldName="StyleName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="SAM",FieldName="Sam",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单价",FieldName="StandardPrice",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsBehavior.Editable = false;
            //gridView.MouseDown += GridView_MouseDown1;
        }

        private void GridView_MouseDown1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                EditStyleProcessFlow(sender as GridView);
            }
        }
        void EditStyleProcessFlow(GridView gv)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (null != gv)
                {
                    var StyleProcessFlow = GetSelectStyleProcessFlow(); //gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
                    if (null == StyleProcessFlow) return;
                    var styleProcessLirbaryInput = new StyleProcessLirbaryInput(ucMain) { Dock = DockStyle.Fill };
                    styleProcessLirbaryInput.ucMain = ucMain;
                    styleProcessLirbaryInput.StyleProcessFlow = StyleProcessFlow;
                    var tab = new XtraTabPage();
                    tab.Name = styleProcessLirbaryInput.Name;
                    tab.Text = string.Format("正在编辑款式工序[{0}]", styleProcessLirbaryInput.StyleProcessFlow.StyleNo?.Trim());
                    if (!ucMain.MainTabControl.TabPages.Contains(tab))
                    {
                        tab.Controls.Add(styleProcessLirbaryInput);
                        ucMain.MainTabControl.TabPages.Add(tab);
                    }
                    ucMain.MainTabControl.SelectedTabPage = tab;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        SuspeSys.Domain.Cus.StyleProcessFlowSectionItemExtModel GetSelectStyleProcessFlow()
        {
            var gv = susGrid1.DataGrid.MainView as GridView;
            if (null == gv) return null;
            var StyleProcessFlow = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.Cus.StyleProcessFlowSectionItemExtModel;
           
            return StyleProcessFlow;
        }
        private void BindData()
        {
            var action = new CommonAction();
            var list=action.GetStyleProcessFlowStore();
            susGrid1.DataGrid.DataSource = list;
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
                        //var sucess = Save();StyleProcessLirbaryInput
                        //if (sucess)
                        //    XtraMessageBox.Show("保存成功!", "提示");

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
                    case ButtonName.Modify:
                        EditStyleProcessFlow( susGrid1.DataGrid.MainView as GridView);
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
       
        private void AddItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();

                tab.Text = string.Format("款式工艺库[{0}]", "");
                tab.Name = string.Format("款式工艺库[{0}]", "");
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new StyleProcessLirbaryInput(ucMain));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        BasicInfoAction basicInfoAction = new BasicInfoAction();
        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
          
            var searchKey = searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = new Dictionary<string,string>();
            ordercondition.Add("StyleNo", "Asc");
            var list = basicInfoAction.SearchStyleProcessFlow(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
    }
}

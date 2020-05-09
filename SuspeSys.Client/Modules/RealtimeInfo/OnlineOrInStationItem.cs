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
using SuspeSys.Client.Action.Customer;
using SuspeSys.Client.Action.Customer.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Client.Action.Products;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraTab;
using SuspeSys.Domain.Ext;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    public partial class OnlineOrInStationItem : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public OnlineOrInStationItem()
        {
            InitializeComponent();
        }
        private string statingNo;
        private string productId;
        private int mainTrackNumber = 0;
        public OnlineOrInStationItem(XtraUserControl1 _ucMain,string _statingNo, string _productId,int _mainTrackNumber=0) : this()
        {
            ucMain = _ucMain;
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            statingNo = _statingNo;
            productId = _productId;
            this.mainTrackNumber = _mainTrackNumber;
            Query(1);
            //RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            RegisterGridEvent();
        }

        private void RegisterGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;

        }

        void Navigation(string hangerNo) {
            XtraTabPage tab = new SusXtraTabPage() ;
            //tab.Text = "";//"衣架信息";
            //tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣架信息"
            //    );
            XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new CoatHangerIndex(ucMain, true));
        }

        void RegisterTooButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }
        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡号",FieldName="CusNo",Visible=true},
 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣架号",FieldName="HangerNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="数量",FieldName="Num",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图",FieldName="LineName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="布匹号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="货卡号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="扎号",FieldName="FlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true},
   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="StatingNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站内",FieldName="InStating",Visible=true,ColumnEdit=new RepositoryItemCheckEdit()}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsBehavior.Editable = true;
            gridView.Columns["HangerNo"].OptionsColumn.AllowEdit = false;
            gridView.DoubleClick += MainView_DoubleClick;
        }
        private void MainView_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                var fieldName = info.Column == null ? "N/A" : info.Column.FieldName;
                //string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                if (fieldName.Equals("HangerNo"))
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        if (null == view) return;
                        var prInfoModel = view.GetRow(view.FocusedRowHandle) as OnlineOrInStationItemModel;
                        if (null == prInfoModel) return;
                        if (string.IsNullOrEmpty(prInfoModel.HangerNo)) return;
                        //if(string.IsNullOrEmpty(info.))
                        XtraTabPage tab = new SusXtraTabPage();
                        var title = prInfoModel.HangerNo;//string.Format("【{0}】站内衣架明细", prInfoModel.StatingNo?.Trim());
                        tab.Text = LanguageAction.Instance.BindLanguageTxt("Billing_CoatHanger")+":"+title;
                        tab.Name = title;// string.Format("【{0}】站内衣架明细", prInfoModel.StatingNo?.Trim());
                        XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new CoatHangerIndex(ucMain, title));
                    }
                    catch (Exception ex)
                    {
                        // log.Error(ex);
                        //XtraMessageBox.Show(ex.Message, "错误");
                        log.Error(ex);
                        XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));

                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

                // MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {

                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;

                    case ButtonName.Max:
                        ucMain.MaxOrMin();
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
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        CustomerAction cutomerAction = new CustomerAction();
        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            //var condition = statingNo;

            IDictionary<string, string> ordercondition = null;

            var list = ProductsAction.Instance.SearchOnlineOrInStationItem(currentPageIndex, pageSize, out totalCount, ordercondition, statingNo, productId,mainTrackNumber);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }
    }
}

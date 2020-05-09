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
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.ProcessOrder;
using DevExpress.XtraGrid;

using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.ProcessFlowChart;
using SuspeSys.Client.Action.Common;
using SuspeSys.Utils.Reflection;

namespace SuspeSys.Client.Modules.ProduceData
{
    /// <summary>
    /// 工艺路线图
    /// </summary>
    [Common.FormPermission(Domain.PermissionConstant.Billing_ProcessFlowChartIndex)]
    public partial class ProcessFlowChartIndex : SusXtraUserControl
    {
        public ProcessFlowChartIndex()
        {
            InitializeComponent();
        }
        public ProcessFlowChartIndex(XtraUserControl1 ucMain) : this()
        {
            processFlowChartMain1.ProcessFlowChartSusToolBar = susToolBar1;
            processFlowChartMain1.ProcessFlowChartIndex = this;
            this.ucMain = ucMain;
            // processFlowChartMain1.ProcessFlowChartSusToolBar = susToolBar1;
            processFlowChartMain1.ucMain = ucMain;
        }
        private DaoModel.ProductsModel ProductsModel;
        public ProcessFlowChartIndex(XtraUserControl1 ucMain, DaoModel.ProductsModel _productsModel) : this()
        {
            this.ucMain = ucMain;
            ProductsModel = _productsModel;
            processFlowChartMain1.ucMain = ucMain;
            processFlowChartMain1.ProcessFlowChartSusToolBar = susToolBar1;
        }
        /// <summary>
        /// 处理连续为不同的版本生成制单工序
        /// </summary>
        /// <param name="ucMain"></param>
        public ProcessFlowChartIndex(XtraUserControl1 ucMain, DaoModel.ProcessOrder _processOrder) : this(ucMain)
        {
            processOrder = _processOrder;
        }
        private DaoModel.ProcessOrder processOrder;
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序版本",FieldName="ProVersionNo",Visible=true,Name="ProVersionNo"}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;

            ProcessOrderQueryAction processOrderQueryAction = new ProcessOrderQueryAction();
            var list = processOrderQueryAction.GetProcessOrderFlowVersionList();
            gc.DataSource = list;
            if (null != processOrder)
            {
                var d = list.Where(q => q.ProcessOrderId.Equals(processOrder.Id)).FirstOrDefault<DaoModel.ProcessFlowVersionModel>();
                BindProcessFlowChartData(d);
            }

        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                popupContainerControl1.OwnerEdit.ClosePopup();
                var gv = gridControl1.MainView as GridView;
                //var selRows = gv.GetSelectedRows();
                //if (0 == selRows.Length) return;

                var downHitInfo = (sender as SusGridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessFlowVersionModel;
                if (null == selecRow) return;
                //var num = new ProcessFlowChartAction().GetCurrentMaxProcessFlowChartNo(selecRow.Id);
                comboProcessOrderNo.Text = string.Format("{0}-{1}", selecRow.POrderNo?.Trim(), selecRow.ProVersionNo?.Trim()).Trim();
                processFlowChartMain1.BindProcessFlowData(selecRow);
            }
        }
        void BindProductsFlowChartData()
        {
            if (null == ProductsModel) return;
            var commAction = new CommonAction();
            //comboProcessOrderNo.Text = string.Format("{0}-{1}", ProductsModel.?.Trim(), ProductsModel.ProVersionNo?.Trim()).Trim();
            var products = commAction.Get<DaoModel.Products>(ProductsModel.Id);
            var processOrder = products.ProcessOrder;
            var pfChart = products?.ProcessFlowChart;
            var pfVersion = commAction.Get<DaoModel.ProcessFlowVersion>(pfChart?.ProcessFlowVersion?.Id);
            comboProcessOrderNo.Text = string.Format("{0}-{1}", ProductsModel.ProcessOrderNo?.Trim(), pfVersion.ProVersionNo?.Trim()).Trim();
            var pfVersionModel = BeanUitls<DaoModel.ProcessFlowVersionModel, DaoModel.ProcessFlowVersion>.Mapper(pfVersion);
            processFlowChartMain1.BindProductProcessFlowChartData(processOrder, ProductsModel, pfVersionModel, pfChart);
            //susToolBar1.Visible = false;
        }
        private void ProcessFlowChartIndex_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindGridHeader(gridControl1);
                BindProductsFlowChartData();
                InitData();
            }
        }
        void BindProcessFlowChartData(DaoModel.ProcessFlowVersionModel dfVersion)
        {
            if (null == dfVersion) return;
            comboProcessOrderNo.Text = dfVersion.POrderNo;
            processFlowChartMain1.BindProcessFlowData(dfVersion);
        }
        private void InitData()
        {
            susToolBar1.ShowDeleteButton = false;
            susToolBar1.ShowModifyButton = false;
            susToolBar1.ShowCancelButton = false;
        }
    }
}

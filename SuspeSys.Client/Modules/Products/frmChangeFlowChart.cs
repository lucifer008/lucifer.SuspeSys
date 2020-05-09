using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Domain;
using SuspeSys.Client.Action.Products;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.SuspeRemotingClient;

namespace SuspeSys.Client.Modules.Products
{
    public partial class frmChangeFlowChart : DevExpress.XtraEditors.XtraForm
    {
        public frmChangeFlowChart()
        {
            InitializeComponent();
        }

        public frmChangeFlowChart(Domain.ProductsModel row):this()
        {
            this.row = row;
        }

        private void frmChangeFlowChart_Load(object sender, EventArgs e)
        {
            BindData();
        }
        DevExpress.XtraEditors.PopupContainerControl pccFlowChart;
        private void BindData()
        {
            pccFlowChart=new PopupContainerControl();
            var gdFlowChart = new DevExpress.XtraGrid.GridControl();
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺图",FieldName="LinkName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图说明",FieldName="Remark",Visible=true}
            });
            gdFlowChart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdFlowChart.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdFlowChart;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;

            gdFlowChart.Dock = System.Windows.Forms.DockStyle.Fill;
            gdFlowChart.Location = new System.Drawing.Point(0, 0);
            gdFlowChart.MainView = gridView;
            gdFlowChart.Name = "gdFlowChart";
            gdFlowChart.Size = new System.Drawing.Size(320, 115);
            gdFlowChart.TabIndex = 0;
            gdFlowChart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            var sql = string.Format(@"
select 
distinct PFC.* 
from ProcessOrder PO
INNER Join ProcessFlowVersion PF On PF.PROCESSORDER_Id=PO.Id
INNER JOin ProcessFlowChart PFC on PFC.PROCESSFLOWVERSION_Id=PF.Id
where po.id=@ProcessOrderId
");
            var list = CommonAction.GetList<Domain.ProcessFlowChart>(sql,new { ProcessOrderId =row.ProcessOrderId });
            gdFlowChart.DataSource = list;
            pccFlowChart.Controls.Add(gdFlowChart);

            //if (!string.IsNullOrEmpty(employeeName))
            //{
            //    list = list.Where(f => f.RealName.Contains(employeeName)).ToList<Domain.Employee>();
            //}
            cbFlowChart.Properties.PopupControl = pccFlowChart;


            var gvFlowChartStatInfo = gridControl1.MainView as GridView;

            gvFlowChartStatInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="加工顺序",FieldName="CraftFlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点数量",FieldName="StatingTotal",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点列表",FieldName="Statings",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gvFlowChartStatInfo});
            gridControl1.MainView = gvFlowChartStatInfo;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gvFlowChartStatInfo.BestFitColumns();//按照列宽度自动适配
            gvFlowChartStatInfo.GridControl = gridControl1;
            gvFlowChartStatInfo.OptionsView.ShowFooter = false;
            gvFlowChartStatInfo.OptionsView.ShowGroupPanel = false;
            gvFlowChartStatInfo.OptionsBehavior.Editable = false;
        }
        ProductsAction productAction = new ProductsAction();
        private Domain.ProductsModel row;

        public bool IsSaveSucess { get; internal set; }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    pccFlowChart.OwnerEdit.ClosePopup();
                    var csl = cbFlowChart.Properties.PopupControl.Controls;
                    if (csl.Count > 0)
                    {
                        var ggv = (sender as SusGridView);
                        var downHitInfo =(sender as SusGridView).CalcHitInfo(new Point(e.X, e.Y));
                        var selecRow = ggv.GetRow(downHitInfo.RowHandle) as SuspeSys.Domain.ProcessFlowChart;
                        if (null == selecRow) return;
                        cbFlowChart.Text = selecRow.LinkName?.Trim();
                        productAction.SearchProcessFlowChartFlowModelList(selecRow.Id);
                        gridControl1.DataSource = productAction.Model.ProcessFlowChartFlowModelList;
                        cbFlowChart.Text = selecRow.LinkName;
                        cbFlowChart.Tag = selecRow;

                    }
                  
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Cursor = Cursors.WaitCursor;
                var t = cbFlowChart.Tag as SuspeSys.Domain.ProcessFlowChart;
                if (t==null) {
                    XtraMessageBox.Show("请选择工艺图!", "温馨提示");
                    return;
                }
                var sql = string.Format("update Products Set LineName=@LineName,PROCESSFLOWCHART_Id=@ProcessFlowChartId where Id=@Id");
                var rows = CommonAction.Instance.Update<SuspeSys.Domain.Products>(sql, new { ProcessFlowChartId = t.Id, Id = row.Id, LineName = t.LinkName?.Trim() });
                if (rows>0) {
                    SuspeRemotingService.statingService.LoadOnLineProductsFlowChart(t.Id);
                    XtraMessageBox.Show("工艺图改变成功!", "温馨提示");
                    this.IsSaveSucess = true;
                    this.Close();
                }
               // product.ProcessFlowChart=
            }
            finally
            {
                this.btnOk.Cursor = Cursors.Default;
            }
        }
    }
}
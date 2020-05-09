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
using DevExpress.XtraGrid;
using System.Windows.Forms.DataVisualization.Charting;
using SuspeSys.Client.Action.Report;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Utils;

namespace SuspeSys.Client.Modules.Reports
{
    /// <summary>
    /// 疵点分析图
    /// </summary>
    public partial class DefectAnalysisReport : SusXtraUserControl
    {
        public DefectAnalysisReport()
        {
            InitializeComponent();
        }
        public DefectAnalysisReport(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susQueryControl1.Visible = false;
            susToolBar1.ShowExportButton = false;
            susToolBar1.ShowPrintButton = false;
        }
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                       // Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Query:
                        susQueryControl1.Visible = !susQueryControl1.Visible;
                        break;

                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                Common.Utils.SusTransitionManager.EndTransition();
            }
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
             //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="疵品原因",FieldName="DefectName",Visible=true,Name="DefectName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="不良数量",FieldName="DefectCount",Visible=true,Name="DefectCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="不良比例",FieldName="DefectRate",Visible=true,ColumnEdit=new RepositoryItemTextEdit() {
                    
                } ,Name="DefectRate"}
            });
            //(gridView.Columns[3].RealColumnEdit as RepositoryItemTextEdit).DisplayFormat.FormatString = "P3";
            //(gridView.Columns[3].RealColumnEdit as RepositoryItemTextEdit).Mask.EditMask = "P3";
            foreach (DevExpress.XtraGrid.Columns.GridColumn item in gridView.Columns)
            {
                item.OptionsColumn.AllowEdit = false;
            }
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            //gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowFooter = true;
            DevExpress.XtraGrid.Columns.GridColumn col_Profit = gridView.Columns[1];
            gridView.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView.Columns[1].SummaryItem.DisplayFormat = "合计：{0：n2}";
        }

        private void DefectAnalysisReport_Load(object sender, EventArgs e)
        {
            BindGridHeader(gridControl1);
           // BindPieceData(queryModel);
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
        }
        QueryModel queryModel;
        private void SusQueryControl1_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            BindPieceData( queryModel);
        }
      
        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        private void BindPieceData(QueryModel queryModel)
        {
            var searchKey = string.Empty;
            //var searchCondition = string.Empty;
            if (null != queryModel)
            {
                if (!string.IsNullOrEmpty(queryModel.ProcessOrderNo))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE ProcessOrderNo like '%{0}%')", queryModel.ProcessOrderNo?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.PO))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE PO like '%{0}%')", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusColor))
                {
                    searchKey += string.Format(" AND T1.PColor like '%{0}%'", queryModel.SusColor?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusSize))
                {
                    searchKey += string.Format(" AND T1.PSize like '%{0}%'", queryModel.SusSize?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusStyle))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE StyleNo like '%{0}%')", queryModel.SusStyle?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.Workshop))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", queryModel.Workshop?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.FlowSelection))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE FlowSection like '%{0}%')", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.GroupNo))
                {
                    searchKey += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", queryModel.GroupNo?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.BeginDate))
                {
                    searchKey += string.Format(" AND T1.InsertDateTime>={0}", queryModel.BeginDate.FormatDBValue());
                }
                if (!string.IsNullOrEmpty(queryModel.EndDate))
                {
                    searchKey += string.Format(" AND T1.InsertDateTime<={0}", queryModel.EndDate.FormatDBValue());
                }
            }
            var model = reportQueryAction.SearchDefectAnalysisReport(searchKey);
            gridControl1.DataSource = model.DefectAnalysisReportDetailModelList;


            double[] yValues = model.DefectAnalysisReportDetailModelList.Select(f =>double.Parse(f.DefectRate.Replace("%",""))*100).ToArray();// { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValues = model.DefectNameList.ToArray();// { "France", "Canada", "Germany", "USA", "Italy" };
            chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].IsVisibleInLegend = true;
            chart1.Series["Series1"].Label  = "#PERCENT{P2}";

            // Set Doughnut chart type
            //chart1.Series["Series1"].ChartType = SeriesChartType.Doughnut;

            // Set labels style
            //  chart1.Series["Series1"]["PieLabelStyle"] = "Outside";

            // Set Doughnut radius percentage
            //chart1.Series["Series1"]["DoughnutRadius"] = "30";

            // Explode data point with label "Italy"
            //chart1.Series["Series1"].Points[4]["Exploded"] = "true";

            //// Enable 3D
            //chart1.ChartAreas["Series1"].Area3DStyle.Enable3D = true;

            //// Set drawing style
            //chart1.Series["Series1"]["PieDrawingStyle"] = "SoftEdge";

        }
    }
}

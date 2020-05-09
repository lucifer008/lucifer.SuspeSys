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
using SuspeSys.Client.Action.Report;
using SuspeSys.Utils;
using DevExpress.XtraPrinting;
using SuspeSys.Domain.Ext.ReportModel;

namespace SuspeSys.Client.Modules.Reports
{
    /// <summary>
    /// 返工汇总
    /// </summary>
    public partial class ReworkCollAndDefectAnalysisReport : SusXtraUserControl
    {
        public ReworkCollAndDefectAnalysisReport()
        {
            InitializeComponent();
        }
        public ReworkCollAndDefectAnalysisReport(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void ReworkCollAndDefectAnalysisReport_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susQueryControl1.Visible = false;
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
           // Query(1,queryModel);
        }
        QueryModel queryModel;
        private void SusQueryControl1_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            Query(1, queryModel);
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
                        //Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Query:
                        susQueryControl1.Visible = !susQueryControl1.Visible;
                        break;
                    case ButtonName.Print:
                        PrintData();
                        break;
                    case ButtonName.Export:
                        PrintData();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
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
        private void PrintData()
        {
            susToolBar1.GetButton(ButtonName.Print).Cursor = Cursors.WaitCursor;
            try
            {
                //           DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
                //           DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
                //           //设置左右间距
                //           compositeLink.Margins.Left = 10;
                //           compositeLink.Margins.Right = 10;
                //           ///设置页眉

                //           PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                //           phf.Header.Content.Clear();
                //           phf.Header.Content.AddRange(new string[] { "", "员工产量报表", "" });
                //           phf.Header.LineAlignment = BrickAlignment.Center;
                //           phf.Header.Font = new Font("黑体", 22, FontStyle.Bold);

                //           ps.Graph.ForeColor = Color.Red;//.Font = new Font("黑体", 10, FontStyle.Bold);
                //           compositeLink.PrintingSystem = ps;
                //           compositeLink.Landscape = false;  //横向

                //           compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;   //纸张
                //           DevExpress.XtraPrinting.PrintableComponentLink link = new
                //DevExpress.XtraPrinting.PrintableComponentLink(ps);




                //           ps.PageSettings.Landscape = false;   //横向

                //           link.Component = susGrid1.DataGrid;
                //           compositeLink.Links.Add(link);

                //           // ps.PageSettings.Assign(new Margins(0, 0, 0, 0), new Margins(0, 0, 0, 0), PaperKind.Custom, new Size(300, 400), false);

                //           // ps.PageSettings.RightMargin = 0;

                //           link.CreateDocument();  //建立文档
                //           ps.PreviewFormEx.Show();//进行预览  
                //                                   //  ps.Print();
                var gccPrint = new GridControl();
                var gridView = GetView();
                gccPrint.MainView = gridView;

                gccPrint.DataSource = Query(queryModel);

                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(ps);
                link.Component = gccPrint;
                link.Landscape = true;
                PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                phf.Header.Content.Clear();
                phf.Header.Content.AddRange(new string[] { "", "返工汇总报表", "" });
                phf.Header.Font = new System.Drawing.Font("宋体", 16, System.Drawing.FontStyle.Regular);
                phf.Header.LineAlignment = BrickAlignment.Center;
                phf.Footer.Content.Clear();
                phf.Footer.Content.AddRange(new string[] { "", String.Format("打印时间: {0:g}", DateTime.Now), "" });
                link.CreateDocument();
                link.ShowPreviewDialog();
                susToolBar1.Controls.Remove(gccPrint);
            }
            finally
            {
                susToolBar1.GetButton(ButtonName.Print).Cursor = Cursors.Default;
            }


        }

        private IList<ReworkCollAndDefectAnalysisReportModel> Query(QueryModel queryModel)
        {
            IDictionary<string, string> ordercondition = null;
            var searchKey = string.Empty; //searchControl1.Text?.Trim();
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
            var list = reportQueryAction.SearchReworkCollAndDefectAnalysisReport( ordercondition, searchKey);
            return list;
        }

        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            SusGridView gridView = GetView();

            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private static SusGridView GetView()
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="时间",FieldName="InspectionDate",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true,Name="ProcessName"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="产量",FieldName="Yield",Visible=true,Name="OutYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工量",FieldName="ReworkYield",Visible=true,Name="ReturnYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工率",FieldName="RewokRate",Visible=true,Name="ReturnRate"}
            });

            foreach (DevExpress.XtraGrid.Columns.GridColumn item in gridView.Columns)
            {
                item.OptionsColumn.AllowEdit = false;
            }

            return gridView;
        }

        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        void Query(int currentPageIndex, QueryModel _queryModel)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var searchKey = string.Empty; //searchControl1.Text?.Trim();
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
            var list = reportQueryAction.SearchReworkCollAndDefectAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }
    }
}

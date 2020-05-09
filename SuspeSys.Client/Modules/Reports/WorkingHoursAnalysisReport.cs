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
using DevExpress.XtraPrinting;
using SuspeSys.Domain.Ext.ReportModel;

namespace SuspeSys.Client.Modules.Reports
{
    /// <summary>
    /// 工时分析报表
    /// </summary>
    public partial class WorkingHoursAnalysisReport : SusXtraUserControl
    {
        public WorkingHoursAnalysisReport()
        {
            InitializeComponent();
            susToolBar1.Load += SusToolBar1_Load;
        }
        public WorkingHoursAnalysisReport(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }
        QueryModel queryModel;
        private void SusQueryControl1_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            Query(1, queryModel);
        }
        private void SusToolBar1_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susQueryControl1.Visible = false;
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
           // Query(1,queryModel);
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
                        Query(1, queryModel);
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
               
                susToolBar1.Controls.Add(gccPrint);
                var gridView = GetView();
                gccPrint.MainView = gridView;
                gccPrint.DataSource = Query(queryModel);

                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(ps);
                link.Component = gccPrint;
                link.Landscape = true;
                PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                phf.Header.Content.Clear();
                phf.Header.Content.AddRange(new string[] { "", "工时分析报表", "" });
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
        /// <summary>
        /// 工时分析报表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private IList<WorkingHoursAnalysisReportModel> Query(QueryModel queryModel)
        {
            var searchKey = string.Empty;
            IDictionary<string, string> ordercondition = null;
            var processOrderNo = queryModel?.ProcessOrderNo?.Trim();// txtProcessOrderNo.Text.Trim();
            string styleCode = queryModel?.SusStyle?.Trim(); //txtStyleCode.Text?.Trim();
            string PO = queryModel?.PO?.Trim(); //txtPO.Text.Trim();
            string flowSelection = queryModel?.FlowSelection?.Trim(); //txtFlowSelection.Text.Trim();
            string color = queryModel?.SusColor?.Trim();// txtColor.Text.Trim();
            string size = queryModel?.SusSize?.Trim(); //txtSize.Text.Trim();
            string workshop = queryModel?.Workshop?.Trim(); //txtWorkshop.Text.Trim();
            string groupNo = queryModel?.GroupNo?.Trim(); //txtGroupNo.Text.Trim();
            string beginDate = queryModel?.BeginDate?.Trim();// dateBegin.Text.Trim();
            string endDate = queryModel?.EndDate?.Trim(); //dateEnd.Text.Trim();
            var list = reportQueryAction.SearchWorkingHoursAnalysisReport( ordercondition, processOrderNo, styleCode, PO, flowSelection, color, size, workshop, groupNo, beginDate, endDate);
            return list;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex, queryModel);
        }

        //private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{

        //}
        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        private void BindGridHeader(GridControl gc)
        {
            SusGridView gridView = GetView();

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
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private static SusGridView GetView()
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="加工顺序",FieldName="FlowIndex",Visible=true,Name="FlowIndex"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessFlowCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessFlowName",Visible=true,Name="ProcessName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工",FieldName="EmployeeName",Visible=true,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(秒)",FieldName="StanardHours",Visible=true,Name="StanardHoursBSecond"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="平均用时(秒)",FieldName="AvgTimes",Visible=true,Name="AvgTimes"}

            });
            return gridView;
        }

        void Query(int currentPageIndex, QueryModel queryModel)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey = string.Empty;
            IDictionary<string, string> ordercondition = null;
            var processOrderNo = queryModel?.ProcessOrderNo?.Trim();// txtProcessOrderNo.Text.Trim();
            string styleCode = queryModel?.SusStyle?.Trim(); //txtStyleCode.Text?.Trim();
            string PO = queryModel?.PO?.Trim(); //txtPO.Text.Trim();
            string flowSelection = queryModel?.FlowSelection?.Trim(); //txtFlowSelection.Text.Trim();
            string color = queryModel?.SusColor?.Trim();// txtColor.Text.Trim();
            string size = queryModel?.SusSize?.Trim(); //txtSize.Text.Trim();
            string workshop = queryModel?.Workshop?.Trim(); //txtWorkshop.Text.Trim();
            string groupNo = queryModel?.GroupNo?.Trim(); //txtGroupNo.Text.Trim();
            string beginDate = queryModel?.BeginDate?.Trim();// dateBegin.Text.Trim();
            string endDate = queryModel?.EndDate?.Trim(); //dateEnd.Text.Trim();
            var list = reportQueryAction.SearchWorkingHoursAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, processOrderNo, styleCode, PO, flowSelection, color, size, workshop, groupNo, beginDate, endDate);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }

        //private void btnQuery_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //       // btnQuery.Cursor = Cursors.WaitCursor;
        //        Query(1);
        //    }
        //    finally
        //    {
        //       // btnQuery.Cursor = Cursors.Default;
        //    }
        //}

        //private void rgDateRange_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindDateRange();
        //}
        //private void BindDateRange()
        //{
        //    var sValue = rgDateRange.Properties.Items[rgDateRange.SelectedIndex].Value?.ToString();
        //    switch (sValue)
        //    {
        //        case "-1"://全部
        //            dateBegin.Text = string.Empty;
        //            dateEnd.Text = string.Empty;
        //            break;
        //        case "0"://今天
        //            dateBegin.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //            dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "Yesterday":
        //            dateBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        //            dateEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "LatelyThreeDay":
        //            dateBegin.Text = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
        //            dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "CurrentWeek":
        //            dateBegin.Text = GetWeekFirstDayMon(DateTime.Now).ToString("yyyy-MM-dd");
        //            dateEnd.Text = GetWeekLastDaySun(DateTime.Now).ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "CurrentMonth":
        //            dateBegin.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-01");
        //            dateEnd.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "LastWeek":
        //            dateBegin.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).ToString("yyyy-MM-dd");
        //            dateEnd.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).AddDays(6).ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //        case "LastMonth":
        //            dateBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
        //            dateEnd.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
        //            break;
        //    }
        //}
        //public DateTime GetWeekFirstDayMon(DateTime datetime)
        //{
        //    //星期一为第一天  
        //    int weeknow = Convert.ToInt32(datetime.DayOfWeek);

        //    //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
        //    weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
        //    int daydiff = (-1) * weeknow;

        //    //本周第一天  
        //    string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
        //    return Convert.ToDateTime(FirstDay);
        //}
        ///// <summary>  
        ///// 得到本周最后一天(以星期天为最后一天)  
        ///// </summary>  
        ///// <param name="datetime"></param>  
        ///// <returns></returns>  
        //public DateTime GetWeekLastDaySun(DateTime datetime)
        //{
        //    //星期天为最后一天  
        //    int weeknow = Convert.ToInt32(datetime.DayOfWeek);
        //    weeknow = (weeknow == 0 ? 7 : weeknow);
        //    int daydiff = (7 - weeknow);

        //    //本周最后一天  
        //    string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
        //    return Convert.ToDateTime(LastDay);
        //}
    }
}

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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using SuspeSys.Utils;
using DevExpress.XtraPrinting;
using SuspeSys.Domain.Ext.ReportModel;
using DevExpress.XtraReports.UI;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.Reports
{
    public partial class EmployeeYieldReport : SusXtraUserControl
    {
        /// <summary>
        /// 员工产量报表
        /// </summary>
        public EmployeeYieldReport()
        {
            InitializeComponent();

            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowDeleteButton = false;
        }
        public EmployeeYieldReport(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            BindData();
        }

        private void BindData()
        {
            #region 透视图组件初始化
            xtraTabPage1.Text = LanguageAction.Instance.BindLanguageTxt("TableView");//"表格形式";
            xtraTabPage1.Name = "TableView";
            xtraTabPage2.Text = LanguageAction.Instance.BindLanguageTxt("PerspectiveView");//"透视图";
            xtraTabPage2.Name = "PerspectiveView";

            this.pgfPO = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFlowSection = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStanardHours = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfProcessOrderNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCardNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfEmployeeName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgffProcessFlowName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfYieldCount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfIncome = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStyleNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfColor = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSize = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStatingNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfProcessFlowName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFlowIndex = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRealyWorkMin = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSeamsRate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfReworkRate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfReworkCount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.RealyWorkMin = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfProcessFlowCode = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFlowNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStandardPrice = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfPO,
            this.pgfDate,
            this.pgfFlowSection,
            this.pgfStanardHours,
            this.pgfProcessOrderNo,
            this.pgfCardNo,
            this.pgfEmployeeName,
            this.pgffProcessFlowName,
            this.pgfYieldCount,
            this.pgfIncome,
            this.pgfStyleNo,
            this.pgfColor,
            this.pgfSize,
            this.pgfStatingNo,
            this.pgfProcessFlowName,
            this.pgfFlowIndex,
            this.pgfRealyWorkMin,
            this.pgfSeamsRate,
            this.pgfReworkRate,
            this.pgfReworkCount,
            this.pgfProcessFlowCode,
            this.pgfFlowNo,
            this.pgfStandardPrice});
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(1429, 653);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // pgfPO
            // 
            this.pgfPO.AreaIndex = 2;
            this.pgfPO.Caption = "PO";
            this.pgfPO.FieldName = "PurchaseOrderNo";
            this.pgfPO.Name = "pgfPO";
            // 
            // pgfDate
            // 
            this.pgfDate.AreaIndex = 0;
            this.pgfDate.Caption = "日期";
            this.pgfDate.FieldName = "InsertDateTime";
            this.pgfDate.Name = "pgfDate";
            // 
            // pgfFlowSection
            // 
            this.pgfFlowSection.AreaIndex = 3;
            this.pgfFlowSection.Caption = "工段";
            this.pgfFlowSection.FieldName = "FlowSection";
            this.pgfFlowSection.Name = "FlowSection1";
            // 
            // pgfStanardHours
            // 
            this.pgfStanardHours.AreaIndex = 8;
            this.pgfStanardHours.Caption = "标准工时";
            this.pgfStanardHours.FieldName = "StanardHours";
            this.pgfStanardHours.Name = "SAM";
            // 
            // pgfProcessOrderNo
            // 
            this.pgfProcessOrderNo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfProcessOrderNo.AreaIndex = 0;
            this.pgfProcessOrderNo.Caption = "制单号";
            this.pgfProcessOrderNo.FieldName = "ProcessOrderNo";
            this.pgfProcessOrderNo.Name = "ProcessOrderNo1";
            // 
            // pgfCardNo
            // 
            this.pgfCardNo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCardNo.AreaIndex = 1;
            this.pgfCardNo.Caption = "工号";
            this.pgfCardNo.FieldName = "EmployeeNo";
            this.pgfCardNo.Name = "Code";
            // 
            // pgfEmployeeName
            // 
            this.pgfEmployeeName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfEmployeeName.AreaIndex = 2;
            this.pgfEmployeeName.Caption = "姓名";
            this.pgfEmployeeName.FieldName = "EmployeeName";
            this.pgfEmployeeName.Name = "RealName";
            // 
            // pgffProcessFlowName
            // 
            this.pgffProcessFlowName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgffProcessFlowName.AreaIndex = 4;
            this.pgffProcessFlowName.Caption = "工序名称";
            this.pgffProcessFlowName.FieldName = "ProcessFlowName";
            this.pgffProcessFlowName.Name = "ProcessName";
            // 
            // pgfYieldCount
            // 
            this.pgfYieldCount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfYieldCount.AreaIndex = 0;
            this.pgfYieldCount.Caption = "产出量";
            this.pgfYieldCount.FieldName = "YieldCount";
            this.pgfYieldCount.Name = "OutYield";
            this.pgfYieldCount.Width = 126;
            // 
            // pgfIncome
            // 
            this.pgfIncome.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfIncome.AreaIndex = 1;
            this.pgfIncome.Caption = "收入";
            this.pgfIncome.FieldName = "Income";
            this.pgfIncome.Name = "Income1";
            // 
            // pgfStyleNo
            // 
            this.pgfStyleNo.AreaIndex = 1;
            this.pgfStyleNo.Caption = "款号";
            this.pgfStyleNo.FieldName = "StyleNo";
            this.pgfStyleNo.Name = "StyleNo1";
            // 
            // pgfColor
            // 
            this.pgfColor.AreaIndex = 4;
            this.pgfColor.Caption = "颜色";
            this.pgfColor.FieldName = "PColor";
            this.pgfColor.Name = "ColorValue";
            // 
            // pgfSize
            // 
            this.pgfSize.AreaIndex = 5;
            this.pgfSize.Caption = "尺码";
            this.pgfSize.FieldName = "PSize";
            this.pgfSize.Name = "SizeDesption";
            // 
            // pgfStatingNo
            // 
            this.pgfStatingNo.AreaIndex = 6;
            this.pgfStatingNo.Caption = "站点";
            this.pgfStatingNo.FieldName = "SiteNo";
            this.pgfStatingNo.Name = "StatingName";
            // 
            // pgfProcessFlowName
            // 
            this.pgfProcessFlowName.AreaIndex = 7;
            this.pgfProcessFlowName.Caption = "工序信息";
            this.pgfProcessFlowName.FieldName = "ProcessFlowName";
            this.pgfProcessFlowName.Name = "FlowInfo";
            // 
            // pgfFlowIndex
            // 
            this.pgfFlowIndex.AreaIndex = 9;
            this.pgfFlowIndex.Caption = "工序顺序";
            this.pgfFlowIndex.FieldName = "FlowIndex";
            this.pgfFlowIndex.Name = "FlowIndex1";
            // 
            // pgfRealyWorkMin
            // 
            this.pgfRealyWorkMin.AreaIndex = 10;
            this.pgfRealyWorkMin.Caption = "实时总工时(分钟)";
            this.pgfRealyWorkMin.FieldName = "RealyWorkMin";
            this.pgfRealyWorkMin.Name = "ReailHours";
            // 
            // pgfSeamsRate
            // 
            this.pgfSeamsRate.AreaIndex = 11;
            this.pgfSeamsRate.Caption = "车缝效率";
            this.pgfSeamsRate.FieldName = "SeamsRate";
            this.pgfSeamsRate.Name = "SeamsEfficiencySite";
            // 
            // pgfReworkRate
            // 
            this.pgfReworkRate.AreaIndex = 12;
            this.pgfReworkRate.Caption = "返工率";
            this.pgfReworkRate.FieldName = "ReworkRate";
            this.pgfReworkRate.Name = "ReturnRate1";
            // 
            // pgfReworkCount
            // 
            this.pgfReworkCount.AreaIndex = 13;
            this.pgfReworkCount.Caption = "返工数量";
            this.pgfReworkCount.FieldName = "ReworkCount";
            this.pgfReworkCount.Name = "ReturnYield";
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.AreaIndex = 4;
            this.pivotGridField1.Caption = "StanardHours";
            this.pivotGridField1.FieldName = "StanardHours";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.AreaIndex = 9;
            this.pivotGridField2.Caption = "工序顺序";
            this.pivotGridField2.Name = "FlowIndex1";
            // 
            // RealyWorkMin
            // 
            this.RealyWorkMin.AreaIndex = 10;
            this.RealyWorkMin.Caption = "实时总工时(分钟)";
            this.RealyWorkMin.Name = "RealyWorkMin";
            // 
            // pgfProcessFlowCode
            // 
            this.pgfProcessFlowCode.AreaIndex = 14;
            this.pgfProcessFlowCode.Caption = "工序代码";
            this.pgfProcessFlowCode.FieldName = "ProcessFlowCode";
            this.pgfProcessFlowCode.Name = "ProcessCode";
            // 
            // pgfFlowNo
            // 
            this.pgfFlowNo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFlowNo.AreaIndex = 3;
            this.pgfFlowNo.Caption = "工序号";
            this.pgfFlowNo.FieldName = "FlowNo";
            this.pgfFlowNo.Name = "DefaultFlowNo";
            // 
            // pgfStandardPrice
            // 
            this.pgfStandardPrice.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfStandardPrice.AreaIndex = 5;
            this.pgfStandardPrice.Caption = "工价";
            this.pgfStandardPrice.FieldName = "StandardPrice";
            this.pgfStandardPrice.Name = "StandardPrice1";
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField5.AreaIndex = 0;
            this.pivotGridField5.Caption = "ProcessOrderNo";
            this.pivotGridField5.FieldName = "ProcessOrderNo";
            this.pivotGridField5.Name = "ProcessOrderNo1";
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField6.AreaIndex = 1;
            this.pivotGridField6.Caption = "CardNo";
            this.pivotGridField6.FieldName = "CardNo";
            this.pivotGridField6.Name = "CardNo1";
            // 
            // pivotGridField7
            // 
            this.pivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField7.AreaIndex = 2;
            this.pivotGridField7.Caption = "EmployeeName";
            this.pivotGridField7.FieldName = "EmployeeName";
            this.pivotGridField7.Name = "pivotGridField7";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.AreaIndex = 7;
            this.pivotGridField3.Caption = "工序信息";
            this.pivotGridField3.FieldName = "ProcessFlowName";
            this.pivotGridField3.Name = "FlowInfo";
            #endregion

            xtraTabControl1.TabIndexChanged += XtraTabControl1_TabIndexChanged;
        }

        private void XtraTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1, queryModel);
        }
        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        void Query(int currentPageIndex, QueryModel queryModel)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey = string.Empty;
            IDictionary<string, string> ordercondition = null;
            //var searchCondition = string.Empty;
            if (null != queryModel)
            {
                if (!string.IsNullOrEmpty(queryModel.ProcessOrderNo))
                {
                    searchKey += string.Format(" AND T1.ProcessOrderNo like '%{0}%'", queryModel.ProcessOrderNo?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.PO))
                {
                    searchKey += string.Format(" AND T2.CustPurchaseOrderNo like '%{0}%'", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusColor))
                {
                    searchKey += string.Format(" AND T5.PColor like '%{0}%'", queryModel.SusColor?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusSize))
                {
                    searchKey += string.Format(" AND T5.PSize like '%{0}%'", queryModel.SusSize?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusStyle))
                {
                    searchKey += string.Format(" AND T5.StyleNo like '%{0}%'", queryModel.SusStyle?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.Workshop))
                {
                    searchKey += string.Format(" AND T1.GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%')", queryModel.Workshop?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.FlowSelection))
                {
                    searchKey += string.Format(" AND T5.FlowSection like '%{0}%'", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.GroupNo))
                {
                    searchKey += string.Format(" AND T1.GroupNO in ({0})", string.Concat("'", string.Join("','", queryModel.GroupNo?.Trim().Split(',')), "'"));
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
            // IDictionary<string, string> ordercondition = null;
            var list = reportQueryAction.SearchEmployeeYield(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            var listPrv = reportQueryAction.SearchEmployeeYield(ordercondition, searchKey);
            pivotGridControl1.DataSource = listPrv;
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }
        private void susToolBar1_Load(object sender, EventArgs e)
        {
            //this.searchControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchControl1_KeyDown);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
            BindGridHeader(susGrid1.DataGrid);
            susQueryControl1.Visible = false;
           // Query(1, queryModel);
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
        IList<EmployeeYieldReportModel> Query(QueryModel queryModel)
        {
            var searchKey = string.Empty;
            IDictionary<string, string> ordercondition = null;
            //var searchCondition = string.Empty;
            if (null != queryModel)
            {
                if (!string.IsNullOrEmpty(queryModel.ProcessOrderNo))
                {
                    searchKey += string.Format(" AND T1.ProcessOrderNo like '%{0}%'", queryModel.ProcessOrderNo?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.PO))
                {
                    searchKey += string.Format(" AND T2.CustPurchaseOrderNo like '%{0}%'", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusColor))
                {
                    searchKey += string.Format(" AND T5.PColor like '%{0}%'", queryModel.SusColor?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusSize))
                {
                    searchKey += string.Format(" AND T5.PSize like '%{0}%'", queryModel.SusSize?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.SusStyle))
                {
                    searchKey += string.Format(" AND T5.StyleNo like '%{0}%'", queryModel.SusStyle?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.Workshop))
                {
                    searchKey += string.Format(" AND T5.GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%')", queryModel.Workshop?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.FlowSelection))
                {
                    searchKey += string.Format(" AND T5.FlowSection like '%{0}%'", queryModel.PO?.Trim());
                }
                if (!string.IsNullOrEmpty(queryModel.GroupNo))
                {
                    searchKey += string.Format(" AND T5.GroupNO like '%{0}%'", queryModel.GroupNo?.Trim());
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
            // IDictionary<string, string> ordercondition = null;
            var list = reportQueryAction.SearchEmployeeYield(ordercondition, searchKey);
            return list;
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
                //var gccPrint = new GridControl();
                //susToolBar1.Controls.Add(gccPrint);
                //SusGridView gridView = GetView();
                //gccPrint.MainView = gridView;
                //gccPrint.DataSource = Query(queryModel);

                //PrintingSystem ps = new PrintingSystem();
                //PrintableComponentLink link = new PrintableComponentLink(ps);
                //link.Component = gccPrint;
                //link.Landscape = true;
                //PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                //phf.Header.Content.Clear();
                //phf.Header.Content.AddRange(new string[] { "", "员工产量报表", "" });
                //phf.Header.Font = new System.Drawing.Font("宋体", 16, System.Drawing.FontStyle.Regular);
                //phf.Header.LineAlignment = BrickAlignment.Center;
                //phf.Footer.Content.Clear();
                //phf.Footer.Content.AddRange(new string[] { "", String.Format("打印时间: {0:g}", DateTime.Now), "" });
                //link.CreateDocument();
                //link.ShowPreviewDialog();
                //susToolBar1.Controls.Remove(gccPrint);
                var list = Query(queryModel);
                var reportEmployeeYield = new ReportEmployeeYield();
                reportEmployeeYield.ReportBindSource.DataSource = list;
                reportEmployeeYield.ShowPreview();
            }
            finally
            {
                susToolBar1.GetButton(ButtonName.Print).Cursor = Cursors.Default;
            }


        }

        private static SusGridView GetView()
        {
            var gridView = new Ext.SusGridView();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true,Name="InsertDateTime"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="GroupSites",Visible=true,Name="StatingName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工",FieldName="EmployeeName",Visible=true,Name="RealName"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="EmployeeNo",Visible=true,Name="EmployeeNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true,Name="ProcessOrderNo"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true,Name="ColorValue"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true,Name="SizeDesption"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO号",FieldName="PurchaseOrderNo",Visible=true,Name="PO"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工段",FieldName="FlowSection",Visible=true,Name="FlowSection"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessFlowCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessFlowName",Visible=true,Name="ProcessName"},

                 
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="WorkShop",Visible=true,Name="workshop"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="产出量",FieldName="YieldCount",Visible=true,Name="OutYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工量",FieldName="ReworkCount",Visible=true,Name="ReturnYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工率",FieldName="ReworkRate",Visible=true,Name="ReturnRate"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="SAM(分钟)",FieldName="Tel",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(分钟)",FieldName="StanardHours",Visible=true,Name="SAM"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="实时总工时(分钟)",FieldName="RealyWorkMin",Visible=true,Name="RealyWorkMin"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车缝效率",FieldName="SeamsRate",Visible=true,Name="SeamsEfficiencySite"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工价(元)",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="收入(元)",FieldName="Income",Visible=true,Name="Income"}

            });
            return gridView;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex, queryModel);
        }

        //private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //       // Query(1);
        //    }
        //}

        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = GetView();//new Ext.SusGridView();

            //gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            //   // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工",FieldName="EmployeeName",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO号",FieldName="PurchaseOrderNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工段",FieldName="FlowSection",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowIndex",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessFlowCode",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessFlowName",Visible=true},

            //     new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="SiteNo",Visible=true},

            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="Tel",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="产出量",FieldName="YieldCount",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工量",FieldName="ReworkCount",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工率",FieldName="ReworkRate",Visible=true},
            //    //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="SAM(分钟)",FieldName="Tel",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(分钟)",FieldName="StanardHours",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="实时总工时(分钟)",FieldName="RealyWorkMin",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车缝效率",FieldName="SeamsRate",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工价(元)",FieldName="StandardPrice",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="收入(元)",FieldName="Income",Visible=true}

            //});

            foreach (GridColumn item in gridView.Columns)
            {
                item.OptionsColumn.AllowEdit = false;
            }

            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            // gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

    }
}

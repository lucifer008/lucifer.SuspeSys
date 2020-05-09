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
    /// 返工详情报表
    /// </summary>
    public partial class ReworkDetailReport : SusXtraUserControl
    {
        public ReworkDetailReport()
        {
            InitializeComponent();
            susToolBar1.Load += susToolBar1_Load;
            susQueryControl1.Visible = false;
        }
        public ReworkDetailReport(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }
        private void susToolBar1_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
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
                        Query(1,queryModel);
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
                var gridView = GetView(gccPrint);
                gccPrint.MainView = gridView;
                gccPrint.DataSource = Query(queryModel);

                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(ps);
                link.Component = gccPrint;
                link.Landscape = true;
                PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                phf.Header.Content.Clear();
                phf.Header.Content.AddRange(new string[] { "", "返工详情报表", "" });
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

        private IList<ReworkDetailReportModel> Query(QueryModel queryModel)
        {
            IDictionary<string, string> ordercondition = null;
            var searchKey = string.Empty;//searchControl1.Text?.Trim();
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
            var list = reportQueryAction.SearchReworkDetailReport(ordercondition, searchKey);
            return list;
        }

        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            SusGridView gridView = GetView(gc);
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private static SusGridView GetView(GridControl gc)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检时间",FieldName="ReworkDate",Visible=true,Name="ReworkDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true,Name="ColorValue"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO号",FieldName="PO",Visible=true,Name="PO"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true,Name="SizeDesption"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工段",FieldName="FlowSection",Visible=true,Name="FlowSection"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="布匹号",FieldName="ClothNumber",Visible=true,Name="ClothNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="条码",FieldName="BarCode",Visible=true,Name="BarCode"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣架号",FieldName="HangerNo",Visible=true,Name="HangerNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="数量",FieldName="Num",Visible=true,Name="Num"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="疵点",FieldName="DefectCode",Visible=true,Name="defectCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="疵点名称",FieldName="DefectName",Visible=true,Name="DefectName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工序号",FieldName="ReworkIndex",Visible=true,Name="ReturnFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生产组别",FieldName="GroupNo",Visible=true,Name="productGroup"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生产站点",FieldName="StatingNo",Visible=true,Name="StatingName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检员工",FieldName="ReworkEmployeeName",Visible=true,Name="ReworkEmployeeName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检组别",FieldName="ReworkGroupNo",Visible=true,Name="ReworkGroupNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检站点",FieldName="ReworkStatingNo",Visible=true,Name="ReworkStatingNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检工序号",FieldName="CheckReworkNo",Visible=true,Name="CheckFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品检工序代码",FieldName="CheckReworkCode",Visible=true,Name="CheckFlowCode"}

            });

            foreach (DevExpress.XtraGrid.Columns.GridColumn item in gridView.Columns)
            {
                item.OptionsColumn.AllowEdit = false;
            }

            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            return gridView;
        }

        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        void Query(int currentPageIndex, QueryModel queryModel)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var searchKey = string.Empty;//searchControl1.Text?.Trim();
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
            var list = reportQueryAction.SearchReworkDetailReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }
        //private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Query(1);
        //}

        //private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        Query(1, queryModel);
        //    }
        //}
    }
}

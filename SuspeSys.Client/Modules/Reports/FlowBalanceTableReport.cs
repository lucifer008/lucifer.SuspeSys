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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils;
using SuspeSys.Utils;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action.ProcessFlowChart;
using SuspeSys.Domain.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Domain.Ext.ReportModel;
using DevExpress.XtraPrinting;

namespace SuspeSys.Client.Modules.Reports
{
    /// <summary>
    /// 工序平衡表
    /// </summary>
    public partial class FlowBalanceTableReport : SusXtraUserControl
    {
        public FlowBalanceTableReport()
        {
            InitializeComponent();
        }
        public FlowBalanceTableReport(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            this.panelControl1.Controls.Remove(susQueryControl1);
            var susFlowChartCon = new SusFlowChartControl();
            susFlowChartCon.Dock = DockStyle.Fill;
            susFlowChartCon.OnQuery += SusFlowChartCon_OnQuery;
            //this.panelControl1.Controls.Add(susFlowChartCon);
            susToolBar1.CustPanelControl.BorderStyle = BorderStyles.NoBorder;
            susToolBar1.CustPanelControl.Width = 400;
            susToolBar1.CustPanelControl.Dock = DockStyle.Fill;
            susToolBar1.CustPanelControl.Visible = true;
            susToolBar1.CustPanelControl.Controls.Add(susFlowChartCon);
        }

        private void SusFlowChartCon_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            Query(1, queryModel);
        }

        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();

            var pg = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar() { ShowTitle = false };
            pg.PercentView = false;
            pg.Maximum = 100;
            SusGridView gridView = GetView(pg);

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
            gridView.CustomDrawCell += GridView_CustomDrawCell;
            gridView.RowCellStyle += GridView_RowCellStyle;
            //gridView.CustomRowCellEdit += GridView_CustomRowCellEdit;
        }

        private static SusGridView GetView(RepositoryItemProgressBar pg)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
             //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true,Name="ProcessName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="SAM(分钟)",FieldName="SAM",Visible=true,Name="SAM"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="分配的站点",FieldName="AllocationStatings",Visible=true,Name="AllocationStatings"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产量",FieldName="TodayYield",Visible=true,Name="TodayYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计产量",FieldName="TotalYield",Visible=true,Name="TotalYield"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="线上衣数",FieldName="OnlineHangerCount",Visible=true,Name="OnlineHangerCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站内衣数",FieldName="InStatingHangerCount",Visible=true,Name="StatingInCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="当前总衣数",FieldName="CurrentHangerCount",
                    ColumnEdit =pg, Visible = true,Name="CurrentHangerCount"}

            });
            return gridView;
        }

        private void GridView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "CurrentHangerCount")
            {
                var pg = (RepositoryItemProgressBar)e.RepositoryItem;
                pg.PercentView = false;
                pg.Maximum = 100;
               
            }
        }

        private void GridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == (susGrid1.DataGrid.MainView as SusGridView).FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.CadetBlue;
            }
        }
        Font cellFont = new Font(AppearanceObject.DefaultFont, FontStyle.Regular);
        Brush principalForeBrush = Brushes.Black;
        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "CurrentHangerCount")
            {
                var view = sender as GridView;
                var flowBalanceTableReportModel = view.GetRow(e.RowHandle) as FlowBalanceTableReportModel;
                e.Appearance.DrawBackground(e.Cache, e.Bounds);
                DrawProgressBar(e);
                DrawEditor(e);
                e.Handled = true;
                e.Graphics.DrawString(flowBalanceTableReportModel.Capacity, cellFont, principalForeBrush,e.Bounds);
            }
        }
        private void DrawEditor(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo cell = e.Cell as GridCellInfo;
            Point offset = cell.CellValueRect.Location;
            BaseEditPainter pb = cell.ViewInfo.Painter as BaseEditPainter;
            AppearanceObject savedStyle = cell.ViewInfo.PaintAppearance;
            if (!offset.IsEmpty)
                cell.ViewInfo.Offset(offset.X, offset.Y);
            try
            {
                pb.Draw(new ControlGraphicsInfoArgs(cell.ViewInfo, e.Cache, cell.Bounds));
            }
            finally
            {
                if (!offset.IsEmpty)
                    cell.ViewInfo.Offset(-offset.X, -offset.Y);
            }
        }
        private void DrawProgressBar(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            decimal percent = Convert.ToDecimal(e.CellValue);
            int width = (int)(10 * Math.Abs(percent) * e.Bounds.Width / 100);//涨跌幅最大为10%，所以要乘以10来计算比例，沾满一个单元格为10%
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);
            Brush b = Brushes.Green;
            if (percent > 0 && percent < 0.1m)
            {
                b = Brushes.Green;
                rect = new Rectangle(e.Bounds.X, e.Bounds.Y, 10, e.Bounds.Height);
            }
            //else if (percent < 2.5m)
            //    b = Brushes.Purple;
            else if (percent > 0 && percent == 1m)
                b = Brushes.Red;
            //else if (percent < 7.5m)
            //    b = Brushes.Yellow;
            e.Graphics.FillRectangle(b, rect);
           // e.Graphics.fi
        }
        private void FlowBalanceTableReport_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            // this.rgDateRange.SelectedIndexChanged += new System.EventHandler(this.rgDateRange_SelectedIndexChanged);
            //susQueryControl1.Visible = false;
            //susQueryControl1.OnQuery += SusQueryControl1_OnQuery;
            //var action = new ProcessFlowChartQueryAction();
            //var flowChartId = action.GetOnlineFlowChartId(CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            //queryModel = new QueryModel();
            //queryModel.FlowChartId = flowChartId;
            //Query(1, queryModel);
        }
        QueryModel queryModel;
        private void SusQueryControl1_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            Query(1, queryModel);
        }

        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
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
                var pg = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar() { ShowTitle = false };
                pg.PercentView = false;
                pg.Maximum = 100;
                var gccPrint = new GridControl();
                susToolBar1.Controls.Add(gccPrint);
                var gridView = GetView(pg); //new Ext.SusGridView();
                
            //    gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
            // //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="SAM(分钟)",FieldName="SAM",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="分配的站点",FieldName="AllocationStatings",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产量",FieldName="TodayYield",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计产量",FieldName="TotalYield",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="线上衣数",FieldName="OnlineHangerCount",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站内衣数",FieldName="InStatingHangerCount",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="当前总衣数",FieldName="CurrentHangerCount",
            //        ColumnEdit =pg, Visible = true}

            //});
                gridView.CustomDrawCell += GridView_CustomDrawCell;
                gridView.RowCellStyle += GridView_RowCellStyle;
                gccPrint.MainView = gridView;
                gccPrint.DataSource = Query(queryModel);

                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(ps);
                link.Component = gccPrint;
                link.Landscape = true;
                PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                phf.Header.Content.Clear();
                phf.Header.Content.AddRange(new string[] { "", "工序平衡表", "" });
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

        private IList<FlowBalanceTableReportModel> Query(QueryModel queryModel)
        {
            var searchKey = string.Empty;
            IDictionary<string, string> ordercondition = null;
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
            var list = reportQueryAction.SearchFlowBalanceTableReportModel(ordercondition, searchKey, queryModel.FlowChartId);
            return list;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex, queryModel);
        }

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
            var list = reportQueryAction.SearchFlowBalanceTableReportModel(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, queryModel.FlowChartId);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }
    }
}

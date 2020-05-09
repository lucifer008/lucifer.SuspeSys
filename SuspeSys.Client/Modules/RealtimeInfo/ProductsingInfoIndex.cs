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
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules.Products;
using System.Collections;
using SuspeSys.Client.Action.Products;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Client.Modules.SusDialog;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using SuspeSys.Domain.Common;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SuspeSys.Client.Modules.Reports;
using DevExpress.XtraTab;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    /// <summary>
    /// 在制品信息
    /// </summary>
    public partial class ProductsingInfoIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public ProductsingInfoIndex()
        {
            InitializeComponent();
            RegisterEvent();
            RegisterGridContextMenuStrip();

            //注册事件
            CurrentUser.Instance.GroupChangeEvent += Instance_GroupChangeEvent;
        }
        public ProductsingInfoIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }

        void RegisterEvent()
        {
            this.Load += ProductsingInfoIndex_Load;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            (susGrid1.DataGrid.MainView as GridView).CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.ProductsingInfoIndex_CustomDrawCell);
            CurrentUser.Instance.Timer.Start();
            CurrentUser.Instance.Timer.Elapsed += Timer_Elapsed;
            susGrid1.DataGrid.MainView.DoubleClick += MainView_DoubleClick;
            //(susGrid1.DataGrid.MainView as GridView).RowStyle += ProductsingInfoIndex_RowStyle; ;
            // (susGrid1.DataGrid.MainView as GridView).OptionsSelection.EnableAppearanceFocusedCell = false;
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
                if (fieldName.Equals("LineName"))
                {
                    BindProcessFlowChart();
                }
                else if (fieldName.Equals("Unit"))
                {
                    AdjustmentUnitProductsQuantity_Click(sender, e);
                }
                else if (fieldName.Equals("TaskNum"))
                {
                    AdjustmentPlanYield_Click(sender, e);
                }
                if (fieldName.Equals("OnlineNum"))
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        if (null == view) return;
                        var prInfoModel = view.GetRow(view.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                        if (null == prInfoModel) return;
                        // if (string.IsNullOrEmpty(prInfoModel.StatingNo)) return;
                        //if(string.IsNullOrEmpty(info.))
                        XtraTabPage tab = new SusXtraTabPage();
                        tab.Text = string.Format("【制单:{0},颜色:{1};尺码 】在线衣架明细", prInfoModel.ProcessOrderNo?.Trim(), prInfoModel.PColor?.Trim(), prInfoModel.PSize?.Trim());
                        tab.Name = string.Format("【{0}】站内衣架明细", "");
                        XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new OnlineOrInStationItem(ucMain, "", prInfoModel.Id));
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        XtraMessageBox.Show(ex.Message, "错误");
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                // MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ThreadPool.SetMaxThreads(1, 1);
            ThreadPool.QueueUserWorkItem(
                  new WaitCallback(TimerRefushMessage), null);
            // this.Invoke(new EventHandler(TimerRefushMessage), null, null);
        }
        void TimerRefushMessage(object data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(RefushMessage), null);
            }
        }
        void RefushMessage(object data, EventArgs e)
        {
            //try
            //{
            //    SusTransitionManager.StartTransition(ucMain.MainTabControl, "");

            Query(1);
            //}
            //finally
            //{
            //    SusTransitionManager.EndTransition();
            //}
        }
        Font cellFont = new Font(AppearanceObject.DefaultFont, FontStyle.Regular);
        Brush interestBrush = Brushes.IndianRed, principalBrush = Brushes.LightGreen, interestForeBrush = Brushes.White, principalForeBrush = Brushes.Black;
        Pen paymentPen = Pens.Green;
        private void ProductsingInfoIndex_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            SuspeSys.Domain.ProductsModel products = view.GetRow(e.RowHandle) as SuspeSys.Domain.ProductsModel;
            if (e.RowHandle >= 0)
            {
                //var m=view.GetRow(view.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                var st = view.GetRowCellDisplayText(e.RowHandle, view.Columns["StatusText"]);
                var isRed = st.Equals(ProductsStatusType.Onlineed.Desption); //m.Status== ProductsStatusType.Onlineed.Value;
                if (isRed)
                {
                    Rectangle r = e.Bounds;
                    Rectangle interestRect = new Rectangle(r.X, r.Y, r.Width + 20, r.Height);
                    //Rectangle principalRect = new Rectangle(r.X + r.Width, r.Y, r.Width, r.Height);
                    e.Graphics.FillRectangle(interestBrush, interestRect);
                    // e.Graphics.FillRectangle(principalBrush, principalRect);
                    using (StringFormat sf = new StringFormat())
                    {
                        //r.Inflate(-3, -3);
                        //int interestWidth =100;
                        //int principalWidth = 100;
                        //Rectangle interestRect = new Rectangle(r.X, r.Y, interestWidth, r.Height);
                        //Rectangle principalRect = new Rectangle(r.X + interestWidth, r.Y, principalWidth, r.Height);
                        //e.Graphics.FillRectangle(interestBrush, interestRect);
                        //e.Graphics.FillRectangle(principalBrush, principalRect);
                        e.Graphics.DrawString(e.CellValue?.ToString(), cellFont, principalForeBrush, r, sf);
                    }
                    // e.Graphics.DrawRectangle(paymentPen, new Rectangle(r.X, r.Y - 1, (r.Width + r.Width), r.Height + 1));
                    e.Handled = true;
                }
            }
        }

        private void ProductsingInfoIndex_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //var m=view.GetRow(view.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                var st = view.GetRowCellDisplayText(e.RowHandle, view.Columns["StatusText"]);
                var isRed = st.Equals(ProductsStatusType.Onlineed.Desption); //m.Status== ProductsStatusType.Onlineed.Value;
                if (isRed)
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }

        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var allocationHangerPiece = new ToolStripMenuItem() { Text = "分配挂片站", Name = "allocationHangerPiece" };
            var clearHangerPiece = new ToolStripMenuItem() { Text = "清除挂片站", Name = "clearHangerPiece" };
            var productsOnline = new ToolStripMenuItem() { Text = "产品上线", Name = "productsOnline" };
            var lookProcessFlowChart = new ToolStripMenuItem() { Text = "查看工艺图", Name = "processFlowChart" };
            var markSuccessProducts = new ToolStripMenuItem() { Text = "标记完成", Name = "markSuccessProducts" };
            var adjustmentPlanYield = new ToolStripMenuItem() { Text = "调整计划投入产量", Name = "adjustmentPlanYield" };
            var adjustmentUnitProductsQuantity = new ToolStripMenuItem() { Text = "调整单位产品数量", Name = "adjustmentUnitProductsQuantity" };
            var changeFlowChart = new ToolStripMenuItem() { Text = "设置工艺图路线图", Name = "changeFlowChart" };
            allocationHangerPiece.Click += AllocationHangerPiece_Click; ;
            lookProcessFlowChart.Click += ProcessFlowChart_Click;
            clearHangerPiece.Click += ClearHangerPiece_Click;
            productsOnline.Click += ProductsOnline_Click;
            markSuccessProducts.Click += MarkSuccessProducts_Click;
            adjustmentPlanYield.Click += AdjustmentPlanYield_Click;
            adjustmentUnitProductsQuantity.Click += AdjustmentUnitProductsQuantity_Click;
            changeFlowChart.Click += ChangeFlowChart_Click;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] {
                
                productsOnline,allocationHangerPiece,clearHangerPiece,
                lookProcessFlowChart,
                changeFlowChart,
                markSuccessProducts,
                adjustmentPlanYield,
                adjustmentUnitProductsQuantity });
        }
        //设置工艺图路线图
        private void ChangeFlowChart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var gv = (susGrid1.DataGrid.MainView as GridView);
                var row = gv.GetRow(gv.FocusedRowHandle) as Domain.ProductsModel;
                var frmChangeFlowChart = new frmChangeFlowChart(row);
                frmChangeFlowChart.ShowDialog();
                if (frmChangeFlowChart.IsSaveSucess)
                {
                    Query(1);
                }
                //var aPlanYieldForm = new SetAdjustmentUnitProductsQuantity(this, row);
                //aPlanYieldForm.ShowDialog();
                //if (aPlanYieldForm.IsSaveSucess)
                //{
                //    Query(1);
                //}
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //调整单位产品数量
        private void AdjustmentUnitProductsQuantity_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var gv = (susGrid1.DataGrid.MainView as GridView);
                var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;
                var aPlanYieldForm = new SetAdjustmentUnitProductsQuantity(this, row);
                aPlanYieldForm.ShowDialog();
                if (aPlanYieldForm.IsSaveSucess)
                {
                    Query(1);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //调整计划投入产量
        private void AdjustmentPlanYield_Click(object sender, EventArgs e)
        {
            try
            {


                this.Cursor = Cursors.WaitCursor;
                var gv = (susGrid1.DataGrid.MainView as GridView);
                var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;
                var aPlanYieldForm = new SetAdjustmentPlanYield(this, row);
                aPlanYieldForm.ShowDialog();
                if (aPlanYieldForm.IsSaveSucess)
                {
                    Query(1);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MarkSuccessProducts_Click(object sender, EventArgs e)
        {
            var dr = XtraMessageBox.Show("确定选中的产品已完成了吗?", "温馨提示", MessageBoxButtons.OKCancel);

            if (DialogResult.OK == dr)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string errMsg = null;
                    var gv = (susGrid1.DataGrid.MainView as GridView);
                    //var rows= gv.GetSelectedRows();
                    var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;
                    //XtraMessageBox.Show(row?.ProductionNumber);
                    bool sucess = productsAction.MarkSuccessProducts(row, ref errMsg);
                    if (sucess)
                    {
                        XtraMessageBox.Show(string.Format("处理成功!"), "温馨提示");

                        Query(1);
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("标记完成失败!失败原因;【{0}】", errMsg), "温馨提示");
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        public string HangingPeiceNo { set; get; }
        private void AllocationHangerPiece_Click(object sender, EventArgs e)
        {
            try
            {
                SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                var gv = (susGrid1.DataGrid.MainView as GridView);
                //var rows= gv.GetSelectedRows();
                var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;
                var hp = new HangingPieceStatingDialog(this);
                hp.ShowDialog();
                if (!string.IsNullOrEmpty(HangingPeiceNo))
                {
                    productsAction.AllocationHangerPiece(row.Id, HangingPeiceNo);
                    Query(1);
                }
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }

        private void ProductsOnline_Click(object sender, EventArgs e)
        {
            var dr = XtraMessageBox.Show("确认上线吗?", "温馨提示", MessageBoxButtons.OKCancel);

            if (DialogResult.OK == dr)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string errMsg = null;
                    var gv = (susGrid1.DataGrid.MainView as GridView);
                    //var rows= gv.GetSelectedRows();
                    var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;

                    if (string.IsNullOrWhiteSpace(row.HangingPieceSiteNo))
                    {
                        XtraMessageBox.Show(string.Format("请分配挂片站!"), "温馨提示");

                        return;
                    }

                    //XtraMessageBox.Show(row?.ProductionNumber);
                    bool sucess = productsAction.ProductsOnline(row, ref errMsg);
                    if (sucess)
                    {
                        XtraMessageBox.Show(string.Format("制品已发往挂片站!"), "温馨提示");

                        Query(1);
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("数据发往挂片站失败!失败原因;【{0}】", errMsg), "温馨提示");
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void ClearHangerPiece_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认清除挂片站吗", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult.OK == dr)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    var gv = susGrid1.DataGrid.MainView as GridView;
                    if (null == gv) return;
                    var products = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                    productsAction.ClearHangingPiece(products.Id);
                    Query(1);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    //XtraMessageBox.Show(ex.Message, "错误");
                    XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void ProcessFlowChart_Click(object sender, EventArgs e)
        {
            BindProcessFlowChart();
        }
        void BindProcessFlowChart()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var gv = susGrid1.DataGrid.MainView as GridView;
                if (null == gv) return;
                var products = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                var tab = new SusXtraTabPage();
                tab.Text = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                tab.Name = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new ProcessFlowChartIndex(ucMain, products));
                //SusTransitionManager.EndTransition();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        ProductsAction productsAction = new ProductsAction();

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {
                    case ButtonName.Add:
                        SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                        var productsInput = new ProductsInput(this);
                        productsInput.ShowDialog();
                        Query(1);
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
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }

        private void ProductsingInfoIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
        }

        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.Columns.Clear();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排产号",FieldName="ProductionNumber",Visible=true,Name="ProductionNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="上线日期",FieldName="ImplementDate",Visible=true,Name="ImplementDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="挂片站点",FieldName="HangingPieceSiteNo",Visible=true,Name="HangingPieceSiteNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="状态",FieldName="StatusText",Visible=true,Name="StatusText"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true,Name="OrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO",FieldName="Po",Visible=true,Name="Po"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true,Name="ColorValue"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true,Name="SizeDesption"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="产品部位",FieldName="ProductPart",Visible=true,Name="productPart"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺路线图",FieldName="LineName",Visible=true,Name="LineName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单位",FieldName="Unit",Visible=true,Name="Unit"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="任务数量",FieldName="TaskNum",Visible=true,Name="TaskNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="在线数",FieldName="OnlineNum",Visible=true,Name="OnlineNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日挂片",FieldName="TotalHangingPieceSiteNum",Visible=true,Name="TotalHangingPieceSiteNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产出",FieldName="TodayProdOutNum",Visible=true,Name="TodayProdOutNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日绑卡",FieldName="TodayBindCardNum",Visible=true,Name="TodayBindCardNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日返工",FieldName="TodayRework",Visible=true,Name="TodayReturnWorkNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计挂片",FieldName="TotalHangingNum",Visible=true,Name="TotalHangingNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计产出",FieldName="TotalProdOutNum",Visible=true,Name="TotalProdOutNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计返工",FieldName="TotalRework",Visible=true,Name="TotalReworkNum"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计绑卡",FieldName="TotalBindCardNum",Visible=true,Name="TotalBindCardNum"}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var pAction = new ProductsAction();
            var searchKey = searchControl1.Text.Trim();
            var ordercondition = new Dictionary<string, string>();
            ordercondition.Add("ProductionNumber", "ASC");
            var list = pAction.SearchProductsList(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, CurrentUser.Instance.CurrentSiteGroup?.GroupNo);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

        }
        public override void RefRefreshData()
        {
            Query(1);
        }
        private void ProductsingInfoIndex_Load_1(object sender, EventArgs e)
        {
            Query(1);
        }

        private void searchControl1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SusTransitionManager.StartTransition(ucMain.MainTabControl, "");

                Query(1);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }

        private void searchControl1_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }

        private void Instance_GroupChangeEvent(object sender, Domain.SusEvent.SusEventArgs e)
        {
            Query(1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            CurrentUser.Instance.Timer.Stop();
            CurrentUser.Instance.Timer.Elapsed -= Timer_Elapsed;
        }

    }
}

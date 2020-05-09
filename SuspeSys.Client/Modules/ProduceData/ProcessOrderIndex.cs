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
using System.Threading;
using DevExpress.XtraTab;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Client.Modules.SusDialog;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Action;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.ProcessOrder;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Query;
using SuspeSys.Client.Modules.Ext;
using System.Collections;
using SuspeSys.Client.Common;
using SuspeSys.Client.Common.Utils.Permission;
using SuspeSys.Client.Action.Common;

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 生产制单
    /// </summary>
    [Common.FormPermission(Domain.PermissionConstant.Billing_ProcessOrderIndex)]
    public partial class ProcessOrderIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        //  private XtraUserControl1 ucMain;
        public ProcessOrderIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        public ProcessOrderIndex()
        {
            InitializeComponent();
            RegisterEvent();
            RegisterGridContextMenuStrip();
            BindGridHeader(susGrid1.DataGrid);
            //Thread.Sleep(2000);
            //susToolBar1.GetButton(ButtonName.Add).Text = "新增普通制单";
            //susToolBar1.GetButton(ButtonName.Add2).Text = "新增合成制单";
            //susToolBar1.GetButton(ButtonName.Add).Size = new System.Drawing.Size(90, 48);
            //susToolBar1.GetButton(ButtonName.Add2).Size = new System.Drawing.Size(90, 48);

            //获取自定义特性
            ToolBarPermision.Process(typeof(ProcessOrderIndex), this.susToolBar1);
        }
        void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;

        }
        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var tsmItemCopyAndNew = new ToolStripMenuItem() { Text = "复制(新增)" };
            tsmItemCopyAndNew.Click += TsmItemCopyAndNew_Click;
            var tsmItemProcessFlowIndex = new ToolStripMenuItem() { Text = "工艺路线图" };
            tsmItemProcessFlowIndex.Click += TsmItemProcessFlowIndex_Click;

            var tsmItemProcessOrderFlow = new ToolStripMenuItem() { Text = "制单工序表" };
            tsmItemProcessOrderFlow.Click += TsmItemProcessOrderFlow_Click;

            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { tsmItemCopyAndNew, tsmItemProcessFlowIndex, tsmItemProcessOrderFlow });
        }
        //制单工序表
        private void TsmItemProcessOrderFlow_Click(object sender, EventArgs e)
        {
            // var processOrder = GetSelectProcessOrder();
            // var processFlowIndex = new ProcessFlowIndex(ucMain, processOrder) { Dock = DockStyle.Fill };
            // var tab = new XtraTabPage();
            // tab.Name = processFlowIndex.Name;
            // tab.Text = "制单工序表";
            //// if (!ucMain.MainTabControl.TabPages.Contains(tab))
            // //{
            //     tab.Controls.Add(processFlowIndex);
            //     ucMain.MainTabControl.TabPages.Add(tab);
            // //}

            // ucMain.MainTabControl.SelectedTabPage = tab;

            try
            {

                this.Cursor = Cursors.WaitCursor;
                var processOrder = GetSelectProcessOrder();
                XtraTabPage tab = new SusXtraTabPage();
                tab.Text = string.Format("制单工序表[{0}]", processOrder.POrderNo?.Trim());
                tab.Name = string.Format("制单工序表[{0}]", processOrder.POrderNo?.Trim());
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new ProcessFlowIndex(ucMain, processOrder));
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
        //工艺路线图
        private void TsmItemProcessFlowIndex_Click(object sender, EventArgs e)
        {
            try
            {

                // SusTransitionManager.StartTransition(ucMain.MainTabControl, "工艺路线图");
                //var processFlowChartIndex = new ProcessFlowChartIndex(ucMain)
                //{
                //    Dock = DockStyle.Fill
                //};
                var processOrder = GetSelectProcessOrder();
                var tab = new SusXtraTabPage();
                tab.Text = string.Format("工艺路线图[{0}]", processOrder.POrderNo?.Trim());
                tab.Name = string.Format("工艺路线图[{0}]", processOrder.POrderNo?.Trim());
                //if (!ucMain.MainTabControl.TabPages.Contains(tab))
                //{
                //    tab.Controls.Add(processFlowChartIndex);
                //    ucMain.MainTabControl.TabPages.Add(tab);
                //}
                //ucMain.MainTabControl.SelectedTabPage = tab;
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new ProcessFlowChartIndex(ucMain, processOrder));
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
        //复制(新增)
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var cpOrderDialog = new CopyProcessOrderDialog();
            DialogResult dr = cpOrderDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var processOrder = new ProcessOrdersInput() { Dock = DockStyle.Fill };
                processOrder.ucMain = ucMain;
                var tab = new XtraTabPage();
                tab.Name = processOrder.Name;
                tab.Text = "新增制单";
                if (!ucMain.MainTabControl.TabPages.Contains(tab))
                {
                    tab.Controls.Add(processOrder);
                    ucMain.MainTabControl.TabPages.Add(tab);
                }
                ucMain.MainTabControl.SelectedTabPage = tab;
            }
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
                        //var processOrder = new PurchaseProcessOrdersInput() { Dock = DockStyle.Fill };
                        //processOrder.ucMain = ucMain;
                        var processOrder = new ProcessOrdersInputNew(ucMain) { Dock = DockStyle.Fill };
                        var tab = new XtraTabPage();
                        tab.Name = processOrder.Name + "1";
                        tab.Text = "新增制单";
                        if (!ucMain.MainTabControl.TabPages.Contains(tab))
                        {
                            tab.Controls.Add(processOrder);
                            ucMain.MainTabControl.TabPages.Add(tab);
                        }
                        ucMain.MainTabControl.SelectedTabPage = tab;
                        break;
                    case ButtonName.Modify:
                        EditProcessOrder(this.susGrid1.DataGrid.MainView as GridView);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                       // SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                        Query(1);
                        break;
                    case ButtonName.Delete:
                        var rs = MessageBox.Show("确认要删除该订单吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.Yes)
                        {
                            var gv = susGrid1.DataGrid.MainView as GridView;
                            if (null == gv) return;
                            var delProcessOrder = gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
                            var isProducts=processOrderQueryAction.CheckProcessOrderIsProducts(delProcessOrder.Id);
                            if (isProducts) {
                                var message = string.Format("制单【{0}】已经上线生产不能删除!",delProcessOrder.POrderNo?.Trim());
                                XtraMessageBox.Show(message, "提示");
                                return;
                            }
                            if (!string.IsNullOrEmpty(delProcessOrder.Id))
                            {
                                CommonAction.Instance.LogicDelete<DaoModel.ProcessOrder>(delProcessOrder.Id);
                                XtraMessageBox.Show("删除成功!", "提示");
                                Query(1);
                            }
                        }
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
                log.Error(ex);
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }
        ProcessOrderQueryAction processOrderQueryAction = new ProcessOrderQueryAction();
        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var conModel = GetSearchModel();
            var ordercondition = new Dictionary<string,string>();
            ordercondition.Add("InsertDateTime","Desc");
            var list = processOrderQueryAction.SearchProcessOrder(currentPageIndex, pageSize, out totalCount, ordercondition, conModel);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }
        DaoModel.ProcessOrderModel GetSearchModel()
        {
            var model = new DaoModel.ProcessOrderModel();
            model.POrderNo = txtPOrderNo.Text.Trim();
            model.StyleCode = txtStyleCode.Text.Trim();
            model.CustomerNo = txtCustomerNo.Text.Trim();
            model.CustOrderNo = txtCustomerOrderNo.Text.Trim();
            model.OrderNo = searchControl1.Text.Trim();
            return model;
        }
        private void ProcessOrderIndex_Load(object sender, EventArgs e)
        {
            Query(1);
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = gc.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式描述",FieldName="StyleName",Visible=true,Name="StyleName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工单号",FieldName="OrderNo",Visible=true,Name="OrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CustomerNo",Visible=true,Name="CustomerNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CustomerName",Visible=true,Name="CustomerName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户款号",FieldName="CustomerStyle",Visible=true,Name="CustomerStyle"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下单日期",FieldName="GenaterOrderDate",Visible=true,Name="GenaterOrderDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="交货日期",FieldName="DeliveryDate",Visible=true,Name="DeliveryDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="状态",FieldName="StatusText",Visible=true,Name="StatusText"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true,Name="InsertDateTime"}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsBehavior.Editable = false;
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.MouseDown += GridView_MouseDown;
            gridView.IndicatorWidth = 40;
            gridView.OptionsView.ShowIndicator = true;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
        }
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                EditProcessOrder(sender as GridView);
            }
        }
        DaoModel.ProcessOrder GetSelectProcessOrder()
        {
            var gv = susGrid1.DataGrid.MainView as GridView;
            if (null == gv) return null;
            var editProcessOrder = gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
            if (!string.IsNullOrEmpty(editProcessOrder.Id))
            {
                editProcessOrder = new ProcessOrderQueryAction().GetProcessOrder(editProcessOrder.Id);
            }
            return editProcessOrder;
        }
        void EditProcessOrder(GridView gv)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (null != gv)
                {
                    var editProcessOrder = GetSelectProcessOrder(); //gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
                    if (null == editProcessOrder) return;
                    //  var processOrder = new PurchaseProcessOrdersInput() { Dock = DockStyle.Fill };
                    var processOrder = new ProcessOrdersInputNew(ucMain,editProcessOrder) { Dock = DockStyle.Fill };
                    processOrder.ucMain = ucMain;
                    processOrder.EditProcessOrderModel = editProcessOrder;

                    var tab = new XtraTabPage();
                    tab.Name = processOrder.Name;
                    tab.Text = string.Format("正在编辑制单[{0}]", editProcessOrder.POrderNo?.Trim());
                    if (!ucMain.MainTabControl.TabPages.Contains(tab))
                    {
                        tab.Controls.Add(processOrder);
                        ucMain.MainTabControl.TabPages.Add(tab);
                    }
                    ucMain.MainTabControl.SelectedTabPage = tab;
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
        private void ribbonStatusBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Cursor = Cursors.WaitCursor;
                //int pageSize = susGrid1.PageSize;
                //long totalCount = 0;
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
                btnSearch.Cursor = Cursors.Default;
            }
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                searchControl1.Cursor = Cursors.WaitCursor;
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
                searchControl1.Cursor = Cursors.Default;
            }
        }

        private void buttonEdit4_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind==DevExpress.XtraEditors.Controls.ButtonPredefines.Close) {
                txtPOrderNo.Text = string.Empty;
            }
        }

        private void txtStyleCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                txtStyleCode.Text = string.Empty;
            }
        }

        private void txtCustomerOrderNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                txtCustomerOrderNo.Text = string.Empty;
            }
        }

        private void txtCustomerNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                txtCustomerNo.Text = string.Empty;
            }
        }
    }
}

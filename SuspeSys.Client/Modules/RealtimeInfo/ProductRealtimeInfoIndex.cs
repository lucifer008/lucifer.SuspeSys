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
using DevExpress.XtraGrid.Views.Grid;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Products;
using SuspeSys.Client.Action.Common;
using SuspeSys.Domain.Common;
using System.Timers;
using System.Threading;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain.Ext;
using SuspeSys.Client.Action.ProductionLineSet;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Utils.Reflection;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    /// <summary>
    /// 产线实时信息
    /// </summary>
    public partial class ProductRealtimeInfoIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public ProductRealtimeInfoIndex()
        {
            InitializeComponent();
            RegisterEvent();
        }
        public ProductRealtimeInfoIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        void RegisterEvent()
        {
            this.Load += ProductRealtimeInfoIndex_Load;
            // susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            //susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            gdFlowChart.MainView.MouseDown += GridViewFlowChart_MouseDown;

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
                if (fieldName.Equals("StatingInCount") || fieldName.Equals("OnlineHangerCount"))
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        if (null == view) return;
                        var prInfoModel = view.GetRow(view.FocusedRowHandle) as ProductRealtimeInfoModel;
                        if (null == prInfoModel) return;
                        if (string.IsNullOrEmpty(prInfoModel.StatingNo)) return;
                        //if(string.IsNullOrEmpty(info.))
                        XtraTabPage tab = new SusXtraTabPage();
                        var title = string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("StatingInHangerItem"), prInfoModel.GroupNO?.Trim()+"-"+ prInfoModel.StatingNo?.Trim());//string.Format("【{0}】站内衣架明细", prInfoModel.StatingNo?.Trim());
                        tab.Text = title;
                        tab.Name = title;// string.Format("【{0}】站内衣架明细", prInfoModel.StatingNo?.Trim());
                        XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new OnlineOrInStationItem(ucMain, prInfoModel.StatingNo?.Trim(), null, prInfoModel.MainTrackNumber.Value));
                    }
                    catch (Exception ex)
                    {
                       // log.Error(ex);
                        //XtraMessageBox.Show(ex.Message, "错误");
                        log.Error(ex);
                        XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));

                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

                // MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
        }

        ProductsAction productAction = new ProductsAction();
        private void GridViewFlowChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {

                popupccFlowChart.OwnerEdit.ClosePopup();
                var gv = gdFlowChart.MainView as GridView;
                //var selRows = gv.GetSelectedRows();
                //if (0 == selRows.Length) return;

                var downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessFlowChart;
                if (null == selecRow) return;
                // productAction.SearchProcessFlowChartFlowModelList(selecRow.Id);
                //gdFlowChartDetail.DataSource = productAction.Model.ProcessFlowChartFlowModelList;
                comboFlowChart.Text = selecRow.LinkName;
                comboFlowChart.Tag = selecRow;
                flowChartId = selecRow?.Id;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void BindFlowChartGridHeader()
        {
            var gridView = gdFlowChart.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图名称",FieldName="LinkName",Visible=true}
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图版本",FieldName="CusName",Visible=true }
            });
            gdFlowChart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdFlowChart.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdFlowChart;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
        }
        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            throw new NotImplementedException();
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
                        var productsInput = new ProductsInput();
                        productsInput.ShowDialog();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                        //  Query();
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, gridControl1);
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
        void RegisterVvent()
        {
            CurrentUser.Instance.GroupChangeEvent += Instance_GroupChangeEvent;
            CurrentUser.Instance.Timer.Start();
            CurrentUser.Instance.Timer.Elapsed += Timer_Elapsed;
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

            SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup?.GroupNo?.Trim());
            //}
            //finally
            //{
            //    SusTransitionManager.EndTransition();
            //}
        }

        private void Instance_GroupChangeEvent(object sender, DaoModel.SusEvent.SusEventArgs e)
        {
            SearchProductRealtimeInfo(flowChartId, e.Tag?.ToString());
        }

        private void ProductRealtimeInfoIndex_Load(object sender, EventArgs e)
        {
            InitStatingRole();
            RegisterVvent();
            BindGridHeader(gridControl1);
            BindFlowChartGridHeader();
            BindFlowChartData();
            RefshData();
            //  RegisterGridContextMenuStrip();
        }
        public void RefshData()
        {
            var onLineProduct = ProductsAction.Instance.GetOnLineProduct(CurrentUser.Instance.CurrentSiteGroup?.GroupNo?.Trim());
            if (null != onLineProduct)
            {
                flowChartId = onLineProduct.ProcessFlowChartId;
            }
            SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup?.GroupNo?.Trim());
        }
        private void RegisterGridContextMenuStrip(ProductRealtimeInfoModel prInfo = null)
        {

            contextMenuStrip1.Items.Clear();
            var loginEmployeeAction = new ToolStripMenuItem()
            {
                Text = Client.Action.LanguageAction.Instance.BindLanguageTxt("loginEmployeeAction")//"登录员工"
                ,
                Name = "loginEmployeeAction"
            };
            loginEmployeeAction.Tag = prInfo;

            var nonOnlineEmployeeAction = new ToolStripMenuItem()
            {
                Text = Client.Action.LanguageAction.Instance.BindLanguageTxt("nonOnlineEmployeeAction")//"离线员工"
                ,
                Name = "nonOnlineEmployeeAction"
            };
            nonOnlineEmployeeAction.Tag = prInfo;

            var updateStatingCapacity = new ToolStripMenuItem()
            {
                Text = Client.Action.LanguageAction.Instance.BindLanguageTxt("updateStatingCapacity")//"修改站点容量"
                ,
                Name = "updateStatingCapacity"
            };
            updateStatingCapacity.Tag = prInfo;

            var updateInStatingHangerNum = new ToolStripMenuItem()
            {
                Text = Client.Action.LanguageAction.Instance.BindLanguageTxt("updateInStatingHangerNum")//"修改站内衣架数"
                ,
                Name = "updateInStatingHangerNum"
            };
            updateInStatingHangerNum.Tag = prInfo;

            //var lookInStatingProdcuts = new ToolStripMenuItem() { Text = "查看站内产品信息" };
            //var lookOnlineProducts = new ToolStripMenuItem() { Text = "查看线上产品信息" };
            var adjustmentStatingRoles = new ToolStripMenuItem()
            {
                Text = Client.Action.LanguageAction.Instance.BindLanguageTxt("adjustmentStatingRoles"),
                //"调整站点角色",

                Name = "adjustmentStatingRoles"
            };

            loginEmployeeAction.Click += LoginEmployeeAction_Click;
            nonOnlineEmployeeAction.Click += NonOnlineEmployeeAction_Click;
            updateStatingCapacity.Click += UpdateStatingCapacity_Click;
            updateInStatingHangerNum.Click += UpdateInStatingHangerNum_Click;
            //lookInStatingProdcuts.Click += LookInStatingProdcuts_Click;
            //lookOnlineProducts.Click += LookOnlineProducts_Click;
            adjustmentStatingRoles.Click += AdjustmentStatingRoles_Click;
            contextMenuStrip1.Items.AddRange(new ToolStripMenuItem[] { loginEmployeeAction, nonOnlineEmployeeAction, updateStatingCapacity,
                updateInStatingHangerNum, adjustmentStatingRoles });//, lookOnlineProducts,lookInStatingProdcuts });

            //站点角色
            if (_StatingRoles != null)
            {
                foreach (var item in _StatingRoles)
                {
                    if (item.RoleCode == StatingType.StatingMultiFunction.Code ||
                        item.RoleCode == StatingType.StatingRework.Code)
                        continue;

                    var ts = new ToolStripMenuItem(item.RoleName, null, TsMenuItemChangeStatingRole_Click);

                    ts.Tag = new StatingInfoModel() { StatingId = prInfo.StatingId, StatingRoleId = item.Id, StatingRoleName = item.RoleName };
                    adjustmentStatingRoles.DropDownItems.Add(ts);
                }
            }
        }

        private void TsMenuItemChangeStatingRole_Click(object sender, EventArgs e)
        {
            var tm = sender as ToolStripMenuItem;
            var sInfo = tm.Tag as StatingInfoModel;
            if (null == sInfo) return;
            new StatingServiceImpl().AdjustmentStatingRoles(sInfo.StatingId, sInfo.StatingRoleId, sInfo.StatingRoleName);
            //MessageBox.Show("已调整");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
        }

        //站点角色
        private IList<DaoModel.StatingRoles> _StatingRoles;
        void InitStatingRole()
        {
            _StatingRoles = Action.Common.CommonAction.GetList<DaoModel.StatingRoles>()?.Where(o => o.RoleCode != StatingType.StatingMultiFunction.Code && o.RoleCode != StatingType.StatingRework.Code && o.RoleCode != StatingType.StatingStorage.Code).ToList();
        }
        private void AdjustmentStatingRoles_Click(object sender, EventArgs e)
        {

        }

        private void LookOnlineProducts_Click(object sender, EventArgs e)
        {

        }

        private void LookInStatingProdcuts_Click(object sender, EventArgs e)
        {

        }

        private void UpdateInStatingHangerNum_Click(object sender, EventArgs e)
        {
            var gv = (gridControl1.MainView as GridView);
            var row = ((ToolStripMenuItem)sender).Tag as ProductRealtimeInfoModel;
            var vTitle = string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptInput"), Client.Action.LanguageAction.Instance.BindLanguageTxt("InStatingHangerNum"));
            var spMain = new StatingPropertyMaintain(1, string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleStatingInHangerItem")//"修改【{0}-{1}】站点站内数"
                , row.GroupNO?.Trim(), row.StatingNo?.Trim()), vTitle);//"请输入站内衣架数");
            spMain.GroupNo = row.GroupNO?.Trim();
            spMain.StatingNo = row.StatingNo?.Trim();
            spMain.ShowDialog();
        }

        private void UpdateStatingCapacity_Click(object sender, EventArgs e)
        {
            var vTitle = string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptInput"), Client.Action.LanguageAction.Instance.BindLanguageTxt("Capacity"));
            var gv = (gridControl1.MainView as GridView);
            var row = ((ToolStripMenuItem)sender).Tag as ProductRealtimeInfoModel;
            var spMain = new StatingPropertyMaintain(2, string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleUpdateInstatingCap")//"修改【{0}-{1}】站点容量"
                , row.GroupNO?.Trim(), row.StatingNo?.Trim()), vTitle//"请输入容量"
                );
            spMain.GroupNo = row.GroupNO?.Trim();
            spMain.StatingNo = row.StatingNo?.Trim();
            spMain.ShowDialog();
        }

        private void NonOnlineEmployeeAction_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                var dt = gridControl1.DataSource as List<ProductRealtimeInfoModel>;
                if (null == dt)
                {
                    // XtraMessageBox.Show("无信息可编辑!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleNoMessageEdit"));
                    return;
                }
                //var gv = (gridControl1.MainView as GridView);
                //var row = gv.GetRow(gv.FocusedRowHandle) as ProductRealtimeInfoModel;
                var row = ((ToolStripMenuItem)sender).Tag as ProductRealtimeInfoModel;
                dt = new List<ProductRealtimeInfoModel>() { row };
                if (dt.Count == 0)
                {
                    // XtraMessageBox.Show("站点员工已是离线状态!");

                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompStatingEmployeeOffline"));
                    return;
                }
                productionLineSetAction.Model.ProductRealtimeInfoModelList = dt;
                productionLineSetAction.EmployeeOffline();
                //XtraMessageBox.Show("操作成功!");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {

                SusTransitionManager.EndTransition();
            }
        }
        EmployeeDialog emDialog = null;
        private void LoginEmployeeAction_Click(object sender, EventArgs e)
        {
            //var gv = gridControl1.MainView as SusGridView;
            //if (null == gv) return;
            var pInfo = ((ToolStripMenuItem)sender).Tag as ProductRealtimeInfoModel;
            DaoModel.EmployeeModel em = null;
            if (!string.IsNullOrEmpty(pInfo.Code))
            {
                em = new DaoModel.EmployeeModel();
                em.Code = pInfo.Code;
                em.RealName = pInfo.RealName;
                em.StatingNo = pInfo.StatingNo;
            }
            if (null == emDialog || (emDialog != null && emDialog.IsDisposed))
                emDialog = new EmployeeDialog(em, pInfo.MainTrackNumber.Value, int.Parse(pInfo.StatingNo), this);
            else
            {
                emDialog.Employee = em;
                emDialog.MainTrackNumber = pInfo.MainTrackNumber.Value;
                emDialog.StatingNo = int.Parse(pInfo.StatingNo);

            }
            emDialog.TopMost = true;
            emDialog.Show();
            SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup?.GroupNo?.Trim());
        }

        private string flowChartId;
        public void SearchProductRealtimeInfo(string flowChartId, string groupNO)
        {
            var list = ProductsAction.Instance.SearchProductRealtimeInfo(flowChartId, groupNO);
            gridControl1.DataSource = list;

        }
        private void BindFlowChartData()
        {
            var sql = string.Format("select * from ProcessFlowChart where Id in(select PROCESSFLOWCHART_Id from Products where Status = 2)");
            var list = CommonAction.GetList<DaoModel.ProcessFlowChart>(sql);
            gdFlowChart.DataSource = list;
        }

        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new SusGridView();
            //标准工时:工序sam总和
            //实时总工时：工序制作时间总和
            //车缝效率=标准总工时÷实际总投入工时×100%
            //标准总工时 = 标准工时×实际产出数
            //生产效率也叫车缝效率
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNO",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站号",FieldName="StatingNo",Visible=true,Name="StatingNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="接收",FieldName="IsReceivingHanger",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="IsReceivingHanger"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车",FieldName="ClothingCar",Visible=true,Name="ClothingCar"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="故障信息",FieldName="FaultInfo",Visible=true,Name="FaultInfo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="分配的工序",FieldName="ProcessFlowName",Visible=true,Name="ProcessFlowName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Code",Visible=true,Name="Code"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="姓名",FieldName="RealName",Visible=true,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="满站",FieldName="FullSite",Visible=true,Name="FullSite"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="线上衣数",FieldName="OnlineHangerCount",Visible=true,Name="OnlineHangerCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站内衣数",FieldName="StatingInCount",Visible=true,Name="StatingInCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="容量",FieldName="Capacity",Visible=true,Name="Capacity"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产出",FieldName="OutSiteNoCount",Visible=true,Name="OutSiteNoCount"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日返工",FieldName="TodayReworkCount",Visible=true,Name="TodayReworkCount"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准总工时",FieldName="TotalStanardHours",Visible=true,Name="TotalStanardHours"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="实时总工时",FieldName="ReailHours",Visible=true,Name="ReailHours"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车缝效率",FieldName="SeamsEfficiencySite",Visible=true,Name="SeamsEfficiencySite"},

                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产出全部)",FieldName="TodayOutAll",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日返工(全部)",FieldName="TodayReworkAll",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="返工率(全部)",FieldName="ReworkRate",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准总工时(分全部)",FieldName="StandardPartialAll",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="实时总工时(分全部)",FieldName="RealyHoursPartialAll",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车缝效率(全部)",FieldName="SeamsEfficiencyAll",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="固件(SN)",FieldName="FirmwareSN",Visible=true,Name="FirmwareSN"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="固件(版本)",FieldName="FirmwareVersion",Visible=true,Name="FirmwareVersion"}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            //gridView.ScrollStyle = ScrollStyleFlags.LiveVertScroll;
            gridView.CellValueChanging += GridView_CellValueChanging;
            gridView.CustomDrawCell += GridView_CustomDrawCell;
            gridView.Columns["StatingInCount"].OptionsColumn.AllowEdit = false;
            gridView.Columns["OnlineHangerCount"].OptionsColumn.AllowEdit = false;

            gridView.OptionsView.EnableAppearanceEvenRow = false;
            gridView.OptionsView.EnableAppearanceOddRow = false;

            gridView.Appearance.EvenRow.BackColor = Color.White;
            gridView.Appearance.OddRow.BackColor = Color.White;


            //列不可编辑
            var list = new List<string>()
            { "IsReceivingHanger"};
            foreach (GridColumn c in gridView.Columns)
            {
                if (!list.Contains(c.FieldName))
                {
                    c.OptionsColumn.AllowEdit = false;
                }
                //列不可编辑
                //treeList1.Columns["IsEn"].OptionsColumn.AllowEdit = false;
            }
            gridView.DoubleClick += MainView_DoubleClick;
            gridView.MouseUp += GridView_MouseUp;
        }
        Font cellFont = new Font(AppearanceObject.DefaultFont, FontStyle.Regular);
        Brush interestBrush = Brushes.Red, principalBrush = Brushes.LightGreen, interestForeBrush = Brushes.White, principalForeBrush = Brushes.Black;
        Pen paymentPen = Pens.Green;
        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            GridView view = sender as GridView;
            ProductRealtimeInfoModel productRealtimeInfoModel = view.GetRow(e.RowHandle) as ProductRealtimeInfoModel;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Equals("FullSite"))
                {
                    //var m=view.GetRow(view.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                    var st = "满站";//view.GetRowCellDisplayText(e.RowHandle, view.Columns["StatusText"]);
                    var isRed = st.Equals(productRealtimeInfoModel.FullSite); //m.Status== ProductsStatusType.Onlineed.Value;
                    if (isRed)
                    {
                        Rectangle r = e.Bounds;
                        Rectangle interestRect = new Rectangle(r.X, r.Y, r.Width, r.Height);
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
                else if (e.Column.FieldName.Equals("FaultInfo"))
                {
                    //var m=view.GetRow(view.FocusedRowHandle) as SuspeSys.Domain.ProductsModel;
                  //  var st = "满站";//view.GetRowCellDisplayText(e.RowHandle, view.Columns["StatusText"]);
                    var isRed =!string.IsNullOrEmpty(productRealtimeInfoModel.FaultInfo); //m.Status== ProductsStatusType.Onlineed.Value;
                    if (isRed)
                    {
                        Rectangle r = e.Bounds;
                        Rectangle interestRect = new Rectangle(r.X, r.Y, r.Width, r.Height);
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
                //FaultInfo
                else if (e.Column.FieldName == "StatingNo")
                {
                    if (e.CellValue != null)
                    {
                        var statingType = StatingType.StatingTypeList.Where(o => o.Desption.Equals(productRealtimeInfoModel.StatingName?.ToString().Trim())).FirstOrDefault();

                        if (statingType != null)
                        {
                            unchecked
                            {
                                e.Appearance.BackColor = Color.FromArgb((int)statingType.EndARGB);
                                e.Appearance.BackColor2 = Color.FromArgb((int)statingType.BeginARGB);
                            }
                            //e.Appearance.ForeColor = Color.Red;
                        }



                    }
                }
                else if (e.Column.FieldName == "IsReceivingHanger")
                {
                    if (e.CellValue != null)
                    {
                        var isReceivingHanger = productRealtimeInfoModel.IsReceivingHanger;

                        if ((isReceivingHanger != null && !isReceivingHanger.Value) || isReceivingHanger == null)
                        {
                            unchecked
                            {
                                e.Appearance.BackColor = Color.Red;
                                e.Appearance.BackColor2 = Color.Red;
                            }
                            //e.Appearance.ForeColor = Color.Red;
                        }



                    }
                }
            }
        }

        private void GridView_MouseUp(object sender, MouseEventArgs e)
        {
            SusGridView list = (SusGridView)sender;
            GridHitInfo info = list.CalcHitInfo(e.Location);
            var row = gridControl1.MainView.GetRow(info.RowHandle) as ProductRealtimeInfoModel;
            if (row == null) return;
            RegisterGridContextMenuStrip(row);
        }

        private void GridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("IsReceivingHanger"))
            {
                try
                {
                    var selectRow = gridControl1.MainView.GetRow(e.RowHandle) as ProductRealtimeInfoModel;
                    var sRows = BeanUitls<ProductRealtimeInfoModel, ProductRealtimeInfoModel>.Mapper(selectRow);

                    this.Cursor = Cursors.WaitCursor;
                    DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    var state = Convert.ToBoolean(e.Value);

                    if (state)
                    {
                        sRows.SuspendReceive = 0;
                    }
                    else
                    {
                        sRows.SuspendReceive = 1;
                    }
                    productionLineSetAction.Model.ProductRealtimeInfoModelList = new List<ProductRealtimeInfoModel>() { sRows };
                    productionLineSetAction.SuspendOrReceiveHanger();
                    // XtraMessageBox.Show("状态推送完成!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            //else if (e.Column.FieldName.Equals("IsOnline"))
            //{
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        DialogResult dr = XtraMessageBox.Show("确认执行此操作吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (dr == DialogResult.No)
            //        {
            //            return;
            //        }
            //        var state = Convert.ToBoolean(e.Value);
            //        var selectRow = gridControl1.MainView.GetRow(e.RowHandle) as ProductRealtimeInfoModel;
            //        if (state)
            //        {
            //            selectRow.IsOnline = 0;
            //        }
            //        else
            //        {
            //            selectRow.SuspendReceive = 1;
            //        }
            //        productionLineSetAction.Model.ProductRealtimeInfoModelList = new List<ProductRealtimeInfoModel>() { selectRow };
            //        productionLineSetAction.SuspendOrReceiveHanger();
            //        XtraMessageBox.Show("状态推送完成!");
            //    }
            //    finally
            //    {
            //        this.Cursor = Cursors.Default;
            //    }
            //}
        }

        private void btnRefsh_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefsh.Cursor = Cursors.WaitCursor;
                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {
                btnRefsh.Cursor = Cursors.Default;
            }
        }

        private void btnMaxScreen_Click(object sender, EventArgs e)
        {
            ucMain.MaxOrMin();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                btnClose.Cursor = Cursors.WaitCursor;
                ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
            }
            finally
            {
                btnClose.Cursor = Cursors.Default;
            }
        }
        ProductionLineSetAction productionLineSetAction = new ProductionLineSetAction();
        //全部接收衣架
        private void btnAllReceiveHanger_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                SusTransitionManager.StartTransition(ucMain.MainTabControl, Client.Action.LanguageAction.Instance.BindLanguageTxt("prompSendWaiting"));//"状态推送中，请稍后....");// "状态推送中，请稍后....");
                //if (string.IsNullOrEmpty(flowChartId)) {
                //    XtraMessageBox.Show("请选择工艺图!");
                //    return;
                //}
                var dt = gridControl1.DataSource as List<ProductRealtimeInfoModel>;
                if (null == dt)
                {
                    //XtraMessageBox.Show("无信息可编辑!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleNoMessageEdit"));
                    return;
                }
                var isReceiveCount = dt.Where(f => null != f.IsReceivingHanger && f.IsReceivingHanger.Value).ToList<ProductRealtimeInfoModel>().Count;
                if (isReceiveCount > 0)
                {
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompStatingEmployeeOffline"));
                    //XtraMessageBox.Show("站点已是接收衣架状态!");
                    return;
                }
                dt.ForEach(f => f.SuspendReceive = 0);
                productionLineSetAction.Model.ProductRealtimeInfoModelList = dt;
                productionLineSetAction.SuspendOrReceiveHanger();
                //   XtraMessageBox.Show("状态推送完成!");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {

                SusTransitionManager.EndTransition();
            }
        }
        //全部暂停进衣
        private void btnAllSuspendHanger_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }

                SusTransitionManager.StartTransition(ucMain.MainTabControl, Client.Action.LanguageAction.Instance.BindLanguageTxt("prompSendWaiting"));//"状态推送中，请稍后....");

                //if (string.IsNullOrEmpty(flowChartId)) {
                //    XtraMessageBox.Show("请选择工艺图!");
                //    return;
                //}
                var dt = gridControl1.DataSource as List<ProductRealtimeInfoModel>;
                if (null == dt)
                {
                    // XtraMessageBox.Show("无信息可编辑!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleNoMessageEdit"));
                    return;
                }
                var isReceiveCount = dt.Where(f => null != f.IsReceivingHanger && !f.IsReceivingHanger.Value).ToList<ProductRealtimeInfoModel>().Count;
                if (isReceiveCount > 0)
                {
                    //   XtraMessageBox.Show("站点已是暂停接收衣架状态!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompStatingStopOrReg"));
                    return;
                }
                dt.ForEach(f => f.SuspendReceive = 1);
                productionLineSetAction.Model.ProductRealtimeInfoModelList = dt;
                productionLineSetAction.SuspendOrReceiveHanger();
                //XtraMessageBox.Show("状态推送完成!");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {

                SusTransitionManager.EndTransition();
            }
        }
        //全部离线
        private void btnEmployeeLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                var dt = gridControl1.DataSource as List<ProductRealtimeInfoModel>;
                if (null == dt)
                {
                    //XtraMessageBox.Show("无信息可编辑!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleNoMessageEdit"));
                    return;
                }
                dt = dt.Where(f => (null != f.IsOnline && f.IsOnline.Value)).ToList<ProductRealtimeInfoModel>();
                if (dt.Count == 0)
                {
                    //XtraMessageBox.Show("站点员工已是离线状态!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompStatingEmployeeOffline"));
                    return;
                }
                productionLineSetAction.Model.ProductRealtimeInfoModelList = dt;
                productionLineSetAction.EmployeeOffline();
                //XtraMessageBox.Show("操作成功!");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));
                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {

                SusTransitionManager.EndTransition();
            }
        }
        //重置站内衣数
        private void btnResetStatingInHangerNum_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dr = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmExecuteOp")//"确认执行此操作吗?"
, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips")
                    //, "温馨提示"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                var dt = gridControl1.DataSource as List<ProductRealtimeInfoModel>;
                if (null == dt)
                {
                    // XtraMessageBox.Show("无信息可编辑!");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleNoMessageEdit"));
                    return;
                }

                productionLineSetAction.Model.ProductRealtimeInfoModelList = dt;
                productionLineSetAction.ResetStatingInNum();
                //XtraMessageBox.Show("操作成功!");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("titleOpterateSucess"));

                SearchProductRealtimeInfo(flowChartId, CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());
            }
            finally
            {

                SusTransitionManager.EndTransition();
            }
        }
    }
    class StatingInfoModel
    {
        public string StatingId { get; internal set; }
        public string StatingRoleId { get; internal set; }
        public string StatingRoleName { get; internal set; }
    }
}

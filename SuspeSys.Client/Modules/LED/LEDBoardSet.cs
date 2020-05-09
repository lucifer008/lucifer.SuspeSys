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
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using SuspeSys.Utils;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.LED;
using DevExpress.XtraGrid.Columns;

namespace SuspeSys.Client.Modules.LED
{
    public partial class LEDBoardSet : SusXtraUserControl
    {
        public LEDBoardSet()
        {
            InitializeComponent();
        }
        public LEDBoardSet(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;

            RegisterGridContextMenuStrip();
            BindGridHeaderScreen(gridControl1);
            BindGridHeaderPage(gridControl2);
            RegisterEvent();
            QueryData();
        }

        private void QueryData()
        {

            var listLedScreen = CommonAction.GetList<LedScreenConfig>();

            var listPage = CommonAction.GetList<LedScreenPage>();

            listLedScreen.ToList<LedScreenConfig>().ForEach(f => f.LSCId = f.Id);
            var siteGroupList = CommonAction.GetList<SiteGroup>();
            foreach (var lls in listLedScreen)
            {
                if (!string.IsNullOrEmpty(lls.GroupNo))
                {
                    var lgList = siteGroupList.Where(f => f.GroupNo.Equals(lls.GroupNo));
                    if (lgList.Count() > 0)
                    {
                        var siteGroup = lgList.First();
                        lls.GroupNoMainTrackNumber = string.Format("{0}",siteGroup.GroupNo?.Trim());
                    }

                }
            }
            foreach (var lp in listPage)
            {
                var ls = listLedScreen.ToList<LedScreenConfig>().Where(f => null != lp.LedScreenConfig && f.Id.Equals(lp.LedScreenConfig.Id)).First();
                lp.ParentId = ls.Id;
            }
            if (listLedScreen.Count > 0)
            {
                var screen = listLedScreen.First();
                gridControl2.DataSource = listPage.Where(f => f.ParentId != null && f.ParentId.Equals(screen.Id)).ToList<LedScreenPage>();
            }

            gridControl1.DataSource = listLedScreen;
        }

        private void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            (gridControl1.MainView as SusGridView).CellValueChanged += LEDConfigGridView_CellValueChanged;
            (gridControl2.MainView as SusGridView).CellValueChanged += LEDConfigPageGridView_CellValueChanged;
            (gridControl1).MouseDown += GridViewFlowChart_MouseDown;
        }

        private void GridViewFlowChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                // ColumnView cv = (ColumnView)gridControl1.FocusedView;
                var downHitInfo = (gridControl1.MainView as SusGridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = (gridControl1.MainView as SusGridView).GetRow(downHitInfo.RowHandle) as LedScreenConfig;
                if (null != selecRow)
                {
                    //int focusedhandle = cv.FocusedRowHandle;
                    object rowIdObj = selecRow.Id;//((SusGridView)gridControl1.MainView).GetRowCellValue(focusedhandle, "Id");
                    if (!string.IsNullOrEmpty(Convert.ToString(rowIdObj)))
                    {
                        var listPage = CommonAction.GetList<LedScreenPage>();
                        listPage = listPage.Where(f => null != f.ParentId && f.ParentId.Equals(Convert.ToString(rowIdObj))).ToList<LedScreenPage>();
                        gridControl2.DataSource = listPage;
                    }
                }
            }
        }

        private IList<int> ledScreenConfigModifiedRows = new List<int>();
        private void LEDConfigGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as SusGridView;

            int sourceHandle = view.GetDataSourceRowIndex(e.RowHandle);
            if (!ledScreenConfigModifiedRows.Contains(e.RowHandle))
                ledScreenConfigModifiedRows.Add(sourceHandle);
        }
        private IList<int> ledScreenConfigPageModifiedRows = new List<int>();
        private void LEDConfigPageGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as SusGridView;

            int sourceHandle = view.GetDataSourceRowIndex(e.RowHandle);
            if (!ledScreenConfigPageModifiedRows.Contains(e.RowHandle))
                ledScreenConfigPageModifiedRows.Add(sourceHandle);
        }
        void RegisterGridContextMenuStrip()
        {

            contextMenuStrip1.Items.Clear();
            var addScreen = new ToolStripMenuItem() { Text = "新增屏幕", Name = "NewScreen" };
            var deleteScreen = new ToolStripMenuItem() { Text = "删除屏幕", Name = "DeleteScreen" };
            addScreen.Click += AddScreen_Click;
            deleteScreen.Click += DeleteScreen_Click;
            var addPage = new ToolStripMenuItem() { Text = "新增页面", Name = "addPage" };
            addPage.Click += AddPage_Click;
            var deletePage = new ToolStripMenuItem() { Text = "删除页面", Name = "deletePage" };
            deletePage.Click += DeletePage_Click;
            contextMenuStrip1.Items.AddRange(new ToolStripMenuItem[] { addScreen, deleteScreen });
            contextMenuStrip2.Items.AddRange(new ToolStripMenuItem[] { addPage, deletePage });
        }

        private void DeletePage_Click(object sender, EventArgs e)
        {

            DeletePageItem();
        }

        private void DeletePageItem()
        {
            SusGridView gv = (gridControl2.MainView as SusGridView);
            var rowIndex = gv.FocusedRowHandle;
            DialogResult dr = XtraMessageBox.Show("确认删除吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            var list = gv.DataSource as List<LedScreenPage>;
            if (list != null && list.Count != 0)
            {
                if (!string.IsNullOrEmpty(list[rowIndex].Id))
                {
                    LEDAction.Instance.DeleteLedScreenPage(list[rowIndex].Id);
                }
            }
            gv.DeleteRow(rowIndex);

        }

        private void AddPage_Click(object sender, EventArgs e)
        {
            AddPageItem();
        }

        private void DeleteScreen_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("删除屏幕会将屏幕及页面一起删除，确认删除吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            SusGridView gv = (gridControl1.MainView as SusGridView);
            var rowIndex = gv.FocusedRowHandle;
            var list = gv.DataSource as List<LedScreenConfig>;
            if (list != null && list.Count != 0)
            {
                if (!string.IsNullOrEmpty(list[rowIndex].Id))
                {
                    LEDAction.Instance.DeleteLEDConfig(list[rowIndex].Id);
                }
            }
            gv.DeleteRow(rowIndex);
            QueryData();
        }

        private void AddScreen_Click(object sender, EventArgs e)
        {
            AddScreenItem();
        }

        private void BindGridHeaderScreen(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();


            var controllerTypeComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            controllerTypeComboBox.NullValuePrompt = "请选择控制器类型";
            controllerTypeComboBox.SelectedValueChanged += ControllerTypeComboBox_SelectedValueChanged;
            controllerTypeComboBox.ParseEditValue += ControllerTypeComboBox_ParseEditValue;

            controllerTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "EQ2011",
                Value = 7,
                Tag = "EQ2011-7"
            });

            var communicationWayComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            communicationWayComboBox.NullValuePrompt = "请选择控制器类型";
            communicationWayComboBox.SelectedValueChanged += CommunicationWayComboBox_SelectedValueChanged; ;
            communicationWayComboBox.ParseEditValue += CommunicationWayComboBox_ParseEditValue; ;

            communicationWayComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "串口",
                Value = 0,
                Tag = "串口-0"
            });
            communicationWayComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "网络",
                Value = 1,
                Tag = "网络-1"
            });


            var colorTypeComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            colorTypeComboBox.NullValuePrompt = "请选择控制器类型";
            colorTypeComboBox.SelectedValueChanged += ColorTypeComboBox_SelectedValueChanged;
            colorTypeComboBox.ParseEditValue += ColorTypeComboBox_ParseEditValue; ;

            colorTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "单色屏",
                Value = 0,
                Tag = "单色屏-0"
            });
            colorTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "双色屏",
                Value = 1,
                Tag = "双色屏-1"
            });



            var groupComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            groupComboBox.NullValuePrompt = "请选择控制器类型";
            groupComboBox.SelectedValueChanged += GroupComboBox_SelectedValueChanged;
            groupComboBox.ParseEditValue += GroupComboBox_ParseEditValue;
            var sql = string.Format(@"select distinct sg.GroupNo,st.MainTrackNumber from SiteGroup sg
inner join Stating st on st.SITEGROUP_Id=sg.Id
where st.Deleted=0");
            var listGroup = CommonAction.GetList<SiteGroup>(sql);
            foreach (var gp in listGroup)
            {
                groupComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = string.Format("{0}",  gp.GroupNo?.Trim()),
                    Value = gp.GroupNo?.Trim(),
                    Tag = gp
                });

            }

            var gridView = new Modules.Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
             //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="屏号",FieldName="ScreenNo",Visible=true,Name="ScreenNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="控制器类型",FieldName="ControllerTypeTxt",Visible=true,ColumnEdit=controllerTypeComboBox,Name="ControllerTypeTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="通信方式",FieldName="CommunicationWayTxt",Visible=true,ColumnEdit=communicationWayComboBox,Name="CommunicationWayTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="显示宽度",FieldName="SWidth",Visible=true,Name="Widht"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="显示屏高度",FieldName="SHeight",Visible=true,Name="Height"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色类型",FieldName="ColorTypeTxt",Visible=true,ColumnEdit=colorTypeComboBox,Name="ColorTypeTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="IP地址",FieldName="IpAddress",Visible=true,Name="IpAddress"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="端口号",FieldName="Port",Visible=true,Name="Port"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生产组",FieldName="GroupNoMainTrackNumber",Visible=true,ColumnEdit=groupComboBox,Name="GroupNo"},
                    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="启用",FieldName="Enable",Visible=true,Name="Enable" } });


            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            ////列不可编辑
            //var list = new List<string>()
            //{ "IsReceivingHanger"};
            //foreach (GridColumn c in gridView.Columns)
            //{
            //    if (!list.Contains(c.FieldName))
            //    {
            //        c.OptionsColumn.AllowEdit = false;
            //    }
            //    //列不可编辑
            //    //treeList1.Columns["IsEn"].OptionsColumn.AllowEdit = false;
            //}
            gridView.Columns["ScreenNo"].OptionsColumn.AllowEdit = false;

        }

        private void GroupComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void GroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                SusGridView gv = (gridControl1.MainView as SusGridView);
                var list = gridControl1.DataSource as List<LedScreenConfig>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var siteGroup = item.Tag as SiteGroup;
                list[dataIndex].GroupNo = siteGroup.GroupNo?.Trim();
                gv.SetFocusedRowCellValue("GroupNoMainTrackNumber", string.Format("{0}", siteGroup.GroupNo?.Trim()));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void ColorTypeComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void ColorTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                SusGridView gv = (gridControl1.MainView as SusGridView);
                var list = gridControl1.DataSource as List<LedScreenConfig>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var ctArr = item.Tag.ToString().Split('-');
                list[dataIndex].ColorType = int.Parse(ctArr[1]);
                list[dataIndex].ColorTypeTxt = ctArr[0];
                gv.SetFocusedRowCellValue("ColorTypeTxt", ctArr[0]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void CommunicationWayComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void CommunicationWayComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                SusGridView gv = (gridControl1.MainView as SusGridView);
                var list = gridControl1.DataSource as List<LedScreenConfig>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var ctArr = item.Tag.ToString().Split('-');
                list[dataIndex].CommunicationWay = int.Parse(ctArr[1]);
                list[dataIndex].CommunicationWayTxt = ctArr[0];
                gv.SetFocusedRowCellValue("CommunicationWayTxt", ctArr[0]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void ControllerTypeComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void ControllerTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                SusGridView gv = (gridControl1.MainView as SusGridView);
                var list = gridControl1.DataSource as List<LedScreenConfig>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var ctArr = item.Tag.ToString().Split('-');
                list[dataIndex].ControllerKey = ctArr[1];
                list[dataIndex].ControllerTypeTxt = ctArr[0];
                gv.SetFocusedRowCellValue("ControllerTypeTxt", ctArr[0]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void BindGridHeaderPage(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();

            var InfoTypeComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            InfoTypeComboBox.NullValuePrompt = "请选择控制器类型";
            InfoTypeComboBox.SelectedValueChanged += InfoTypeComboBox_SelectedValueChanged; ;
            InfoTypeComboBox.ParseEditValue += InfoTypeComboBox_ParseEditValue; ;

            InfoTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "自定义",
                Value = 130,
                Tag = "130-自定义"
            });
            InfoTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "制单日产量",
                Value = 131,
                Tag = "131-制单日产量"
            });

            InfoTypeComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "小时达成率",
                Value = 136,
                Tag = "136-小时达成率"
            });
            var gridView = new Modules.Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
             //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="页面序号",FieldName="PageNo",Visible=true,Name="PageNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="信息类别",FieldName="InfoTypeTxt",ColumnEdit=InfoTypeComboBox,Visible=true,Name="InfoTypeTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="自定义信息内容",FieldName="CusContent",Visible=true,Name="CusContent"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="时长(秒)",FieldName="Times",Visible=true,Name="Times"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="刷新周期(秒)",FieldName="RefreshCycle",Visible=true,Name="RefreshCycle"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生效",FieldName="Enabled",Visible=true,Name="Enabled"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertTime",Visible=true,Name="InsertDateTime"}

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

        private void InfoTypeComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void InfoTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;

                SusGridView gv = (gridControl2.MainView as SusGridView);
                var list = gridControl2.DataSource as List<LedScreenPage>;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var ctArr = item.Tag.ToString().Split('-');
                list[dataIndex].InfoType = int.Parse(ctArr[0]);
                list[dataIndex].InfoTypeTxt = ctArr[1];
                gv.SetFocusedRowCellValue("InfoTypeTxt", ctArr[1]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        //SusTransitionManager.StartTransition(ucMain.MainTabControl, "");
                        //var productsInput = new ProductsInput();
                        //productsInput.ShowDialog();
                        Save();

                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        QueryData();
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
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

        private void Save()
        {
            //if (ledScreenConfigModifiedRows.Count == 0)
            //{
            //    XtraMessageBox.Show("数据为空或者数据未改变!");
            //    return;
            //}
            var ledConfigList = gridControl1.DataSource as List<LedScreenConfig>; //new List<LedScreenConfig>();
                                                                                  //foreach (var rowHandle in ledScreenConfigModifiedRows)
                                                                                  //{
                                                                                  //    ledConfigList.Add(gridControl1.MainView.GetRow(rowHandle) as LedScreenConfig);
                                                                                  //}
            if (null == ledConfigList && ledConfigList.Count == 0)
            {
                XtraMessageBox.Show("数据为空或者数据未改变!");
                return;
            }
            var ledConfigPageList = new List<LedScreenPage>();
            foreach (var rowHandle in ledScreenConfigPageModifiedRows)
            {
                var lp = gridControl2.MainView.GetRow(rowHandle) as LedScreenPage;
                if (lp != null)
                {
                    ledConfigPageList.Add(lp);
                }

            }
            LEDAction.Instance.SaveLedScreenInfo(ledConfigList, ledConfigPageList);
            // XtraMessageBox.Show("保存成功!");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            QueryData();
        }

        void AddScreenItem()
        {
            var dt = gridControl1.DataSource as IList<LedScreenConfig>;
            if (null != dt)
            {
                dt.Add(new LedScreenConfig() { LSCId = GUIDHelper.GetGuidString() });
            }
            else
            {
                dt = new List<LedScreenConfig>();
                dt.Add(new LedScreenConfig() { LSCId = GUIDHelper.GetGuidString() });
            }
            gridControl1.DataSource = dt;
            gridControl1.MainView.RefreshData();
        }
        void AddPageItem()
        {
            var dtScreenConfig = gridControl1.DataSource as IList<LedScreenConfig>;
            if (dtScreenConfig.Count == 0)
            {
                XtraMessageBox.Show("屏幕为空，请先添加屏幕");
                return;
            }
            var screenIndex = (gridControl1.MainView as SusGridView).FocusedRowHandle;
            int dataIndex = (gridControl1.MainView as SusGridView).GetDataSourceRowIndex(screenIndex);
            var sc = dtScreenConfig[dataIndex];

            var dt = gridControl2.DataSource as IList<LedScreenPage>;
            if (null != dt)
            {
                dt.Add(new LedScreenPage() { ParentId = sc.LSCId });
            }
            else
            {
                dt = new List<LedScreenPage>();
                dt.Add(new LedScreenPage() { ParentId = sc.LSCId });
            }
            gridControl2.DataSource = dt;
            gridControl2.MainView.RefreshData();
        }
    }
}

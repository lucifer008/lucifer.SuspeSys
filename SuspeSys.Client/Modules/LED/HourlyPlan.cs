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
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.LED;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;

namespace SuspeSys.Client.Modules.LED
{
    public partial class HourlyPlan : SusXtraUserControl
    {
        public HourlyPlan()
        {
            InitializeComponent();
        }
        public HourlyPlan(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            BindGridHeadeGroup(gcProductGroup);
            BindGridHeadeGroupDetail(gcProductsGroupItem);
            RegisterEvent();
            QueryData();
        }

        private void QueryData()
        {
            var beginDate = deBeginDate.DateTime.FormatDate();
            var endDate = deEndDate.DateTime.FormatDate();
            var gropNo = txtGroupNo.Text?.Trim();
            var listLEDHoursPlanTableItem = CommonAction.GetList<LedHoursPlanTableItem>();
            if (!string.IsNullOrEmpty(beginDate.ToString()))
            {
                listLEDHoursPlanTableItem = listLEDHoursPlanTableItem.Where(f => f.BeginDate >= beginDate).ToList<LedHoursPlanTableItem>();
            }
            if (!string.IsNullOrEmpty(endDate.ToString()))
            {
                listLEDHoursPlanTableItem = listLEDHoursPlanTableItem.Where(f => f.EndDate <= endDate).ToList<LedHoursPlanTableItem>();
            }
            if (!string.IsNullOrEmpty(gropNo))
            {
                listLEDHoursPlanTableItem = listLEDHoursPlanTableItem.Where(f => f.GroupNo.Contains(gropNo)).ToList<LedHoursPlanTableItem>();
            }

            foreach (var lp in listLEDHoursPlanTableItem)
            {
                lp.BeginDateShortDate = lp.BeginDate.Value.ToString("HH:mm");
                lp.EndDateShortDate = lp.EndDate.Value.ToString("HH:mm");
                lp.PlanDate = lp.BeginDate.Value.ToString("yyyy-MM-dd");
            }
            gcProductsGroupItem.DataSource = listLEDHoursPlanTableItem;
        }

        private void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            //  (gridControl1.MainView as SusGridView).CellValueChanged += LEDConfigGridView_CellValueChanged;
            (gcProductsGroupItem.MainView as SusGridView).CellValueChanged += HourlyPlan_CellValueChanged;
            gcProductGroup.MainView.Click += MainView_Click;
        }

        private void MainView_Click(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SusGridView view = sender as SusGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {

                //string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                if (info.Column == null)
                {
                    return;
                }
                var rowHandle = info.RowHandle;
                var listSiteGroup = gcProductGroup.DataSource as List<SiteGroup>;
                // (gcProductsGroupItem.MainView as SusGridView)?.ClearRows();
                gcProductsGroupItem.DataSource = null;
                if (null != listSiteGroup && listSiteGroup.Count > 0)
                {
                    var groupNo = listSiteGroup[rowHandle]?.GroupNo?.Trim();
                    var lhpItem = CommonAction.GetList<LedHoursPlanTableItem>().Where(f => null != f.GroupNo && f.GroupNo.Equals(groupNo));
                    gcProductsGroupItem.DataSource = lhpItem;
                }

            }
        }

        private IList<int> ledGroupItemModifiedRows = new List<int>();
        private void HourlyPlan_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as SusGridView;

            int sourceHandle = view.GetDataSourceRowIndex(e.RowHandle);
            if (!ledGroupItemModifiedRows.Contains(e.RowHandle))
                ledGroupItemModifiedRows.Add(sourceHandle);
        }

        private void BindGridHeadeGroup(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Modules.Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
             //   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="QueryDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="线号",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNo"}

            });

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

            var listGroup = CommonAction.GetList<SiteGroup>();
            gcProductGroup.DataSource = listGroup;

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
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Delete:
                        DelteHourPlan();
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
        void DelteHourPlan()
        {
            SusGridView gv = (gcProductsGroupItem.MainView as SusGridView);
            var rowIndex = gv.FocusedRowHandle;
            DialogResult dr = XtraMessageBox.Show("确认删除吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            var list = gv.DataSource as List<LedHoursPlanTableItem>;
            if (list != null && list.Count != 0)
            {
                if (!string.IsNullOrEmpty(list[rowIndex].Id))
                {
                    LEDAction.Instance.DeleteLedLedHoursPlanTableIteme(list[rowIndex].Id);
                }
            }
            QueryData();
        }
        private void AddItem()
        {
            var siteGroupList = gcProductGroup.DataSource as IList<SiteGroup>;
            if (siteGroupList.Count == 0)
            {
                XtraMessageBox.Show("组别为空，请先添加组别");
                return;
            }
            var screenIndex = (gcProductGroup.MainView as SusGridView).FocusedRowHandle;
            int dataIndex = (gcProductGroup.MainView as SusGridView).GetDataSourceRowIndex(screenIndex);
            var sc = siteGroupList[dataIndex];

            var dt = gcProductsGroupItem.DataSource as IList<LedHoursPlanTableItem>;
            if (null != dt)
            {
                dt.Add(new LedHoursPlanTableItem() { GroupNo = sc.GroupNo?.Trim() });
            }
            else
            {
                dt = new List<LedHoursPlanTableItem>();
                dt.Add(new LedHoursPlanTableItem() { GroupNo = sc.GroupNo?.Trim() });
            }
            gcProductsGroupItem.DataSource = dt;
            gcProductsGroupItem.MainView.RefreshData();
        }

        private void Save()
        {
            if (ledGroupItemModifiedRows.Count == 0)
            {
                XtraMessageBox.Show("数据为空或者数据未改变!");
                return;
            }
            var ledHoursPlanTableItemList = new List<LedHoursPlanTableItem>();
            foreach (var rowHandle in ledGroupItemModifiedRows)
            {
                var lgItem = gcProductsGroupItem.MainView.GetRow(rowHandle) as LedHoursPlanTableItem;
                lgItem.BeginDate = Convert.ToDateTime(lgItem.BeginDate.Value.ToString("yyyy-MM-dd") + " " + lgItem.BeginDateShortDate);
                lgItem.EndDate = Convert.ToDateTime(lgItem.BeginDate.Value.ToString("yyyy-MM-dd") + " " + lgItem.EndDateShortDate);
                ledHoursPlanTableItemList.Add(lgItem);
            }

            LEDAction.Instance.SaveLedHoursPlanTableItem(ledHoursPlanTableItemList);
            // XtraMessageBox.Show("保存成功!");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            QueryData();
        }

        private void BindGridHeadeGroupDetail(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Modules.Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="BeginDate",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="开始时间",FieldName="BeginDateShortDate",ColumnEdit=new RepositoryItemTextEdit(),Visible=true,Name="BeginDateShortDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="结束时间",FieldName="EndDateShortDate",ColumnEdit=new RepositoryItemTextEdit() ,Visible=true,Name="EndDateShortDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="计划数量",FieldName="PlanNum",Visible=true,Name="PlanNum"}//,
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDate",Visible=true,Name="InsertDateTime"}


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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Cursor = Cursors.WaitCursor;
                QueryData();
            }
            finally {
                btnQuery.Cursor = Cursors.Default;
            }
        }
    }
}

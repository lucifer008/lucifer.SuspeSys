using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuspeSys.Client.Action.Attendance;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Common;

namespace SuspeSys.Client.Modules.Attendance
{
    /// <summary>
    /// 假日信息
    /// </summary>
    public partial class HolidayIndex  : Ext.CRUDControl
    {
        AttendanceAction attendanceAction = AttendanceAction.Instance;

        public HolidayIndex()
        {
            InitializeComponent();
        }

        public HolidayIndex(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }

        private void HolidayIndex_Load(object sender, EventArgs e)
        {
            //RegisterEvent();
            //BindGridHeader();
            //Query(1);
        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {
            

            base.InitToolBarButton(susToolBar);
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请输入内容搜索";

            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
              //  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="顺序号",FieldName="BIndex",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排序号",FieldName="OrderNo",Visible=true,Name="SortNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="假日日期",FieldName="HolidayDateTime",Visible=true,ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit(),Name="HolidayDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工作日",FieldName="WorkDay",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit(),Visible=true,Name="WorkDay"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=true,Name="Memo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"}
            });
            // gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["UpdateDateTime"].OptionsColumn.AllowFocus = false;
            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["OrderNo"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            base.BindGridHeader(dataGrid);
        }

        protected override void AddItem(GridControl dataGrid)
        {

            var maxOrderNo = this.MaxOrderNo(dataGrid);

            var dt = dataGrid.DataSource as IList<Domain.HolidayInfo>;
            if (null != dt)
            {
                dt.Add(new Domain.HolidayInfo() { OrderNo = maxOrderNo + 1 }) ;
            }
            dataGrid.MainView.RefreshData();

            base.AddItem(dataGrid);
        }

        private double MaxOrderNo(GridControl dataGrid)
        {
            double maxOrderNo = 0;
            var list = dataGrid.DataSource as List<Domain.HolidayInfo>;
            if (list != null)
                maxOrderNo = list.Max(o => o.OrderNo ?? 0);

            var dbMaxOrderNo = new CommonAction().GetMaxOrderNo<double>("OrderNo", "HolidayInfo");

            if (maxOrderNo > dbMaxOrderNo)
                return maxOrderNo;
            else
                return dbMaxOrderNo;
        }

        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = attendanceAction.SeacherHolidayInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }

        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();

            var list = dataGrid.DataSource as List<Domain.HolidayInfo>;
            if (null != list)
            {
                var listDis = new List<string>();
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog<Domain.HolidayInfo > (m.Id, m, "更新假日信息", "HolidayInfo", m.GetType().Name);
                        action.Update<Domain.HolidayInfo > (m);

                    }
                    else
                    {
                        action.Save<Domain.HolidayInfo>(m);
                        action.InsertLog<Domain.HolidayInfo>(m.Id, "添加假日信息", "HolidayInfo", m.GetType().Name);
                    }
                }
            }

            base.Save(dataGrid);

            XtraMessageBox.Show("保存成功");
        }
    }
}

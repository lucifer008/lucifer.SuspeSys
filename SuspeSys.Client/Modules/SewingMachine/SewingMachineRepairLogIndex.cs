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
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.ClothingVehicle;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Modules.Ext;

namespace SuspeSys.Client.Modules.SewingMachine
{
    /// <summary>
    /// 衣车维修日志
    /// </summary>
    public partial class SewingMachineRepairLogIndex : Ext.CRUDControl
    {
        public SewingMachineRepairLogIndex()
        {
           // InitializeComponent();
            
        }
        public SewingMachineRepairLogIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }
        /// <summary>
        /// 员工先报修，机修刷卡开始维修，维修完成后刷员工卡结束完成报修
        /// </summary>
        /// <param name="gc"></param>
        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new SusGridView();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="Code",Visible=true,Name="Code"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="workshop",Visible=true,Name="workshop"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="productGroup",Visible=true,Name="productGroup"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="StatingNo",Visible=true,Name="StatingNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车编号",FieldName="ClothingCarNo",Visible=true,Name="ClothingCarNo"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车卡号",FieldName="SewingMachineCardNo",Visible=true,Name="SewingMachineCardNo"},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修时间",FieldName="ReportTime",Visible=true,Name="ReportTime"},
                   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修故障号",FieldName="ReportFaultNo",Visible=true,Name="ReportFaultNo"},
                    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修故障名称",FieldName="ReportFaultName",Visible=true,Name="ReportFaultName"},
                     new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修员工工号",FieldName="ReportEmployeeNo",Visible=true,Name="ReportEmployeeNo"},
                      new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修员工姓名",FieldName="ReportEmployeeName",Visible=true,Name="ReportEmployeeName"},
                       new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报修用时(分)",FieldName="ReportUseTimes",Visible=true,Name="ReportUseTimes"},
                        
                         new DevExpress.XtraGrid.Columns.GridColumn() { Caption="开始报修时间",FieldName="BeginReportTime",Visible=true,Name="BeginReportTime"},
                        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="机修工号",FieldName="MachineRepairEmployeeNo",Visible=true,Name="MachineRepairEmployeeNo"},
                         new DevExpress.XtraGrid.Columns.GridColumn() { Caption="机修姓名",FieldName="MachineRepairEmployeeName",Visible=true,Name="MachineRepairEmployeeName"},
                          new DevExpress.XtraGrid.Columns.GridColumn() { Caption="完成维修时间",FieldName="successRepairTime",Visible=true,Name="successRepairTime"},
                           new DevExpress.XtraGrid.Columns.GridColumn() { Caption="维修用时(分)",FieldName="RepairUseTimes",Visible=true,Name="RepairUseTimes"},
                            new DevExpress.XtraGrid.Columns.GridColumn() { Caption="维修故障号",FieldName="RepairFaulNo",Visible=true,Name="RepairFaulNo"},
                             new DevExpress.XtraGrid.Columns.GridColumn() { Caption="维修故障名称",FieldName="RepairFaultName",Visible=true,Name="RepairFaultName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=true,Name="Memo"}

            });
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
        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<Domain.ClothingVehicleMaintenanceLogs>;
            if (null != dt)
            {
                dt.Add(new Domain.ClothingVehicleMaintenanceLogs());
            }


            base.AddItem(dataGrid);
        }
        ClothingVehicleAction _action = new ClothingVehicleAction();
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetClothingVehicleMaintenanceLogsAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.ClothingVehicleMaintenanceLogs>();

            //var list = dataGrid.DataSource as List<Domain.DepartmentModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.ClothingVehicleMaintenanceLogs>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {

                        action.Update<Domain.ClothingVehicleMaintenanceLogs>(t);

                    }
                    else
                    {

                        action.Save<Domain.ClothingVehicleMaintenanceLogs>(t);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.ClothingVehicleMaintenanceLogs).TransformTo<Domain.ClothingVehicleMaintenanceLogs>();

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);//XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                action.PhysicsDelte<Domain.ClothingVehicleMaintenanceLogs>(selectData.Id);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}

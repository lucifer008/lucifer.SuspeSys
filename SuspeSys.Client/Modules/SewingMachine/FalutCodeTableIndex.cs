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
using SuspeSys.Client.Action.ClothingVehicle;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Domain;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Utils.Reflection;

namespace SuspeSys.Client.Modules.SewingMachine
{
    public partial class FalutCodeTableIndex : Ext.CRUDControl
    {
        ClothingVehicleAction _action = new ClothingVehicleAction();
        public FalutCodeTableIndex()
        {
            //InitializeComponent();

        }
        public FalutCodeTableIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new SusGridView();
            var sewingMachineComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            // statingDirectionComboBox.NullValuePrompt = "请选择站点角色";
            sewingMachineComboBox.SelectedValueChanged += SewingMachineComboBox_SelectedValueChanged;
            sewingMachineComboBox.ParseEditValue += new ConvertEditValueEventHandler(sewingMachineComboBox_ParseEditValue);
            var clothingVehicleTypeList = CommonAction.GetList<ClothingVehicleType>();
            foreach (var cc in clothingVehicleTypeList)
            {
                sewingMachineComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.Name?.Trim(),
                    Value = cc.Id,
                    Tag = cc
                });
            }
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="SerialNumber",Visible=true,Name="Seq"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="故障代码",FieldName="FaultCode",Visible=true,Name="FaultCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="故障名称",FieldName="FaultName",Visible=true,Name="FaultName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车类别",FieldName="ClothingVehicleName",ColumnEdit=sewingMachineComboBox,Visible=true,Name="MachineType"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="允许进衣",FieldName="POrderNo",Visible=true,Name="AllowInCloth"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="允许做货",FieldName="POrderNo",Visible=true,Name="AllowDo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"}

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

        private void sewingMachineComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
           e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void SewingMachineComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                GridView gv = (this.SusGrid.DataGrid.MainView as GridView);
                var list = this.SusGrid.DataGrid.DataSource as List<FaultCodeTableModel>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                // var clothingVehicleType = BeanUitls<ClothingVehicleType, ClothingVehicleTypeModel>.Mapper((item.Tag as ClothingVehicleType));
                list[dataIndex].ClothingVehicleType = (item.Tag as ClothingVehicleType);
                //list[dataIndex].ClothingVehicleName = (item.Tag as ClothingVehicleType).Name?.Trim();
                gv.SetFocusedRowCellValue("ClothingVehicleName", (item.Tag as ClothingVehicleType)?.Name?.Trim());
               // this.SusGrid.DataGrid.DataSource = list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<Domain.FaultCodeTableModel>;
            if (null != dt)
            {
                dt.Add(new Domain.FaultCodeTableModel());
            }


            base.AddItem(dataGrid);
        }
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetFaultCodeTableAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            //list.ToList().ForEach(delegate (FaultCodeTableModel fct)
            //{
            //    fct.ClothingVehicleName = null != fct.ClothingVehicleType ? fct.ClothingVehicleType.Name:null;
            //});
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.FaultCodeTableModel>();

            //var list = dataGrid.DataSource as List<Domain.DepartmentModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.FaultCodeTable>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {

                        action.Update<Domain.FaultCodeTable>(t);

                    }
                    else
                    {

                        action.Save<Domain.FaultCodeTable>(t);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.FaultCodeTable).TransformTo<Domain.FaultCodeTable>();

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
                action.PhysicsDelte<Domain.FaultCodeTable>(selectData.Id);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}

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
using SuspeSys.Client.Action.PersonnelManagement;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.ClothingVehicle;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace SuspeSys.Client.Modules.SewingMachine
{
    /// <summary>
    /// 衣车资料
    /// </summary>
    public partial class SewingMachineDataIndex : Ext.CRUDControl
    {
        ClothingVehicleAction _action = new ClothingVehicleAction();
        public SewingMachineDataIndex()
        {
          //  InitializeComponent();
           
        }
        public SewingMachineDataIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }
        protected override void BindGridHeader(GridControl dataGrid)
        {
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

            var workShopComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            // statingDirectionComboBox.NullValuePrompt = "请选择站点角色";
            workShopComboBox.SelectedValueChanged += WorkShopComboBox_SelectedValueChanged; ;
            workShopComboBox.ParseEditValue += new ConvertEditValueEventHandler(workShopComboBox_ParseEditValue);
            var workshopList = CommonAction.GetList<Workshop>();
            foreach (var cc in workshopList)
            {
                workShopComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.WorName?.Trim(),
                    Value = cc.Id,
                    Tag = cc
                });
            }
            var gridView = new SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车编号",FieldName="No",Visible=true,Name="ClothingCarNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡号",FieldName="CardNo",Visible=true,Name="CardNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车类别",FieldName="ClothingVehicleName",ColumnEdit=sewingMachineComboBox,Visible=true,Name="MachineType"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="品牌",FieldName="Brand",Visible=true,Name="Brand"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="型号",FieldName="model",Visible=true,Name="model"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="出厂编号",FieldName="SerialNumber",Visible=true,Name="SerialNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="购入日期",FieldName="PurchaseDate",Visible=true,Name="PurchaseDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="所属车间",FieldName="WorName",ColumnEdit=workShopComboBox,Visible=true,Name="Workshop"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="使用",FieldName="Used",Visible=true,Name="Used"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="多点登录",FieldName="MuiltLogin",Visible=true,Name="MuiltLogin"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报废日期",FieldName="ScrapDate",Visible=true,Name="ScrapDate"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="报废原因",FieldName="ScrapReason",Visible=true,Name="ScrapReason"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNo"},//衣车在线组别，登出刷空
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="StatingNo",Visible=true,Name="StatingNo"},//衣车在线站点，登出刷空
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="登录时间",FieldName="LoginDate",Visible=true,Name="LoginDate"},//登出刷空
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="时长(分)",FieldName="Times",Visible=true,Name="Times"}//登录后一直没登出的登录总时间
              //  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

            });
            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            base.BindGridHeader(dataGrid);
        }

        private void workShopComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void WorkShopComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                GridView gv = (this.SusGrid.DataGrid.MainView as GridView);
                var list = this.SusGrid.DataGrid.DataSource as List<ClothingVehicleModel>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                // var clothingVehicleType = BeanUitls<ClothingVehicleType, ClothingVehicleTypeModel>.Mapper((item.Tag as ClothingVehicleType));
                list[dataIndex].WorkShop = (item.Tag as Workshop).WorName;
                //list[dataIndex].ClothingVehicleName = (item.Tag as ClothingVehicleType).Name?.Trim();
                gv.SetFocusedRowCellValue("WorName", (item.Tag as Workshop)?.WorName?.Trim());
                // this.SusGrid.DataGrid.DataSource = list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
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
                var list = this.SusGrid.DataGrid.DataSource as List<ClothingVehicleModel>; ;
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
            var dt = dataGrid.DataSource as IList<Domain.ClothingVehicleModel>;
            if (null != dt)
            {
                dt.Add(new Domain.ClothingVehicleModel());
            }


            base.AddItem(dataGrid);
        }
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetClothingVehicleAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.ClothingVehicleModel>();

            //var list = dataGrid.DataSource as List<Domain.DepartmentModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.ClothingVehicle>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {

                        action.Update<Domain.ClothingVehicle>(t);

                    }
                    else
                    {

                        action.Save<Domain.ClothingVehicle>(t);
                        
                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.DepartmentModel).TransformTo<Domain.Department>();

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
                selectData.Deleted = 1;
                action.Update<Domain.Department>(selectData.TransformTo<Domain.Department>());
               
                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}

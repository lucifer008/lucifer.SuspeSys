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
using SuspeSys.Client.Modules.ProduceData;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.CustPurchaseOrder.Model;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules.SusDialog;
using SuspeSys.Client.Action.CustPurchaseOrder;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Utils.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace SuspeSys.Client.Modules.Customer
{
    public partial class CustomerOrderInput : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public CustomerOrderInput()
        {
            InitializeComponent();
            BindColorSizeGridHeader();
        }
        public CustomerOrderInput(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            //editCustPurchaseOrderModel = _model;
        }
        public CustomerOrderInput(XtraUserControl1 _ucMain, DaoModel.CustomerPurchaseOrder editModel) : this(_ucMain)
        {
            EditCustomerPurchaseOrder = editModel;
            BindEditCustomerPurchaseOrderInfo();
        }

        private void BindEditCustomerPurchaseOrderInfo()
        {
            if (null != EditCustomerPurchaseOrder)
            {
                btnEditCustomer.Tag = EditCustomerPurchaseOrder.Customer;
                btnEditCustomer.Text = EditCustomerPurchaseOrder.Customer?.CusName;

                txtDeliverAddress.Text = EditCustomerPurchaseOrder.DeliverAddress;
                if (null != EditCustomerPurchaseOrder.GeneratorDate)
                    txtGenaterOrderDate.DateTime = EditCustomerPurchaseOrder.GeneratorDate.Value;
                txtLinkMan.Text = EditCustomerPurchaseOrder.LinkMan?.Trim();
                txtMobile.Text = EditCustomerPurchaseOrder.Mobile?.Trim();
                txtLinkTel.Text = EditCustomerPurchaseOrder.Tel?.Trim();
                txtCustomerOrderNo.Text = EditCustomerPurchaseOrder.OrderNo?.Trim();
                txtPONo.Text = EditCustomerPurchaseOrder.PurchaseOrderNo?.Trim();
                if (null != EditCustomerPurchaseOrder.DeliveryDate)
                    txtDeliveryDate.DateTime = EditCustomerPurchaseOrder.DeliveryDate.Value;

                //客户订单明细
                // var queryAction = new ProcessOrderQueryAction();
                action.GetCustomerOrderItem(EditCustomerPurchaseOrder.Id);
                var pOrderColorList = action.Model.CustomerPurchaseOrderColorItemModelList;
                var pSizeList = new List<DaoModel.PSize>();
                var pColorList = new List<DaoModel.PoColor>();
                var pColorSizeDataList = new List<ColorAndSizeModel>();
                var tag = false;
                foreach (var item in pOrderColorList)
                {
                    var m = new ColorAndSizeModel();
                    m.ColorId = item.PoColor.Id;
                    m.SizeColumnCount = item.CustomerPurchaseOrderColorSizeItemList.Count;
                    m.ColorValue = item.PoColor.ColorValue;
                    m.ColorDescption = item.ColorDescription;
                    m.Total = Convert.ToInt32(item.Total);

                    pColorList.Add(item.PoColor);
                    var index = 1;
                    foreach (var size in item.CustomerPurchaseOrderColorSizeItemList)
                    {
                        if (!tag)
                        {
                            pSizeList.Add(size.PSize);
                        }
                        ReflectionUtils<ColorAndSizeModel>.SetPropertyValue(m, string.Format("SizeValue{0}", index), size.Total);
                        index++;
                    }
                    if (pSizeList.Count > 0)
                    {
                        tag = true;
                    }
                    pColorSizeDataList.Add(m);
                }
                CustomerOrderColorSizeList = pColorSizeDataList;

                BindProcessOrderColorSizeList(pColorList, pSizeList);
            }
        }

        void BindColorSizeGridHeader()
        {
            //var gridview=gridControl1.MainView as SusGridView;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="No",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="SizeValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码描述",FieldName="SizeDescption",Visible=true}//,
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码数量",FieldName="SizeDescption",Visible=true}

            });
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.CellValueChanged += Gv_CellValueChanged;
            gridView1.CustomRowCellEdit += Gv_CustomRowCellEdit;
        }
        RepositoryItem _disabledItem;
        IList<string> disableColumnList = new List<string>() { "No", "ColorValue", "ColorDescption", "SizeValue", "Total", "ColorValue", "ColorDescption" };
        private void Gv_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!disableColumnList.Contains(e.Column.FieldName))//需要设置的列名
                return;

            if (_disabledItem == null)
            {
                _disabledItem = (RepositoryItem)e.RepositoryItem.Clone();
                _disabledItem.ReadOnly = true;
                _disabledItem.Enabled = false;
            }
            e.RepositoryItem = _disabledItem;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.StartsWith("SizeValue"))
            {
                var gv = gridView1;// sender as S;
                var selectedRow = gv.GetRow(gv.FocusedRowHandle) as ColorAndSizeModel;
                if (null != selectedRow)
                {
                    var total = 0;

                    for (var size = 0; size < CustomerOrderSizeList.Count; size++)
                    {
                        var num = ReflectionUtils<ColorAndSizeModel>.GetPropertyValue(selectedRow, "SizeValue" + (size + 1));
                        if (!string.IsNullOrEmpty(num))
                        {
                            total += Convert.ToInt32(num);
                        }
                    }
                    selectedRow.Total = total;
                    gv.RefreshData();
                }
            }
        }
        private void btnEditCustomer_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var customerDialog = new CustomerSelectDialog();
                customerDialog.ShowDialog();
                var selCust = customerDialog.SelectedCustomer;
                if (null != selCust)
                {
                    btnEditCustomer.Tag = selCust;
                    btnEditCustomer.Text = selCust?.CusName;
                    txtDeliverAddress.Text = selCust.Address?.Trim();
                    txtLinkMan.Text = selCust.LinkMan?.Trim();
                    txtLinkTel.Text = selCust.Tel?.Trim();
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
        void RegisterTooButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        Save();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        // (this.Parent as XtraTabPage).cl
                        break;
                    case ButtonName.SaveAndClose:
                        Save();
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.SaveAndAdd:
                        Save();
                        var tab = new SusXtraTabPage();
                        tab.Text = string.Format("客户订单[{0}]", "");
                        tab.Name = string.Format("客户订单[{0}]", "");
                        XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new CustomerOrderInput(ucMain));
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
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        CustPurchaseOrderModel GetModel()
        {
            var model = new CustPurchaseOrderModel();
            var customer = btnEditCustomer.Tag as DaoModel.Customer;
            var cusPurchaseOrder = null == EditCustomerPurchaseOrder ? new DaoModel.CustomerPurchaseOrder() : EditCustomerPurchaseOrder;

            cusPurchaseOrder.Id = EditCustomerPurchaseOrder?.Id;
            cusPurchaseOrder.CusNo = customer?.CusNo;
            cusPurchaseOrder.CusName = customer?.CusName;
            cusPurchaseOrder.Customer = customer;
            cusPurchaseOrder.DeliverAddress = txtDeliverAddress.Text.Trim();
            cusPurchaseOrder.GeneratorDate = txtGenaterOrderDate.DateTime.FormatDate();
            cusPurchaseOrder.LinkMan = txtLinkMan.Text.Trim();
            cusPurchaseOrder.Mobile = txtMobile.Text.Trim();
            cusPurchaseOrder.Tel = txtLinkTel.Text.Trim();
            cusPurchaseOrder.OrderNo = txtCustomerOrderNo.Text.Trim();
            cusPurchaseOrder.PurchaseOrderNo = txtPONo.Text.Trim();
            cusPurchaseOrder.DeliveryDate = txtDeliveryDate.DateTime.FormatDate();
            model.CustomerPurchaseOrder = cusPurchaseOrder;

            //制单颜色
            var customerOrderColorList = CustomerOrderColorList ?? new List<DaoModel.PoColor>();
            var customerPurchaseOrderColorItemModelList = new List<DaoModel.CustomerPurchaseOrderColorItemModel>();
            foreach (var item in customerOrderColorList)
            {
                var customerPurchaseOrderColorItem = new DaoModel.CustomerPurchaseOrderColorItemModel() { PoColor = item, Color = item.ColorValue, ColorDescription = item.ColorDescption };
                customerPurchaseOrderColorItemModelList.Add(customerPurchaseOrderColorItem);

            }
            //颜色大小明细
            var inputColorAndSizeList = (gridControl1.DataSource as List<ColorAndSizeModel>) ?? new List<ColorAndSizeModel>();

            //var processOrderColorSizeItemList = new List<DaoModel.ProcessOrderColorSizeItem>();
            foreach (var cr in customerPurchaseOrderColorItemModelList)
            {
                cr.CustomerPurchaseOrderColorSizeItemList = new List<DaoModel.CustomerPurchaseOrderColorSizeItem>();
                foreach (var item in inputColorAndSizeList)
                {
                    if (cr.PoColor.Id.Equals(item.ColorId))
                    {
                        var colorTotal = 0;
                        for (var i = 1; i < item.SizeColumnCount + 1; i++)
                        {
                            var filedName = "SizeValue" + i;
                            var cSize = GridControlHelper.GetColumnTag(gridControl1, filedName) as DaoModel.PSize;
                            var it = new DaoModel.CustomerPurchaseOrderColorSizeItem()
                            {
                                PSize = cSize,
                                SizeDesption = cSize?.Size,
                                Total = ReflectionUtils<ColorAndSizeModel>.GetPropertyValue(item, filedName)
                            };
                            if (!string.IsNullOrEmpty(it.Total))
                            {
                                colorTotal += Convert.ToInt32(it.Total);
                            }
                            cr.CustomerPurchaseOrderColorSizeItemList.Add(it);
                        }
                        cr.Total = colorTotal.ToString();
                    }
                    //  processOrderColorSizeItemList.Add(new ProcessOrderColorSizeItem() { PSize=item,SizeDesption=item.SizeDesption});
                }
            }
            model.CustomerPurchaseOrderColorItemModelList = customerPurchaseOrderColorItemModelList;
            return model;
        }
        CustPurchaseOrderAction action = new CustPurchaseOrderAction();
        // CustPurchaseOrderModel editCustPurchaseOrderModel;

        public DaoModel.CustomerPurchaseOrder EditCustomerPurchaseOrder { get; internal set; }
        public List<DaoModel.PoColor> CustomerOrderColorList { get; internal set; }
        public List<DaoModel.PSize> CustomerOrderSizeList { get; internal set; }
        public List<ColorAndSizeModel> CustomerOrderColorSizeList { get; internal set; }
        // public List<DaoModel.PoColor> CustomerOrderColorList { get; internal set; }
        //public List<DaoModel.PSize> ProcessOrderSizeList { get; internal set; }

        void Save()
        {
            action.Model = GetModel();
            if (string.IsNullOrEmpty(action.Model.CustomerPurchaseOrder.CusName))
            {
                XtraMessageBox.Show("客户信息不能为空，请选择客户信息!", "提示");
                return;
            }
            if (null == EditCustomerPurchaseOrder)
            {
                action.AddCustPurchaseOrder();
                EditCustomerPurchaseOrder = action.Model.CustomerPurchaseOrder;
            }
            else
                action.UpdateCustPurchaseOrder();

            //XtraMessageBox.Show("保存成功!", "提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
        }
        private void btnSelecedColorAndSize_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var csDialog = new PColorAndSizeDialog(this);
                csDialog.GridControlColorSizes = gridControl1;
                csDialog.ShowDialog();
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void CustomerOrderInput_Load(object sender, EventArgs e)
        {
            RegisterTooButtonEvent();
        }
        private void BindProcessOrderColorSizeList(List<DaoModel.PoColor> selectedColorList, List<DaoModel.PSize> selectedSizeList)
        {
            var gv = gridView1;// gridControl1.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "No",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gridControl1.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gv.BestFitColumns();//按照列宽度自动适配
            gv.GridControl = gridControl1;
            gv.OptionsView.ShowFooter = true;
            gv.OptionsView.ShowGroupPanel = false;

            gv.ClearRows();

            gridControl1.DataSource = CustomerOrderColorSizeList;
            gridControl1.MainView.RefreshData();
            gv.IndicatorWidth = 40;
            //gv.OptionsView.ShowIndicator = true;
            //  gv.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            //保存选择颜色尺码明细
            //this.ProcessOrderColorSizeList = processOrderColorSizeList;
            this.CustomerOrderColorList = selectedColorList;
            this.CustomerOrderSizeList = selectedSizeList;


        }

        private void btnClearAllItem_Click(object sender, EventArgs e)
        {
            gridView1?.ClearRows();
            CustomerOrderColorList.RemoveAll(q => null != q.GetType());
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (CustomerOrderColorList.Count > 0)
                CustomerOrderColorList.RemoveAt(gridView1.FocusedRowHandle);
            gridView1.ClearSelectRow();
          
        }
    }
}

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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules.Customer;
using SuspeSys.Client.Modules.Ext;

using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Query;
using SuspeSys.Client.Action.CustPurchaseOrder;
using System.Collections;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 客户订单
    /// </summary>
    public partial class CustomerOrderIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public CustomerOrderIndex()
        {
            InitializeComponent();
        }
        public CustomerOrderIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下单日期",FieldName="GeneratorDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="交货日期",FieldName="DeliveryDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="发货地址",FieldName="DeliverAddress",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="联系人",FieldName="LinkMan",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="电话",FieldName="Tel",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="手机",FieldName="Mobile",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.MouseDown += GridView_MouseDown;
        }

        private void CustomerOrderIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            RegisterEvent();
            Query(1);
        }
        void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        //  Save();
                        //XtraMessageBox.Show("保存成功!", "提示");
                        XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Modify:
                        EditProcessOrder(this.susGrid1.DataGrid.MainView as GridView);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Delete:
                        var rs = MessageBox.Show("确认要删除该订单吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.Yes)
                        {
                            var gv = susGrid1.DataGrid.MainView as GridView;
                            if (null == gv) return;
                            var delCustomer = gv.GetRow(gv.FocusedRowHandle) as DaoModel.CustomerPurchaseOrder;
                            if (!string.IsNullOrEmpty(delCustomer.Id))
                            {
                                CommonAction.Instance.LogicDelete<DaoModel.CustomerPurchaseOrder>(delCustomer.Id);
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
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AddItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();

                tab.Text = string.Format("客户订单[{0}]", "");
                tab.Name = string.Format("客户订单[{0}]", "");
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new CustomerOrderInput(ucMain));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        CustPurchaseOrderAction customerOrderAction = new CustPurchaseOrderAction();
        private void Query(int currentPageIndex)
        {
            try
            {
                searchControl1.Cursor = Cursors.WaitCursor;
                int pageSize = susGrid1.PageSize;
                long totalCount = 0;
                var conModel = searchControl1.Text.Trim();
                IDictionary<string, string> ordercondition = null;
                var list = customerOrderAction.SearchCustomerOrder(currentPageIndex, pageSize, out totalCount, ordercondition, conModel);
                susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                searchControl1.Cursor = Cursors.Default;
            }
           
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                EditProcessOrder(sender as GridView);
            }
        }
        DaoModel.CustomerPurchaseOrder GetCustomerPurchaseOrder()
        {
            var gv = susGrid1.DataGrid.MainView as GridView;
            if (null == gv) return null;
            var editCustomerPurchaseOrder = gv.GetRow(gv.FocusedRowHandle) as DaoModel.CustomerPurchaseOrder;
            if (!string.IsNullOrEmpty(editCustomerPurchaseOrder.Id))
            {
                editCustomerPurchaseOrder = new CustPurchaseOrderAction().GetCustPurchaseOrder(editCustomerPurchaseOrder.Id);
            }
            return editCustomerPurchaseOrder;
        }
        void EditProcessOrder(GridView gv)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (null != gv)
                {
                    var editCustomerPurchaseOrder = GetCustomerPurchaseOrder(); //gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
                    if (null == editCustomerPurchaseOrder) return;
                    var customerOrderInput = new CustomerOrderInput(ucMain,editCustomerPurchaseOrder) { Dock = DockStyle.Fill };
                    //customerOrderInput.ucMain = ucMain;
                    customerOrderInput.EditCustomerPurchaseOrder = editCustomerPurchaseOrder;
                    var tab = new XtraTabPage();
                    tab.Name = customerOrderInput.Name;
                    tab.Text = string.Format("正在编辑客户订单[{0}]", editCustomerPurchaseOrder.CusName.Trim());
                    if (!ucMain.MainTabControl.TabPages.Contains(tab))
                    {
                        tab.Controls.Add(customerOrderInput);
                        ucMain.MainTabControl.TabPages.Add(tab);
                    }
                    ucMain.MainTabControl.SelectedTabPage = tab;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
    }
}

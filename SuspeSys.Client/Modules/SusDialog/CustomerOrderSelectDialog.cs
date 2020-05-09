using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Customer;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.Query;
using log4net;
using SuspeSys.Client.Action.CustPurchaseOrder;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class CustomerOrderSelectDialog : DevExpress.XtraEditors.XtraForm
    {
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(CustomerOrderSelectDialog));
        public CustomerOrderSelectDialog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selectedCustomerPurchaseOrderList = GetSelectedCustomerPurchaseOrderList();
            this.Close();
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下单日期",FieldName="GeneratorDate",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
           // DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = (sender as GridView).CalcHitInfo(e.X, e.Y);
            if (e.Button == MouseButtons.Left && e.Clicks== 2)
            {
                this.selectedCustomerPurchaseOrderList = GetSelectedCustomerPurchaseOrderList();
                this.Close();
            }
        }

        private IList<DaoModel.CustomerPurchaseOrder> selectedCustomerPurchaseOrderList;
        public IList<DaoModel.CustomerPurchaseOrder> SelectedCustomerPurchaseOrderList { get { return selectedCustomerPurchaseOrderList; } }
        IList<DaoModel.CustomerPurchaseOrder> GetSelectedCustomerPurchaseOrderList() {
            var gv = susGrid1.DataGrid.MainView as GridView;
            var selecRow=gv.GetRow(gv.FocusedRowHandle) as DaoModel.CustomerPurchaseOrder;
            int[] rownumber = gv.GetSelectedRows();//获取选中行号；
            selectedCustomerPurchaseOrderList = new List<DaoModel.CustomerPurchaseOrder>();
            foreach (var row in  rownumber) {
                selectedCustomerPurchaseOrderList.Add(gv.GetRow(row) as DaoModel.CustomerPurchaseOrder);
            }
            return selectedCustomerPurchaseOrderList;
            //return null==selecRow? new DaoModel.CustomerPurchaseOrder():selecRow;
        }
        //void BindData()
        //{
        //    try
        //    {
        //        var cmmonAction = new CommonQueryAction<DaoModel.CustomerPurchaseOrder>();
        //        susGrid1.DataGrid.DataSource= cmmonAction.GetAllList().Where<DaoModel.CustomerPurchaseOrder>(f=>f.Deleted==0);

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //}
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
                XtraMessageBox.Show(ex.Message, "错误");
            }
            finally
            {
                searchControl1.Cursor = Cursors.Default;
            }

        }
        private void CustomerSelectDialog_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            Query(1);
        }
        void RegisterEvent()
        {
           susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void searchControl1_Properties_Click(object sender, EventArgs e)
        {
            Query(1);
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
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
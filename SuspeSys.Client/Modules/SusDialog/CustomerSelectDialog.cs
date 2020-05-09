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
using log4net;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class CustomerSelectDialog : DevExpress.XtraEditors.XtraForm
    {
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(CustomerSelectDialog));
        public CustomerSelectDialog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selectedCustomer = GetSelectCustomer();
            this.Close();
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new GridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true}
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

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
           // DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = (sender as GridView).CalcHitInfo(e.X, e.Y);
            if (e.Button == MouseButtons.Left && e.Clicks== 2)
            {
                this.selectedCustomer = GetSelectCustomer();
                this.Close();
            }
        }

        private DaoModel.Customer selectedCustomer;
        public DaoModel.Customer SelectedCustomer { get { return selectedCustomer; } }
        DaoModel.Customer GetSelectCustomer() {
            var gv = gridControl1.MainView as GridView;
            var selecRow=gv.GetRow(gv.FocusedRowHandle) as DaoModel.Customer;
            return null==selecRow? new DaoModel.Customer():selecRow;
        }
        void BindData()
        {
            try
            {
                var customerAction = new CustomerAction();
                gridControl1.DataSource=customerAction.GetAllCustomerList().Where<DaoModel.Customer>(f=>f.Deleted==0);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error("绑定客户信息失败",ex);
            }
        }
        private void CustomerSelectDialog_Load(object sender, EventArgs e)
        {
            BindGridHeader(gridControl1);
            BindData();
        }
    }
}
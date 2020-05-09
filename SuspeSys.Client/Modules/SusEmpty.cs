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
using SuspeSys.Client.Action.ProcessOrder;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules
{
    public partial class SusEmpty : DevExpress.XtraEditors.XtraUserControl
    {
        public SusEmpty()
        {
            InitializeComponent();

        }
        public SusEmpty(int tag):this()
        {
            this.Tag = tag;
            InitData(tag);
        }
        private void InitData(int tag=0)
        {
            switch (Tag)
            {
                case 1:
                    BindProcessOrder();
                    break;
                case 2:
                    BindPO();
                    break;
                case 3:
                    BindSytle();
                    break;
                case 4:
                    BindEmloyee();
                    break;
            }
        }

        private void BindEmloyee(string employeeName=null)
        {
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Code",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工",FieldName="RealName",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;
            var list = CommonAction.GetList<Domain.Employee>();
            if (!string.IsNullOrEmpty(employeeName))
            {
                list = list.Where(f => f.RealName.Contains(employeeName)).ToList<Domain.Employee>();
            }
            gridControl1.DataSource = list;
        }

        private void BindSytle(string styleNo=null)
        {
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式",FieldName="StyleName",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;
            var list = CommonAction.GetList<Domain.Style>();
            if (!string.IsNullOrEmpty(styleNo))
            {
                list = list.Where(f => f.StyleNo.Contains(styleNo)).ToList<Domain.Style>();
            }
            gridControl1.DataSource = list;
        }

        private void BindPO(string po=null)
        {
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO",FieldName="PurchaseOrderNo",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;
            var list = CommonAction.GetList<Domain.CustomerPurchaseOrder>();
            if (!string.IsNullOrEmpty(po))
            {
                list = list.Where(f => f.PurchaseOrderNo.Contains(po)).ToList<Domain.CustomerPurchaseOrder>();
            }
            gridControl1.DataSource = list;
        }

        void BindProcessOrder(string processOrderNo = null)
        {
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;
            var list = CommonAction.GetList<Domain.ProcessOrder>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                list = list.Where(f => f.POrderNo.Contains(processOrderNo)).ToList<Domain.ProcessOrder>();
            }
            gridControl1.DataSource = list;
        }
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                (this.Parent as PopupContainerControl).OwnerEdit.ClosePopup();
                var gv = gridControl1.MainView as GridView;
                switch (Tag)
                {
                    case 1:
                        var selecRow = gv.GetRow(gv.FocusedRowHandle) as ProcessOrder;
                        pcEdit.Text = selecRow?.POrderNo?.Trim();
                        break;
                    case 2:
                        var poRow = gv.GetRow(gv.FocusedRowHandle) as CustomerPurchaseOrder;
                        pcEdit.Text = poRow?.PurchaseOrderNo?.Trim();
                        break;
                    case 3:
                        var styleRow = gv.GetRow(gv.FocusedRowHandle) as Domain.Style;
                        pcEdit.Text = styleRow?.StyleNo?.Trim();
                        break;
                    case 4:
                        var employeeRow = gv.GetRow(gv.FocusedRowHandle) as Domain.Employee;
                        pcEdit.Text = employeeRow?.RealName?.Trim();
                        break;
                }
            }
        }

        /// <summary>
        /// 1:制单号
        /// 2:PO
        /// 3:款式
        /// 4:员工
        /// 5:工段
        /// </summary>
        public int Tag { set; get; }
        public PopupContainerEdit pcEdit { set; get; }
        private void SusEmpty_Load(object sender, EventArgs e)
        {
            //InitData();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Query();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Query() {

            switch (Tag)
            {
                case 1:
                    var pOrderNo = txtKey.Text?.Trim();
                    BindProcessOrder(pOrderNo);
                    break;
                case 2:
                    var po = txtKey.Text?.Trim();
                    BindPO(po);
                    break;
                case 3:
                    var sNo = txtKey.Text?.Trim();
                    BindSytle(sNo);
                    break;
                case 4:
                    var emName = txtKey.Text?.Trim();
                    BindEmloyee(emName);
                    break;
            }
        }
        private void txtKey_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            Query();
        }
    }
}

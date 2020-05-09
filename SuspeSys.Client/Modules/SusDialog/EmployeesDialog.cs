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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Attendance;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class EmployeesDialog : DevExpress.XtraEditors.XtraForm
    {
        public EmployeesDialog()
        {
            InitializeComponent();
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }

        private void searchControl1_Properties_Click(object sender, EventArgs e)
        {
            Query(1);
        }
        private void BindGridHeader()
        {
            GridControl gc = susGrid1.DataGrid;
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工编号",FieldName="Code",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工姓名",FieldName="RealName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="性别",FieldName="Sex",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Email",FieldName="Email",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="手机号",FieldName="Phone",Visible=true}
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
            
        }

        private void EmployeesDialog_Load(object sender, EventArgs e)
        {
            BindGridHeader();
            Query(1);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey= searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = null;
            var list = AttendanceAction.Instance.SearchEmployee(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
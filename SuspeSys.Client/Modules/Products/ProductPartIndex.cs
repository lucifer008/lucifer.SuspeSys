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


namespace SuspeSys.Client.Modules.Products
{
    public partial class ProductPartIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public ProductPartIndex()
        {
            InitializeComponent();
        }

        private void susGrid1_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new DevExpress.XtraGrid.Views.Grid.GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="部位代码",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

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
    }
}

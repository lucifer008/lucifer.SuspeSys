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

namespace SuspeSys.Client.Modules.CuttingBed
{
    /// <summary>
    /// 货卡信息
    /// </summary>
    public partial class GoodCardInfoIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public GoodCardInfoIndex()
        {
            InitializeComponent();
        }
        void RegisterEvent() {
            this.Load += GoodCardInfoIndex_Load;
        }

        private void GoodCardInfoIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
        }

        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new DevExpress.XtraGrid.Views.Grid.GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="POrderNo",Visible=true},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="布匹号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="扎号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="货卡号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序段代码",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序段名称",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="件数",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="关联衣架数",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="关联衣架号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="装箱卡号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="装箱站点",FieldName="POrderNo",Visible=true},

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

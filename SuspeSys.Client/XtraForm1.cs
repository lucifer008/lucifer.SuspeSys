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

namespace SuspeSys.Client
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            BindGridView1Header();
        }
        void BindGridView1Header() {

            var gridViewOrder= gridControl1.MainView as GridView;
            gridViewOrder.Columns.Clear();
            gridViewOrder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编码",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="StyleCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="ProVersionNo",Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridViewOrder});
            gridViewOrder.GridControl = gridControl1;

            var gridViewColor = new GridView();
            gridViewColor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色编号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="StyleCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ProVersionNo",Visible=true}
            });
            gridViewColor.GridControl = gridControl1;

            var gridColorSize = new GridView();
            gridColorSize.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码编号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="StyleCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码描述",FieldName="ProVersionNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码数量",FieldName="ProVersionNo",Visible=true}
            });
            gridColorSize.GridControl = gridControl1;


            var gridOrder = new GridView();
            gridOrder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单号",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生产日期",FieldName="StyleCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下单日期",FieldName="ProVersionNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入日期",FieldName="ProVersionNo",Visible=true}
            });
            gridControl1.MainView = gridOrder;
            gridOrder.GridControl = gridControl1;
            var gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            gridLevelNode1.LevelTemplate = gridViewOrder;
            gridLevelNode1.RelationName = "Orders";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});

         
        }
    }
}
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
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.Products;
using System.Collections;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain.Common;

namespace SuspeSys.Client.Modules.TcpTest
{
    public partial class ProcessFlowHangingPiece : SusXtraUserControl
    {
        public ProcessFlowHangingPiece()
        {
            InitializeComponent();
        }
        public ProcessFlowHangingPiece(XtraUserControl1 _ucMain) : this()
        {
            this.ucMain = _ucMain;
            RegisterGridContextMenuStrip();
        }
        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var hangerPiece = new ToolStripMenuItem() { Text = "挂衣架" };

            hangerPiece.Click += HangerPiece_Click; ;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { hangerPiece });
        }

        private void HangerPiece_Click(object sender, EventArgs e)
        {
            var gv = (susGrid1.DataGrid.MainView as GridView);
            //var rows= gv.GetSelectedRows();
            var row = gv.GetRow(gv.FocusedRowHandle) as Domain.Products;
            //XtraMessageBox.Show(row?.ProductionNumber);
            try
            {
                SusTransitionManager.StartTransition(ucMain, "");
                var hangerToMainTrack = new HangerToMainTrack(row);
                hangerToMainTrack.ShowDialog();
            }
            finally {
                SusTransitionManager.EndTransition();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Cursor = Cursors.WaitCursor;
                Query(1);
            }
            finally {
                btnQuery.Cursor = Cursors.Default;
            }
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.Columns.Clear();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排产号",FieldName="ProductionNumber",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="上线日期",FieldName="ImplementDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="挂片站点",FieldName="HangingPieceSiteNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="ProcessOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="状态",FieldName="StatusText",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="PColor",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="PSize",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="产品部位",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺路线图",FieldName="LineName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单位",FieldName="Unit",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="任务数量",FieldName="TaskNum",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="在线数",FieldName="OnlineNum",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日裁片",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日产出",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="今日返工",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计裁片",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计产出",FieldName="POrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="累计返工",FieldName="POrderNo",Visible=true}

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

        private void ProcessFlowHangingPiece_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            Query(1);
        }
        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var pAction = new ProductsAction();
            var searchKey = "1";
            var ordercondition = new Dictionary<string,string>();
            ordercondition.Add("ProductionNumber", "ASC");
            var list = pAction.SearchProductsList(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, CurrentUser.Instance.CurrentSiteGroup?.GroupNo);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

        }
    }
}

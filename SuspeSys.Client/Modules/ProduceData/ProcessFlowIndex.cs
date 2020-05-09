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
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.ProcessOrder;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Modules.ProduceData
{
    /// <summary>
    /// 制单工序
    /// </summary>
    [Common.FormPermission(Domain.PermissionConstant.Billing_ProcessFlowIndex)]

    public partial class ProcessFlowIndex : SusXtraUserControl
    {
        //private XtraUserControl1 ucMain;

        public ProcessFlowIndex()
        {
            InitializeComponent();
            // Thread.Sleep(5000);
          
        }
        /// <summary>
        /// 处理连续为不同的版本生成制单工序
        /// </summary>
        /// <param name="ucMain"></param>
        public ProcessFlowIndex(XtraUserControl1 ucMain):this()
        {
            processFlowMain1.ProcessFlowSusToolBar = susToolBar1;
            this.ucMain = ucMain;
            processFlowMain1.ucMain = ucMain;
        }

        private DaoModel.ProcessOrder processOrder;
        /// <summary>
        /// 处理制单工序编辑
        /// </summary>
        /// <param name="_ucMain"></param>
        /// <param name="_processOrder"></param>
        public ProcessFlowIndex(XtraUserControl1 _ucMain, DaoModel.ProcessOrder _processOrder) : this(_ucMain) {
            if (null == _processOrder) return;
            processOrder = _processOrder;
            txtProcessOrderNo.Text = _processOrder.POrderNo;
            //txtProcessOrderNo.ReadOnly = true;
            processFlowMain1.BindProcessOrder(_processOrder);
            processFlowMain1.BindProcessFlowVerson(_processOrder.Id, false);
        }
        /// <summary>
        /// 绘制制单工序Grid，并绑定已有制单(处理来自不同的入口)
        /// </summary>
        /// <param name="gc"></param>
        //public SusToolBar ProcessFlowSusToolBar{ get { return susToolBar1; } }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.ShowIndicator = true;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            ProcessOrderQueryAction processOrderQueryAction = new ProcessOrderQueryAction();
            processOrderQueryAction.SearchProcessOrder();
            gc.DataSource = processOrderQueryAction.Model.ProcessOrderList;
            
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                popupContainerControl1.OwnerEdit.ClosePopup();
                var gv = gridControl1.MainView as GridView;
                var downHitInfo = (sender as SusGridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessOrder;
                if (null == selecRow) return;
               // var selecRow = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.ProcessOrder;
                txtProcessOrderNo.Text = selecRow.POrderNo?.Trim();
                txtProcessOrderNo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                processFlowMain1.BindProcessOrder(selecRow);

                susToolBar1.ShowCancelButton = true;
            }
        }

        private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void ProcessFlowIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(gridControl1);
            InitData();
        }

        private void InitData()
        {
            susToolBar1.ShowDeleteButton = false;
            susToolBar1.ShowModifyButton = false;
            susToolBar1.ShowCancelButton = false;
            susToolBar1.ShowAddButton = true;
            //susToolBar1.ShowCancelButton = true;
        }
    }
}

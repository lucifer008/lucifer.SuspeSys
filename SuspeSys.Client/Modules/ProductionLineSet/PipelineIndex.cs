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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.Common;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class PipelineIndex : SusXtraUserControl
    {
        public PipelineIndex()
        {
            if (!DesignMode)
            { 
                InitializeComponent();
            }
        }
        public PipelineIndex(XtraUserControl1 _ucMain) : this()
        {
            this.ucMain = _ucMain;
            this.Load += PipelineIndex_Load;
            pipelineMain1.ucMain = _ucMain;
        }

        private void PipelineIndex_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                BindPipelining();
            }

        }
        public PopupContainerEdit ComboPipelining { get { return comboPipelining; } }
        public SusToolBar SusToolBar { get { return susToolBar1; } }
        public PopupContainerEdit PPCPipelining
        {
            get
            {
                return comboPipelining;
            }
        }
       public void BindPipelining()
        {
            //流水线
            var poupcPipelining = new DevExpress.XtraEditors.PopupContainerControl();
            var gdPipelining = new GridControl() { Dock = DockStyle.Fill };
            comboPipelining.Properties.PopupControl = poupcPipelining;
            poupcPipelining.Controls.Add(gdPipelining);
            var gvPipelining = new GridView();
            gdPipelining.MainView = gvPipelining;
            gvPipelining.OptionsView.ShowGroupPanel = false;
            gvPipelining.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="流水线号",FieldName="PipeliNo",Visible=true,Name="PipeliNo"}
            });
            var pipeliningList = CommonAction.GetList<DaoModel.Pipelining>();
            gdPipelining.DataSource = pipeliningList;
            gvPipelining.Columns["PipeliNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐
            gvPipelining.MouseDown += GvPipelining_MouseDown;
        }

        private void GvPipelining_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                comboPipelining.Properties.PopupControl.OwnerEdit.ClosePopup();
                var csl = comboPipelining.Properties.PopupControl.Controls;
                if (csl.Count > 0)
                {
                    var gv = (csl[0] as GridControl)?.MainView as GridView;
                    var downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
                    var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.Pipelining;
                    if (null == selecRow) return;
                    comboPipelining.Text = selecRow.PipeliNo;
                    pipelineMain1.BindData(selecRow);
                }
            }
        }
    }
}

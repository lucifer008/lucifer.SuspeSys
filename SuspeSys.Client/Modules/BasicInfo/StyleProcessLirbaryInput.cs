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
using SuspeSys.Client.Action.Common;
using SuspeSys.Domain;
using SuspeSys.Domain.Cus;

namespace SuspeSys.Client.Modules.BasicInfo
{

    /// <summary>
    /// 款式工序库录入
    /// </summary>
    public partial class StyleProcessLirbaryInput : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public StyleProcessLirbaryInput()
        {
            InitializeComponent();
        }

        public StyleProcessLirbaryInput(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            styleProcessLirbaryInputMain1.SusToolBar = susToolBar1;
            styleProcessLirbaryInputMain1.ucMain = _ucMain;
            styleProcessLirbaryInputMain1.comStyleList = comStyleList;
            styleProcessLirbaryInputMain1.gcStyleSelectDialog = gcStyleSelectDialog;
            //editCustPurchaseOrderModel = _model;
        }

        public StyleProcessFlowSectionItemExtModel StyleProcessFlow { get; internal set; }

        void BindStyleSelectedDialogGrid()
        {

            gvSelectDialog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true,Name="StyleNo"},
  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式名称",FieldName="StyleName",Visible=true,Name="StyleName"}
            });
            var styleList = CommonAction.GetList<SuspeSys.Domain.Style>();
            gcStyleSelectDialog.DataSource = styleList;

            gvSelectDialog.MouseDown += GvSelectDialog_MouseDown;
        }

        private void GvSelectDialog_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                comStyleList.Properties.PopupControl.OwnerEdit.ClosePopup();
                var csl = comStyleList.Properties.PopupControl.Controls;
                if (csl.Count > 0)
                {

                    var downHitInfo = gvSelectDialog.CalcHitInfo(new Point(e.X, e.Y));
                    var selecRow = gvSelectDialog.GetRow(downHitInfo.RowHandle) as SuspeSys.Domain.Style;
                    if (null == selecRow) return;
                    comStyleList.Text = selecRow.StyleName?.Trim();
                    styleProcessLirbaryInputMain1.BindStyleProcessFlowData(selecRow);
                    
                }
            }
        }

        private void StyleProcessLirbaryInput_Load(object sender, EventArgs e)
        {
            BindStyleSelectedDialogGrid();
            if (null != StyleProcessFlow)
            {
                comStyleList.Text = StyleProcessFlow.StyleName?.Trim();
                if (null != StyleProcessFlow.Id)
                {
                    var spFlowItem = new CommonAction().Get<SuspeSys.Domain.StyleProcessFlowSectionItem>(StyleProcessFlow.Id);
                   
                }
                var style = new CommonAction().Get<SuspeSys.Domain.Style>(StyleProcessFlow.StyleId2);
                styleProcessLirbaryInputMain1.BindStyleProcessFlowData(style);
            }
        }
    }
}

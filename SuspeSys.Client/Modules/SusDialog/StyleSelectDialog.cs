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
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Utils.Reflection;
using DevExpress.XtraEditors.Repository;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class StyleSelectDialog : DevExpress.XtraEditors.XtraForm
    {
        public StyleSelectDialog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                btnOK.Cursor = Cursors.WaitCursor;
                var style = GetSelectStyle();
                if (null == style)
                {
                    //XtraMessageBox.Show("款式不能为空!");
                    XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("StyleNo")));
                    return;
                }
                this.selectedStyle = style;
                this.Close();
            }
            finally {
                btnOK.Cursor = Cursors.Default;
            }
        }
        private SuspeSys.Domain.Style selectedStyle;
        public SuspeSys.Domain.Style SelectedStyle { get { return selectedStyle; } }
        SuspeSys.Domain.Style GetSelectStyle()
        {
            var gv = gridControl1.MainView as GridView;
            var selecRow = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.StyleModel;
            if (!selecRow.IsSelected)
            {
                return null;
            }
            return BeanUitls<SuspeSys.Domain.Style, SuspeSys.Domain.StyleModel>.Mapper(selecRow);
        }
        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new GridView();
            gridView.OptionsSelection.MultiSelect = false;

            // gridView.DoubleClick += GridView_DoubleClick;
            var cdStyle = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit() { CheckStyle = CheckStyles.Radio };
            cdStyle.CheckedChanged += CdStyle_CheckedChanged;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="选择",FieldName="IsSelected",Visible=true,
                   ColumnEdit =cdStyle},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式描述",FieldName="StyleName",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsSelection.MultiSelect = false;
            
            gridView.Columns["IsSelected"].OptionsColumn.AllowEdit = true;
            gridView.Columns["StyleNo"].OptionsColumn.AllowEdit = false;
            gridView.Columns["StyleName"].OptionsColumn.AllowEdit = false;
        }

        private void CdStyle_CheckedChanged(object sender, EventArgs e)
        {
            var dt = gridControl1.MainView.DataSource as List<SuspeSys.Domain.StyleModel>;
            foreach (var s in dt) {
                s.IsSelected = false;
            }
            gridControl1.RefreshDataSource();
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        void BindData()
        {
            //var commonAction = new CommonAction();
            gridControl1.DataSource = CommonAction.GetList<SuspeSys.Domain.StyleModel>(true);
        }
        private void StyleSelectDialog_Load(object sender, EventArgs e)
        {
            BindData();
            BindGridHeader(gridControl1);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
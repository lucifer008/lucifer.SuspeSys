using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Permission;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.Permission
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public partial class UserIndex : Ext.CRUDControl
    {
        private UsersIndexAction _action = new UsersIndexAction();

        private UserIndex()
        {
            InitializeComponent();
        }

        public UserIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

            //base.SusToolBar.ShowModifyButton = true;
        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {
            base.InitToolBarButton(susToolBar);

            SusToolBar.ShowModifyButton = true;
        }

        /// <summary>
        /// 绑定Grid列头
        /// </summary>
        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();

            base.SearchControl.Properties.NullValuePrompt = "请输入用户名";

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="用户名",FieldName="UserName",Visible=true,Name="UserName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工姓名",FieldName="EmployeeName",Visible=true,Name="RealName"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="用户卡号",FieldName="CardNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true,Name="Deleted"},
            });
            gridView.Columns["UserName"].OptionsColumn.AllowFocus = false;
            gridView.Columns["EmployeeName"].OptionsColumn.AllowFocus = false;

            gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["UpdateDateTime"].OptionsColumn.AllowFocus = false;
            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            //gridView.MouseDown += GridView_MouseDown;

            base.BindGridHeader(dataGrid);  
        }


        protected override void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (null != base.SusGrid)
                    {

                        var selectRow = ((ColumnView)base.SusGrid.DataGrid.MainView).GetFocusedRow();

                        if (selectRow != null)
                        {
                            //Domain.UserOperateLogsModel model = selectRow as Domain.UserOperateLogsModel;

                            //UserOperatorLogDetail detail = new UserOperatorLogDetail(model.OperateLogDetailModels);
                            //detail.ShowDialog();
                            this.Modify(base.SusGrid.DataGrid, selectRow);
                            //base.AddMainTabControl(new UserAdd(ucMain, model.Id), "UserEdit", "修改用户用户");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    //XtraMessageBox.Show(ex.Message, "错误");
                    XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

            base.GridView_MouseDown(sender, e);
        }

        protected override void AddItem(GridControl dataGrid)
        {
            base.AddMainTabControl(new UserAdd(ucMain, string.Empty), "UserAdd", "添加用户");
            //XtraMessageBox.Show("添加");
            base.AddItem(dataGrid);
        }

        protected override void Modify(GridControl dataGrid, object selectRow)
        {
            base.Modify(dataGrid, selectRow);
            Domain.UsersModel model = selectRow as Domain.UsersModel;
            base.AddMainTabControl(new UserAdd(ucMain, model.Id), "UserEdit", "修改用户信息");

        }

        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }


    }
}

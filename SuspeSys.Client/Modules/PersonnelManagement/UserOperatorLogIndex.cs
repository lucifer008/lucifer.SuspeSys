using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.PersonnelManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class UserOperatorLogIndex : Ext.CRUDControl
    {

        UserOperatorLogIndexAction _action = new UserOperatorLogIndexAction();

        public UserOperatorLogIndex()
        {
            InitializeComponent();
        }

        public UserOperatorLogIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

           
        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {
            base.InitToolBarButton(susToolBar);
            SusToolBar.ShowAddButton = false;
            SusToolBar.ShowCancelButton = false;
            SusToolBar.ShowDeleteButton = false;
            SusToolBar.ShowModifyButton = false;
            SusToolBar.ShowSaveAndAddButton = false;
            SusToolBar.ShowSaveAndCloseButton = false;
            SusToolBar.ShowSaveButton = false;
            
        }

        private void UserOperatorLogIndex_Load(object sender, EventArgs e)
        {
            this.SearchControl.Properties.NullValuePrompt = "请输入模块名";
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();
            //base.SearchControl.Properties.NullValuePrompt = "请输入组名称";

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="页面名称",FieldName="OpFormName",Visible=true,Name="OpFormName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="页面编码",FieldName="OpFormCode",Visible=true,Name="OpFormCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="表名",FieldName="OpTableName",Visible=true,Name="OpTableName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="操作数据Id",FieldName="OpDataCode",Visible=true,Name="OpDataCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="操作类型",FieldName="OperatorTypeName",Visible=true,Name="OperatorTypeName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true},
            });
            //gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["OpFormName"].OptionsColumn.AllowFocus = false;
            gridView.Columns["OpFormCode"].OptionsColumn.AllowFocus = false;
            gridView.Columns["OpTableName"].OptionsColumn.AllowFocus = false;
            gridView.Columns["OpDataCode"].OptionsColumn.AllowFocus = false;
            gridView.Columns["OperatorTypeName"].OptionsColumn.AllowFocus = false;
            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            //this.BindSubGridView(dataGrid);
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
                            Domain.UserOperateLogsModel model = selectRow as Domain.UserOperateLogsModel;

                            UserOperatorLogDetail detail = new UserOperatorLogDetail(model.OperateLogDetailModels);
                            detail.ShowDialog();
                            //base.AddMainTabControl(new UserAdd(ucMain, model.Id), "UserEdit", "修改用户用户");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    XtraMessageBox.Show(ex.Message, "错误");
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        //private void BindSubGridView(GridControl dataGrid)
        //{
        //    var gridView2 = new Ext.SusGridView();
        //    gridView2.Name = "gridView2";

        //    gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="字段名",FieldName="FieldName",Visible=true},
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="修改之前",FieldName="BeforeChange",Visible=true},
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="修改之后",FieldName="Changed",Visible=true},

        //        //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true},
        //        //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true},
        //    });
        //    //gridView2.Columns["Deleted"].OptionsColumn.AllowFocus = false;
        //    //gridView2.Columns["Deleted"].
        //    gridView2.Columns["FieldName"].OptionsColumn.AllowFocus = false;
        //    gridView2.Columns["BeforeChange"].OptionsColumn.AllowFocus = false;
        //    gridView2.Columns["Changed"].OptionsColumn.AllowFocus = false;

        //    //gridView2.BestFitColumns();//按照列宽度自动适配
        //    gridView2.GridControl = dataGrid;
        //    gridView2.OptionsView.ShowFooter = false;
        //    gridView2.OptionsView.ShowGroupPanel = false;

        //    //dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
        //    //gridView2});


        //    DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();

        //    gridLevelNode3.LevelTemplate = gridView2;
        //    gridLevelNode3.RelationName = "OperateLogDetailModels";


        //    dataGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
        //    gridLevelNode3
        //    });
        //}

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

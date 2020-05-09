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
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using DevExpress.XtraTab;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class EmployeeIndex : Ext.CRUDControl
    {
        EmployeeIndexAction _action = new EmployeeIndexAction();
        public EmployeeIndex()
        {
            InitializeComponent();
        }

        public EmployeeIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
            base.SusToolBar.ShowSaveButton = false;
        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {
            base.InitToolBarButton(susToolBar);

            susToolBar.ShowModifyButton = true;
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请输入员工姓名";
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Code",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="姓名",FieldName="RealName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡号",FieldName="CardNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="是否有效",FieldName="Valid",Visible=true, MinWidth=100},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="部门",FieldName="DeptmentName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工种",FieldName="WorkTypeName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="入职时间",FieldName="StartingDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录用日期",FieldName="EmploymentDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="离职日期",FieldName="LeaveDate",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="手机号",FieldName="Phone",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="电话",FieldName="Mobile",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="银行卡号",FieldName="BankCardNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="地区",FieldName="AreaName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="住址",FieldName="Address",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=true},


                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="编码",FieldName= "Code",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="性别",FieldName="SexName",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Email",FieldName="Email",Visible=true},
                
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="部门",FieldName="DeptmentName",Visible=true},
                
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组织机构",FieldName="Organizations.ActionName",Visible=true},
                


                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true},
            });
            //gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            //gridView.Columns["UpdateDateTime"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;
            foreach (DevExpress.XtraGrid.Columns.GridColumn item in gridView.Columns)
            {
                item.OptionsColumn.AllowFocus = false;
            }

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

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

        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }

        protected override void AddItem(GridControl dataGrid)
        {
            //打开窗口

            var processFlowIndex = new EmployeeAdd(ucMain, string.Empty ,null) { Dock = DockStyle.Fill };
            var tab = new XtraTabPage();
            tab.Name = "EmployeeAdd";
            tab.Text = "员工添加";
            if (!ucMain.MainTabControl.TabPages.Contains(tab))
            {
                tab.Controls.Add(processFlowIndex);
                ucMain.MainTabControl.TabPages.Add(tab);
            }
            ucMain.MainTabControl.SelectedTabPage = tab;
        }

        protected override void Modify(GridControl dataGrid, object selectRow)
        {
            if (selectRow == null)
            {
                XtraMessageBox.Show("请选择要修改的数据!", "提示");
                return;
            }
            Domain.EmployeeModel model = (Domain.EmployeeModel)selectRow;
            var processFlowIndex = new EmployeeAdd(ucMain, string.Empty, model.Transform<Domain.EmployeeModel, Domain.Employee>()) { Dock = DockStyle.Fill };
            var tab = new XtraTabPage();
            tab.Name = "EmployeeEdit";
            tab.Text = "员工修改";
            if (!ucMain.MainTabControl.TabPages.Contains(tab))
            {
                tab.Controls.Add(processFlowIndex);
                ucMain.MainTabControl.TabPages.Add(tab);
            }
            ucMain.MainTabControl.SelectedTabPage = tab;
            //if (selectData)
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.EmployeeModel).TransformTo<Domain.Employee>();

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);//XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.Employee>(selectData.TransformTo<Domain.Employee>());
                action.DeleteLog<Domain.Employee>(selectData.Id, "员工管理", "DepartmentIndex", selectData.GetType().Name);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}

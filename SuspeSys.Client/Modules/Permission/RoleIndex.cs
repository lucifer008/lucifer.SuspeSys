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
using SuspeSys.Client.Action.Permission;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;

namespace SuspeSys.Client.Modules.Permission
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class RoleIndex : Ext.SusXtraUserControl
    {
        RoleAction roleAction = new RoleAction();

        
        

        public RoleIndex()
        {
            InitializeComponent();
            this.susToolBar1.ShowModifyButton = false;

            SimpleButton btnAddPremission = this.susToolBar1.CreateButton();
            btnAddPremission.Name = "btnAddPremission";
            btnAddPremission.Text = "设置页面权限";
            btnAddPremission.Click += BtnAddPremission_Click;
            btnAddPremission.Width = btnAddPremission.Width * 2;
            btnAddPremission.Image = global::SuspeSys.Client.Properties.Resources.icon_edit_16;
        }

        private void BtnAddPremission_Click(object sender, EventArgs e)
        {
            TsmItemSetRoleModule_Click(sender, e);
        }

        public RoleIndex(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }

        private void RoleIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            Query(1);
            RegisterGridContextMenuStrip();
            RegisterToolButtonEvent();
            RegisterGridEvent();
        }

        private void RegisterGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void RegisterToolButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        Save();
                        //XtraMessageBox.Show("保存成功!", "提示");
                        XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Delete:
                        Delete();
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
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

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
          var selectData =   ((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.Roles;

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);// XtraMessageBox.Show( "确定要删除吗？","提示信息",MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.Roles>(selectData);
                action.DeleteLog<Domain.Department>(selectData.Id, "角色管理", "RoleIndex", selectData.GetType().Name);
                Query(1);
            }
        }

        private void Save()
        {
            var action = new CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<Domain.Roles>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog(m.Id, m, "角色管理", "RoleIndex", m.GetType().Name);
                        action.Update<Domain.Roles>(m);
                    }
                    else
                    {
                        action.Save<Domain.Roles>(m);
                        action.InsertLog<Domain.Department>(m.Id, "角色管理", "RoleIndex", m.GetType().Name);

                    }
                }
            }
        }

        private void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var tsmItemCopyAndNew = new ToolStripMenuItem() { Text = "复制(新增)" };
            tsmItemCopyAndNew.Click += TsmItemCopyAndNew_Click;

            var tsmItemSetRoleModule = new ToolStripMenuItem() { Text = "设置权限" };
            tsmItemSetRoleModule.Click += TsmItemSetRoleModule_Click;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { tsmItemCopyAndNew,tsmItemSetRoleModule });
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmItemSetRoleModule_Click(object sender, EventArgs e)
        {
            var selectData = ((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.Roles;


            var processFlowIndex = new RoleModuleIndex(ucMain, selectData) { Dock = DockStyle.Fill };
            var tab = new XtraTabPage();
            tab.Name = "ModuleAdd";
            tab.Text = "角色权限设置";
            if (!ucMain.MainTabControl.TabPages.Contains(tab))
            {
                tab.Controls.Add(processFlowIndex);
                ucMain.MainTabControl.TabPages.Add(tab);
            }
            ucMain.MainTabControl.SelectedTabPage = tab;
        }

        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<Domain.Roles>;
            if (null != dt)
            {
                dt.Add(new Domain.Roles());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }

        private void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = roleAction.GetAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl1.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }

        private void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="角色名",FieldName="ActionName",Visible=true,Name="RoleName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="描述",FieldName="Description",Visible=true,Name="Description"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true,Name="Deleted"},
            });
            gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["UpdateDateTime"].OptionsColumn.AllowFocus = false;
            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //dataGrid.MainView.MouseDown += MainView_MouseDown;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (null != susGrid1)
                    {

                        var selectRow = ((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow();

                        if (selectRow != null)
                        {
                            //Domain.UserOperateLogsModel model = selectRow as Domain.UserOperateLogsModel;

                            //UserOperatorLogDetail detail = new UserOperatorLogDetail(model.OperateLogDetailModels);
                            //detail.ShowDialog();
                            //this.Modify(susGrid1.DataGrid, selectRow);
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
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
    }
}

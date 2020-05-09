using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using SuspeSys.Client.Action.Permission;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.Permission
{
    public partial class RoleModuleIndex  : Ext.SusXtraUserControl
    {
        private RoleModuleIndexAction _action = new RoleModuleIndexAction();

        #region 属性
        private Roles _role;
        private Roles Role
        {
            set
            {
                lbl_CreatedTime.Text = value?.InsertDateTime.ToString();
                lbl_Description.Text = value?.Description.ToString();
                lbl_ModifiedTime.Text = value?.UpdateDateTime.ToString();
                lbl_Name.Text = value?.ActionName.ToString();
                _role = value;
            }
            get { return _role; }
        }
        #endregion

        #region 构造函数
        private RoleModuleIndex()
        {
            InitializeComponent();
        }

        public RoleModuleIndex(XtraUserControl1 ucMain, Roles role) : base(ucMain)
        {
            InitializeComponent();
            InitEvent();
            InitFromData(role);
        }


        #endregion


        public void InitFromData(Roles role)
        {
            this.Role = role;
            this.gridControl1.DataSource = _action.GetEmployeeListByRoleId(role.Id);

            //初始化Tree
            this.treeList1.DataSource = _action.GetModulesByRoleId(role.Id);
            
            //this.treeList1.DataMember ="Id";
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            
            treeList1.OptionsBehavior.AllowIndeterminateCheckState = true; //设置节点是否有中间状态，即一部分子节点选中，一部分子节点没有选中 
            treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;

            this.treeList1.ExpandAll();

            this.SetSelectNode(this.treeList1.Nodes);

            this.gridView1.OptionsView.ShowGroupPanel = false;
        }

        /// <summary>
        /// 递归设置节点状态
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="selectNodes"></param>
         public List<TreeListNode> SetSelectNode(TreeListNodes nodes)
        {
            List<TreeListNode> selectNodes = new List<TreeListNode>();
            foreach (TreeListNode item in nodes)
            {


                item.Checked = (bool)item.GetValue("Checked");

                XtraTreeHelpler.SetCheckedChildNodes(item, item.CheckState);
                XtraTreeHelpler.SetCheckedParentNodes(item, item.CheckState);

                SetSelectNode(item);
            }

            return selectNodes;
        }

        /// <summary>
        /// 递归设置节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selectNodes"></param>
        /// <param name="checkState"></param>
        private void SetSelectNode(TreeListNode node)
        {
            if (!node.HasChildren)
            {
                return;
            }
            else
            {
                foreach (TreeListNode item in node.Nodes)
                {
                    item.Checked = (bool)item.GetValue("Checked");
                    XtraTreeHelpler.SetCheckedChildNodes(item, item.CheckState);
                    XtraTreeHelpler.SetCheckedParentNodes(item, item.CheckState);
                    SetSelectNode(item);
                }
            }
        }



        private void InitEvent()
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
                   
                    case ButtonName.Refresh:
                        InitFromData(this.Role);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.SaveAndClose:
                        {
                            Save();
                            ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        }
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
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

        private void Save()
        {
            //获取选中节点
            List<TreeListNode> selectNodes = XtraTreeHelpler.GetSelectNode(this.treeList1.Nodes);
            if (!selectNodes.Any())
            {
                XtraMessageBox.Show("请选菜单", "提示信息");
                return;
            }

            IList<RolesModules> rolesModel = new List<RolesModules>();
            foreach (TreeListNode item in selectNodes)
            {
                rolesModel.Add(new RolesModules()
                {
                     Modules = new Domain.Modules() { Id = item.GetValue("Id").ToString() },
                     Roles = new Roles() {  Id= this._role.Id}
                });
            }

            _action.SaveRolesModules(rolesModel, this.Role.Id);
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            XtraTreeHelpler.SetCheckedChildNodes(e.Node, e.Node.CheckState);
            XtraTreeHelpler.SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeList1_CustomDrawNodeCheckBox(object sender, DevExpress.XtraTreeList.CustomDrawNodeCheckBoxEventArgs e)
        {
            //e.Node.CheckState = CheckState.Checked;
            e.Handled = false;
        }


    }
}

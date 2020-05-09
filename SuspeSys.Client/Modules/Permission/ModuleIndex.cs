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
using SuspeSys.Client.Action.Permission;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SuspeSys.Client.Common;

namespace SuspeSys.Client.Modules.Permission
{
    [FormPermission("Permission.ModuleIndex")]
    public partial class ModuleIndex :  Ext.SusXtraUserControl
    {
        private ModuleAction _moduleAction = new ModuleAction();

        public ModuleIndex()
        {
            InitializeComponent();

            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = false;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowSaveAndAddButton = false; 
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = false;
            
        }
        public ModuleIndex(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void susToolBar1_Load(object sender, EventArgs e)
        {

        }

        private void ModuleIndex_Load(object sender, EventArgs e)
        {

            //RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            Query();
        }

        private void Query()
        {
            treeList1.DataSource = _moduleAction.GetAllModulesList().OrderBy( o => o.ModuleLevel * o.OrderField).ToList();
            this.treeList1.ExpandAll();
        }


        void RegisterTooButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;

            //this.treeList1.OptionsView.ShowCheckBoxes = true;
            //treeList1.OptionsBehavior.AllowIndeterminateCheckState = true; //设置节点是否有中间状态，即一部分子节点选中，一部分子节点没有选中 


        }





        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        //SaveCustomer();
                        //XtraMessageBox.Show("保存成功!", "提示");
                        break;
                    case ButtonName.Add:
                        //AddItem();
                        this.AddModule();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
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
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AddModule()
        {
            var processFlowIndex = new ModuleAdd(ucMain,string.Empty) { Dock = DockStyle.Fill };
            var tab = new XtraTabPage();
            tab.Name = "ModuleAdd";
            tab.Text = "资源添加";
            if (!ucMain.MainTabControl.TabPages.Contains(tab))
            {
                tab.Controls.Add(processFlowIndex);
                ucMain.MainTabControl.TabPages.Add(tab);
            }
            ucMain.MainTabControl.SelectedTabPage = tab;
        }

        private void susGrid1_Load(object sender, EventArgs e)
        {

        }
    }
}

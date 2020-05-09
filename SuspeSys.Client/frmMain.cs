
using DevExpress.DevAV;
using DevExpress.DevAV.ViewModels;
using DevExpress.Utils.Taskbar;
using DevExpress.Utils.Taskbar.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.DevAV.Modules;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.Utils;
using DUtilsAn = DevExpress.Utils.Animation;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Navigation;
using DUtils = DevExpress.Utils;
using DevExpress.XtraTab;

namespace SuspeSys.Client
{
    public partial class frmMain : RibbonForm, IMainModule, ISupportViewModel
    {
        public frmMain()
        {
            InitBefore();
            InitializeComponent();
            //InitRibbon();
            InitAfter();

        }
        void InitBefore()
        {
            TaskbarHelper.InitDemoJumpList(TaskbarAssistant.Default, this);
            AppHelper.MainForm = this;
            DevExpress.DevAV.StartUpProcess.OnStart("系统加载中，请稍后......");
        }
        void InitAfter()
        {
            //Thread.Sleep(3000);
            DevExpress.DevAV.StartUpProcess.OnRunning("Initializing...");
            Icon = AppHelper.AppIcon;

            
            //当前窗体必须实现IMainModule, ISupportViewModel接口
            mvvmContext1.ViewModelConstructorParameter = this;

            ViewModel.ModuleAdded += viewModel_ModuleAdded;
            ViewModel.ModuleRemoved += viewModel_ModuleRemoved;
            //底部选择项改变后触发
            ViewModel.SelectedModuleTypeChanged += viewModel_SelectedModuleTypeChanged;
            ViewModel.Print += viewModel_Print;
            ViewModel.ShowAllFolders += viewModel_ShowAllFolders;
            ViewModel.IsReadingModeChanged += viewModel_IsReadingModeChanged;

          

        }
        void InitSuspeMenu() {
           
        }
        //void InitRibbon()
        //{
        //    ribbonControl1.Pages.AddRange(new RibbonPage[] { new RibbonPage("吊挂管理"),
        //    new RibbonPage("生产资料1"),new RibbonPage("裁床管理"),new RibbonPage("组织架构"),
        //    new RibbonPage("考勤管理"),
        //    new RibbonPage("衣车管理"),
        //    new RibbonPage("系统管理") });
        //    //barButtonItem13


        //}
        private void viewModel_IsReadingModeChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void viewModel_ShowAllFolders(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void viewModel_Print(object sender, PrintEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void viewModel_SelectedModuleTypeChanged(object sender, EventArgs e)
        {
            if (ViewModel.SelectedNavPaneModuleType != ModuleType.Unknown)
                navBarControl1.ActiveGroup = GetNavBarGroup(ViewModel.SelectedNavPaneModuleType);
            UpdateCompactLayout(!ribbonControl1.Minimized);
            //throw new NotImplementedException();
        }
        void UpdateCompactLayout(bool compact)
        {
            if (ViewModel.SelectedNavPaneModuleType != ModuleType.Unknown)
                UpdateCompactLayout(GetNavPaneModule(ViewModel.SelectedNavPaneModuleType) as ISupportCompactLayout, compact);
            if (ViewModel.SelectedNavPaneHeaderModuleType != ModuleType.Unknown)
                UpdateCompactLayout(GetNavPaneModule(ViewModel.SelectedNavPaneHeaderModuleType) as ISupportCompactLayout, compact);
        }
        Control GetNavPaneModule(ModuleType navPaneModuleType)
        {
            Control moduleControl = ViewModel.GetModule(navPaneModuleType, ViewModel.SelectedModuleViewModel) as Control;
            ViewModelHelper.EnsureModuleViewModel(moduleControl, ViewModel);
            return moduleControl;
        }
        NavBarGroup GetNavBarGroup(ModuleType navPaneModuleType)
        {
            return navBarControl1.Groups
                .FirstOrDefault(g => object.Equals(g.Tag, navPaneModuleType));
        }
        void UpdateCompactLayout(ISupportCompactLayout module, bool compact)
        {
            if (module != null) module.Compact = compact;
        }
        private void viewModel_ModuleRemoved(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 绑定选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewModel_ModuleAdded(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            var moduleControl = sender as Control;
            moduleControl.Dock = DockStyle.Fill;
            moduleControl.Parent = modulesContainer;
            navBarControl1.SendToBack();
            Text = string.Format("{1} - {0}", ViewModel.GetModuleCaption(ViewModel.SelectedModuleType), "DevAV");
            IRibbonModule ribbonModuleControl = moduleControl as IRibbonModule;
            if (ribbonModuleControl != null)
            {
                Ribbon.MergeRibbon(ribbonModuleControl.Ribbon);
                Ribbon.StatusBar.MergeStatusBar(ribbonModuleControl.Ribbon.StatusBar);
            }
            else
            {
                Ribbon.UnMergeRibbon();
                Ribbon.StatusBar.UnMergeStatusBar();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //BindSkin();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

           
            ////设置默认选项卡
            //ViewModel.SelectedModuleType = ModuleType.Suspe; //ModuleType.Employees;

            var types = new ModuleType[] { ModuleType.ProduceData,ModuleType.Suspe};
            //RegisterNavigationMenuItems(barSubItem1, types);

            ////左边菜单栏及底部导航处理
            RegisterNavPanes(navBarControl1, types);
            ////InitRibbon();


        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            DevExpress.DevAV.StartUpProcess.OnComplete();
        }

        void RegisterNavPanes(NavBarControl navBar, ModuleType[] types)
        {
            for (int i = 0; i < types.Length; i++)
                RegisterNavPane(navBar, ViewModel.GetNavPaneModuleType(types[i]));
            officeNavigationBar1.RegisterItem += officeNavigationBar_RegisterItem;
            officeNavigationBar1.NavigationClient = navBar;
        }

        private void officeNavigationBar_RegisterItem(object sender, NavigationBarNavigationClientItemEventArgs e)
        {
            NavBarGroup navGroup = (NavBarGroup)e.NavigationItem;
            var type = ViewModel.GetMainModuleType((ModuleType)navGroup.Tag);
            e.Item.Tag = ViewModel.GetPeekModuleType(type);
            e.Item.Text = ViewModel.GetModuleCaption(type);
            e.Item.Name = "navItem" + ViewModel.GetModuleName(type);
            if (type == ModuleType.Orders || type == ModuleType.Quotes)
                e.Item.ShowPeekFormOnItemHover = DUtils.DefaultBoolean.False;
            e.Item.BindCommand((t) => ViewModel.SelectModule(t), ViewModel, () => type);
        }

        void RegisterNavPane(NavBarControl navBar, ModuleType type)
        {
            NavBarGroup navGroup = new NavBarGroup();
            navGroup.Tag = type;
            navGroup.Name = "navGroup" + ViewModel.GetModuleName(type);
            navGroup.Caption = ViewModel.GetModuleCaption(type);
            navGroup.LargeImage = (System.Drawing.Image)ViewModel.GetModuleImage(type);
            navGroup.GroupStyle = NavBarGroupStyle.ControlContainer;
            navGroup.ControlContainer = new NavBarGroupControlContainer();
            navBar.Controls.Add(navGroup.ControlContainer);
            navBar.Groups.Add(navGroup);
        }

        /// <summary>
        /// 注册导航相关：并处理导航链接渲染方式
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="types"></param>
        void RegisterNavigationMenuItems(BarLinkContainerItem menuItem, ModuleType[] types)
        {
            for (int i = 0; i < types.Length; i++)
                RegisterNavigationMenuItem(menuItem, types[i]);
        }
        void RegisterNavigationMenuItem(BarLinkContainerItem menuItem, ModuleType type)
        {
            BarCheckItem biModule = new BarCheckItem();
            biModule.Caption = ViewModel.GetModuleCaption(type);
            biModule.Name = "biModule" + ViewModel.GetModuleName(type);
            biModule.Glyph = (System.Drawing.Image)ViewModel.GetModuleImage(type);
            biModule.GroupIndex = 1;
            biModule.BindCommand((t) => ViewModel.SelectModule(t), ViewModel, () => type);
            menuItem.AddItem(biModule);
        }
        public MainViewModel ViewModel
        {
            get { return mvvmContext1.GetViewModel<MainViewModel>(); }
        }

        object ISupportViewModel.ViewModel
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //private void BindSkin()
        //{
        //    foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
        //    {
        //        comboBoxEdit1.Properties.Items.Add(skin.SkinName);
        //    }
        //}

        //private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    this.LookAndFeel.SkinName=comboBoxEdit1.EditValue.ToString();
        //}
        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //this.LookAndFeel.SkinName = comboBoxEdit1.EditValue.ToString();
        }
      
        bool IPeekModulesHost.IsDocked(ModuleType type)
        {
            throw new NotImplementedException();
        }

        void IPeekModulesHost.DockModule(ModuleType moduleType)
        {
            throw new NotImplementedException();
        }

        void IPeekModulesHost.UndockModule(ModuleType moduleType)
        {
            throw new NotImplementedException();
        }

        void IPeekModulesHost.ShowPeek(ModuleType moduleType)
        {
            throw new NotImplementedException();
        }

        void ISupportModuleLayout.SaveLayoutToStream(MemoryStream ms)
        {
            dockManager1.SaveLayoutToStream(ms);
            //throw new NotImplementedException();
        }

        void ISupportModuleLayout.RestoreLayoutFromStream(MemoryStream ms)
        {
            //throw new NotImplementedException();
            dockManager1.RestoreLayoutFromStream(ms);
        }

        /// <summary>
        /// 顶部导航处理
        /// </summary>
        /// <param name="forward"></param>
        /// <param name="waitParameter"></param>
        void ISupportTransitions.StartTransition(bool forward, object waitParameter)
        {
            //throw new NotImplementedException();
            
            var transition = transitionManager1.Transitions[modulesContainer];
            var animator = transition.TransitionType as DevExpress.Utils.Animation.SlideFadeTransition;
            animator.Parameters.EffectOptions = forward ? DUtilsAn.PushEffectOptions.FromRight : DUtilsAn.PushEffectOptions.FromLeft;
            if (waitParameter == null)
                transition.ShowWaitingIndicator = DefaultBoolean.False;
            else
            {
                transition.ShowWaitingIndicator = DefaultBoolean.True;
                transition.WaitingIndicatorProperties.Caption = DevExpress.XtraEditors.EnumDisplayTextHelper.GetDisplayText(waitParameter);
                transition.WaitingIndicatorProperties.Description = "Loading...";
                transition.WaitingIndicatorProperties.ContentMinSize = new System.Drawing.Size(160, 0);
            }
            transitionManager1.StartTransition(modulesContainer);

        }

        void ISupportTransitions.EndTransition()
        {
            transitionManager1.EndTransition();
        }

        void ISupportViewModel.ParentViewModelAttached()
        {
            throw new NotImplementedException();
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//得到关闭的选项卡的text  
            foreach (XtraTabPage page in xtraTabControl1.TabPages)//遍历得到和关闭的选项卡一样的Text  
            {
                if (page.Text == name)
                {
                    xtraTabControl1.TabPages.Remove(page);
                    page.Dispose();
                    return;
                }
            }
        }
    }
}

using System;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SuspeSys.Client.Modules;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Navigation;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using SuspeSys.Domain;

namespace SuspeSys.Client
{

    /// <summary>
    /// 主内容面板
    /// </summary>
    public partial class XtraUserControl1 : DevExpress.XtraEditors.XtraUserControl
    {

        #region 构造器
        public XtraUserControl1()
        {
            InitializeComponent();

            //生产制单
            this.accordionControlElement2.Tag = new TagExt(PermissionConstant.Billing_ProcessOrderIndex);
            //制单工序
            this.acElementProcessFlow.Tag = new TagExt(PermissionConstant.Billing_ProcessFlowIndex);
            //工艺路线图
            this.accElementFlowChart.Tag = new TagExt(PermissionConstant.Billing_ProcessFlowChartIndex);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 左边树视图
        /// </summary>
        public DockPanel LeftDockPanel
        {
            get { return dockPanel1; }
        }
        public AccordionControl LeftAccordionControl
        {
            get { return accordionControl1; }
        }
        public XtraTabControl MainTabControl
        {
            get { return xtraTabControl1; }
        }
        public RibbonControl MainRibbonControl
        {
            set;get;
        }
        #endregion

        #region 事件处理程序
        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        //生产制单
        private void accordionControlElement2_Click(object sender, EventArgs e)
        {


            //var forward = false;
            var waitParameter = accordionControlElement2.Text;
            //var transition = transitionManager1.Transitions[xtraTabControl1];
            ////var animator = transition.TransitionType as DevExpress.Utils.Animation.SlideFadeTransition;
            ////animator.Parameters.EffectOptions = forward ? DUtilsAn.PushEffectOptions.FromRight : DUtilsAn.PushEffectOptions.FromLeft;
            //if (waitParameter == null)
            //    transition.ShowWaitingIndicator = DefaultBoolean.False;
            //else
            //{
            //    transition.ShowWaitingIndicator = DefaultBoolean.True;
            //    transition.WaitingIndicatorProperties.Caption = DevExpress.XtraEditors.EnumDisplayTextHelper.GetDisplayText(waitParameter);
            //    transition.WaitingIndicatorProperties.Description = "Loading...";
            //    transition.WaitingIndicatorProperties.ContentMinSize = new System.Drawing.Size(160, 0);
            //}
            //transitionManager1.StartTransition(xtraTabControl1);

            SusTransitionManager.StartTransition(xtraTabControl1, waitParameter);
            var porderIndex = new ProcessOrderIndex(this) { Dock = DockStyle.Fill };
            var tab = new SusXtraTabPage();
            tab.Text = accordionControlElement2.Text;
            tab.Name = porderIndex.Name;
            if (!xtraTabControl1.TabPages.Contains(tab))
            {
                tab.Controls.Add(porderIndex);
                xtraTabControl1.TabPages.Add(tab);
                xtraTabControl1.SelectedTabPage = tab;
            }
            else
            {
                var index = xtraTabControl1.TabPages.IndexOf(tab);
                xtraTabControl1.SelectedTabPageIndex = index;
            }
            SusTransitionManager.EndTransition();


        }

        //private void accordionControlElement3_Click(object sender, EventArgs e)
        //{
        //    XtraTabPage tab = new XtraTabPage();
        //    tab.Text = accordionControlElement3.Text + "好";
        //    tab.Controls.Add(new ProcessOrders() { Dock = DockStyle.Fill });

        //    xtraTabControl1.TabPages.Add(tab);
        //    xtraTabControl1.SelectedTabPage = tab;
        //}

        #endregion

        #region 初始化属性
        void InitData()
        {
            var accSearchControl = accordionControl1.GetSearchFilterControl() as AccordionSearchControl;
            accSearchControl.NullValuePrompt = "输入名称搜索";
            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {

        }

        private void XtraUserControl1_Load(object sender, EventArgs e)
        {
            InitData();
        }
        #endregion

        //制单工序
        private void acElementProcessFlow_Click(object sender, EventArgs e)
        {
            var waitParameter = acElementProcessFlow.Text;
            //var transition = transitionManager1.Transitions[xtraTabControl1];
            ////var animator = transition.TransitionType as DevExpress.Utils.Animation.SlideFadeTransition;
            ////animator.Parameters.EffectOptions = forward ? DUtilsAn.PushEffectOptions.FromRight : DUtilsAn.PushEffectOptions.FromLeft;
            //if (waitParameter == null)
            //    transition.ShowWaitingIndicator = DefaultBoolean.False;
            //else
            //{
            //    transition.ShowWaitingIndicator = DefaultBoolean.True;
            //    transition.WaitingIndicatorProperties.Caption = DevExpress.XtraEditors.EnumDisplayTextHelper.GetDisplayText(waitParameter);
            //    transition.WaitingIndicatorProperties.Description = "Loading...";
            //    transition.WaitingIndicatorProperties.ContentMinSize = new System.Drawing.Size(160, 0);
            //}
            //transitionManager1.StartTransition(xtraTabControl1);

            SusTransitionManager.StartTransition(xtraTabControl1, waitParameter);
            var processFlowIndex = new ProcessFlowIndex(this) { Dock = DockStyle.Fill };
            var tab = new SusXtraTabPage();
            tab.Text = acElementProcessFlow.Text;
            tab.Name = processFlowIndex.Name;
            if (!xtraTabControl1.TabPages.Contains(tab))
            {
                tab.Controls.Add(processFlowIndex);
                xtraTabControl1.TabPages.Add(tab);
                xtraTabControl1.SelectedTabPage = tab;
            }
            else
            {
                var index = xtraTabControl1.TabPages.IndexOf(tab);
                xtraTabControl1.SelectedTabPageIndex = index;
            }
            SusTransitionManager.EndTransition();

            //transitionManager1.EndTransition();

        }
        //工艺路线图(生产资料)
        private void accElementFlowChart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();
                tab.Text = "工艺路线图";
                tab.Name = string.Format("{0}", "工艺路线图");
                XtraTabPageHelper.AddTabPage(this.MainTabControl, tab, new ProcessFlowChartIndex(this));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "错误");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        public void MaxOrMin()
        {
            dockPanel1.Visible = !dockPanel1.Visible;
            MainRibbonControl.Minimized = !MainRibbonControl.Minimized;
        }
    }
}

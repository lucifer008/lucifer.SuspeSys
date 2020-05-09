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
using SuspeSys.Client.Action;

using log4net;

namespace SuspeSys.Client.Modules.Ext
{
    public partial class SusXtraUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        protected ILog log = LogManager.GetLogger(typeof(SusXtraUserControl));

        public delegate void DoCloseDelegate(object sender, EventArgs e);

        public event DoCloseDelegate DoClose;
        public SusXtraUserControl()
        {
            //this.TopLevel = false;

            InitializeComponent();
        }

        public SusXtraUserControl(XtraUserControl1 _ucMain)
        {
            this.ucMain = _ucMain;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public XtraUserControl1 ucMain { set; get; }
        protected bool FixFlag { set; get; }
        /// <summary>
        /// 添加Table
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        protected virtual void AddMainTabControl(Control control, string name, string text)
        {
            var tab = new DevExpress.XtraTab.XtraTabPage();
            tab.Name = name;
            tab.Text = text;
            if (!ucMain.MainTabControl.TabPages.Contains(tab))
            {
                control.Dock = DockStyle.Fill;
                tab.Controls.Add(control);
                ucMain.MainTabControl.TabPages.Add(tab);
            }
            ucMain.MainTabControl.SelectedTabPage = tab;
        }
        public virtual void RefRefreshData() {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageAction.Instance.ChangeLanguage(this.Controls);
           // this.Refresh();
        }

        /// <summary>
        /// 关闭页面，添加完成刷新父页面， 父页面需要实现事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Closing(object sender, EventArgs e)
        {
            if (DoClose != null)
                DoClose(sender, e);
        }
    }
}

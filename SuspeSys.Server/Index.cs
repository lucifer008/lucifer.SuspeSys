using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SuspeSys.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;
using SuspeSys.Server.Module.WinService;
using DevExpress.XtraTab;
using log4net;
using System.Threading;
using SuspeSys.Remoting;
using System.Drawing;
using DevExpress.XtraEditors;
using System.Diagnostics;
using SuspeSys.CustomerControls.Sqlite.Repository;

namespace SuspeSys.Server
{
    public partial class Index : DevExpress.XtraEditors.XtraForm
    {
        private ILog log = LogManager.GetLogger("LogLogger");

        public Index()
        {
            //授权验证
            bool auth = Authorization.AuthorizationDetection.Instance.Authorization();
            //if (!auth)
            //{
            //    var auther = new AuthorizationIndex();
            //    auther.ShowDialog();
            //    return;
            //}
            //throw new Exception("授权失败，请联系服务商！");

            InitializeComponent();
            loadDBSet();
        }

        private void barSubItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ServiceMenuInit();
        }
        private void barSubItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProtocalConverterMenuInit();
        }
        void ServiceMenuInit()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                this.accordionControl1.Elements.Clear();
                this.accordionControl1.Elements.AddRange(acElementArr);
                this.accordionControl1.Refresh();
            }
            catch (Exception ex)
            {
                Log4netHelper.Instance.GetLog<Index>().Error("ServiceMenuInit", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        void ProtocalConverterMenuInit()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                this.accordionControl1.Elements.Clear();
                // this.accordionControl1.Refresh();
                var protocalConverterMsg = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = "转换器管理",
                    Expanded = true
                };

                accordionControl1.Elements.Add(protocalConverterMsg);
                accordionControl1.Refresh();
            }
            catch (Exception ex)
            {
                Log4netHelper.Instance.GetLog<Index>().Error("ProtocalConverter", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        AccordionControlElement[] acElementArr = null;
        private void Index_Load(object sender, EventArgs e)
        {

            var acc = accordionControl1;
            acElementArr = new AccordionControlElement[acc.Elements.Count];
            acc.Elements.CopyTo(acElementArr, 0);
            xtraTabPage1.ShowCloseButton = DefaultBoolean.False;
            //this.accordionControl1.Elements.Clear();

            //new Thread(Init).Start();
            //new Thread(SetDefault).Start();
        }
        void SetDefault()
        {

            SusBootstrap.Instance.Start(lcTcpMessage, false);
            //SusRedisClient.Instance.Init();
            ////注册remoting
            //TcpBootstrap.Instance.RegsiterRemotingByCode(lcTcpMessage);
            //CANTcp.Instance.ConnectCAN(lcTcpMessage);
            // var susRemotingMessage = string.Format("悬挂业务端口【{0}】已开启！", TcpBootstrap.basePort);
            //this.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        }
        void Init()
        {
            var beginMessage = string.Format("端口开启中...请稍后!");
            this.Invoke(new EventHandler(this.AddMessage), beginMessage, null);

        }
        static int index = 1;
        void AddMessage(object sender, EventArgs e)
        {
            var data = string.Format("{0}--->{1}", index, sender.ToString());
            lcTcpMessage.Items.Add(data);
            index++;
        }
        //安装服务
        private void accoControlElemInstallService_Click(object sender, EventArgs e)
        {
            var xucServiceIndex = new xucServiceIndex() { Dock = DockStyle.Fill, lcTcpMessage = this.lcTcpMessage };
            var tp = new XtraTabPage() { Text = "服务管理" };
            tp.Controls.Add(xucServiceIndex);
            var tpp = xtraTabControl1.TabPages.Where(f => f.Text.Equals(tp.Text)).SingleOrDefault<XtraTabPage>();
            if (tpp != null)
            {
                xtraTabControl1.SelectedTabPage = tpp;
                return;
            }
            xtraTabControl1.TabPages.Add(tp);
            xtraTabControl1.SelectedTabPage = tp;
        }

        //悬挂业务端口
        private void barSubItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Thread(SetDefault).Start();
        }

        private void bbItemCAN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CANTcp.Instance.ConnectCAN(lcTcpMessage);
        }

        private void 清空消息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lcTcpMessage.Items.Clear();
        }

        private void Index_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭所有子线程
            Application.Exit();
        }

        private void lcTcpMessage_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            if (e.Index % 2 == 0)
            {
                e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.Red),
           e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
            //e.Appearance.BackColor = Color.Red;

        }
        void loadDBSet()
        {
            var defaultDBConfig = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
            if (string.IsNullOrEmpty(defaultDBConfig))
            {
                SetDBConfig();
            }
        }
        private void barbuttonItemDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetDBConfig();
        }

        private void SetDBConfig()
        {
            //MessageBox.Show("添加");
            //ChangeDatabase change = new ChangeDatabase();
            SuspeSys.CustomerControls.Common.ChangeDatabase changeDataBase = new SuspeSys.CustomerControls.Common.ChangeDatabase();
            changeDataBase.ResetAdminPasswordEvent += ChangeDataBase_ResetAdminPasswordEvent;
            DialogResult result = changeDataBase.ShowDialog();
            if (result == DialogResult.OK)
            {
                var pTitle = "修改成功!软件会自动重启!";//LanguageAction.Instance.BindLanguageTxt("prompUpdateSuc", "修改成功!软件会自动重启!");
                XtraMessageBox.Show(pTitle);
                //if (!tagConnectionStr.Equals(DatabaseConnectionRepository.Instance.GetDeafultConnectionStr()))
                //{
                //    var defaultDB = DatabaseConnectionRepository.Instance.GetDeafult();
                //    if (defaultDB != null)
                //    {
                //        tagConnectionStr = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
                //        SuspeSys.Service.Impl.SuspeApplication.ResetConfig(defaultDB);
                //    }
                //}
                //GetDefaultDataBase();
                var defaultDB = DatabaseConnectionRepository.Instance.GetDeafult();
                if (defaultDB != null)
                {
                    //   tagConnectionStr = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
                    SuspeApplicationGlob.Instance.ResetDBConfig(defaultDB);
                }
                RestartMe();
            }
        }

        private void ChangeDataBase_ResetAdminPasswordEvent(object sender, EventArgs e)
        {
            //string password = "123";
            //try
            //{
            //    _action.ResetPassword("admin", password);
            //    var prompTitle = LanguageAction.Instance.BindLanguageTxt("prompResetPassword", "密码已经重置为");
            //    var promptTips = LanguageAction.Instance.BindLanguageTxt("promptTips");

            //    XtraMessageBox.Show(prompTitle + $"{password}", promptTips);
            //}
            //catch (Exception ex)
            //{

            //    XtraMessageBox.Show(ex.Message);
            //}
        }
        private static void RestartMe()
        {
            Application.ExitThread();
            Application.Exit();
            Application.Restart();
            Process.GetCurrentProcess().Kill();
        }
    }
}
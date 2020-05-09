using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Remoting;
using System.Threading;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SuspeSys.Server.Module.WinService
{
    public partial class xucServiceIndex : DevExpress.XtraEditors.XtraUserControl
    {
        public xucServiceIndex()
        {
            InitializeComponent();
            var windowServicePath = "ServicePath/SuspeSys.WinService.exe";
            var fileInfo = new FileInfo(windowServicePath);
            txtServcePath.Text = fileInfo.FullName;
        }
        public DevExpress.XtraEditors.ListBoxControl lcTcpMessage;
        private void txtServcePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (DialogResult.OK == dr)
            {
                var fileName = openFileDialog1.FileName;
                txtServcePath.Text = fileName;
            }
        }

        private void btnInstallService_Click(object sender, EventArgs e)
        {
            try
            {
                btnInstallService.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtServcePath.Text.Trim()))
                {
                    XtraMessageBox.Show("请选择服务文件(exe)！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnStartService.Cursor = Cursors.WaitCursor;
                _frmServiceStatus.SetServiceFileName(txtServcePath.Text.Trim());
                _frmServiceStatus.ExecuteRes = "";
                _frmServiceStatus.SetExecuteMethod("install");
                if (_frmServiceStatus.ShowDialog(this) == DialogResult.OK)
                {
                    if (_frmServiceStatus.ExecuteRes == "install_success")
                    {
                        XtraMessageBox.Show("服务安装成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnInstallService.Enabled = false;
                        btnUninstallService.Enabled = !btnInstallService.Enabled;
                    }
                    else if (_frmServiceStatus.ExecuteRes == "install_error")
                    {
                        XtraMessageBox.Show("服务安装失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnInstallService.Enabled = true;
                        btnInstallService.Enabled = !btnInstallService.Enabled;
                    }
                }
            }
            finally
            {
                btnStartService.Cursor = Cursors.Default;
            }
        }

        private void btnUninstallService_Click(object sender, EventArgs e)
        {
            try
            {
                btnUninstallService.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtServcePath.Text.Trim()))
                {
                    XtraMessageBox.Show("请选择服务文件(exe)！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _frmServiceStatus.SetServiceFileName(txtServcePath.Text.Trim());
                _frmServiceStatus.ExecuteRes = "";
                _frmServiceStatus.SetExecuteMethod("uninstall");
                if (_frmServiceStatus.ShowDialog(this) == DialogResult.OK)
                {
                    if (_frmServiceStatus.ExecuteRes == "uninstall_success")
                    {
                        XtraMessageBox.Show("服务卸载成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnInstallService.Enabled = true;
                        btnUninstallService.Enabled = !btnInstallService.Enabled;
                    }
                    else if (_frmServiceStatus.ExecuteRes == "uninstall_error")
                    {
                        XtraMessageBox.Show("服务卸载失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnInstallService.Enabled = false;
                        btnUninstallService.Enabled = !btnInstallService.Enabled;
                    }

                }
            }
            finally
            {
                btnUninstallService.Cursor = Cursors.Default;
            }
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            try
            {
                btnStartService.Cursor = Cursors.WaitCursor;
                //if (string.IsNullOrEmpty(txtServiceExePath.Text.Trim()))
                //{
                //    MessageBox.Show("请选择服务文件(exe)！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                if (!WinServiceUtils.ISWindowsServiceInstalled(ServiceName))
                {
                    XtraMessageBox.Show("服务没有安装!");
                    return;
                }
                if (WinServiceUtils.ISStart(ServiceName))
                {
                    XtraMessageBox.Show("服务已在运行中...!");
                    return;
                }
                _frmServiceStatus.ExecuteRes = "";
                _frmServiceStatus.SetExecuteMethod("start");
                if (_frmServiceStatus.ShowDialog(this) == DialogResult.OK)
                {
                    if (_frmServiceStatus.ExecuteRes == "start_success")
                    {
                        //  MessageBox.Show("服务开启成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnStopService.Enabled = true;
                        btnStartService.Enabled = false;
                        btnUninstallService.Enabled = !btnUninstallService.Enabled;
                    }
                    else if (_frmServiceStatus.ExecuteRes == "start_error")
                    {
                        // MessageBox.Show("服务开启失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // btnInstall.Enabled = false;
                        btnUninstallService.Enabled = !btnUninstallService.Enabled;
                    }

                }
                //string errMsg = string.Empty;
                //bool result = WinServiceUtils.StartService(ServiceName, ref errMsg);
                //if (result)
                //{
                //    MessageBox.Show("服务启动成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show("启动失败!" + errMsg);
                //}
            }
            finally
            {
                btnStartService.Cursor = Cursors.Default;
            }
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                btnStopService.Cursor = Cursors.WaitCursor;

                if (!WinServiceUtils.ISWindowsServiceInstalled(ServiceName))
                {
                    XtraMessageBox.Show("服务没有安装!");
                    return;
                }
                //string errMsg = null;
                //WinServiceUtils.StopService(ServiceName,ref errMsg);
                //MessageBox.Show("服务停止成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _frmServiceStatus.ExecuteRes = "";
                _frmServiceStatus.SetExecuteMethod("stop");
                if (_frmServiceStatus.ShowDialog(this) == DialogResult.OK)
                {
                    if (_frmServiceStatus.ExecuteRes == "stop_success")
                    {
                        //  MessageBox.Show("服务停止成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnStopService.Enabled = false;
                        btnStartService.Enabled = true;
                        btnUninstallService.Enabled = !btnUninstallService.Enabled;
                    }
                    else if (_frmServiceStatus.ExecuteRes == "stop_error")
                    {
                        // MessageBox.Show("服务停止失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // btnInstall.Enabled = false;
                        btnUninstallService.Enabled = !btnUninstallService.Enabled;
                    }

                }
            }
            finally
            {
                btnStopService.Cursor = Cursors.Default;
            }
        }
        static string ServiceName = "SuspeSysService";
        static string ServiceFileName = "SuspeSysService.exe";

        SusStatus _frmServiceStatus;

        private void xucServiceIndex_Load(object sender, EventArgs e)
        {
            btnInstallService.Enabled = !WinServiceUtils.ServiceIsExisted(ServiceName);
            btnUninstallService.Enabled = !btnInstallService.Enabled;

            _frmServiceStatus = new SusStatus();
            _frmServiceStatus.SetServiceName(ServiceName);
            _frmServiceStatus.SetServiceFileName(ServiceFileName);
            _frmServiceStatus.MessageReceived += _frmServiceStatus_MessageReceived;
            LoadSet();
        }


        private void _frmServiceStatus_MessageReceived(object sender, MessageEventArgs e)
        {
            string status = sender?.ToString();
            switch (status)
            {
                case "install_success":
                    btnInstallService.Enabled = false;
                    btnUninstallService.Enabled = true;
                    break;
                case "install_error":
                    btnInstallService.Enabled = true;
                    btnUninstallService.Enabled = false;
                    break;
                case "uninstall_success":
                    btnInstallService.Enabled = true;
                    btnUninstallService.Enabled = false;
                    break;
                case "uninstall_error":
                    btnInstallService.Enabled = false;
                    btnUninstallService.Enabled = true;
                    break;
                case "start_success":
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                    break;
                case "start_error":
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                    break;
                case "stop_success":
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                    break;
                case "stop_error":
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                    break;
            }
        }

        #region Tcp
        private void btnStartPort_Click(object sender, EventArgs e)
        {
            var strPort = txtNetServerPort.Text.Trim();
            if (string.IsNullOrEmpty(strPort))
            {
                XtraMessageBox.Show("端口不能为空!");
                return;
            }

            var port = Convert.ToInt32(strPort);
            //SuspeTcpServer.Intance.StartServer(port);
            //TcpAction.StartTcpPort(port);
            XtraMessageBox.Show("端口已开启!");
            //btnCANSet.Enabled = false;
        }
        #endregion

        private void btnConnectCAN_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            btnTest.Enabled = false;
            new Thread(Init).Start();
            new Thread(SetDefault).Start();
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

        private void btnServerTest_Click(object sender, EventArgs e)
        {
            btnCloseClient.Enabled = true;
            btnServerTest.Enabled = false;
            btnServerTest.Cursor = Cursors.WaitCursor;
            try
            {
                // btnTest.Enabled = false;
                new Thread(Init).Start();
                new Thread(SetServerDefault).Start();
            }
            finally
            {
                btnServerTest.Cursor = Cursors.Default;
                btnServerClose.Enabled = true;
            }
        }
        void SetServerDefault()
        {

            SusBootstrap.Instance.Start(lcTcpMessage, false, null, true);
            //SusRedisClient.Instance.Init();
            ////注册remoting
            //TcpBootstrap.Instance.RegsiterRemotingByCode(lcTcpMessage);
            //CANTcp.Instance.ConnectCAN(lcTcpMessage);
            // var susRemotingMessage = string.Format("悬挂业务端口【{0}】已开启！", TcpBootstrap.basePort);
            //this.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        }

        private void btnServerClose_Click(object sender, EventArgs e)
        {
            btnServerClose.Enabled = false;
            btnServerClose.Cursor = Cursors.WaitCursor;
            try
            {
                SusBootstrap.Instance.Stop(lcTcpMessage, false);
            }
            finally
            {
                btnServerClose.Cursor = Cursors.Default;
                btnServerTest.Enabled = true;
            }
        }

        private void btnCloseClient_Click(object sender, EventArgs e)
        {
            btnCloseClient.Enabled = false;
            btnCloseClient.Cursor = Cursors.WaitCursor;
            try
            {
                SusBootstrap.Instance.Disconnect(lcTcpMessage, false);
            }
            finally
            {
                btnCloseClient.Cursor = Cursors.Default;
                btnTest.Enabled = true;
            }
        }

        private void btnSend1_Click(object sender, EventArgs e)
        {
            btnSend1.Cursor = Cursors.WaitCursor;
            Thread thread = new Thread(new ThreadStart(NewThreadForm1));
            thread.Start();
            MessageBox.Show("success");
            btnSend1.Cursor = Cursors.Default;
        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            btnSend2.Cursor = Cursors.WaitCursor;
            Thread thread = new Thread(new ThreadStart(NewThreadForm2));
            thread.Start();
            MessageBox.Show("success");
            btnSend2.Cursor = Cursors.Default;
        }
        public void NewThreadForm1()
        {
            for (var index = 1; index < 100; index++)
            {
                Thread thread = new Thread(new ThreadStart(startForm1));
                thread.Start();
            }
        }

        private void startForm1()
        {
            SusBootstrap.Instance.SendMessageWithCANTest("1");
        }

        public void NewThreadForm2()
        {
            for (var index = 1; index < 100; index++)
            {
                Thread thread = new Thread(new ThreadStart(startForm2));
                thread.Start();
            }
        }

        private void startForm2()
        {
            SusBootstrap.Instance.SendMessageWithCANTest("2");
        }


        public void NewThreadForm3()
        {
            for (var index = 1; index < 100; index++)
            {
                Thread thread = new Thread(new ThreadStart(startForm3));
                thread.Start();
            }
        }

        private void startForm3()
        {
            SusBootstrap.Instance.SendMessageWithCANTest("3");
        }

        private void btnSend3_Click(object sender, EventArgs e)
        {
            btnSend3.Cursor = Cursors.WaitCursor;
            Thread thread = new Thread(new ThreadStart(NewThreadForm3));
            thread.Start();
            MessageBox.Show("success");
            btnSend3.Cursor = Cursors.Default;
        }
        private void LoadSet()
        {
            txtBusinessPort.Text = System.Configuration.ConfigurationManager.AppSettings["SusTcpPort"];
            txtNetServerPort.Text = System.Configuration.ConfigurationManager.AppSettings["CANPort"];
            txtRedisIP.Text = System.Configuration.ConfigurationManager.AppSettings["RedisIp"];
            txtRedisPort.Text = System.Configuration.ConfigurationManager.AppSettings["RedisPort"];
        }
        private static void SetSuspeLEDClientDBConfig(string susTcpPort, string cANPort, string redisIp, string redisPort)
        {
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(Application.StartupPath + "\\SuspeSys.Server.exe.config");//加载启动目录下的App.config配置文件
            XmlNode myNode = myDoc.SelectSingleNode("//appSettings");//查找appSettings结点
            XmlElement susTcpPortElement = (XmlElement)myNode.SelectSingleNode("//add [@key='SusTcpPort']");//查找add结点中key=sql的结点
            susTcpPortElement.SetAttribute("value", susTcpPort);//获取该结点中value值

            XmlElement cANPortElement = (XmlElement)myNode.SelectSingleNode("//add [@key='CANPort']");//查找add结点中key=sql的结点
            cANPortElement.SetAttribute("value", cANPort);//获取该结点中value值

            XmlElement redisIpElement = (XmlElement)myNode.SelectSingleNode("//add [@key='RedisIp']");//查找add结点中key=sql的结点
            redisIpElement.SetAttribute("value", redisIp);//获取该结点中value值

            XmlElement redisPortElement = (XmlElement)myNode.SelectSingleNode("//add [@key='RedisPort']");//查找add结点中key=sql的结点
            redisPortElement.SetAttribute("value", redisPort);//获取该结点中value值

            myDoc.Save(Application.StartupPath + "//SuspeSys.Server.exe.config");//保存到启动目录下的App.config配置文件
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                var susTcpPort = txtBusinessPort.Text;
                var netServerPort = txtNetServerPort.Text;
                var redisIP = txtRedisIP.Text;
                var redisPort = txtRedisPort.Text;
                SetSuspeLEDClientDBConfig(susTcpPort, netServerPort, redisIP, redisPort);
                //  XtraMessageBox.Show("设置成功！配置生效需重启软件", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var rs = MessageBox.Show("设置成功！配置生效需重启软件,是否重启?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    RestartMe();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("设置失败！错误-->" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

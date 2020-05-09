using DevExpress.DevAV;
using DevExpress.Internal;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using log4net;
using NHibernate.Cfg;
using SuspeSys.Client;
using SuspeSys.Client.Action;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules;
using SuspeSys.Client.Modules.AuthorizationManagement;
using SuspeSys.Client.Sqlite.Entity;
using SuspeSys.Client.Sqlite.Repository;

using SuspeSys.Domain.Common;
using SuspeSys.Domain.SusEnum;
using SuspeSys.SusRedisService.SusRedis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DevExpress.XtraTreeList.Demos
{
    public partial class frmLogin : XtraForm
    {
        frmLoginAction _action = new frmLoginAction();

        private Point mouseOffset; //记录鼠标指针的坐标 
        private bool isMouseDown = false; //记录鼠标按键是否按下 
        public frmLogin()
        {
            this.Opacity = 1;
            InitializeComponent();
            this.btnEditDatabase.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.None;

            //this.WindowState = FormWindowState.Maximized;
            //this.btnLogin.FlatStyle = FlatStyle.Flat;
            //this.btnLogin.FlatAppearance.BorderSize = 0;

            //this.btnCancel.FlatStyle = FlatStyle.Flat;
            //this.btnCancel.FlatAppearance.BorderSize = 0;

            this.StartPosition = FormStartPosition.CenterScreen;
            EnableLoginInfo = true;
            InitClientSysInfo();
            InitLanguage();
        }

        private void InitLanguage()
        {
            comboLanguage.Properties.Items.AddRange(new object[] { SusLanguageCons.SimplifiedChinese, SusLanguageCons.TraditionalChinese, SusLanguageCons.English, SusLanguageCons.Cambodia, SusLanguageCons.Vietnamese });
            comboLanguage.SelectedIndex = 0;


        }

        private bool EnableLoginInfo
        {
            set
            {

                btnLogin.Enabled = value;
                btnCancel.Enabled = value;
                txtPassword.Enabled = value;
                txtUserName.Enabled = value;
                btnEditDatabase.Enabled = value;
                //cmClient.Enabled = value;
                simpleButton1.Enabled = value;
                chcSaveUserName.Enabled = value;
                chcSavePwd.Enabled = value;

                //this.Opacity = value ? 1 : 0.7;
            }
        }

        #region 移动窗体
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            // 修改鼠标状态isMouseDown的值 
            // 确保只有鼠标左键按下并移动时，才移动窗体 
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }


        }
        #endregion

        //SuspeSys.Client.Modules.Ext.Loading loading = new SuspeSys.Client.Modules.Ext.Loading();

        #region 登录相关
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var promptTips = LanguageAction.Instance.BindLanguageTxt("promptTips");

                if (!backWorkLogin.IsBusy)
                {
                    var prompPleaseSetDataBase = LanguageAction.Instance.BindLanguageTxt("prompPleaseSetDataBase", "请设置数据库");

                    if (string.IsNullOrEmpty(btnEditDatabase.Text?.Trim()))
                    {
                        XtraMessageBox.Show(prompPleaseSetDataBase, promptTips);
                        return;
                    }
                    var prompInputUsername = LanguageAction.Instance.BindLanguageTxt("prompInputUsername", "请输入用户名");
                    if (string.IsNullOrEmpty(txtUserName.Text?.Trim()))
                    {
                        XtraMessageBox.Show(prompInputUsername, promptTips);
                        return;
                    }

                    var prompInputPassword = LanguageAction.Instance.BindLanguageTxt("prompInputPassword", "请输入密码");
                    if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        XtraMessageBox.Show(prompInputPassword, promptTips);
                        return;
                    }
                    var prompLoadApplication = LanguageAction.Instance.BindLanguageTxt("prompLoadApplication", "正在加载系统应用");
                    var prompWaiting = LanguageAction.Instance.BindLanguageTxt("prompWaiting", "请稍后...");
                    EnableLoginInfo = false;

                    WaitDialogForm waitDialogForm = new WaitDialogForm(prompWaiting, prompLoadApplication, new Size(300, 40), this);
                    waitDialogForm.Visible = false;
                    //new Thread(test).Start(waitDialogForm);
                    waitDialogForm.Show();
                    //  XtraMessageBox.Show("11");
                    backWorkLogin.RunWorkerAsync(waitDialogForm);

                    //backWorkLogin_DoWork(null, new DoWorkEventArgs(waitDialogForm));
                    //XtraMessageBox.Show("22");
                }
                else
                {
                    var prompLoging = LanguageAction.Instance.BindLanguageTxt("prompLoging", "正在登录....");
                    XtraMessageBox.Show(prompLoging, promptTips);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 取消，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.EnableLoginInfo = false;
            base.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        static string tagConnectionStr = "";
        static ILog errorLog = LogManager.GetLogger("Error");
        static ILog log = LogManager.GetLogger(typeof(frmLogin));
        private void backWorkLogin_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                // XtraMessageBox.Show("1");
                if (!hasInit)
                {
                    //   XtraMessageBox.Show("2");
                    //初始化
                    new SuspeApplicationAction();
                    // XtraMessageBox.Show("3");
                }
                //XtraMessageBox.Show("4");
                var defaultDB = DatabaseConnectionRepository.Instance.GetDeafult();
                // XtraMessageBox.Show(defaultDB?.ToString());
                if (defaultDB == null)
                {
                    //btnEditDatabase_ButtonClick(null, null);

                }
                else
                {
                    // XtraMessageBox.Show("5");
                    if (!tagConnectionStr.Equals(DatabaseConnectionRepository.Instance.GetDeafultConnectionStr()))
                    {
                        tagConnectionStr = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
                        SuspeSys.Service.Impl.SuspeApplication.ResetConfig(defaultDB);
                    }
                    log.InfoFormat("语言初始化开始");
                    LanguageAction.Instance.InitLanguage(tagConnectionStr);
                    log.InfoFormat("语言初始化成功");
                    SuspeSys.Service.Impl.SuspeApplication.Init();
                    // XtraMessageBox.Show("6");
                }
                try
                {
                    log.InfoFormat("redis初始化开始");
                    SuspeSys.Service.Impl.SusRedis.SusRedisClient.Instance.Init();
                    log.InfoFormat("redis初始化成功！");
                }
                catch (Exception ex)
                {
                    log.InfoFormat("redis初始化失败！");
                    errorLog.Error(ex);
                }
                //try
                //{
                //    log.InfoFormat("语言初始化开始");
                //    LanguageAction.Instance.InitLanguage();
                //    log.InfoFormat("语言初始化成功");
                //}
                //catch (Exception ex)
                //{
                //    errorLog.Error(ex);
                //}
                //XtraMessageBox.Show("7");
                //System.Threading.Thread.Sleep(5 * 1000);
                string userName = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();
                string clientName = cmClient.EditValue?.ToString();

                //开启授权验证
                if (SuspeSysSetting.Default.EnableAuthorize)
                    _action.GranValid(clientName);

                _action.Login(userName, password, clientName);


            }
            catch (Exception ex)
            {
                errorLog.Error(ex);
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                var waitDialogForm = e.Argument as WaitDialogForm;
                waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
            }

        }

        private void backWorkLogin_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  XtraMessageBox.Show("111");
            labelControl1.Text = CurrentUser.Instance.LoginInfo;
        }

        private void backWorkLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //   XtraMessageBox.Show("112");
            this.EnableLoginInfo = true;

            if (CurrentUser.Instance.User != null && !string.IsNullOrEmpty(CurrentUser.Instance.User.Id))
            {
                //登录成功记录
                SaveExt();

                //////开启授权验证
                ////if (SuspeSysSetting.Default.EnableAuthorize)
                ////    //授权检测
                ////    CurrentUser.Instance.IsAuthorization = SuspeSys.Utils.Authorization.AuthorizationDetection.Instance.Authorization();

                base.DialogResult = DialogResult.OK;

            }
            else
            {
                this.Opacity = 1;
                this.btnLogin.Enabled = true;
            }
            EnableLoginInfo = true;
            // SplashScreenManager.CloseForm();
            //loading.Hide();
        }
        #endregion


        private void frmLogin_Load(object sender, EventArgs e)
        {
            GetDefaultDataBase();
        }

        private void InitFormData()
        {
            //this.cmClient.Properties.DataSource = _action.GetAllClientMachines();
            //cmClient.Properties.Columns.Add(new LookUpColumnInfo("ClientMachineName"));
            //cmClient.Properties.ValueMember = "Id";
            //cmClient.Properties.DisplayMember = "ClientMachineName";
            //cmClient.Properties.ShowHeader = false;
            //cmClient.Properties.NullText = "请选择客户机";
            this.cmClient.EditValue = CustomSysInfo.Instance.ClientName;
            InitDefaultInfo();

        }


        private void btnEditDatabase_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //MessageBox.Show("添加");
            //ChangeDatabase change = new ChangeDatabase();
            SuspeSys.CustomerControls.Common.ChangeDatabase changeDataBase = new SuspeSys.CustomerControls.Common.ChangeDatabase();
            changeDataBase.ResetAdminPasswordEvent += ChangeDataBase_ResetAdminPasswordEvent;
            DialogResult result = changeDataBase.ShowDialog();
            if (result == DialogResult.OK)
            {
                var pTitle = LanguageAction.Instance.BindLanguageTxt("prompUpdateSuc", "修改成功!软件会自动重启!");
                XtraMessageBox.Show(pTitle);
                if (!tagConnectionStr.Equals(DatabaseConnectionRepository.Instance.GetDeafultConnectionStr()))
                {
                    var defaultDB = DatabaseConnectionRepository.Instance.GetDeafult();
                    if (defaultDB != null)
                    {
                        tagConnectionStr = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
                        SuspeSys.Service.Impl.SuspeApplication.ResetConfig(defaultDB);
                    }
                }
                //GetDefaultDataBase();
                RestartMe();
            }

        }
        private static void RestartMe()
        {
            Application.ExitThread();
            Application.Exit();
            Application.Restart();
            Process.GetCurrentProcess().Kill();
        }
        private void ChangeDataBase_ResetAdminPasswordEvent(object sender, EventArgs e)
        {
            string password = "123";
            try
            {
                _action.ResetPassword("admin", password);
                var prompTitle = LanguageAction.Instance.BindLanguageTxt("prompResetPassword", "密码已经重置为");
                var promptTips = LanguageAction.Instance.BindLanguageTxt("promptTips");

                XtraMessageBox.Show(prompTitle + $"{password}", promptTips);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }



        const string connectionFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        private void GetDefaultDataBase()
        {
            var defaultDB = DatabaseConnectionRepository.Instance.GetDeafult();
            if (defaultDB == null)
            {
                //btnEditDatabase_ButtonClick(null, null);

            }
            else
            {


                btnEditDatabase.EditValue = defaultDB.Alias;

                string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Config/hibernate.cfg.xml");

                XDocument doc = XDocument.Load(path);
                doc.Elements().First()
                    .Elements().First()
                    .Elements().Where(o => o.Attribute("name").Value == "connection.connection_string").First()
                    //
                    .SetValue(string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password));
                doc.Save(path, SaveOptions.DisableFormatting);
                //修改配置文件
                //Configuration conf = new Configuration();
                //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Config/hibernate.cfg.xml");

                //conf.Configure(path);

                //conf.Properties[NHibernate.Cfg.Environment.ConnectionString] = string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password);
                //var sessionFactory = conf.BuildSessionFactory();

                try
                {
                    this.InitFormData();
                }
                catch (Exception ex)
                {
                    var prompTitle = LanguageAction.Instance.BindLanguageTxt("prompSetDatabaseConfirm", "请设置正确的数据库服务器");
                    var promptTips = LanguageAction.Instance.BindLanguageTxt("promptTips");

                    var typeException = (System.TypeInitializationException)ex;
                    if (typeException != null && typeException.TypeName == "SuspeSys.Dao.Nhibernate.SessionFactory")
                    {
                        XtraMessageBox.Show(prompTitle, promptTips);
                    }
                    else
                    {
                        throw ex;
                    }

                }

            }
        }

        #region 记住密码
        const string sharedSecret = "susClient";
        /// <summary>
        /// 记录用户名，密码，客户端
        /// </summary>
        private void SaveExt()
        {
            Users user = new Users();
            user.SavePassword = this.chcSavePwd.Checked;
            user.SaveUserName = this.chcSaveUserName.Checked;

            if (chcSavePwd.Checked == true)
            {
                user.Password = SuspeSys.Utils.Security.AESCrypto.EncryptStringAES(txtPassword.Text.Trim(), sharedSecret);

            }
            else
            {
                user.Password = string.Empty;
                user.SavePassword = false;
            }

            user.UserName = txtUserName.Text.Trim();
            if (chcSaveUserName.Checked == true)
            {
                user.IsDefault = true;
            }
            UsersRepository.Instance.InsertOrUpdate(user);

            //{
            //    //记录客户端
            //    var model = new BasicInfo();
            //    model.Name = BasicInfoEnum.DefaultClient.ToString();
            //    model.Value = txtUserName.Text.Trim();
            //    BasicInfoRepository.Instance.Save(model);
            //}


        }
        protected List<Users> UserList;
        private void InitDefaultInfo()
        {
            //初始化用户列表
            UserList = UsersRepository.Instance.GetList().ToList();
            if (UserList != null)
            {
                UserList.ForEach(o =>
                {
                    this.txtUserName.Properties.Items.Add(o.UserName);
                });

                var defaultUser = UserList.Where(o => o.IsDefault == true).FirstOrDefault();
                if (defaultUser != null)
                {
                    InitDefaultUser(defaultUser);
                }
            }
        }

        private void InitDefaultUser(Users defaultUser)
        {
            if (defaultUser == null)
                return;

            this.txtUserName.EditValue = defaultUser.UserName;
            chcSaveUserName.Checked = defaultUser.SaveUserName;
            chcSavePwd.Checked = defaultUser.SavePassword;

            if (!string.IsNullOrEmpty(defaultUser.Password))
            {
                //解密
                txtPassword.Text = SuspeSys.Utils.Security.AESCrypto.DecryptStringAES(defaultUser.Password, sharedSecret);
            }
            else
            {
                txtPassword.Text = string.Empty;
            }
        }

        private void chcSaveUserName_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSaveUserName.Checked == false)
                chcSavePwd.Checked = false;
        }

        private void txtUserName_SelectedValueChanged(object sender, EventArgs e)
        {

            string userName = txtUserName.SelectedText;
            if (!string.IsNullOrEmpty(userName))
            {
                var defaultUser = UserList.Where(o => o.UserName == userName).FirstOrDefault();

                InitDefaultUser(defaultUser);
            }

        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnEditDatabase_ButtonClick(null, null);
        }

        #region 窗体圆角
        private void frmLogin_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                SetWindowRegion();
            }
            else
            {
                this.Region = null;
            }
        }

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 80);
            this.Region = new Region(FormPath);
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="rect">窗体大小</param>  
        /// <param name="radius">圆角大小</param>  
        /// <returns></returns>  
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角  

            arcRect.X = rect.Right - diameter;//右上角  
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角  
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角  
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion

        #region 客戶端注冊

        private void InitClientSysInfo()
        {

            CustomSysInfo.Instance.ClientName = BasicInfoRepository.Instance.GetByName(BasicInfoEnum.DefaultClient);
            this.cmClient.Text = CustomSysInfo.Instance.ClientName;
        }

        /// <summary>
        /// 是否初始化susperApplication
        /// </summary>
        bool hasInit = false;
        /// <summary>
        /// 切换客户机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeClient_Click(object sender, EventArgs e)
        {
            try
            {
                new SuspeApplicationAction();
                SuspeSys.Service.Impl.SuspeApplication.Init();
                hasInit = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            //1、获取客户机

            //2、填充客户机下拉列表
            this.cmClientList.Items.Clear();
            //cmClientList
            var clients = _action.GetAllClientMachines();
            if (clients != null && clients.Count > 0)
            {
                clients.ForEach((o) =>
                {

                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(o.ClientMachineName);
                    toolStripMenuItem.Tag = o;
                    this.cmClientList.Items.Add((ToolStripItem)toolStripMenuItem);
                });
                //this.cmClientList.Items.Add((ToolStripItem)new ToolStripMenuItem("新建客户机..."));
            }
            //3、填充新建客户机
            this.cmClientList.Items.Add((ToolStripItem)new ToolStripMenuItem("新建客户机..."));
            //Point screen = this.cmClientList.PointToScreen(this.btnChangeClient.Location);
            //screen.Offset(0, this.cmClientList.Height);
            this.cmClientList.Show(btnChangeClient, 0, this.btnChangeClient.Height - 2);
        }

        private void cmClientList_Click(object sender, EventArgs e)
        {

        }
        void ChangeLanguage(string language, string oldLanguage = null)
        {
            try
            {
                SusTransitionManager.StartTransition(this, "loading....");
                //var culture = CultureInfo.CreateSpecificCulture(language);
                //CultureInfo.DefaultThreadCurrentCulture = culture;
                //CultureInfo.DefaultThreadCurrentUICulture = culture;
                //Thread.CurrentThread.CurrentCulture = culture;
                //Thread.CurrentThread.CurrentUICulture = culture;
                //btnHelp.Caption = rm.GetString("MenuProcessOrder", culture); ;
                switch (language)
                {
                    case "中文(简体)":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.SimplifiedChinese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "中文(繁体)":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.TraditionalChinese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "English":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.English;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "Cambodia":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.Cambodia;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "Vietnamese":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.Vietnamese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                }
                LanguageAction.Instance.ChangeLanguage(this.Controls);
                this.Refresh();
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }

        }
        private void cmClientList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.cmClientList.Close();
            if (e.ClickedItem.Tag != null)
            {
                SuspeSys.Domain.ClientMachinesModel tag = e.ClickedItem.Tag as SuspeSys.Domain.ClientMachinesModel;
                if (tag == null || !(CustomSysInfo.Instance.ClientName != tag.ClientMachineName))
                    return;
                this.cmClient.Text = CustomSysInfo.Instance.ClientName = tag.ClientMachineName;
                var prompTitle = LanguageAction.Instance.BindLanguageTxt("prompRememberPassword", "请问需要记住客户机信息吗？");
                var promptTips = LanguageAction.Instance.BindLanguageTxt("promptTips");
                if (DialogResult.Yes != XtraMessageBox.Show(prompTitle, promptTips, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    return;

                BasicInfoRepository.Instance.Update(tag.ClientMachineName, BasicInfoEnum.DefaultClient);
            }
            else
            {
                //LdSysInfo.RefreshSysPara((Form)null);
                string companyName = new SuspeSys.Client.Action.Common.CommonAction().GetApplicationProfileByName(ApplicationProfileEnum.CustomerName);

                if (string.IsNullOrEmpty(companyName))
                {
                    InitAppInfo info = new InitAppInfo();
                    if (info.ShowDialog() != DialogResult.OK)
                        return;
                }

                companyName = new SuspeSys.Client.Action.Common.CommonAction().GetApplicationProfileByName(ApplicationProfileEnum.CustomerName);
                using (RegIndex formReg = new RegIndex(companyName, string.Empty))
                {
                    int num = (int)formReg.ShowDialog();
                }
            }
        }
        #endregion

        private void comboLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            var deConnStr = DatabaseConnectionRepository.Instance.GetDeafultConnectionStr();
            if (!string.IsNullOrEmpty(deConnStr))
            {
                var language = comboLanguage.Text;
                //var oldLanguage = comboLanguage.OldEditValue?.ToString();
                LanguageAction.Instance.InitLanguage(deConnStr);
                ChangeLanguage(language);
                LanguageAction.Instance.ChangeLanguage(this.Controls);
            }
            SusLanguageCons.CurrentLanguageTxt = comboLanguage.EditValue?.ToString();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageAction.Instance.ChangeLanguage(this.Controls);
        }
    }

}

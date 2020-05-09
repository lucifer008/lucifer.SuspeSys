using DevExpress.DevAV;
using DevExpress.Internal;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils.Taskbar;
using DevExpress.XtraRichEdit.API.Word;
using SuspeSys.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action;
using log4net;
using DevExpress.XtraSplashScreen;
using SuspeSys.Client.SusException;
using System.Net.Sockets;
using SuspeSys.Client.Action.SuspeRemotingClient;

namespace DevExpress.XtraTreeList.Demos
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {

                TaskbarAssistant.Default.Initialize();
                AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
                System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                DataDirectoryHelper.LocalPrefix = "WinOutlookInspiredApp";
                bool exit;
                using (IDisposable singleInstanceApplicationGuard = DataDirectoryHelper.SingleInstanceApplicationGuard("SuspeSysApp", out exit))
                {
                    if (exit) return;
                    DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
                    DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();
                    DevExpress.XtraEditors.WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("Office 2013 Light Gray");
                    DevExpress.XtraEditors.WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
                    DevExpress.XtraEditors.WindowsFormsSettings.ScrollUIMode = XtraEditors.ScrollUIMode.Touch;
                    DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", 10);//AppHelper.GetDefaultSize());
                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                    DevExpress.Mvvm.CommandBase.DefaultUseCommandManager = false;
                    using (new StartUpProcess())
                    {
                        using (StartUpProcess.Status.Subscribe(new DemoStartUp()))
                        {
                            //new SuspeSys.Client.Modules.ChangeDatabase().ShowDialog();

                            DialogResult dialogResult = DialogResult.Cancel;
                            //Login login = new Login();
                            //ElementHost.EnableModelessKeyboardInterop(login)
                            using (frmLogin login = new frmLogin())
                            {
                                dialogResult = login.ShowDialog();
                            }

                            if (dialogResult == DialogResult.OK)
                            {
                                try
                                {
                                    System.Windows.Forms.Application.Run(new Index());//new frmMain());
                                }
                                catch (AuthorizationException ex)
                                {

                                    //throw ex;
                                    MessageBox.Show(ex.Message);
                                    System.Windows.Forms.Application.Exit();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //BonusSkins.Register();
            //SkinManager.EnableFormSkins();
            //UserLookAndFeel.Default.SetSkinStyle("Office 2016 Black");//DevExpress Style

            // new SuspeApplicationAction();
            //Application.Run(new frmMain());
        }
        static ILog log = LogManager.GetLogger(typeof(Program));
         static  ILog errorLog = LogManager.GetLogger("Error");
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is SocketException)
            {
                var errInfo = "悬挂客户端未连接到采集服务，请检测服务是否正常!,若服务后启动请重新启动客户端!";
                XtraMessageBox.Show(errInfo, "错误");
                errorLog.Error(e.ExceptionObject);
                return;
            }

            errorLog.Error("CurrentDomain_UnhandledException" + (e.ExceptionObject as Exception)?.StackTrace, e.ExceptionObject as Exception);
            //throw new NotImplementedException();
            XtraMessageBox.Show((e.ExceptionObject as Exception)?.StackTrace, "错误");
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is SocketException)
            {
                var errInfo = "悬挂客户端未连接到采集服务，请检测服务是否正常!";
                XtraMessageBox.Show(errInfo, "错误");
                errorLog.Error(e.Exception);
                //SuspeRemotingService.Instance.Init();
                return;
            }
            //  ILog log = LogManager.GetLogger(typeof(Program));
            errorLog.Error("Application_ThreadException", e.Exception);
            XtraMessageBox.Show(e.Exception.Message, "错误");
        }

        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if (partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6")
            {
                string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }
    }
}

using log4net.Config;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Log4netHelper.Instance.InitLog4net();

            var fileName = string.Format("Config/log4net.cfg.xml");
            var fileInfo = new FileInfo(fileName);
            XmlConfigurator.Configure(fileInfo);
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", 10);//AppHelper.GetDefaultSize());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Index());
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
           // ILog log = LogManager.GetLogger(typeof(Program));
            //log.Error("CurrentDomain_UnhandledException", e.ExceptionObject as Exception);
            //throw new NotImplementedException();
            MessageBox.Show(e.ExceptionObject?.ToString(), "错误");
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
           // ILog log = LogManager.GetLogger(typeof(Program));
            //log.Error("Application_ThreadException", e.Exception);
            MessageBox.Show(e.Exception.Message, "错误");
        }
    }
}

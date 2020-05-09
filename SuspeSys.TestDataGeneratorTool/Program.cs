using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.TestDataGeneratorTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmToolMain());
        }
        static void Init()
        {
            
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            var log = LogManager.GetLogger(typeof(Program));
            log.Info("info---->log4net init success");

       }
    }
}

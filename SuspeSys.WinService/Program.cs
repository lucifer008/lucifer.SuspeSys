using SuspeSys.Remoting;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.WinService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            var service = new Service1();
            var appPath = ServiceUtils.GetWindowsServiceInstallPath(service.ServiceName);
            var log4Path = string.Format("{0}\\Config\\log4net.cfg.xml", appPath);
         
            Log4netHelper.Instance.InitLog4net(log4Path);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                service
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

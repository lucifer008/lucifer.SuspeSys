using SuspeSys.Remoting;
using SuspeSys.Remoting.TcpPattern;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log4netHelper.Instance.GetLog<Service1>().Info("悬挂系统服务开启中....");
            var appPath = ServiceUtils.GetWindowsServiceInstallPath(this.ServiceName);
            Log4netHelper.Instance.GetLog<Service1>().Info("OnStart--->appPath--->"+ appPath);
            ReigsterRemoting(appPath);
            Log4netHelper.Instance.GetLog<Service1>().Info("悬挂系统服务已开启!");
        }
        void ReigsterRemoting(string appPath=null)
        {
            try
            {
                //TcpBootstrap.Instance.RegsiterRemotingByCode();
                SusBootstrap.Instance.Start(null, true, appPath);
                Log4netHelper.Instance.GetLog<Service1>().Info("remoting start sucess!");
            }
            catch (Exception ex)
            {
                Log4netHelper.Instance.GetLog<Service1>().Error("ReigsterRemoting:remoting start fail-->", ex);
            }
        }

        protected override void OnStop()
        {
            //var appPath = ServiceUtils.GetWindowsServiceInstallPath(this.ServiceName);
            Log4netHelper.Instance.GetLog<Service1>().Info("悬挂系统服务已停止!");
        //    Log4netHelper.Instance.GetLog<Service1>().Info("OnStop--->End....");
        }
    }
}

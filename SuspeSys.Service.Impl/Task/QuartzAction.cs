using log4net;
using SuspeSys.Service.Task;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Task
{
    public class QuartzAction: SusLog
    {
        //static bool StartSchedulersEabled = Convert.ToBoolean(ConfigurationManager.AppSettings["StartSchedulersEabled"]);
        //static string EmployeeAutomaticLoginImportTime = Convert.ToString(ConfigurationManager.AppSettings["EmployeeAutomaticLoginImportTime"]);
        //protected static ILog TaskLogger = LogManager.GetLogger("TaskLogger");
        public static void Start()
        {
            try
            {
                //tcpLogInfo.Info("【定时任务启动】启动开始...");
                //QuartzHelper.ExecuteByCron<EmployeeAutomaticJob>(EmployeeAutomaticLoginImportTime);
                //tcpLogInfo.Info("【定时任务启动】启动完成.");
                EmployeeAutomaticJob.Instance.EmployeeAutomaticLoginExecute();
            }
            catch (Exception ex)
            {
                tcpLogError.ErrorFormat("【定时任务启动】异常--->{0}", ex);
            }
        }
    }
}

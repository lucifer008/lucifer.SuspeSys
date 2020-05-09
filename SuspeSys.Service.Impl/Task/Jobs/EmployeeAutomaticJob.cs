using log4net;
using Quartz;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.Task.Models;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Task
{
    /// <summary>
    /// 员工自动登录定时任务
    /// </summary>
    public class EmployeeAutomaticJob : SusLog,IJob
    {
        // protected static ILog TaskLogger = LogManager.GetLogger("TaskLogger");
        public readonly static EmployeeAutomaticJob Instance = new EmployeeAutomaticJob();
        public async System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {
            EmployeeAutomaticLoginExecute();
        }

        public void EmployeeAutomaticLoginExecute()
        {
            try
            {
                tcpLogInfo.Info("【定时任务启动】员工自动登录处理开始...");
                var sql = string.Format(@"select  t4.CARDINFO_Id CardInfoId,t4.MainTrackNumber,t4.LoginStatingNo,MAX(LoginDate) LoginDate from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  --AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
--and t4.MainTrackNumber=@MainTrackNumber
group by t4.CARDINFO_Id,t4.MainTrackNumber,t4.LoginStatingNo");
                var list = DapperHelp.QueryForList<CardInfoModel>(sql, null);
                list.ToList().ForEach(delegate (CardInfoModel ci)
                {
                    var logintime = ci.LoginDate.Value.ToString("yyyyMMdd");
                    if (!logintime.Equals(DateTime.Now.ToString("yyyyMMdd")))
                    {
                        var cardInfo = CardInfoDao.Instance.LoadById(ci.CardInfoId);
                        var cardLoginInfo = new CardLoginInfo();
                        cardLoginInfo.MainTrackNumber = ci.MainTrackNumber;
                        cardLoginInfo.CardInfo = cardInfo;
                        cardLoginInfo.LoginDate = DateTime.Now;
                        cardLoginInfo.LoginStatingNo = ci.LoginStatingNo;
                        cardLoginInfo.IsOnline = true;
                        CardLoginInfoDao.Instance.Save(cardLoginInfo);

                        tcpLogInfo.Info($"【定时任务启动】主轨:{ci.MainTrackNumber} 站点:{ci.LoginStatingNo} 已自动登录");
                    }
                });

                tcpLogInfo.Info("【定时任务启动】员工自动登录处理完成.");
            }
            catch (Exception ex)
            {
                tcpLogError.ErrorFormat("【定时任务启动】员工自动登录处理异常--->{0}", ex);
            }
        }
    }
}

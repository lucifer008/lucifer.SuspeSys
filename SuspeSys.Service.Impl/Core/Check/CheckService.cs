using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.AuxiliaryTools;
using SuspeSys.Dao;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuspeSys.Service.Impl.Core.Check
{
    public class CheckService : SusLog
    {
        private CheckService() { }
        public readonly static CheckService Instance = new CheckService();
        private static readonly object locObject = new object();
        public bool CheckStatingIsLogin(int statingNo, int MainTrackNumber)
        {
            var sql = string.Format(@"select ISNULL(COUNT(1),0) tCount from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
and t4.MainTrackNumber=@MainTrackNumber");
            //var paramValues = new string[] { statingNo };
            var c = DapperHelp.QueryForObject<int>(sql, new { LoginStatingNo = statingNo, MainTrackNumber = MainTrackNumber });
            return c != 0;
        }
        public bool CheckEmployeeIsLoginStating(int statingNo, int mainTrackNumber,string cardNo)
        {
            var sql = string.Format(@"select ISNULL(COUNT(1),0) tCount from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
and t4.MainTrackNumber=@MainTrackNumber AND T1.CardNo=@CardNo");
            //var paramValues = new string[] { statingNo };
            var c = DapperHelp.QueryForObject<int>(sql, new { LoginStatingNo = statingNo, MainTrackNumber = mainTrackNumber, CardNo=cardNo });
            return c != 0;
        }
        /// <summary>
        /// 衣架是否在工艺图，不在工艺图直接发送出战指令
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <returns></returns>
        internal bool HangerIsNotFlowChartHandler(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
            //var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo+""))//!dicHangerProcessFlowChart.ContainsKey(tenHangerNo.ToString()))
            {
                var info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】衣架不在工艺图上!", tenMaintracknumber, tenStatingNo, tenHangerNo);
                tcpLogInfo.Info(info);
                //发送出战指令
                //isFlowSucess = true;

                //出站
                CANTcpServer.server.AutoHangerOutStating(HexHelper.TenToHexString2Len(tenMaintracknumber), HexHelper.TenToHexString2Len(tenStatingNo), true, tenHangerNo);

                var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", info, tenMaintracknumber, tenStatingNo, tenHangerNo);
                //if (!isServiceStart)
                //    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                tcpLogInfo.Info(sucessOutSiteMessage);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return true;
            }
            //不是桥接站且不在工艺图上，且不是当前衣架所在的站
            var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(tenMaintracknumber + "", tenStatingNo.ToString());
            if (!isBridgeOutSite)
            {
                var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo+""); //dicHangerProcessFlowChart[tenHangerNo + ""];
                var isExist = fcList.Where(f => f.MainTrackNumber.Value == tenMaintracknumber && f.StatingNo != null && f.StatingNo.Value == tenStatingNo).Count() > 0;
                //  var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo+""); //dicCurrentHangerProductingFlowModelCache[tenHangerNo+""];
                var currentHangerNoStating = current.StatingNo != tenStatingNo;
                if (!isExist && currentHangerNoStating)
                {
                    var info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】站点不能生产衣架!", tenMaintracknumber, tenStatingNo, tenHangerNo);
                    tcpLogInfo.Info(info);
                    //发送出战指令
                    //isFlowSucess = true;

                    //出站
                    CANTcpServer.server.AutoHangerOutStating(HexHelper.TenToHexString2Len(tenMaintracknumber), HexHelper.TenToHexString2Len(tenStatingNo), true, tenHangerNo);

                    var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", info, tenMaintracknumber, tenStatingNo, tenHangerNo);
                    //if (!isServiceStart)
                    //    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                    tcpLogInfo.Info(sucessOutSiteMessage);
                    return true;
                }
            }
            return false;
        }

        internal void CheckLoginHandler(int tenStatingNo, int tenMaintracknumber)
        {
            var emloyeeIsLogin = CheckStatingIsLogin(tenStatingNo, tenMaintracknumber);
            if (!emloyeeIsLogin)
            {
                var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】没有员工登录!不能出站", tenMaintracknumber, tenStatingNo));
                tcpLogError.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 检查衣架是否重复出战，若是重复出战直接发送出战指令
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <returns></returns>
        internal bool RepeatOutSiteHandler(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
            bool isRepeatOutSite = NewCacheService.Instance.IsReatOutSite(tenMaintracknumber,tenStatingNo, tenHangerNo); // IsCheckRepeatOutSite(tenMaintracknumber.ToString(), tenStatingNo.ToString(), tenHangerNo.ToString());
            if (isRepeatOutSite)
            {
                var info = string.Format("3【错误】主轨:【{0}】站点:【{1}】衣架【{2}】重复出站", tenMaintracknumber, tenStatingNo, tenHangerNo);
                tcpLogInfo.Info(info);
                //出站
                CANTcpServer.server.AutoHangerOutStating(HexHelper.TenToHexString2Len(tenMaintracknumber), HexHelper.TenToHexString2Len(tenStatingNo), true, tenHangerNo);

                var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", info, tenMaintracknumber, tenStatingNo, tenHangerNo);
                //if (!isServiceStart)
                //    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                tcpLogInfo.Info(sucessOutSiteMessage);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检查衣架是否挂片过，若没有挂片直接发送出战指令
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <returns></returns>
        public bool HangerNonHangingPiece(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {

           // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerIsContainsAllocationItem(tenHangerNo+""))//dicCurrentHangerProductingFlowModelCache.ContainsKey(tenHangerNo.ToString()))
            {
                //衣架衣架生产完成!
                var info = string.Format("衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", tenHangerNo, tenMaintracknumber, tenStatingNo);
                tcpLogError.ErrorFormat(info);
                //出站
                CANTcpServer.server.AutoHangerOutStating(HexHelper.TenToHexString2Len(tenMaintracknumber), HexHelper.TenToHexString2Len(tenStatingNo), true, tenHangerNo);
                var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", info, tenMaintracknumber, tenStatingNo, tenHangerNo);
                //if (!isServiceStart)
                //    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                tcpLogInfo.Info(sucessOutSiteMessage);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return true;
                // return -2;
            }
            return false;
        }

        public bool IsCheckRepeatOutSite(string mainTrackNo, string statingNo, string hangerNo)
        {
            var isBridge = CANProductsService.Instance.IsBridge(int.Parse(mainTrackNo), int.Parse(statingNo));
            if (isBridge) return false;
            //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
            var statFlowNoList = fcList.Where(f => f.MainTrackNumber.Value == int.Parse(mainTrackNo) && f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo));
            if (statFlowNoList.Count() == 1 && statFlowNoList.Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 1)
            {
                return true;
            }
            else if (statFlowNoList.Count() > 1)
            {
                //  var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo); //dicCurrentHangerProductingFlowModelCache[hangerNo];
                var currentStatingFlowNoList = statFlowNoList.Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Select(ff => ff.FlowNo);
                if (statFlowNoList.Count() == currentStatingFlowNoList.Count())
                {
                    return true;
                }
                if (!currentStatingFlowNoList.Contains(current.FlowNo) && current.StatingNo != int.Parse(statingNo))
                {
                    return true;
                }
                if (currentStatingFlowNoList.Contains(current.FlowNo) && current.FlowIndex == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

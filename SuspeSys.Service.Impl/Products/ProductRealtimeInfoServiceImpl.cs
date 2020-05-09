using StackExchange.Redis.DataTypes;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Service.SusTcp;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusCache
{
    public class ProductRealtimeInfoServiceImpl : ServiceBase, IProductRealtimeInfoService
    {
        public static readonly ProductRealtimeInfoServiceImpl Instance = new ProductRealtimeInfoServiceImpl();
        /// <summary>
        /// 在线实时信息
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<ProductRealtimeInfoModel> SearchProductRealtimeInfo(string flowChartId, string groupNo)
        {
            var sql = string.Format(@"
SELECT 
            Res_Main.GroupNO,RTRIM(Res_Main.StatingNo) StatingNo,Res_Main.Capacity,Res_Main.MainTrackNumber,
            Res_Main.IsReceivingHanger,Res_Main.FirmwareSN,Res_Main.FirmwareVersion,Res_Main.StatingName,Res_Main.FaultInfo,
            Res_LoginStatInfo.Code,Res_LoginStatInfo.RealName,Res_LoginStatInfo.IsOnline,Res_LoginStatInfo.StatingLoginId, Res_Main.StatingId 
            ,(
                select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from HangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType IN(0,2)
				    AND ProcessChartId in('{0}')
                    UNION ALL
                    select COUNT(*) OutSiteNoCount from SuccessHangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType IN(0,2) and HangerNo>0
				    AND ProcessChartId in('{0}')
                )T_OutSiteNoCount
            ) OutSiteNoCount,
(
                select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from HangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				    AND ProcessChartId in('{0}')
                    UNION ALL
                    select COUNT(*) OutSiteNoCount from SuccessHangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1 and HangerNo>0
				    AND ProcessChartId in('{0}')
                )T_OutSiteNoCount
            ) TodayReworkCount,
             (     
                      select sum(TodayReworkAll) FROM(
                        select COUNT(*) TodayReworkAll from HangerProductFlowChart
				        WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				        AND CONVERT(varchar(10), 
				        DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				        AND MainTrackNumber=Res_Main.MainTrackNumber And 
				        EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				        AND ProcessChartId in('{0}')
                         UNION ALL
                        select COUNT(*) TodayReworkAll from SuccessHangerProductFlowChart
				        WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				        AND CONVERT(varchar(10), 
				        DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				        AND MainTrackNumber=Res_Main.MainTrackNumber And 
				        EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				        AND ProcessChartId in('{0}')
                    )T_TodayReworkAll
            )TodayReworkAll,
            (
				select SUM(ISNULL(ReHours,0)) ReHours from (
				select DATEDIFF (second , CompareDate , OutSiteDate )ReHours  
				from HangerProductFlowChart where Status=2 And FlowType=0 and ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				union all 
				select DATEDIFF ( second  , CompareDate , OutSiteDate )ReHours 
				from SuccessHangerProductFlowChart where Status=2 And FlowType=0
				AND  ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				)Res_ReailHours
            ) ReailHours,Res_FlowChart.TotalStanardHours
 FROM(
select T1.GroupNO,T2.StatingNo,T2.MainTrackNumber,T2.Capacity,T2.IsReceivingHanger,T2.SerialNumber FirmwareSN,T2.MainboardNumber FirmwareVersion,T2.StatingName,T2.Id StatingId,T2.FaultInfo  
 from SiteGroup T1
INNER JOIN ( SELECT * FROM Stating WHERE (IsEnabled=1 OR IsEnabled is null) and Deleted=0) T2 ON T2.SITEGROUP_Id=T1.Id
)Res_Main
LEFT JOIN(
     select T.SiteGroupNo,T.IsReceivingHanger,T.No,SUM(CONVERT(DECIMAL,CAST(COALESCE(T3.StanardHours,0) as DECIMAL ))) TotalStanardHours
    from ProcessFlowStatingItem T 
    INNER JOIN ProcessFlowChartFlowRelation T2 ON T2.ID=T.PROCESSFLOWCHARTFLOWRELATION_Id
    INNER JOIN ProcessFlow T3 ON T3.Id=t2.PROCESSFLOW_Id
    WHERE T2.PROCESSFLOWCHART_Id='{0}'
    Group by T.SiteGroupNo,T.IsReceivingHanger,T.No
) Res_FlowChart ON Res_FlowChart.SiteGroupNo=Res_Main.GroupNO 
and Res_FlowChart.No=Res_Main.StatingNo

LEFT JOIN(
	select  Tc.Id StatingLoginId,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline,MAX(TC.LoginDate) LoginDate from [dbo].[CardLoginInfo] TC
    LEFT JOIN CardInfo CI ON TC.CARDINFO_Id=CI.Id
	LEFT JOIN EmployeeCardRelation EC ON EC.CARDINFO_Id=TC.CARDINFO_Id
	LEFT JOIN Employee EM ON EM.Id=EC.EMPLOYEE_Id
	WHERE IsOnline=1  AND CI.CardType!=5  AND TC.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
   GROUP BY Tc.Id,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline
    union all
	select  Tc.Id StatingLoginId,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline,MAX(TC.LoginDate) LoginDate 
	from [dbo].[CardLoginInfo] TC
	LEFT JOIN CardInfo CI ON TC.CARDINFO_Id=CI.Id
	LEFT JOIN EmployeeCardRelation EC ON EC.CARDINFO_Id=TC.CARDINFO_Id
	LEFT JOIN Employee EM ON EM.Id=EC.EMPLOYEE_Id
	WHERE IsOnline=1 AND CI.CardType =5  AND TC.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
	 GROUP BY Tc.Id,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline
) Res_LoginStatInfo ON Res_LoginStatInfo.LoginStatingNo=Res_Main.StatingNo 
AND Res_LoginStatInfo.MainTrackNumber=Res_Main.MainTrackNumber

WHERE 1=1 

", flowChartId);
            if (!string.IsNullOrEmpty(groupNo))
            {
                sql += string.Format(" AND Res_Main.GroupNO='{0}'", groupNo);
            }
            sql += string.Format("  ORDER BY Res_Main.GroupNO,Res_Main.StatingNo");
            //var orderCondition = new Dictionary<string, string>();
            //orderCondition.Add(" GroupNO", "ASC");
            //orderCondition.Add(" StatingNo", "ASC");
            var list = DapperHelp.Query<ProductRealtimeInfoModel>(sql, null).ToList<ProductRealtimeInfoModel>(); //Query<ProductRealtimeInfoModel>(new StringBuilder(sql), orderCondition, true, null);

            return list.OrderBy(f => string.IsNullOrEmpty(f.StatingNo) ? -1 : int.Parse(f.StatingNo)).ToList();
        }
        /// <summary>
        /// 在线实时信息
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<ProductRealtimeInfoModel> SearchProductRealtimeInfoByServer(string flowChartId, string groupNo)
        {
            flowChartId = flowChartId == null ? string.Empty : flowChartId;

            #region sql
            var sql = string.Format(@"
SELECT 
            Res_Main.GroupNO,RTRIM(Res_Main.StatingNo) StatingNo,Res_Main.Capacity,Res_Main.MainTrackNumber,
            Res_Main.IsReceivingHanger,Res_Main.FirmwareSN,Res_Main.FirmwareVersion,Res_Main.StatingName,Res_Main.FaultInfo,
            Res_LoginStatInfo.Code,Res_LoginStatInfo.RealName,Res_LoginStatInfo.IsOnline,Res_LoginStatInfo.StatingLoginId, Res_Main.StatingId 
            ,(
                select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from HangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType IN(0,2)
				    AND ProcessChartId in('{0}')
                    UNION ALL
                    select COUNT(*) OutSiteNoCount from SuccessHangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType IN(0,2) and HangerNo>0
				    AND ProcessChartId in('{0}')
                )T_OutSiteNoCount
            ) OutSiteNoCount,
(
                select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from HangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				    AND ProcessChartId in('{0}')
                    UNION ALL
                    select COUNT(*) OutSiteNoCount from SuccessHangerProductFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				    AND MainTrackNumber=Res_Main.MainTrackNumber 
				    And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1 and HangerNo>0
				    AND ProcessChartId in('{0}')
                )T_OutSiteNoCount
            ) TodayReworkCount,
             (     
                      select sum(TodayReworkAll) FROM(
                        select COUNT(*) TodayReworkAll from HangerProductFlowChart
				        WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				        AND CONVERT(varchar(10), 
				        DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				        AND MainTrackNumber=Res_Main.MainTrackNumber And 
				        EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				        AND ProcessChartId in('{0}')
                         UNION ALL
                        select COUNT(*) TodayReworkAll from SuccessHangerProductFlowChart
				        WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				        AND CONVERT(varchar(10), 
				        DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				        AND MainTrackNumber=Res_Main.MainTrackNumber And 
				        EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				        AND ProcessChartId in('{0}')
                    )T_TodayReworkAll
            )TodayReworkAll,
            (
				select SUM(ISNULL(ReHours,0)) ReHours from (
				select DATEDIFF (second , CompareDate , OutSiteDate )ReHours  
				from HangerProductFlowChart where Status=2 And FlowType=0 and ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				union all 
				select DATEDIFF ( second  , CompareDate , OutSiteDate )ReHours 
				from SuccessHangerProductFlowChart where Status=2 And FlowType=0
				AND  ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				)Res_ReailHours
            ) ReailHours,Res_FlowChart.TotalStanardHours
 FROM(
select T1.GroupNO,T2.StatingNo,T2.MainTrackNumber,T2.Capacity,T2.IsReceivingHanger,T2.SerialNumber FirmwareSN,T2.MainboardNumber FirmwareVersion,T2.StatingName,T2.Id StatingId,T2.FaultInfo  
 from SiteGroup T1
INNER JOIN ( SELECT * FROM Stating WHERE (IsEnabled=1 OR IsEnabled is null) and Deleted=0) T2 ON T2.SITEGROUP_Id=T1.Id
)Res_Main
LEFT JOIN(
     select T.SiteGroupNo,T.IsReceivingHanger,T.No,SUM(CONVERT(DECIMAL,CAST(COALESCE(T3.StanardHours,0) as DECIMAL ))) TotalStanardHours
    from ProcessFlowStatingItem T 
    INNER JOIN ProcessFlowChartFlowRelation T2 ON T2.ID=T.PROCESSFLOWCHARTFLOWRELATION_Id
    INNER JOIN ProcessFlow T3 ON T3.Id=t2.PROCESSFLOW_Id
    WHERE T2.PROCESSFLOWCHART_Id='{0}'
    Group by T.SiteGroupNo,T.IsReceivingHanger,T.No
) Res_FlowChart ON Res_FlowChart.SiteGroupNo=Res_Main.GroupNO 
and Res_FlowChart.No=Res_Main.StatingNo

LEFT JOIN(
	select  Tc.Id StatingLoginId,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline,MAX(TC.LoginDate) LoginDate from [dbo].[CardLoginInfo] TC
    LEFT JOIN CardInfo CI ON TC.CARDINFO_Id=CI.Id
	LEFT JOIN EmployeeCardRelation EC ON EC.CARDINFO_Id=TC.CARDINFO_Id
	LEFT JOIN Employee EM ON EM.Id=EC.EMPLOYEE_Id
	WHERE IsOnline=1  AND CI.CardType!=5  AND TC.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
   GROUP BY Tc.Id,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline
    union all
	select  Tc.Id StatingLoginId,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline,MAX(TC.LoginDate) LoginDate 
	from [dbo].[CardLoginInfo] TC
	LEFT JOIN CardInfo CI ON TC.CARDINFO_Id=CI.Id
	LEFT JOIN EmployeeCardRelation EC ON EC.CARDINFO_Id=TC.CARDINFO_Id
	LEFT JOIN Employee EM ON EM.Id=EC.EMPLOYEE_Id
	WHERE IsOnline=1 AND CI.CardType =5  AND TC.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
	 GROUP BY Tc.Id,TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName,Tc.IsOnline
) Res_LoginStatInfo ON Res_LoginStatInfo.LoginStatingNo=Res_Main.StatingNo 
AND Res_LoginStatInfo.MainTrackNumber=Res_Main.MainTrackNumber

WHERE 1=1 

", flowChartId);
            #endregion

            if (!string.IsNullOrEmpty(groupNo))
            {
                sql += string.Format(" AND Res_Main.GroupNO='{0}'", groupNo);
            }
            sql += string.Format("  ORDER BY Res_Main.GroupNO,Res_Main.StatingNo");
            //var orderCondition = new Dictionary<string, string>();
            //orderCondition.Add(" GroupNO", "ASC");
            //orderCondition.Add(" StatingNo", "ASC");
            var list = DapperHelp.Query<ProductRealtimeInfoModel>(sql, null).ToList<ProductRealtimeInfoModel>(); //Query<ProductRealtimeInfoModel>(new StringBuilder(sql), orderCondition, true, null);

            var list1 = list.OrderBy(f => string.IsNullOrEmpty(f.StatingNo) ? -1 : int.Parse(f.StatingNo)).ToList();
            if (!string.IsNullOrEmpty(flowChartId))
            {
                var allocationList = GetAllocationFlow(flowChartId);
                foreach (var m in list1)
                {
                    //  m.IsReceive = (null != m.IsReceivingHanger && m.IsReceivingHanger.Value == 1) ? true : false;
                    //var flows = allocationList.Where(f => f.SiteNo.Equals(m.StatingNo.Trim())).Select(z => z.ProcessFlowName).Distinct();
                    m.ProcessFlowName = GetAllocationFlow(m.StatingNo?.Trim(), m.MainTrackNumber.Value, allocationList); //string.Join(",", flows);
                    if ("挂片站".Equals(m.StatingName?.Trim()))
                    {

                        var aList = allocationList.Where(f => string.IsNullOrEmpty(f.MergeProcessFlowChartFlowRelationId) && string.IsNullOrEmpty(f.SiteNo)).Select(f => f.ProcessFlowName);
                        m.ProcessFlowName = aList.Count() > 0 ? aList.First() : null;
                    }
                    var mainStatingKey = string.Format("{0}:{1}", m.MainTrackNumber, m.StatingNo.Trim());
                    //var dicMainTrackStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
                    var dicMainTrackStating = SuspeSys.Service.Impl.SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
                    if (dicMainTrackStating.Keys.Contains(mainStatingKey))
                    {
                        var mainTrackStating = dicMainTrackStating[mainStatingKey];
                        m.FullSite = mainTrackStating.IsFullSite ? "满站" : "";
                    }
                    var dicStatingInNumCache = SuspeSys.Service.Impl.SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
                    if (dicStatingInNumCache.Keys.Contains(mainStatingKey))
                    {
                        m.StatingInCount = dicStatingInNumCache[mainStatingKey].ToString();
                    }
                    var dicAllocationNumCache = SuspeSys.Service.Impl.SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);
                    if (dicAllocationNumCache.Keys.Contains(mainStatingKey))
                    {
                        var onlineCount = dicAllocationNumCache[mainStatingKey];
                        m.OnlineHangerCount = onlineCount > 0 ? onlineCount.ToString() : "0";
                    }
                    var stardHourTotal = m.TotalStanardHours;
                    var reaiyHourTotal = m.ReailHours;
                    //车缝效率 = 标准总工时÷实际总投入工时×100 %
                    if (reaiyHourTotal == 0)
                    {
                        m.SeamsEfficiencySite = "0%";
                    }
                    else
                    {
                        m.SeamsEfficiencySite = string.Format("{0:0%}", (decimal.Parse(stardHourTotal.ToString()) / reaiyHourTotal));
                    }
                }
            }
            return list1;
        }


        public IList<HangerStatingAllocationItemModel> GetAllocationFlow(string flowChartId)
        {
            var sql = string.Format(@"
select T1.Id  FlowChartd,T2.FlowName ProcessFlowName,T2.Id ProcessFlowChartFlowRelationId,
T3.No SiteNo,
T2.IsMergeForward,T2.MergeProcessFlowChartFlowRelationId,T4.MainTrackNumber    
FROM ProcessFlowChart T1 
INNER JOIN ProcessFlowChartFlowRelation T2 ON  T1.Id=T2.PROCESSFLOWCHART_Id
LEFT JOIN  ProcessFlowStatingItem T3 ON T2.Id=T3.PROCESSFLOWCHARTFLOWRELATION_Id
Left Join Stating T4 ON t3.STATING_Id=t4.Id
Where T1.ID='{0}'", flowChartId);
            return DapperHelp.Query<HangerStatingAllocationItemModel>(sql, null).ToList<HangerStatingAllocationItemModel>();
        }
        public string GetAllocationFlow(string statingNo, int mainTrakNumber, IList<HangerStatingAllocationItemModel> allList)
        {
            var flowNameList = allList.Where(f => null != f.SiteNo && f.MainTrackNumber != null && f.MainTrackNumber.Value == mainTrakNumber && f.SiteNo.Equals(statingNo.Trim())).Select(z => z.ProcessFlowName?.Trim()).Distinct().ToList<string>();
            foreach (var item in allList.Where(f => null != f.SiteNo && f.SiteNo.Equals(statingNo.Trim())))
            {
                var meFlowList = allList.Where(f => null == f.SiteNo && (f.MergeProcessFlowChartFlowRelationId != null && f.MergeProcessFlowChartFlowRelationId.Equals(item.ProcessFlowChartFlowRelationId))).Select(z => z.ProcessFlowName?.Trim()).Distinct().ToList<string>();
                flowNameList.AddRange(meFlowList);
            }
            return string.Join(",", flowNameList);
        }
        public SuspeSys.Domain.ProductsModel GetOnLineProduct(string groupNo)
        {
            var pSql = "select top 1 HangingPieceSiteNo,ProductionNumber,PROCESSFLOWCHART_Id ProcessFlowChartId from Products where GroupNo=@GroupNo";
            var onLineP = DapperHelp.QueryForObject<SuspeSys.Domain.ProductsModel>(pSql, new { GroupNo = groupNo });
            return onLineP;
        }

        public IList<OnlineOrInStationItemModel> SearchOnlineOrInStationItem(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string statingNo, string productId, int mainTrackNumber)
        {
            //  var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);

            var dicHangerProcessFlowChart = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);// SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                                                                                                                                                                                                                   //  var dicHangerStatingALloListCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            IList<OnlineOrInStationItemModel> reslt = new List<OnlineOrInStationItemModel>();
            try
            {
                totalCount = 0;
                //condition=string.IsNullOrEmpty(condition)?"1"
                IEnumerable<List<HangerProductFlowChartModel>> list = null;
                log.Info($"主轨:{mainTrackNumber} 站号:{statingNo}");
                if (!string.IsNullOrEmpty(statingNo))
                {

                    if (mainTrackNumber != 0)
                    {
                        list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.StatingNo.Value == short.Parse(statingNo) && ff.MainTrackNumber.Value == mainTrackNumber && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);

                        log.Info($"主轨:{mainTrackNumber} 站号:{statingNo} 条数--->{list.Count()}");
                    }
                    else
                    {
                        list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.StatingNo.Value == short.Parse(statingNo) && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);
                    }
                }
                if (!string.IsNullOrEmpty(productId))
                {
                    if (mainTrackNumber != 0)
                    {
                        list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.ProductsId.Equals(productId) && (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.MainTrackNumber.Value == mainTrackNumber).Count() > 0);
                    }
                    else
                    {
                        list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.ProductsId.Equals(productId) && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);
                    }
                }


                //foreach (var k in )
                //{
                //    var list = dicHangerProcessFlowChart[k].Where(f => f.IncomeSiteDate != null && f.OutSiteDate == null);

                //}
                //var dicHangingPieceStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);
                //foreach (var k in dicHangingPieceStating.Keys)
                //{
                //    var keys = k.Split(':');
                //    var maTrackNumber = keys[0];
                //    var statNo = keys[1];
                //    if (statNo.Equals(statingNo))
                //    {
                //        foreach (var vs in dicHangingPieceStating[k])
                //        {
                //            var oos = new OnlineOrInStationItemModel();
                //            oos.HangerNo = vs.HangerNo.ToString();
                //            oos.ProcessOrderNo = vs.ProcessOrderNo;
                //            oos.StyleNo = vs.StyleNo;
                //            oos.PColor = vs.PColor;
                //            oos.PSize = vs.PSize;
                //            oos.Num = vs.Num;
                //            oos.LineName = vs.LineName;
                //            oos.FlowNo = vs.FlowNo;
                //            oos.FlowName = vs.FlowName;
                //            oos.StatingNo = short.Parse(vs.StatingNo);
                //            oos.InStating = true;
                //            reslt.Add(oos);
                //            totalCount++;
                //        }

                //        return reslt;
                //    }
                //}
                foreach (var l in list)
                {
                    IEnumerable<HangerProductFlowChartModel> lists = null;
                    if (!string.IsNullOrEmpty(statingNo))
                    {
                        lists = l.Where(f => (f.HangerStatus == 0 || f.HangerStatus == 1) && (f.StatingNo.Value == short.Parse(statingNo)));
                    }
                    if (!string.IsNullOrEmpty(productId))
                    {
                        lists = l.Where(f => (f.HangerStatus == 0 || f.HangerStatus == 1) && (f.ProductsId.Equals(productId)));
                    }
                    foreach (var item in lists)
                    {

                        var oos = BeanUitls<OnlineOrInStationItemModel, HangerProductFlowChartModel>.Mapper(item);
                        oos.HangerStatus = item.HangerStatus;
                        oos.InStating = oos.HangerStatus == 1;
                        reslt.Add(oos);
                        totalCount++;
                    }
                    //var mainStatingKey = string.Format("{0}:{1}", xID, iD);
                    //var dicHangingPieceStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);

                }
                ////var hsAllList = dicHangerStatingALloListCache.Values;
                //IEnumerable<List<HangerProductFlowChartModel>> listAll = null;
                //if (!string.IsNullOrEmpty(statingNo))
                //{
                //    listAll = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.StatingNo.Value== int.Parse(statingNo)).Count() > 0);
                //}
                //if (!string.IsNullOrEmpty(productId))
                //{
                //    listAll = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.ProductsId.Equals(productId)).Count() > 0);
                //}
                //foreach (var l in listAll)
                //{

                //    foreach (var item in l)
                //    {
                //        var oos = new OnlineOrInStationItemModel();
                //        oos.HangerNo = item.HangerNo;
                //        oos.InStating = item.HangerStatus==1?true:false;
                //        oos.ProcessOrderNo = item.ProcessOrderNo;
                //        oos.StyleNo = item.StyleNo;
                //        oos.PColor = item.PColor;
                //        oos.PSize = item.PSize;
                //        oos.Num = item.Num?.ToString();
                //        oos.LineName = item.LineName;
                //        oos.FlowNo = item.FlowNo;
                //        oos.FlowName = item.FlowName;
                //        oos.FlowCode = item.FlowCode;
                //        oos.StatingNo = item.StatingNo;
                //        reslt.Add(oos);
                //        totalCount++;
                //    }
                //}
            }
            catch (Exception ex)
            {
                totalCount = 0;
                log.Error(ex);
            }
          
            return reslt;
        }

        public IList<OnlineOrInStationItemModel> SearchOnlineOrInStationItemByServer(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string statingNo, string productId, int mainTrackNumber)
        {
            //  var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);

            var dicHangerProcessFlowChart = SuspeSys.Service.Impl.SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);// SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                                                                                                                                                                                                                      //  var dicHangerStatingALloListCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            IList<OnlineOrInStationItemModel> reslt = new List<OnlineOrInStationItemModel>();
            totalCount = 0;
            //condition=string.IsNullOrEmpty(condition)?"1"
            IEnumerable<List<HangerProductFlowChartModel>> list = null;
            log.Info($"主轨:{mainTrackNumber} 站号:{statingNo}");
            if (!string.IsNullOrEmpty(statingNo))
            {

                if (mainTrackNumber != 0)
                {
                    list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.StatingNo.Value == short.Parse(statingNo) && ff.MainTrackNumber.Value == mainTrackNumber && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);

                    log.Info($"主轨:{mainTrackNumber} 站号:{statingNo} 条数--->{list.Count()}");
                }
                else
                {
                    list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.StatingNo.Value == short.Parse(statingNo) && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);
                }
            }
            if (!string.IsNullOrEmpty(productId))
            {
                if (mainTrackNumber != 0)
                {
                    list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.ProductsId.Equals(productId) && (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.MainTrackNumber.Value == mainTrackNumber).Count() > 0);
                }
                else
                {
                    list = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => ff.ProductsId.Equals(productId) && (ff.HangerStatus == 0 || ff.HangerStatus == 1)).Count() > 0);
                }
            }


            //foreach (var k in )
            //{
            //    var list = dicHangerProcessFlowChart[k].Where(f => f.IncomeSiteDate != null && f.OutSiteDate == null);

            //}
            //var dicHangingPieceStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);
            //foreach (var k in dicHangingPieceStating.Keys)
            //{
            //    var keys = k.Split(':');
            //    var maTrackNumber = keys[0];
            //    var statNo = keys[1];
            //    if (statNo.Equals(statingNo))
            //    {
            //        foreach (var vs in dicHangingPieceStating[k])
            //        {
            //            var oos = new OnlineOrInStationItemModel();
            //            oos.HangerNo = vs.HangerNo.ToString();
            //            oos.ProcessOrderNo = vs.ProcessOrderNo;
            //            oos.StyleNo = vs.StyleNo;
            //            oos.PColor = vs.PColor;
            //            oos.PSize = vs.PSize;
            //            oos.Num = vs.Num;
            //            oos.LineName = vs.LineName;
            //            oos.FlowNo = vs.FlowNo;
            //            oos.FlowName = vs.FlowName;
            //            oos.StatingNo = short.Parse(vs.StatingNo);
            //            oos.InStating = true;
            //            reslt.Add(oos);
            //            totalCount++;
            //        }

            //        return reslt;
            //    }
            //}
            foreach (var l in list)
            {
                IEnumerable<HangerProductFlowChartModel> lists = null;
                if (!string.IsNullOrEmpty(statingNo))
                {
                    lists = l.Where(f => (f.HangerStatus == 0 || f.HangerStatus == 1) && (f.StatingNo.Value == short.Parse(statingNo)));
                }
                if (!string.IsNullOrEmpty(productId))
                {
                    lists = l.Where(f => (f.HangerStatus == 0 || f.HangerStatus == 1) && (f.ProductsId.Equals(productId)));
                }
                foreach (var item in lists)
                {

                    var oos = BeanUitls<OnlineOrInStationItemModel, HangerProductFlowChartModel>.Mapper(item);
                    oos.HangerStatus = item.HangerStatus;
                    oos.InStating = oos.HangerStatus == 1;
                    reslt.Add(oos);
                    totalCount++;
                }
                //var mainStatingKey = string.Format("{0}:{1}", xID, iD);
                //var dicHangingPieceStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);

            }
            ////var hsAllList = dicHangerStatingALloListCache.Values;
            //IEnumerable<List<HangerProductFlowChartModel>> listAll = null;
            //if (!string.IsNullOrEmpty(statingNo))
            //{
            //    listAll = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.StatingNo.Value== int.Parse(statingNo)).Count() > 0);
            //}
            //if (!string.IsNullOrEmpty(productId))
            //{
            //    listAll = dicHangerProcessFlowChart.Values.Where(f => f.Where(ff => (ff.HangerStatus == 0 || ff.HangerStatus == 1) && ff.ProductsId.Equals(productId)).Count() > 0);
            //}
            //foreach (var l in listAll)
            //{

            //    foreach (var item in l)
            //    {
            //        var oos = new OnlineOrInStationItemModel();
            //        oos.HangerNo = item.HangerNo;
            //        oos.InStating = item.HangerStatus==1?true:false;
            //        oos.ProcessOrderNo = item.ProcessOrderNo;
            //        oos.StyleNo = item.StyleNo;
            //        oos.PColor = item.PColor;
            //        oos.PSize = item.PSize;
            //        oos.Num = item.Num?.ToString();
            //        oos.LineName = item.LineName;
            //        oos.FlowNo = item.FlowNo;
            //        oos.FlowName = item.FlowName;
            //        oos.FlowCode = item.FlowCode;
            //        oos.StatingNo = item.StatingNo;
            //        reslt.Add(oos);
            //        totalCount++;
            //    }
            //}

            return reslt;
        }
        /// <summary>
        /// 查询衣架信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="conModel"></param>
        /// <param name="endFlow"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public IList<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel> SearchCoatHangerInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string conModel, ref SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel endFlow, string hangerNo = null)
        {
            var rslt1 = new List<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel>();
            var dicHangerResume = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME);//SuspeSys.SusRedisService.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME);
            if (!dicHangerResume.ContainsKey(hangerNo))
            {
                totalCount = 0;
                return rslt1;
            }
            var hangerProductResumeList = dicHangerResume[hangerNo];
            hangerProductResumeList.ForEach(delegate (HangerProductFlowChartModel f)
            {
                var model = BeanUitls<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel, HangerProductFlowChartModel>.Mapper(f);
                if (model.FlowType.Value == 1 && model.Status != null && model.Status.Value == 2)
                {
                    model.CheckResult = "返工完成";
                    model.CheckInfo = (model.ReworkEmployeeNo + '/' + model.ReworkEmployeeName);
                    model.ReworkDate1 = model.ReworkDate.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.CheckInfo))
                        model.CheckResult = "操作返工";
                }
                //if (null != model.IsReworkSourceStating && model.IsReworkSourceStating.Value)
                //{
                //    model.CheckInfo = string.Format("工序返工");
                //}
                rslt1.Add(model);
            });
            totalCount = rslt1.Count;
            if (rslt1.Count > 0)
            {
                endFlow = BeanUitls<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel, HangerProductFlowChartModel>.Mapper(rslt1[rslt1.Count - 1]);
                if (endFlow.Status.Value == HangerProductFlowChartStaus.Successed.Value)
                {
                    endFlow.IsFlowSucess = true;
                    endFlow.IsSuccess = true;
                    endFlow.IsInStating = GetSuccessHangerStatingStatus(hangerNo); ;
                    endFlow.StatingNo = GetSuccessHangerStatingNo(hangerNo);
                    endFlow.FlowCode = null;
                    endFlow.FlowName = null;
                    endFlow.FlowNo = null;
                }
                else
                {
                    endFlow.IsInStating = endFlow.IncomeSiteDate != null;
                }
                if (endFlow.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                {
                    rslt1.RemoveAt(rslt1.Count - 1);
                }
            }

            endFlow.PieceNum = endFlow?.Num;
            var sqlProducts = "select * from Products where id=@Id";
            var sqlColor = "select top 1 ColorDescption from PoColor where  ColorValue=@ColorValue";
            var sqlSize = "select top 1 SizeDesption from PSize where Size=@Size";
            var prodcuts = DapperHelp.QueryForObject<SuspeSys.Domain.Products>(sqlProducts, new { Id = endFlow?.ProductsId?.Trim() });
            endFlow.StyleNo = prodcuts?.StyleNo;
            endFlow.ColorName = DapperHelp.QueryForObject<PoColor>(sqlColor, new { ColorValue = endFlow?.PColor?.Trim() })?.ColorDescption?.Trim();
            endFlow.SizeName = DapperHelp.QueryForObject<PSize>(sqlSize, new { Size = endFlow?.PSize?.Trim() })?.SizeDesption?.Trim();
            //string[] paramValues = null;
            ////            var queryString = string.Format(@"select * from(SELECT *,(case FlowType  when 1 then '返工' else '' end)CheckResult,(ReworkEmployeeNo+'/'+ReworkEmployeeName) CheckInfo FROM dbo.HangerProductFlowChart
            ////UNION SELECT * FROM dbo.SuccessHangerProductFlowChart)Res where 1=1");
            //var queryString = string.Format(@"select * from(SELECT *,(case FlowType  when 1 then '返工' else '' end)CheckResult,(ReworkEmployeeNo+'/'+ReworkEmployeeName) CheckInfo,CONVERT(varchar(20),ReworkDate,121) ReworkDate1 FROM dbo.HangerProductFlowChart)Res where 1=1");
            //queryString = string.Format(queryString + " {0}", string.IsNullOrEmpty(conModel) ? string.Empty : conModel);

            //var rslt1 = Query<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            ////查询最新分配的衣架信息
            //var dicHangerStatingAllocationItem = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            //if (dicHangerStatingAllocationItem.ContainsKey(hangerNo))
            //{

            //    var isSuccess = dicHangerStatingAllocationItem[hangerNo].Where(f => f.AllocatingStatingDate != null && f.OutSiteDate == null && !"-1".Equals(f.Memo)).Count() == 0;
            //    if (isSuccess)
            //    {
            //        endFlow = rslt1.Count > 0 ? BeanUitls<Domain.Ext.ReportModel.CoatHangerIndexModel, Domain.Ext.ReportModel.CoatHangerIndexModel>.Mapper(rslt1[rslt1.Count - 1]) : new Domain.Ext.ReportModel.CoatHangerIndexModel();
            //        endFlow.IsSuccess = true;
            //        endFlow.IsInStating = false;
            //        endFlow.StatingNo = null;
            //        endFlow.FlowCode = null;
            //        endFlow.FlowName = null;
            //        endFlow.FlowNo = null;
            //    }
            //    else
            //    {
            //        endFlow = rslt1.Count > 0 ? BeanUitls<Domain.Ext.ReportModel.CoatHangerIndexModel, Domain.Ext.ReportModel.CoatHangerIndexModel>.Mapper(rslt1[rslt1.Count - 1]) : new Domain.Ext.ReportModel.CoatHangerIndexModel();
            //        var lastAllocationFlow = dicHangerStatingAllocationItem[hangerNo].OrderByDescending(f => f.AllocatingStatingDate).First();
            //        if (!string.IsNullOrEmpty(lastAllocationFlow?.NextSiteNo?.Trim()))
            //        {
            //            endFlow.StatingNo = string.IsNullOrEmpty(lastAllocationFlow?.NextSiteNo?.Trim()) ? default(short) : short.Parse(lastAllocationFlow?.NextSiteNo?.Trim());
            //        }
            //        endFlow.IsSuccess = false;
            //        endFlow.IsInStating = lastAllocationFlow.IncomeSiteDate != null;
            //        endFlow.FlowCode = lastAllocationFlow?.ProcessFlowCode;
            //        endFlow.FlowName = lastAllocationFlow?.ProcessFlowName;
            //        endFlow.FlowNo = lastAllocationFlow?.FlowNo;

            //    }
            //    var sqlProducts = "select * from Products where id=@Id";
            //    var sqlColor = "select top 1 ColorDescption from PoColor where  ColorValue=@ColorValue";
            //    var sqlSize = "select top 1 SizeDesption from PSize where Size=@Size";
            //    var prodcuts = DapperHelp.QueryForObject<SuspeSys.Domain.Products>(sqlProducts, new { Id = endFlow.ProductsId });
            //    endFlow.StyleNo = prodcuts?.StyleNo;
            //    endFlow.PieceNum = prodcuts?.Unit;
            //    endFlow.ColorName = DapperHelp.QueryForObject<PoColor>(sqlColor, new { ColorValue = endFlow.PColor })?.ColorDescption;
            //    endFlow.SizeName = DapperHelp.QueryForObject<PSize>(sqlSize, new { Size = endFlow.PSize })?.SizeDesption;
            //}
            // rslt1.ToList<FlowBalanceTableReportModel>().ForEach(f => f.RewokRate = (100 * (decimal.Parse(f.ReworkYield.ToString()) / int.Parse(f.Yield.ToString()))).ToString("#0.00") + "%");
            return rslt1;
        }

        private bool GetSuccessHangerStatingStatus(string hangerNo)
        {
            var statingInItemDic = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (!statingInItemDic.ContainsKey(hangerNo)) return false;
            var hangerStatingItemList = statingInItemDic[hangerNo];
            var stList = hangerStatingItemList.Where(f => f.FlowIndex != null && f.FlowIndex.Value == -2 && f.StatingNo != null && f.HangerStatus == 1);
            if (stList.Count() == 0) return false;
            return true;
        }

        private short? GetSuccessHangerStatingNo(string hangerNo)
        {
            var statingInItemDic = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (!statingInItemDic.ContainsKey(hangerNo)) return null;
            var hangerStatingItemList = statingInItemDic[hangerNo];
            var stList = hangerStatingItemList.Where(f => f.FlowIndex != null && f.FlowIndex.Value == -2 && f.StatingNo != null);
            if (stList.Count() == 0) return null;
            return stList.First().StatingNo.Value;
        }
    }
}

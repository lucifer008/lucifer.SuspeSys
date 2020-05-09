using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Dao;
using SuspeSys.Domain;

namespace SuspeSys.CoreTest.Query
{
    [TestClass]
    public class QueryHangerProcessFlowChartTest: QueryTestBase
    {
        [TestMethod]
        public void TestQueryHangerProcessFlowChart(string flowChartId)
        {
            TestQueryBridge();
            var sql = string.Format(@" select T.SiteGroupNo GroupNo,T2.CraftFlowNo FlowIndex,T2.FlowNo,T2.FlowName,t.mainTrackNumber,T.IsReceivingHanger,T.No StatingNo,T2.IsEnabled,T2.IsMergeForward,T2.MergeFlowNo 
    from ProcessFlowStatingItem T 
    INNER JOIN ProcessFlowChartFlowRelation T2 ON T2.ID=T.PROCESSFLOWCHARTFLOWRELATION_Id
    INNER JOIN ProcessFlow T3 ON T3.Id=t2.PROCESSFLOW_Id
    WHERE T2.PROCESSFLOWCHART_Id='{0}'
  Order by T2.CraftFlowNo", flowChartId);
            var list=DapperHelp.QueryForList<HangerProductFlowChart>(sql);
            foreach (var item in list) {
                //log.Info("站点组:{0} 主轨:{1} 站点:{2} 生产顺序:{3} 工序号:{4} 工序名称:{5} 工序是否生效:{6}");
                var isEnableTxt = item.IsEnabled == null ? "否" : (item.IsEnabled.Value==1?"是":"否");
                var isReceivHangerTxt=item.IsReceivingHanger==null ? "否" : (item.IsReceivingHanger.Value == 1 ? "是" : "否");
                var isMergeForTxt=item.IsMergeForward == null ? "否" : (item.IsMergeForward.Value ? "是" : "否");
                log.Info($"站点组:{item.GroupNo?.Trim()} 主轨:{item.MainTrackNumber} 站点:{item.StatingNo} 是否接收衣架:{isReceivHangerTxt} 生产顺序:{item.FlowIndex} 工序号:{item.FlowNo?.Trim()} 工序名称:{item.FlowName?.Trim()} 工序是否生效:{isEnableTxt}) 是否向前合并:{isMergeForTxt} 合并工序号:{item.MergeFlowNo?.Trim()}");
            }
            log.Info("-----------------------------------------------------------------------------------------------------------");
        }
        [TestMethod]
        public void ExecuseTestQueryHangerProcessFlowChart() {
            TestQueryHangerProcessFlowChart("39b76b82c50c4cb8a191d0be5a240ac6");
        }
        [TestMethod]
        public void ExecuseTestQueryHangerProcessFlowChartByCompany()
        {
            TestQueryHangerProcessFlowChart("1ace70bef1d345858e3097b07899f8b8");
        }
        [TestMethod]
        public void TestQueryBridge()
        {
            var sql = string.Format(@" select * from BridgeSet where enabled=1");
            var list = DapperHelp.QueryForList<BridgeSet>(sql);
            log.Info("------------------------------桥接begin-----------------------------------------------------------------------------");
            foreach (var item in list)
            {
                
                log.Info($"主轨:{item.AMainTrackNumber} 站号:{item.ASiteNo} 方向:{item.DirectionTxt} 主轨:{item.BMainTrackNumber} 生产顺序:{item.BSiteNo}");
            }
            log.Info("-----------------------------------------桥接end------------------------------------------------------------------");
        }
    }

}

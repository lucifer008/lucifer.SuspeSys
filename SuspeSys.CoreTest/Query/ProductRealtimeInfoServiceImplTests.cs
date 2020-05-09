using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Client.Action.Products;
using SuspeSys.CoreTest;
using SuspeSys.Domain.Ext.ReportModel;
using System.Collections.Generic;

namespace SuspeSys.Service.Impl.SusCache.Tests
{
    [TestClass()]
    public class ProductRealtimeInfoServiceImplTests : QueryTestBase
    {
        [TestMethod()]
        public void SearchProductRealtimeInfoTestA()
        {
            string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            string groupNo = "A";
            // var statingNo = "8";
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat("主轨:{0} 站点:{1} 站内数:{2} 在线数:{3} 产出:{4}", prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
            }
        }
        [TestMethod()]
        public void SearchProductRealtimeInfoTest100()
        {
            string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            string groupNo = "100";
            // var statingNo = "8";
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat("主轨:{0} 站点:{1} 站内数:{2} 在线数:{3} 产出:{4}", prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
            }
        }
        [TestMethod()]
        public void SearchProductRealtimeInfoTestB()
        {
            string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            string groupNo = "B";
            // var statingNo = "8";
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat("主轨:{0} 站点:{1} 站内数:{2} 在线数:{3} 产出:{4}", prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
            }
        }
        [TestMethod()]
        public void SeachHangerInfo(string hangerNo)
        {
            long totalCount;
            IDictionary<string, string> ordercondition = new Dictionary<string, string>();
            string conModel = null;
            CoatHangerIndexModel endFlow = null;
           // string hangerNo = "1";
            var list = new ProductRealtimeInfoServiceImpl().SearchCoatHangerInfo(1, 200, out totalCount, ordercondition, conModel, ref endFlow, hangerNo);
            foreach (var prModel in list)
            {
                log.InfoFormat("衣架号:{0} 工序顺序:{1} 工序号:{2} 工序代码:{3} 工序名称:{4} 加工时间:{5} 加工站点:{6} 员工号:{7} 员工姓名:{8} 检验结果:{9} 检验信息:{10} 检验时间:{11} 站内:{12} 是否完成:{13}",
                    prModel.HangerNo, prModel.FlowIndex, prModel.FlowNo, prModel.FlowCode?.Trim(), prModel.FlowName?.Trim(), prModel.CompareDate, prModel.StatingNo, prModel.CardNo, prModel.EmployeeName, 
                    prModel.CheckResult, prModel.CheckInfo, prModel.ReworkDate1,
                    prModel.IsInStating?"是":"否",prModel.IsFlowSucess.Value?"是":"否");
            }
            log.InfoFormat("衣架号:{0} 工序顺序:{1} 工序号:{2} 工序代码:{3} 工序名称:{4} 加工时间:{5} 加工站点:{6} 员工号:{7} 员工姓名:{8} 检验结果:{9} 检验信息:{10} 检验时间:{11} 站内:{12} 是否完成:{13}",
                    endFlow.HangerNo, endFlow.FlowIndex, endFlow.FlowNo, endFlow.FlowCode?.Trim(), endFlow.FlowName?.Trim(), endFlow.CompareDate, endFlow.StatingNo, endFlow.CardNo, endFlow.EmployeeName,
                    endFlow.CheckResult, endFlow.CheckInfo, endFlow.ReworkDate1,
                    endFlow.IsInStating ? "是" : "否", (endFlow.IsFlowSucess!=null && endFlow.IsFlowSucess.Value) ? "是" : "否");
        }
        [TestMethod()]
        public void ExecueSeachHangerInfo()
        {
            SeachHangerInfo("2");
        }
    }
}
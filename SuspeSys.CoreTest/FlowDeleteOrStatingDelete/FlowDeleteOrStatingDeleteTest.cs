using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.ProcessOrder;
using System.Linq;
using SuspeSys.Domain;
using System.Collections.Generic;
using SusNet.Common.Utils;
using System.Threading;
using SuspeSys.Client.Action.Products;
using SuspeSys.Service.Impl.Core.Allocation;
using SuspeSys.Dao;

namespace SuspeSys.CoreTest.FlowDeleteOrStatingDelete
{
    [TestClass]
    public class FlowDeleteOrStatingDeleteTest : TestBase
    {
        [TestMethod]
        public void InitProcessFlowChart()
        {
            var processFlowChartService = ProcessFlowChartServiceImpl.Instance;
            Domain.ProcessFlowChart processFlowChart = GetProcessFlowChart("39b76b82c50c4cb8a191d0be5a240ac6");//new Domain.ProcessFlowChart();
            var processOrdFlowVersionList = new ProcessOrderQueryServiceImpl().GetProcessOrderFlowVersionList();
            var procesOrFlowVersion = processOrdFlowVersionList.ToList().First();
            processFlowChart.ProcessFlowVersion = procesOrFlowVersion;

            processFlowChart.LinkName = "测试1";
            processFlowChart.Remark = "测试1说明";
            var fcGroupList = new List<ProcessFlowChartGrop>();
            var processFlowChartFlowRelaionList = new List<ProcessFlowChartFlowRelationModel>();
            var pfcRelation = new ProcessFlowChartFlowRelationModel();
            pfcRelation.IsEnabled = 1;//是否生效
            pfcRelation.IsMergeForward = false;//是否往前合并
            pfcRelation.ProcessFlowChart = processFlowChart;
            // pfcRelation.ProcessFlow = pfChart.ProcessFlow;//制单工序Id
            System.Collections.Generic.IList<Domain.ProcessFlowChartFlowRelationModel> processFlowChartFlowRelationModelList = new List<ProcessFlowChartFlowRelationModel>();
            var processFlowList = GetAllProcessFlowList();
            var statingList = GetAllStatingList();

            var index = 1;
            foreach (var pf in processFlowList)
            {
                var pfcFlowRelation = new ProcessFlowChartFlowRelationModel();
                pfcFlowRelation.ProcessFlow = pf;
                pfcFlowRelation.ProcessFlowChart = processFlowChart;
                pfcFlowRelation.CraftFlowNo = index + "";
                pfcFlowRelation.IsEnabled = 1;
                pfcFlowRelation.FlowNo = pf.DefaultFlowNo+"";
                pfcFlowRelation.FlowName = pf.ProcessName;
                pfcFlowRelation.ProcessFlowChartFlowRelationModelList = new List<ProcessFlowChartFlowRelationModel>();
                var pfcFlowStatingItem = new ProcessFlowChartFlowRelationModel();
                var stating = statingList[index];
                pfcFlowStatingItem.StatingNo = stating.StatingNo?.Trim();
                pfcFlowStatingItem.IsAcceHanger = true;
                pfcFlowStatingItem.ProcessflowIdOrStatingId = stating.Id;
                pfcFlowStatingItem.FlowCode="";//角色
                pfcFlowStatingItem.IsReceivingAllColor = true;
                pfcFlowStatingItem.IsReceivingAllSize = true;
                //pfcFlowStatingItem.StatingRoleName = stating.StatingRoles?.RoleName?.Trim();
                pfcFlowRelation.ProcessFlowChartFlowRelationModelList.Add(pfcFlowStatingItem);
                processFlowChartFlowRelationModelList.Add(pfcFlowRelation);
                index++;
            }

            System.Collections.Generic.IList<Domain.ProcessFlowChartGrop> processFlowChartGroupList = fcGroupList;
            
            processFlowChartService.AddProcessFlowChart(processFlowChart, processFlowChartGroupList, processFlowChartFlowRelationModelList);
        }
        ProcessFlowChart GetProcessFlowChart(string flowChartId) {
            var sql = string.Format(@"select * from ProcessFlowChart where id=@Id'");
            var flowChart = DapperHelp.QueryForObject<ProcessFlowChart>(sql,new { Id=flowChartId});
            return flowChart;
        }
        IList<ProcessFlow> GetAllProcessFlowList()
        {
            var sql = string.Format(@"select * from ProcessFlow");
            var list = DapperHelp.QueryForList<ProcessFlow>(sql);
            return list;
        }
        IList<Stating> GetAllStatingList()
        {
            var sql = string.Format(@"select * from Stating");
            var list = DapperHelp.QueryForList<Stating>(sql);
            return list;
        }

        private int hangerNo = 1;
        private int mainTrackNumber1 = 1;
        private int hangingPieceStatingNo = 4;
        [TestMethod()]
        public void HangingPieceOutSiteDoServiceTest(string hangerNo)
        {

            var hanger = new SusNet.Common.Model.Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = mainTrackNumber1.ToString();
            hanger.StatingNo = hangingPieceStatingNo.ToString();
            var fData = string.Format("{0} {1} 06 FF 00 55 01 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            Thread.CurrentThread.Join(10000);
        }
        [TestMethod]
        public void A_CommonStatingOutSiteDoServiceTest(string hangerNo, int mainTrackNumber, int statingNo)
        {
            var hanger = new SusNet.Common.Model.Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = mainTrackNumber + "";
            hanger.StatingNo = statingNo + "";
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            Thread.CurrentThread.Join(10000);
        }
        /// <summary>
        /// 无桥接全量测试
        /// </summary>
        [TestMethod]
        public void TestNonBridgeAl(string hangerNo)
        {
            //hangerNo = "1";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            SearchProductRealtimeInfoTest("A");
            Thread.CurrentThread.Join(10000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            Thread.CurrentThread.Join(10000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            SearchProductRealtimeInfoTest("A");
        }
        [TestMethod()]
        public void SearchProductRealtimeInfoTest(string groupNo)
        {
            string flowChartId = "39b76b82c50c4cb8a191d0be5a240ac6";
            // string groupNo = "100";
            // var statingNo = "8";
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo + "");
            foreach (var prModel in list)
            {
                log.InfoFormat("主轨:{0} 站点:{1} 站内数:{2} 在线数:{3} 产出:{4}", prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
            }
        }
        //只删除站的
        [TestMethod]
        public void StatingOnlyDeleteTest1()
        {
            TestNonBridgeAl("22");
        }
        /// <summary>
        /// 无桥接全量测试
        /// </summary>
        [TestMethod]
        public void TestNonBridgeAl2(string hangerNo)
        {
            //hangerNo = "1";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            SearchProductRealtimeInfoTest("A");
            Thread.CurrentThread.Join(10000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");

            Thread.CurrentThread.Join();

        }
        //【无桥接全量测试】只删除站的
        [TestMethod]
        public void StatingOnlyDeleteTest2()
        {
            TestNonBridgeAl2("22");
        }
        /// <summary>
        /// 无桥接全量测试
        /// </summary>
        [TestMethod]
        public void TestNonBridgeAl3(string hangerNo)
        {

            //SearchProductRealtimeInfoTest("A");
            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            Thread.CurrentThread.Join(10000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            SearchProductRealtimeInfoTest("A");
        }
        //【无桥接全量测试】衣架在9号内，删除9号走出战
        [TestMethod]
        public void StatingOnlyDeleteTest3()
        {
            TestNonBridgeAl3("22");
        }
    }
}

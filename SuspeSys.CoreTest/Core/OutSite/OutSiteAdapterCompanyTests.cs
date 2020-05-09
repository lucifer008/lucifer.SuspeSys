using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Model;
using SusNet.Common.Utils;
using SuspeSys.Client.Action.Products;
using SuspeSys.CoreTest.Query;
using SuspeSys.CoreTest.Rework;
using SuspeSys.Domain.Ext.ReportModel;
using SuspeSys.Service.Impl.Core.Allocation;
using SuspeSys.Service.Impl.SusCache;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SuspeSys.CoreTest
{
    [TestClass()]
    public class OutSiteAdapterCompanyTests : TestBase
    {
        public OutSiteAdapterCompanyTests() { }
        public OutSiteAdapterCompanyTests(ILog _log)
        {
            if (null == log)
            {
                log = _log;
            }
        }
        private int hangerNo = 1;
        private int mainTrackNumber1 = 1;
        private int hangingPieceStatingNo = 4;
        [TestMethod()]
        public void HangingPieceOutSiteDoServiceTest()
        {

            var hanger = new Hanger();
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
            log.InfoFormat("HangingPieceOutSiteDoServiceTest-->执行完成");
        }

        [TestMethod]
        public void CommonStatingOutSiteDoServiceTest9()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "9";
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
            log.InfoFormat("CommonStatingOutSiteDoServiceTest9-->执行完成");
        }
        //[TestMethod]
        //public void CommonStatingOutSiteDoServiceTest6()
        //{
        //    var hanger = new Hanger();
        //    var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
        //    hanger.MainTrackNo = mainTrackNumber1.ToString();
        //    hanger.StatingNo = "6";
        //    var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
        //    var resBytes = HexHelper.StringToHexByte(fData);
        //    //byte[] resBytes = null;
        //    SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
        //    request.HangerNo = hexHangerNo;

        //    OutSiteAdapter.Instance.DoService(request);
        //    // Thread.Sleep(25000);
        //    // HangerComeInStating();
        //    //Thread.Sleep(25000);
        //    Thread.CurrentThread.Join(10000);
        //    log.InfoFormat("CommonStatingOutSiteDoServiceTest6-->执行完成");
        //}
        [TestMethod]
        public void BridgeSetOutSiteDoServiceTest_2_11()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "2";
            hanger.StatingNo = "11";
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
            log.InfoFormat("BridgeSetOutSiteDoServiceTest_100-->执行完成");
        }
        [TestMethod]
        public void B_CommonStatingOutSiteDoServiceTest9()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "2";
            hanger.StatingNo = "9";
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
        [TestMethod]
        public void A_BridgeSetOutSiteDoServiceTest()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "10";
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            Thread.CurrentThread.Join();
        }
        [TestMethod]
        public void A_CommonStatingOutSiteDoServiceTest3()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "3";
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            Thread.CurrentThread.Join();
        }
        [TestMethod]
        public void A_CommonStatingOutSiteDoServiceTest7()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "7";
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            Thread.CurrentThread.Join();
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
        public void SearchProductRealtimeInfoTest101()
        {
            string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            string groupNo = "101";
            // var statingNo = "8";
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat("主轨:{0} 站点:{1} 站内数:{2} 在线数:{3} 产出:{4}", prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
            }
        }
        [TestMethod()]
        public void SearchProductRealtimeInfoTestGroup(string groupNo, bool isTrow = true,string _flowChartId=null)
        {
            var testStatingNumItem = new QueryHangerStatingItemTest(log);
            string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            if (!string.IsNullOrEmpty(_flowChartId))
                flowChartId = _flowChartId;
            //string groupNo = "101";
            // var statingNo = "8";
            log.InfoFormat("----------------------------{0}------begin---------------------------------------------", groupNo);
            var list = new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat($"主轨:{0}-------> 站点:{1} 站内数:{2} 在线数:{3} 产出:{4} 满站:{prModel.FullSite} 今日产出:{prModel.OutSiteNoCount} 今日返工:{prModel.TodayReworkCount}", 
                    prModel.MainTrackNumber, prModel.StatingNo, prModel.StatingInCount, prModel.OnlineHangerCount, prModel.OutSiteNoCount);
                if ((Convert.ToInt32(prModel.StatingInCount) > 0) || (Convert.ToInt32(prModel.OnlineHangerCount) > 0))
                {
                    //log.Info($"【站内数明细】主轨:{prModel.MainTrackNumber} ---------->站点:{prModel.StatingNo}");
                    var listItem = testStatingNumItem.TestQueryStatingItem(prModel.MainTrackNumber.Value, prModel.StatingNo);
                    if (listItem.Count == 0)
                    {
                        var exxx = new ApplicationException($"站内数或者在线数与外部不一致!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 主轨--->{prModel.MainTrackNumber} 站点:------>{prModel.StatingNo}");
                        log.Error(exxx);
                        if (isTrow)
                        {
                            throw exxx;
                        }
                    }
                    if (listItem.Count != (Convert.ToInt32(prModel.StatingInCount) + Convert.ToInt32(prModel.OnlineHangerCount)))
                    {
                        var exxx = new ApplicationException($"站内数或者在线数与外部不一致!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 主轨--->{prModel.MainTrackNumber} 站点:------>{prModel.StatingNo}");
                        log.Error(exxx);
                        throw exxx;
                    }
                }
                else
                {
                    var sList = testStatingNumItem.GetStatingItemList(prModel.MainTrackNumber.Value, prModel.StatingNo);
                    if (sList.Count != 0)
                    {
                        var exxx = new ApplicationException($"站内数或者在线数与外部不一致!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 主轨--->{prModel.MainTrackNumber} 站点:------>{prModel.StatingNo}");
                        log.Error(exxx);
                        testStatingNumItem.TestQueryStatingItem(prModel.MainTrackNumber.Value, prModel.StatingNo);
                        if (isTrow)
                        {
                            throw exxx;
                        }

                    }

                }
            }
            log.InfoFormat("----------------------------{0}--------end-------------------------------------------", groupNo);
        }
        [TestMethod]
        public void BridgeSetOutSiteDoServiceTest_1_10()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "10";
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
            log.InfoFormat("BridgeSetOutSiteDoServiceTest_100-->执行完成");
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
            log.InfoFormat("----------------------------衣架轨迹【{0}】------begin---------------------------------------------", hangerNo);
            foreach (var prModel in list)
            {
                var fTxt = (prModel.IsFlowSucess != null && prModel.IsFlowSucess.Value) ? "是" : "否";
                log.InfoFormat($"衣架号:{ prModel.HangerNo}--------------------->主轨:{prModel.MainTrackNumber}------->加工站点:{prModel.StatingNo} 工序顺序:{prModel.FlowIndex}   站内:{(prModel.IsInStating ? "是" : "否")} 工序号:{prModel.FlowNo} 工序代码:{prModel.FlowCode} 工序名称:{prModel.FlowName} 加工时间:{prModel.CompareDate}  ------->员工号:{prModel.CardNo} 员工姓名:{prModel.EmployeeName} 检验结果:{ prModel.CheckResult} 检验信息:{prModel.CheckInfo} 检验时间:{prModel.ReworkDate1} 是否完成:{ fTxt}");
            }
            if (null != endFlow)
            {
                var fTxt = (endFlow.IsFlowSucess != null && endFlow.IsFlowSucess.Value) ? "是" : "否";
                log.InfoFormat($"衣架号:{ endFlow.HangerNo}--------------------->主轨:{endFlow.MainTrackNumber}------->加工站点:{endFlow.StatingNo} 工序顺序:{endFlow.FlowIndex}   站内:{(endFlow.IsInStating ? "是" : "否")} 工序号:{endFlow.FlowNo} 工序代码:{endFlow.FlowCode} 工序名称:{endFlow.FlowName} 加工时间:{endFlow.CompareDate}  ------->员工号:{endFlow.CardNo} 员工姓名:{endFlow.EmployeeName} 检验结果:{ endFlow.CheckResult} 检验信息:{endFlow.CheckInfo} 检验时间:{endFlow.ReworkDate1} 是否完成:{ fTxt}");
            }
            log.InfoFormat("----------------------------衣架轨迹【{0}】------end---------------------------------------------", hangerNo);
        }
        [TestMethod]
        public void ReworkHangerGenerator()
        {
            HangingPieceOutSiteDoServiceTest();
            SearchProductRealtimeInfoTest100();
            SeachHangerInfo(hangerNo + "");
            Thread.CurrentThread.Join(10000);
            CommonStatingOutSiteDoServiceTest9();
            SearchProductRealtimeInfoTest100();
            SeachHangerInfo(hangerNo + "");
            Thread.CurrentThread.Join(10000);
            BridgeSetOutSiteDoServiceTest_2_11();
            SearchProductRealtimeInfoTest101();
            SeachHangerInfo(hangerNo + "");
            Thread.CurrentThread.Join(10000);

            new ReworkDecodeUploadUnitTest().TestReworkDecodeUpload();
            SearchProductRealtimeInfoTest101();
            SeachHangerInfo(hangerNo + "");
            Thread.CurrentThread.Join(10000);

            BridgeSetOutSiteDoServiceTest_1_10();
            SeachHangerInfo(hangerNo + "");
            Thread.CurrentThread.Join(10000);
            Thread.CurrentThread.Join(20000);
        }
        public void HangingPieceOutSiteDoServiceTest(string hangerNo)
        {


          
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = mainTrackNumber1.ToString();
            hanger.StatingNo = hangingPieceStatingNo.ToString();

            var hangerCompareCard = string.Format("{0} {1} 06 FF 00 54 01 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resHangingBytes = HexHelper.StringToHexByte(hangerCompareCard);
            client.SendData(resHangingBytes);
            Thread.CurrentThread.Join(10000);

            var fData = string.Format("{0} {1} 06 FF 00 55 01 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            var request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            Thread.CurrentThread.Join(10000);
            log.InfoFormat("HangingPieceOutSiteDoServiceTest-->执行完成");
        }
        [TestMethod]
        public void A_CommonStatingOutSiteDoServiceTest(string hangerNo, string mainTrackNumber, string statingNo)
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = mainTrackNumber;
            hanger.StatingNo = statingNo;
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            // Thread.CurrentThread.Join();
        }
        [TestMethod]
        public void TestAllProcess(object objHangerNo)
        {
            // var hangerNo = "1";
            var hangerNo = objHangerNo + "";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            SeachHangerInfo(hangerNo + "");
            //Thread.CurrentThread.Join(5000);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "7");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            //// Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");
            //Thread.CurrentThread.Join(5000);
            //  A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "10");



            // SeachHangerInfo(hangerNo + "");
            //   Thread.CurrentThread.Join(5000);
            // SearchProductRealtimeInfoTestGroup("100");
            // new ReworkDecodeUploadUnitTest().TestReworkDecodeUpload();
            A_CommonStatingOutSiteDoServiceTest(hangerNo, "2", "2");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            SearchProductRealtimeInfoTestGroup("101");
            SeachHangerInfo(hangerNo + "");
            //  Thread.CurrentThread.Join(5000);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, "2", "6");
            Thread.CurrentThread.Join(10000);
            SeachHangerInfo(hangerNo + "");
            SearchProductRealtimeInfoTestGroup("101");
            // Thread.CurrentThread.Join(5000);

            //Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "9");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            SearchProductRealtimeInfoTestGroup("101");
            // Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");

            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "6");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            //Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");

            //Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "8");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("100");
            //Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");
            //Thread.CurrentThread.Join(5000);

            SearchProductRealtimeInfoTestGroup("100");
            SearchProductRealtimeInfoTestGroup("101");
            Thread.CurrentThread.Join(10000);
        }
        [TestMethod]
        public void TestQueryAllMaintrackNuberRealInfo()
        {
            SearchProductRealtimeInfoTestGroup("100");
            SearchProductRealtimeInfoTestGroup("101");
        }
        [TestMethod]
        public void TestQueryAlHangerResume()
        {
            for (var index = 1; index < 51; index++)
            {
                SeachHangerInfo(index + "");
            }
        }
        [TestMethod]
        public void TestAllLoadAll2()
        {
            TestAllProcess("324");
            Thread.CurrentThread.Join(10000);
        }
        [TestMethod]
        public void TestAllLoadAll()
        {
            for (var index = 1; index < 51; index++)
            {
                ParameterizedThreadStart ParameterizedThreadStart = new ParameterizedThreadStart(TestAllProcess);
                Thread thread = new Thread(ParameterizedThreadStart);
                // TestAllProcess(""+index);
                thread.Start(index + "");
            }
            Thread.CurrentThread.Join();
        }

        [TestMethod]
        public void TestOnlyOneMainTracknumberAllProcess(object objHangerNo)
        {
            // var hangerNo = "1";
            var hangerNo = objHangerNo + "";
            hangingPieceStatingNo = 1;
            string flowChartId = "0ed88b60e65b47cdbbff8b0cc3bff23b";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1",true, flowChartId);
            SeachHangerInfo(hangerNo + "");
            //Thread.CurrentThread.Join(5000);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "2");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            //// Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");
            //Thread.CurrentThread.Join(5000);
            //  A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "10");



            // SeachHangerInfo(hangerNo + "");
            //   Thread.CurrentThread.Join(5000);
            // SearchProductRealtimeInfoTestGroup("100");
            // new ReworkDecodeUploadUnitTest().TestReworkDecodeUpload();
            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "3");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            // SearchProductRealtimeInfoTestGroup("B");
            SeachHangerInfo(hangerNo + "");
            //  Thread.CurrentThread.Join(5000);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "4");
            Thread.CurrentThread.Join(10000);
            SeachHangerInfo(hangerNo + "");
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            // SearchProductRealtimeInfoTestGroup("B");
            // Thread.CurrentThread.Join(5000);

            //Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "5");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            // SearchProductRealtimeInfoTestGroup("B");
            // Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");

            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "6");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            // SearchProductRealtimeInfoTestGroup("B");
            //Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");

            MonitorUpload(1, 8, hangerNo);

            Thread.CurrentThread.Join(20000);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, "1", "6");
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTestGroup("1", true, flowChartId);
            // SearchProductRealtimeInfoTestGroup("B");
            //Thread.CurrentThread.Join(5000);
            SeachHangerInfo(hangerNo + "");

            Thread.CurrentThread.Join(10000);
            log.Info(objHangerNo+" 生产完成!!!!!!!!!!!");
        }

        [TestMethod]
        public void TestOnlyOneMainTracknumberAllProcess()
        {
            TestOnlyOneMainTracknumberAllProcess("78869");
            Thread.CurrentThread.Join(10000);
        }
        [TestMethod]
        public void TestCalStatingItem() {
            var testStatingNumItem = new QueryHangerStatingItemTest(log);
            testStatingNumItem.ExecueTestQueryStatingItem();
        }
    }
}
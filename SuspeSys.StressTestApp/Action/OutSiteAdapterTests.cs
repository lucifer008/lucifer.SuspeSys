using log4net;

using SusNet.Common.Model;
using SusNet.Common.Utils;
using SuspeSys.StressTestApp.Query;
using SuspeSys.Domain.Ext.ReportModel;
using SuspeSys.Service.Impl.Core.Allocation;
using SuspeSys.Service.Impl.SusCache;
using SuspeSys.StressTestApp.Base;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SuspeSys.StressTestApp
{
    //[TestClass()]
    public class OutSiteAdapterTests : TestBase
    {
        private int hangerNo = 1;
        private int mainTrackNumber1 = 1;
        private int hangingPieceStatingNo = 1;

        public OutSiteAdapterTests()
        {

        }
        public OutSiteAdapterTests(ILog _log)
        {
            if (null == log)
            {
                log = _log;
            }
        }
        private static readonly object lockObj = new Object();
        //[TestMethod()]
        public void HangingPieceOutSiteDoServiceTest(string hangerNo, string hangingPieceStatingNo = "1")
        {
            lock (lockObj)
            {
                log.Info($"【挂片】-------------------------- 衣架:{hangerNo} 站点:{hangingPieceStatingNo}--------------------------------------------------------------------------------------------------");
                var threadHangingPieceOutSite = new Thread(() => HangingPieceOutSiteHandler(hangerNo, hangingPieceStatingNo));
                threadHangingPieceOutSite.Start();
            }
            //HangingPieceOutSiteHandler(hangerNo, hangingPieceStatingNo);
        }

        private void HangingPieceOutSiteHandler(string hangerNo, string hangingPieceStatingNo)
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

            // OutSiteAdapter.Instance.DoService(request);

            client.SendData(resBytes);

            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            //  Thread.CurrentThread.Join(1000);
        }
        public void HangerInComeStatingTest(int hangerNo,int mainTrackNumber, int statingNo)
        {
            lock (lockObj)
            {
                log.Info($"【衣架进站】-------------------------- 衣架:{hangerNo} 主轨:{mainTrackNumber} 站点:{statingNo}--------------------------------------------------------------------------------------------------");
                var inComeThread = new Thread(() => HangerComeInStating(mainTrackNumber, statingNo, hangerNo));
                inComeThread.Start();
            }
        }
        private void HangerComeInStating(int mainTrackNumber, int statingNo, int hangerNo)
        {
            //衣架进站回应
            var data = string.Format("{0} {1} 02 FF 00 50 00 {2}", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo), string.Format("{0}", HexHelper.TenToHexString10Len(hangerNo)));
            var dBytes = HexHelper.StringToHexByte(data);
            client.SendData(dBytes);
        }
        public void HangerCompareTest(int hangerNo, int mainTrackNumber, int statingNo)
        {
            lock (lockObj)
            {
                log.Info($"【衣架进站比较】-------------------------- 衣架:{hangerNo} 主轨:{mainTrackNumber} 站点:{statingNo}--------------------------------------------------------------------------------------------------");
                var inComeThread = new Thread(() => SendHangerCompareRequest(mainTrackNumber+"", statingNo+"", hangerNo+""));
                inComeThread.Start();
            }
        }
        /// <summary>
        /// 衣架比较
        /// </summary>
        /// <param name="tenMainTrackNumber1"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        public void SendHangerCompareRequest(string tenMainTrackNumber1, string tenStatingNo, string tenHangerNo)
        {
            ////衣架比较回应
            var data = string.Format("{0} {1} 06 FF 00 54 00 {2}", HexHelper.TenToHexString2Len(tenMainTrackNumber1), HexHelper.TenToHexString2Len(tenStatingNo), string.Format("{0}", HexHelper.TenToHexString10Len(tenHangerNo)));
            var dBytes = HexHelper.StringToHexByte(data);
            client.SendData(dBytes);

        }
        // 
        public void CommonStatingOutSiteDoServiceTest8()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = mainTrackNumber1.ToString();
            hanger.StatingNo = "8";
            var fData = string.Format("{0} {1} 06 FF 00 55 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var resBytes = HexHelper.StringToHexByte(fData);
            //byte[] resBytes = null;
            SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage request = new SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage(resBytes);
            request.HangerNo = hexHangerNo;

            OutSiteAdapter.Instance.DoService(request);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            //  Thread.CurrentThread.Join(10000);
        }
        // 
        public void B_BridgeSetOutSiteDoServiceTest()
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
        }
        //  
        public void B_CommonStatingOutSiteDoServiceTest8()
        {
            var hanger = new Hanger();
            hangerNo = 33;
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
        // 
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
            Thread.CurrentThread.Join(10000);
        }
        //
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
            Thread.CurrentThread.Join(10000);
        }
        // 
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
            Thread.CurrentThread.Join(10000);
        }
        // 
        public void A_CommonStatingOutSiteDoServiceTest(string hangerNo, int mainTrackNumber, int statingNo)
        {
            lock (lockObj)
            {
                log.Info($"------【普通站出战】 衣架:{hangerNo} 站点:{hangingPieceStatingNo}--------------------------------------------------------------------------------------------------");
                var commonStatingOutSiteThread = new Thread(() => CommonStatingOUtSiteHandler(hangerNo, mainTrackNumber, statingNo));
                commonStatingOutSiteThread.Start();
            }
            //   CommonStatingOUtSiteHandler(hangerNo, mainTrackNumber, statingNo);
        }

        private void CommonStatingOUtSiteHandler(string hangerNo, int mainTrackNumber, int statingNo)
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

            // OutSiteAdapter.Instance.DoService(request);
            client.SendData(resBytes);
            // Thread.Sleep(25000);
            // HangerComeInStating();
            //Thread.Sleep(25000);
            //Thread.CurrentThread.Join(1000);
        }

        /// <summary>
        /// 桥接都不携带工序(1-10,2-11是桥接站)
        /// </summary>
      //  
        public void TestAllStating(string hangerNo)
        {
            HangingPieceOutSiteDoServiceTest(hangerNo);

            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 11);

            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(5000);
            // A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
        }
        /// <summary>
        /// 桥接都不携带工序
        /// </summary>
      //  
        public void TestExecuseAllStating()
        {
            TestAllStating("2");
        }
        /// <summary>
        /// 桥接都不携带工序
        /// </summary>
      //  
        public void TestAllStating33(string hangerNo)
        {
            HangingPieceOutSiteDoServiceTest(hangerNo);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            // A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 11);
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 9);
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            // A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 11);
            SearchProductRealtimeInfoTest("A");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
            // A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
        }
        /// <summary>
        /// 桥接都不携带工序
        /// </summary>
       // 
        public void TestExecuseAllStating33()
        {
            TestAllStating33("2");
        }
        //[TestMethod()]
        //public void SeachHangerInfo(string hangerNo)
        //{
        //    long totalCount;
        //    IDictionary<string, string> ordercondition = new Dictionary<string, string>();
        //    string conModel = null;
        //    CoatHangerIndexModel endFlow = null;
        //    // string hangerNo = "1";
        //    var list = new ProductRealtimeInfoServiceImpl().SearchCoatHangerInfo(1, 200, out totalCount, ordercondition, conModel, ref endFlow, hangerNo);
        //    foreach (var prModel in list)
        //    {
        //        log.InfoFormat("衣架号:{0} 工序顺序:{1} 工序号:{2} 工序代码:{3} 工序名称:{4} 加工时间:{5} 加工站点:{6} 员工号:{7} 员工姓名:{8} 检验结果:{9} 检验信息:{10} 检验时间:{11} 站内:{12} 是否完成:{13}",
        //            prModel.HangerNo, prModel.FlowIndex, prModel.FlowNo, prModel.FlowCode?.Trim(), prModel.FlowName?.Trim(), prModel.CompareDate, prModel.StatingNo, prModel.CardNo, prModel.EmployeeName,
        //            prModel.CheckResult, prModel.CheckInfo, prModel.ReworkDate1,
        //            prModel.IsInStating ? "是" : "否", prModel.IsFlowSucess.Value ? "是" : "否");
        //    }
        //    log.InfoFormat("衣架号:{0} 工序顺序:{1} 工序号:{2} 工序代码:{3} 工序名称:{4} 加工时间:{5} 加工站点:{6} 员工号:{7} 员工姓名:{8} 检验结果:{9} 检验信息:{10} 检验时间:{11} 站内:{12} 是否完成:{13}",
        //            endFlow.HangerNo, endFlow.FlowIndex, endFlow.FlowNo, endFlow.FlowCode?.Trim(), endFlow.FlowName?.Trim(), endFlow.CompareDate, endFlow.StatingNo, endFlow.CardNo, endFlow.EmployeeName,
        //            endFlow.CheckResult, endFlow.CheckInfo, endFlow.ReworkDate1,
        //            endFlow.IsInStating ? "是" : "否", (endFlow.IsFlowSucess != null && endFlow.IsFlowSucess.Value) ? "是" : "否");
        //}
        //[TestMethod()]
        public void ExecueSeachHangerInfo()
        {
            SeachHangerInfo("2");
        }

        //[TestMethod()]
        public void SearchProductRealtimeInfoTest(string groupNo, bool isTrow = true, string flowChartId = "1ace70bef1d345858e3097b07899f8b8")
        {
            var testStatingNumItem = new QueryHangerStatingItemTest(log);
            //string flowChartId = "1ace70bef1d345858e3097b07899f8b8";
            //string groupNo = "101";
            // var statingNo = "8";
            log.InfoFormat("----------------------------{0}------begin---------------------------------------------", groupNo);
            var realyService = new ProductRealtimeInfoServiceImpl();
            var list = realyService.SearchProductRealtimeInfoByServer(flowChartId, groupNo); //new ProductsAction().SearchProductRealtimeInfo(flowChartId, groupNo);
            foreach (var prModel in list)
            {
                log.InfoFormat($"主轨:{ prModel.MainTrackNumber}-------> 站点:{prModel.StatingNo} 站内数:{prModel.StatingInCount} 在线数:{ prModel.OnlineHangerCount} 产出:{prModel.OutSiteNoCount} 满站:{prModel.FullSite} 今日产出:{prModel.OutSiteNoCount} 今日返工:{prModel.TodayReworkCount}");
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
                        if (isTrow)
                        {
                            throw exxx;
                        }
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
        //[TestMethod()]
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
                log.InfoFormat($"衣架号:{ prModel.HangerNo}--------------------->生产组:{prModel.GroupNo?.Trim()} 主轨:{prModel.MainTrackNumber}------->加工站点:{prModel.StatingNo} 工序顺序:{prModel.FlowIndex}   站内:---->{(prModel.IsInStating ? "是" : "否")} 工序号:{prModel.FlowNo?.Trim()} 工序代码:{prModel.FlowCode?.Trim()} 工序名称:{prModel.FlowName?.Trim()} 加工时间:{prModel.CompareDate}  ------->员工号:{prModel.CardNo} 员工姓名:{prModel.EmployeeName} 检验结果:{ prModel.CheckResult} 检验信息:【{prModel.CheckInfo}】 检验时间:{prModel.ReworkDate1} 是否完成:--->【{ fTxt}】");
            }
            if (null != endFlow)
            {
                var fTxt = (endFlow.IsFlowSucess != null && endFlow.IsFlowSucess.Value) ? "是" : "否";
                log.InfoFormat($"衣架号:{ endFlow.HangerNo}--------------------->生产组:{endFlow.GroupNo?.Trim()} 主轨:{endFlow.MainTrackNumber}------->加工站点:{endFlow.StatingNo} 工序顺序:{endFlow.FlowIndex}   站内:---->{(endFlow.IsInStating ? "是" : "否")} 工序号:{endFlow.FlowNo?.Trim()} 工序代码:{endFlow.FlowCode?.Trim()} 工序名称:{endFlow.FlowName?.Trim()} 加工时间:{endFlow.CompareDate}  ------->员工号:{endFlow.CardNo} 员工姓名:{endFlow.EmployeeName} 检验结果:【{ endFlow.CheckResult}】 检验信息:【{endFlow.CheckInfo}】 检验时间:{endFlow.ReworkDate1} 是否完成:--->【{ fTxt}】");
            }
            log.InfoFormat("----------------------------衣架轨迹【{0}】------end---------------------------------------------", hangerNo);
        }
        /// <summary>
        /// 桥接都携带工序（桥接1-3；2-4）
        /// </summary>

        public void TestAllBridgeStatingNonCarryFlow(string hangerNo)
        {
            //hangerNo = "1";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }

        /// <summary>
        /// 桥接都不携带工序
        /// </summary>

        public void TestExecuseTestAllBridgeStatingNonCarryFlow()
        {
            TestAllBridgeStatingNonCarryFlow("28");
        }

        /// <summary>
        /// 桥接1-10 2-9 且 2-9携带工序
        /// </summary>

        public void TestOnlyOneStatingBrdigeSetCarryFlow(string hangerNo)
        {
            //hangerNo = "1";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 9);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 8);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        /// <summary>
        /// 桥接仅携带工序返工
        /// </summary>

        public void ExeuceTestOnlyOneStatingBrdigeSetCarryFlow()
        {
            TestOnlyOneStatingBrdigeSetCarryFlow("2");
            Thread.CurrentThread.Join(10000);
        }


        /// <summary>
        /// 桥接1-10 2-9 且 2-9都携带工序
        /// </summary>

        public void TestTwoStatingBrdigeSetCarryFlow(string hangerNo)
        {
            //hangerNo = "1";
            HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);


            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            SearchProductRealtimeInfoTest("A");
            SearchProductRealtimeInfoTest("B");
            SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        /// <summary>
        /// 桥接仅携带工序返工
        /// </summary>

        public void ExeuceTestTwoStatingBrdigeSetCarryFlow()
        {
            TestTwoStatingBrdigeSetCarryFlow("35");
            Thread.CurrentThread.Join(10000);
        }
    }
}
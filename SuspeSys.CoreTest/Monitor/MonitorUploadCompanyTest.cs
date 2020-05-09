using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.AuxiliaryTools;
using SuspeSys.CoreTest.Rework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.CoreTest.Monitor
{
    [TestClass]
    public class MonitorUploadCompanyTest : TestBase
    {

        int monitorMaintracknumber = 1;
        int monitorStatingNo = 8;

        /// <summary>
        /// 非返工衣架监测点衣架重新分配
        /// </summary>
        [TestMethod]
        public void TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            MonitorUpload(1, 8, hangerNo);
            MainThreadTools.Instance.SuspendThread();
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            
        //    return;

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            MonitorUpload(1, 8, hangerNo);
            MainThreadTools.Instance.SuspendThread();
            Thread.CurrentThread.Join(20000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            MonitorUpload(1, 8, hangerNo);
            MainThreadTools.Instance.SuspendThread();
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            var reu = new ReworkDecodeUploadUnitCompanyTest(log);
            reu.TestReworkDecodeUpload("2", "6", hangerNo, "4");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100",false);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101", false);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);


            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100", false);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101", false);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        //  MonitorUpload(1, 8, hangerNo);
        [TestMethod]
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow()
        {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow("806");
        }

        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
        [TestMethod]
        public void TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlowForRework(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            var reu = new ReworkDecodeUploadUnitCompanyTest(log);
            reu.TestReworkDecodeUpload("2", "6", hangerNo, "4");
            
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------监测点上传---------------------------------------");

            MonitorUpload(1, 8, hangerNo);
            MainThreadTools.Instance.SuspendThread();
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
           // Thread.CurrentThread.Join();

           

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------监测点上传---------------------------------------");

            MonitorUpload(1, 8, hangerNo);
            MainThreadTools.Instance.SuspendThread();
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join();

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);


            //A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //SearchProductRealtimeInfoTest("A");
            //SearchProductRealtimeInfoTest("B");
            //SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        //返工衣架监测点重新分配
        [TestMethod]
        public void TestTestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlowForRework() {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlowForRework("223");
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using SuspeSys.AuxiliaryTools;
using SuspeSys.CoreTest.Core;

namespace SuspeSys.CoreTest.F2
{
    [TestClass]
    public class F2UnitTest : TestBase
    {
        [TestMethod]
        public void ExecuseTestF2NonCrossMainTrack()
        {
            TestF2NonCrossMainTrack("55");
            TestF2Return();
        }
        string hangerNo = "55";
        [TestMethod]
        public void TestQueryGroupStatingData()
        {
          
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
        }
        public void TestGeneratorData(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


           


        }
        [TestMethod]
        public void TestF2NonCrossMainTrack(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            TestGeneratorData(hangerNo);

            int sourceMainTrackNumber = 2;
            int sourceStatingNo =5;
            int targetStatingNo = 4;
            F2NonCrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo, targetStatingNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void TestF2Return() {
            string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            TestF2ReturnOutStie();
        }
        [TestMethod]
        public void TestF2ReturnOutStie()
        {
            string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
    }
}

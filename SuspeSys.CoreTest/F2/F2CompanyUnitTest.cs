using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using SuspeSys.AuxiliaryTools;
using SuspeSys.CoreTest.Core;

namespace SuspeSys.CoreTest.F2
{
    [TestClass]
    public class F2CompanyUnitTest : TestBase
    {
        [TestMethod]
        public void ExecuseTestF2NonCrossMainTrack()
        {
            TestF2NonCrossMainTrack("55");
            TestF2NonCrossMainTrackF2Return();
        }

        [TestMethod]
        public void TestQueryGroupStatingData()
        {
            string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
        }
        public void TestF2NonCrossMainTrackGeneratorData(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "4");
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


        }
        [TestMethod]
        public void TestF2NonCrossMainTrack(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            TestF2NonCrossMainTrackGeneratorData(hangerNo);

            int sourceMainTrackNumber = 1;
            int sourceStatingNo = 7;
            int targetStatingNo = 6;
            F2NonCrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo, targetStatingNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);

            
        }
        [TestMethod]
        public void TestF2NonCrossMainTrackF2Return() {
            string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            TestF2NonCrossMainTrackF2ReturnOutStie();
        }
        [TestMethod]
        public void TestF2NonCrossMainTrackF2ReturnOutStie()
        {
            string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
    }
}

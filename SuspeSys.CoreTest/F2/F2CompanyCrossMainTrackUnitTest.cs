using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using SuspeSys.AuxiliaryTools;
using SuspeSys.CoreTest.Core;

namespace SuspeSys.CoreTest.F2
{
    [TestClass]
    public class F2CompanyCrossMainTrackUnitTest : TestBase
    {
        [TestMethod]
        public void ExecuseTestF2CrossMainTrack()
        {
            TestF2CrossMainTrack(hangerNo);
           TestF2Return();
        }
        string hangerNo = "88657";
        [TestMethod]
        public void TestQueryGroupStatingData()
        {
          
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
        }
        public void TestGeneratorData(string hangerNo)
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





        }
        [TestMethod]
        public void TestF2CrossMainTrack(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            TestGeneratorData(hangerNo);

            int sourceMainTrackNumber = 2;
            int sourceStatingNo =6;
            int targetMainTrack = 1;
            int targetStatingNo = 7;
            
            F2CrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo,targetMainTrack, targetStatingNo);
            Thread.CurrentThread.Join(20000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void TestF2Return() {
           // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            TestF2ReturnOutStie();
        }
        [TestMethod]
        public void TestF2ReturnOutStie()
        {
           // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 8);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("100");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("101");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
    }
}

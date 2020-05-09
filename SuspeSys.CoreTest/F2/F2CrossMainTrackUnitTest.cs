using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using SuspeSys.AuxiliaryTools;
using SuspeSys.CoreTest.Core;

namespace SuspeSys.CoreTest.F2
{
    [TestClass]
    public class F2CrossMainTrackUnitTest : TestBase
    {
        [TestMethod]
        public void ExecuseTestF2CrossMainTrack()
        {
            TestF2CrossMainTrack(hangerNo);
            TestF2Return();
        }
        string hangerNo = "1965";
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
        public void ExecuseTestF2CrossMainTrack2()
        {
            TestF2CrossMainTrack2(hangerNo);
            TestF2CrossMainTrack3(hangerNo);
            TestF2Return2();
        }
        public void TestGeneratorData2(string hangerNo)
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
        }
        [TestMethod]
        public void TestF2CrossMainTrack2(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
         TestGeneratorData2(hangerNo);

            int sourceMainTrackNumber =1;
            int sourceStatingNo = 5;
           // int targetMainTrack = 1;
            int targetStatingNo = 4;
           
            F2NonCrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo, targetStatingNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void TestF2CrossMainTrack3(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
         
            int sourceMainTrackNumber = 1;
            int sourceStatingNo = 4;
            int targetMainTrack = 2;
            int targetStatingNo = 5;
           
            F2CrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo, targetMainTrack, targetStatingNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void TestF2Return2()
        {
            // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            TestF2ReturnOutStie2();
        }
        [TestMethod]
        public void TestF2ReturnOutStie2()
        {
            // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
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

        [TestMethod]
        public void TestF2Return()
        {
            // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
            TestF2ReturnOutStie();
        }
        [TestMethod]
        public void TestF2CrossMainTrack(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            TestGeneratorData(hangerNo);

            int sourceMainTrackNumber = 2;
            int sourceStatingNo =5;
            int targetStatingNo = 2;
            int targetMainTrack = 1;
            F2CrossMainTracAssignHandlers(sourceMainTrackNumber, sourceStatingNo, hangerNo,targetMainTrack, targetStatingNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(20000);
        }
  
        [TestMethod]
        public void TestF2ReturnOutStie()
        {
           // string hangerNo = "55";
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);


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

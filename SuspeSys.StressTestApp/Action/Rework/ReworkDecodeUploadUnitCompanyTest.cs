using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Model;
using SusNet.Common.Utils;
using Sus.Net.Common.Utils;
using System.Collections.Generic;
using System.Threading;
using SuspeSys.Service.Impl.Core.Allocation;
using log4net;
using SuspeSys.StressTestApp.Base;

namespace SuspeSys.StressTestApp.Rework
{
    // [TestClass]
    public class ReworkDecodeUploadUnitCompanyTest : TestBase
    {

        const int beginAddress = 0x0090;
        const int reciprocalAddress = 0x0098;
        const int endAddress = 0x0099;
        public ReworkDecodeUploadUnitCompanyTest() { }
        public ReworkDecodeUploadUnitCompanyTest(ILog _log)
        {
            log = _log;
        }
        //  //
        public void TestReworkDecodeUpload()
        {
            //var dataWait = string.Format("{0} {1} 06 FF 00 55 {2} AA BB CC DD EE",hanger.MainTrackNo,hanger.StatingNo,hanger.ProductNumber);
            var flowOrDefectCodes = "0";
            var hanger = new Hanger();
            hanger.MainTrackNo = "2";
            hanger.StatingNo = "2";
            hanger.HangerNo = "1";

            var dataWait = string.Format("{0} {1} 06 FF 00 56 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
                 HexHelper.TenToHexString10Len(hanger.HangerNo));
            var data = HexHelper.StringToHexByte(dataWait);
            //susTCPServer.SendMessageToAll(data);
            client.SendData(data);

            var j = 0;
            var bytesFlowDefect = AssicUtils.EncodeByStr(flowOrDefectCodes);
            var times = bytesFlowDefect.Length % 6 == 0 ? bytesFlowDefect.Length / 6 : (bytesFlowDefect.Length / 6 + 1);
            var address = beginAddress;
            for (var t = 0; t < times; t++)
            {
                if (address > reciprocalAddress)
                {
                    break;
                }
                var dataList = new List<byte>();
                var header = string.Format("{0} {1} 06 FF {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), HexHelper.TenToHexString4Len(address));
                dataList.AddRange(HexHelper.StringToHexByte(header));
                if (j < bytesFlowDefect.Length)
                {
                    for (int b = j; j < bytesFlowDefect.Length; j++)
                    {
                        if (dataList.Count == 12)
                        {
                            break;
                        }
                        dataList.Add(bytesFlowDefect[j]);
                    }
                }
                var teLen = dataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (dataList.Count == 12)
                    {
                        break;
                    }
                    //数据位不足补FF
                    dataList.AddRange(AssicUtils.EncodeByStr("@"));
                }
                // susTCPServer.SendMessageToAll(dataList.ToArray());
                client.SendData(dataList.ToArray());
                address++;
            }
            var endHeaderMessage = string.Format("{0} {1} 06 FF 00 99", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
                 HexHelper.TenToHexString10Len(hanger.HangerNo));
            var endList = new List<byte>();
            endList.AddRange(HexHelper.StringToHexByte(endHeaderMessage));
            endList.AddRange(AssicUtils.EncodeByStr("000000"));
            // susTCPServer.SendMessageToAll(endList.ToArray());
            client.SendData(endList.ToArray());
            Thread.CurrentThread.Join(10000);
            log.InfoFormat("TestReworkDecodeUpload-->执行完成");
        }
        //  //
        public void BridgeSetOutSiteDoServiceTest_100()
        {
            var hangerNo = "1";
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
        ////
        public void ReworkHangerOutSiteDoServiceTest()
        {
            var hangerNo = "1";
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
            log.InfoFormat("BridgeSetOutSiteDoServiceTest_100-->执行完成");
        }
        ////
        public void ReworkHangerSourceBridgesetOutSiteDoServiceTest()
        {
            var hangerNo = "1";
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
        // //
        public void ReworkHangerSourceOutSiteDoServiceTest()
        {
            var hangerNo = "1";
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "2";
            hanger.StatingNo = "2";
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
        ////
        public void HangerSourceOutSiteDoServiceTest1_10()
        {
            var hangerNo = "1";
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
        // //
        public void HangerSourceOutSiteDoServiceTest1_6()
        {
            var hangerNo = "1";
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "6";
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
        // //
        public void HangerSourceOutSiteDoServiceTest1_7()
        {
            var hangerNo = "1";
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "6";
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

        public void TestReworkDecodeUpload(string mainTrackNumber, string statingNo, string hangerNo, string flowNoDefectCodes)
        {
            log.Info($"-----------------------【返工上传】 衣架:{hangerNo} 主轨:{mainTrackNumber} 站点:{statingNo} 疵点及工序号:{flowNoDefectCodes}---------------------------------------");
            var reworkThread = new Thread(() => ReworkDecodeUploadHandler(mainTrackNumber, statingNo, hangerNo, flowNoDefectCodes));
            //ReworkDecodeUploadHandler(mainTrackNumber, statingNo, hangerNo, flowNoDefectCodes);
            reworkThread.Start();
        }

        private static void ReworkDecodeUploadHandler(string mainTrackNumber, string statingNo, string hangerNo, string flowNoDefectCodes)
        {
            //var dataWait = string.Format("{0} {1} 06 FF 00 55 {2} AA BB CC DD EE",hanger.MainTrackNo,hanger.StatingNo,hanger.ProductNumber);
            //var flowOrDefectCodes = "0";
            var hanger = new Hanger();
            hanger.MainTrackNo = mainTrackNumber;// "2";
            hanger.StatingNo = statingNo;// "2";
            hanger.HangerNo = hangerNo;// "1";

            var dataWait = string.Format("{0} {1} 06 FF 00 56 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
                 HexHelper.TenToHexString10Len(hanger.HangerNo));
            var data = HexHelper.StringToHexByte(dataWait);
            //susTCPServer.SendMessageToAll(data);
            client.SendData(data);

            var j = 0;
            var bytesFlowDefect = AssicUtils.EncodeByStr(flowNoDefectCodes);
            var times = bytesFlowDefect.Length % 6 == 0 ? bytesFlowDefect.Length / 6 : (bytesFlowDefect.Length / 6 + 1);
            var address = beginAddress;
            for (var t = 0; t < times; t++)
            {
                if (address > reciprocalAddress)
                {
                    break;
                }
                var dataList = new List<byte>();
                var header = string.Format("{0} {1} 06 FF {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), HexHelper.TenToHexString4Len(address));
                dataList.AddRange(HexHelper.StringToHexByte(header));
                if (j < bytesFlowDefect.Length)
                {
                    for (int b = j; j < bytesFlowDefect.Length; j++)
                    {
                        if (dataList.Count == 12)
                        {
                            break;
                        }
                        dataList.Add(bytesFlowDefect[j]);
                    }
                }
                var teLen = dataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (dataList.Count == 12)
                    {
                        break;
                    }
                    //数据位不足补FF
                    dataList.AddRange(AssicUtils.EncodeByStr("@"));
                }
                // susTCPServer.SendMessageToAll(dataList.ToArray());
                client.SendData(dataList.ToArray());
                address++;
            }
            var endHeaderMessage = string.Format("{0} {1} 06 FF 00 99", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
                 HexHelper.TenToHexString10Len(hanger.HangerNo));
            var endList = new List<byte>();
            endList.AddRange(HexHelper.StringToHexByte(endHeaderMessage));
            endList.AddRange(AssicUtils.EncodeByStr("000000"));
            // susTCPServer.SendMessageToAll(endList.ToArray());
            client.SendData(endList.ToArray());
            Thread.CurrentThread.Join(5000);
            log.InfoFormat("TestReworkDecodeUpload-->执行完成");
        }

        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
    //    //
        public void TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow(string hangerNo)
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
            TestReworkDecodeUpload("2", "6", hangerNo, "4");
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


        //
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow()
        {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow("106");
        }
        //
        public void QueryHangerResume()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.SeachHangerInfo("1");
        }
        //
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow33()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
        }

        //
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow(object oHangerNo)
        {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow(oHangerNo + "");
        }

        //
        public void TestAllLoadAll()
        {
            for (var index = 1; index < 51; index++)
            {
                ParameterizedThreadStart ParameterizedThreadStart = new ParameterizedThreadStart(ExecuseTestPreareAllBridgeStatingNonCarryFlow);
                Thread thread = new Thread(ParameterizedThreadStart);
                // TestAllProcess(""+index);
                thread.Start(index + "");
            }
            Thread.CurrentThread.Join();
        }
        //
        public void ExecuseTestOnlyOneMainTrackNumberRework()
        {
            object oHangerNo = null;
            if (oHangerNo == null)
                oHangerNo = "126";
            TestOnlyOneMainTrackNumberRework(oHangerNo + "");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
        //
        public void TestOnlyOneMainTrackNumberRework(string hangerNo)
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

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 9);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);
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
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        //
        public void ExecuseTestOnlyOneMainTrackNumberRework2()
        {
            object oHangerNo = null;
            if (oHangerNo == null)
                oHangerNo = "225";
            TestOnlyOneMainTrackNumberRework2(oHangerNo + "");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
        //
        public void TestOnlyOneMainTrackNumberRework2(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }


        //
        public void ExecuseTestOnlyOneMainTrackNumberRework3()
        {
            object oHangerNo = null;
            if (oHangerNo == null)
                oHangerNo = "229";
            TestOnlyOneMainTrackNumberRework3(oHangerNo + "");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
        //
        public void TestOnlyOneMainTrackNumberRework3(string hangerNo)
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


        }
        //
        public void ReworkOutSite()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            string hangerNo = "128";


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------监测点上传---------------------------------------");

            MonitorUpload(1, 8, hangerNo);

            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(10000);
        }
        ////
        //public void BridgeSetOutSiteDoServiceTest_100()
        //{
        //    var hangerNo = "1";
        //    var hanger = new Hanger();
        //    var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
        //    hanger.MainTrackNo = "1";
        //    hanger.StatingNo = "10";
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
        //    log.InfoFormat("BridgeSetOutSiteDoServiceTest_100-->执行完成");
        //}
        //
        public void ExecuseTestOnlyOneMainTrackNumberReworkAndMonitor()
        {
            object oHangerNo = null;
            if (oHangerNo == null)
                oHangerNo = "2529";
            TestOnlyOneMainTrackNumberReworkAndMonitor(oHangerNo + "");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-9,2-2】 衣架停留在在2-6 ,并且返工到1-7
        /// </summary>
        //
        public void TestOnlyOneMainTrackNumberReworkAndMonitor(string hangerNo)
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

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.Sleep(20000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------监测点上传---------------------------------------");

            MonitorUpload(1, 8, hangerNo);

            Thread.Sleep(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", true, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", true, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);
            Thread.CurrentThread.Join(20000);
        }
        //
        public void ExecuseQueryStatingRealyTimeInfo()
        {
            QueryStatingRealyTimeInfo();
        }

        private void QueryStatingRealyTimeInfo()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", true, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", true, flowChartId);
            // outSiteAdapterTests.SeachHangerInfo("1531");
            outSiteAdapterTests.SeachHangerInfo("1531");
            Thread.CurrentThread.Join(5000);
        }
        //
        public void TestManyThreadOneMainTrackNumberCommonHanger()
        {
            var beginHanerNo = 1000;
            var eachHangerLength = beginHanerNo + 50;
            for (var index = beginHanerNo; index < eachHangerLength; index++)
            {
                var thread = new Thread(() => TestOnlyOneMainTrackNumbeCommonHanger(index + ""));
                thread.Start();
            }
            log.Info("_______________________TestManyThreadOneMainTrackNumberCommonHanger执行完成!_______________________________");
            //Thread.CurrentThread.Join();

        }
        public void TestManyThreadOneMainTrackNumberCommonHanger2()
        {
            var beginHanerNo = 2000;
            var eachHangerLength = beginHanerNo + 50;
            for (var index = beginHanerNo; index < eachHangerLength; index++)
            {
                var thread = new Thread(() => TestOnlyOneMainTrackNumbeCommonHanger(index + ""));
                thread.Start();
            }
            log.Info("_______________________TestManyThreadOneMainTrackNumberCommonHanger执行完成!_______________________________");
            //Thread.CurrentThread.Join();

        }
        public void TestManyThreadOneMainTrackNumberCommonHanger3()
        {
            var beginHanerNo = 3000;
            var eachHangerLength = beginHanerNo + 50;
            for (var index = beginHanerNo; index < eachHangerLength; index++)
            {
                var thread = new Thread(() => TestOnlyOneMainTrackNumbeCommonHanger(index + ""));
                thread.Start();
            }
            log.Info("_______________________TestManyThreadOneMainTrackNumberCommonHanger执行完成!_______________________________");
            //Thread.CurrentThread.Join();

        }
        public void TestManyThreadOneMainTrackNumberCommonHangerExt(int beginHanerNo, int increment)
        {
            // var beginHanerNo = 3000;
            var eachHangerLength = beginHanerNo + increment;
            for (var index = beginHanerNo; index < eachHangerLength; index++)
            {
                var thread = new Thread(() => TestOnlyOneMainTrackNumbeCommonHanger(index + ""));
                thread.Start();
            }
            log.Info("_______________________TestManyThreadOneMainTrackNumberCommonHanger执行完成!_______________________________");
            //Thread.CurrentThread.Join();

        }
        //
        public void ExecuseTestOnlyOneMainTrackNumbeCommonHanger()
        {
            object oHangerNo = null;
            if (oHangerNo == null)
                oHangerNo = "1531";
            TestOnlyOneMainTrackNumbeCommonHanger(oHangerNo + "");
        }
        private static readonly object lockObject = new object();
        string flowChartId = "39b76b82c50c4cb8a191d0be5a240ac6";
        //
        public void TestOnlyOneMainTrackNumbeCommonHanger(string hangerNo)
        {
            lock (lockObject)
            {
                log.Info($"----------------衣架号:{hangerNo}----线程Id:{Thread.CurrentThread.ManagedThreadId}---------------");
                var outSiteAdapterTests = new OutSiteAdapterTests(log);
                var group1 = "";
                var group2 = "";
                var isTrow = false;
                //hangerNo = "1";
                outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
               // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 2);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 2);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);


                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);


                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 3);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 3);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);


                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);


                //Thread.CurrentThread.Join(10000);
                //log.Info("-----------------------返工上传---------------------------------------");
                //TestReworkDecodeUpload("1", "4", hangerNo, "3");
                //Thread.Sleep(20000);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
                //outSiteAdapterTests.SeachHangerInfo(hangerNo);

                //log.Info("-----------------------监测点上传---------------------------------------");

                //MonitorUpload(1, 8, hangerNo);

                //Thread.Sleep(10000);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
                //outSiteAdapterTests.SeachHangerInfo(hangerNo);


                //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
                //Thread.CurrentThread.Join(10000);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("1");
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("2");
                //outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 4);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 4);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                // outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);



                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 5);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 5);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);
                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);

                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);


                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 6);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 6);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                // outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);



                outSiteAdapterTests.HangerInComeStatingTest(int.Parse(hangerNo), 1, 7);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //  outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.HangerCompareTest(int.Parse(hangerNo), 1, 7);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);

                outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
                Thread.CurrentThread.Join(5000);
                outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //  outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
                outSiteAdapterTests.SeachHangerInfo(hangerNo);
                Thread.CurrentThread.Join(5000);

                //isTrow = true;
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
                //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);

                log.Info($"-------------------【生产完成，衣架号:{hangerNo}】----------------------------------");
            }
        }
        //
        public void TestRealInfo()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            bool isTrow = true;
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
        }
        //
        public void TestMontiner()
        {
            MonitorUpload(1, 8, "1530");
            Thread.CurrentThread.Join(20000);
        }
    }
}

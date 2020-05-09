using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Model;
using SusNet.Common.Utils;
using Sus.Net.Common.Utils;
using System.Collections.Generic;
using System.Threading;
using SuspeSys.Service.Impl.Core.Allocation;

namespace SuspeSys.CoreTest.Rework
{
    [TestClass]
    public class ReworkDecodeUploadUnitTest : TestBase
    {
        const int beginAddress = 0x0090;
        const int reciprocalAddress = 0x0098;
        const int endAddress = 0x0099;
        [TestMethod]
        public void TestReworkDecodeUpload()
        {
            //var dataWait = string.Format("{0} {1} 06 FF 00 55 {2} AA BB CC DD EE",hanger.MainTrackNo,hanger.StatingNo,hanger.ProductNumber);
            var flowOrDefectCodes = "6";
            var hanger = new Hanger();
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "7";
            hanger.HangerNo = "2";

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
            Thread.CurrentThread.Join();
            log.InfoFormat("TestReworkDecodeUpload-->执行完成");
        }
        ///// <summary>
        ///// 返工上传
        ///// </summary>
        ///// <param name="mainTrackNumber">返工发起主轨</param>
        ///// <param name="statingNo">返工发起站点</param>
        ///// <param name="hangerNo"></param>
        ///// <param name="flowNoDefectCodes">返工的 工序号+疵点(格式xxx,yyyy;xxx1,yyy1)</param>
        //[TestMethod]
        //public void TestReworkDecodeUpload(string mainTrackNumber,string statingNo,string hangerNo,string flowNoDefectCodes)
        //{
        //    //var dataWait = string.Format("{0} {1} 06 FF 00 55 {2} AA BB CC DD EE",hanger.MainTrackNo,hanger.StatingNo,hanger.ProductNumber);
        //    var flowOrDefectCodes = flowNoDefectCodes;
        //    var hanger = new Hanger();
        //    hanger.MainTrackNo = mainTrackNumber;
        //    hanger.StatingNo = statingNo;
        //    hanger.HangerNo = hangerNo;

        //    var dataWait = string.Format("{0} {1} 06 FF 00 56 00 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
        //         HexHelper.TenToHexString10Len(hanger.HangerNo));
        //    var data = HexHelper.StringToHexByte(dataWait);
        //    //susTCPServer.SendMessageToAll(data);
        //    client.SendData(data);

        //    var j = 0;
        //    var bytesFlowDefect = AssicUtils.EncodeByStr(flowOrDefectCodes);
        //    var times = bytesFlowDefect.Length % 6 == 0 ? bytesFlowDefect.Length / 6 : (bytesFlowDefect.Length / 6 + 1);
        //    var address = beginAddress;
        //    for (var t = 0; t < times; t++)
        //    {
        //        if (address > reciprocalAddress)
        //        {
        //            break;
        //        }
        //        var dataList = new List<byte>();
        //        var header = string.Format("{0} {1} 06 FF {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), HexHelper.TenToHexString4Len(address));
        //        dataList.AddRange(HexHelper.StringToHexByte(header));
        //        if (j < bytesFlowDefect.Length)
        //        {
        //            for (int b = j; j < bytesFlowDefect.Length; j++)
        //            {
        //                if (dataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                dataList.Add(bytesFlowDefect[j]);
        //            }
        //        }
        //        var teLen = dataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (dataList.Count == 12)
        //            {
        //                break;
        //            }
        //            //数据位不足补FF
        //            dataList.AddRange(AssicUtils.EncodeByStr("@"));
        //        }
        //        // susTCPServer.SendMessageToAll(dataList.ToArray());
        //        client.SendData(dataList.ToArray());
        //        address++;
        //    }
        //    var endHeaderMessage = string.Format("{0} {1} 06 FF 00 99", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo),
        //         HexHelper.TenToHexString10Len(hanger.HangerNo));
        //    var endList = new List<byte>();
        //    endList.AddRange(HexHelper.StringToHexByte(endHeaderMessage));
        //    endList.AddRange(AssicUtils.EncodeByStr("000000"));
        //    // susTCPServer.SendMessageToAll(endList.ToArray());
        //    client.SendData(endList.ToArray());
        //    Thread.CurrentThread.Join(20000);
        //    log.InfoFormat("TestReworkDecodeUpload-->执行完成");
        //}



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

        [TestMethod]
        public void BridgeSetOutSiteDoServiceTest_100()
        {
            var hangerNo = "2";
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
            log.InfoFormat("BridgeSetOutSiteDoServiceTest_100-->执行完成");
        }
        [TestMethod]
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
        [TestMethod]
        public void ReworkHangerSourceBridgesetOutSiteDoServiceTest()
        {
            var hangerNo = "2";
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        /// <summary>
        /// 桥接都携带工序【桥接站1-10,2-11】 衣架停留在在2-8 ,并且返工到1-8
        /// </summary>
        [TestMethod]
        public void TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow(string hangerNo)
        {

            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "1");
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

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("2", "5", hangerNo, "2");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
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


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "5", hangerNo, "4");


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

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "5", hangerNo, "4");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");

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


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
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
            log.Info("-------------------------------------------【衣架生产完成】--------------------------------------------------------------------");
            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow()
        {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow("5347");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-10,2-11】 衣架停留在在2-8 ,并且返工到1-8
        /// </summary>
        [TestMethod]
        public void TestBridgeStatingNonCanRework(string hangerNo)
        {

            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "1");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "3", hangerNo, "2");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            ////outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,5);
            ////Thread.CurrentThread.Join(10000);
            ////outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            ////outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            ////outSiteAdapterTests.SeachHangerInfo(hangerNo);

            ////outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            ////Thread.CurrentThread.Join(10000);
            ////outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            ////outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            ////outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");


            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
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
            log.Info("-------------------------------------------【衣架生产完成】--------------------------------------------------------------------");
            Thread.CurrentThread.Join(20000);
        }
        [TestMethod]
        public void ExecuseTestBridgeStatingNonCanRework()
        {
            TestBridgeStatingNonCanRework("534117");
        }

        [TestMethod]
        public void ExecuseTestPreareAllBridgeStatingNonCarryFlow(object oHangerNo)
        {
            TestPreareAllBridgeStatingNonCarryFlowAndUploadReworkFlow(oHangerNo + "");
        }

        [TestMethod]
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
        [TestMethod]
        public void ExecuseTestOnlyOneMainTrackNumberByRework()
        {
            TestOnlyOneMainTrackNumberByRework("534120");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-10,2-11】 衣架停留在在2-8 ,并且返工到1-8
        /// </summary>
        [TestMethod]
        public void TestOnlyOneMainTrackNumberByRework(string hangerNo)
        {

            var isTrow = false;
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "1");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            ////Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------监测点上传---------------------------------------");

            //MonitorUpload(1, 8, hangerNo);

            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");


            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
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
            log.Info("-------------------------------------------【衣架生产完成】--------------------------------------------------------------------");
            Thread.CurrentThread.Join(20000);
        }


        [TestMethod]
        public void ExecuseTestOnlyOneMainTrackNumberByRework2()
        {
            TestOnlyOneMainTrackNumberByRework2("534121");
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-10,2-11】 衣架停留在在2-8 ,并且返工到1-8
        /// </summary>
        [TestMethod]
        public void TestOnlyOneMainTrackNumberByRework2(string hangerNo)
        {

            var flowChartId = "f0b60b398f724bcd8c9aee0d67c3603d";
            var isTrow = false;
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "1");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            ////Thread.CurrentThread.Join(10000);
            log.Info("-----------------------返工上传---------------------------------------");
            TestReworkDecodeUpload("1", "4", hangerNo, "3");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------监测点上传---------------------------------------");

            //MonitorUpload(1, 8, hangerNo);

            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");


            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
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
            log.Info("-------------------------------------------【衣架生产完成】--------------------------------------------------------------------");
            Thread.CurrentThread.Join(20000);
        }


        [TestMethod]
        public void TestRealInfo()
        {
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            bool isTrow = true;
            var flowChartId = "f0b60b398f724bcd8c9aee0d67c3603d";
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow,flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            //var beginHanerNo = 1000;
            //var hengerLeng = beginHanerNo + 20;
            //for (var index = beginHanerNo; index < hengerLeng; index++)
            //{
            //    outSiteAdapterTests.SeachHangerInfo(index + "");
            //}
            Thread.CurrentThread.Join(10000);
        }
        [TestMethod]
        public void TestManyThreadOneMainTrackNumberCommonHanger()
        {
            var beginHanerNo = 5000;
            var hengerLeng = beginHanerNo + 50;
            for (var index = beginHanerNo; index < hengerLeng; index++)
            {
                var thread = new Thread(() => TestOnlyOneMainTrackNumber(index + ""));
                thread.Start();
            }
            log.Info("_______________________TestManyThreadOneMainTrackNumberCommonHanger执行完成!_______________________________");
            Thread.CurrentThread.Join();
        }
        /// <summary>
        /// 桥接都携带工序【桥接站1-10,2-11】 衣架停留在在2-8 ,并且返工到1-8
        /// </summary>
        [TestMethod]
        public void TestOnlyOneMainTrackNumber(string hangerNo)
        {

            var flowChartId = "f0b60b398f724bcd8c9aee0d67c3603d";
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            var isTrow = false;
            var outSiteAdapterTests = new OutSiteAdapterTests(log);
            //hangerNo = "1";
            outSiteAdapterTests.HangingPieceOutSiteDoServiceTest(hangerNo, "1");
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 2);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            ////Thread.CurrentThread.Join(10000);
            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "4", hangerNo, "3");
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------监测点上传---------------------------------------");

            //MonitorUpload(1, 8, hangerNo);

            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow);
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 4);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);

            outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            Thread.CurrentThread.Join(10000);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("A", isTrow, flowChartId);
            outSiteAdapterTests.SearchProductRealtimeInfoTest("B", isTrow, flowChartId);
            outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1,5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");


            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //log.Info("-----------------------返工上传---------------------------------------");
            //TestReworkDecodeUpload("1", "5", hangerNo, "4");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 4);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 2, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 3);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 5);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);


            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 10);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 7);
            //Thread.CurrentThread.Join(10000);
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("A");
            //outSiteAdapterTests.SearchProductRealtimeInfoTest("B");
            //outSiteAdapterTests.SeachHangerInfo(hangerNo);

            //outSiteAdapterTests.A_CommonStatingOutSiteDoServiceTest(hangerNo, 1, 6);
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
            log.Info("-------------------------------------------【衣架生产完成】--------------------------------------------------------------------");
            Thread.CurrentThread.Join(20000);
        }

        [TestMethod]
        public void ExecuseTestOnlyOneMainTrackNumber()
        {
            TestOnlyOneMainTrackNumber("534118");
        }
    }
}

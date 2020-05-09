using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using log4net.Config;
using log4net;
using Sus.Net.Client;
using Sus.Net.Common.Entity;
using Sus.Net.Client.Sockets;
using Sus.Net.Common.Event;
using System.Threading;
using SusNet.Common.Utils;
using SusNet.Common.Message;
using SuspeSys.AuxiliaryTools;
using Sus.Net.Common.Constant;
using SuspeSys.Remoting;

namespace SuspeSys.CoreTest
{
    [TestClass()]
    public class TestBase
    {
        public static ILog log = null;
        public static bool isOpenTcp = true;
        [TestInitialize]
        public void Init()
        {
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            log = LogManager.GetLogger(typeof(TestBase));
            log.Info("info---->hibernate init....");

            Console.WriteLine("Test Init....");

            //跟踪nhibernate执行的sql配置
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            //


            SusBootstrap.Instance.Start(null, false, null, true);
            if (isOpenTcp)
            {
                InitClient();
                SuspeSys.Service.Impl.SusRedis.SusRedisClient.Instance.Init();
            }

        }
        public static SusTCPClient client = null;
        private void InitClient()
        {

            ClientUserInfo info = new ClientUserInfo("1", "2");
            string ipAddress = "127.0.0.1";
            string port = "9998";
            client = new SusTCPClient(info, ipAddress, int.Parse(port));
            client.MessageReceived += Server_MessageReceived;

            client.ServerConnected += Client_ServerConnected;
            client.ServerDisconnected += Client_ServerDisconnected;
            client.ServerExceptionOccurred += Client_ServerExceptionOccurred;
            // client.InitProductsCMD();
            client.Connect();
            ToolsBase.isUnitTest = true;
            ToolsBase.client = client;
            log.Info("线程休眠10秒开始...");
            Thread.Sleep(10000);
            log.Info("线程休眠10秒结束...");
            MainThreadTools.Instance.SetLog(log);
            MainThreadTools.Instance.SetCurrentThread(Thread.CurrentThread);

        }
        private void Client_ServerExceptionOccurred(object sender, TcpServerExceptionOccurredEventArgs e)
        {
            log.Info("连接异常--->" + e?.Exception?.Message);
        }

        private void Client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            log.Info("已连接到服务器--->" + e?.Addresses);

        }

        private void Client_ServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            log.Info("已断开连接--->" + e?.Addresses);
            InitBasic("01");
        }

        private static object lockObj = new object();
        private void Server_MessageReceived(object sender, MessageEventArgs e)
        {
            log.Info("收到消息---->" + e?.message);
            lock (lockObj)
            {
                HandlerMessage(e);
            }
        }
        const string dateFormat = "dd HH:mm:ss:ffff";
        private void HandlerMessage(MessageEventArgs e)
        {
            if (ToolsBase.isUnitTest)
            {
                var byteData = HexHelper.StringToHexByte(e.message);
                var messageBody = new MessageBody(byteData);
                if (messageBody.CMD.Equals(SuspeConstants.cmd_Site_Allocation_Req) && (messageBody.ADDH + messageBody.ADDL).Equals(SuspeConstants.address_Site_Allocation_Hanger))
                {
                    var hangerModel = ToolsBase.ParseMessage(e.message);
                    //if (!ToolsBase.AllocationHanger.ContainsKey(hangerModel.HangerNo.ToString())) {
                    //    ToolsBase.AllocationHanger.TryAdd(hangerModel.HangerNo.ToString(), hangerModel);
                    //}
                    //ToolsBase.AllocationHanger.TryUpdate(hangerModel.HangerNo.ToString(), hangerModel, hangerModel);
                    ToolsBase.AllocationHanger.AddOrUpdate(hangerModel.HangerNo.ToString(), (f) => hangerModel, (z, vv) => hangerModel);
                    Thread.Sleep(1000);
                    HangerAllocationResponse(e, messageBody);
                }

            }
        }

        private static void HangerAllocationResponse(MessageEventArgs e, MessageBody messageBody)
        {
            var hangerModel = ToolsBase.ParseMessage(e.message);
            log.Info(string.Format("【分配工序到衣架成功回应】服务器端收到【客户端】消息:{0} 时间:{1}", e.message, DateTime.Now.ToString(dateFormat)));

            var startmtResponseMessageLog = string.Format("{0} {1} 04 FF 00 51 00 {2}", messageBody.XID, messageBody.ID, hangerModel.HangerNo);//AA BB CC DD EE";

            var startmtResponseMessage = string.Format("{0} {1} 04 FF 00 51 00 {2}", messageBody.XID, messageBody.ID, HexHelper.TenToHexString10Len(hangerModel.HangerNo.Value.ToString()));//AA BB CC DD EE";
            log.Info(string.Format("【分配工序到衣架成功回应】---->服务器端发送开始--->客户端消息为:【{0}】 时间:{1}", startmtResponseMessageLog, DateTime.Now.ToString(dateFormat)));
            //server.SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
            client.SendData(HexHelper.StringToHexByte(startmtResponseMessage));
            log.Info(string.Format("【分配工序到衣架成功回应】---->服务器端发送完成--->客户端消息为:【{0}】 时间:{1}", startmtResponseMessageLog, DateTime.Now.ToString(dateFormat)));
        }

        void InitBasic(string mainTrackNumber)
        {

            //var _mainTrackNumber = int.Parse(mainTrackNumber);
            //if (_mainTrackNumber == 1)
            //{
            //    var hexMessage1 = string.Format("{0} 00 06 FF 00 02 00 00 00 00 00 00", HexHelper.TenToHexString2Len(_mainTrackNumber));
            //    log.InfoFormat("初始化指令发送开始---->{0}", hexMessage1);
            //    var data1 = HexHelper.StringToHexByte(hexMessage1);
            //    MessageBody message1 = new MessageBody(data1);
            //    client.SendData(message1.GetBytes());
            //    log.InfoFormat("初始化指令发送完成---->{0}", hexMessage1);



            //    var hexMessage3 = string.Format("{0} 00 06 FF 00 02 00 00 00 00 00 00", HexHelper.TenToHexString2Len("2"));
            //    log.InfoFormat("初始化指令发送开始---->{0}", hexMessage3);
            //    var data3 = HexHelper.StringToHexByte(hexMessage3);
            //    MessageBody message3 = new MessageBody(data3);
            //    client.SendData(message3.GetBytes());
            //    log.InfoFormat("初始化指令发送完成---->{0}", hexMessage3);

            //    return;
            //}
            //mainTrackNumber = "2";
            //var hexMessage = string.Format("{0} 00 06 FF 00 02 00 00 00 00 00 00", HexHelper.TenToHexString2Len(mainTrackNumber));
            //log.InfoFormat("初始化指令发送开始---->{0}", hexMessage);
            //var data = HexHelper.StringToHexByte(hexMessage);
            //MessageBody message = new MessageBody(data);
            //client.SendData(message.GetBytes());
            //log.InfoFormat("初始化指令发送完成---->{0}", hexMessage);

            var _mainTrackNumber = int.Parse(mainTrackNumber);
            if (_mainTrackNumber == 1)
            {
                var hexMessage1 = string.Format("{0} 02 02 FF 00 1C 00 00 00 00 00 00", HexHelper.TenToHexString2Len(_mainTrackNumber));
                log.InfoFormat("初始化指令发送开始---->{0}", hexMessage1);
                var data1 = HexHelper.StringToHexByte(hexMessage1);
                MessageBody message1 = new MessageBody(data1);
                client.SendData(message1.GetBytes());
                log.InfoFormat("初始化指令发送完成---->{0}", hexMessage1);



                var hexMessage3 = string.Format("{0} 02 02 FF 00 1C 00 00 00 00 00 00", HexHelper.TenToHexString2Len("2"));
                log.InfoFormat("初始化指令发送开始---->{0}", hexMessage3);
                var data3 = HexHelper.StringToHexByte(hexMessage3);
                MessageBody message3 = new MessageBody(data3);
                client.SendData(message3.GetBytes());
                log.InfoFormat("初始化指令发送完成---->{0}", hexMessage3);

                return;
            }
            mainTrackNumber = "2";
            var hexMessage = string.Format("{0} 00 06 FF 00 02 00 00 00 00 00 00", HexHelper.TenToHexString2Len(mainTrackNumber));
            log.InfoFormat("初始化指令发送开始---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("初始化指令发送完成---->{0}", hexMessage);
        }
        public void MonitorUpload(int mainTrackNumber, int statingNo, string hangerNo)
        {
            log.Info($"-----------------------【监测点上传】 衣架:{hangerNo} 主轨:{mainTrackNumber} 站点:{statingNo}---------------------------------------");
            var monitorThread = new Thread(()=> MonitorUploadHanler(mainTrackNumber,statingNo,hangerNo));
            //MonitorUploadHanler(mainTrackNumber, statingNo, hangerNo);
            monitorThread.Start();
        }

        private static void MonitorUploadHanler(int mainTrackNumber, int statingNo, string hangerNo)
        {
            //  var mainTrackNumber = txtMainTrackNumber.Text;
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            //var statingNo = txtStatingNo.Text;

            var dataWait = string.Format("{0} {1} 02 FF 00 06 {2} {3}", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo), "00", hexHangerNo);
            var data = HexHelper.StringToHexByte(dataWait);
            client.SendData(data);
            log.Info($"【监测点读卡】");
        }

        public void F2CrossMainTracAssignHandlers(int sourceMainTrackNumber, int sourceStatingNo, string hangerNo, int targetMainTrack, int targetStatingNo)
        {
            //  var mainTrackNumber = txtMainTrackNumber.Text;
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            //var statingNo = txtStatingNo.Text;

            var dataWaitUploadHangerNo = string.Format($"{ HexHelper.TenToHexString2Len(sourceMainTrackNumber)} { HexHelper.TenToHexString2Len(sourceStatingNo)} {SuspeConstants.cmd_F2_Assign_Req} FF {SuspeConstants.address_F2_Upload_HangerNO_Assign} 00 {hexHangerNo}");
            var data = HexHelper.StringToHexByte(dataWaitUploadHangerNo);
            client.SendData(data);

            log.Info($"【F2跨主轨】衣架号上传中....");
            Thread.CurrentThread.Join(1000);

            var dataWait = string.Format($"{ HexHelper.TenToHexString2Len(sourceMainTrackNumber)} { HexHelper.TenToHexString2Len(sourceStatingNo)} {SuspeConstants.cmd_F2_Assign_Req} FF {SuspeConstants.address_F2_Assign_Action}00000000{HexHelper.TenToHexString2Len(targetMainTrack)}{HexHelper.TenToHexString2Len(targetStatingNo)}");
            data = HexHelper.StringToHexByte(dataWait);
            client.SendData(data);
            log.Info($"【F2不跨主轨】");
        }
        public void F2NonCrossMainTracAssignHandlers(int sourceMainTrackNumber, int sourceStatingNo, string hangerNo, int targetStatingNo)
        {
            //  var mainTrackNumber = txtMainTrackNumber.Text;
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            //var statingNo = txtStatingNo.Text;

            var dataWaitUploadHangerNo = string.Format($"{ HexHelper.TenToHexString2Len(sourceMainTrackNumber)} { HexHelper.TenToHexString2Len(sourceStatingNo)} {SuspeConstants.cmd_F2_Assign_Req} FF {SuspeConstants.address_F2_Upload_HangerNO_Assign} 00 {hexHangerNo}");
            var data = HexHelper.StringToHexByte(dataWaitUploadHangerNo);
            client.SendData(data);

            log.Info($"【F2不跨主轨】衣架号上传中....");
            Thread.CurrentThread.Join(1000);

            var dataWait = string.Format($"{ HexHelper.TenToHexString2Len(sourceMainTrackNumber)} { HexHelper.TenToHexString2Len(sourceStatingNo)} {SuspeConstants.cmd_F2_Assign_Req} FF {SuspeConstants.address_F2_Assign_Action} 0000000000{HexHelper.TenToHexString2Len(targetStatingNo)}");
            data = HexHelper.StringToHexByte(dataWait);
            client.SendData(data);
            log.Info($"【F2不跨主轨】");
        }
    }
}

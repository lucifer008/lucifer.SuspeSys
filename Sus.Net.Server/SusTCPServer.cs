using log4net;
using Sus.Net.Common.Common;
using Sus.Net.Common.Constant;
using Sus.Net.Common.Event;
using Sus.Net.Common.Message;
using Sus.Net.Common.SusBusMessage;
using Sus.Net.Common.Utils;
using Sus.Net.Server.Sockets;
using SusNet.Common.Message;
using SusNet.Common.SusBusMessage;
using SusNet.Common.Utils;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Sus.Net.Server
{
    public class SusTCPServer : SusLog
    {
        private ILog log = LogManager.GetLogger(typeof(SusTCPServer));
        private readonly AsyncTcpServer server;
        /// <summary>
        /// 接收到消息
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        //public event EventHandler<MessageEventArgs> HangingPieceBindProcessMessageReceived;

        //public event EventHandler<MessageEventArgs> ProductsDirectOnlineMessageReceived;
        public event EventHandler<TcpClientConnectedEventArgs> ClientConnected;
        public event EventHandler<TcpClientDisconnectedEventArgs> ClientDisconnected;

        public static Dictionary<string, StatingInfo> DictStatingInfo = new Dictionary<string, StatingInfo>();

        #region 业务事件
        /// <summary>
        /// 衣架进站：终端向pc的通知消息时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangerArrivalStatingMessageReceived;

        /// <summary>
        /// 启动主轨响应【终端向pc通知触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> StartMainTrackResponseMessageReceived;

        /// <summary>
        /// 停止主轨响应【终端向pc通知时触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> StopMainTrackResponseMessageReceived;

        /// <summary>
        /// 急停主轨响应【终端向pc通知时触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> EmergencyStopMainTrackResponseMessageReceived;

        /// <summary>
        /// 硬件按【暂停键】终端向pc发送请求时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> StopReceiveHangerRequestMessageReceived;

        /// <summary>
        /// 衣架落入读卡器发送的请求，硬件发pc端时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangerDropCardRequestMessageReceived;

        /// <summary>
        /// 衣架出站，硬件发pc端时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangerOutStatingRequestMessageReceived;
        /// <summary>
        /// 【协议2.0】 挂片站上传衣架号信息事件
        /// </summary>

        public event EventHandler<MessageEventArgs> HangingPieceHangerUploadRequestMessageReceived;

        /// <summary>
        /// 软件给硬件分配工序到衣架成功时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> AllocationHangerResponseMessageReceived;
        /// <summary>
        /// 制品界面上线来自硬件的响应
        /// </summary>
        public event EventHandler<MessageEventArgs> ClientMachineResponseMessageReceived;

        /// <summary>
        /// 挂片站上线时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangingPieceStatingOnlineMessageReceived;

        /// <summary>
        /// 衣架返工时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangerReworkMessageReceived;
        /// <summary>
        /// 衣架返工收到疵点代码时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> ReworkDefectMessageReceived;

        /// <summary>
        /// 卡片请求时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> CardRequestMessageReceived;

        /// <summary>
        /// 衣架在监测点读卡时触发消息
        /// </summary>
        public event EventHandler<MessageEventArgs> MonitorMessageReceived;

        /// <summary>
        /// 衣架进站时读卡发现满站触发
        /// </summary>
        public event EventHandler<MessageEventArgs> FullSiteMessageReceived;

        /// <summary>
        /// 清除衣架缓存 硬件的响应
        /// </summary>
        public event EventHandler<MessageEventArgs> ClearHangerCacheResponseMessageReceived;

        /// <summary>
        /// 返工疵点代码
        /// </summary>
        public event EventHandler<MessageEventArgs> ReworkFlowDefectRequestMessageReceived;

        /// <summary>
        /// 修改站点容量
        /// </summary>
        public event EventHandler<MessageEventArgs> StatingCapacityResponseMessageReceived;

        /// <summary>
        /// 修改或者新增站点类型
        /// </summary>
        public event EventHandler<MessageEventArgs> StatingTypeResponseMessageReceived;

        /// <summary>
        /// 上电初始化
        /// </summary>

        public event EventHandler<MessageEventArgs> PowerSupplyInitMessageReceived;
        /// <summary>
        /// 主板版本
        /// </summary>
        public event EventHandler<MessageEventArgs> MainboardVersionMessageReceived;
        /// <summary>
        /// SN序列号
        /// </summary>
        public event EventHandler<MessageEventArgs> SNSerialNumberMessageReceived;

        /// <summary>
        /// 下位机暂停或接收衣架
        /// </summary>
        public event EventHandler<MessageEventArgs> LowerMachineSuspendOrReceiveMessageReceived;

      
        /// <summary>
        /// 上位机发起的上电初始化请求后的下位机的响应
        /// </summary>
        public event EventHandler<MessageEventArgs> UpperComputerInitResponseMessageReceived;

        /// <summary>
        /// 上位机发起满站查询修正
        /// </summary>
        public event EventHandler<MessageEventArgs> FullSiteQueryResponseMessageReceived;
        #endregion

        public SusTCPServer(int port)
        {
            server = new AsyncTcpServer(port) { Encoding = Encoding.UTF8 };
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectedEventArgs>(server_ClientDisconnected);
           
            //server.PlaintextReceived += new EventHandler<TcpDatagramReceivedEventArgs<string>>(server_PlaintextReceived);
           // server.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(server_DatagramReceived);
          //  MessageFactory.Instance.Init(new ClientUserInfo("0", "0"));
        }
      
        //private void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        //{
        //    //throw new NotImplementedException();
        //    try
        //    {
        //        //Log.Info("【客户端:{0}】收到消息:【服务器: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),BufferUtils.ByteToHexStr(e.Datagram));
        //        tcpLogInfo.Info(string.Format("【服务端:{0}】收到消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), BufferUtils.ByteToHexStr(e.Datagram)));
        //        tcpLogHardware.Info(string.Format("【服务端:{0}】收到消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), BufferUtils.ByteToHexStr(e.Datagram)));
        //        List<SusNet.Common.Message.MessageBody> messageList = SusNet.Common.SusBusMessage.MessageProcesser.Instance.ProcessRecvData(e.TcpClient.Client.RemoteEndPoint.ToString(), e.Datagram);
        //        foreach (var rMessage in messageList)
        //        {
        //            tcpLogInfo.Info(string.Format("【服务端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), rMessage.GetHexStr()));
        //            tcpLogHardware.Info(string.Format("【服务端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), rMessage.GetHexStr()));

        //            if (MessageReceived != null)
        //            {
        //                MessageEventArgs args = new MessageEventArgs(e.TcpClient,rMessage.GetHexStr());
        //                try
        //                {
        //                    MessageReceived(this, args);
        //                }
        //                catch (Exception ex)
        //                {
        //                    //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
        //                  //  tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //                    tcpLogError.Error(ex);
        //                }
        //            }
        //            #region 业务事件
        //            //【启动主轨后响应】
        //            var startmtMessage = SusNet.Common.SusBusMessage.StartMainTrackResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != StartMainTrackResponseMessageReceived && null != startmtMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【启动主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【启动主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = startmtMessage;
        //                StartMainTrackResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【停止主轨后响应】
        //            var smtMessage = SusNet.Common.SusBusMessage.StopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != StopMainTrackResponseMessageReceived && null != smtMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【停止主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【停止主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = smtMessage;
        //                StopMainTrackResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【急停主轨后响应】
        //            var esmtMessage = SusNet.Common.SusBusMessage.EmergencyStopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != EmergencyStopMainTrackResponseMessageReceived && null != esmtMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【急停主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【急停主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = esmtMessage;
        //                EmergencyStopMainTrackResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【【停止接收衣架】终端按下【暂停键时】硬件通知pc】
        //            var srhMessage = SusNet.Common.SusBusMessage.StopReceiveHangerRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != StopReceiveHangerRequestMessageReceived && null != srhMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = srhMessage;
        //                StopReceiveHangerRequestMessageReceived(this, args);
        //                continue;
        //            }

        //            //【衣架落入读卡器发送的请求，硬件发pc端时触发】
        //            var hdcMessage = SusNet.Common.SusBusMessage.HangerDropCardRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != HangerDropCardRequestMessageReceived && null != hdcMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = hdcMessage;
        //                HangerDropCardRequestMessageReceived(this, args);
        //                continue;
        //            }

        //            //【衣架进站】
        //            var hasMessage = SusNet.Common.SusBusMessage.HangerArrivalStatingRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != HangerArrivalStatingMessageReceived && null != hasMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【衣架进站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【衣架进站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = hasMessage;
        //                HangerArrivalStatingMessageReceived(this, args);
        //                continue;
        //            }
        //            //【衣架出站请求】
        //            var hoMessage = SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != HangerOutStatingRequestMessageReceived && null != hoMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【衣架出站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【衣架出站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = hoMessage;
        //                HangerOutStatingRequestMessageReceived(this, args);
        //                continue;
        //            }
        //            ////【协议2.0---->挂片站衣架信息上传请求】
        //            //var hpsUploadHangerInfoMessage = HangingPieceHangerUploadRequestMessage.isEqual(rMessage.GetBytes());
        //            //if (null != HangingPieceHangerUploadRequestMessageReceived && null != hpsUploadHangerInfoMessage)
        //            //{
        //            //    tcpLogInfo.Info(string.Format("【>挂片站衣架信息上传请求】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //            //    MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //            //    args.Tag = hpsUploadHangerInfoMessage;
        //            //    HangingPieceHangerUploadRequestMessageReceived(this, args);
        //            //    continue;
        //            //}
        //            //【分配工序到衣架成功回应】
        //            var allMessage = SusNet.Common.SusBusMessage.AllocationHangerResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != AllocationHangerResponseMessageReceived && null != allMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【分配工序到衣架成功回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【分配工序到衣架成功回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = allMessage;
        //                AllocationHangerResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【制品界面上线】
        //            var cmsResMessage = SusNet.Common.SusBusMessage.ClientMachineResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != ClientMachineResponseMessageReceived && null != cmsResMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【制品界面上线来自硬件的响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【制品界面上线来自硬件的响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = cmsResMessage;
        //                ClientMachineResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【挂片站上线】
        //            var hpsOnlineResMessage = SusNet.Common.SusBusMessage.HangingPieceStatingOnlineRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != HangingPieceStatingOnlineMessageReceived && null != hpsOnlineResMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【挂片站上线】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【挂片站上线】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = hpsOnlineResMessage;
        //                HangingPieceStatingOnlineMessageReceived(this, args);
        //                continue;
        //            }
        //            //【衣架返工】
        //            var hangerReworkRequestMessage = SusNet.Common.SusBusMessage.ReworkRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != HangerReworkMessageReceived && null != hangerReworkRequestMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【衣架返工】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【衣架返工】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = hangerReworkRequestMessage;
        //                HangerReworkMessageReceived(this, args);
        //                continue;
        //            }
        //            //【衣架返工工序及疵点代码】
        //            var reworkFlowDefectRequestMessage = ReworkFlowDefectRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != reworkFlowDefectRequestMessage && null != ReworkFlowDefectRequestMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【衣架返工工序及疵点代码】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = reworkFlowDefectRequestMessage;
        //                ReworkFlowDefectRequestMessageReceived(this, args);
        //                continue;
        //            }
        //            //【卡片相关】
        //            var cardRequestMessage = CardRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != cardRequestMessage && null != CardRequestMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【卡片相关】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【卡片相关】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = cardRequestMessage;
        //                CardRequestMessageReceived(this, args);
        //                continue;
        //            }
        //            //【清除衣架缓存】
        //            var cHangerCacheResponse = ClearHangerCacheResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != cHangerCacheResponse && null != ClearHangerCacheResponseMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【清除衣架缓存】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【清除衣架缓存】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = cHangerCacheResponse;
        //                ClearHangerCacheResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //【监测点上传】
        //            var monitorMessage = MonitorMessage.isEqual(rMessage.GetBytes());
        //            if (null != monitorMessage && null != MonitorMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【监测点上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【监测点上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = monitorMessage;
        //                MonitorMessageReceived(this, args);
        //                continue;
        //            }
        //            //【满站上传】
        //            var fullSiteMessage = FullSiteMessage.isEqual(rMessage.GetBytes());
        //            if (null != fullSiteMessage && null != FullSiteMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【满站上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【满站上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = fullSiteMessage;
        //                FullSiteMessageReceived(this, args);
        //                continue;
        //            }

        //            //【修改站点容量】
        //            var statingCapacityResponseMessage = StatingCapacityResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != statingCapacityResponseMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【修改站点容量】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【修改站点容量】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = statingCapacityResponseMessage;
        //                StatingCapacityResponseMessageReceived(this, args);
        //                continue;
        //            }

        //            //【修改站点类型】
        //            var statingTypeResponseMessage = StatingTypeResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != statingCapacityResponseMessage)
        //            {
        //                tcpLogInfo.Info(string.Format("【修改站点类型】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【修改站点类型】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = statingTypeResponseMessage;
        //                StatingTypeResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //上电初始化
        //            //PowerSupplyInitMessageReceived
        //            var powerSupplyInitRequestMessage = PowerSupplyInitRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != powerSupplyInitRequestMessage && null != PowerSupplyInitMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【上电初始化】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【上电初始化】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = powerSupplyInitRequestMessage;
        //                PowerSupplyInitMessageReceived(this, args);
        //                continue;
        //            }
        //            //SN序列号
        //            var sNSerialNumberRequestMessage = SNSerialNumberRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != sNSerialNumberRequestMessage && null != SNSerialNumberMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【SN上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【SN上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = sNSerialNumberRequestMessage;
        //                SNSerialNumberMessageReceived(this, args);
        //                continue;
        //            }
        //            //主版版本号
        //            var mainboardVersionRequestMessage = MainboardVersionRequestMessage.isEqual(rMessage.GetBytes());
        //            if (null != mainboardVersionRequestMessage && null != MainboardVersionMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【主版版本号上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【主版版本号上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = mainboardVersionRequestMessage;
        //                MainboardVersionMessageReceived(this, args);
        //                continue;
        //            }

        //            //下位机接收或暂停衣架
        //            var lowerMachineSuspendOrReceiveMessage = LowerMachineSuspendOrReceiveMessage.isEqual(rMessage.GetBytes());
        //            if (null != lowerMachineSuspendOrReceiveMessage && null != LowerMachineSuspendOrReceiveMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【下位机接收或暂停衣架上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【下位机接收或暂停衣架上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = lowerMachineSuspendOrReceiveMessage;
        //                LowerMachineSuspendOrReceiveMessageReceived(this, args);
        //                continue;
        //            }
        //            //上位机发起的上电初始化，硬件回应
        //            var upperComputerInitResponseMessage = UpperComputerInitResponseMessage.isEqual(rMessage.GetBytes());
        //            if (null != upperComputerInitResponseMessage && null != UpperComputerInitResponseMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【上位机发起的上电初始化，硬件回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【上位机发起的上电初始化，硬件回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = upperComputerInitResponseMessage;
        //                UpperComputerInitResponseMessageReceived(this, args);
        //                continue;
        //            }

        //            //【满站查询状态上传】
        //            var fullSiteQueryResponseMessage = FullSiteMessage.isFullSiteQueryEqual(rMessage.GetBytes());
        //            if (null != fullSiteQueryResponseMessage && null != FullSiteQueryResponseMessageReceived)
        //            {
        //                tcpLogInfo.Info(string.Format("【满站查询状态上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
        //                tcpLogHardware.Info(string.Format("【满站查询状态上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = fullSiteQueryResponseMessage;
        //                FullSiteQueryResponseMessageReceived(this, args);
        //                continue;
        //            }
        //            //UpperComputerInitResponseMessageReceived

        //            //if ( rMessage.body != null )
        //            //{
        //            //    //Log.Info("【客户端:{0}】收到消息完整解析消息:【服务器: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),rMessage.Describe());
        //            //    Log.Info("【客户端:{0}】收到消息完整解析消息:【服务器: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(rMessage.body));
        //            //    MessageBody messageBody = JsonConvert.DeserializeObject<MessageBody>(Encoding.UTF8.GetString(rMessage.body));
        //            //    if ( messageBody != null )
        //            //    {
        //            //        Log.Info("【客户端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),messageBody.Describe());
        //            //        switch ( rMessage.type )
        //            //        {
        //            //            case MessageType.ACK:
        //            //                break;
        //            //            case MessageType.Heartbeat:
        //            //                Ack(messageBody);
        //            //                break;
        //            //            case MessageType.Common:
        //            //                if ( MessageReceived != null )
        //            //                {
        //            //                    MessageEventArgs args = new MessageEventArgs(messageBody.DATA);
        //            //                    try
        //            //                    {
        //            //                        MessageReceived(this,args);
        //            //                    }
        //            //                    catch ( Exception ex )
        //            //                    {
        //            //                        Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //            //                    }
        //            //                }
        //            //                Ack(messageBody);
        //            //                break;
        //            //            default:
        //            //                break;
        //            //        }
        //            //    }
        //            //}
        //            #endregion

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        tcpLogError.Error(ex);
        //        tcpLogHardware.Error(ex);
        //    }
        //}


  
        //启动
        public void Start()
        {
            //InitProductsCMD();
            server.Start();
            //Log.Info("服务器 {0} 已经启动",server.ToString());
            log.Info(string.Format("服务器 {0} 已经启动", server.ToString()));
        }

        public void Stop()
        {
            server.Stop();
            ClientManager.Instance.Clear();
            //Log.Info("服务器 {0} 已经停止",server.ToString());
            log.Info(string.Format("服务器 {0} 已经停止", server.ToString()));
        }

        public void SendMessageToAll(string strMessage)
        {
            try
            {
                Message message = MessageFactory.Instance.CreateMessage(strMessage);
                // Log.Info("已经给所有终端发送消息:{0}",message.Describe());
                log.Info(string.Format("已经给所有终端发送消息:{0}", message.Describe()));
                server.SendToAll(message.Encode());
            }
            catch (Exception ex)
            {
                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                log.Error(ex);
            }
        }
        public void SendMessageToAll(byte[] data)
        {
            try
            {
                //Message message = MessageFactory.Instance.CreateMessage(strMessage);
                // Log.Info("已经给所有终端发送消息:{0}",message.Describe());
                log.Info(string.Format("已经给所有终端发送消息:{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(data)));
                server.SendToAll(data);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                log.Error(ex);
            }
        }
        //public void SendMessageWithCorps(string strMessage, List<string> toCorpIds)
        //{
        //    try
        //    {
        //        if (null != toCorpIds)
        //        {

        //            SendMessageWithClients(strMessage, ClientManager.Instance.ClientInfoWithCorpIds(toCorpIds));
        //        }
        //        else
        //        {
        //            //Log.Warn("客户端列表为空，取消发送！！！！！");
        //            log.Warn(string.Format("客户端列表为空，取消发送！！！！！"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //}

        //public void SendMessageWithClients(string strMessage, List<ClientUserInfo> clientInfos)
        //{
        //    try
        //    {
        //        if (null != clientInfos)
        //        {
        //            //Log.Info("将要发送消息:{0}",strMessage);
        //            //Log.Info("到客户端:");
        //            //Log.Info("[");
        //            log.Info(string.Format("将要发送消息:{0}", strMessage));
        //            log.Info(string.Format("到客户端:"));
        //            log.Info(string.Format("["));
        //            foreach (var info in clientInfos)
        //            {
        //                //Log.Info("{0}",info.ToString());
        //                log.Info(string.Format("{0}", info.ToString()));
        //            }
        //            //Log.Info("]");
        //            log.Info("]");
        //            SendMessage(MessageFactory.Instance.CreateMessage(strMessage), ClientManager.Instance.ClientKeyWithUserInfos(clientInfos));
        //        }
        //        else
        //        {
        //            //Log.Warn("客户端列表为空，取消发送！！！！！");
        //            log.Warn("客户端列表为空，取消发送！！！！！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //}
        public void SendMessage()
        {

        }
        private void SendMessage(Message message, List<string> tcpClientEndPointKeys)
        {
            try
            {
                if (null != tcpClientEndPointKeys)
                {
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    //Log.Info("到客户端:");
                    //Log.Info("[");
                    log.Info(string.Format("已经发送消息:{0}", message.Describe()));
                    log.Info(string.Format("到客户端:"));
                    log.Info(string.Format("["));
                    foreach (string clientKey in tcpClientEndPointKeys)
                    {
                        try
                        {
                            //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
                            log.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
                            server.Send(clientKey, message.Encode());
                        }
                        catch (Exception ex)
                        {
                            // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                           // log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                            log.Error(ex);
                        }
                    }
                    //  Log.Info("]");
                    log.Info("]");
                }
                else
                {
                    // Log.Warn("客户端列表为空，取消发送！！！！！");
                    log.Warn("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        private void Ack(MessageBody recvMessage, TcpClient tcpClient)
        {
            Message message = MessageFactory.Instance.CreateMessage(string.Empty, Common.Message.MessageType.ACK, recvMessage.id);
            SendMessage(message, new List<string> { ClientKey(tcpClient) });
        }

        private string ClientKey(TcpClient tcpClient)
        {
            if (tcpClient != null)
            {
                return tcpClient.Client.RemoteEndPoint.ToString();
            }
            return string.Empty;
        }

        private void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            tcpLogInfo.InfoFormat("客户端:{0} 已连接",ClientKey(e.TcpClient));
            //tcpLogInfo.Info(string.Format("已连接->服务器:{0}", e.ToString()));
            //  Login();
            ClientConnected?.Invoke(this, e);
            //log.Info(string.Format("客户端:{0} 已连接", ClientKey(e.TcpClient)));
        }

        private void server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            try
            {
                //Log.Info("客户端:{0} 已断开",ClientKey(e.TcpClient));
                tcpLogInfo.InfoFormat("客户端:{0} 已断开", ClientKey(e.TcpClient));
                ClientDisconnected?.Invoke(this, e);
                //ClientManager.Instance.RemoveTcpClient(e.TcpClient);

            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }
        static List<byte> hangingPieceProductList = new List<byte>();
        static List<byte> g_productDataList = new List<byte>();
        static List<byte> g_productsInfo = new List<byte>();

        static List<byte> g_HangingPieceOnlineProductsInfo = new List<byte>();
        static List<byte> g_HangerDropCardCamporesInfo = new List<byte>();

        //public static Dictionary<string, List<Hanger>> dicHangingPiece = new Dictionary<string, List<Hanger>>();
        //public static Dictionary<string, List<Hanger>> dicCommonPiece = new Dictionary<string, List<Hanger>>();

        #region Discard  server_DatagramReceived

        //private void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        //{
        //    try
        //    {
        //        TcpClient tcpClient = e.TcpClient;
        //        log.InfoFormat("收到消息:【客户端: {0}】-->【消息:{1}】", ClientKey(tcpClient), HexHelper.BytesToHexString(e.Datagram));
        //        //List<Message> messageList = MessageProcesser.Instance.ProcessRecvData(ClientKey(tcpClient),e.Datagram);
        //        List<SusNet.Common.Message.MessageBody> messageList = SusNet.Common.SusBusMessage.MessageProcesser.Instance.ProcessRecvData(e.TcpClient.Client.RemoteEndPoint.ToString(), e.Datagram);
        //        foreach (var rMessage in messageList)
        //        {
        //            //Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】",ClientKey(tcpClient),rMessage.Describe());
        //            log.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", ClientKey(tcpClient), rMessage.GetHexStr()));
        //            if (null != MessageReceived)
        //            {
        //                MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
        //                args.Tag = rMessage;
        //                try
        //                {
        //                    MessageReceived(this, args);
        //                }
        //                catch (Exception ex)
        //                {
        //                    //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
        //                    log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //                }
        //            }

        //            #region defect

        //            //    #region 主轨相关
        //            //    //【启动主轨后响应】
        //            //    var startmtMessage = SusNet2.Common.SusBusMessage.StartMainTrackRequestMessage.isEqual(rMessage.GetBytes());
        //            //    if (null != startmtMessage)
        //            //    {
        //            //        log.Info(string.Format("【启动主轨后响应】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));

        //            //        var startmtResponseMessage = "01 44 04 FF 00 37 00 00 00 00 00 10";
        //            //        log.Info(string.Format("【启动主轨后响应】---->服务器端发送开始--->客户端消息为:{0}", startmtResponseMessage));
        //            //        SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
        //            //        log.Info(string.Format("【启动主轨后响应】---->服务器端发送完成--->客户端消息为:{0}", startmtResponseMessage));

        //            //    }
        //            //    //【停止主轨后响应】
        //            //    var stopmtMessage = SusNet2.Common.SusBusMessage.StopMainTrackRequestMessage.isEqual(rMessage.GetBytes());
        //            //    if (null != stopmtMessage)
        //            //    {
        //            //        log.Info(string.Format("【停止主轨后响应】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));

        //            //        var startmtResponseMessage = "01 44 04 FF 00 37 00 00 00 00 00 11";
        //            //        log.Info(string.Format("【停止主轨后响应】---->服务器端发送开始--->客户端消息为:{0}", startmtResponseMessage));
        //            //        SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
        //            //        log.Info(string.Format("【停止主轨后响应】---->服务器端发送完成--->客户端消息为:{0}", startmtResponseMessage));

        //            //    }

        //            //    //【急停主轨后响应】
        //            //    var emercMessage = SusNet2.Common.SusBusMessage.EmergencyStopMainTrackRequestMessage.isEqual(rMessage.GetBytes());
        //            //    if (null != emercMessage)
        //            //    {
        //            //        log.Info(string.Format("【急停主轨后响应】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));

        //            //        var startmtResponseMessage = "01 44 04 FF 00 37 00 00 00 00 00 12";
        //            //        log.Info(string.Format("【急停主轨后响应】---->服务器端发送开始--->客户端消息为:【{0}】", startmtResponseMessage));
        //            //        SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
        //            //        log.Info(string.Format("【急停主轨后响应】---->服务器端发送完成--->客户端消息为:【{0}】", startmtResponseMessage));

        //            //    }
        //            //    #endregion

        //            //    #region 挂片站上线
        //            //    var hPieceNo = "04";
        //            //    var hexMainTrackNum = rMessage.XID;
        //            //    var cmd = rMessage.CMD;
        //            //    var address = string.Format("{0}{1}", rMessage.ADDH, rMessage.ADDL);
        //            //    var id = rMessage.ID;
        //            //    int productNum= GetProductNumber(address);
        //            //    if (0 != productNum && id.Equals(hPieceNo))
        //            //    {
        //            //        var hg = new Hanger()
        //            //        {
        //            //            ProductNumber = productNum.ToString(),
        //            //            MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //            StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //            Content = "制品发送挂片站确认",
        //            //            DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //        };
        //            //        if (!dicHangingPiece.ContainsKey(hPieceNo))
        //            //        {
        //            //            dicHangingPiece.Add(rMessage.ID, new List<Hanger>() { hg });
        //            //        }
        //            //        else {
        //            //            dicHangingPiece[hPieceNo].Add(hg);
        //            //        }
        //            //    }
        //            //    // var hexSta
        //            //    //if (!dicHangingPiece.ContainsKey("04") && rMessage.CMD.Equals("03") && rMessage.ADDH.Equals("60") && rMessage.ADDL.Equals("00"))
        //            //    //{
        //            //    //    var hg = new Hanger()
        //            //    //    {
        //            //    //        ProductNumber = HexHelper.HexToTen(rMessage.DATA1).ToString(),
        //            //    //        Index = dicHangingPiece.Count,
        //            //    //        MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //    //        StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //    //        Content = "制品发送挂片站确认",
        //            //    //        DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //    //    };
        //            //    //    //首次制品上线-->挂片
        //            //    //    if (!dicHangingPiece.ContainsKey("04"))
        //            //    //    {
        //            //    //        dicHangingPiece.Add(rMessage.ID, new List<Hanger>() { hg });
        //            //    //    }
        //            //    //    else
        //            //    //    {
        //            //    //        dicHangingPiece["04"].Add(hg);
        //            //    //    }
        //            //    //}
        //            //    //else if (dicHangingPiece.ContainsKey("04") && rMessage.CMD.Equals("03") && rMessage.ADDH.Equals("60") && rMessage.ADDL.Equals("00"))
        //            //    //{
        //            //    //    var hg = new Hanger()
        //            //    //    {
        //            //    //        ProductNumber = HexHelper.HexToTen(rMessage.DATA1).ToString(),
        //            //    //        Index = dicHangingPiece.Count,
        //            //    //        MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //    //        StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //    //        Content = "制品发送挂片站确认",
        //            //    //        DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //    //    };
        //            //    //    //首次制品上线-->挂片
        //            //    //    if (!dicHangingPiece.ContainsKey("04"))
        //            //    //    {
        //            //    //        dicHangingPiece.Add(rMessage.ID, new List<Hanger>() { hg });
        //            //    //    }
        //            //    //    else
        //            //    //    {
        //            //    //        dicHangingPiece["04"].Add(hg);
        //            //    //    }
        //            //    //}
        //            //    #endregion

        //            //    #region 制品界面直接上线
        //            //    var statingNo = rMessage.ID;
        //            //    if (!dicHangingPiece.ContainsKey("04") && rMessage.CMD.Equals("03") && rMessage.ADDL.Equals("35"))
        //            //    {//首次制品上线-->挂片
        //            //        if (!dicHangingPiece.ContainsKey("04"))
        //            //        {
        //            //            dicHangingPiece.Add(rMessage.ID, new List<Hanger>() {
        //            //            new Hanger() {
        //            //                ProductNumber=HexHelper.HexToTen(rMessage.DATA6).ToString(),
        //            //                Index=dicHangingPiece.Count,
        //            //                MainTrackNo=HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                StatingNo=HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                Content="制品发送挂片站确认",
        //            //            DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //            }
        //            //        });
        //            //        }
        //            //        else
        //            //        {
        //            //            dicHangingPiece["04"].Add(new Hanger()
        //            //            {
        //            //                ProductNumber = HexHelper.HexToTen(rMessage.DATA6).ToString(),
        //            //                Index = dicHangingPiece.Count,
        //            //                MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                Content = "制品发送挂片站确认"
        //            //            });
        //            //        }
        //            //    }
        //            //    else if (dicHangingPiece.ContainsKey("04") && rMessage.CMD.Equals("03") && rMessage.ADDL.Equals("35"))
        //            //    {//多次  制品上线-->挂片
        //            //        if (!dicHangingPiece.ContainsKey("04"))
        //            //        {
        //            //            dicHangingPiece[rMessage.ID].Add(new Hanger()
        //            //            {
        //            //                ProductNumber = HexHelper.HexToTen(rMessage.DATA6).ToString(),
        //            //                Index = dicHangingPiece.Count,
        //            //                MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                Content = "制品发送挂片站确认",
        //            //                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //            });
        //            //        }
        //            //        else
        //            //        {
        //            //            dicHangingPiece["04"].Add(new Hanger()
        //            //            {
        //            //                ProductNumber = HexHelper.HexToTen(rMessage.DATA6).ToString(),
        //            //                Index = dicHangingPiece.Count,
        //            //                MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                Content = "制品发送挂片站确认",
        //            //                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //            });
        //            //        }
        //            //    }
        //            //    //制品界面显示信息
        //            //    else if (dicHangingPiece.ContainsKey("04") && rMessage.CMD.Equals("05") && rMessage.ADDH.Equals("01"))
        //            //    {

        //            //    }
        //            //    #endregion

        //            //    #region 普通站
        //            //    if (!statingNo.Equals("04"))
        //            //    {
        //            //        //站1
        //            //        if (!dicCommonPiece.ContainsKey(statingNo) && rMessage.CMD.Equals("03") && rMessage.ADDL.Equals("51"))
        //            //        {//首次制品上线-->挂片
        //            //            if (!dicCommonPiece.ContainsKey(statingNo))
        //            //            {
        //            //                dicCommonPiece.Add(rMessage.ID, new List<Hanger>() {
        //            //            new Hanger() {
        //            //                //ProductNumber=rMessage.DATA6,
        //            //                Index=dicCommonPiece.Count,
        //            //                MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                HangerNo=StringUtils.ToFixLenStringFormat(HexHelper.HexToTen(string.Format(rMessage.DATA2+rMessage.DATA3+rMessage.DATA4+rMessage.DATA5+rMessage.DATA6)).ToString()),
        //            //            DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //            }
        //            //        });
        //            //            }
        //            //            else
        //            //            {
        //            //                dicCommonPiece[statingNo].Add(new Hanger()
        //            //                {
        //            //                    //ProductNumber=rMessage.DATA6,
        //            //                    Index = dicHangingPiece.Count,
        //            //                    MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                    StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                    Content = "普通站消息",
        //            //                    HangerNo = StringUtils.ToFixLenStringFormat(HexHelper.HexToTen(string.Format(rMessage.DATA2 + rMessage.DATA3 + rMessage.DATA4 + rMessage.DATA5 + rMessage.DATA6)).ToString()),
        //            //                    DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //                });
        //            //            }
        //            //            //衣架进站回应
        //            //            var data = string.Format("{0} {1} 02 FF 00 50 00 {2}", rMessage.XID, rMessage.ID, string.Format("{0}{1}{2}{3}{4}", rMessage.DATA2, rMessage.DATA3, rMessage.DATA4, rMessage.DATA5, rMessage.DATA6));
        //            //            var dBytes = HexHelper.strToToHexByte(data);
        //            //            SendMessageToAll(dBytes);

        //            //            ////衣架工序对比
        //            //            //var dataCompare = string.Format("{0} {1} 06 FF 00 54 00 {2}", rMessage.XID, rMessage.ID, string.Format("{0}{1}{2}{3}{4}", rMessage.DATA2, rMessage.DATA3, rMessage.DATA4, rMessage.DATA5, rMessage.DATA6));
        //            //            //var dataCompareBytes = HexHelper.strToToHexByte(dataCompare);
        //            //            //SendMessageToAll(dataCompareBytes);

        //            //        }
        //            //        else if (dicCommonPiece.ContainsKey(statingNo) && rMessage.CMD.Equals("03") && rMessage.ADDL.Equals("51"))
        //            //        {//多次  制品上线-->挂片
        //            //            if (!dicHangingPiece.ContainsKey(statingNo))
        //            //            {
        //            //                dicCommonPiece[rMessage.ID].Add(new Hanger()
        //            //                {
        //            //                    //  ProductNumber = rMessage.DATA6,
        //            //                    Index = dicCommonPiece.Count,
        //            //                    MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                    StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                    Content = "普通站消息",
        //            //                    HangerNo = StringUtils.ToFixLenStringFormat(HexHelper.HexToTen(string.Format(rMessage.DATA2 + rMessage.DATA3 + rMessage.DATA4 + rMessage.DATA5 + rMessage.DATA6)).ToString()),
        //            //                    DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //                });
        //            //            }
        //            //            else
        //            //            {
        //            //                dicCommonPiece[statingNo].Add(new Hanger()
        //            //                {
        //            //                    //  ProductNumber = rMessage.DATA6,
        //            //                    Index = dicCommonPiece.Count,
        //            //                    MainTrackNo = HexHelper.HexToTen(rMessage.XID).ToString(),
        //            //                    StatingNo = HexHelper.HexToTen(rMessage.ID).ToString(),
        //            //                    Content = "普通站消息",
        //            //                    HangerNo = StringUtils.ToFixLenStringFormat(HexHelper.HexToTen(string.Format(rMessage.DATA2 + rMessage.DATA3 + rMessage.DATA4 + rMessage.DATA5 + rMessage.DATA6)).ToString()),
        //            //                    DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm.sss")
        //            //                });
        //            //            }
        //            //            //衣架进站回应
        //            //            var data = string.Format("{0} {1} 02 FF 00 50 00 {2}", rMessage.XID, rMessage.ID, string.Format("{0}{1}{2}{3}{4}", rMessage.DATA2, rMessage.DATA3, rMessage.DATA4, rMessage.DATA5, rMessage.DATA6));
        //            //            var dBytes = HexHelper.strToToHexByte(data);
        //            //            SendMessageToAll(dBytes);
        //            //        }

        //            //        //站2
        //            //    }
        //            //    #endregion

        //            //    //【制品界面上线来自硬件的响应】
        //            //    //var cmRsMessage = SusNet2.Common.SusBusMessage.ClientMachineRequestMessage.isEqual(rMessage.GetBytes());
        //            //    //if (null != cmRsMessage)
        //            //    //{
        //            //    //    log.Info(string.Format("【制品界面上线来自硬件的响应】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //    //    cmRsMessage.CMD = "04";
        //            //    //    var startmtResponseMessage = cmRsMessage.ToString(); //"01 01 04 FF 00 35 00 00 00 00 00 05";

        //            //    //    log.Info(string.Format("【制品界面上线来自硬件的响应】---->服务器端发送开始--->客户端消息为:【{0}】", startmtResponseMessage));
        //            //    //    SendMessageToAll(cmRsMessage.GetBytes());
        //            //    //    log.Info(string.Format("【制品界面上线来自硬件的响应】---->服务器端发送完成--->客户端消息为:【{0}】", startmtResponseMessage));

        //            //    //}

        //            //    ////【分配工序到衣架成功回应】
        //            //    //var allHangerMessage = SusNet2.Common.SusBusMessage.AllocationHangerRequestMessage.isEqual(rMessage.GetBytes());
        //            //    //if (null != allHangerMessage)
        //            //    //{
        //            //    //    log.Info(string.Format("【分配工序到衣架成功回应】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));

        //            //    //    var startmtResponseMessage = string.Format("{0} {1} 04 FF 00 51 00 {2}", allHangerMessage.XID, allHangerMessage.ID, allHangerMessage.HangerNo);//AA BB CC DD EE";
        //            //    //    log.Info(string.Format("【分配工序到衣架成功回应】---->服务器端发送开始--->客户端消息为:【{0}】", startmtResponseMessage));
        //            //    //    SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
        //            //    //    log.Info(string.Format("【分配工序到衣架成功回应】---->服务器端发送完成--->客户端消息为:【{0}】", startmtResponseMessage));

        //            //    //}
        //            //    //【衣架出站状态】
        //            //    //var hSOutangerMessage = SusNet2.Common.SusBusMessage.HangerOutStatingResponseMessage.isEqual(rMessage.GetBytes());
        //            //    //if (null != hSOutangerMessage)
        //            //    //{
        //            //    //    log.Info(string.Format("【衣架出站状态】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));

        //            //    //    //var startmtResponseMessage = "01 04 04 FF 00 51 00 AA BB CC DD EE";
        //            //    //    //log.Info(string.Format("【衣架出站状态】---->服务器端发送开始--->客户端消息为:【{0}】", startmtResponseMessage));
        //            //    //    //SendMessageToAll(HexHelper.strToToHexByte(startmtResponseMessage));
        //            //    //    //log.Info(string.Format("【衣架出站状态】---->服务器端发送完成--->客户端消息为:【{0}】", startmtResponseMessage));

        //            //    //}
        //            //    ////【衣架落入读卡器工序比较 pc---->硬件】
        //            //    //var hDropCardMessage = SusNet2.Common.SusBusMessage.HangerDropCardResponseMessage.isEqual(rMessage.GetBytes());
        //            //    //if (null != hDropCardMessage)
        //            //    //{
        //            //    //    log.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //    //}
        //            //    //【挂片终端上线制单回显示】
        //            //    List<byte> productData = null;
        //            //    List<byte> productNumberData = null;
        //            //    if (SusNet2.Common.SusBusMessage.BindProcessOrderHangingPieceRequestMessage.isEqual(rMessage.GetBytes(), out productData, out productNumberData) && !BindProcessOrderHangingPieceRequestMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        hangingPieceProductList.AddRange(productData.ToArray());
        //            //        if (productNumberData != null)
        //            //        {
        //            //            g_productDataList.AddRange(productNumberData.ToArray());
        //            //        }
        //            //    }
        //            //    if (BindProcessOrderHangingPieceRequestMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        log.Info(string.Format("【pc挂片制单信息发送接收结束 pc---->硬件】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //        if (null != HangingPieceBindProcessMessageReceived)
        //            //        {
        //            //            var hexStr = HexHelper.byteToHexStr(hangingPieceProductList.ToArray());
        //            //            var hexProductNmuber = HexHelper.byteToHexStr(g_productDataList.ToArray());
        //            //            var productNumber = HexHelper.HexToTen(hexProductNmuber);
        //            //            MessageEventArgs args = new MessageEventArgs(UnicodeUtils.GetChsFromHex(hexStr));//HexHelper.UnHex(hexStr, "utf-8"));
        //            //            args.message = args.message + string.Format("  排产号:【{0}】", productNumber);
        //            //            // args.Tag = productNumber;
        //            //            HangingPieceBindProcessMessageReceived(this, args);
        //            //            hangingPieceProductList.Clear();
        //            //            productNumberData?.Clear();
        //            //            g_productDataList.Clear();
        //            //        }
        //            //    }
        //            //    ////【制品界面直接上线】
        //            //    //List<byte> productsInfo = null;
        //            //    //if (SusNet2.Common.SusBusMessage.ProductsDirectOnlineRequestMessage.isEqual(rMessage.GetBytes(), out productsInfo) && !ProductsDirectOnlineRequestMessage.isEnd(rMessage.GetBytes()))
        //            //    //{
        //            //    //    g_productsInfo.AddRange(productsInfo.ToArray());
        //            //    //}
        //            //    //if (ProductsDirectOnlineRequestMessage.isEnd(rMessage.GetBytes()))
        //            //    //{
        //            //    //    log.Info(string.Format("【制品界面直接上线pc向硬件发送上线信息 pc---->硬件】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //    //    if (null != ProductsDirectOnlineMessageReceived)
        //            //    //    {
        //            //    //        var hexStr = HexHelper.byteToHexStr(g_productsInfo.ToArray());

        //            //    //        MessageEventArgs args = new MessageEventArgs(UnicodeUtils.GetChsFromHex(hexStr));//HexHelper.UnHex(hexStr, "utf-8"));
        //            //    //        ProductsDirectOnlineMessageReceived(this, args);
        //            //    //        g_productsInfo?.Clear();

        //            //    //    }
        //            //    //}
        //            //    //【挂片站上线】
        //            //    List<byte> hangingPieceOnlineProduct = null;
        //            //    if (SusNet2.Common.SusBusMessage.HangingPieceStatingOnlineResponseMessage.isEqual(rMessage.GetBytes(), out hangingPieceOnlineProduct) && !HangingPieceStatingOnlineResponseMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        g_HangingPieceOnlineProductsInfo.AddRange(hangingPieceOnlineProduct.ToArray());
        //            //    }
        //            //    if (HangingPieceStatingOnlineResponseMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        log.Info(string.Format("【挂片站上线pc向硬件发送上线信息 pc---->硬件】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //        if (null != ProductsDirectOnlineMessageReceived)
        //            //        {
        //            //            //var hexStr = HexHelper.byteToHexStr(g_HangingPieceOnlineProductsInfo.ToArray());
        //            //            MessageEventArgs args = new MessageEventArgs(UnicodeUtils.GetChsFromHex(HexHelper.byteToHexStr(g_HangingPieceOnlineProductsInfo.ToArray())));//HexHelper.UnHex(hexStr, "utf-8"));
        //            //            ProductsDirectOnlineMessageReceived(this, args);
        //            //            g_HangingPieceOnlineProductsInfo?.Clear();

        //            //        }
        //            //    }
        //            //    //g_HangerDropCardCamporesInfo
        //            //    //【衣架落入读卡器工序信息的比较信息推送】
        //            //    List<byte> hangerDropCardFlowInfo = null;
        //            //    if (SusNet2.Common.SusBusMessage.HangerDropCardResponseMessage.isEqual(rMessage.GetBytes(), out hangerDropCardFlowInfo) && !HangerDropCardResponseMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        g_HangerDropCardCamporesInfo.AddRange(hangerDropCardFlowInfo.ToArray());
        //            //    }
        //            //    if (HangerDropCardResponseMessage.isEnd(rMessage.GetBytes()))
        //            //    {
        //            //        log.Info(string.Format("【衣架落入读卡器工序信息的比较信息推送 pc---->硬件】服务器端收到【客户端】消息:{0}", rMessage.GetHexStr()));
        //            //        if (null != ProductsDirectOnlineMessageReceived)
        //            //        {
        //            //            // var hexStr = HexHelper.byteToHexStr(g_HangerDropCardCamporesInfo.ToArray());
        //            //            MessageEventArgs args = new MessageEventArgs(UnicodeUtils.GetChsFromHex(HexHelper.byteToHexStr(g_HangerDropCardCamporesInfo.ToArray())));//HexHelper.UnHex(hexStr, "utf-8"));
        //            //            ProductsDirectOnlineMessageReceived(this, args);
        //            //            g_HangerDropCardCamporesInfo?.Clear();

        //            //        }
        //            //    }
        //            //    //【协议2.0】 衣架缓存清除
        //            //    if (rMessage.CMD.Equals("03") && rMessage.ADDH.Equals("00") && rMessage.ADDL.Equals("52"))
        //            //    {
        //            //        var bList = new List<byte>();
        //            //        for (var index = 7; index < 12; index++)
        //            //        {
        //            //            bList.Add(rMessage.GetBytes()[index]);
        //            //        }

        //            //        var chcResponseMessage = string.Format("{0} {1} 04 00 00 52 00 {2}", rMessage.XID, rMessage.ID, HexHelper.byteToHexStr(bList.ToArray()));//AA BB CC DD EE";
        //            //        log.Info(string.Format("【衣架缓存清除成功回应】---->服务器端发送开始--->客户端消息为:【{0}】", chcResponseMessage));
        //            //        SendMessageToAll(HexHelper.strToToHexByte(chcResponseMessage));
        //            //        log.Info(string.Format("【衣架缓存清除成功回应】---->服务器端发送完成--->客户端消息为:【{0}】", chcResponseMessage));
        //            //    }
        //            //    //if ( rMessage.body != null )
        //            //    //{
        //            //    //    Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", ClientKey(tcpClient), Encoding.UTF8.GetString(rMessage.body));
        //            //    //    ClientManager.Instance.AddOrUpdateTcpClient("1111","1111", tcpClient);
        //            //    //    //MessageBody messageBody = JsonConvert.DeserializeObject<MessageBody>(Encoding.UTF8.GetString(rMessage.body));
        //            //    //    //if ( messageBody != null )
        //            //    //    //{
        //            //    //    //    ClientManager.Instance.AddOrUpdateTcpClient(messageBody.gid,messageBody.uid,tcpClient);
        //            //    //    //    Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】",ClientKey(tcpClient),messageBody.Describe());
        //            //    //    //    switch ( rMessage.type )
        //            //    //    //    {
        //            //    //    //        case MessageType.ACK:
        //            //    //    //            break;
        //            //    //    //        case MessageType.Heartbeat:
        //            //    //    //        case MessageType.Common:
        //            //    //    //            Ack(messageBody,tcpClient);
        //            //    //    //            break;
        //            //    //    //        default:
        //            //    //    //            break;
        //            //    //    //    }
        //            //    //    //}
        //            //    //}
        //            //}
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //}

        //public void TestDataProcess(string strMessage, List<string> toCorpIds)
        //{
        //    try
        //    {
        //        List<ClientUserInfo> clientInfos = ClientManager.Instance.ClientInfoWithCorpIds(toCorpIds);
        //        //Log.Info("将要发送消息:{0}",strMessage);
        //        //Log.Info("到客户端:");
        //        //Log.Info("[");
        //        log.Info(string.Format("将要发送消息:{0}", strMessage));
        //        log.Info(string.Format("到客户端:"));
        //        log.Info(string.Format("["));
        //        foreach (var info in clientInfos)
        //        {
        //            //Log.Info("{0}",info.ToString());
        //            log.Info(string.Format("{0}", info.ToString()));
        //        }
        //        //  Log.Info("]");
        //        log.Info(string.Format("]"));

        //        Message message = MessageFactory.Instance.CreateMessage(strMessage);
        //        List<string> tcpClientEndPointKeys = ClientManager.Instance.ClientKeyWithUserInfos(clientInfos);
        //        byte[] messageData = message.Encode();
        //        //Log.Info("已经发送消息:{0}",message.Describe());
        //        //Log.Info("到客户端:");
        //        //Log.Info("[");
        //        log.Info(string.Format("已经发送消息:{0}", message.Describe()));
        //        log.Info(string.Format("到客户端:"));
        //        log.Info(string.Format("["));
        //        foreach (string clientKey in tcpClientEndPointKeys)
        //        {
        //            try
        //            {
        //                server.Send(clientKey, messageData);
        //                //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
        //                log.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //                log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //            }
        //        }
        //        // Log.Info("]");
        //        log.Info("]");

        //        List<Message> messageList = Common.Message.MessageProcesser.Instance.ProcessRecvData("0", messageData);
        //        foreach (var rMessage in messageList)
        //        {
        //            //Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】","0",rMessage.Describe());
        //            log.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", "0", rMessage.Describe()));
        //            if (rMessage.body != null)
        //            {
        //                MessageBody messageBody = JsonConvert.DeserializeObject<MessageBody>(Encoding.UTF8.GetString(rMessage.body));
        //                if (messageBody != null)
        //                {
        //                    // Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】","0",messageBody.Describe());
        //                    log.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", "0", messageBody.Describe()));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //}

        ///// <summary>
        ///// 【协议2.0】初始化制品推送地址与排产号的关系
        ///// </summary>
        //public static void InitProductsCMD()
        //{
        //    ProductNumberAddresList.Clear();
        //    ProductNumberHeadAddresList.Clear();

        //    var begin = 24576;//60 00
        //    var end = 26623;//67 FF
        //    //var dicProductNumber = new Dictionary<int, List<string>>();
        //    var i = 1;
        //    var pNumber = 0;
        //    var addressList = new List<string>();
        //    for (var index = begin; index <= end; index++)
        //    {
        //        var hexStr = HexHelper.tenToHexString(index);
        //        Console.WriteLine(index + "--->" + hexStr);
        //        //Console.WriteLine("");
        //        addressList.Add(hexStr);
        //        if (i % 8 == 0)
        //        {
        //            string[] aa = new string[8];
        //            addressList.CopyTo(aa);
        //            ProductNumberAddresList.Add(pNumber, new List<string>(aa));
        //            ProductNumberHeadAddresList.Add(pNumber, new List<string>(aa)[0]);
        //            addressList = new List<string>();

        //            pNumber++;
        //        }

        //        i++;
        //    }
        //    //生成排产号与首地址的关系
        //    //foreach (var key in ) {

        //    //}
        //}
        //public int GetProductNumber(string address)
        //{
        //    var productNumber = 0;
        //    foreach (var k in ProductNumberHeadAddresList.Keys)
        //    {
        //        if (ProductNumberHeadAddresList[k].Equals(address))
        //        {
        //            productNumber = k;
        //            return productNumber;
        //        }
        //    }
        //    return 0;
        //}
        ///// <summary>
        ///// 【协议2.0】排产号与地址的约定关系
        ///// </summary>
        //public static Dictionary<int, List<string>> ProductNumberAddresList = new Dictionary<int, List<string>>();
        ///// <summary>
        ///// 【协议2.0】排产号约定地址的首位地址
        ///// </summary>
        //public static Dictionary<int, string> ProductNumberHeadAddresList = new Dictionary<int, string>();
        /// <summary>
        /// 排产号与地址关系
        /// </summary>
        // public static Dictionary<string, int> AddressProductNumberList = new Dictionary<string, int>();
        #endregion

        public void SendData(byte[] data,TcpClient tcpClient=null)
        {
            try
            {
                if (server.IsRunning)
                {
                    server.Send(tcpClient,data);
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", HexHelper.BytesToHexString(data)));
                }

                else
                {
                    // Log.Error("未连接，无法发送消息:{0}",message);
                    tcpLogError.Error(string.Format("未连接，无法发送消息:{0}", HexHelper.BytesToHexString(data)));
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
              //  tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
              //  tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }
        public void SendMessage(SusNet.Common.Message.MessageBody message,TcpClient tcpClient=null)
        {
            try
            {
                if (server.IsRunning)
                {
                    server.Send(tcpClient, message.GetBytes());
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", message.GetHexStr()));
                }

                else
                {
                    // Log.Error("未连接，无法发送消息:{0}",message);
                    tcpLogError.Error(string.Format("未连接，无法发送消息:{0}", message.GetHexStr()));
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        #region 业务层
        #region 业务消息处理
        /// <summary>
        /// 员工登录信息推送，[工号]姓名
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByEmployeeLoginInfo(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient=null)
        {
            if (null == onlineInfo || onlineInfo.Count == 0)
            {
                var ex = new ApplicationException("发送内容不能为空!");
                tcpLogError.Error("【衣架落入读卡器,衣架携带制品信息推送】", ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(xor))
            {
                xor = "00";
            }
            var j = 0;
            var times = 0;
            if (onlineInfo.Count % 6 == 0)
            {
                times = onlineInfo.Count / 6;
            }
            else
            {
                times = 1 + onlineInfo.Count / 6;
            }
            var decBeginAddress = SuspeConstants.address_Send_Employee_Login_Info_Begin;//0x0140
            var decEndAddress = SuspeConstants.address_Send_Employee_Login_Info_End;//0x0147

            for (var i = 0; i < times; i++)
            {
                if (decBeginAddress > decEndAddress)
                {
                    var ex = new ApplicationException(string.Format("员工信息推送超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【员工信息推送】", ex);
                }
                var sendDataList = new List<byte>();
                var sData = EmployeeResponseMessage.GetHeaderBytesExt(mainTrackNo, statingNo, HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                if (j < onlineInfo.Count)
                {
                    for (int b = j; j < onlineInfo.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(onlineInfo[j]);
                    }
                }
                var teLen = sendDataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (sendDataList.Count == 12)
                    {
                        break;
                    }
                    sendDataList.AddRange(HexHelper.StringToHexByte("00"));
                }
                decBeginAddress++;
                tcpLogInfo.Info(string.Format("【员工信息推送 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                server.Send(tcpClient,bs);
                Console.WriteLine(string.Format("【员工信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【员工信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【员工信息推送 ，发送信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }
        /// <summary>
        /// 不能返工的提示信息推送(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag"></param>
        /// <param name="xor"></param>
        public void SendReworkException(int mainTrackNo, int statingNo, int hangerNo, int tag, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_ReturnWork_Res,
                xor,
                SuspeConstants.address_ReturnWork_Request,
                HexHelper.TenToHexString2Len(tag),
                HexHelper.TenToHexString10Len(hangerNo));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【返工异常推送 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【返工异常推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 异常推送(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag"></param>
        /// <param name="xor"></param>
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, string xor = null,TcpClient tcpClient=null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_ExcpetionOrPromptInfo,
                xor,
                SuspeConstants.address_ExcpetionOrPromptInfo,
                HexHelper.TenToHexString2Len(tag),
                HexHelper.TenToHexString10Len(0));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【异常推送 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【异常推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        /// <summary>
        /// 异常提示(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag"></param>
        /// <param name="promptData">数据位</param>
        /// <param name="xor"></param>
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, List<byte> promptData, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_ExcpetionOrPromptInfo,
                xor,
                SuspeConstants.address_ExcpetionOrPromptInfo,
                HexHelper.TenToHexString2Len(tag),
                HexHelper.BytesToHexString(promptData.ToArray()));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 衣架落入读卡器工序比较 pc---->硬件
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag">
        /// 0:相同工序
        /// 1:工序不同
        /// 2:返工衣架
        /// </param>
        /// <param name="xor"></param>
        public void HangerDropCardProcessFlowCompare(string mainTrackNo, string statingNo, string hangerNo, int tag, string xor = null, TcpClient tcpClient = null)
        {
            //01 04 05 XX 00 54 00 AA BB CC DD EE

            var message = new SusNet.Common.SusBusMessage.HangerDropCardResponseMessage(mainTrackNo, statingNo, hangerNo, tag, xor);
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message,tcpClient);
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 暂停或者接收衣架给下位机的响应
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        /// <param name="xor"></param>
        public void SuspendOrReceiveHangerReponseToLowerMachine(int mainTrackNo, int statingNo, int suspendReceive, string xor = null, TcpClient tcpClient = null)
        {
            var srTag = HexHelper.TenToHexString2Len(suspendReceive);

            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }

            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Lower_Machine_Suspend_OR_Receive_Hanger_Response,
                xor,
                SuspeConstants.address_Suspend_OR_Receive_Hanger,
                HexHelper.TenToHexString10Len(0),
                srTag);

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架给下位机的响应 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架给下位机的响应 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        //// <summary>
        /// zxl 2018年4月4日 22:56:36 
        /// 地址调整为0160-017F
        /// 衣架落入读卡器,衣架携带制品信息推送：制品信息【产品及工艺信息，制单号，颜色，尺码，单位，工序：工序号，工艺信息】发送
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByHangerDropCardCompareExt(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null,TcpClient tcpClient=null)
        {
            if (null == onlineInfo || onlineInfo.Count == 0)
            {
                var ex = new ApplicationException("发送内容不能为空!");
                tcpLogError.Error("【衣架落入读卡器,衣架携带制品信息推送】", ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(xor))
            {
                xor = "00";
            }
            var j = 0;
            var times = 0;
            if (onlineInfo.Count % 6 == 0)
            {
                times = onlineInfo.Count / 6;
            }
            else
            {
                times = 1 + onlineInfo.Count / 6;
            }
            var decBeginAddress = SuspeConstants.address_Fow_Compare_ProductOut_NoEqualFlow_Begin;// 0x0160;//0160
            var decEndAddress = SuspeConstants.address_Fow_Compare_ProductOut_NoEqualFlow_End;//0x017F;//017F

            for (var i = 0; i < times; i++)
            {
                if (decBeginAddress > decEndAddress)
                {
                    var ex = new ApplicationException(string.Format("工序比较内容推送超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【工序比较内容推送】", ex);
                }
                var sendDataList = new List<byte>();
                var sData = HangerDropCardResponseMessage.GetHeaderBytesExt(mainTrackNo, statingNo, HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                if (j < onlineInfo.Count)
                {
                    for (int b = j; j < onlineInfo.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(onlineInfo[j]);
                    }
                }
                var teLen = sendDataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (sendDataList.Count == 12)
                    {
                        break;
                    }
                    sendDataList.AddRange(HexHelper.StringToHexByte("00"));
                }
                decBeginAddress++;
                tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                server.Send(tcpClient,bs);
                Console.WriteLine(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }

        /// <summary>
        /// zxl 2018年4月29日 13:59:56 
        /// 地址调整为0160-017F
        /// 衣架落入读卡器,返工信息推送【本衣架:AN1060207,红色,XL,
        ///返工工序：30;
        ///疵点：245,爆口;309,破洞;】
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByHangerDropCardCompareExt2(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
        {
            if (null == onlineInfo || onlineInfo.Count == 0)
            {
                var ex = new ApplicationException("发送内容不能为空!");
                tcpLogError.Error("【衣架落入读卡器,衣架携带制品信息推送】", ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(xor))
            {
                xor = "00";
            }
            var j = 0;
            var times = 0;
            if (onlineInfo.Count % 6 == 0)
            {
                times = onlineInfo.Count / 6;
            }
            else
            {
                times = 1 + onlineInfo.Count / 6;
            }
            var decBeginAddress = SuspeConstants.address_Fow_Compare_ProductOut_ReworkFlow_Begin;// 0x0160;//0160
            var decEndAddress = SuspeConstants.address_Fow_Compare_ProductOut_ReworkFlow_End;//0x017F;//017F

            for (var i = 0; i < times; i++)
            {
                if (decBeginAddress > decEndAddress)
                {
                    var ex = new ApplicationException(string.Format("工序比较内容推送超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【工序比较内容推送】", ex);
                }
                var sendDataList = new List<byte>();
                var sData = HangerDropCardResponseMessage.GetHeaderBytesExt(mainTrackNo, statingNo, HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                if (j < onlineInfo.Count)
                {
                    for (int b = j; j < onlineInfo.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(onlineInfo[j]);
                    }
                }
                var teLen = sendDataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (sendDataList.Count == 12)
                    {
                        break;
                    }
                    sendDataList.AddRange(HexHelper.StringToHexByte("00"));
                }
                decBeginAddress++;
                tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                server.Send(tcpClient,bs);
                Console.WriteLine(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }
        /// <summary>
        /// 返工衣架出站产量推送
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void SendDataByReworkSiteOutSite(List<byte> sData, string mainTrackNo, string statingNo, TcpClient tcpClient = null)
        {
            if (null == sData || sData.Count == 0)
            {
                var ex = new ApplicationException(string.Format("发送内容不能为空! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【返工衣架产量数据推送】", ex);
                throw ex;
            }
            if (sData.Count > 6)
            {
                var ex = new ApplicationException(string.Format("发送内容超过6个字节! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【返工衣架产量数据推送】", ex);
                throw ex;
            }

            var sendDataList = new List<byte>();
            var dataHeader = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_ReworkHangerOutSite_HangerOutSite_Data_Send, SuspeConstants.XOR, SuspeConstants.Address_ReworkHangerOutSite_HangerOutSite_Data_Send);
            sendDataList.AddRange(HexHelper.StringToHexByte(dataHeader));
            sendDataList.AddRange(sData.ToArray());
            tcpLogInfo.Info(string.Format("【返工衣架产量数据推送 pc---->硬件】发送开始,消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
            server.Send(tcpClient,sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【返工衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        #endregion

        /// <summary>
        /// 出站【对硬件是否出战的回应】
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="isAuto"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void AutoHangerOutStating(string mainTrackNo, string statingNo, bool isAuto, int hangerNo, string xor = null, TcpClient tcpClient=null)
        {
            //01 44 05 XX 00 55 00 AA BB CC DD EE 允许出站
            var message = new SusNet.Common.SusBusMessage.HangerOutStatingResponseMessage(mainTrackNo, statingNo, isAuto, hangerNo, xor);
            tcpLogInfo.Info(string.Format("【出站】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message,tcpClient);
            tcpLogInfo.Info(string.Format("【出站】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        public void SendDataByCommonSiteOutSite(List<byte> sData, string mainTrackNo, string statingNo, TcpClient tcpClient)
        {
            if (null == sData || sData.Count == 0)
            {
                var ex = new ApplicationException(string.Format("发送内容不能为空! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【普通站衣架产量数据推送】", ex);
                throw ex;
            }
            if (sData.Count > 6)
            {
                var ex = new ApplicationException(string.Format("发送内容超过6个字节! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【普通站衣架产量数据推送】", ex);
                throw ex;
            }

            var sendDataList = new List<byte>();
            var dataHeader = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_CommonSite_HangerOutSite_Data_Send, SuspeConstants.XOR, SuspeConstants.Address_CommonSite_HangerOutSite_Data_Send);
            sendDataList.AddRange(HexHelper.StringToHexByte(dataHeader));
            sendDataList.AddRange(sData.ToArray());
            tcpLogInfo.Info(string.Format("【普通站衣架产量数据推送 pc---->硬件】发送开始,消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
            server.Send(tcpClient,sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【普通站衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        /// <summary>
        /// 启动主轨
        /// </summary>
        public void StartMainTrack(string mainTrackNo, string xor = null,TcpClient tcpClient=null)
        {
            //01 00 03 XX 00 37 00 00 00 00 00 10
            var message = new SusNet.Common.SusBusMessage.StartMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【启动主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message,tcpClient);
            tcpLogInfo.Info(string.Format("【启动主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 停止主轨
        /// </summary>
        public void StopMainTrack(string mainTrackNo, string xor = null, TcpClient tcpClient = null)
        {
            //01 00 03 XX 00 37 00 00 00 00 00 11
            var message = new SusNet.Common.SusBusMessage.StopMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【停止主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message,tcpClient);
            tcpLogInfo.Info(string.Format("【停止主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 急停主轨
        /// </summary>
        public void EmergencyStopMainTrack(string mainTrackNo, string xor = null, TcpClient tcpClient = null)
        {
            //01 00 03 XX 00 06 00 00 00 00 00 12
            var message = new SusNet.Common.SusBusMessage.EmergencyStopMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【急停主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message,tcpClient);
            tcpLogInfo.Info(string.Format("【急停主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        ///  【协议2.0】向挂片站发送制品信息
        ///  发送内容:【制单号，颜色，尺码，件数】
        /// </summary>
        /// <param name="products"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <param name="xor"></param>
        public void BindProudctsToHangingPieceNew(List<byte> products, string mainTrackNo, string statingNo, int productNumber, string xor = null, TcpClient tcpClient = null)
        {
            if (productNumber > 255)
            {
                var ex = new ApplicationException("排产号超过最大值255!不能挂片!productNumber：" + productNumber);
                tcpLogError.Error("【向挂片站发送制品信息】", ex);
                throw ex;
            }
            if (products.Count > 46)
            {
                var ex = new ApplicationException("制品信息超过46个字节!不能挂片!");
                tcpLogError.Error("【向挂片站发送制品信息】", ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(xor))
            {
                xor = "00";
            }
            var j = 0;
            var index = 1;
            //【协议2.0】根据排产号获取约定的地址
            var addresList = SusNetContext.ProductNumberAddresList[productNumber];
            foreach (var ad in addresList)
            {
                var sendDataList = new List<byte>();
                var messageHeader = BindProcessOrderHangingPieceRequestMessage.GetHeaderBytesNew(mainTrackNo, statingNo, ad, xor);
                sendDataList.AddRange(messageHeader);
                if (j < products.Count)
                {
                    for (int b = j; j < products.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(products[j]);
                    }
                }
                var teLen = sendDataList.Count;
                for (var ii = 0; ii < 12 - teLen; ii++)
                {
                    if (sendDataList.Count == 12)
                    {
                        break;
                    }
                    //数据位不足补FF
                    sendDataList.AddRange(HexHelper.StringToHexByte("FF"));
                }
                tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", index, HexHelper.BytesToHexString(sendDataList.ToArray())));
                server.Send(tcpClient,sendDataList.ToArray());
                tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", index, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                index++;
            }
        }

        public void ClientMancheOnLine(string mainTrackNo, string statingNo, string productNumber, string xor = null, TcpClient tcpClient = null)
        {
            //01 01 03 XX 00 35 00 00 00 00 00 05
            xor = "FF";
            var message = new SusNet.Common.SusBusMessage.ClientMachineRequestMessage(mainTrackNo, statingNo, productNumber, xor);
            tcpLogInfo.Info(string.Format("【制品界面上线请求】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message, tcpClient);
            tcpLogInfo.Info(string.Format("【制品界面上线请求】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }

        /// <summary>
        /// 【满站查询】上位机初始化时查询下位机站点满站状态
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void QueryFullSite(int mainTrackNumber, int statingNo, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNumber),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_FullSite_Req,
                xor,
                SuspeConstants.address_FullSite,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString10Len(0)
                );

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【满站查询 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【满站查询 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 初始化站内数(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="num"></param>
        /// <param name="xor"></param>
        public void SendStatingNum(int mainTrackNo, int statingNo, int num, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Stating_Num,
                xor,
                SuspeConstants.address_Stating_Num,
                HexHelper.TenToHexString8Len(0),
                HexHelper.TenToHexString4Len(num)
                );

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【初始化站内数 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【初始化站内数 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 上位机对站点类型的添加
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        /// <param name="data">16进制数据</param>
        /// <param name="type">01:修改;02:新增</param>
        public void SendStatingType(int mainTrackNo, string statingNo, int type, int data, string xor = null, TcpClient tcpClient = null)
        {
            //【四字节00+维护类型+站类型】
            //01 02 03 XX 00 3F 00 00 00 01/02 XX

            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var statingTypeStr = HexHelper.TenToHexString2Len(data);
            var typeStr = HexHelper.TenToHexString2Len(type); //新增，修改

            var cRequest = new StatingTypeRequestMessage(mainTrackNum, statingNoHex, typeStr, statingTypeStr);
            tcpLogInfo.Info(string.Format("【修改/添加站点类型 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            server.Send(tcpClient, cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【修改/添加站点类型 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        /// <summary>
        /// 暂停或者接收衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        /// <param name="xor"></param>
        public void SuspendOrReceiveHanger(int mainTrackNo, int statingNo, int suspendReceive, string xor = null, TcpClient tcpClient = null)
        {
            var srTag = HexHelper.TenToHexString2Len(suspendReceive);

            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }

            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Suspend_OR_Receive_Hanger_Request,
                xor,
                SuspeConstants.address_Suspend_OR_Receive_Hanger,
                HexHelper.TenToHexString10Len(0),
                srTag);

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架 pc---->硬件】发送开始,消息:--->{0}", message));
            server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 修改站点容量
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="capacity"></param>
        /// <param name="xor"></param>
        public void ModifyStatingCapacity(int mainTrackNo, string statingNo, int capacity, string xor = null, TcpClient tcpClient = null)
        {
            //01 02 03 XX 00 33 00 00 00 00 01 08（4字节00+2字节数据）
            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var capacityHex = HexHelper.TenToHexString4Len(capacity);

            var cRequest = new StatingCapacityRequestMessage(mainTrackNum, statingNoHex, capacityHex, SuspeConstants.cmd_StatingCapacity_3);

            tcpLogInfo.Info(string.Format("【修改站点容量 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            server.Send(tcpClient, cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【修改站点容量 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }
        #endregion

    }
}

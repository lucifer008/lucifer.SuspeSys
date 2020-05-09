using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using Sus.Net.Client.Sockets;
using Sus.Net.Common.Entity;
using Sus.Net.Common.Utils;
using Sus.Net.Common.Message;
using Sus.Net.Common.Event;
using Newtonsoft.Json;
using log4net;
using SusNet.Common.BusModel;
using SusNet.Common.Utils;
using SusNet.Common.SusBusMessage;
using SusNet.Common.Message;
using Sus.Net.Common.SusBusMessage;
using Sus.Net.Common.Common;
using SuspeSys.Utils;
using Sus.Net.Common.Constant;

namespace Sus.Net.Client
{
    public class SusTCPClient : SusLog
    {
        //private ILog log = LogManager.GetLogger(typeof(SusTCPClient));
        // private static readonly ILog tcpLogInfo = LogManager.GetLogger("TcpLogInfo");

        private readonly AsyncTcpClient client;

        private readonly ClientUserInfo clientUserInfo;
        /// <summary>
        /// 服务器地址
        /// </summary>
        private readonly string serverIP;
        /// <summary>
        /// 服务器端口
        /// </summary>
        private readonly int serverPort;

        /// <summary>
        /// 心跳定时器
        /// </summary>
        private System.Timers.Timer heartbeatTimer = new System.Timers.Timer();

        /// <summary>
        /// 与服务器的连接已建立事件
        /// </summary>
        public event EventHandler<TcpServerConnectedEventArgs> ServerConnected;
        /// <summary>
        /// 与服务器的连接已断开事件
        /// </summary>
        public event EventHandler<TcpServerDisconnectedEventArgs> ServerDisconnected;
        /// <summary>
        /// 与服务器的连接发生异常事件
        /// </summary>
        public event EventHandler<TcpServerExceptionOccurredEventArgs> ServerExceptionOccurred;

        /// <summary>
        /// 接收到消息
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

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
        static SusTCPClient()
        {

            SusNetContext.InitProductsCMD();
        }
        public SusTCPClient(ClientUserInfo info, string serverIP, int serverPort)
        {
            this.clientUserInfo = info;
            this.serverIP = serverIP;
            this.serverPort = serverPort;

            MessageFactory.Instance.Init(clientUserInfo);

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            client = new AsyncTcpClient(remoteEP) { Encoding = Encoding.UTF8 };
            client.ServerExceptionOccurred += new EventHandler<TcpServerExceptionOccurredEventArgs>(client_ServerExceptionOccurred);
            client.ServerConnected += new EventHandler<TcpServerConnectedEventArgs>(client_ServerConnected);
            client.ServerDisconnected += new EventHandler<TcpServerDisconnectedEventArgs>(client_ServerDisconnected);
            //client.PlaintextReceived += new EventHandler<TcpDatagramReceivedEventArgs<string>>(client_PlaintextReceived);
           client.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(client_DatagramReceived);
            //无限制重连
            client.Retries = AsyncTcpClient.UnlimitedRetry;
            client.RetryInterval = 3;
        }

        public void Connect()
        {
            if (!client.Connected)
            {
                client.Connect();
                // Log.Info("开始连接服务器:({0}:{1})",serverIP,serverPort);
                tcpLogInfo.Info(string.Format("开始连接服务器:({0}:{1})", serverIP, serverPort));
            }
            else
            {
            }
        }

        public void Disconnect()
        {
            if(null!= client)
                client.Close(true);
        }
        public void SendData(byte[] data)
        {
            try
            {
                if (client.Connected)
                {
                    client.Send(data);
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
        public void SendMessage(SusNet.Common.Message.MessageBody message)
        {
            try
            {
                if (client.Connected)
                {
                    client.Send(message.GetBytes());
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
                //tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

       

        private void SendMessage(Message message)
        {
            try
            {
                if (client.Connected)
                {
                    client.Send(message.Encode());
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", message.Describe()));
                }

                else
                {
                    // Log.Error("未连接，无法发送消息:{0}",message);
                    tcpLogError.Error(string.Format("未连接，无法发送消息:{0}", message));
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
               // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }


        private void Login()
        {
            SendMessage(MessageFactory.Instance.CreateMessage(string.Empty, Common.Message.MessageType.Login));
        }

        private void Ack(MessageBody recvMessage)
        {
            SendMessage(MessageFactory.Instance.CreateMessage(string.Empty, Common.Message.MessageType.ACK, recvMessage.id));
        }

        private void Heartbeat(object o, EventArgs e)
        {
            //  var message = new SusMessage(HexHelper.StringToHexByte("00 00 03 00 00 00 00 00 00 00 00 00"));
            //SendMessage(MessageFactory.Instance.CreateMessage(string.Empty, Common.Message.MessageType.Heartbeat));
            var data = HexHelper.StringToHexByte("00 00 00 00 00 00 00 00 00 00 00 00");
            SendData(data);
        }

        public void ManualEmployeeLoginStating(int mainTrackNo, int statingNo, int type, int cardNo,string info)
        {
            var cardRes = new CardResponseMessage(HexHelper.TenToHexString2Len(mainTrackNo),HexHelper.TenToHexString2Len(statingNo), 3, HexHelper.TenToHexString10Len(cardNo), SuspeConstants.XOR);
            SendData(cardRes.GetBytes());
            var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
            var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
            SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo), SuspeConstants.XOR);
        }

        private void startHeartbeatTimer()
        {
            if (heartbeatTimer.Enabled)
            {
                return;
            }
            heartbeatTimer = new System.Timers.Timer
            {
                Interval = 120 * 1000,
                AutoReset = true
            };
            heartbeatTimer.Elapsed += Heartbeat;
            heartbeatTimer.Start();
        }

        private void stopHeartbeatTimer()
        {
            heartbeatTimer.Stop();
        }

        private void client_ServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            try
            {
                tcpLogInfo.Info(string.Format("已连接->服务器:{0}", e.ToString()));
                //  Login();
                ServerConnected?.Invoke(this, e);
                startHeartbeatTimer();
            }
            catch (Exception ex)
            {
                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
              //  tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        private void client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            try
            {
                //Log.Warn("已经断开->服务器 {0} .",e.ToString());
                tcpLogInfo.Warn(string.Format("已经断开->服务器 {0} .", e.ToString()));
                ServerDisconnected?.Invoke(this, e);
                stopHeartbeatTimer();
            }
            catch (Exception ex)
            {
                //  Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                //tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        private void client_ServerExceptionOccurred(object sender, TcpServerExceptionOccurredEventArgs e)
        {
            try
            {
                //Log.Error("服务器:{0} 发生异常:{1}.",e.ToString(),e.Exception.Message);
                tcpLogError.Error(string.Format("服务器:{0} 发生异常:{1}.", e.ToString(), e.Exception.Message));
                ServerExceptionOccurred?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
               // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        private void client_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            try
            {
                //Log.Info("【客户端:{0}】收到消息:【服务器: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),BufferUtils.ByteToHexStr(e.Datagram));
              //  tcpLogInfo.Info(string.Format("【客户端:{0}】收到消息:【服务器: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), BufferUtils.ByteToHexStr(e.Datagram)));
                tcpLogHardware.Info(string.Format("【客户端:{0}】收到消息:【服务器: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), BufferUtils.ByteToHexStr(e.Datagram)));
                List<SusNet.Common.Message.MessageBody> messageList = SusNet.Common.SusBusMessage.MessageProcesser.Instance.ProcessRecvData(e.TcpClient.Client.RemoteEndPoint.ToString(), e.Datagram);
                foreach (var rMessage in messageList)
                {
                  //  tcpLogInfo.Info(string.Format("【客户端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), rMessage.GetHexStr()));
                    tcpLogHardware.Info(string.Format("【客户端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), rMessage.GetHexStr()));

                    if (MessageReceived != null)
                    {
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        try
                        {
                            MessageReceived(this, args);
                        }
                        catch (Exception ex)
                        {
                            //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                           // tcpLogError.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                            tcpLogError.Error(ex);
                        }
                    }

                    //【启动主轨后响应】
                    var startmtMessage = SusNet.Common.SusBusMessage.StartMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != StartMainTrackResponseMessageReceived && null != startmtMessage)
                    {
                        //tcpLogInfo.Info(string.Format("【启动主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【启动主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = startmtMessage;
                        StartMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【停止主轨后响应】
                    var smtMessage = SusNet.Common.SusBusMessage.StopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != StopMainTrackResponseMessageReceived && null != smtMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【停止主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【停止主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = smtMessage;
                        StopMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【急停主轨后响应】
                    var esmtMessage = SusNet.Common.SusBusMessage.EmergencyStopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != EmergencyStopMainTrackResponseMessageReceived && null != esmtMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【急停主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【急停主轨后响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = esmtMessage;
                        EmergencyStopMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【【停止接收衣架】终端按下【暂停键时】硬件通知pc】
                    var srhMessage = SusNet.Common.SusBusMessage.StopReceiveHangerRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != StopReceiveHangerRequestMessageReceived && null != srhMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = srhMessage;
                        StopReceiveHangerRequestMessageReceived(this, args);
                        continue;
                    }

                    //【衣架落入读卡器发送的请求，硬件发pc端时触发】
                    var hdcMessage = SusNet.Common.SusBusMessage.HangerDropCardRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerDropCardRequestMessageReceived && null != hdcMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hdcMessage;
                        HangerDropCardRequestMessageReceived(this, args);
                        continue;
                    }

                    //【衣架进站】
                    var hasMessage = SusNet.Common.SusBusMessage.HangerArrivalStatingRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerArrivalStatingMessageReceived && null != hasMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【衣架进站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架进站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hasMessage;
                        HangerArrivalStatingMessageReceived(this, args);
                        continue;
                    }
                    //【衣架出站请求】
                    var hoMessage = SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerOutStatingRequestMessageReceived && null != hoMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【衣架出站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架出站】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hoMessage;
                        HangerOutStatingRequestMessageReceived(this, args);
                        continue;
                    }
                    ////【协议2.0---->挂片站衣架信息上传请求】
                    //var hpsUploadHangerInfoMessage = HangingPieceHangerUploadRequestMessage.isEqual(rMessage.GetBytes());
                    //if (null != HangingPieceHangerUploadRequestMessageReceived && null != hpsUploadHangerInfoMessage)
                    //{
                    //    tcpLogInfo.Info(string.Format("【>挂片站衣架信息上传请求】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                    //    MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                    //    args.Tag = hpsUploadHangerInfoMessage;
                    //    HangingPieceHangerUploadRequestMessageReceived(this, args);
                    //    continue;
                    //}
                    //【分配工序到衣架成功回应】
                    var allMessage = SusNet.Common.SusBusMessage.AllocationHangerResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != AllocationHangerResponseMessageReceived && null != allMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【分配工序到衣架成功回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【分配工序到衣架成功回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = allMessage;
                        AllocationHangerResponseMessageReceived(this, args);
                        continue;
                    }
                    //【制品界面上线】
                    var cmsResMessage = SusNet.Common.SusBusMessage.ClientMachineResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != ClientMachineResponseMessageReceived && null != cmsResMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【制品界面上线来自硬件的响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【制品界面上线来自硬件的响应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cmsResMessage;
                        ClientMachineResponseMessageReceived(this, args);
                        continue;
                    }
                    //【挂片站上线】
                    var hpsOnlineResMessage = SusNet.Common.SusBusMessage.HangingPieceStatingOnlineRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangingPieceStatingOnlineMessageReceived && null != hpsOnlineResMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【挂片站上线】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【挂片站上线】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hpsOnlineResMessage;
                        HangingPieceStatingOnlineMessageReceived(this, args);
                        continue;
                    }
                    //【衣架返工】
                    var hangerReworkRequestMessage = SusNet.Common.SusBusMessage.ReworkRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerReworkMessageReceived && null != hangerReworkRequestMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【衣架返工】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架返工】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hangerReworkRequestMessage;
                        HangerReworkMessageReceived(this, args);
                        continue;
                    }
                    //【衣架返工工序及疵点代码】
                    var reworkFlowDefectRequestMessage = ReworkFlowDefectRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != reworkFlowDefectRequestMessage && null != ReworkFlowDefectRequestMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【衣架返工工序及疵点代码】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = reworkFlowDefectRequestMessage;
                        ReworkFlowDefectRequestMessageReceived(this, args);
                        continue;
                    }
                    //【卡片相关】
                    var cardRequestMessage = CardRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != cardRequestMessage && null != CardRequestMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【卡片相关】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【卡片相关】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cardRequestMessage;
                        CardRequestMessageReceived(this, args);
                        continue;
                    }
                    //【清除衣架缓存】
                    var cHangerCacheResponse = ClearHangerCacheResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != cHangerCacheResponse && null != ClearHangerCacheResponseMessageReceived)
                    {
                        //tcpLogInfo.Info(string.Format("【清除衣架缓存】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【清除衣架缓存】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cHangerCacheResponse;
                        ClearHangerCacheResponseMessageReceived(this, args);
                        continue;
                    }
                    //【监测点上传】
                    var monitorMessage = MonitorMessage.isEqual(rMessage.GetBytes());
                    if (null != monitorMessage && null != MonitorMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【监测点上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【监测点上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = monitorMessage;
                        MonitorMessageReceived(this, args);
                        continue;
                    }
                    //【满站上传】
                    var fullSiteMessage = FullSiteMessage.isEqual(rMessage.GetBytes());
                    if (null != fullSiteMessage && null != FullSiteMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【满站上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【满站上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = fullSiteMessage;
                        FullSiteMessageReceived(this, args);
                        continue;
                    }

                    //【修改站点容量】
                    var statingCapacityResponseMessage = StatingCapacityResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != statingCapacityResponseMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【修改站点容量】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【修改站点容量】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = statingCapacityResponseMessage;
                        StatingCapacityResponseMessageReceived(this, args);
                        continue;
                    }

                    //【修改站点类型】
                    var statingTypeResponseMessage = StatingTypeResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != statingCapacityResponseMessage)
                    {
                       // tcpLogInfo.Info(string.Format("【修改站点类型】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【修改站点类型】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = statingTypeResponseMessage;
                        StatingTypeResponseMessageReceived(this, args);
                        continue;
                    }
                    //上电初始化
                    //PowerSupplyInitMessageReceived
                    var powerSupplyInitRequestMessage = PowerSupplyInitRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != powerSupplyInitRequestMessage && null != PowerSupplyInitMessageReceived)
                    {
                      //  tcpLogInfo.Info(string.Format("【上电初始化】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【上电初始化】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = powerSupplyInitRequestMessage;
                        PowerSupplyInitMessageReceived(this, args);
                        continue;
                    }
                    //SN序列号
                    var sNSerialNumberRequestMessage = SNSerialNumberRequestMessage.isEqual(rMessage.GetBytes());
                    if (null!= sNSerialNumberRequestMessage && null != SNSerialNumberMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【SN上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【SN上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = sNSerialNumberRequestMessage;
                        SNSerialNumberMessageReceived(this, args);
                        continue;
                    }
                    //主版版本号
                    var mainboardVersionRequestMessage = MainboardVersionRequestMessage.isEqual(rMessage.GetBytes());
                    if (null!= mainboardVersionRequestMessage && null != MainboardVersionMessageReceived) {
                      //  tcpLogInfo.Info(string.Format("【主版版本号上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【主版版本号上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = mainboardVersionRequestMessage;
                        MainboardVersionMessageReceived(this, args);
                        continue;
                    }

                    //下位机接收或暂停衣架
                    var lowerMachineSuspendOrReceiveMessage = LowerMachineSuspendOrReceiveMessage.isEqual(rMessage.GetBytes());
                    if (null!=lowerMachineSuspendOrReceiveMessage && null!= LowerMachineSuspendOrReceiveMessageReceived) {
                       // tcpLogInfo.Info(string.Format("【下位机接收或暂停衣架上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【下位机接收或暂停衣架上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = lowerMachineSuspendOrReceiveMessage;
                        LowerMachineSuspendOrReceiveMessageReceived(this, args);
                        continue;
                    }
                    //上位机发起的上电初始化，硬件回应
                    var upperComputerInitResponseMessage = UpperComputerInitResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != upperComputerInitResponseMessage && null != UpperComputerInitResponseMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【上位机发起的上电初始化，硬件回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【上位机发起的上电初始化，硬件回应】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = upperComputerInitResponseMessage;
                        UpperComputerInitResponseMessageReceived(this, args);
                        continue;
                    }

                    //【满站查询状态上传】
                    var fullSiteQueryResponseMessage = FullSiteMessage.isFullSiteQueryEqual(rMessage.GetBytes());
                    if (null != fullSiteQueryResponseMessage && null != FullSiteQueryResponseMessageReceived)
                    {
                       // tcpLogInfo.Info(string.Format("【满站查询状态上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【满站查询状态上传】客户端收到服务器端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = fullSiteQueryResponseMessage;
                        FullSiteQueryResponseMessageReceived(this, args);
                        continue;
                    }
                    //UpperComputerInitResponseMessageReceived

                    //if ( rMessage.body != null )
                    //{
                    //    //Log.Info("【客户端:{0}】收到消息完整解析消息:【服务器: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),rMessage.Describe());
                    //    Log.Info("【客户端:{0}】收到消息完整解析消息:【服务器: {1}】-->【消息:{2}】", e.TcpClient.Client.LocalEndPoint.ToString(), e.TcpClient.Client.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(rMessage.body));
                    //    MessageBody messageBody = JsonConvert.DeserializeObject<MessageBody>(Encoding.UTF8.GetString(rMessage.body));
                    //    if ( messageBody != null )
                    //    {
                    //        Log.Info("【客户端:{0}】收到消息业务消息:【客户端: {1}】-->【消息:{2}】",e.TcpClient.Client.LocalEndPoint.ToString(),e.TcpClient.Client.RemoteEndPoint.ToString(),messageBody.Describe());
                    //        switch ( rMessage.type )
                    //        {
                    //            case MessageType.ACK:
                    //                break;
                    //            case MessageType.Heartbeat:
                    //                Ack(messageBody);
                    //                break;
                    //            case MessageType.Common:
                    //                if ( MessageReceived != null )
                    //                {
                    //                    MessageEventArgs args = new MessageEventArgs(messageBody.DATA);
                    //                    try
                    //                    {
                    //                        MessageReceived(this,args);
                    //                    }
                    //                    catch ( Exception ex )
                    //                    {
                    //                        Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                    //                    }
                    //                }
                    //                Ack(messageBody);
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                tcpLogError.Error(ex);
                tcpLogHardware.Error(ex);
            }
        }

        /// <summary>
        /// 启动主轨
        /// </summary>
        public void StartMainTrack(string mainTrackNo, string xor = null)
        {
            //01 00 03 XX 00 37 00 00 00 00 00 10
            var message = new SusNet.Common.SusBusMessage.StartMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【启动主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【启动主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 停止主轨
        /// </summary>
        public void StopMainTrack(string mainTrackNo, string xor = null)
        {
            //01 00 03 XX 00 37 00 00 00 00 00 11
            var message = new SusNet.Common.SusBusMessage.StopMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【停止主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【停止主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 急停主轨
        /// </summary>
        public void EmergencyStopMainTrack(string mainTrackNo, string xor = null)
        {
            //01 00 03 XX 00 06 00 00 00 00 00 12
            var message = new SusNet.Common.SusBusMessage.EmergencyStopMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【急停主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【急停主轨】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }

        /// <summary>
        /// 出站【对硬件是否出战的回应】
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="isAuto"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void AutoHangerOutStating(string mainTrackNo, string statingNo, bool isAuto, int hangerNo, string xor = null)
        {
            //01 44 05 XX 00 55 00 AA BB CC DD EE 允许出站
            var message = new SusNet.Common.SusBusMessage.HangerOutStatingResponseMessage(mainTrackNo, statingNo, isAuto, hangerNo, xor);
            tcpLogInfo.Info(string.Format("【出站】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【出站】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }

        /// <summary>
        /// 【协议2.0】 出站【对硬件是否出战的回应】
        /// 01 01 05 XX 00 59 00 AA BB CC DD EE---允许出站
        /// 01 01 05 XX 00 59 01 AA BB CC DD EE ---不允许出站
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="isAuto"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        //public void AutoHangerOutStatingExt(string mainTrackNo, string statingNo, bool isAuto, int hangerNo, string xor = null)
        //{
        //    //01 44 05 XX 00 59 00 AA BB CC DD EE 允许出站
        //    var message = new SusNet.Common.SusBusMessage.HangerOutStatingResponseMessage(mainTrackNo, statingNo, isAuto, hangerNo, "00", "59", xor);
        //    tcpLogInfo.Info(string.Format("【出站】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //    SendMessage(message);
        //    tcpLogInfo.Info(string.Format("【出站】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //}
        ///// <summary>
        ///// 分配衣架下一工序站点回应
        ///// </summary>
        //public void AllocationHanger(string mainTrackNo, string statingNo, string hangerNo, string xor = null)
        //{
        //    //01 46 04 XX 00 51 00 AA BB CC DD EE 将衣架分配到下一个46工位成功
        //    var message = new SusNet.Common.SusBusMessage.AllocationHangerResponseMessage(mainTrackNo, hangerNo, xor);
        //    tcpLogInfo.Info(string.Format("【分配衣架下一工序站点回应】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //    SendMessage(message);
        //    tcpLogInfo.Info(string.Format("【分配衣架下一工序站点回应】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //}
        ///// <summary>
        ///// 衣架进站软件回应
        ///// </summary>
        //public void HangerArrivalStating(string mainTrackNo, string statingNo, string hangerNo, string xor = null)
        //{
        //    //上位机回复：01 44 05 XX 00 50 00 AA BB CC DD EE 回复
        //    var message = new SusNet.Common.SusBusMessage.HangerArrivalStatingResponeMessage(mainTrackNo, statingNo, "00", "50", hangerNo, xor);
        //    tcpLogInfo.Info(string.Format("【衣架进站软件回应】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //    SendMessage(message);
        //    tcpLogInfo.Info(string.Format("【衣架进站软件回应】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //}
        ///// <summary>
        ///// 工序检查回应
        ///// </summary>
        //public void CheckFlowResponse(string mainTrackNo, string statingNo, string hangerNo, string xor = null)
        //{
        //    //上位机回复：01 44 05 XX 00 54 01 AA BB CC DD EE 不同工序

        //    var message = new SusNet.Common.SusBusMessage.HangerDropCardResponseMessage(mainTrackNo, hangerNo, "00", "54", xor);
        //    log.Info(string.Format("【工序检查回应】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.byteToHexStr(message.GetBytes())));
        //    SendMessage(message);
        //    log.Info(string.Format("【工序检查回应】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.byteToHexStr(message.GetBytes())));
        //}
        /// <summary>
        /// 制品界面上线
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <param name="xor"></param>
        public void ClientMancheOnLine(string mainTrackNo, string statingNo, string productNumber, string xor = null)
        {
            //01 01 03 XX 00 35 00 00 00 00 00 05
            xor = "FF";
            var message = new SusNet.Common.SusBusMessage.ClientMachineRequestMessage(mainTrackNo, statingNo, productNumber, xor);
            tcpLogInfo.Info(string.Format("【制品界面上线请求】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【制品界面上线请求】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }

        ///// <summary>
        ///// 挂片站上线请求响应pc---->硬件
        ///// </summary>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="productNumber"></param>
        ///// <param name="xor"></param>
        //public void HangingPieceOnLine(string mainTrackNo, string statingNo, string productNumber, string xor = null)
        //{
        //    var message = new SusNet.Common.SusBusMessage.HangingPieceStatingOnlineResponseMessage(mainTrackNo, statingNo, "00", "35", productNumber, xor);
        //    tcpLogInfo.Info(string.Format("【挂片站上线请求响应pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //    SendMessage(message);
        //    tcpLogInfo.Info(string.Format("【挂片站上线请求响应pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //}
        ///// <summary>
        ///// 【衣架进站pc---->硬件】
        ///// </summary>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="hangerNo"></param>
        //public void HangerArriveStating(string mainTrackNo, string statingNo, string hangerNo, string xor = null)
        //{
        //    //01 04 05 XX 00 50 00 AA BB CC DD EE
        //    var message = new SusNet.Common.SusBusMessage.HangerArrivalStatingResponeMessage(mainTrackNo, statingNo, "00", "50", hangerNo, xor);
        //    tcpLogInfo.Info(string.Format("【衣架进站pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //    SendMessage(message);
        //    tcpLogInfo.Info(string.Format("【衣架进站pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        //}
        /// <summary>
        /// 给衣架分配下一站点
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void AllocationHangerToNextStating(string mainTrackNo, string statingNo, string hangerNo, string xor = null)
        {
            //01 04 03 XX 00 51 00 AA BB CC DD EE
            var message = new SusNet.Common.SusBusMessage.AllocationHangerRequestMessage(mainTrackNo, statingNo, hangerNo, xor);
            tcpLogInfo.Info(string.Format("【给衣架分配下一站点pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【给衣架分配下一站点pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }

        /// <summary>
        /// pc对硬件返工的响应
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void HangerReworkResponse(int mainTrackNo, int statingNo, int hangerNo, int flowCode, string xor = null)
        {
            // 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
            //01 02 05 XX 00 56 01 AA BB CC DD EE
            if (string.IsNullOrEmpty(xor))
                xor = "00";
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                   HexHelper.TenToHexString2Len(statingNo),
               // "05",
               SuspeConstants.cmd_ReturnWork_Res,
                  xor,
                SuspeConstants.address_ReturnWork_Request,
                HexHelper.TenToHexString2Len(flowCode),
                StringUtils.ToFixLenStringFormat(HexHelper.TenToHexString2Len(hangerNo)));
            tcpLogInfo.Info(string.Format("【pc对硬件返工的响应---->硬件】发送开始,消息:--->{0}", message));
            SendData(HexHelper.StringToHexByte(message));
            tcpLogInfo.Info(string.Format("【pc对硬件返工的响应---->硬件】发送完成，消息:--->{0}", message));
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
        public void HangerDropCardProcessFlowCompare(string mainTrackNo, string statingNo, string hangerNo, int tag, string xor = null)
        {
            //01 04 05 XX 00 54 00 AA BB CC DD EE

            var message = new SusNet.Common.SusBusMessage.HangerDropCardResponseMessage(mainTrackNo, statingNo, hangerNo, tag, xor);
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message);
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainTrackNo"></param>
        public void UpperComputerPowerSupplyInit(int mainTrackNo)
        {
            //01 04 05 XX 00 54 00 AA BB CC DD EE
            var message =  UpperComputerInitRequestMessage.GetUpperComputerInitRequestMessage(mainTrackNo);
            tcpLogInfo.Info(string.Format("【上电启动站点初始化信息 pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message)));
            SendData(message);
            tcpLogInfo.Info(string.Format("【上电启动站点初始化信息 pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message)));
        }
        //public void Test()
        //{
        //    var productsModel = new ProductsModel();
        //    var data = string.Format("933304-9BUY,010,28,任务1867件,单位1件,累计出10000件,今日出213件");
        //    var hexData = SusNet.Common.Utils.HexHelper.ToHex(data, "utf-8", false);
        //}

        ///// <summary>
        ///// 向挂片站发送制品信息
        ///// </summary>
        ///// <param name="products"></param>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="productNumber"></param>
        ///// <param name="xor"></param>
        //public void BindProudctsToHangingPiece(List<byte> products, string mainTrackNo, string statingNo, int productNumber, bool isEnd, string xor = null)
        //{
        //    if (productNumber > 255)
        //    {
        //        var ex = new ApplicationException("排产号超过最大值255!不能挂片!");
        //        log.Error("【向挂片站发送制品信息】", ex);
        //        throw ex;
        //    }
        //    if (products.Count > 46)
        //    {
        //        var ex = new ApplicationException("制品信息超过46个字节!不能挂片!");
        //        log.Error("【向挂片站发送制品信息】", ex);
        //        throw ex;
        //    }
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = "00";
        //    }
        //    var endData = string.Format("{0} {1} 03 {2} 60 0F FF FF FF FF FF FF", mainTrackNo, statingNo, xor);
        //    //var data = string.Format("{0} {1} {2} {3} {60}", mainTrackNo, statingNo, "03", xor, "60");
        //    //var index = 0;

        //    var j = 0;
        //    for (var i = 0; i < 8; i++)
        //    {
        //        var sendDataList = new List<byte>();
        //        var sData = BindProcessOrderHangingPieceRequestMessage.GetHeaderBytes(mainTrackNo, statingNo, "60", string.Format("0{0}", i), productNumber, i, xor);
        //        sendDataList.AddRange(sData);
        //        if (j < products.Count)
        //        {
        //            for (int b = j; j < products.Count; j++)
        //            {
        //                if (sendDataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                sendDataList.Add(products[j]);
        //            }
        //        }
        //        var teLen = sendDataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (sendDataList.Count == 12)
        //            {
        //                break;
        //            }
        //            sendDataList.AddRange(HexHelper.strToToHexByte("00"));
        //        }
        //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", i, HexHelper.byteToHexStr(sendDataList.ToArray())));
        //        client.Send(sendDataList.ToArray());

        //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.byteToHexStr(sendDataList.ToArray())));

        //    }
        //    if (isEnd)
        //    {
        //        client.Send(HexHelper.strToToHexByte(endData));
        //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【主轨号+站号+排产号:{0}】 消息:--->{1}", string.Format("{0},{1},{2}", mainTrackNo, statingNo, productNumber), endData));
        //    }

        //    //for (var b= 0;b<products.Count;b++) {
        //    //    if (index == 0) {
        //    //        var fisrtData = string.Format(data+" 0{0} {1}", index, productNumber);
        //    //        sendData.AddRange(HexHelper.strToToHexByte(fisrtData));
        //    //        var bTemp = new byte[5];
        //    //        products.CopyTo(bTemp, b);
        //    //        sendData.AddRange(bTemp);
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", index, HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        b = 4;
        //    //        index++;
        //    //        client.Send(sendData.ToArray());
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}",index, SusNet.Common.Utils.HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        continue;
        //    //    }
        //    //    if (sendData.Count==12) {
        //    //        sendData = new List<byte>();
        //    //        var fisrtData = string.Format(data + " 0{0} {1}", index, productNumber);
        //    //        sendData.AddRange(HexHelper.strToToHexByte(fisrtData));
        //    //        var bTemp = new byte[6];
        //    //        products.CopyTo(bTemp, b+1);
        //    //        sendData.AddRange(bTemp);
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", index, HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        b = b+6;
        //    //        client.Send(sendData.ToArray());
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}",index, SusNet.Common.Utils.HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        index++;
        //    //    }

        //    //}
        //}
        public void ClearHangingPieceStatingInfo(string mainTrackNo, string statingNo, int productNumber)
        {
            var sendDataList = new List<byte>();
            var addresList = SusNetContext.ProductNumberHeadAddresList[productNumber];
            var messageHeader = BindProcessOrderHangingPieceRequestMessage.GetHeaderBytesNew(mainTrackNo, statingNo, addresList, null);
            sendDataList.AddRange(messageHeader);
            for (var index = 0; index < 6; index++)
            {
                sendDataList.AddRange(HexHelper.StringToHexByte("FF"));
            }
            tcpLogInfo.Info(string.Format("【清除挂片站制品信息pc---->硬件】发送开始 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
            client.Send(sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【清除挂片站制品信息pc---->硬件】发送完成 消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
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
        public void BindProudctsToHangingPieceNew(List<byte> products, string mainTrackNo, string statingNo, int productNumber, string xor = null)
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
                client.Send(sendDataList.ToArray());
                tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", index, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                index++;
            }
        }

        ///// <summary>
        ///// 【协议2.0】清除挂片站的在制品数据
        ///// </summary>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="productNumber"></param>
        ///// <param name="xor"></param>
        //public void ClearHangingPieceProducts(string mainTrackNo, string statingNo, int productNumber, string xor = null)
        //{
        //    if (productNumber > 255)
        //    {
        //        var ex = new ApplicationException("排产号超过最大值255!不能挂片! productNumber：" + productNumber);
        //        tcpLogError.Error("【清除挂片站制品信息】", ex);
        //        throw ex;
        //    }
        //    var clearMessageAddress = SusNetContext.ProductNumberHeadAddresList[productNumber];
        //    var message = BindProcessOrderHangingPieceRequestMessage.GetClearProductsMessage(mainTrackNo, statingNo, clearMessageAddress);
        //    tcpLogInfo.Info(string.Format("【清除挂片站制品信息pc---->硬件】发送开始,消息:--->{0}", HexHelper.BytesToHexString(message)));
        //    client.Send(message);
        //    tcpLogInfo.Info(string.Format("【清除挂片站制品信息pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(message)));

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <param name="address"></param>
        /// <param name="isEnd"></param>
        /// <param name="xor"></param>
        //public void BindProudctsToHangingPieceExt(List<byte> products, string mainTrackNo, string statingNo, int productNumber, ref int address, bool isEnd, string xor = null)
        //{
        //    if (productNumber > 255)
        //    {
        //        var ex = new ApplicationException("排产号超过最大值255!不能挂片!");
        //        tcpLogError.Error("【向挂片站发送制品信息】", ex);
        //        throw ex;
        //    }
        //    if (products.Count > 46)
        //    {
        //        var ex = new ApplicationException("制品信息超过46个字节!不能挂片!");
        //        tcpLogError.Error("【向挂片站发送制品信息】", ex);
        //        throw ex;
        //    }
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = "00";
        //    }

        //    //var data = string.Format("{0} {1} {2} {3} {60}", mainTrackNo, statingNo, "03", xor, "60");
        //    //var index = 0;

        //    var j = 0;
        //    for (var i = 0; i < 8; i++)
        //    {
        //        var sendDataList = new List<byte>();
        //        var sData = BindProcessOrderHangingPieceRequestMessage.GetHeaderBytesExt(mainTrackNo, statingNo, address, productNumber, i, xor);
        //        sendDataList.AddRange(sData);
        //        if (j < products.Count)
        //        {
        //            for (int b = j; j < products.Count; j++)
        //            {
        //                if (sendDataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                sendDataList.Add(products[j]);
        //            }
        //        }
        //        var teLen = sendDataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (sendDataList.Count == 12)
        //            {
        //                break;
        //            }
        //            sendDataList.AddRange(HexHelper.StringToHexByte("00"));
        //        }
        //        tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", i, HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        client.Send(sendDataList.ToArray());

        //        tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        address++;
        //    }
        //    if (isEnd)
        //    {
        //        var endData = string.Format("{0} {1} 03 {2} {3} FF FF FF FF FF FF", mainTrackNo, statingNo, xor, address);
        //        client.Send(HexHelper.StringToHexByte(endData));
        //        tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【主轨号+站号+排产号:{0}】 消息:--->{1}", string.Format("{0},{1},{2}", mainTrackNo, statingNo, productNumber), endData));
        //        address++;
        //    }

        //    //for (var b= 0;b<products.Count;b++) {
        //    //    if (index == 0) {
        //    //        var fisrtData = string.Format(data+" 0{0} {1}", index, productNumber);
        //    //        sendData.AddRange(HexHelper.strToToHexByte(fisrtData));
        //    //        var bTemp = new byte[5];
        //    //        products.CopyTo(bTemp, b);
        //    //        sendData.AddRange(bTemp);
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", index, HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        b = 4;
        //    //        index++;
        //    //        client.Send(sendData.ToArray());
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}",index, SusNet.Common.Utils.HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        continue;
        //    //    }
        //    //    if (sendData.Count==12) {
        //    //        sendData = new List<byte>();
        //    //        var fisrtData = string.Format(data + " 0{0} {1}", index, productNumber);
        //    //        sendData.AddRange(HexHelper.strToToHexByte(fisrtData));
        //    //        var bTemp = new byte[6];
        //    //        products.CopyTo(bTemp, b+1);
        //    //        sendData.AddRange(bTemp);
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", index, HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        b = b+6;
        //    //        client.Send(sendData.ToArray());
        //    //        log.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}",index, SusNet.Common.Utils.HexHelper.byteToHexStr(sendData.ToArray())));
        //    //        index++;
        //    //    }

        //    //}
        //}
        /// <summary>
        /// 发送立即显示数据
        /// </summary>
        /// <param name="hexData">字节数组</param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void SendShowData(List<byte> hexData, int mainTrackNo, int statingNo)
        {
            if (null == hexData)
            {
                var ex = new ApplicationException("发送数据不能为空");
                tcpLogError.Error("【SendShowData】", ex);
                throw ex;
            }
            if (hexData.Count > 192)
            {
                var ex = new ApplicationException("发送数据超过最大字节数192");
                tcpLogError.Error("【SendShowData】", ex);
                throw ex;
            }
            var j = 0;
            var times = 0;
            if (hexData.Count % 6 == 0)
            {
                times = hexData.Count / 6;
            }
            else
            {
                times = 1 + hexData.Count / 6;
            }
            var xor = "00";
            var beginAddress = 256;
            var hexMainTrackNo = HexHelper.TenToHexString2Len(mainTrackNo);
            var hexStatingNo = HexHelper.TenToHexString2Len(statingNo);
            for (var i = 0; i < times; i++)
            {
                var sendDataList = new List<byte>();
                string hexStr = string.Format("{0} {1} {2} {3} {4}", hexData, hexStatingNo, "05", xor, HexHelper.TenToHexString2Len(beginAddress));
                var sData = HexHelper.StringToHexByte(hexStr);
                sendDataList.AddRange(sData);
                if (j < hexData.Count)
                {
                    for (int b = j; j < hexData.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(hexData[j]);
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
                tcpLogInfo.Info(string.Format("【SendShowData】发送开始,【序号:{0}】 消息:--->{1}", i, HexHelper.BytesToHexString(sendDataList.ToArray())));
                client.Send(sendDataList.ToArray());

                tcpLogInfo.Info(string.Format("【SendShowData】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                beginAddress++;
            }
            var endData = string.Format("{0} {1} 05 {2} 01 20 00 00 00 00 00 00", hexData, hexStatingNo, xor);
            client.Send(HexHelper.StringToHexByte(endData));
            tcpLogInfo.Info(string.Format("【SendShowData】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo), endData));
        }

        /// <summary>
        /// 【协议2.0】 客户机上线与挂片站上线产量数据推送
        /// 任务数256件(2字节)
        ///累计完成100件(2字节)
        ///本日完成20件(2字节)
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendOutputDataToHangingPiece(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
        {
            if (null == onlineInfo || onlineInfo.Count == 0)
            {
                var ex = new ApplicationException(string.Format("发送内容不能为空! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【客户机上线与挂片站上线产量数据推送】", ex);
                throw ex;
            }
            if (onlineInfo.Count > 6)
            {
                var ex = new ApplicationException(string.Format("发送内容超过6个字节! 主轨:{0} 站点:{1}", mainTrackNo, statingNo));
                tcpLogError.Error("【客户机上线与挂片站上线产量数据推送】", ex);
                throw ex;
            }

            var sendDataList = new List<byte>();
            var data = ProductsDirectOnlineRequestMessage.GetHeaderBytes(mainTrackNo, statingNo);
            sendDataList.AddRange(data);
            sendDataList.AddRange(onlineInfo.ToArray());
            tcpLogInfo.Info(string.Format("【客户机上线与挂片站上线产量数据推送 pc---->硬件】发送开始,消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
            client.Send(sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【客户机上线与挂片站上线产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }

        public void SendDataByCommonSiteOutSite(List<byte> sData, string mainTrackNo, string statingNo)
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
            client.Send(sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【普通站衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        /// <summary>
        /// 返工衣架出站产量推送
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void SendDataByReworkSiteOutSite(List<byte> sData, string mainTrackNo, string statingNo)
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
            client.Send(sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【返工衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        /// <summary>
        /// 不能返工的提示信息推送(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag"></param>
        /// <param name="xor"></param>
        public void SendReworkException(int mainTrackNo, int statingNo, int hangerNo, int tag, string xor = null)
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
            client.Send(data);
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
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, string xor = null)
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
            client.Send(data);
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
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag,List<byte> promptData, string xor = null)
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
            client.Send(data);
            tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }


        #region 注释掉代码
        ///// <summary>
        ///// 制品界面直接上线：上线信息【制单号，颜色，尺码，任务数量，单位，累计完成，今日上线】发送
        ///// </summary>
        ///// <param name="onlineInfo"></param>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="xor"></param>
        //public void SendDataByProductsDirectOnline(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
        //{
        //    if (null == onlineInfo || onlineInfo.Count == 0)
        //    {
        //        var ex = new ApplicationException("发送内容不能为空!");
        //        tcpLogError.Error("【制品界面直接上线】", ex);
        //        throw ex;
        //    }
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = "00";
        //    }
        //    var j = 0;
        //    var times = 0;
        //    if (onlineInfo.Count % 6 == 0)
        //    {
        //        times = onlineInfo.Count / 6;
        //    }
        //    else
        //    {
        //        times = 1 + onlineInfo.Count / 6;
        //    }
        //    for (var i = 0; i < times; i++)
        //    {
        //        var sendDataList = new List<byte>();
        //        var sData = ProductsDirectOnlineRequestMessage.GetHeaderBytes(mainTrackNo, statingNo, "01", string.Format("{0:00}", i), i, xor);
        //        sendDataList.AddRange(sData);
        //        if (j < onlineInfo.Count)
        //        {
        //            for (int b = j; j < onlineInfo.Count; j++)
        //            {
        //                if (sendDataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                sendDataList.Add(onlineInfo[j]);
        //            }
        //        }
        //        var teLen = sendDataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (sendDataList.Count == 12)
        //            {
        //                break;
        //            }
        //            sendDataList.AddRange(HexHelper.StringToHexByte("00"));
        //        }
        //        tcpLogInfo.Info(string.Format("【制品界面直接上线 pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", i, HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        client.Send(sendDataList.ToArray());

        //        tcpLogInfo.Info(string.Format("【制品界面直接上线pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

        //    }
        //    var endData = string.Format("{0} {1} 05 {2} 01 20 00 00 00 00 00 00", mainTrackNo, statingNo, xor);
        //    client.Send(HexHelper.StringToHexByte(endData));
        //    tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo), endData));

        //}


        ///// <summary>
        ///// 挂片站上线，制品信息回写到硬件：上线信息【制单号，颜色，尺码，任务数量，单位，累计完成，今日上线】发送
        ///// </summary>
        ///// <param name="onlineInfo"></param>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="xor"></param>
        //public void SendDataByHangingPieceOnline(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
        //{
        //    if (null == onlineInfo || onlineInfo.Count == 0)
        //    {
        //        var ex = new ApplicationException("发送内容不能为空!");
        //        tcpLogError.Error("【挂片站上线】", ex);
        //        throw ex;
        //    }
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = "00";
        //    }
        //    var j = 0;
        //    var times = 0;
        //    if (onlineInfo.Count % 6 == 0)
        //    {
        //        times = onlineInfo.Count / 6;
        //    }
        //    else
        //    {
        //        times = 1 + onlineInfo.Count / 6;
        //    }
        //    for (var i = 0; i < times; i++)
        //    {
        //        var sendDataList = new List<byte>();
        //        var sData = HangingPieceStatingOnlineResponseMessage.GetHeaderBytes(mainTrackNo, statingNo, "01", string.Format("{0:00}", i), i, xor);
        //        sendDataList.AddRange(sData);
        //        if (j < onlineInfo.Count)
        //        {
        //            for (int b = j; j < onlineInfo.Count; j++)
        //            {
        //                if (sendDataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                sendDataList.Add(onlineInfo[j]);
        //            }
        //        }
        //        var teLen = sendDataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (sendDataList.Count == 12)
        //            {
        //                break;
        //            }
        //            sendDataList.AddRange(HexHelper.StringToHexByte("00"));
        //        }
        //        tcpLogInfo.Info(string.Format("【挂片站上线 pc---->硬件】发送开始,【序号:{0}】 消息:--->{1}", i, HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        client.Send(sendDataList.ToArray());

        //        tcpLogInfo.Info(string.Format("【挂片站上线 pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

        //    }
        //    var endData = string.Format("{0} {1} 05 {2} 01 {3} 00 00 00 00 00 00", mainTrackNo, statingNo, xor, string.Format("{0:00}", times));
        //    client.Send(HexHelper.StringToHexByte(endData));
        //    tcpLogInfo.Info(string.Format("【挂片站上线 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo), endData));

        //}

        ///// <summary>
        ///// 衣架落入读卡器,衣架携带制品信息推送：制品信息【产品及工艺信息，制单号，颜色，尺码，单位，工序：工序号，工艺信息】发送
        ///// </summary>
        ///// <param name="onlineInfo"></param>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="xor"></param>
        //public void SendDataByHangerDropCardCompare(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
        //{
        //    if (null == onlineInfo || onlineInfo.Count == 0)
        //    {
        //        var ex = new ApplicationException("发送内容不能为空!");
        //        tcpLogError.Error("【衣架落入读卡器,衣架携带制品信息推送】", ex);
        //        throw ex;
        //    }
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = "00";
        //    }
        //    var j = 0;
        //    var times = 0;
        //    if (onlineInfo.Count % 6 == 0)
        //    {
        //        times = onlineInfo.Count / 6;
        //    }
        //    else
        //    {
        //        times = 1 + onlineInfo.Count / 6;
        //    }
        //    for (var i = 0; i < times; i++)
        //    {
        //        var sendDataList = new List<byte>();
        //        var sData = HangerDropCardResponseMessage.GetHeaderBytes(mainTrackNo, statingNo, "01", string.Format("{0:00}", i), i, xor);
        //        sendDataList.AddRange(sData);
        //        if (j < onlineInfo.Count)
        //        {
        //            for (int b = j; j < onlineInfo.Count; j++)
        //            {
        //                if (sendDataList.Count == 12)
        //                {
        //                    break;
        //                }
        //                sendDataList.Add(onlineInfo[j]);
        //            }
        //        }
        //        var teLen = sendDataList.Count;
        //        for (var ii = 0; ii < 12 - teLen; ii++)
        //        {
        //            if (sendDataList.Count == 12)
        //            {
        //                break;
        //            }
        //            sendDataList.AddRange(HexHelper.StringToHexByte("00"));
        //        }
        //        tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        //Console.WriteLine();
        //        //var sendDataListResult = new List<byte>();
        //        //var dataBytes = new List<byte>();
        //        //var headBytes = new List<byte>();
        //        ////sendDataList.CopyTo(dataBytes, 5);
        //        ////sendDataList.CopyTo(headBytes, 0);
        //        //for (var index = 0; index < sendDataList.Count; index++)
        //        //{
        //        //    if (index <= 5)
        //        //    {
        //        //        headBytes.Add(sendDataList[index]);
        //        //    }
        //        //    else
        //        //    {
        //        //        dataBytes.Add(sendDataList[index]);
        //        //    }
        //        //}
        //        //dataBytes.Reverse();
        //        //sendDataListResult.AddRange(headBytes);
        //        //var tt = new List<byte>();
        //        //var count = 1;
        //        //for (var index = 0; index < dataBytes.Count; index++)
        //        //{
        //        //    if (index != 0 && count % 2 == 0)
        //        //    {
        //        //        tt.Add(dataBytes[index]);
        //        //        var ttBytes = new byte[2];
        //        //        tt.CopyTo(ttBytes);
        //        //        Array.Reverse(ttBytes);
        //        //        sendDataListResult.AddRange(ttBytes);
        //        //        tt = new List<byte>();
        //        //    }
        //        //    else {
        //        //        tt.Add(dataBytes[index]);
        //        //    }
        //        //    count++;
        //        //}
        //        //sendDataListResult.AddRange(dataBytes);
        //        var bs = sendDataList.ToArray();//sendDataList.ToArray();
        //        //Array.Reverse(bs);
        //        client.Send(bs);
        //        Console.WriteLine(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
        //        tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

        //    }
        //    var endData = string.Format("{0} {1} 05 {2} 01 {3} 00 00 00 00 00 00", mainTrackNo, statingNo, xor, string.Format("{0:00}", times));
        //    client.Send(HexHelper.StringToHexByte(endData));
        //    tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo), endData));

        //}
        #endregion

        //// <summary>
        /// zxl 2018年4月4日 22:56:36 
        /// 地址调整为0160-017F
        /// 衣架落入读卡器,衣架携带制品信息推送：制品信息【产品及工艺信息，制单号，颜色，尺码，单位，工序：工序号，工艺信息】发送
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByHangerDropCardCompareExt(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
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
                client.Send(bs);
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
        public void SendDataByHangerDropCardCompareExt2(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
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
                client.Send(bs);
                Console.WriteLine(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【衣架落入读卡器,衣架携带制品信息推送 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }
        /// <summary>
        /// 手动离线员工
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="type">3:登录;4:登出</param>
        /// <param name="cardNo"></param>
        public void ManualEmployeeOffline(int mainTrackNo, int statingNo, int type, int cardNo)
        {
            var cardRepatRes = new CardResponseMessage(HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo)
                , type, HexHelper.TenToHexString10Len(cardNo), null);

            var sData = cardRepatRes.GetBytes();
            tcpLogInfo.Info(string.Format("【手动离线员工 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(sData)));
            client.Send(sData);
            tcpLogInfo.Info(string.Format("【手动离线员工 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(sData)));
        }
        /// <summary>
        /// 员工登录信息推送，[工号]姓名
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByEmployeeLoginInfo(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null)
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
                client.Send(bs);
                Console.WriteLine(string.Format("【员工信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【员工信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【员工信息推送 ，发送信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }
        /// <summary>
        /// 清除衣架缓存
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void ClearHangerCache(int mainTrackNo, int statingNo, int hangerNo, string xor = null)
        {
            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var cRequest = new ClearHangerCacheRequestMessage(mainTrackNum, statingNoHex, hangerNo, xor);
            tcpLogInfo.Info(string.Format("【清除衣架缓存 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            client.Send(cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【清除衣架缓存 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        /// <summary>
        /// 上位机对站点类型的添加
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        /// <param name="data">16进制数据</param>
        /// <param name="type">01:修改;02:新增</param>
        public void SendStatingType(int mainTrackNo, string statingNo, int type, int data, string xor = null)
        {
            //【四字节00+维护类型+站类型】
            //01 02 03 XX 00 3F 00 00 00 01/02 XX

            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var statingTypeStr = HexHelper.TenToHexString2Len(data);
            var typeStr = HexHelper.TenToHexString2Len(type); //新增，修改

            var cRequest = new StatingTypeRequestMessage(mainTrackNum, statingNoHex, typeStr, statingTypeStr);
            tcpLogInfo.Info(string.Format("【修改/添加站点类型 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            client.Send(cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【修改/添加站点类型 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        /// <summary>
        /// 上位机到硬件
        /// </summary>
        /// <param name="bytes"></param>
        public void SCMModifyStatingCapacitySuccess(short mainTrackNo, string statingNo, int capacity)
        {
            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var capacityHex = HexHelper.TenToHexString4Len(capacity);

            var cRequest = new StatingCapacityRequestMessage(mainTrackNum, statingNoHex, capacityHex, SuspeConstants.cmd_StatingCapacity_5);
            tcpLogInfo.Info(string.Format("【设置站容量（回复） pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            client.Send(cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【设置站容量（回复） pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        /// <summary>
        /// 修改站点容量
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="capacity"></param>
        /// <param name="xor"></param>
        public void ModifyStatingCapacity(int mainTrackNo, string statingNo, int capacity, string xor = null)
        {
            //01 02 03 XX 00 33 00 00 00 00 01 08（4字节00+2字节数据）
            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var capacityHex = HexHelper.TenToHexString4Len(capacity);

            var cRequest = new StatingCapacityRequestMessage(mainTrackNum, statingNoHex, capacityHex, SuspeConstants.cmd_StatingCapacity_3);

            tcpLogInfo.Info(string.Format("【修改站点容量 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            client.Send(cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【修改站点容量 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        #region 监测点相关
        /// <summary>
        /// 监测点设置
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="isOpen"></param>
        /// <param name="xor"></param>
        public void OpenOrCloseMainTrackStatingMonitor(int mainTrackNo, int statingNo, bool isOpen, string xor = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var state = isOpen ? SuspeConstants.state_Set_Monitor_Open : SuspeConstants.state_Set_Monitor_Close;
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Set_Monitor_Stating,
                xor,
                HexHelper.TenToHexString4Len(SuspeConstants.address_Set_Monitor_Stating),
                HexHelper.TenToHexString10Len(0),
                state);

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【监测点设置推送 pc---->硬件】发送开始,消息:--->{0}", message));
            client.Send(data);
            tcpLogInfo.Info(string.Format("【监测点设置推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        #endregion

        #region 暂停或者接收衣架
        /// <summary>
        /// 暂停或者接收衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        /// <param name="xor"></param>
        public void SuspendOrReceiveHanger(int mainTrackNo, int statingNo, int suspendReceive, string xor = null)
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
            client.Send(data);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 暂停或者接收衣架给下位机的响应
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        /// <param name="xor"></param>
        public void SuspendOrReceiveHangerReponseToLowerMachine(int mainTrackNo, int statingNo, int suspendReceive, string xor = null)
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
            client.Send(data);
            tcpLogInfo.Info(string.Format("【暂停或者接收衣架给下位机的响应 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        #endregion

        /// <summary>
        /// 初始化站内数(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="num"></param>
        /// <param name="xor"></param>
        public void SendStatingNum(int mainTrackNo, int statingNo, int num, string xor = null)
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
            client.Send(data);
            tcpLogInfo.Info(string.Format("【初始化站内数 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        /// <summary>
        /// 【满站查询】上位机初始化时查询下位机站点满站状态
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void QueryFullSite(int mainTrackNumber, int statingNo, string xor = null)
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
            client.Send(data);
            tcpLogInfo.Info(string.Format("【满站查询 pc---->硬件】发送完成，消息:--->{0}", message));
        }
    }
}

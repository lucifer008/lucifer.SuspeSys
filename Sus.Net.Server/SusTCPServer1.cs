using Newtonsoft.Json;
using Sus.Net.Common.Common;
using Sus.Net.Common.Constant;
using Sus.Net.Common.Entity;
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
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Sus.Net.Server
{
    public class SusTCPServer1 : SusLog
    {
        // private readonly static ILog log = LogManager.GetLogger(typeof(SusTCPServer1));
        private readonly AsyncTcpServer server;
        /// <summary>
        /// 接收到消息
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;


        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<TcpClientConnectedEventArgs> ClientConnected;

        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<TcpClientDisconnectedEventArgs> ClientDisconnected;

        public static Dictionary<string, StatingInfo> DictStatingInfo = new Dictionary<string, StatingInfo>();

        #region 成员变量
        static List<byte> hangingPieceProductList = new List<byte>();
        static List<byte> g_productDataList = new List<byte>();
        static List<byte> g_productsInfo = new List<byte>();

        static List<byte> g_HangingPieceOnlineProductsInfo = new List<byte>();
        static List<byte> g_HangerDropCardCamporesInfo = new List<byte>();



        public static Dictionary<string, List<SusNet.Common.Model.Hanger>> dicHangingPiece = new Dictionary<string, List<SusNet.Common.Model.Hanger>>();
        public static Dictionary<string, List<SusNet.Common.Model.Hanger>> dicCommonPiece = new Dictionary<string, List<SusNet.Common.Model.Hanger>>();

        #endregion

        #region 业务事件
        /// <summary>
        /// 衣架进站：终端向pc的通知消息时触发
        /// </summary>
        public event EventHandler<MessageEventArgs> HangerArrivalStatingMessageReceived;

        /// <summary>
        /// 启动主轨响应【终端向pc通知触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> StartMainTrackResponseMessageReceived;

        public void ManualEmployeeOffline(int mainTrackNo, int statingNo, int type, int cardNo)
        {
            var cardRepatRes = new CardResponseMessage(HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo)
                , type, HexHelper.TenToHexString10Len(cardNo), null);

            var sData = cardRepatRes.GetBytes();
            tcpLogInfo.Info(string.Format("【手动离线员工 pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(sData)));
            // client.Send(sData);
            SendMessage(cardRepatRes, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", cardRepatRes.XID)));
            tcpLogInfo.Info(string.Format("【手动离线员工 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(sData)));
        }

        /// <summary>
        /// 停止主轨响应【终端向pc通知时触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> StopMainTrackResponseMessageReceived;

        /// <summary>
        /// 急停主轨响应【终端向pc通知时触发】
        /// </summary>
        public event EventHandler<MessageEventArgs> EmergencyStopMainTrackResponseMessageReceived;

        public void OpenOrCloseMainTrackStatingMonitor(int mainTrackNumber, int statingNo, bool isOpen, string xor = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var state = isOpen ? SuspeConstants.state_Set_Monitor_Open : SuspeConstants.state_Set_Monitor_Close;
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNumber),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Set_Monitor_Stating,
                xor,
                HexHelper.TenToHexString4Len(SuspeConstants.address_Set_Monitor_Stating),
                HexHelper.TenToHexString10Len(0),
                state);

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【监测点设置推送 pc---->硬件】发送开始,消息:--->{0}", message));
            SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNumber)));
            tcpLogInfo.Info(string.Format("【监测点设置推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }

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

        /// <summary>
        /// 主轨上传
        /// </summary>
        public event EventHandler<MessageEventArgs> MainTrakNumberUploadRequestMessageMessageReceived;


        /// <summary>
        /// 重连后主轨查询上传
        /// </summary>
        public event EventHandler<MessageEventArgs> ReconnectedQueryMaintrackNumbertMessageReceived;

        /// <summary>
        /// 缺料呼叫
        /// </summary>
        public event EventHandler<MessageEventArgs> MaterialCallMessageReceived;

        /// <summary>
        /// 缺料呼叫上传
        /// </summary>
        public event EventHandler<MessageEventArgs> MaterialCallUploadMessageReceived;

        /// <summary>
        /// 终止缺料
        /// </summary>
        public event EventHandler<MessageEventArgs> MaterialCallStopRecivied;



        /// <summary>
        /// F2主轨上传
        /// </summary>
        public event EventHandler<MessageEventArgs> F2AssignHangerNoUploadMessageReceived;

        /// <summary>
        /// F2不跨主轨
        /// </summary>
        public event EventHandler<MessageEventArgs> F2AssignMessageReceived;
        /// <summary>
        /// 呼叫机修开始
        /// </summary>
        public event EventHandler<MessageEventArgs> CallMachineRepairMessageReceived;
        /// <summary>
        /// 呼叫管理开始
        /// </summary>
        public event EventHandler<MessageEventArgs> CallManagementStartMessageReceived;
        /// <summary>
        /// 呼叫停止
        /// </summary>
        public event EventHandler<MessageEventArgs> CallStopMessageReceived;

        /// <summary>
        /// 故障报修:故障代码下发请求
        /// </summary>
        public event EventHandler<MessageEventArgs> FaultRepairUploadStartRequestMessageReceived;

        /// <summary>
        /// 故障报修：报修请求
        /// </summary>
        public event EventHandler<MessageEventArgs> FaultRepairReqtRequestMessageReceived;
        /// <summary>
        /// 故障报修：报修
        /// </summary>

        public event EventHandler<MessageEventArgs> FaultRepairClothingTypeAndFaultCodeRequestMessageReceived;
        /// <summary>
        /// 故障报修 :终止报修
        /// </summary>
        public event EventHandler<MessageEventArgs> FaultRepairStopRequestMessageReceived;

        /// <summary>
        /// 故障报修：开始维修
        /// </summary>
        public event EventHandler<MessageEventArgs> FaultRepairStartRequestMessageReceived;

        /// <summary>
        /// 故障报修：完成维修
        /// </summary>
        public event EventHandler<MessageEventArgs> FaultRepairSucessRequestMessageReceived;
        #endregion

        public SusTCPServer1(int port)
        {
            server = new AsyncTcpServer(port) { Encoding = Encoding.UTF8 };
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectedEventArgs>(server_ClientDisconnected);
            //server.PlaintextReceived += new EventHandler<TcpDatagramReceivedEventArgs<string>>(server_PlaintextReceived);
            server.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(server_DatagramReceived);
            MessageFactory.Instance.Init(new ClientUserInfo("0", "0"));
        }

        //启动
        public void Start()
        {
            SusNetContext.InitProductsCMD();
            server.Start();
            //Log.Info("服务器 {0} 已经启动",server.ToString());
            tcpLogInfo.Info(string.Format("服务器 {0} 已经启动", server.ToString()));
        }

        public void Stop()
        {
            server.Stop();
            ClientManager.Instance.Clear();
            //Log.Info("服务器 {0} 已经停止",server.ToString());
            tcpLogInfo.Info(string.Format("服务器 {0} 已经停止", server.ToString()));
        }

        #region Socket相关方法
        //public void SendMessageToAll(string strMessage)
        //{
        //    try
        //    {
        //        Message message = MessageFactory.Instance.CreateMessage(strMessage);
        //        // Log.Info("已经给所有终端发送消息:{0}",message.Describe());
        //        log.InfoFormat(string.Format("已经给所有终端发送消息:{0}", message.Describe()));
        //        server.SendToAll(message.Encode());
        //    }
        //    catch (Exception ex)
        //    {
        //        log.FatalFormat("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
        //    }
        //}
        public void SendMessageToAll(byte[] data)
        {
            try
            {
                //Message message = MessageFactory.Instance.CreateMessage(strMessage);
                // Log.Info("已经给所有终端发送消息:{0}",message.Describe());
                tcpLogInfo.Info(string.Format("已经给所有终端发送消息:{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(data)));
                server.SendToAll(data);
            }
            catch (Exception ex)
            {
                tcpLogInfo.FatalFormat("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                tcpLogError.Error(ex);
            }
        }

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
            SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", mainTrackNo));
            tcpLogInfo.Info(string.Format("【清除挂片站制品信息pc---->硬件】发送完成 消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
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
        //public void SendMessage()
        //{

        //}
        //private void SendMessage(Message message, List<string> tcpClientEndPointKeys)
        //{
        //    try
        //    {
        //        if (null != tcpClientEndPointKeys)
        //        {
        //            //Log.Info("已经发送消息:{0}",message.Describe());
        //            //Log.Info("到客户端:");
        //            //Log.Info("[");
        //            log.Info(string.Format("已经发送消息:{0}", message.Describe()));
        //            log.Info(string.Format("到客户端:"));
        //            log.Info(string.Format("["));
        //            foreach (string clientKey in tcpClientEndPointKeys)
        //            {
        //                try
        //                {
        //                    //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
        //                    log.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
        //                    server.Send(clientKey, message.Encode());
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //                    log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //                }
        //            }
        //            //  Log.Info("]");
        //            log.Info("]");
        //        }
        //        else
        //        {
        //            // Log.Warn("客户端列表为空，取消发送！！！！！");
        //            log.Warn("客户端列表为空，取消发送！！！！！");
        //        }
        //    }
        //    catch (InvalidProgramException ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //    }
        //}
        private void SendMessages(SusNet.Common.Message.MessageBody message, List<string> tcpClientEndPointKeys)
        {
            try
            {
                if (null != tcpClientEndPointKeys)
                {
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    //Log.Info("到客户端:");
                    //Log.Info("[");
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", message.Describe()));
                    tcpLogHardware.Info(string.Format("到客户端:"));
                    tcpLogHardware.Info(string.Format("["));
                    foreach (string clientKey in tcpClientEndPointKeys)
                    {
                        try
                        {
                            //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
                            tcpLogHardware.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
                            server.Send(clientKey, message.GetBytes());
                        }
                        catch (Exception ex)
                        {
                            // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                            // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                            tcpLogHardware.Error(ex);
                        }
                    }
                    //  Log.Info("]");
                    tcpLogHardware.Info("]");
                }
                else
                {
                    // Log.Warn("客户端列表为空，取消发送！！！！！");
                    tcpLogError.Warn("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                //tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                //   tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }
        private void SendMessage(SusNet.Common.Message.MessageBody message, string clientKey)
        {
            try
            {
                if (null != clientKey)
                {
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    //Log.Info("到客户端:");
                    //Log.Info("[");
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", message.Describe()));
                    tcpLogHardware.Info(string.Format("到客户端:"));
                    tcpLogHardware.Info(string.Format("["));
                    try
                    {
                        //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
                        tcpLogHardware.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
                        server.Send(clientKey, message.GetBytes());
                    }
                    catch (Exception ex)
                    {
                        // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                        // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                        tcpLogError.Error(ex);
                    }
                    //  Log.Info("]");
                    tcpLogHardware.Info("]");
                }
                else
                {
                    // Log.Warn("客户端列表为空，取消发送！！！！！");
                    tcpLogInfo.Warn("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        public void ManualEmployeeLoginStating(int mainTrackNo, int statingNo, int type, int cardNo, string info)
        {
            var cardRes = new CardResponseMessage(HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo), 3, HexHelper.TenToHexString10Len(cardNo), SuspeConstants.XOR);
            SendMessageByByte(cardRes.GetBytes(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
            var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
            SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo), SuspeConstants.XOR);
        }

        public void SendMessageByByte(byte[] datas, ClientUserInfo clientInfo, TcpClient tcpClient = null)
        {
            try
            {
                if (null != clientInfo)
                {
                    //Log.Info("已经发送消息:{0}",message.Describe());
                    //Log.Info("到客户端:");
                    //Log.Info("[");
                    tcpLogHardware.Info(string.Format("已经发送消息:{0}", HexHelper.BytesToHexString(datas)));
                    tcpLogHardware.Info(string.Format("到客户端:"));
                    tcpLogHardware.Info(string.Format("["));
                    try
                    {
                        var clientKey = ClientManager.Instance.ClientKeyWithUserInfo(clientInfo);
                        if (clientKey == null || clientKey == "")
                        {
                            tcpLogError.ErrorFormat("客户端clientInfo-->[{0}] 找不到客户端!", clientInfo.ToString());
                        }
                        //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
                        tcpLogHardware.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
                        if (tcpClient != null)
                        {
                            server.Send(tcpClient, datas);
                        }
                        else
                        {
                            server.Send(clientKey, datas);
                        }

                    }
                    catch (Exception ex)
                    {
                        // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                        //tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                        tcpLogError.Error(ex);
                    }
                    //  Log.Info("]");
                    tcpLogHardware.Info("]");
                }
                else
                {
                    // Log.Warn("客户端列表为空，取消发送！！！！！");
                    tcpLogInfo.Error("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (InvalidProgramException ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }
        public void SendMessageWithCANs(SusNet.Common.Message.MessageBody message, List<ClientUserInfo> clientInfos)
        {
            try
            {
                if (null != clientInfos)
                {
                    tcpLogHardware.InfoFormat("将要发送消息:{0}", message.GetHexStr());
                    tcpLogHardware.InfoFormat("到客户端:");
                    tcpLogHardware.InfoFormat("[");
                    foreach (var info in clientInfos)
                    {
                        tcpLogInfo.InfoFormat("{0}", info.ToString());
                    }
                    tcpLogInfo.InfoFormat("]");
                    //SendMessage(MessageFactory.Instance.CreateMessage(strMessage), ClientManager.Instance.ClientKeyWithUserInfos(clientInfos));
                    SendMessageToCAN(message, ClientManager.Instance.ClientKeyWithUserInfos(clientInfos));


                }
                else
                {
                    tcpLogInfo.Warn("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (Exception ex)
            {
                //  tcpLogInfo.FatalFormat("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                tcpLogError.Error(ex);
            }
        }

        private void SendMessageToCAN(SusNet.Common.Message.MessageBody message, List<string> tcpClientEndPointKeys)
        {
            SendMessages(message, tcpClientEndPointKeys);
        }

        public void SendMessageWithCAN(SusNet.Common.Message.MessageBody message, ClientUserInfo clientInfo, TcpClient tcpClient = null)
        {
            try
            {

                if (null != clientInfo)
                {
                    tcpLogHardware.InfoFormat("将要发送消息:{0}", message.GetHexStr());
                    tcpLogHardware.InfoFormat("到客户端:");
                    tcpLogHardware.InfoFormat("[");
                    tcpLogHardware.InfoFormat("{0}", clientInfo.ToString());
                    tcpLogHardware.InfoFormat("]");
                    if (null != tcpClient)
                        server.Send(tcpClient, message.GetBytes());
                    else
                        SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(clientInfo));

                }
                else
                {
                    tcpLogError.Error(new ApplicationException("clientInfo为空!"));
                    tcpLogHardware.Warn("客户端列表为空，取消发送！！！！！");
                }
            }
            catch (Exception ex)
            {
                //tcpLogInfo.FatalFormat("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                tcpLogError.Error(ex);
            }
        }

        #endregion

        private void AckInfo(SusNet.Common.Message.MessageBody recvMessage, TcpClient tcpClient)
        {
            //Message message = MessageFactory.Instance.CreateMessage(string.Empty, Common.Message.MessageType.ACK, recvMessage.id);
            //SendMessage(message, new List<string> { ClientKey(tcpClient) });
            tcpLogHardware.Info(string.Format("Ack消息--->{0} 客户端{1}", recvMessage.GetHexStr(), ClientManager.Instance.ClientUserInfoDesc(ClientKey(tcpClient))));
        }
        private void Ack(SusNet.Common.Message.MessageBody rMessage, TcpClient tcpClient)
        {
            //SendMessage(rMessage, new List<string> { ClientKey(tcpClient) });
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
            // Log.Info("客户端:{0} 已连接",ClientKey(e.TcpClient));
            if (ClientConnected != null)
                ClientConnected(null, e);
            tcpLogHardware.Info(string.Format("客户端:{0} 已连接", ClientKey(e.TcpClient)));
        }

        private void server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            try
            {
                if (ClientDisconnected != null)
                    ClientDisconnected(null, e);
                //Log.Info("客户端:{0} 已断开",ClientKey(e.TcpClient));
                tcpLogHardware.Info(string.Format("客户端:{0} 已断开", ClientKey(e.TcpClient)));
                ClientManager.Instance.RemoveTcpClient(e.TcpClient);
            }
            catch (Exception ex)
            {
                //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                // tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }
        private readonly static object locObject = new object();
        private void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
          //  lock (locObject)
           // {
                DatagramReceivedHandler(e);
           // }
        }

        private void DatagramReceivedHandler(TcpDatagramReceivedEventArgs<byte[]> e)
        {
            try
            {
                TcpClient tcpClient = e.TcpClient;
                tcpLogHardware.InfoFormat("收到消息:【客户端: {0}】-->【消息:{1}】", ClientKey(tcpClient), BufferUtils.ByteToHexStr(e.Datagram));
                //List<Message> messageList = MessageProcesser.Instance.ProcessRecvData(ClientKey(tcpClient),e.Datagram);
                List<SusNet.Common.Message.MessageBody> messageList = SusNet.Common.SusBusMessage.MessageProcesser.Instance.ProcessRecvData(e.TcpClient.Client.RemoteEndPoint.ToString(), e.Datagram);
                foreach (var rMessage in messageList)
                {
                    //Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】",ClientKey(tcpClient),rMessage.Describe());
                    //心跳包特殊处理，不更新keys
                    if ("00 00 00 00 00 00 00 00 00 00 00 00".Equals(rMessage.GetHexStr()))
                    {
                        AckInfo(rMessage, e.TcpClient);
                    }
                    else
                    {

                    }
                    tcpLogHardware.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", ClientKey(tcpClient), rMessage.GetHexStr()));


                    if (null != MessageReceived)
                    {
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = rMessage;
                        try
                        {
                            MessageReceived(this, args);
                        }
                        catch (Exception ex)
                        {
                            //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                            //  tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                            tcpLogError.Error(ex);
                        }
                    }
                    #region 业务事件
                    //【启动主轨后响应】
                    var startmtMessage = SusNet.Common.SusBusMessage.StartMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != StartMainTrackResponseMessageReceived && null != startmtMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【启动主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【启动主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = startmtMessage;
                        StartMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【停止主轨后响应】
                    var smtMessage = SusNet.Common.SusBusMessage.StopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != StopMainTrackResponseMessageReceived && null != smtMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【停止主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【停止主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = smtMessage;
                        StopMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【急停主轨后响应】
                    var esmtMessage = SusNet.Common.SusBusMessage.EmergencyStopMainTrackResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != EmergencyStopMainTrackResponseMessageReceived && null != esmtMessage)
                    {
                        // tcpLogInfo.Info(string.Format("【急停主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【急停主轨后响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = esmtMessage;
                        EmergencyStopMainTrackResponseMessageReceived(this, args);
                        continue;
                    }
                    //【【停止接收衣架】终端按下【暂停键时】硬件通知pc】
                    var srhMessage = SusNet.Common.SusBusMessage.StopReceiveHangerRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != StopReceiveHangerRequestMessageReceived && null != srhMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【停止接收衣架】【终端按下【暂停键时】硬件通知pc消息】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = srhMessage;
                        StopReceiveHangerRequestMessageReceived(this, args);
                        continue;
                    }

                    //【衣架落入读卡器发送的请求，硬件发pc端时触发】
                    var hdcMessage = SusNet.Common.SusBusMessage.HangerDropCardRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerDropCardRequestMessageReceived && null != hdcMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架落入读卡器 硬件通知pc消息】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hdcMessage;
                        HangerDropCardRequestMessageReceived(this, args);
                        continue;
                    }

                    //【衣架进站】
                    var hasMessage = SusNet.Common.SusBusMessage.HangerArrivalStatingRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerArrivalStatingMessageReceived && null != hasMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【衣架进站】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架进站】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hasMessage;
                        HangerArrivalStatingMessageReceived(this, args);
                        continue;
                    }
                    //【衣架出站请求】
                    var hoMessage = SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerOutStatingRequestMessageReceived && null != hoMessage)
                    {


                        // tcpLogInfo.Info(string.Format("【衣架出站】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架出站请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hoMessage;
                        args.TcpClient = e.TcpClient;
                        // ThreadPool.QueueUserWorkItem(HangerOutStatingRequestMessageReceived(this,args));
                        HangerOutStatingRequestMessageReceived(this, args);
                        continue;
                    }
                    ////【协议2.0---->挂片站衣架信息上传请求】
                    //var hpsUploadHangerInfoMessage = HangingPieceHangerUploadRequestMessage.isEqual(rMessage.GetBytes());
                    //if (null != HangingPieceHangerUploadRequestMessageReceived && null != hpsUploadHangerInfoMessage)
                    //{
                    //    tcpLogInfo.Info(string.Format("【>挂片站衣架信息上传请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                    //    MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                    //    args.Tag = hpsUploadHangerInfoMessage;
                    //    HangingPieceHangerUploadRequestMessageReceived(this, args);
                    //    continue;
                    //}
                    //【分配工序到衣架成功回应】
                    var allMessage = SusNet.Common.SusBusMessage.AllocationHangerResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != AllocationHangerResponseMessageReceived && null != allMessage)
                    {
                        // tcpLogInfo.Info(string.Format("【分配工序到衣架成功回应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【分配工序到衣架成功回应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = allMessage;
                        AllocationHangerResponseMessageReceived(this, args);
                        continue;
                    }
                    //【制品界面上线】
                    var cmsResMessage = SusNet.Common.SusBusMessage.ClientMachineResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != ClientMachineResponseMessageReceived && null != cmsResMessage)
                    {
                        // tcpLogInfo.Info(string.Format("【制品界面上线来自硬件的响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【制品界面上线来自硬件的响应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cmsResMessage;
                        ClientMachineResponseMessageReceived(this, args);
                        continue;
                    }
                    //【挂片站上线】
                    var hpsOnlineResMessage = SusNet.Common.SusBusMessage.HangingPieceStatingOnlineRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangingPieceStatingOnlineMessageReceived && null != hpsOnlineResMessage)
                    {
                        //    tcpLogInfo.Info(string.Format("【挂片站上线】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【挂片站上线】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hpsOnlineResMessage;
                        HangingPieceStatingOnlineMessageReceived(this, args);
                        continue;
                    }
                    //【衣架返工】
                    var hangerReworkRequestMessage = SusNet.Common.SusBusMessage.ReworkRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != HangerReworkMessageReceived && null != hangerReworkRequestMessage)
                    {
                        //   tcpLogInfo.Info(string.Format("【衣架返工】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架返工】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = hangerReworkRequestMessage;
                        HangerReworkMessageReceived(this, args);
                        continue;
                    }
                    //【衣架返工工序及疵点代码】
                    var reworkFlowDefectRequestMessage = ReworkFlowDefectRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != reworkFlowDefectRequestMessage && null != ReworkFlowDefectRequestMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【衣架返工工序及疵点代码】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【衣架返工工序及疵点代码】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = reworkFlowDefectRequestMessage;
                        ReworkFlowDefectRequestMessageReceived(this, args);
                        continue;
                    }
                    //【卡片相关】
                    var cardRequestMessage = CardRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != cardRequestMessage && null != CardRequestMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【卡片相关】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【卡片相关】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cardRequestMessage;
                        args.TcpClient = e.TcpClient;
                        CardRequestMessageReceived(this, args);
                        continue;
                    }
                    //【清除衣架缓存】
                    var cHangerCacheResponse = ClearHangerCacheResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != cHangerCacheResponse && null != ClearHangerCacheResponseMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【清除衣架缓存】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【清除衣架缓存】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = cHangerCacheResponse;
                        ClearHangerCacheResponseMessageReceived(this, args);
                        continue;
                    }
                    //【监测点上传】
                    var monitorMessage = MonitorMessage.isEqual(rMessage.GetBytes());
                    if (null != monitorMessage && null != MonitorMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【监测点上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【监测点上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = monitorMessage;
                        MonitorMessageReceived(this, args);
                        continue;
                    }
                    //【满站上传】
                    var fullSiteMessage = FullSiteMessage.isEqual(rMessage.GetBytes());
                    if (null != fullSiteMessage && null != FullSiteMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【满站上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【满站上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = fullSiteMessage;
                        FullSiteMessageReceived(this, args);
                        continue;
                    }

                    //【修改站点容量】
                    var statingCapacityResponseMessage = StatingCapacityResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != statingCapacityResponseMessage)
                    {
                        //   tcpLogInfo.Info(string.Format("【修改站点容量】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【修改站点容量】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = statingCapacityResponseMessage;
                        StatingCapacityResponseMessageReceived(this, args);
                        continue;
                    }

                    //【修改站点类型】
                    var statingTypeResponseMessage = StatingTypeResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != statingCapacityResponseMessage)
                    {
                        //   tcpLogInfo.Info(string.Format("【修改站点类型】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【修改站点类型】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

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
                        // tcpLogInfo.Info(string.Format("【上电初始化】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【上电初始化】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = powerSupplyInitRequestMessage;



                        //  tcpLogInfo.Info(string.Format("【上电初始化-->主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【上电初始化-->主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        //MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = powerSupplyInitRequestMessage;
                        MainTrakNumberUploadRequestMessageMessageReceived(this, args);
                        ClientManager.Instance.AddOrUpdateTcpClient(rMessage.gid, rMessage.XID, tcpClient);

                        //上电初始化
                        PowerSupplyInitMessageReceived(this, args);
                        continue;
                    }
                    //SN序列号
                    var sNSerialNumberRequestMessage = SNSerialNumberRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != sNSerialNumberRequestMessage && null != SNSerialNumberMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【SN上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【SN上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = sNSerialNumberRequestMessage;
                        SNSerialNumberMessageReceived(this, args);
                        continue;
                    }
                    //主版版本号
                    var mainboardVersionRequestMessage = MainboardVersionRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != mainboardVersionRequestMessage && null != MainboardVersionMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【主版版本号上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【主版版本号上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = mainboardVersionRequestMessage;
                        MainboardVersionMessageReceived(this, args);
                        continue;
                    }

                    //下位机接收或暂停衣架
                    var lowerMachineSuspendOrReceiveMessage = LowerMachineSuspendOrReceiveMessage.isEqual(rMessage.GetBytes());
                    if (null != lowerMachineSuspendOrReceiveMessage && null != LowerMachineSuspendOrReceiveMessageReceived)
                    {
                        // tcpLogInfo.Info(string.Format("【下位机接收或暂停衣架上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【下位机接收或暂停衣架上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = lowerMachineSuspendOrReceiveMessage;
                        LowerMachineSuspendOrReceiveMessageReceived(this, args);
                        continue;
                    }
                    //上位机发起的上电初始化，硬件回应
                    var upperComputerInitResponseMessage = UpperComputerInitResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != upperComputerInitResponseMessage && null != UpperComputerInitResponseMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【上位机发起的上电初始化，硬件回应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【上位机发起的上电初始化，硬件回应】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.TcpClient = e.TcpClient;
                        args.Tag = upperComputerInitResponseMessage;
                        UpperComputerInitResponseMessageReceived(this, args);
                        continue;
                    }

                    //【满站查询状态上传】
                    var fullSiteQueryResponseMessage = FullSiteMessage.isFullSiteQueryEqual(rMessage.GetBytes());
                    if (null != fullSiteQueryResponseMessage && null != FullSiteQueryResponseMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【满站查询状态上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【满站查询状态上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = fullSiteQueryResponseMessage;
                        FullSiteQueryResponseMessageReceived(this, args);
                        continue;
                    }
                    //【主轨上传】
                    var mainTrakNumberUploadRequestMessage = MainTrakNumberUploadRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != mainTrakNumberUploadRequestMessage && null != MainTrakNumberUploadRequestMessageMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = mainTrakNumberUploadRequestMessage;
                        MainTrakNumberUploadRequestMessageMessageReceived(this, args);
                        ClientManager.Instance.AddOrUpdateTcpClient(rMessage.gid, rMessage.XID, tcpClient);
                        continue;
                    }
                    //【连接后主轨上传】
                    var connectedQueryMaintrackNumberResponseMessage = ConnectedQueryMaintrackNumberResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != connectedQueryMaintrackNumberResponseMessage)
                    {
                        //  tcpLogInfo.Info(string.Format("【连接后主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【连接后主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = connectedQueryMaintrackNumberResponseMessage;
                        //   ConnectedQueryMaintrackNumbertMessageReceived(this, args);
                        ClientManager.Instance.AddOrUpdateTcpClient(rMessage.gid, rMessage.XID, tcpClient);
                        continue;
                    }
                    //【重新连接后主轨上传
                    var reconnectedQueryMaintrackNumberResponseMessage = ReconnectedQueryMaintrackNumberResponseMessage.isEqual(rMessage.GetBytes());
                    if (null != reconnectedQueryMaintrackNumberResponseMessage && null != ReconnectedQueryMaintrackNumbertMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【重新连接后主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【重新连接后主轨上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = reconnectedQueryMaintrackNumberResponseMessage;
                        ReconnectedQueryMaintrackNumbertMessageReceived(this, args);
                        ClientManager.Instance.AddOrUpdateTcpClient(rMessage.gid, rMessage.XID, tcpClient);
                        continue;
                    }
                    //缺料呼叫相关
                    //【缺料呼叫】
                    var materialCallRequestMessage = MaterialCallRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != materialCallRequestMessage && null != MaterialCallMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【缺料呼叫】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【缺料呼叫】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));

                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = materialCallRequestMessage;
                        MaterialCallMessageReceived(this, args);
                        continue;
                    }
                    //MaterialCallUploadRequestMessage
                    //【缺料呼叫上传】
                    var materialCallUploadRequestMessage = MaterialCallUploadRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != materialCallUploadRequestMessage && null != MaterialCallUploadMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【缺料呼叫上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【缺料呼叫上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = materialCallUploadRequestMessage;
                        MaterialCallUploadMessageReceived(this, args);
                        continue;
                    }

                    var materialCallStop = MaterialCallStopRequestMessaage.isEqual(rMessage.GetBytes());
                    if (null != materialCallStop && null != MaterialCallStopRecivied)
                    {
                        //    tcpLogInfo.Info(string.Format("【取消呼叫】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【取消呼叫】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = materialCallStop;
                        MaterialCallStopRecivied(this, args);
                        continue;
                    }
                    //【F2指定相关】相关
                    //跨主轨 F2CrossMainTrackMessageReceived
                    var f2AssignMainTrackUploadRequestMessage = F2AssignHangerNoUploadRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != f2AssignMainTrackUploadRequestMessage && null != F2AssignHangerNoUploadMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【F2指定业务衣架号上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【F2指定业务衣架号上传】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = f2AssignMainTrackUploadRequestMessage;
                        F2AssignHangerNoUploadMessageReceived(this, args);
                        continue;
                    }
                    //F2指定业务
                    var f2AssignRequestMessage = F2AssignRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != f2AssignRequestMessage && null != F2AssignMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【F2指定业务】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【F2指定业务】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = f2AssignRequestMessage;
                        F2AssignMessageReceived(this, args);
                        continue;
                    }
                    //呼叫机修
                    var callMachineRepaireRequestMessage = CallMachineRepairStartRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != callMachineRepaireRequestMessage && null != CallMachineRepairMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【呼叫机修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【呼叫机修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = callMachineRepaireRequestMessage;
                        CallMachineRepairMessageReceived(this, args);
                        continue;
                    }
                    //呼叫管理
                    var callManganeRequestMessage = CallManagementStartRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != callManganeRequestMessage && null != CallManagementStartMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【呼叫管理】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【呼叫管理】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = callManganeRequestMessage;
                        CallManagementStartMessageReceived(this, args);
                        continue;
                    }
                    //呼叫(管理/机修)停止
                    var callStopRequestMessage = CallStopRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != callStopRequestMessage && null != CallStopMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【呼叫(管理/机修)停止】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【呼叫(管理/机修)停止】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = callStopRequestMessage;
                        CallStopMessageReceived(this, args);
                        continue;
                    }
                    #region 衣车部分
                    //故障报修故障代码上传请求
                    var faultRepairUploadStartRequestMessage = FaultRepairUploadStartRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairUploadStartRequestMessage && null != FaultRepairUploadStartRequestMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【故障报修故障代码上传请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【故障报修故障代码上传请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairUploadStartRequestMessage;
                        FaultRepairUploadStartRequestMessageReceived(this, args);
                        continue;
                    }
                    //故障报修故障代码请求
                    var faultRepairReqtRequestMessage = FaultRepairReqtRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairReqtRequestMessage && null != FaultRepairReqtRequestMessageReceived)
                    {
                        // tcpLogInfo.Info(string.Format("【故障报修故障代码请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【故障报修故障代码请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairReqtRequestMessage;
                        FaultRepairReqtRequestMessageReceived(this, args);
                        continue;
                    }
                    //故障报修类别及故障代码请求
                    var faultRepairClothingTypeAndFaultCodeRequestMessage = FaultRepairClothingTypeAndFaultCodeRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairClothingTypeAndFaultCodeRequestMessage && null != FaultRepairClothingTypeAndFaultCodeRequestMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【故障报修类别及故障代码请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【故障报修类别及故障代码请求】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairClothingTypeAndFaultCodeRequestMessage;
                        FaultRepairClothingTypeAndFaultCodeRequestMessageReceived(this, args);
                        continue;
                    }
                    //中止故障报修
                    var faultRepairStopRequestMessage = FaultRepairStopRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairStopRequestMessage && null != FaultRepairStopRequestMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【中止故障报修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【中止故障报修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairStopRequestMessage;
                        FaultRepairStopRequestMessageReceived(this, args);
                        continue;
                    }
                    //故障报修:开始维修
                    var faultRepairStartRequestMessage = FaultRepairStartRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairStartRequestMessage && null != FaultRepairStartRequestMessageReceived)
                    {
                        //   tcpLogInfo.Info(string.Format("【故障报修:开始维修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【故障报修:开始维修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairStartRequestMessage;
                        FaultRepairStartRequestMessageReceived(this, args);
                        continue;
                    }
                    //故障报修:完成维修
                    var faultRepairSucessRequestMessage = FaultRepairSucessRequestMessage.isEqual(rMessage.GetBytes());
                    if (null != faultRepairSucessRequestMessage && null != FaultRepairSucessRequestMessageReceived)
                    {
                        //  tcpLogInfo.Info(string.Format("【故障报修:完成维修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        tcpLogHardware.Info(string.Format("【故障报修:完成维修】服务器端收到客户端端消息:{0}", rMessage.GetHexStr()));
                        MessageEventArgs args = new MessageEventArgs(rMessage.GetHexStr());
                        args.Tag = faultRepairSucessRequestMessage;
                        FaultRepairSucessRequestMessageReceived(this, args);
                        continue;
                    }
                    #endregion
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
                    #endregion

                }
            }
            catch (Exception ex)
            {
                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                //tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                tcpLogError.Error(ex);
            }
        }

        public void UpperComputerPowerSupplyInit(IList<int> mainTrackNumberList, TcpClient tcpClient)
        {
            //01 04 05 XX 00 54 00 AA BB CC DD EE
            foreach (var mainTrackNumber in mainTrackNumberList)
            {
                var message = UpperComputerInitRequestMessage.GetUpperComputerInitRequestMessage(mainTrackNumber);
                tcpLogInfo.Info(string.Format("【上电启动站点初始化信息 pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message)));
                server.Send(tcpClient, message);
                tcpLogInfo.Info(string.Format("【上电启动站点初始化信息 pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message)));
            }
        }

        private static readonly object lockObject = new object();
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
            lock (lockObject)
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
                SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", mainTrackNo));
                tcpLogInfo.Info(string.Format("【客户机上线与挂片站上线产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
            }
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
            tcpLogInfo.Info(string.Format($"【清除衣架缓存 pc---->硬件】发送开始,【衣架:{hangerNo} 主轨:{mainTrackNo} 站点:{statingNo}】【消息:--->{HexHelper.BytesToHexString(cRequest.GetBytes())}】"));
            SendMessageByByte(cRequest.GetBytes(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format($"【清除衣架缓存 pc---->硬件】发送完成,【衣架:{hangerNo} 主轨:{mainTrackNo} 站点:{statingNo}】【消息:--->{HexHelper.BytesToHexString(cRequest.GetBytes())}】"));
        }
        //public void TestDataProcess(string strMessage, List<string> toCorpIds)
        //{
        //    try
        //    {
        //        List<ClientUserInfo> clientInfos = ClientManager.Instance.ClientInfoWithCorpIds(toCorpIds);
        //        //Log.Info("将要发送消息:{0}",strMessage);
        //        //Log.Info("到客户端:");
        //        //Log.Info("[");
        //        tcpLogInfo.Info(string.Format("将要发送消息:{0}", strMessage));
        //        tcpLogInfo.Info(string.Format("到客户端:"));
        //        tcpLogInfo.Info(string.Format("["));
        //        foreach (var info in clientInfos)
        //        {
        //            //Log.Info("{0}",info.ToString());
        //            tcpLogInfo.Info(string.Format("{0}", info.ToString()));
        //        }
        //        //  Log.Info("]");
        //        tcpLogInfo.Info(string.Format("]"));

        //        Message message = MessageFactory.Instance.CreateMessage(strMessage);
        //        List<string> tcpClientEndPointKeys = ClientManager.Instance.ClientKeyWithUserInfos(clientInfos);
        //        byte[] messageData = message.Encode();
        //        //Log.Info("已经发送消息:{0}",message.Describe());
        //        //Log.Info("到客户端:");
        //        //Log.Info("[");
        //        tcpLogInfo.Info(string.Format("已经发送消息:{0}", message.Describe()));
        //        tcpLogInfo.Info(string.Format("到客户端:"));
        //        tcpLogInfo.Info(string.Format("["));
        //        foreach (string clientKey in tcpClientEndPointKeys)
        //        {
        //            try
        //            {
        //                server.Send(clientKey, messageData);
        //                //Log.Info("{0}",ClientManager.Instance.ClientUserInfoDesc(clientKey));
        //                tcpLogInfo.Info(string.Format("{0}", ClientManager.Instance.ClientUserInfoDesc(clientKey)));
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //                //   tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //                tcpLogError.Error(ex);
        //            }
        //        }
        //        // Log.Info("]");
        //        tcpLogInfo.Info("]");

        //        List<Message> messageList = Common.Message.MessageProcesser.Instance.ProcessRecvData("0", messageData);
        //        foreach (var rMessage in messageList)
        //        {
        //            //Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】","0",rMessage.Describe());
        //            tcpLogInfo.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", "0", rMessage.Describe()));
        //            if (rMessage.body != null)
        //            {
        //                MessageBody messageBody = JsonConvert.DeserializeObject<MessageBody>(Encoding.UTF8.GetString(rMessage.body));
        //                if (messageBody != null)
        //                {
        //                    // Log.Info("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】","0",messageBody.Describe());
        //                    tcpLogInfo.Info(string.Format("收到消息完整解析消息:【客户端: {0}】-->【消息:{1}】", "0", messageBody.Describe()));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
        //        //tcpLogInfo.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
        //        tcpLogError.Error(ex);
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
        //        var hexStr = HexHelper.TenToHexString2Len(index);
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

        /// <summary>
        /// 发送立即显示数据
        /// </summary>
        /// <param name="hexData">字节数组</param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void SendShowData(List<byte> hexData, int mainTrackNo, int statingNo, TcpClient tcpClient = null)
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
                if (tcpClient == null)
                    SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
                else
                    server.Send(tcpClient, sendDataList.ToArray());

                tcpLogInfo.Info(string.Format("【SendShowData】发送完成，【序号:{0}】 消息:--->{1}", i, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                beginAddress++;
            }
            var endData = string.Format("{0} {1} 05 {2} 01 20 00 00 00 00 00 00", hexData, hexStatingNo, xor);
            //  client.Send(HexHelper.StringToHexByte(endData));
            SendMessageByByte(HexHelper.StringToHexByte(endData), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)), tcpClient);
            tcpLogInfo.Info(string.Format("【SendShowData】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo), endData));
        }


        public int GetProductNumber(string address)
        {
            var productNumber = 0;
            foreach (var k in ProductNumberHeadAddresList.Keys)
            {
                if (ProductNumberHeadAddresList[k].Equals(address))
                {
                    productNumber = k;
                    return productNumber;
                }
            }
            return 0;
        }
        /// <summary>
        /// 【协议2.0】排产号与地址的约定关系
        /// </summary>
        public static Dictionary<int, List<string>> ProductNumberAddresList = new Dictionary<int, List<string>>();
        /// <summary>
        /// 【协议2.0】排产号约定地址的首位地址
        /// </summary>
        public static Dictionary<int, string> ProductNumberHeadAddresList = new Dictionary<int, string>();

        /// <summary>
        /// 给衣架分配下一站点
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="hexHangerNo"></param>
        /// <param name="xor"></param>
        public void AllocationHangerToNextStating(string tenMainTrackNo, string tenStatingNo, string hexHangerNo, string xor = null)
        {
            //01 04 03 XX 00 51 00 AA BB CC DD EE
            var message = new SusNet.Common.SusBusMessage.AllocationHangerRequestMessage(tenMainTrackNo, tenStatingNo, hexHangerNo, xor);
            tcpLogInfo.Info(string.Format("【给衣架分配下一站点pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
            tcpLogInfo.Info(string.Format("【给衣架分配下一站点pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
        }
        public void F2AssginExceptionNotice(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int tag, TcpClient tcpClient = null)
        {
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
            HexHelper.TenToHexString2Len(sourceMainTrackNuber),
            HexHelper.TenToHexString2Len(sourceStatingNo),
            SuspeConstants.cmd_F2_Assign_Res,
            null,
            SuspeConstants.address_F2_Assign_Action,
            HexHelper.TenToHexString2Len(tag),
           hexHangerNo
            );

        }

        /// <summary>
        /// 无缺料明细数据
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag"></param>
        public void SendExceptionNotMaterial(int mainTrackNo, int statingNo, string xor = null)
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
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString2Len(0));

            //var sendDataList = new List<byte>();
            //var data = HexHelper.StringToHexByte(message);
            //tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送开始,消息:--->{0}", message));
            ////client.Send(data);

            var sendData = HexHelper.StringToHexByte(message);
            //this.SendMessage(data);
            //this.SendExcpetionOrPromptInfo
            SendMessageByByte(sendData, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format("【异常提示推送 无缺料明细数据 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 无缺料消息接收站点：01 08 FF 05 0200 00 00 00 00 00 00
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tcpClient"></param>
        public void MaterialCallUploadNotStating(int mainTrackNo, int statingNo, TcpClient tcpClient = null, string xor = null)
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_ExcpetionOrPromptInfo,
                xor,
                SuspeConstants.address_ExcpetionOrPromptInfo,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString2Len(0));


            var sendData = HexHelper.StringToHexByte(message);
            SendMessageByByte(sendData, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format("【无缺料消息接收站点 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 缺料呼叫成功  呼叫成功：01 08 FF 05 0220 00 00 00 00 00 00
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tcpClient"></param>
        /// <param name="xor"></param>
        public void MaterialCallUploadSuccess(int mainTrackNo, int statingNo, TcpClient tcpClient = null, string xor = null)
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_meterials_Res,
                xor,
                SuspeConstants.address_Lack_Materials_Call,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString2Len(0));


            var sendData = HexHelper.StringToHexByte(message);
            SendMessageByByte(sendData, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format("【缺料呼叫成功 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 发送缺料信息到接收站：1-8 缺少辅料
        /// 01 01 FF 03 021A 00 00 00 00 01 08
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="receviceStating"></param>
        /// <param name="tcpClient"></param>
        /// <param name="xor"></param>
        public void MaterialCallUploadSendToStating(int mainTrackNo, int statingNo, string receviceStating, TcpClient tcpClient = null, string xor = null)
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
               HexHelper.TenToHexString2Len(mainTrackNo),
               HexHelper.TenToHexString2Len(receviceStating),
               SuspeConstants.cmd_lack_Material_Send_to_Stating,
               xor,
               SuspeConstants.address_lack_Material_Send_to_Stating,
               HexHelper.TenToHexString2Len(mainTrackNo),
               HexHelper.TenToHexString2Len(statingNo));


            var sendData = HexHelper.StringToHexByte(message);
            SendMessageByByte(sendData, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format("【发送缺料信息到接收站 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 缺料终止
        /// 01 08 FF 05 0223 00 00 00 00 00 00
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="receviceStaring"></param>
        /// <param name="tcpClient"></param>
        /// <param name="xor"></param>
        public void MaterialCallStop(int mainTrackNo, string receviceStaring, TcpClient tcpClient = null, string xor = null)
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
               HexHelper.TenToHexString2Len(mainTrackNo),
               HexHelper.TenToHexString2Len(receviceStaring),
               SuspeConstants.cmd_Lack_Materials_Call_Stop_To_Stating,
               xor,
               SuspeConstants.address_Lack_Materials_Call_Stop_To_Stating,
               HexHelper.TenToHexString2Len(mainTrackNo),
               HexHelper.TenToHexString2Len(0));


            var sendData = HexHelper.StringToHexByte(message);
            SendMessageByByte(sendData, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            tcpLogInfo.Info(string.Format("【发送缺料终止信息到接收站 pc---->硬件】发送完成，消息:--->{0}", message));
        }

        /// <summary>
        /// 排产号与地址关系
        /// </summary>
        // public static Dictionary<string, int> AddressProductNumberList = new Dictionary<string, int>();

        #region 业务层
        #region 业务消息处理
        /// <summary>
        /// 员工登录信息推送，[工号]姓名
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByClothesCardLoginInfo(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
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
            var decBeginAddress = SuspeConstants.address_Send_Clothes_Cards_Login_Info_Begin;//0x0148
            var decEndAddress = SuspeConstants.address_Send_Clothes_Cards_Login_Info_End;//0x014F

            for (var i = 0; i < times; i++)
            {
                if (decBeginAddress > decEndAddress)
                {
                    var ex = new ApplicationException(string.Format("衣车登录信息推送超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【衣车登录信息推送】", ex);
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
                tcpLogInfo.Info(string.Format("【衣车登录信息推送 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                //server.Send(tcpClient, bs);
                if (null == tcpClient)
                    SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
                else
                    server.Send(tcpClient, bs);
                Console.WriteLine(string.Format("【衣车登录信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【衣车登录信息推送 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【衣车登录信息推送 ，发送信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mainTrackNo, statingNo)));

        }

        /// <summary>
        /// 员工登录信息推送，[工号]姓名
        /// </summary>
        /// <param name="onlineInfo"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        public void SendDataByEmployeeLoginInfo(List<byte> onlineInfo, string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
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
                //server.Send(tcpClient, bs);
                if (null == tcpClient)
                    SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
                else
                    server.Send(tcpClient, bs);
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
            // server.Send(tcpClient, data);
            SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
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
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, string xor = null, TcpClient tcpClient = null)
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
            //server.Send(tcpClient, data);
            if (tcpClient == null)
                SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
                server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【异常推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        /// <summary>
        /// 异常推送满站(pc--->硬件)
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="fullStatingNos"></param>
        /// <param name="FullStatingFlowNo"></param>
        /// <param name="xor"></param>
        /// <param name="tcpClient"></param>
        public void SendExcpetionOrPromptInfoByFullStating(int mainTrackNo, int statingNo, IList<string> fullStatingNos, string FullStatingFlowNo, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            if (null == fullStatingNos)
            {
                fullStatingNos = new List<string>();
            }
            var dataList = new List<byte>();
            var fullStatingFlowInfoBytes = AssicUtils.EncodeByStr("00" + FullStatingFlowNo);
            if (fullStatingFlowInfoBytes == null || fullStatingFlowInfoBytes.Length == 0)
            {
                dataList.AddRange(AssicUtils.EncodeByStr("00000"));
                tcpLogError.Error("【 异常推送满站(pc--->硬件)】满站站点和工序号都为空!");
            }
            else
            {
                dataList = FillInFullStatingDataBit(fullStatingFlowInfoBytes);
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_ExcpetionOrPromptInfo,
                xor,
                SuspeConstants.address_ExcpetionOrPromptInfo,
                HexHelper.TenToHexString2Len(SuspeConstants.tag_FullSiteOrStopWork_New),
                HexHelper.BytesToHexString(dataList.ToArray()));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【异常推送满站(pc--->硬件)】 满站站点:{0} 满站工序:{1} 发送开始,消息:--->{2}", string.Join(",", fullStatingNos), FullStatingFlowNo, message));
            //server.Send(tcpClient, data);
            if (tcpClient == null)
                SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
                server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【异常推送满站(pc--->硬件)】 满站站点:{0} 满站工序:{1} 发送完成，消息:--->{2}", string.Join(",", fullStatingNos), FullStatingFlowNo, message));
        }

        private List<byte> FillInFullStatingDataBit(byte[] fullStatingFlowInfoBytes)
        {
            var dataList = new List<byte>(fullStatingFlowInfoBytes);
            if (dataList.Count == 5) return dataList;
            var fillBitLen = 5 - fullStatingFlowInfoBytes.Length;
            var strList = string.Empty;
            for (var index = 0; index < fillBitLen; index++)
            {
                strList += "0";
            }

            dataList.AddRange(AssicUtils.EncodeByStr(strList));
            return dataList;
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public void Send(TcpClient tcpClient, byte[] datagram)
        {
            server.Send(tcpClient, datagram);
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
        public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, List<byte> promptData, string xor = null)
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
            //server.Send(tcpClient, data);
            SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
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
        public void HangerDropCardProcessFlowCompare(string mainTrackNo, string statingNo, string hangerNo, int tag, string xor = null)
        {
            //01 04 05 XX 00 54 00 AA BB CC DD EE

            var message = new SusNet.Common.SusBusMessage.HangerDropCardResponseMessage(mainTrackNo, statingNo, hangerNo, tag, xor);
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            SendMessageWithCAN(message, new ClientUserInfo("1", mainTrackNo));
            tcpLogInfo.Info(string.Format("【衣架落入读卡器工序比较 pc---->硬件】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
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
            // server.Send(tcpClient, data);
            SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
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
                                                //server.Send(tcpClient, bs);
                SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
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
                                                // server.Send(tcpClient, bs);
                SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
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
            // server.Send(tcpClient, sendDataList.ToArray());
            if (tcpClient == null)
                SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
                server.Send(tcpClient, sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【返工衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        #endregion

        /// <summary>
        /// 出站【对硬件是否出战的回应】
        /// </summary>
        /// <param name="hexMainTrackNo"></param>
        /// <param name="hexStatingNo"></param>
        /// <param name="isAuto"></param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public void AutoHangerOutStating(string hexMainTrackNo, string hexStatingNo, bool isAuto, int hangerNo, string xor = null)
        {
            lock (lockObject)
            {
                //01 44 05 XX 00 55 00 AA BB CC DD EE 允许出站
                var message = new SusNet.Common.SusBusMessage.HangerOutStatingResponseMessage(hexMainTrackNo, hexStatingNo, isAuto, hangerNo, xor);
                tcpLogInfo.Info(string.Format("【出站指令推送】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
                SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
                tcpLogInfo.Info(string.Format("【出站指令推送】发送完成，消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            }

        }
        /// <summary>
        /// 普通站衣架产量数据推送
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void SendDataByCommonSiteOutSite(List<byte> sData, string mainTrackNo, string statingNo, TcpClient tcpClient = null)
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
            // server.Send(tcpClient, sendDataList.ToArray());
            if (tcpClient == null)
                SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
                server.Send(tcpClient, sendDataList.ToArray());
            tcpLogInfo.Info(string.Format("【普通站衣架产量数据推送 pc---->硬件】发送完成，消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));
        }
        /// <summary>
        /// 启动主轨
        /// </summary>
        public void StartMainTrack(string mainTrackNo, string xor = null, TcpClient tcpClient = null)
        {
            //01 00 03 XX 00 37 00 00 00 00 00 10
            var message = new SusNet.Common.SusBusMessage.StartMainTrackRequestMessage(mainTrackNo, xor);
            tcpLogInfo.Info(string.Format("【启动主轨】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            if (tcpClient == null)
                SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
            else
                server.Send(tcpClient, message.GetBytes());
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
            if (tcpClient == null)
                SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
            else
                server.Send(tcpClient, message.GetBytes());
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
            if (tcpClient == null)
                SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
            else
                server.Send(tcpClient, message.GetBytes());
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
                //server.Send(tcpClient, sendDataList.ToArray());
                if (tcpClient == null)
                    SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
                else
                    server.Send(tcpClient, sendDataList.ToArray());
                tcpLogInfo.Info(string.Format("【向挂片站发送制品信息pc---->硬件】发送完成，【序号:{0}】 消息:--->{1}", index, SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                index++;
            }
        }
        /// <summary>
        /// 制品界面上线
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <param name="xor"></param>
        public void ClientMancheOnLine(string mainTrackNo, string statingNo, string productNumber, string xor = null, TcpClient tcpClient = null)
        {
            //01 01 03 XX 00 35 00 00 00 00 00 05
            xor = "FF";
            var message = new SusNet.Common.SusBusMessage.ClientMachineRequestMessage(mainTrackNo, statingNo, productNumber, xor);
            tcpLogInfo.Info(string.Format("【制品界面上线请求】发送开始,消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(message.GetBytes())));
            if (tcpClient == null)
                SendMessage(message, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", message.XID)));
            else
                server.Send(tcpClient, message.GetBytes());
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
            if (tcpClient == null)
                SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNumber)));
            else
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
            if (tcpClient == null)
                SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
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
            //server.Send(tcpClient, cRequest.GetBytes());
            if (tcpClient == null)
                SendMessageByByte(cRequest.GetBytes(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
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
            //server.Send(tcpClient, data);
            if (tcpClient == null)
                SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
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
            //server.Send(tcpClient, cRequest.GetBytes());
            if (tcpClient == null)
                SendMessage(cRequest, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo))));
            else
                server.Send(tcpClient, cRequest.GetBytes());
            tcpLogInfo.Info(string.Format("【修改站点容量 pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }
        /// <summary>
        /// 设置站容量
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="capacity"></param>
        public void SCMModifyStatingCapacitySuccess(int mainTrackNo, string statingNo, int capacity)
        {
            var mainTrackNum = HexHelper.TenToHexString2Len(mainTrackNo);
            var statingNoHex = HexHelper.TenToHexString2Len(statingNo);
            var capacityHex = HexHelper.TenToHexString4Len(capacity);

            var cRequest = new StatingCapacityRequestMessage(mainTrackNum, statingNoHex, capacityHex, SuspeConstants.cmd_StatingCapacity_5);
            tcpLogInfo.Info(string.Format("【设置站容量（回复） pc---->硬件】发送开始,【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
            SendMessage(cRequest, ClientManager.Instance.ClientKeyWithUserInfo(new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo))));
            tcpLogInfo.Info(string.Format("【设置站容量（回复） pc---->硬件】发送完成，【消息:--->{0}】", HexHelper.BytesToHexString(cRequest.GetBytes())));
        }

        /// <summary>
        /// 连接主轨查询
        /// </summary>
        public void ConnectedQueryMaintrackNumber(string clientKey)
        {
            try
            {
                tcpLogInfo.InfoFormat("【连接主轨查询】 开始");
                //var clients = ClientManager.Instance.Clients;
                //if (clients.Keys.Count == 0)
                //{
                //    tcpLogInfo.InfoFormat("【连接主轨查询】无客户端连接");
                //    return;
                //}
                var messageQueryMaintrackNumber = string.Format("00 00 {0} 00 {1} {2}", SuspeConstants.cmd_Reconnected_Query_MaintrackNumber_Req, SuspeConstants.address_Connected_Query_MaintrackNumber, "00 00 00 00 00 00");
                tcpLogInfo.InfoFormat("【连接主轨查询】 开始-->{0}", messageQueryMaintrackNumber);
                server.Send(clientKey, HexHelper.StringToHexByte(messageQueryMaintrackNumber));
                tcpLogInfo.InfoFormat("【连接主轨查询】 结束-->{0}", messageQueryMaintrackNumber);
            }
            catch (Exception ex)
            {
                tcpLogInfo.ErrorFormat("【连接主轨查询】异常{0}", ex);
            }
            finally
            {
                tcpLogInfo.InfoFormat("【连接主轨查询】结束");
            }

        }
        #endregion

    }
}

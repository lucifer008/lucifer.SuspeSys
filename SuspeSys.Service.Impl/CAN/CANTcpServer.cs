using DevExpress.XtraEditors;
using Sus.Net.Client;
using Sus.Net.Common.Entity;
using System;
using log4net;
using SuspeSys.Service.Impl.Products;
using SusNet.Common.Utils;
using SuspeSys.Domain.SusEnum;
using Sus.Net.Common.SusBusMessage;
using SuspeSys.Utils;
using Sus.Net.Common.Constant;
using SusNet.Common.SusBusMessage;
using System.Collections.Generic;
using Sus.Net.Common.Model;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusThread;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Service.Impl.Products.SusCache.Model;
using Newtonsoft.Json;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Service.Impl;
using SuspeSys.Domain.Ext;
using Sus.Net.Common.Utils;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using Sus.Net.Server;
using System.Net;
using SuspeSys.Service.Impl.Core;
using SuspeSys.AuxiliaryTools;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System.Threading;
using SuspeSys.Service.Impl.CAN;

namespace Suspe.CAN.Action.CAN
{
    public class CANTcpServer : SusLog
    {
        private CANTcpServer()
        {

        }

        public void Stop(ListBoxControl _lbMessage, bool isServiceStart)
        {
            if (!isServiceStart)
            {
                lbMessage = _lbMessage;
            }
            if (server != null)
            {
                server.Stop();
                isConnected = false;
                server = null;
                var sucMessage = string.Format("server端关闭成功!端口:【{0}】 .....", CANPort);
                if (!isServiceStart)
                {
                    _lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);

                }
                tcpLogInfo.Info(sucMessage);
            }
            else
            {
                var sucMessage = string.Format("沒有有效的连接,端口:【{0}】 .....", CANPort);
                if (!isServiceStart)
                {
                    _lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);

                }
                tcpLogInfo.Info(sucMessage);
            }
        }

        public static SusTCPServer1 server;
        public readonly static CANTcpServer Instance = new CANTcpServer();
        //static readonly string CanIp = System.Configuration.ConfigurationManager.AppSettings["CANIp"];
        static readonly int CANPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CANPort"]);
        static bool isConnected = false;
        const string XOR = SuspeConstants.XOR;
        // private static readonly ILog log = LogManager.GetLogger(typeof(CANTcp));
        ListBoxControl lbMessage = null;
        bool isServiceStart;
        public void ConnectCAN(ListBoxControl _lbMessage, bool _isServiceStart = false)
        {
            try
            {
                if (!isConnected)
                {
                    isServiceStart = _isServiceStart;
                    if (!isServiceStart)
                    {
                        lbMessage = _lbMessage;
                    }
                    ClientUserInfo info = new ClientUserInfo("gid", "testId");

                    server = new SusTCPServer1(CANPort);
                    server.MessageReceived += client_PlaintextReceived;
                    server.Start();
                    server.ClientConnected += Server_ClientConnected;
                    server.ClientDisconnected += Server_ServerDisconnected;
                    //server.ServerExceptionOccurred += Client_ServerExceptionOccurred;
                    server.EmergencyStopMainTrackResponseMessageReceived += Server_EmergencyStopMainTrackResponseMessageReceived;
                    server.StartMainTrackResponseMessageReceived += Client_StartMainTrackResponseMessageReceived;
                    server.StopMainTrackResponseMessageReceived += Client_StopMainTrackResponseMessageReceived;
                    server.ClientMachineResponseMessageReceived += Client_ClientMachineResponseMessageReceived;
                    server.HangingPieceStatingOnlineMessageReceived += Client_HangingPieceStatingOnlineMessageReceived;
                    server.HangerArrivalStatingMessageReceived += Client_HangerArrivalStatingMessageReceived;
                    server.HangerOutStatingRequestMessageReceived += Client_HangerOutStatingRequestMessageReceived;
                    //client.HangingPieceHangerUploadRequestMessageReceived += Client_HangingPieceHangerUploadRequestMessageReceived;
                    server.AllocationHangerResponseMessageReceived += Client_AllocationHangerResponseMessageReceived;
                    server.HangerDropCardRequestMessageReceived += Client_HangerDropCardRequestMessageReceived;
                    server.HangerReworkMessageReceived += Client_HangerReworkMessageReceived;
                    //client.ReworkDefectMessageReceived += Client_ReworkDefectMessageReceived;
                    server.CardRequestMessageReceived += Client_CardRequestMessageReceived;
                    server.ClearHangerCacheResponseMessageReceived += Client_ClearHangerCacheResponseMessageReceived;
                    server.FullSiteMessageReceived += Client_FullSiteMessageReceived;
                    server.MonitorMessageReceived += Client_MonitorMessageReceived;
                    server.ReworkFlowDefectRequestMessageReceived += Client_ReworkFlowDefectRequestMessageReceived;
                    server.StatingCapacityResponseMessageReceived += Client_StatingCapacityResponseMessageReceived;
                    server.StatingTypeResponseMessageReceived += Client_StatingTypeResponseMessageReceived;
                    server.PowerSupplyInitMessageReceived += Client_PowerSupplyInitMessageReceived;
                    server.SNSerialNumberMessageReceived += Client_SNSerialNumberMessageReceived;
                    server.MainboardVersionMessageReceived += Client_MainboardVersionMessageReceived;
                    server.LowerMachineSuspendOrReceiveMessageReceived += Client_LowerMachineSuspendOrReceiveMessageReceived;
                    server.UpperComputerInitResponseMessageReceived += Client_UpperComputerInitResponseMessageReceived;
                    server.FullSiteQueryResponseMessageReceived += Client_FullSiteQueryResponseMessageReceived;
                    server.MainTrakNumberUploadRequestMessageMessageReceived += Server_MainTrakNumberUploadRequestMessageMessageReceived;
                    server.MaterialCallMessageReceived += Server_MaterialCallMessageReceived;
                    server.MaterialCallUploadMessageReceived += Server_MaterialCallUploadMessageReceived;
                    server.MaterialCallStopRecivied += Server_MaterialCallStopRecivied;
                    server.F2AssignHangerNoUploadMessageReceived += Server_F2AssignHangerNoUploadMessageReceived;
                    server.F2AssignMessageReceived += Server_F2AssignMessageReceived;
                    server.CallMachineRepairMessageReceived += Server_CallMachineRepairMessageReceived;
                    server.CallManagementStartMessageReceived += Server_CallManagementStartMessageReceived;
                    server.CallStopMessageReceived += Server_CallStopMessageReceived;
                    server.FaultRepairUploadStartRequestMessageReceived += Server_FaultRepairUploadStartRequestMessageReceived;
                    server.FaultRepairReqtRequestMessageReceived += Server_FaultRepairReqtRequestMessageReceived;
                    server.FaultRepairStopRequestMessageReceived += Server_FaultRepairStopRequestMessageReceived;
                    server.FaultRepairStartRequestMessageReceived += Server_FaultRepairStartRequestMessageReceived;
                    server.FaultRepairSucessRequestMessageReceived += Server_FaultRepairSucessRequestMessageReceived;
                    server.FaultRepairClothingTypeAndFaultCodeRequestMessageReceived += Server_FaultRepairClothingTypeAndFaultCodeRequestMessageReceived;
                    isConnected = true;
                    tcpLogInfo.Info("开启成功!");
                    var sucMessage = string.Format("开启成功,端口:【{0}】 .....", CANPort);
                    if (!isServiceStart)
                    {
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
                    }
                    tcpLogInfo.Info(sucMessage);
                }
            }
            catch (Exception ex)
            {
                tcpLogError.Error("【端口开启失败】", ex);
            }
        }
       
        #region 故障报修
        //故障报修 类别及故障代码请求
        private void Server_FaultRepairClothingTypeAndFaultCodeRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }

        //故障报修:完成维修
        private void Server_FaultRepairSucessRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //故障报修:开始维修
        private void Server_FaultRepairStartRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //故障报修:中止维修
        private void Server_FaultRepairStopRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //故障报修:维修请求
        private void Server_FaultRepairReqtRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //故障报修:完成维修
        private void Server_FaultRepairUploadStartRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        #endregion

        #region 呼叫管理

        //呼叫(管理/机修)停止
        private void Server_CallStopMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //呼叫管理
        private void Server_CallManagementStartMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //呼叫机修
        private void Server_CallMachineRepairMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }

        //终止缺料
        private void Server_MaterialCallStopRecivied(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }

        #endregion


        #region F2相关
        //【F2指定不跨主轨】
        private void Server_F2AssignMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart, e.TcpClient);
        }
        //【F2指定跨主轨】
        private void Server_F2AssignHangerNoUploadMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart, e.TcpClient);
        }
        #endregion

        #region 缺料呼叫
        //缺料呼叫上传 
        private void Server_MaterialCallUploadMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }
        //缺料呼叫
        private void Server_MaterialCallMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
            // SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
        }

        #endregion

        #region 主轨上传及上位机下发等等

        /// <summary>
        /// 主轨上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_MainTrakNumberUploadRequestMessageMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as MainTrakNumberUploadRequestMessage;
            if (message == null)
            {
                var message1 = e.Tag as PowerSupplyInitRequestMessage;
                var XID1 = HexHelper.HexToTen(message1.XID);
                var sucessMessage1 = string.Format("【上电初始化-->主轨上传 主轨【{0}】!", XID1);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage1, null);
                tcpLogInfo.Info(sucessMessage1);
                return;
            }
            var XID = HexHelper.HexToTen(message.XID);
            var sucessMessage = string.Format("【主轨上传 主轨【{0}】!", XID);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);
        }

        /// <summary>
        /// 上位机发起的初始化【满站查询回应】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_FullSiteQueryResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as FullSiteMessage;
            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);
            var isFullSite = message.IsFullSite;
            var sucessMessage = string.Format("【上位机发起的初始化【满站查询回应】 主轨【{0}】 站点【{1}】 是否满站【{2}】!", XID, ID, message.IsFullSite ? "是" : "否");
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);
            CANProductsService.Instance.UpdateMainTrackStatingStatus(XID, ID, isFullSite);
        }

        /// <summary>
        /// 上位机发起的初始化，下位机的回应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_UpperComputerInitResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var tpInfo = e.TcpClient?.Client?.RemoteEndPoint?.ToString();
            var tpInfoMessage = string.Format("【上位机发起的初始化，下位机的回应】客户端--->{0}", tpInfo);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), tpInfo, null);
            tcpLogInfo.Info(tpInfoMessage);

            var message = e.Tag as UpperComputerInitResponseMessage;
            var sucessMessage = string.Format("【上位机发起的初始化，下位机的回应 主轨【{0}】 站点【{1}】 ", message.MainTrackNo, message.StatingNo);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);

            sucessMessage = string.Format("【上位机发起的初始化，下位机的回应 主轨【{0}】 站点【{1}】 更新主版号开始-->主板号为:{2}", message.MainTrackNo, message.StatingNo, message.MainboardVersion.ToString());
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);

            StatingServiceImpl.Instance.UpdateStatingMainboard(message.MainTrackNo, message.StatingNo,
               message.MainboardVersion.ToString());

            sucessMessage = string.Format("【上位机发起的初始化，下位机的回应 主轨【{0}】 站点【{1}】 更新主版号完成-->主板号为:{2}", message.MainTrackNo, message.StatingNo, message.MainboardVersion.ToString());
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);

            sucessMessage = string.Format("【上位机发起的初始化，下位机的回应 主轨【{0}】 站点【{1}】 站点及主轨状态初始化开始", message.MainTrackNo, message.StatingNo);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);
            new SuspeApplication().PowerSupplyInit(message.MainTrackNo, message.StatingNo, server, e.TcpClient);

            sucessMessage = string.Format("【上位机发起的初始化，下位机的回应 主轨【{0}】 站点【{1}】 站点及主轨状态初始化完成", message.MainTrackNo, message.StatingNo);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);
        }

        #endregion

        #region 下位机更新站点接收或停止接收衣架
        /// <summary>
        /// 下位机更新站点接收或停止接收衣架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_LowerMachineSuspendOrReceiveMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as LowerMachineSuspendOrReceiveMessage;
            StatingServiceImpl.Instance.UpdateStatingSuspendOrReceive(message.XID, message.ID, message.Tag);
            server.SuspendOrReceiveHangerReponseToLowerMachine(HexHelper.HexToTen(message.XID), HexHelper.HexToTen(message.ID), message.Tag, null);
        }
        #endregion

        #region 主版版本号及SN号上传
        /// <summary>
        /// 主版版本号上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MainboardVersionMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as MainboardVersionRequestMessage;
            StatingServiceImpl.Instance.UpdateStatingMainboard(message.MainTrackNo, message.StatingNo,
                HexHelper.HexToTen(message.MainboardVersion).ToString());
        }
        /// <summary>
        /// 主版SN号上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_SNSerialNumberMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as SNSerialNumberRequestMessage;
            StatingServiceImpl.Instance.UpdateStatingSN(message.MainTrackNo, message.StatingNo,
                HexHelper.HexToTen(message.SN).ToString());
        }
        #endregion

        #region //上电初始化
        private void Client_PowerSupplyInitMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as PowerSupplyInitRequestMessage;
            new SuspeApplication().PowerSupplyInit(message.MainTrackNo, message.StatingNo, server);
        }
        #endregion

        #region 站类型相关
        /// <summary>
        /// 修改站点类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_StatingTypeResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var res = e.Tag as StatingTypeResponseMessage;
            if (null != res)
            {
                var XID = HexHelper.HexToTen(res.XID);
                var ID = HexHelper.HexToTen(res.ID);
                var OpType = res.OpType;
                var statingType = res.StatingType;
                var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 操作类型【{2}】站点类型【{3}】 站点类型操作完成!", XID, ID, OpType, statingType);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                tcpLogInfo.Info(sucessMessage);
            }
        }

        /// <summary>
        /// 修改站点容量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_StatingCapacityResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var res = e.Tag as StatingCapacityResponseMessage;
            if (null != res)
            {
                if (res.CMD == SuspeConstants.cmd_StatingCapacity_4)
                {
                    var XID = HexHelper.HexToTen(res.XID);
                    var ID = HexHelper.HexToTen(res.ID);
                    var capacity = res.Capacity;
                    var sucessMessage = string.Format("【站点容量修改消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!", XID, ID, capacity);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                    tcpLogInfo.Info(sucessMessage);
                }
                else if (res.CMD == SuspeConstants.cmd_StatingCapacity_6)
                {
                    //设置站容量（液晶设置时，主动上传PC）
                    StatingModel stating = new StatingModel()
                    {
                        MainTrackNumber = short.Parse(res.XID),
                        StatingNo = res.ID.TrimStart('0'),
                        Capacity = res.Capacity,
                    };

                    string json = JsonConvert.SerializeObject(stating);
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.STATING_EDIT_ACTION, json);


                    var sucessMessage = string.Format("【站点容量修改消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!", res.XID, res.id, res.Capacity);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                    tcpLogInfo.Info(sucessMessage);
                }
                else if (res.CMD == SuspeConstants.cmd_StatingCapacity_4)
                {
                    //设置站容量（回复）
                    var sucessMessage = string.Format("【站点容量修改消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!（硬件发送）", res.XID, res.id, res.Capacity);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                    tcpLogInfo.Info(sucessMessage);
                }

            }
        }

        #endregion

        #region 监测点
        /// <summary>
        /// 监测点上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MonitorMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var res = e.Tag as MonitorMessage;
            if (null != res)
            {
                var XID = HexHelper.HexToTen(res.XID);
                var ID = HexHelper.HexToTen(res.ID);
                var mtMontor = new MainTrackStatingMontorModel() { MainTrackNumber = XID, StatingNo = ID, HangerNo = res.HangerNo.ToString() };
                var mtMontorJson = JsonConvert.SerializeObject(mtMontor);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION, mtMontorJson);
                var sucessMessage = string.Format("【监测点消息】 主轨【{0}】 站点【{1}】 衣架【{2}】!", XID, ID, res.HangerNo);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                tcpLogInfo.Info(sucessMessage);
            }

        }
        #endregion

        #region 站点状态
        //站点状态监测更新
        /// <summary>
        /// 站点状态上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_FullSiteMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as FullSiteMessage;
            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);
            var isFullSite = message.IsFullSite;
            var sucessMessage = string.Format("【满站消息上报】 主轨【{0}】 站点【{1}】 是否满站【{2}】!", XID, ID, message.IsFullSite ? "是" : "否");
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
            tcpLogInfo.Info(sucessMessage);
            CANProductsService.Instance.UpdateMainTrackStatingStatus(XID, ID, isFullSite);
        }
        #endregion

        #region 清除衣架缓存
        //【协议2.0】 清除衣架缓存
        /// <summary>
        /// 清除衣架缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_ClearHangerCacheResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var res = e.Tag as ClearHangerCacheResponseMessage;
            if (null != res)
            {
                var XID = HexHelper.HexToTen(res.XID);
                var ID = HexHelper.HexToTen(res.ID);
                var hangerNo = res.HangerNo;
                var sucessMessage = string.Format("【下位机缓存清除回应消息】 主轨【{0}】 站点【{1}】 衣架【{2}】 缓存清除完成!", XID, ID, hangerNo);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
                tcpLogInfo.Info(sucessMessage);
            }
        }
        #endregion

        #region//【协议2.0】 挂片站上传衣架号信息事件
        //private void Client_HangingPieceHangerUploadRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        // {
        //     tcpLogInfo.Info("【协议2.0】 挂片站上传衣架号信息事件---->已触发");
        //     //接着发送是否出站的命令
        //     /*
        //      * 01 01 05 XX 00 59 00 AA BB CC DD EE---允许出站
        //        01 01 05 XX 00 59 01 AA BB CC DD EE ---不允许出站
        //      */
        //     //zxl 2018年3月14日 23:45:04

        //     var req = e.Tag as HangingPieceHangerUploadRequestMessage;
        //     if (null != req)
        //     {
        //         var XID = HexHelper.HexToTen(req.XID);
        //         var ID = HexHelper.HexToTen(req.ID);
        //         var hangerNo = HexHelper.HexToTen(req.HangerNo);
        //         var productNumber = HexHelper.HexToTen(req.ProductionNumber);

        //         string nextStatingNo = null;
        //         try
        //         {
        //             int outMainTrackNumber = 0;
        //             var sucess = CANProductsService.Instance.HangerOutStatingRequest(XID.ToString(), ID.ToString(), productNumber.ToString(), hangerNo.ToString(), ref nextStatingNo, ref outMainTrackNumber);
        //             if (sucess)
        //             {
        //                 if (string.IsNullOrEmpty(nextStatingNo))
        //                 {
        //                     var sucessMessage = string.Format("【消息】 主轨【{0}】 站点{1} 衣架【{2}】 生产完成!", XID, ID, hangerNo);
        //                     lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);

        //                     tcpLogInfo.Info(string.Format("【衣架生产完成】 衣架出站指令发送开始! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", XID, ID, ID, hangerNo));
        //                     //衣架出站
        //                     client.AutoHangerOutStating(req.XID, req.ID, true, hangerNo, XOR);

        //                     //更新产量
        //                     string sucessSData = null;
        //                     var sucessProdOutData = CANProductsQueryService.Instance.GetProductByBytes(XID, ID, productNumber, ref sucessSData);
        //                     CANTcp.client.SendOutputDataToHangingPiece(sucessProdOutData, req.XID, req.ID);
        //                     lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessSData, null);

        //                     var clearHangerNoCache1 = string.Format("正在清除站点{0} 衣架【{1}】的站点缓存...", ID, hangerNo);
        //                     CANTcp.client.ClearHangerCache(XID, ID, hangerNo);
        //                     lbMessage?.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache1, null);

        //                     return;
        //                 }
        //                 var hexID = HexHelper.TenToHexString2Len(outMainTrackNumber);
        //                 CANTcp.client.AllocationHangerToNextStating(hexID, HexHelper.TenToHexString2Len(nextStatingNo), req.HangerNo);
        //                 var susAllocatingMessage = string.Format("【消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", XID, nextStatingNo);
        //                 lbMessage?.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);

        //                 //更新产量
        //                 string sData = null;
        //                 var prodOutData = CANProductsQueryService.Instance.GetProductByBytes(XID, ID, productNumber, ref sData);
        //                 CANTcp.client.SendOutputDataToHangingPiece(prodOutData, req.XID, req.ID);
        //                 lbMessage?.Invoke(new EventHandler(this.AddMessage), sData, null);

        //                 var clearHangerNoCache = string.Format("正在清除站点{0} 衣架【{1}】的站点缓存...", ID, hangerNo);
        //                 CANTcp.client.ClearHangerCache(XID, ID, hangerNo);
        //                 lbMessage?.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);

        //             }
        //         }
        //         catch (ApplicationException ex)
        //         {
        //             var cusExData = ex.Message;
        //             tcpLogError.Error("【衣架出站】", ex);
        //             var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //             lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //             //回显异常数据到终端
        //             CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);

        //         }
        //         catch (Exception ex)
        //         {
        //             var sysEx = string.Format("【衣架出站】 异常:{0}", ex.Message);
        //             tcpLogError.Error(sysEx, ex);
        //             lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //         }
        //     }
        // }
        #endregion

        #region 卡相关
        //【卡片请求】
        private void Client_CardRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var cardRequestMessage = e.Tag as Sus.Net.Common.SusBusMessage.CardRequestMessage;
            if (null != cardRequestMessage)
            {
                //lbMessage?.Invoke(new EventHandler(this.AddMessage), cardRequestMessage.ToString(), null);
                var XID = HexHelper.HexToTen(cardRequestMessage.XID);
                var ID = HexHelper.HexToTen(cardRequestMessage.ID);
                var cardNo = HexHelper.HexToTen(cardRequestMessage.CardNo);
                var type = 0;
                string info = string.Empty;
                try
                {

                    //给下位机发送 --->  [工号]员工姓名   【unicode码发送】
                    //client.SendDataByEmployeeLoginInfo
                    var other = string.Empty;
                    CANProductsService.Instance.CardLogin(XID, ID, cardNo, ref type, ref other, ref info);
                    switch ((short)type)
                    {
                        case 3://员工卡登录
                            var cardRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 3, cardRequestMessage.CardNo, XOR);
                            server.SendMessageByByte(cardRes.GetBytes(), new ClientUserInfo("1", cardRequestMessage.XID), e.TcpClient);
                            var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
                            var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
                            server.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), cardRequestMessage.XID, cardRequestMessage.StatingNo, XOR, e.TcpClient);
                            var message = string.Format("【员工卡登录】主轨:【{0}】站点:【{1}】 卡号:【{2}】 员工:【{3}】 已登录!", XID, ID, cardNo, other);
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(this.AddMessage), message, null);
                            tcpLogInfo.Info(message);
                            break;
                        case 4://员工卡衣车登出
                            var cardRepatRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 4, cardRequestMessage.CardNo);
                            server.SendMessageByByte(cardRepatRes.GetBytes(), new ClientUserInfo("1", cardRequestMessage.XID), e.TcpClient);
                            var emRepatMessage = string.Format("【员工卡登录】主轨:【{0}】站点:【{1}】 卡号:【{2}】 员工:【{3}】 已登出!", XID, ID, cardNo, other);
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(this.AddMessage), emRepatMessage, null);
                            tcpLogInfo.Info(emRepatMessage);
                            break;
                        case 5://衣车登录
                            var vechiCardLoginRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 5, cardRequestMessage.CardNo, XOR);
                            server.SendMessageByByte(vechiCardLoginRes.GetBytes(), new ClientUserInfo("1", cardRequestMessage.XID), e.TcpClient);
                            var vechiInfoLoginEncoding = UnicodeUtils.CharacterToCoding(info);
                            var vechiInfoLoginBytes = HexHelper.StringToHexByte(vechiInfoLoginEncoding);
                            server.SendDataByClothesCardLoginInfo(new List<byte>(vechiInfoLoginBytes), cardRequestMessage.XID, cardRequestMessage.StatingNo, XOR, e.TcpClient);
                            var messageLogin = string.Format($"【衣车登录】主轨:【{XID}】站点:【{ID}】{info}");
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(this.AddMessage), messageLogin, null);
                            tcpLogInfo.Info(messageLogin);
                            break;
                        case 6://衣车登出
                            var vechiCardLogoutRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 6, cardRequestMessage.CardNo, XOR);
                            server.SendMessageByByte(vechiCardLogoutRes.GetBytes(), new ClientUserInfo("1", cardRequestMessage.XID), e.TcpClient);
                            var vechiInfoLogoutEncoding = UnicodeUtils.CharacterToCoding(info);
                            var vechiInfoLogoutBytes = HexHelper.StringToHexByte(vechiInfoLogoutEncoding);
                            server.SendDataByClothesCardLoginInfo(new List<byte>(vechiInfoLogoutBytes), cardRequestMessage.XID, cardRequestMessage.StatingNo, XOR, e.TcpClient);
                            var messageLogout = string.Format($"【衣车登录】主轨:【{XID}】站点:【{ID}】{info}");
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(this.AddMessage), messageLogout, null);
                            tcpLogInfo.Info(messageLogout);
                            break;
                        default:
                            var cardRepatOffDuty = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 0, cardRequestMessage.CardNo, XOR);
                            server.SendMessageWithCAN(cardRepatOffDuty, new ClientUserInfo("1", cardRequestMessage.XID));
                            var emOffDutyMessage = string.Format("【卡号登陆异常】主轨:【{0}】站点:【{1}】 卡号:【{2}】 卡不存在或出错!", XID, ID, cardNo);
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(this.AddMessage), emOffDutyMessage, null);
                            tcpLogInfo.Info(emOffDutyMessage);
                            break;
                    }
                }
                catch (CanLoginFromStationException ex)
                {
                    var cusExData = ex.Message;
                    tcpLogError.Error("【卡片请求】", ex);
                    var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);

                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                    //回显异常数据到终端
                    //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);
                    CANTcpServer.server.SendExcpetionOrPromptInfo(XID, ID, (int)_0130ExcpetionTag.CanLoginFromStation);
                }
                catch (ApplicationException ex)
                {
                    var cusExData = ex.Message;
                    tcpLogError.Error("【卡片请求】", ex);
                    var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                    //回显异常数据到终端
                    CANTcpServer.server.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID, e.TcpClient);
                }

                catch (Exception ex)
                {
                    var sysEx = string.Format("【卡片请求】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
            }

        }
        #endregion

        ////衣架返工疵点代码
        //private void Client_ReworkDefectMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var hangerReworkDefectMessage = e.Tag as SusNet.Common.SusBusMessage.ReworkDefectRequestMessage;
        //    if (null != hangerReworkDefectMessage)
        //    {
        //        var XID = HexHelper.HexToTen(hangerReworkDefectMessage.XID);
        //        var ID = HexHelper.HexToTen(hangerReworkDefectMessage.ID);
        //        var hangerNo = HexHelper.HexToTen(hangerReworkDefectMessage.HangerNo);
        //        var reworkDefectCode = HexHelper.HexToTen(hangerReworkDefectMessage.ReworkDefectCode);
        //        var message = string.Format("【衣架返工疵点代码】 收到返工疵点代码请求 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3}", XID, ID, hangerNo, reworkDefectCode);
        //        tcpLogInfo.Info(message);
        //        lbMessage?.Invoke(new EventHandler(AddMessage), message, null);
        //        string errMsg = null;
        //        string nextStatingNo = null;
        //        try
        //        {
        //            bool success = CANProductsService.Instance.RecordReworkDefectHangerInfo(XID, hangerNo, ID, reworkDefectCode, ref nextStatingNo, ref errMsg);
        //            if (success)
        //            {
        //                //对返工到达的站点发出返工请求
        //                //client.HangerReworkResponse(XID, ID, hangerNo, flowCode);
        //                //不满足返工条件
        //                var messageAuto = string.Format("【衣架返工疵点代码】 衣架满足出站条件,正在请求下一站是否满足接收衣架 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3} 下一站:{4}", XID, ID, hangerNo, reworkDefectCode, nextStatingNo);
        //                tcpLogInfo.Info(messageAuto);
        //                lbMessage?.Invoke(new EventHandler(AddMessage), messageAuto, null);

        //                client.AllocationHangerToNextStating(hangerReworkDefectMessage.XID, hangerReworkDefectMessage.ID, hangerReworkDefectMessage.HangerNo);
        //                return;
        //            }

        //            //不满足返工条件
        //            var messageNotAuto = string.Format("【衣架返工疵点代码】 衣架不满足出站条件 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3}", XID, ID, hangerNo, reworkDefectCode);
        //            tcpLogInfo.Info(messageNotAuto);
        //            lbMessage?.Invoke(new EventHandler(AddMessage), messageNotAuto, null);
        //        }
        //        catch (ApplicationException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架返工疵点代码】", ex);
        //            var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //            lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //            //回显异常数据到终端
        //            CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);

        //        }
        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【衣架返工疵点代码】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }
        //}


        #region 衣架返工相关
        /// <summary>
        /// 衣架返工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_HangerReworkMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var hangerReworkMessage = e.Tag as SusNet.Common.SusBusMessage.ReworkRequestMessage;
            if (null != hangerReworkMessage)
            {
                var tenMainTrackNumber = HexHelper.HexToTen(hangerReworkMessage.XID);
                var tenStatingNo = HexHelper.HexToTen(hangerReworkMessage.ID);
                var tenHangerNo = HexHelper.HexToTen(hangerReworkMessage.HangerNo);
                var message = string.Format("【衣架返工】 收到返工请求 主轨:{0} 站号:{1} 衣架号:{2}", tenMainTrackNumber, tenStatingNo, tenHangerNo);
                tcpLogInfo.Info(message);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(AddMessage), message, null);
                string errMsg = null;
                int tag = 0;
                try
                {

                    bool success = SusReworkQueryService.Instance.CheckIsCanRework(tenMainTrackNumber, tenStatingNo.ToString(), tenHangerNo.ToString(), ref tag, ref errMsg);//CANProductsService.Instance.RecordReworkHangerInfo(XID, hangerNo, ID, ref errMsg);
                    if (!success)
                    {
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), errMsg, null);
                        tcpLogError.Info(errMsg);
                        return;
                    }
                    var isSucessRegister = SusReworkService.Instance.RegisterReworkHanger(tenHangerNo.ToString(), tenMainTrackNumber, tenStatingNo.ToString(), ref errMsg);
                    if (isSucessRegister)
                    {
                        var sucessMesg = string.Format("【衣架返工】返工衣架绑定成功! 衣架号:{0} 站点:{1} 主轨:{2}", tenHangerNo, tenStatingNo, tenMainTrackNumber);
                        tcpLogInfo.Info(sucessMesg);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessMesg, null);
                        return;
                    }
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), errMsg, null);
                }
                catch (HangingPieceReworkException ex)
                {
                    var cusExData = ex.Message;
                    tcpLogError.Error("【衣架返工】", ex);
                    //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

                    server.SendReworkException(tenMainTrackNumber, tenStatingNo, tenHangerNo, tag, null);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                    //Array.Reverse(unHangerReworkExBytes);

                    ////回显异常数据到终端
                    //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(unHangerReworkExBytes), XID, ID);

                }
                catch (StatingNoLoginEmployeeException ex)
                {
                    var cusExData = ex.Message;
                    tcpLogError.Error("【衣架返工】", ex);
                    //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

                    server.SendReworkException(tenMainTrackNumber, tenStatingNo, tenHangerNo, tag, null);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
                }
                catch (HangerNoProductException ex)
                {
                    var cusExData = ex.Message;
                    tcpLogError.Error("【衣架返工】", ex);
                    //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

                    server.SendReworkException(tenMainTrackNumber, tenStatingNo, tenHangerNo, tag, null);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
                }
                catch (Exception ex)
                {
                    var sysEx = string.Format("【衣架返工】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
            }

        }
        /// <summary>
        /// 工序及疵点及代码上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_ReworkFlowDefectRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var reqMess = e.Tag as Dictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>>;
            if (null != reqMess)
            {
                int tag = 0;
                string tenNextStatingNo = null;
                int outMainTrackNumber = 0;
                int currentMainTrackNumber = 0;
                int currentStatingNo = 0;
                int hangerNo = 0;
                try
                {

                    bool isRequireBridge = false;
                    string info = null;
                    CANProductsService.Instance.FlowReworkAction(reqMess, ref currentMainTrackNumber, ref currentStatingNo, ref tenNextStatingNo, ref outMainTrackNumber, ref hangerNo, ref tag, ref isRequireBridge, ref info);
                    if (!isRequireBridge && !string.IsNullOrEmpty(tenNextStatingNo))
                    {
                        var tenMainTrackNumber = outMainTrackNumber;//HexHelper.TenToHexString2Len(outMainTrackNumber);
                        CANTcpServer.server.AllocationHangerToNextStating(tenMainTrackNumber.ToString(), tenNextStatingNo, HexHelper.TenToHexString10Len(hangerNo), XOR);
                        var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                        {
                            HangerNo = hangerNo + "",
                            MainTrackNumber = (short)tenMainTrackNumber,
                            SiteNo = tenNextStatingNo + ""
                            ,
                            AllocatingStatingDate = DateTime.Now
                        });
                        NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

                        var susAllocatingMessage = string.Format("【衣架返工工序及疵点】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenMainTrackNumber, tenNextStatingNo);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
                        tcpLogInfo.Info(susAllocatingMessage);
                    }
                    if (isRequireBridge)//桥接只打日志
                    {
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), info, null);
                        tcpLogInfo.Info(info);
                    }
                }
                catch (FlowNotFoundException ex)
                {
                    server.SendReworkException(currentMainTrackNumber, currentStatingNo, hangerNo, tag, XOR);
                    var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);

                }
                catch (ReworkHangerNotFoundException ex)
                {
                    server.SendReworkException(currentMainTrackNumber, currentStatingNo, hangerNo, tag, XOR);
                    var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
                catch (ReworkDefectNotFoundException ex)
                {
                    server.SendReworkException(currentMainTrackNumber, currentStatingNo, hangerNo, tag, XOR);
                    var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
                catch (FullStatingExcpetion ex)
                {
                    var fullStatingNo = currentStatingNo;
                    if (ex.FullStatingList != null && ex.FullStatingList.Count != 0)
                    {
                        foreach (var fStat in ex.FullStatingList)
                        {
                            server.SendExcpetionOrPromptInfo(currentMainTrackNumber, int.Parse(fStat), SuspeConstants.tag_FullSiteOrStopWork, XOR, e.TcpClient);
                        }
                    }
                    else
                    {
                        server.SendExcpetionOrPromptInfo(currentMainTrackNumber, fullStatingNo, SuspeConstants.tag_FullSiteOrStopWork, XOR, e.TcpClient);
                    }

                    var cusExData = ex.Message;
                    tcpLogError.Error("【衣架返工工序及疵点】", ex);
                    //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
                }
                catch (ReworkFlowNoNotFoundException ex)
                {
                    server.SendReworkException(currentMainTrackNumber, currentStatingNo, hangerNo, SuspeConstants.tag_ExcpetionOrPromptInfo_ReworkFlowNoFound, XOR);
                    var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
                catch (Exception ex)
                {
                    var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
            }
        }
        #endregion

        #region CAN 连接相关
        /// <summary>
        /// CAN连接异常上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_ServerExceptionOccurred(object sender, Sus.Net.Client.Sockets.TcpServerExceptionOccurredEventArgs e)
        {
            var susRemotingMessage = string.Format("【消息】CAN连接异常【{0}】!", e.Exception?.Message);
            tcpLogError.Error("Client_ServerExceptionOccurred", e?.Exception);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
        }
        /// <summary>
        /// CAN连接关闭触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ServerDisconnected(object sender, Sus.Net.Server.Sockets.TcpClientDisconnectedEventArgs e)
        {
            var susRemotingMessage = string.Format("【消息】{0}", "CAN连接已关闭!", e.TcpClient?.Client?.RemoteEndPoint?.ToString());
            tcpLogInfo.Info(susRemotingMessage);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
        }
        static int tag = 0;
        /// <summary>
        /// CAN连接成功上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ClientConnected(object sender, Sus.Net.Server.Sockets.TcpClientConnectedEventArgs e)
        {
            var susRemotingMessage = string.Format("【消息】客户端{0}】连接成功!", e.TcpClient?.Client?.RemoteEndPoint?.ToString());
            tcpLogInfo.Info(susRemotingMessage);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);

            if (!isServiceStart)
            {
                var clientKeys = e.TcpClient?.Client?.RemoteEndPoint?.ToString();
                susRemotingMessage = string.Format("【消息】客户端{0}】主轨查询命令下发开始...", clientKeys);
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
                ConnectedQueryMaintrackNumber(clientKeys);
                susRemotingMessage = string.Format("【消息】客户端{0}】主轨查询命令下发完成.", clientKeys);
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
            }

            var clientKey = e.TcpClient?.Client?.RemoteEndPoint?.ToString();
            tag = 1;
            var pMessage = string.Format("【上电启动站点初始化信息】发送开始...");
            tcpLogInfo.Info(pMessage);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), pMessage, null);
            IList<int> list = CANProductsService.Instance.GetAllMainTracknumber();
            server.UpperComputerPowerSupplyInit(list, e.TcpClient);
            pMessage = string.Format("上电启动站点初始化信息】发送完成...");
            tcpLogInfo.Info(pMessage);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), pMessage, null);
        }
        public void SendMessageWithCANTest(string maintackNumber)
        {
            var data = HexHelper.StringToHexByte(string.Format("{0} 01 06 FF 00 12 00 00 00 00 00 01", HexHelper.TenToHexString2Len(maintackNumber)));
            SusNet.Common.Message.MessageBody message1 = new SusNet.Common.Message.MessageBody(data);
            Sus.Net.Common.Entity.ClientUserInfo clientInfo = new Sus.Net.Common.Entity.ClientUserInfo("1", HexHelper.TenToHexString2Len(maintackNumber));
            server.SendMessageWithCAN(message1, clientInfo);
        }
        void AddMessage(object sender, EventArgs e)
        {
            var index = lbMessage?.Items.Count + 1;
            var data = string.Format("{0}--->{1}", index, sender.ToString());
            lbMessage?.Items.Add(data);
        }
        private void client_PlaintextReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var susRemotingMessage = string.Format("【消息】{0}", e.message);
            //if (!isServiceStart)
            //    lbMessage?.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
            tcpLogHardware.Info(susRemotingMessage);
        }
        #endregion

        #region 站内衣架工序比较
        //站内衣架工序比较
        /// <summary>
        /// 衣架进站后再读卡触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_HangerDropCardRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            lock (lockObject)
            {
                //站内衣架工序比较
                var messageRequest = e.Tag as SusNet.Common.SusBusMessage.HangerDropCardRequestMessage;
                var tenMainTrackNumber = HexHelper.HexToTen(messageRequest.XID);
                var tenStatingNo = HexHelper.HexToTen(messageRequest.ID);
                var tenHangerNo = HexHelper.HexToTen(messageRequest.HangerNo);
                var firstData = CANProductsService.Instance.IsHangPieceStating(tenMainTrackNumber, tenStatingNo) ? 1 : 0;// HexHelper.HexToTen(messageRequest.DATA1);//0:普通站比较:1:挂片站比较
                var isPowerSupply = HexHelper.HexToTen(messageRequest.DATA1).Equals(1);//为1时是断电重启状态
                try
                {

                    bool isNonInBridge = CANProductsService.Instance.IsNonInBridgeByCompareFlowAction(tenMainTrackNumber, tenStatingNo, tenHangerNo);
                    if (isNonInBridge)
                    {
                        var sData = string.Format("【桥接工序比较信息】桥接主轨:{0} 站点:{1} 衣架:{2}", tenMainTrackNumber, tenStatingNo, tenHangerNo);
                        tcpLogInfo.InfoFormat(sData);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), sData, null);
                        return;
                    }
                    //普通站
                    if (firstData == 0)
                    {
                        var message = string.Format("【站内衣架工序比较】 主轨:{0} 站号:{1} 衣架号:{2}", tenMainTrackNumber, tenStatingNo, tenHangerNo);
                        tcpLogInfo.Info(message);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), message, null);

                        int tag = 0;
                        string info = null;
                        string errMsg = null;
                        bool isReworkFlow = false;
                        string flowInfo = string.Empty;
                        bool isEq = CANProductsService.Instance.CompareHangerFlow(tenMainTrackNumber.ToString(), tenHangerNo.ToString(), tenStatingNo.ToString(), ref tag, ref info, ref errMsg, ref isReworkFlow, ref flowInfo, isPowerSupply);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(AddMessage), string.Format("【本次工序】:--->【{0}】", flowInfo), null);
                        if (!isEq)
                        {
                            string mainTrackNo = tenMainTrackNumber.ToString();
                            //var hexInfo = UnicodeUtils.GetHexFromChs(info);
                            //var hexBytes = HexHelper.StringToHexByte(hexInfo);//HexHelper.strToToHexByte(hexData);

                            var btsFlowCompare = UnicodeUtils.CharacterToCoding(info);
                            tcpLogInfo.Info(string.Format("【站内衣架工序比较】 将要发送的内容--->【{0}】", info));
                            Console.WriteLine(string.Format("【站内衣架工序比较】 将要发送的内容 pc--->硬件【{0}】", info));

                            server.HangerDropCardProcessFlowCompare(messageRequest.XID, messageRequest.ID, tenHangerNo.ToString(), tag, XOR);
                            var btsFlowCompareBytes = HexHelper.StringToHexByte(btsFlowCompare);
                            //Array.Reverse(btsFlowCompare);

                            //client.SendDataByHangerDropCardCompare(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, "FF");
                            if (!isReworkFlow)
                            {
                                server.SendDataByHangerDropCardCompareExt(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, XOR);
                            }
                            else
                            {
                                //返工信息发送
                                server.SendDataByHangerDropCardCompareExt2(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, XOR);
                            }
                            var strSucess = string.Format("【站内衣架工序比较】 制品信息【{0}】发送完成!", info);
                            tcpLogInfo.Info(strSucess);
                            if (!isServiceStart)
                                lbMessage?.Invoke(new EventHandler(AddMessage), strSucess, null);
                            return;
                        }
                        //相同工序
                        server.HangerDropCardProcessFlowCompare(messageRequest.XID, messageRequest.ID, tenHangerNo.ToString(), 0, XOR);
                        return;
                    }
                    //挂片站
                    if (firstData == 1)
                    {
                        //var data = CANProductsQueryService.Instance.GetProductsExt(XID, ID);
                        //var pInfo = UnicodeUtils.CharacterToCoding(data);
                        //var hangPieceProductsBytes = HexHelper.StringToHexByte(pInfo);
                        //client.SendDataByHangerDropCardCompareExt(new System.Collections.Generic.List<byte>(hangPieceProductsBytes), messageRequest.XID, messageRequest.ID, XOR);
                        //tcpLogInfo.Info(string.Format("【挂片站衣架产量推送完成】 主轨:{0} 站点:{1} 推送内容(unicode编码(小段模式))--->{2}", messageRequest.XID, messageRequest.ID, pInfo));
                        //var message = string.Format("【挂片站衣架产量推送完成】 主轨:{0} 站号:{1} 衣架号:{2} 发送内容-->【{3}】", XID, ID, hangerNo, data);
                        //tcpLogInfo.Info(message);
                        //lbMessage?.Invoke(new EventHandler(this.AddMessage), message, null);
                        var productNumber = CANProductsQueryService.Instance.GetCurrentOnlineProductNumber(tenMainTrackNumber, tenStatingNo);//获取当前上线的排产号
                        string sData = null;
                        var prodOutData = CANProductsQueryService.Instance.GetProductByBytes(tenMainTrackNumber, tenStatingNo, productNumber, ref sData);
                        CANTcpServer.server.SendOutputDataToHangingPiece(prodOutData, messageRequest.XID, messageRequest.ID);
                        CANProductsService.Instance.CacheHangingHangerRequest(tenMainTrackNumber, tenStatingNo, tenHangerNo);
                        CANProductsQueryService.Instance.HangingPieceHandler(tenMainTrackNumber, tenStatingNo, tenHangerNo);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), sData, null);

                    }

                }
                catch (NoFoundOnlineProductsException ex)
                {
                    server.SendExcpetionOrPromptInfo(tenMainTrackNumber, tenStatingNo, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundOnlineProducts, XOR);
                    var sysEx = string.Format("【衣架工序比较】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
                catch (Exception ex)
                {
                    var sysEx = string.Format("【衣架工序比较】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
            }
        }

        #endregion

        #region 出站相关
        //【硬件对衣架分配成功的回应】
        //软件让衣架出站【衣架给硬件分配成功后，才让衣架出站】
        private void Client_AllocationHangerResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
           // lock (lockObject)
            {
                var message = e.Tag as SusNet.Common.SusBusMessage.AllocationHangerResponseMessage;

                if (null != message)
                {
                    var susAllocatingMessage = string.Format("【衣架出站响应】 主轨【{0}】 站点【{1}】 衣架号【{2}】已保存!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), SusNet.Common.Utils.HexHelper.HexToTen(message.HangerNo));
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
                    tcpLogInfo.Info(susAllocatingMessage);

                    var tenMaintrackNumber = HexHelper.HexToTen(message.XID);
                    var tenStatingNo = HexHelper.HexToTen(message.ID);
                    var tenHangerNo = HexHelper.HexToTen(message.HangerNo);
                    ////var productNumber = SusNet.Common.Utils.HexHelper.HexToTen(message.ProductionNumber);
                    try
                    {
                        int flowType = 0;
                        string tenOutSiteStatingNo = null;
                        var isReworkSourceStating = false;
                        var isMonitoringAllocation = false;
                        bool isStatingStorage = false;
                        int outSiteMaintrackNumber = 0;
                        var isBridgeStatingOutStatingAllocate = false;
                        var isF2Assign = false;
                        CANProductsService.Instance.HangerOutStatingResponse(tenMaintrackNumber.ToString(), tenStatingNo.ToString(), tenHangerNo.ToString(), ref outSiteMaintrackNumber, ref tenOutSiteStatingNo, ref flowType, ref isReworkSourceStating, ref isMonitoringAllocation, ref isStatingStorage, ref isBridgeStatingOutStatingAllocate, ref isF2Assign);
                        var hexOutSiteMainTrackNumber = HexHelper.TenToHexString2Len(outSiteMaintrackNumber);
                        if (string.IsNullOrEmpty(tenOutSiteStatingNo))
                        {
                            tcpLogInfo.Info(string.Format("【衣架出站pc响应】 下一站为空! 主轨:{0} 请求站:{1} 衣架:{2}", tenMaintrackNumber, tenStatingNo, tenHangerNo));
                        }
                        if (!string.IsNullOrEmpty(tenOutSiteStatingNo))
                        {
                            //监测站分配不推送产量
                            if (isMonitoringAllocation)
                            {
                                var monitoringAllocationMessage = string.Format("【衣架出站响应】【监测点分配衣架】 监测点分配衣架成功! 主轨:{0} 下一站:{1} 衣架:{2} ", hexOutSiteMainTrackNumber, tenOutSiteStatingNo, tenHangerNo);
                                if (!isServiceStart)
                                    lbMessage?.Invoke(new EventHandler(this.AddMessage), monitoringAllocationMessage, null);
                                return;
                            }
                            //F2指定分配
                            if (isF2Assign)
                            {
                                var monitoringAllocationMessage = string.Format("【衣架出站响应】【F2指定分配衣架】 F2指定分配衣架成功! 主轨:{0} 下一站:{1} 衣架:{2} ", hexOutSiteMainTrackNumber, tenOutSiteStatingNo, tenHangerNo);
                                if (!isServiceStart)
                                    lbMessage?.Invoke(new EventHandler(this.AddMessage), monitoringAllocationMessage, null);
                                return;
                            }
                            #region 衣架出站 及产量推送
                            var outMessageInfo = string.Format("【衣架出站pc响应】 衣架出站指令发送开始! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", hexOutSiteMainTrackNumber, tenStatingNo, tenOutSiteStatingNo, tenHangerNo);
                            tcpLogInfo.Info(outMessageInfo);

                            //【返工衣架】
                            if (flowType == 1)
                            {
                                #region 【返工相关】
                                //返工衣架出站
                                //var messageOut = new ReworkResponseMessage(message.XID, outSiteStatingNo, 0, HexHelper.TenToHexString10Len(hangerNo), XOR);
                                var messageOut = new ReworkResponseMessage(hexOutSiteMainTrackNumber, tenOutSiteStatingNo, 0, HexHelper.TenToHexString10Len(tenHangerNo), XOR);
                                server.SendMessageByByte(messageOut.GetBytes(), new ClientUserInfo("1", message.XID));

                                #region Test相关
                                if (ToolsBase.isUnitTest)
                                {
                                    if (ToolsBase.AllocationHanger.ContainsKey(tenHangerNo.ToString()))
                                    {

                                        var hangers = ToolsBase.AllocationHanger[tenHangerNo.ToString()];
                                        LowerPlaceInstrHangerIncomeStatingResponse.Instance.HangerComeInStating(HexHelper.HexToTen(hangers.HexMaintrackNumber).ToString()
                                            , HexHelper.HexToTen(hangers.HexStatingNo).ToString(), tenHangerNo.ToString());
                                        var isBrdgeSet = CANProductsService.Instance.IsBridge(HexHelper.HexToTen(hangers.HexMaintrackNumber), HexHelper.HexToTen(hangers.HexStatingNo));
                                        if (!isBrdgeSet)
                                        {
                                            LowerPlaceInstrHangerCompareRequest.Instance.SendHangerCompareRequest(HexHelper.HexToTen(hangers.HexMaintrackNumber).ToString()
                                                , HexHelper.HexToTen(hangers.HexStatingNo).ToString(), tenHangerNo.ToString());
                                        }
                                    }
                                }
                                #endregion

                                var outSiteDataMessage = string.Format("【衣架出站响应】【返工衣架】出站指令推送成功!  主轨:{0} 本站:{1} 衣架:{2}", hexOutSiteMainTrackNumber, tenOutSiteStatingNo, tenHangerNo);
                                if (!isServiceStart)
                                    lbMessage?.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);

                                tcpLogInfo.Info(outSiteDataMessage);
                                if (!string.IsNullOrEmpty(tenOutSiteStatingNo))
                                {
                                    var clearHangerNoCache = string.Format("【衣架出站响应】正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", outSiteMaintrackNumber, tenOutSiteStatingNo, tenHangerNo);
                                    tcpLogInfo.Info(clearHangerNoCache);
                                    CANTcpServer.server.ClearHangerCache(outSiteMaintrackNumber, int.Parse(tenOutSiteStatingNo), tenHangerNo, XOR);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);

                                }

                                //【返工发起站点不推送产量】是否是返工工序发起站点出站
                                if (!isReworkSourceStating)
                                {
                                    var info = string.Empty;
                                    var decOutSiteNo = int.Parse(tenOutSiteStatingNo);
                                    var fData = CANProductsQueryService.Instance.GetReworkHangerOutSiteFlowInfo(tenMaintrackNumber, decOutSiteNo, tenHangerNo, ref info);

                                    var rOutSiteDataMessage = string.Format("【衣架出站响应】【返工衣架-->衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", tenMaintrackNumber, decOutSiteNo, tenHangerNo, info);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), rOutSiteDataMessage, null);
                                    tcpLogInfo.Info(rOutSiteDataMessage);

                                    server.SendDataByReworkSiteOutSite(fData, message.XID, HexHelper.TenToHexString2Len(tenOutSiteStatingNo));
                                    rOutSiteDataMessage = string.Format("【衣架出站响应】【返工衣架-->衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", tenMaintrackNumber, decOutSiteNo, tenHangerNo, info);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), rOutSiteDataMessage, null);
                                    tcpLogInfo.Info(rOutSiteDataMessage);
                                }
                                #endregion
                            }
                            else
                            {
                                //正常 衣架出站
                                server.AutoHangerOutStating(hexOutSiteMainTrackNumber, HexHelper.TenToHexString2Len(tenOutSiteStatingNo), true, tenHangerNo, XOR);

                                #region Test相关
                                if (ToolsBase.isUnitTest)
                                {
                                    if (ToolsBase.AllocationHanger.ContainsKey(tenHangerNo.ToString()))
                                    {

                                        var hangers = ToolsBase.AllocationHanger[tenHangerNo.ToString()];
                                        LowerPlaceInstrHangerIncomeStatingResponse.Instance.HangerComeInStating(HexHelper.HexToTen(hangers.HexMaintrackNumber).ToString()
                                            , HexHelper.HexToTen(hangers.HexStatingNo).ToString(), tenHangerNo.ToString());
                                        var isBrdgeSet = CANProductsService.Instance.IsBridge(HexHelper.HexToTen(hangers.HexMaintrackNumber), HexHelper.HexToTen(hangers.HexStatingNo));
                                        if (!isBrdgeSet)
                                        {
                                            LowerPlaceInstrHangerCompareRequest.Instance.SendHangerCompareRequest(HexHelper.HexToTen(hangers.HexMaintrackNumber).ToString()
                                                , HexHelper.HexToTen(hangers.HexStatingNo).ToString(), tenHangerNo.ToString());
                                        }
                                    }
                                }
                                #endregion

                                if (!isMonitoringAllocation)
                                {
                                    // tcpLogInfo.Info(string.Format("【衣架出站pc响应】 衣架出站指令发送成功! 主轨:{0}  本站:{1} 下一站:{2} 衣架:{3}", hexOutSiteMainTrackNumber, outSiteStatingNo, tenStatingNo, tenHangerNo));

                                    // var sucessOutSiteMessage = string.Format("【衣架出站pc响应】 衣架出站指令发送成功! 主轨:{0} 本站:{1} 下一站:{2} 衣架:{3}", hexOutSiteMainTrackNumber, outSiteStatingNo, tenStatingNo, tenHangerNo);
                                    var sucessOutSiteMessage = string.Format("【衣架出站pc响应】 衣架出站指令发送成功! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", hexOutSiteMainTrackNumber, tenStatingNo, tenOutSiteStatingNo, tenHangerNo);
                                    tcpLogInfo.Info(sucessOutSiteMessage);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                                }

                                if (!string.IsNullOrEmpty(tenOutSiteStatingNo))
                                {
                                    var clearHangerNoCache = string.Format("【衣架出站响应】正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", outSiteMaintrackNumber, tenOutSiteStatingNo, tenHangerNo);
                                    tcpLogInfo.Info(clearHangerNoCache);
                                    CANTcpServer.server.ClearHangerCache(outSiteMaintrackNumber, int.Parse(tenOutSiteStatingNo), tenHangerNo, XOR);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);
                                }
                                //判断是否是挂片站(非挂片站推送)
                                //计算 2018年3月27日 21:54:31 lucifer
                                /*
                                 今日数（2字节）+
                                 效率（2字节）
                                 本次工序时间（2字节）
                                 */
                                var isHangingPieceStating = CANProductsService.Instance.IsHangPieceStating(outSiteMaintrackNumber, int.Parse(tenOutSiteStatingNo));// new ProductsQueryServiceImpl().isHangingPiece(null, outSiteStatingNo, XID.ToString());

                                //var isStatingStorage = CANProductsService.Instance.IsStatingStorage(XID, int.Parse(outSiteStatingNo));
                                //非挂片站和挂片站都要发送产量//zxl/2018年6月19日 23:03:54
                                if (!isHangingPieceStating && !isStatingStorage)
                                {
                                    #region 【普通站出站产量推送】
                                    var info = string.Empty;
                                    var decOutSiteNo = int.Parse(tenOutSiteStatingNo);
                                    List<Byte> outData = null;

                                    var isSendOutData = CANProductsQueryService.Instance.GetOutSiteHangerFlowInfo(tenMaintrackNumber, decOutSiteNo, tenHangerNo, ref outData, ref info);
                                    if (isSendOutData)
                                    {
                                        var outSiteDataMessage = string.Format("【衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", tenMaintrackNumber, decOutSiteNo, tenHangerNo, info);
                                        if (!isServiceStart)
                                            lbMessage?.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
                                        tcpLogInfo.Info(outSiteDataMessage);

                                        server.SendDataByCommonSiteOutSite(outData, message.XID, HexHelper.TenToHexString2Len(tenOutSiteStatingNo));
                                        outSiteDataMessage = string.Format("【衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", tenMaintrackNumber, decOutSiteNo, tenHangerNo, info);
                                        if (!isServiceStart)
                                            lbMessage?.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
                                        tcpLogInfo.Info(outSiteDataMessage);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 【挂片出站产量推送】
                                    var info = string.Empty;
                                    var decOutSiteNo = HexHelper.HexToTen(tenOutSiteStatingNo);
                                    List<Byte> outData = null;
                                    var isSendOutData = CANProductsQueryService.Instance.GetOutSiteHagingPieceFlowInfo(outSiteMaintrackNumber, decOutSiteNo, tenHangerNo, ref outData, ref info);
                                    if (isSendOutData)
                                    {
                                        var outSiteDataMessage = string.Format("【衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", hexOutSiteMainTrackNumber, decOutSiteNo, tenHangerNo, info);
                                        if (!isServiceStart)
                                            lbMessage?.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
                                        tcpLogInfo.Info(outSiteDataMessage);

                                        server.SendDataByCommonSiteOutSite(outData, hexOutSiteMainTrackNumber, HexHelper.TenToHexString2Len(tenOutSiteStatingNo));
                                        outSiteDataMessage = string.Format("【衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", hexOutSiteMainTrackNumber, decOutSiteNo, tenHangerNo, info);
                                        if (!isServiceStart)
                                            lbMessage?.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
                                        tcpLogInfo.Info(outSiteDataMessage);
                                    }
                                    #endregion
                                }
                            }

                            #endregion

                            #region //桥接站不在工艺图出战清除桥接站逆向的站点衣架缓存
                            if (isBridgeStatingOutStatingAllocate)
                            {
                                BridgeSet bridgeSet = null;
                                bool isNonInBridge = CANProductsService.Instance.IsNonInBridgeByOutsiteResponseAction(outSiteMaintrackNumber, tenOutSiteStatingNo, tenHangerNo, tenMaintrackNumber, tenStatingNo, ref bridgeSet);
                                if (isNonInBridge)
                                {
                                    var clearHangerNoCache = string.Format("【衣架出站响应】【桥接】正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", bridgeSet.AMainTrackNumber.Value, bridgeSet.ASiteNo, tenHangerNo);
                                    CANTcpServer.server.ClearHangerCache(bridgeSet.AMainTrackNumber.Value, bridgeSet.ASiteNo.Value, tenHangerNo, XOR);
                                    if (!isServiceStart)
                                        lbMessage?.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);
                                }
                            }
                            #endregion

                        }



                    }

                    catch (ApplicationException ex)
                    {
                        var cusExData = ex.Message;
                        tcpLogError.Error("【衣架出站响应】", ex);
                        var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                        //回显异常数据到终端
                        CANTcpServer.server.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), tenMaintrackNumber, tenStatingNo);

                    }
                    catch (Exception ex)
                    {
                        var sysEx = string.Format("【衣架出站响应】 异常:{0}", ex.Message);
                        tcpLogError.Error(sysEx, ex);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                    }
                }
            }
        }
        //【硬件请求衣架出站】
        /// <summary>
        /// 硬件请求衣架出站 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static readonly object lockObject = new object();
        private void Client_HangerOutStatingRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            // lock (lockObject)
            {
                SusThreadShareState message = new SusThreadShareState();
                message.Sender = sender;
                message.EventArg = e;
                //ThreadPool.SetMaxThreads(1, 1);

                //ThreadPool.QueueUserWorkItem(
                //   new WaitCallback(OutRequestTask), message);
                OutRequestTask(message);
            }
        }
        private void OutRequestTask(object state)
        {
            SusThreadShareState susState = state as SusThreadShareState;
            OutRequestHandler(susState.Sender, susState.EventArg);
        }

        private void OutRequestHandler(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            //衣架从出站

            var req = e.Tag as SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage;
            var tenMaintracknumber = HexHelper.HexToTen(req.XID);
            var tenStatingNo = HexHelper.HexToTen(req.ID);
            var tenHangerNo = HexHelper.HexToTen(req.HangerNo);
            var outType = CANProductsService.Instance.IsHangPieceStating(tenMaintracknumber, tenStatingNo) ? 1 : 0;//HexHelper.HexToTen(req.outType);1:挂片站;0:普通站
                                                                                                                   // string tenNextStatingNo = null;

            // var isFlowSucess = false;
            var infoEx = string.Empty;
            try
            {
                #region 系统参数相关
                if (outType == 1)
                {
                    //出站判断  “挂片站出衣架达到计划数后停止出衣”
                    bool result = SystemParameterService.Instance.StartingStopOutWhenOverPlan(tenMaintracknumber, tenStatingNo);
                    if (result == false)
                    {
                        throw new StartingStopOutWhenOverPlanException("挂片站出衣架达到计划数后停止出衣");
                    }
                }
                //挂片站出衣架数， 计划数
                #endregion

                SusDispatcher.Instance.DoService(sender, e, lbMessage, new EventHandler(this.AddMessage), isServiceStart);
            }
            catch (StartingStopOutWhenOverPlanException ex)
            {
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                CANTcpServer.server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, (int)_0130ExcpetionTag.StartingStopOutWhenOverPlan, XOR, e.TcpClient);
            }
            catch (StatingNoLoginEmployeeException ex)
            {
                server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, SuspeConstants.tag_ExcpetionOrPromptInfo_EmployeeNoLoginStating, XOR, e.TcpClient);
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

            }
            catch (NoFoundOnlineProductsException ex)
            {
                server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundOnlineProducts, XOR, e.TcpClient);
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

            }
            catch (FullStatingExcpetion ex)
            {
                var fullStatingNo = tenStatingNo;
                if (ex.FullStatingList != null && ex.FullStatingList.Count != 0)
                {
                    //foreach (var fStat in ex.FullStatingList)
                    //{
                    //   server.SendExcpetionOrPromptInfo(tenMaintracknumber, int.Parse(fStat), SuspeConstants.tag_FullSiteOrStopWork, XOR, e.TcpClient);
                    //   //server.SendExcpetionOrPromptInfoByFullStating(tenMaintracknumber,tenStatingNo, int.Parse(fStat))
                    //}
                    server.SendExcpetionOrPromptInfoByFullStating(tenMaintracknumber, tenStatingNo, ex.FullStatingList, ex.FullFlowNo, XOR, e.TcpClient);
                }
                else
                {
                    server.SendExcpetionOrPromptInfoByFullStating(tenMaintracknumber, tenStatingNo, null, ex.FullFlowNo, XOR, e.TcpClient);
                }
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
            }
            catch (NoFoundStatingException ex)
            {
                var promptData = UnicodeUtils.CharacterToCoding(ex?.FlowNo?.Trim());
                var promtDataByte = DataFillingUtils.Get5ByteDF(new List<Byte>(HexHelper.StringToHexByte(promptData)), 0);
                server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundNextStating, XOR, e.TcpClient);
                var cusExData = string.Format(ex.Message + " 工序号:{0}", ex?.FlowNo?.Trim());
                tcpLogError.Error("【衣架出站】", ex);
                //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
            }
            catch (NonAllocationOutStatingException ex)
            {
                server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, SuspeConstants.tag_ExcptionNonAllocationOutStating, XOR, e.TcpClient);
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
            }
            catch (NonExistStatingException ex)
            {
                server.SendExcpetionOrPromptInfo(tenMaintracknumber, tenStatingNo, SuspeConstants.tag_ExcptionNonExistStating, XOR, e.TcpClient);
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);
            }
            catch (ApplicationException ex)
            {
                var cusExData = ex.Message;
                tcpLogError.Error("【衣架出站】", ex);
                var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), cusExData, null);

                ////回显异常数据到终端
                //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);
            }
            catch (Exception ex)
            {
                var sysEx = string.Format("【衣架出站】 异常:{0}", ex.Message);
                tcpLogError.Error(sysEx, ex);
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
            }
            finally
            {

            }
        }


        private WaitCallback OutHanderl(SusThreadShareState susThreadShareState)
        {
            return null;

        }

        #endregion

        #region 衣架进站
        /// <summary>
        /// 衣架进站上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_HangerArrivalStatingMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            //衣架进站
            var message = e.Tag as SusNet.Common.SusBusMessage.HangerArrivalStatingRequestMessage;
            if (null != message)
            {

                try
                {
                    var tenMainTracknumber = HexHelper.HexToTen(message.XID);
                    var tenHangerNo = HexHelper.HexToTen(message.HangerNo);
                    var tenStatingNo = HexHelper.HexToTen(message.ID);
                    var isHangerRepeatInStating = false;
                    CANProductsService.Instance.RecordHangerArriveStating(tenMainTracknumber, tenHangerNo.ToString(), tenStatingNo.ToString(), ref isHangerRepeatInStating);
                    if (isHangerRepeatInStating)
                    {
                        var susRepeatInStatingMessage = string.Format("【衣架进站】【消息】 主轨【{0}】 站点【{1}】 衣架号【{2}】重复进站!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), tenHangerNo.ToString());
                        tcpLogInfo.Info(susRepeatInStatingMessage);
                        if (!isServiceStart)
                            lbMessage?.Invoke(new EventHandler(this.AddMessage), susRepeatInStatingMessage, null);
                        return;
                    }
                    var susAllocatingMessage = string.Format("【衣架进站】【消息】 主轨【{0}】 站点【{1}】 衣架号【{2}】已进站!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), tenHangerNo.ToString());
                    tcpLogInfo.Info(susAllocatingMessage);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
                }
                catch (Exception ex)
                {
                    var sysEx = string.Format("【衣架进站】 异常:{0}", ex.Message);
                    tcpLogError.Error(sysEx, ex);
                    if (!isServiceStart)
                        lbMessage?.Invoke(new EventHandler(this.AddMessage), sysEx, null);
                }
            }
        }

        #endregion

        #region 挂片站上线
        //挂片站上线【把上线的排产号设置为当前上线的产品】
        /// <summary>
        /// 挂片站上线上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_HangingPieceStatingOnlineMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            var message = e.Tag as HangingPieceStatingOnlineRequestMessage;
            var XID = HexHelper.HexToTen(message.XID);
            var sucess = CANProductsService.Instance.BindHangpieceStatingOnlineProductNumber(XID, message.ProductNumber);
            if (sucess)
            {
                var messageRes = string.Format("{0} {1} {2} {3} {4} 00 00 00 00 00 {5}", message.XID, message.ID, SuspeConstants.cmd_HangPieceOnline_Res, XOR, SuspeConstants.address_HangpieceOnline, HexHelper.TenToHexString2Len(message.ProductNumber));
                tcpLogInfo.Info(string.Format("【挂片站上线产品回应发送开始】 发送内容-->{0}", messageRes));
                server.SendMessageByByte(HexHelper.StringToHexByte(messageRes), new ClientUserInfo("", message.XID));
                if (!isServiceStart)
                    tcpLogInfo.Info(string.Format("【挂片站上线产品回应发送完成】 发送内容-->{0}", messageRes));
            }
            //回应
        }
        #endregion

        #region 制品界面直接上线
        /// <summary>
        /// 制品界面直接上线 下发回应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_ClientMachineResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            Console.WriteLine(string.Format("【{0}】 收到消息-->【{1}】", "Client_ClientMachineResponseMessageReceived", e.message));

            var message = e.Tag as SusNet.Common.SusBusMessage.ClientMachineResponseMessage;

            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);
            var productNumber = message.ProductNumber;//SusNet.Common.Utils.HexHelper.HexToTen(message.ProductNumber);

            var susRemotingMessage = string.Format("【制品界面直接上线】收到主轨【{0}】 挂片站【{1}】 响应!", XID.ToString(), ID.ToString());
            tcpLogInfo.Info(susRemotingMessage);
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);

            var sendMessageT = string.Format("【制品界面直接上线】 主轨【{0}】挂片站【{1}】 等待放衣架.....", XID.ToString(), ID.ToString());
            if (!isServiceStart)
                lbMessage?.Invoke(new EventHandler(AddMessage), sendMessageT, null);

            SusCacheBootstarp.Instance.Init();

            //制品界面上线后，上线制品信息给挂片站推送

            /*
            try
            {
                var data = CANProductsQueryService.Instance.GetProducts(XID, ID, productNumber);//string.Format("933304-9BUY,010,28,任务1863件,单位1件,累计出1117件,今日出213件");
                //   var hexData = HexHelper.ToHex(data, "utf-8", false);
                var hexChs = UnicodeUtils.GetHexFromChs(data);
                var hexBytes = HexHelper.StringToHexByte(hexChs);//HexHelper.strToToHexByte(hexData);

                string mainTrackNo = HexHelper.TenToHexString(XID); //SusNet.Common.Utils.HexHelper.tenToHexString("1");//主轨暂时写死的
                string statingNo = HexHelper.TenToHexString(ID);//SusNet.Common.Utils.HexHelper.tenToHexString(message.StatingNo);
                tcpLogInfo.Info(string.Format("【制品界面直接上线】 将要发送的内容--->【{0}】", data));
                Console.WriteLine(string.Format("【制品界面直接上线】 将要发送的内容 pc--->硬件【{0}】", data));
                client.SendDataByProductsDirectOnline(new System.Collections.Generic.List<byte>(hexBytes), mainTrackNo, statingNo, "FF");

                var strSucess = string.Format("【制品界面直接上线】 制品信息【{0}】发送完成!", data);
                tcpLogInfo.Info(strSucess);
                lbMessage?.Invoke(new EventHandler(AddMessage), strSucess, null);
            }
            catch (Exception ex)
            {
                tcpLogError.Error("【制品界面直接上线】 异常:", ex);
            }
            */

        }

        #endregion

        #region 主轨相关
        /// <summary>
        /// 停止主轨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_StopMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            tcpLogInfo.Info(e.message);
            var message = e.Tag as SusNet.Common.SusBusMessage.StopMainTrackResponseMessage;
            if (null != message)
            {
                var susRemotingMessage = string.Format("【停止主轨消息】 主轨【{0}】 已停止!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
                tcpLogInfo.Info(susRemotingMessage);
            }
        }
        /// <summary>
        /// 启动主轨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_StartMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            tcpLogInfo.Info(e.message);
            var message = e.Tag as SusNet.Common.SusBusMessage.StartMainTrackResponseMessage;
            if (null != message)
            {
                var susRemotingMessage = string.Format("【启动主轨消息】 主轨【{0}】 已启动!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
                tcpLogInfo.Info(susRemotingMessage);
            }
        }
        /// <summary>
        /// 急停主轨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_EmergencyStopMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        {
            tcpLogInfo.Info(e.message);
            var message = e.Tag as SusNet.Common.SusBusMessage.EmergencyStopMainTrackResponseMessage;
            if (null != message)
            {
                var susRemotingMessage = string.Format("【急停主轨消息】 主轨【{0}】 急停成功!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
                if (!isServiceStart)
                    lbMessage?.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
                tcpLogInfo.Info(susRemotingMessage);
            }
        }
        #endregion

        #region 连接主轨查询
        /// <summary>
        /// 连接主轨查询
        /// </summary>
        public void ConnectedQueryMaintrackNumber(string clientKeys)
        {
            server.ConnectedQueryMaintrackNumber(clientKeys);
        }
        #endregion
    }
}

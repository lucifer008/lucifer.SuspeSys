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
using SuspeSys.SusRedis.SusRedis.SusConst;

namespace Suspe.CAN.Action.CAN
{
    [Obsolete("此类已废弃;由TcpServer代替")]
    public class CANTcp : SusLog
    {
        private CANTcp()
        {

        }
        public static SusTCPClient client;
        public readonly static CANTcp Instance = new CANTcp();
        static readonly string CanIp = System.Configuration.ConfigurationManager.AppSettings["CANIp"];
        static readonly int CANPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CANPort"]);
        static bool isConnected = false;
        const string XOR = SuspeConstants.XOR;
        // private static readonly ILog log = LogManager.GetLogger(typeof(CANTcp));
        ListBoxControl lbMessage = null;
        bool isServiceStart;
        public void ConnectCAN(ListBoxControl _lbMessage, bool _isServiceStart = false)
        {
            //try
            //{
            //    if (!isConnected)
            //    {
            //        isServiceStart = _isServiceStart;
            //        if (!isServiceStart)
            //        {
            //            lbMessage = _lbMessage;
            //        }
            //        ClientUserInfo info = new ClientUserInfo("gid", "testId");
            //        client = new SusTCPClient(info, CanIp, CANPort);
            //        client.MessageReceived += client_PlaintextReceived;
            //        client.Connect();
            //        client.ServerConnected += Client_ServerConnected;
            //        client.ServerDisconnected += Client_ServerDisconnected;
            //        client.ServerExceptionOccurred += Client_ServerExceptionOccurred;
            //        client.EmergencyStopMainTrackResponseMessageReceived += Client_EmergencyStopMainTrackResponseMessageReceived;
            //        client.StartMainTrackResponseMessageReceived += Client_StartMainTrackResponseMessageReceived;
            //        client.StopMainTrackResponseMessageReceived += Client_StopMainTrackResponseMessageReceived;
            //        client.ClientMachineResponseMessageReceived += Client_ClientMachineResponseMessageReceived;
            //        client.HangingPieceStatingOnlineMessageReceived += Client_HangingPieceStatingOnlineMessageReceived;
            //        client.HangerArrivalStatingMessageReceived += Client_HangerArrivalStatingMessageReceived;
            //        client.HangerOutStatingRequestMessageReceived += Client_HangerOutStatingRequestMessageReceived;
            //        //client.HangingPieceHangerUploadRequestMessageReceived += Client_HangingPieceHangerUploadRequestMessageReceived;
            //        client.AllocationHangerResponseMessageReceived += Client_AllocationHangerResponseMessageReceived;
            //        client.HangerDropCardRequestMessageReceived += Client_HangerDropCardRequestMessageReceived;
            //        client.HangerReworkMessageReceived += Client_HangerReworkMessageReceived;
            //        //client.ReworkDefectMessageReceived += Client_ReworkDefectMessageReceived;
            //        client.CardRequestMessageReceived += Client_CardRequestMessageReceived;
            //        client.ClearHangerCacheResponseMessageReceived += Client_ClearHangerCacheResponseMessageReceived;
            //        client.FullSiteMessageReceived += Client_FullSiteMessageReceived;
            //        client.MonitorMessageReceived += Client_MonitorMessageReceived;
            //        client.ReworkFlowDefectRequestMessageReceived += Client_ReworkFlowDefectRequestMessageReceived;
            //        client.StatingCapacityResponseMessageReceived += Client_StatingCapacityResponseMessageReceived;
            //        client.StatingTypeResponseMessageReceived += Client_StatingTypeResponseMessageReceived;
            //        client.PowerSupplyInitMessageReceived += Client_PowerSupplyInitMessageReceived;
            //        client.SNSerialNumberMessageReceived += Client_SNSerialNumberMessageReceived;
            //        client.MainboardVersionMessageReceived += Client_MainboardVersionMessageReceived;
            //        client.LowerMachineSuspendOrReceiveMessageReceived += Client_LowerMachineSuspendOrReceiveMessageReceived;
            //        client.UpperComputerInitResponseMessageReceived += Client_UpperComputerInitResponseMessageReceived;
            //        client.FullSiteQueryResponseMessageReceived += Client_FullSiteQueryResponseMessageReceived;
            //        isConnected = true;
            //        tcpLogInfo.Info("已启动CAN连接!");
            //        var sucMessage = string.Format("已启动CAN连接,端口:【{0}】 .....", CANPort);
            //        if (!isServiceStart)
            //        {
            //            lbMessage.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
            //        }
            //        tcpLogInfo.Info(sucMessage);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tcpLogError.Error("【CAN连接失败】", ex);
            //}
        }

        ////上位机发起的初始化【满站查询回应】
        //private void Client_FullSiteQueryResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as FullSiteMessage;
        //    var XID = HexHelper.HexToTen(message.XID);
        //    var ID = HexHelper.HexToTen(message.ID);
        //    var isFullSite = message.IsFullSite;
        //    var sucessMessage = string.Format("【上位机发起的初始化【满站查询回应】 主轨【{0}】 站点【{1}】 是否满站【{2}】!", XID, ID, message.IsFullSite ? "是" : "否");
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //    tcpLogInfo.Info(sucessMessage);
        //    CANProductsService.Instance.UpdateMainTrackStatingStatus(XID, ID, isFullSite);
        //}

        ////上位机发起的初始化，下位机的回应
        //private void Client_UpperComputerInitResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    //var message = e.Tag as UpperComputerInitResponseMessage;
        //    //StatingServiceImpl.Instance.UpdateStatingMainboard(message.MainTrackNo, message.StatingNo,
        //    //   message.MainboardVersion.ToString());
        //    //StatingServiceImpl.Instance.UpdateStatingSN(message.MainTrackNo, message.StatingNo,
        //    //    message.SN.ToString());
        //    //new SuspeApplication().PowerSupplyInit(message.MainTrackNo, message.StatingNo, client);
        //}

        //#region 下位机更新站点接收或停止接收衣架
        //private void Client_LowerMachineSuspendOrReceiveMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as LowerMachineSuspendOrReceiveMessage;
        //    StatingServiceImpl.Instance.UpdateStatingSuspendOrReceive(message.XID, message.ID, message.Tag);
        //    client.SuspendOrReceiveHangerReponseToLowerMachine(HexHelper.HexToTen(message.XID), HexHelper.HexToTen(message.ID), message.Tag);
        //}
        //#endregion

        //#region 主版版本号及SN号上传
        //private void Client_MainboardVersionMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as MainboardVersionRequestMessage;
        //    StatingServiceImpl.Instance.UpdateStatingMainboard(message.MainTrackNo, message.StatingNo,
        //        HexHelper.HexToTen(message.MainboardVersion).ToString());
        //}

        //private void Client_SNSerialNumberMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as SNSerialNumberRequestMessage;
        //    StatingServiceImpl.Instance.UpdateStatingSN(message.MainTrackNo, message.StatingNo,
        //        HexHelper.HexToTen(message.SN).ToString());
        //}
        //#endregion

        //#region //上电初始化
        //private void Client_PowerSupplyInitMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    //var message = e.Tag as PowerSupplyInitRequestMessage;
        //   // new SuspeApplication().PowerSupplyInit(message.MainTrackNo, message.StatingNo, client);
        //}
        //#endregion

        //#region 站类型相关
        ///// <summary>
        ///// 修改站点类型
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Client_StatingTypeResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var res = e.Tag as StatingTypeResponseMessage;
        //    if (null != res)
        //    {
        //        var XID = HexHelper.HexToTen(res.XID);
        //        var ID = HexHelper.HexToTen(res.ID);
        //        var OpType = res.OpType;
        //        var statingType = res.StatingType;
        //        var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 操作类型【{2}】站点类型【{3}】 站点类型操作完成!", XID, ID, OpType, statingType);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //        tcpLogInfo.Info(sucessMessage);
        //    }
        //}

        ///// <summary>
        ///// 修改站点容量
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Client_StatingCapacityResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var res = e.Tag as StatingCapacityResponseMessage;
        //    if (null != res)
        //    {
        //        if (res.CMD == SuspeConstants.cmd_StatingCapacity_4)
        //        {
        //            var XID = HexHelper.HexToTen(res.XID);
        //            var ID = HexHelper.HexToTen(res.ID);
        //            var capacity = res.Capacity;
        //            var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!", XID, ID, capacity);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //            tcpLogInfo.Info(sucessMessage);
        //        }
        //        else if (res.CMD == SuspeConstants.cmd_StatingCapacity_6)
        //        {
        //            //设置站容量（液晶设置时，主动上传PC）
        //            StatingModel stating = new StatingModel()
        //            {
        //                MainTrackNumber = short.Parse(res.XID),
        //                StatingNo = res.ID.TrimStart('0'),
        //                Capacity = res.Capacity,
        //            };

        //            string json = JsonConvert.SerializeObject(stating);
        //            SusRedisClient.subcriber.Publish(SusRedisConst.STATING_EDIT_ACTION, json);


        //            var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!", res.XID, res.id, res.Capacity);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //            tcpLogInfo.Info(sucessMessage);
        //        }
        //        else if (res.CMD == SuspeConstants.cmd_StatingCapacity_4)
        //        {
        //            //设置站容量（回复）
        //            var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 容量【{2}】 容量修改完成!（硬件发送）", res.XID, res.id, res.Capacity);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //            tcpLogInfo.Info(sucessMessage);
        //        }

        //    }
        //}

        //#endregion

        //#region 监测点
        //private void Client_MonitorMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var res = e.Tag as MonitorMessage;
        //    if (null != res)
        //    {
        //        var XID = HexHelper.HexToTen(res.XID);
        //        var ID = HexHelper.HexToTen(res.ID);
        //        var mtMontor = new MainTrackStatingMontorModel() { MainTrackNumber = XID, StatingNo = ID, HangerNo = res.HangerNo.ToString() };
        //        var mtMontorJson = JsonConvert.SerializeObject(mtMontor);
        //        SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION, mtMontorJson);
        //        var sucessMessage = string.Format("【监测点消息】 主轨【{0}】 站点【{1}】 衣架【{2}】!", XID, ID, res.HangerNo);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //        tcpLogInfo.Info(sucessMessage);
        //    }

        //}
        //#endregion

        //#region 站点状态
        ////站点状态监测更新
        //private void Client_FullSiteMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as FullSiteMessage;
        //    var XID = HexHelper.HexToTen(message.XID);
        //    var ID = HexHelper.HexToTen(message.ID);
        //    var isFullSite = message.IsFullSite;
        //    var sucessMessage = string.Format("【满站消息上报】 主轨【{0}】 站点【{1}】 是否满站【{2}】!", XID, ID, message.IsFullSite ? "是" : "否");
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //    tcpLogInfo.Info(sucessMessage);
        //    CANProductsService.Instance.UpdateMainTrackStatingStatus(XID, ID, isFullSite);
        //}
        //#endregion

        //#region 清除衣架缓存
        ////【协议2.0】 清除衣架缓存
        //private void Client_ClearHangerCacheResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var res = e.Tag as ClearHangerCacheResponseMessage;
        //    if (null != res)
        //    {
        //        var XID = HexHelper.HexToTen(res.XID);
        //        var ID = HexHelper.HexToTen(res.ID);
        //        var hangerNo = res.HangerNo;
        //        var sucessMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 衣架【{2}】 缓存清除完成!", XID, ID, hangerNo);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //        tcpLogInfo.Info(sucessMessage);
        //    }
        //}
        //#endregion

        //#region//【协议2.0】 挂片站上传衣架号信息事件
        ////private void Client_HangingPieceHangerUploadRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //// {
        ////     tcpLogInfo.Info("【协议2.0】 挂片站上传衣架号信息事件---->已触发");
        ////     //接着发送是否出站的命令
        ////     /*
        ////      * 01 01 05 XX 00 59 00 AA BB CC DD EE---允许出站
        ////        01 01 05 XX 00 59 01 AA BB CC DD EE ---不允许出站
        ////      */
        ////     //zxl 2018年3月14日 23:45:04

        ////     var req = e.Tag as HangingPieceHangerUploadRequestMessage;
        ////     if (null != req)
        ////     {
        ////         var XID = HexHelper.HexToTen(req.XID);
        ////         var ID = HexHelper.HexToTen(req.ID);
        ////         var hangerNo = HexHelper.HexToTen(req.HangerNo);
        ////         var productNumber = HexHelper.HexToTen(req.ProductionNumber);

        ////         string nextStatingNo = null;
        ////         try
        ////         {
        ////             int outMainTrackNumber = 0;
        ////             var sucess = CANProductsService.Instance.HangerOutStatingRequest(XID.ToString(), ID.ToString(), productNumber.ToString(), hangerNo.ToString(), ref nextStatingNo, ref outMainTrackNumber);
        ////             if (sucess)
        ////             {
        ////                 if (string.IsNullOrEmpty(nextStatingNo))
        ////                 {
        ////                     var sucessMessage = string.Format("【消息】 主轨【{0}】 站点{1} 衣架【{2}】 生产完成!", XID, ID, hangerNo);
        ////                     lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);

        ////                     tcpLogInfo.Info(string.Format("【衣架生产完成】 衣架出站指令发送开始! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", XID, ID, ID, hangerNo));
        ////                     //衣架出站
        ////                     client.AutoHangerOutStating(req.XID, req.ID, true, hangerNo, XOR);

        ////                     //更新产量
        ////                     string sucessSData = null;
        ////                     var sucessProdOutData = CANProductsQueryService.Instance.GetProductByBytes(XID, ID, productNumber, ref sucessSData);
        ////                     CANTcp.client.SendOutputDataToHangingPiece(sucessProdOutData, req.XID, req.ID);
        ////                     lbMessage.Invoke(new EventHandler(this.AddMessage), sucessSData, null);

        ////                     var clearHangerNoCache1 = string.Format("正在清除站点{0} 衣架【{1}】的站点缓存...", ID, hangerNo);
        ////                     CANTcp.client.ClearHangerCache(XID, ID, hangerNo);
        ////                     lbMessage.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache1, null);

        ////                     return;
        ////                 }
        ////                 var hexID = HexHelper.TenToHexString2Len(outMainTrackNumber);
        ////                 CANTcp.client.AllocationHangerToNextStating(hexID, HexHelper.TenToHexString2Len(nextStatingNo), req.HangerNo);
        ////                 var susAllocatingMessage = string.Format("【消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", XID, nextStatingNo);
        ////                 lbMessage.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);

        ////                 //更新产量
        ////                 string sData = null;
        ////                 var prodOutData = CANProductsQueryService.Instance.GetProductByBytes(XID, ID, productNumber, ref sData);
        ////                 CANTcp.client.SendOutputDataToHangingPiece(prodOutData, req.XID, req.ID);
        ////                 lbMessage.Invoke(new EventHandler(this.AddMessage), sData, null);

        ////                 var clearHangerNoCache = string.Format("正在清除站点{0} 衣架【{1}】的站点缓存...", ID, hangerNo);
        ////                 CANTcp.client.ClearHangerCache(XID, ID, hangerNo);
        ////                 lbMessage.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);

        ////             }
        ////         }
        ////         catch (ApplicationException ex)
        ////         {
        ////             var cusExData = ex.Message;
        ////             tcpLogError.Error("【衣架出站】", ex);
        ////             var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        ////             lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        ////             //回显异常数据到终端
        ////             CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);

        ////         }
        ////         catch (Exception ex)
        ////         {
        ////             var sysEx = string.Format("【衣架出站】 异常:{0}", ex.Message);
        ////             tcpLogError.Error(sysEx, ex);
        ////             lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        ////         }
        ////     }
        //// }
        //#endregion

        //#region 卡相关
        ////【卡片请求】
        //private void Client_CardRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var cardRequestMessage = e.Tag as Sus.Net.Common.SusBusMessage.CardRequestMessage;
        //    if (null != cardRequestMessage)
        //    {
        //        //lbMessage.Invoke(new EventHandler(this.AddMessage), cardRequestMessage.ToString(), null);
        //        var XID = HexHelper.HexToTen(cardRequestMessage.XID);
        //        var ID = HexHelper.HexToTen(cardRequestMessage.ID);
        //        var cardNo = HexHelper.HexToTen(cardRequestMessage.CardNo);
        //        var type = 0;
        //        string info = string.Empty;
        //        try
        //        {

        //            //给下位机发送 --->  [工号]员工姓名   【unicode码发送】
        //            //client.SendDataByEmployeeLoginInfo
        //            var other = string.Empty;
        //            CANProductsService.Instance.CardLogin(XID, ID, cardNo, ref type, ref other, ref info);
        //            switch ((short)type)
        //            {
        //                case 3:
        //                    var cardRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 3, cardRequestMessage.CardNo, XOR);
        //                    client.SendData(cardRes.GetBytes());
        //                    var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
        //                    var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
        //                    client.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), cardRequestMessage.XID, cardRequestMessage.StatingNo, XOR);
        //                    var message = string.Format("主轨:【{0}】站点:【{1}】 卡号:【{2}】 员工:【{3}】 已登录!", XID, ID, cardNo, other);
        //                    if (!isServiceStart)
        //                        lbMessage.Invoke(new EventHandler(this.AddMessage), message, null);
        //                    tcpLogInfo.Info(message);
        //                    break;
        //                case 4:
        //                    var cardRepatRes = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 4, cardRequestMessage.CardNo, XOR);
        //                    client.SendData(cardRepatRes.GetBytes());
        //                    var emRepatMessage = string.Format("主轨:【{0}】站点:【{1}】 卡号:【{2}】 员工:【{3}】 已登出!", XID, ID, cardNo, other);
        //                    if (!isServiceStart)
        //                        lbMessage.Invoke(new EventHandler(this.AddMessage), emRepatMessage, null);
        //                    tcpLogInfo.Info(emRepatMessage);
        //                    break;
        //                default:
        //                    var cardRepatOffDuty = new CardResponseMessage(cardRequestMessage.XID, cardRequestMessage.StatingNo, 0, cardRequestMessage.CardNo, XOR);
        //                    client.SendData(cardRepatOffDuty.GetBytes());
        //                    var emOffDutyMessage = string.Format("主轨:【{0}】站点:【{1}】 卡号:【{2}】 卡不存在或出错!", XID, ID, cardNo);
        //                    if (!isServiceStart)
        //                        lbMessage.Invoke(new EventHandler(this.AddMessage), emOffDutyMessage, null);
        //                    tcpLogInfo.Info(emOffDutyMessage);
        //                    break;
        //            }
        //        }
        //        catch (CanLoginFromStationException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【卡片请求】", ex);
        //            var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);

        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //            //回显异常数据到终端
        //            //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);
        //            CANTcp.client.SendExcpetionOrPromptInfo(XID, ID, (int)_0130ExcpetionTag.CanLoginFromStation);
        //        }
        //        catch (ApplicationException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【卡片请求】", ex);
        //            var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //            //回显异常数据到终端
        //            CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);
        //        }

        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【卡片请求】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }

        //}
        //#endregion

        //////衣架返工疵点代码
        ////private void Client_ReworkDefectMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        ////{
        ////    var hangerReworkDefectMessage = e.Tag as SusNet.Common.SusBusMessage.ReworkDefectRequestMessage;
        ////    if (null != hangerReworkDefectMessage)
        ////    {
        ////        var XID = HexHelper.HexToTen(hangerReworkDefectMessage.XID);
        ////        var ID = HexHelper.HexToTen(hangerReworkDefectMessage.ID);
        ////        var hangerNo = HexHelper.HexToTen(hangerReworkDefectMessage.HangerNo);
        ////        var reworkDefectCode = HexHelper.HexToTen(hangerReworkDefectMessage.ReworkDefectCode);
        ////        var message = string.Format("【衣架返工疵点代码】 收到返工疵点代码请求 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3}", XID, ID, hangerNo, reworkDefectCode);
        ////        tcpLogInfo.Info(message);
        ////        lbMessage.Invoke(new EventHandler(AddMessage), message, null);
        ////        string errMsg = null;
        ////        string nextStatingNo = null;
        ////        try
        ////        {
        ////            bool success = CANProductsService.Instance.RecordReworkDefectHangerInfo(XID, hangerNo, ID, reworkDefectCode, ref nextStatingNo, ref errMsg);
        ////            if (success)
        ////            {
        ////                //对返工到达的站点发出返工请求
        ////                //client.HangerReworkResponse(XID, ID, hangerNo, flowCode);
        ////                //不满足返工条件
        ////                var messageAuto = string.Format("【衣架返工疵点代码】 衣架满足出站条件,正在请求下一站是否满足接收衣架 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3} 下一站:{4}", XID, ID, hangerNo, reworkDefectCode, nextStatingNo);
        ////                tcpLogInfo.Info(messageAuto);
        ////                lbMessage.Invoke(new EventHandler(AddMessage), messageAuto, null);

        ////                client.AllocationHangerToNextStating(hangerReworkDefectMessage.XID, hangerReworkDefectMessage.ID, hangerReworkDefectMessage.HangerNo);
        ////                return;
        ////            }

        ////            //不满足返工条件
        ////            var messageNotAuto = string.Format("【衣架返工疵点代码】 衣架不满足出站条件 主轨:{0} 站号:{1} 衣架号:{2} 疵点代码:{3}", XID, ID, hangerNo, reworkDefectCode);
        ////            tcpLogInfo.Info(messageNotAuto);
        ////            lbMessage.Invoke(new EventHandler(AddMessage), messageNotAuto, null);
        ////        }
        ////        catch (ApplicationException ex)
        ////        {
        ////            var cusExData = ex.Message;
        ////            tcpLogError.Error("【衣架返工疵点代码】", ex);
        ////            var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        ////            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        ////            //回显异常数据到终端
        ////            CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);

        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            var sysEx = string.Format("【衣架返工疵点代码】 异常:{0}", ex.Message);
        ////            tcpLogError.Error(sysEx, ex);
        ////            lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        ////        }
        ////    }
        ////}


        //#region 衣架返工相关
        ////衣架返工
        //private void Client_HangerReworkMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var hangerReworkMessage = e.Tag as SusNet.Common.SusBusMessage.ReworkRequestMessage;
        //    if (null != hangerReworkMessage)
        //    {
        //        var XID = HexHelper.HexToTen(hangerReworkMessage.XID);
        //        var ID = HexHelper.HexToTen(hangerReworkMessage.ID);
        //        var hangerNo = HexHelper.HexToTen(hangerReworkMessage.HangerNo);
        //        var message = string.Format("【衣架返工】 收到返工请求 主轨:{0} 站号:{1} 衣架号:{2}", XID, ID, hangerNo);
        //        tcpLogInfo.Info(message);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(AddMessage), message, null);
        //        string errMsg = null;
        //        int tag = 0;
        //        try
        //        {

        //            bool success = false; //SusReworkQueryService.Instance.CheckIsCanRework(XID, ID.ToString(), hangerNo.ToString(), ref tag, ref errMsg);//CANProductsService.Instance.RecordReworkHangerInfo(XID, hangerNo, ID, ref errMsg);
        //            if (!success)
        //            {
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), errMsg, null);
        //                tcpLogError.Info(errMsg);
        //                return;
        //            }
        //            var isSucessRegister = SusReworkService.Instance.RegisterReworkHanger(hangerNo.ToString(), XID, ID.ToString(), ref errMsg);
        //            if (isSucessRegister)
        //            {
        //                var sucessMesg = string.Format("返工衣架绑定成功! 衣架号:{0} 站点:{1} 主轨:{2}", hangerNo, ID, XID);
        //                tcpLogInfo.Info(sucessMesg);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMesg, null);
        //                return;
        //            }
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), errMsg, null);
        //        }
        //        catch (HangingPieceReworkException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架返工】", ex);
        //            //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

        //            client.SendReworkException(XID, ID, hangerNo, tag);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //            //Array.Reverse(unHangerReworkExBytes);

        //            ////回显异常数据到终端
        //            //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(unHangerReworkExBytes), XID, ID);

        //        }
        //        catch (StatingNoLoginEmployeeException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架返工】", ex);
        //            //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

        //            client.SendReworkException(XID, ID, hangerNo, tag);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //        }
        //        catch (HangerNoProductException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架返工】", ex);
        //            //var unHangerReworkExBytes = UnicodeUtils.GetBytesByUnicode(cusExData); //UnicodeUtils.GetHexFromChs(cusExData);

        //            client.SendReworkException(XID, ID, hangerNo, tag);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【衣架返工】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }

        //}
        ////工序及疵点
        //private void Client_ReworkFlowDefectRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var reqMess = e.Tag as Dictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>>;
        //    if (null != reqMess)
        //    {
        //        int tag = 0;
        //        string nextStatingNo = null;
        //        int outMainTrackNumber = 0;
        //        int currentStatingNo = 0;
        //        int hangerNo = 0;
        //        try
        //        {

        //            bool isRequireBridge = false;
        //            string info = null;
        //           // CANProductsService.Instance.FlowReworkAction(reqMess, ref currentStatingNo, ref nextStatingNo, ref outMainTrackNumber, ref hangerNo, ref tag,ref isRequireBridge,ref info);
        //            if (!string.IsNullOrEmpty(nextStatingNo))
        //            {
        //                var hexOutID = HexHelper.TenToHexString2Len(outMainTrackNumber);
        //                CANTcp.client.AllocationHangerToNextStating(hexOutID, HexHelper.TenToHexString2Len(nextStatingNo), HexHelper.TenToHexString10Len(hangerNo), XOR);
        //                var susAllocatingMessage = string.Format("【消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", hexOutID, nextStatingNo);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
        //                tcpLogInfo.Info(susAllocatingMessage);
        //            }
        //        }
        //        catch (FlowNotFoundException ex)
        //        {
        //            client.SendReworkException(outMainTrackNumber, currentStatingNo, hangerNo, tag);
        //            var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);

        //        }
        //        catch (ReworkHangerNotFoundException ex)
        //        {
        //            client.SendReworkException(outMainTrackNumber, currentStatingNo, hangerNo, tag);
        //            var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //        catch (ReworkDefectNotFoundException ex)
        //        {
        //            client.SendReworkException(outMainTrackNumber, currentStatingNo, hangerNo, tag);
        //            var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //        catch (FullStatingExcpetion ex)
        //        {
        //            client.SendExcpetionOrPromptInfo(outMainTrackNumber, currentStatingNo, SuspeConstants.tag_FullSiteOrStopWork);

        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架返工工序及疵点】", ex);
        //            //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //        }
        //        catch (ReworkFlowNoNotFoundException ex)
        //        {
        //            client.SendReworkException(outMainTrackNumber, currentStatingNo, hangerNo, SuspeConstants.tag_ExcpetionOrPromptInfo_ReworkFlowNoFound);
        //            var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【衣架返工工序及疵点】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }
        //}
        //#endregion

        //#region CAN 连接相关
        //private void Client_ServerExceptionOccurred(object sender, Sus.Net.Client.Sockets.TcpServerExceptionOccurredEventArgs e)
        //{
        //    var susRemotingMessage = string.Format("【消息】CAN连接异常【{0}】!", e.Exception?.Message);
        //    tcpLogError.Error("Client_ServerExceptionOccurred", e?.Exception);
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
        //}

        //private void Client_ServerDisconnected(object sender, Sus.Net.Client.Sockets.TcpServerDisconnectedEventArgs e)
        //{
        //    var susRemotingMessage = string.Format("【消息】{0}", "CAN连接已关闭!");
        //    tcpLogInfo.Info(susRemotingMessage);
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
        //}
        //static int tag = 0;
        //private void Client_ServerConnected(object sender, Sus.Net.Client.Sockets.TcpServerConnectedEventArgs e)
        //{
        //    var susRemotingMessage = string.Format("【消息】{0} 【端口为{1}】", "已连接到CAN!", CANTcp.CANPort);
        //    tcpLogInfo.Info(susRemotingMessage);
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);
        //    if (tag == 0)
        //    {
        //        tag = 1;
        //        var pMessage = string.Format("上电启动站点初始化信息发送开始...");
        //        tcpLogInfo.Info(pMessage);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(AddMessage), pMessage, null);
        //        client.UpperComputerPowerSupplyInit(1);
        //        pMessage = string.Format("上电启动站点初始化信息发送完成...");
        //        tcpLogInfo.Info(pMessage);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(AddMessage), pMessage, null);

        //    }
        //}

        //void AddMessage(object sender, EventArgs e)
        //{
        //    var index = lbMessage.Items.Count + 1;
        //    var data = string.Format("{0}--->{1}", index, sender.ToString());
        //    lbMessage.Items.Add(data);
        //}
        //private void client_PlaintextReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var susRemotingMessage = string.Format("【消息】{0}", e.message);
        //    //if (!isServiceStart)
        //    //    lbMessage.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        //    tcpLogInfo.Info(susRemotingMessage);
        //}
        //#endregion

        //#region 站内衣架工序比较
        ////站内衣架工序比较
        //private void Client_HangerDropCardRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    //站内衣架工序比较
        //    var messageRequest = e.Tag as SusNet.Common.SusBusMessage.HangerDropCardRequestMessage;
        //    var XID = HexHelper.HexToTen(messageRequest.XID);
        //    var ID = HexHelper.HexToTen(messageRequest.ID);
        //    var hangerNo = HexHelper.HexToTen(messageRequest.HangerNo);
        //    var firstData = CANProductsService.Instance.IsHangPieceStating(XID, ID) ? 1 : 0;// HexHelper.HexToTen(messageRequest.DATA1);//0:普通站比较:1:挂片站比较
        //    var isPowerSupply = HexHelper.HexToTen(messageRequest.DATA1).Equals(1);//为1时是断电重启状态
        //    try
        //    {
        //        //普通站
        //        if (firstData == 0)
        //        {
        //            var message = string.Format("【站内衣架工序比较】 主轨:{0} 站号:{1} 衣架号:{2}", XID, ID, hangerNo);
        //            tcpLogInfo.Info(message);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), message, null);

        //            int tag = 0;
        //            string info = null;
        //            string errMsg = null;
        //            bool isReworkFlow = false;
        //            string flowInfo = string.Empty;
        //            bool isEq = CANProductsService.Instance.CompareHangerFlow(XID.ToString(), hangerNo.ToString(), ID.ToString(), ref tag, ref info, ref errMsg, ref isReworkFlow,ref flowInfo, isPowerSupply);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(AddMessage), string.Format("【本次工序】:--->【{0}】" , flowInfo), null);
        //            if (!isEq)
        //            {
        //                string mainTrackNo = XID.ToString();
        //                //var hexInfo = UnicodeUtils.GetHexFromChs(info);
        //                //var hexBytes = HexHelper.StringToHexByte(hexInfo);//HexHelper.strToToHexByte(hexData);

        //                var btsFlowCompare = UnicodeUtils.CharacterToCoding(info);
        //                tcpLogInfo.Info(string.Format("【站内衣架工序比较】 将要发送的内容--->【{0}】", info));
        //                Console.WriteLine(string.Format("【站内衣架工序比较】 将要发送的内容 pc--->硬件【{0}】", info));

        //                client.HangerDropCardProcessFlowCompare(messageRequest.XID, messageRequest.ID, hangerNo.ToString(), tag, XOR);
        //                var btsFlowCompareBytes = HexHelper.StringToHexByte(btsFlowCompare);
        //                //Array.Reverse(btsFlowCompare);

        //                //client.SendDataByHangerDropCardCompare(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, "FF");
        //                if (!isReworkFlow)
        //                {
        //                    client.SendDataByHangerDropCardCompareExt(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, XOR);
        //                }
        //                else
        //                {
        //                    //返工信息发送
        //                    client.SendDataByHangerDropCardCompareExt2(new System.Collections.Generic.List<byte>(btsFlowCompareBytes), messageRequest.XID, messageRequest.ID, XOR);
        //                }
        //                var strSucess = string.Format("【站内衣架工序比较】 制品信息【{0}】发送完成!", info);
        //                tcpLogInfo.Info(strSucess);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(AddMessage), strSucess, null);
        //                return;
        //            }
        //            //相同工序
        //            client.HangerDropCardProcessFlowCompare(messageRequest.XID, messageRequest.ID, hangerNo.ToString(), 0);
        //            return;
        //        }
        //        //挂片站
        //        if (firstData == 1)
        //        {
        //            //var data = CANProductsQueryService.Instance.GetProductsExt(XID, ID);
        //            //var pInfo = UnicodeUtils.CharacterToCoding(data);
        //            //var hangPieceProductsBytes = HexHelper.StringToHexByte(pInfo);
        //            //client.SendDataByHangerDropCardCompareExt(new System.Collections.Generic.List<byte>(hangPieceProductsBytes), messageRequest.XID, messageRequest.ID, XOR);
        //            //tcpLogInfo.Info(string.Format("【挂片站衣架产量推送完成】 主轨:{0} 站点:{1} 推送内容(unicode编码(小段模式))--->{2}", messageRequest.XID, messageRequest.ID, pInfo));
        //            //var message = string.Format("【挂片站衣架产量推送完成】 主轨:{0} 站号:{1} 衣架号:{2} 发送内容-->【{3}】", XID, ID, hangerNo, data);
        //            //tcpLogInfo.Info(message);
        //            //lbMessage.Invoke(new EventHandler(this.AddMessage), message, null);
        //            var productNumber = CANProductsQueryService.Instance.GetCurrentOnlineProductNumber(XID, ID);//获取当前上线的排产号
        //            string sData = null;
        //            var prodOutData = CANProductsQueryService.Instance.GetProductByBytes(XID, ID, productNumber, ref sData);
        //            CANTcp.client.SendOutputDataToHangingPiece(prodOutData, messageRequest.XID, messageRequest.ID);
        //            CANProductsService.Instance.CacheHangingHangerRequest(XID, ID, hangerNo);
        //            CANProductsQueryService.Instance.HangingPieceHandler(XID, ID, hangerNo);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sData, null);

        //        }

        //    }
        //    catch (NoFoundOnlineProductsException ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundOnlineProducts);
        //        var sysEx = string.Format("【衣架工序比较】 异常:{0}", ex.Message);
        //        tcpLogError.Error(sysEx, ex);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        var sysEx = string.Format("【衣架工序比较】 异常:{0}", ex.Message);
        //        tcpLogError.Error(sysEx, ex);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //    }
        //}

        //#endregion

        //#region 出站相关
        ////【硬件对衣架分配成功的回应】
        ////软件让衣架出站【衣架给硬件分配成功后，才让衣架出站】
        //private void Client_AllocationHangerResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{

        //    var message = e.Tag as SusNet.Common.SusBusMessage.AllocationHangerResponseMessage;
        //    if (null != message)
        //    {
        //        var susAllocatingMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 衣架号【{2}】已保存!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), SusNet.Common.Utils.HexHelper.HexToTen(message.HangerNo));
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
        //        tcpLogInfo.Info(susAllocatingMessage);

        //        var XID = HexHelper.HexToTen(message.XID);
        //        var ID = HexHelper.HexToTen(message.ID);
        //        var hangerNo = HexHelper.HexToTen(message.HangerNo);
        //        ////var productNumber = SusNet.Common.Utils.HexHelper.HexToTen(message.ProductionNumber);
        //        try
        //        {
        //            int flowType = 0;
        //            string outSiteStatingNo = null;
        //            var isReworkSourceStating = false;
        //            var isMonitoringAllocation = false;
        //            bool isStatingStorage = false;
        //            int outMainTracknumber = 0;
        //            var isBridgeStatingOutStatingAllocate = false;
        //            var isF2Assign = false;
        //           // CANProductsService.Instance.HangerOutStatingResponse(XID.ToString(), ID.ToString(), hangerNo.ToString(),ref outMainTracknumber, ref outSiteStatingNo, ref flowType, ref isReworkSourceStating, ref isMonitoringAllocation,ref isStatingStorage,ref isBridgeStatingOutStatingAllocate,ref isF2Assign);
        //            var hexOutMainTracknumber = HexHelper.TenToHexString2Len(outMainTracknumber);
        //            if (!string.IsNullOrEmpty(outSiteStatingNo))
        //            {
        //                //监测站分配不推送产量
        //                if (isMonitoringAllocation)
        //                {
        //                    var monitoringAllocationMessage = string.Format("【监测点分配衣架】 监测点分配衣架成功! 主轨:{0} 下一站:{1} 衣架:{2} ", hexOutMainTracknumber, outSiteStatingNo, hangerNo);
        //                    if (!isServiceStart)
        //                        lbMessage.Invoke(new EventHandler(this.AddMessage), monitoringAllocationMessage, null);
        //                    return;
        //                }

        //                #region 衣架出站 及产量推送
        //                tcpLogInfo.Info(string.Format("【衣架出站pc响应】 衣架出站指令发送开始! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", hexOutMainTracknumber, ID, outSiteStatingNo, hangerNo));

        //                //【返工衣架】
        //                if (flowType == 1)
        //                {
        //                    #region 【返工相关】
        //                    //返工衣架出站
        //                    var messageOut = new ReworkResponseMessage(message.XID, outSiteStatingNo, 0, HexHelper.TenToHexString10Len(hangerNo), XOR);
        //                    client.SendData(messageOut.GetBytes());

        //                    var outSiteDataMessage = string.Format("【返工衣架】  主轨:{0} 本站:{1} 衣架:{2}", XID, outSiteStatingNo, hangerNo);
        //                    if (!isServiceStart)
        //                        lbMessage.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);

        //                    tcpLogInfo.Info(outSiteDataMessage);

        //                    //【返工发起站点不推送产量】是否是返工工序发起站点出站
        //                    if (!isReworkSourceStating)
        //                    {
        //                        var info = string.Empty;
        //                        var decOutSiteNo = HexHelper.HexToTen(outSiteStatingNo);
        //                        var fData = CANProductsQueryService.Instance.GetReworkHangerOutSiteFlowInfo(XID, decOutSiteNo, hangerNo, ref info);

        //                        var rOutSiteDataMessage = string.Format("【返工衣架-->衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                        if (!isServiceStart)
        //                            lbMessage.Invoke(new EventHandler(this.AddMessage), rOutSiteDataMessage, null);
        //                        tcpLogInfo.Info(rOutSiteDataMessage);

        //                        client.SendDataByReworkSiteOutSite(fData, message.XID, HexHelper.TenToHexString2Len(outSiteStatingNo));
        //                        rOutSiteDataMessage = string.Format("【返工衣架-->衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                        if (!isServiceStart)
        //                            lbMessage.Invoke(new EventHandler(this.AddMessage), rOutSiteDataMessage, null);
        //                        tcpLogInfo.Info(rOutSiteDataMessage);
        //                    }
        //                    #endregion
        //                }
        //                else
        //                {
        //                    //正常 衣架出站
        //                    client.AutoHangerOutStating(message.XID, HexHelper.TenToHexString2Len(outSiteStatingNo), true, hangerNo, XOR);

        //                    if (!isMonitoringAllocation)
        //                    {
        //                        tcpLogInfo.Info(string.Format("【衣架出站pc响应】 衣架出站指令发送成功! 主轨:{0}  本站:{1} 下一站:{2} 衣架:{3}", XID, outSiteStatingNo, ID, hangerNo));

        //                        var sucessOutSiteMessage = string.Format("【衣架出站pc响应】 衣架出站指令发送成功! 主轨:{0} 本站:{1} 下一站:{2} 衣架:{3}", XID, outSiteStatingNo, ID, hangerNo);

        //                        if (!isServiceStart)
        //                            lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
        //                    }

        //                    //判断是否是挂片站(非挂片站推送)
        //                    //计算 2018年3月27日 21:54:31 lucifer
        //                    /*
        //                     今日数（2字节）+
        //                     效率（2字节）
        //                     本次工序时间（2字节）
        //                     */
        //                    var isHangingPieceStating = CANProductsService.Instance.IsHangPieceStating(XID, int.Parse(outSiteStatingNo));// new ProductsQueryServiceImpl().isHangingPiece(null, outSiteStatingNo, XID.ToString());

        //                    //var isStatingStorage = CANProductsService.Instance.IsStatingStorage(XID, int.Parse(outSiteStatingNo));
        //                    //非挂片站和挂片站都要发送产量//zxl/2018年6月19日 23:03:54
        //                    if (!isHangingPieceStating && !isStatingStorage)
        //                    {
        //                        #region 【普通站出站产量推送】
        //                        var info = string.Empty;
        //                        var decOutSiteNo = HexHelper.HexToTen(outSiteStatingNo);
        //                        List<Byte> outData = null;

        //                        var isSendOutData = false;//CANProductsQueryService.Instance.GetOutSiteHangerFlowInfo(XID, decOutSiteNo, hangerNo, ref outData, ref info);
        //                        if (isSendOutData)
        //                        {
        //                            var outSiteDataMessage = string.Format("【衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                            if (!isServiceStart)
        //                                lbMessage.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
        //                            tcpLogInfo.Info(outSiteDataMessage);

        //                            client.SendDataByCommonSiteOutSite(outData, message.XID, HexHelper.TenToHexString2Len(outSiteStatingNo));
        //                            outSiteDataMessage = string.Format("【衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                            if (!isServiceStart)
        //                                lbMessage.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
        //                            tcpLogInfo.Info(outSiteDataMessage);
        //                        }
        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region 【挂片出站产量推送】
        //                        var info = string.Empty;
        //                        var decOutSiteNo = HexHelper.HexToTen(outSiteStatingNo);
        //                        List<Byte> outData = null;
        //                        var isSendOutData = CANProductsQueryService.Instance.GetOutSiteHagingPieceFlowInfo(XID, decOutSiteNo, hangerNo, ref outData, ref info);
        //                        if (isSendOutData)
        //                        {
        //                            var outSiteDataMessage = string.Format("【衣架出站产量正在推送】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                            if (!isServiceStart)
        //                                lbMessage.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
        //                            tcpLogInfo.Info(outSiteDataMessage);

        //                            client.SendDataByCommonSiteOutSite(outData, message.XID, HexHelper.TenToHexString2Len(outSiteStatingNo));
        //                            outSiteDataMessage = string.Format("【衣架出站产量推送完成】 推送信息! 主轨:{0} 本站:{1} 衣架:{2} 产量数据:{3} ", XID, decOutSiteNo, hangerNo, info);
        //                            if (!isServiceStart)
        //                                lbMessage.Invoke(new EventHandler(this.AddMessage), outSiteDataMessage, null);
        //                            tcpLogInfo.Info(outSiteDataMessage);
        //                        }
        //                        #endregion
        //                    }
        //                }

        //                #endregion
        //            }


        //            if (!string.IsNullOrEmpty(outSiteStatingNo))
        //            {
        //                var clearHangerNoCache = string.Format("正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", XID, outSiteStatingNo, hangerNo);
        //                CANTcp.client.ClearHangerCache(XID, HexHelper.HexToTen(outSiteStatingNo), hangerNo, XOR);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);
        //            }

        //        }

        //        catch (ApplicationException ex)
        //        {
        //            var cusExData = ex.Message;
        //            tcpLogError.Error("【衣架出站响应】", ex);
        //            var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //            //回显异常数据到终端
        //            CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);

        //        }
        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【衣架出站响应】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }

        //}
        ////【硬件请求衣架出站】
        //private void Client_HangerOutStatingRequestMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    //衣架从出站

        //    var req = e.Tag as SusNet.Common.SusBusMessage.HangerOutStatingRequestMessage;
        //    var XID = HexHelper.HexToTen(req.XID);
        //    var ID = HexHelper.HexToTen(req.ID);
        //    var hangerNo = HexHelper.HexToTen(req.HangerNo);
        //    var outType = CANProductsService.Instance.IsHangPieceStating(XID, ID) ? 1 : 0;//HexHelper.HexToTen(req.outType);

        //    string nextStatingNo = null;

        //    var isFlowSucess = false;
        //    var infoEx = string.Empty;
        //    try
        //    {
        //        #region 系统参数相关
        //        if (outType == 1)
        //        {
        //            //出站判断  “挂片站出衣架达到计划数后停止出衣”
        //            bool result = SystemParameterService.Instance.StartingStopOutWhenOverPlan(XID, ID);
        //            if (result == false)
        //            {
        //                throw new StartingStopOutWhenOverPlanException("挂片站出衣架达到计划数后停止出衣");
        //            }
        //        }
        //        //挂片站出衣架数， 计划数
        //        #endregion

        //        int outMainTrackNumber = 0;
        //        var aMainTracknumberBridgeStatingIsInFlowChart = false;
        //        var sucess = CANProductsService.Instance.HangerOutStatingRequest(XID.ToString(), ID.ToString(), outType, hangerNo.ToString(), ref nextStatingNo, ref outMainTrackNumber, ref isFlowSucess, ref infoEx,ref aMainTracknumberBridgeStatingIsInFlowChart);
        //        var hexOutMainTrackNumber = HexHelper.TenToHexString2Len(outMainTrackNumber);
        //        if (outMainTrackNumber==0) {
        //            var nextStatingMainTracknumberNoFoundEx = new ApplicationException("下一站主轨未找到!");
        //            tcpLogError.Error(nextStatingMainTracknumberNoFoundEx);
        //            throw nextStatingMainTracknumberNoFoundEx;
        //        }
        //        ///没有绑定制单的的衣架/重复出站/衣架已经在该站点生产完成
        //        if (isFlowSucess)
        //        {
        //            //出站
        //            client.AutoHangerOutStating(req.XID, req.ID, true, hangerNo, XOR);

        //            var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", infoEx, XID, ID, hangerNo);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
        //            tcpLogInfo.Info(sucessOutSiteMessage);

        //            return;
        //        }
        //        if (sucess)
        //        {
        //            #region 衣架生产完成
        //            if (string.IsNullOrEmpty(nextStatingNo))
        //            {
        //                var sucessMessage = string.Format("【消息】 主轨【{0}】 站点{1} 衣架【{2}】 生产完成!", XID, ID, hangerNo);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessMessage, null);
        //                //var clearHangerNoCache = string.Format("正在清除衣架【{0}】的所有站点缓存...",hangerNo);
        //                //client.ClearHangerCache(XID, 0, hangerNo);
        //                //lbMessage.Invoke(new EventHandler(this.AddMessage), clearHangerNoCache, null);

        //                //tcpLogInfo.Info(string.Format("【衣架生产完成】 衣架出站指令发送开始! 主轨:{0} 响应站:{1} 出站站点:{2} 衣架:{3}", XID, ID, ID, hangerNo));

        //                int outSiteStatingNo = 0;
        //                //【衣架回流】--->【给挂片站分配衣架】
        //                //var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //                CANProductsValidService.Instance.GetSucessHangerNoHangingPieceStating(hangerNo.ToString(), XID, ID, ref outMainTrackNumber, ref outSiteStatingNo);
        //                var sucessHangerHangingPieceMainTrackNumber = HexHelper.TenToHexString2Len(outMainTrackNumber);
        //                var hexOutSiteStatingNo = HexHelper.TenToHexString2Len(outSiteStatingNo);

        //                //给下一站分配衣架
        //                CANTcp.client.AllocationHangerToNextStating(sucessHangerHangingPieceMainTrackNumber, hexOutSiteStatingNo, req.HangerNo, XOR);

        //                var sucessHangerHangingMessage = string.Format("【衣架回流】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", sucessHangerHangingPieceMainTrackNumber, hexOutSiteStatingNo);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessHangerHangingMessage, null);
        //                tcpLogInfo.Info(sucessHangerHangingMessage);

        //                return;
        //            }
        //            #endregion

        //            var hexOutID = HexHelper.TenToHexString2Len(outMainTrackNumber);

        //            //给下一站分配衣架
        //            CANTcp.client.AllocationHangerToNextStating(hexOutID, HexHelper.TenToHexString2Len(nextStatingNo), req.HangerNo, XOR);

        //            var susAllocatingMessage = string.Format("【消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", hexOutID, nextStatingNo);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
        //            tcpLogInfo.Info(susAllocatingMessage);
        //        }
        //    }
        //    catch (StartingStopOutWhenOverPlanException ex)
        //    {
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //        CANTcp.client.SendExcpetionOrPromptInfo(XID, ID, (int)_0130ExcpetionTag.StartingStopOutWhenOverPlan);
        //    }
        //    catch (StatingNoLoginEmployeeException ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcpetionOrPromptInfo_EmployeeNoLoginStating);
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //    }
        //    catch (NoFoundOnlineProductsException ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundOnlineProducts);
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //    }
        //    catch (FullStatingExcpetion ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_FullSiteOrStopWork);
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //    }
        //    catch (NoFoundStatingException ex)
        //    {
        //        var promptData = UnicodeUtils.CharacterToCoding(ex?.FlowNo?.Trim());
        //        var promtDataByte = DataFillingUtils.Get5ByteDF(new List<Byte>(HexHelper.StringToHexByte(promptData)), 0);
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcpetionOrPromptInfo_NotFoundNextStating, promtDataByte);
        //        var cusExData = string.Format(ex.Message + " 工序号:{0}", ex?.FlowNo?.Trim());
        //        tcpLogError.Error("【衣架出站】", ex);
        //        //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //    }
        //    catch (NonAllocationOutStatingException ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcptionNonAllocationOutStating);
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //    }
        //    catch (NonExistStatingException ex)
        //    {
        //        client.SendExcpetionOrPromptInfo(XID, ID, SuspeConstants.tag_ExcptionNonExistStating);
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        //var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        var cusExData = ex.Message;
        //        tcpLogError.Error("【衣架出站】", ex);
        //        var hexcusExData = UnicodeUtils.GetHexFromChs(cusExData);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), cusExData, null);

        //        ////回显异常数据到终端
        //        //CANTcp.client.SendShowData(new System.Collections.Generic.List<byte>(HexHelper.StringToHexByte(hexcusExData)), XID, ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        var sysEx = string.Format("【衣架出站】 异常:{0}", ex.Message);
        //        tcpLogError.Error(sysEx, ex);
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //    }
        //    finally
        //    {

        //    }

        //}

        //#endregion

        //#region 衣架进站
        ////衣架进站
        //private void Client_HangerArrivalStatingMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    //衣架进站
        //    var message = e.Tag as SusNet.Common.SusBusMessage.HangerArrivalStatingRequestMessage;
        //    if (null != message)
        //    {

        //        try
        //        {
        //            var XID = HexHelper.HexToTen(message.XID);
        //            var hangerNo = HexHelper.HexToTen(message.HangerNo);
        //            var ID = HexHelper.HexToTen(message.ID);
        //            var isHangerRepeatInStating = false;
        //            CANProductsService.Instance.RecordHangerArriveStating(XID, hangerNo.ToString(), ID.ToString(), ref isHangerRepeatInStating);
        //            if (isHangerRepeatInStating)
        //            {
        //                var susRepeatInStatingMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 衣架号【{2}】重复进站!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), hangerNo.ToString());
        //                tcpLogInfo.Info(susRepeatInStatingMessage);
        //                if (!isServiceStart)
        //                    lbMessage.Invoke(new EventHandler(this.AddMessage), susRepeatInStatingMessage, null);
        //                return;
        //            }
        //            var susAllocatingMessage = string.Format("【消息】 主轨【{0}】 站点【{1}】 衣架号【{2}】已进站!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID), SusNet.Common.Utils.HexHelper.HexToTen(message.ID), hangerNo.ToString());
        //            tcpLogInfo.Info(susAllocatingMessage);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), susAllocatingMessage, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            var sysEx = string.Format("【衣架进站】 异常:{0}", ex.Message);
        //            tcpLogError.Error(sysEx, ex);
        //            if (!isServiceStart)
        //                lbMessage.Invoke(new EventHandler(this.AddMessage), sysEx, null);
        //        }
        //    }
        //}

        //#endregion

        //#region 挂片站上线
        ////挂片站上线【把上线的排产号设置为当前上线的产品】
        //private void Client_HangingPieceStatingOnlineMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    var message = e.Tag as HangingPieceStatingOnlineRequestMessage;
        //    var XID = HexHelper.HexToTen(message.XID);
        //    var sucess = CANProductsService.Instance.BindHangpieceStatingOnlineProductNumber(XID, message.ProductNumber);
        //    if (sucess)
        //    {
        //        var messageRes = string.Format("{0} {1} {2} {3} {4} 00 00 00 00 00 {5}", message.XID, message.ID, SuspeConstants.cmd_HangPieceOnline_Res, XOR, SuspeConstants.address_HangpieceOnline, HexHelper.TenToHexString2Len(message.ProductNumber));
        //        tcpLogInfo.Info(string.Format("【挂片站上线产品回应发送开始】 发送内容-->{0}", messageRes));
        //        client.SendData(HexHelper.StringToHexByte(messageRes));
        //        if (!isServiceStart)
        //            tcpLogInfo.Info(string.Format("【挂片站上线产品回应发送完成】 发送内容-->{0}", messageRes));
        //    }
        //    //回应
        //}
        //#endregion

        //#region 制品界面直接上线
        ////制品界面直接上线
        //private void Client_ClientMachineResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    Console.WriteLine(string.Format("【{0}】 收到消息-->【{1}】", "Client_ClientMachineResponseMessageReceived", e.message));

        //    var message = e.Tag as SusNet.Common.SusBusMessage.ClientMachineResponseMessage;

        //    var XID = HexHelper.HexToTen(message.XID);
        //    var ID = HexHelper.HexToTen(message.ID);
        //    var productNumber = message.ProductNumber;//SusNet.Common.Utils.HexHelper.HexToTen(message.ProductNumber);

        //    var susRemotingMessage = string.Format("【制品界面直接上线】收到主轨【{0}】 挂片站【{1}】 响应!", XID.ToString(), ID.ToString());
        //    tcpLogInfo.Info(susRemotingMessage);
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(AddMessage), susRemotingMessage, null);

        //    var sendMessageT = string.Format("【制品界面直接上线】 主轨【{0}】挂片站【{1}】 等待放衣架.....", XID.ToString(), ID.ToString());
        //    if (!isServiceStart)
        //        lbMessage.Invoke(new EventHandler(AddMessage), sendMessageT, null);

        //    SusCacheBootstarp.Instance.Init();

        //    //制品界面上线后，上线制品信息给挂片站推送

        //    /*
        //    try
        //    {
        //        var data = CANProductsQueryService.Instance.GetProducts(XID, ID, productNumber);//string.Format("933304-9BUY,010,28,任务1863件,单位1件,累计出1117件,今日出213件");
        //        //   var hexData = HexHelper.ToHex(data, "utf-8", false);
        //        var hexChs = UnicodeUtils.GetHexFromChs(data);
        //        var hexBytes = HexHelper.StringToHexByte(hexChs);//HexHelper.strToToHexByte(hexData);

        //        string mainTrackNo = HexHelper.TenToHexString(XID); //SusNet.Common.Utils.HexHelper.tenToHexString("1");//主轨暂时写死的
        //        string statingNo = HexHelper.TenToHexString(ID);//SusNet.Common.Utils.HexHelper.tenToHexString(message.StatingNo);
        //        tcpLogInfo.Info(string.Format("【制品界面直接上线】 将要发送的内容--->【{0}】", data));
        //        Console.WriteLine(string.Format("【制品界面直接上线】 将要发送的内容 pc--->硬件【{0}】", data));
        //        client.SendDataByProductsDirectOnline(new System.Collections.Generic.List<byte>(hexBytes), mainTrackNo, statingNo, "FF");

        //        var strSucess = string.Format("【制品界面直接上线】 制品信息【{0}】发送完成!", data);
        //        tcpLogInfo.Info(strSucess);
        //        lbMessage.Invoke(new EventHandler(AddMessage), strSucess, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        tcpLogError.Error("【制品界面直接上线】 异常:", ex);
        //    }
        //    */

        //}

        //#endregion

        //#region 主轨相关
        ////停止主轨
        //private void Client_StopMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    tcpLogInfo.Info(e.message);
        //    var message = e.Tag as SusNet.Common.SusBusMessage.StopMainTrackResponseMessage;
        //    if (null != message)
        //    {
        //        var susRemotingMessage = string.Format("【消息】 主轨【{0}】 已停止!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        //        tcpLogInfo.Info(susRemotingMessage);
        //    }
        //}
        ////启动主轨
        //private void Client_StartMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    tcpLogInfo.Info(e.message);
        //    var message = e.Tag as SusNet.Common.SusBusMessage.StartMainTrackResponseMessage;
        //    if (null != message)
        //    {
        //        var susRemotingMessage = string.Format("【消息】 主轨【{0}】 已启动!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        //        tcpLogInfo.Info(susRemotingMessage);
        //    }
        //}
        ////急停主轨
        //private void Client_EmergencyStopMainTrackResponseMessageReceived(object sender, Sus.Net.Common.Event.MessageEventArgs e)
        //{
        //    tcpLogInfo.Info(e.message);
        //    var message = e.Tag as SusNet.Common.SusBusMessage.EmergencyStopMainTrackResponseMessage;
        //    if (null != message)
        //    {
        //        var susRemotingMessage = string.Format("【消息】 主轨【{0}】 急停成功!", SusNet.Common.Utils.HexHelper.HexToTen(message.XID));
        //        if (!isServiceStart)
        //            lbMessage.Invoke(new EventHandler(this.AddMessage), susRemotingMessage, null);
        //        tcpLogInfo.Info(susRemotingMessage);
        //    }
        //}
        //#endregion

        //public void Disconnect(ListBoxControl _lbMessage, bool isServiceStart)
        //{
        //    if (!isServiceStart)
        //    {
        //        lbMessage = _lbMessage;
        //    }
        //    if (client != null)
        //    {
        //        client.Disconnect();
        //        isConnected = false;
        //        client = null;
        //        var sucMessage = string.Format("客户端关闭成功!端口:【{0}】 .....", CANPort);
        //        if (!isServiceStart)
        //        {
        //            _lbMessage.Invoke(new EventHandler(this.AddMessage), sucMessage, null);

        //        }
        //        tcpLogInfo.Info(sucMessage);
        //    }
        //    else
        //    {
        //        var sucMessage = string.Format("沒有有效的连接,端口:【{0}】 .....", CANPort);
        //        if (!isServiceStart)
        //        {
        //            _lbMessage.Invoke(new EventHandler(this.AddMessage), sucMessage, null);

        //        }
        //        tcpLogInfo.Info(sucMessage);
        //    }
        //}
    }
}

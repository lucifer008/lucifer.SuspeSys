using Sus.Net.Common.Constant;
using Sus.Net.Common.Entity;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core
{
    /// <summary>
    /// 下位机指令相关
    /// </summary>
    public class LowerPlaceInstr : SusLog
    {
        private LowerPlaceInstr() { }
        public readonly static LowerPlaceInstr Instance = new LowerPlaceInstr();
        private readonly static Object lockObject = new object();
        /// <summary>
        /// 下一站分配-->给下位机发送分配指令
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="tenNextStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        public void AllocationHangerToNextStating(string tenHangerNo, string tenNextStatingNo, int outMainTrackNumber)
        {
            lock (lockObject)
            {
                //给下一站分配衣架
                CANTcpServer.server.AllocationHangerToNextStating(outMainTrackNumber.ToString(), tenNextStatingNo, HexHelper.TenToHexString10Len(tenHangerNo));
                var susAllocatingMessage = string.Format("【衣架分配消息】 衣架{0}往主轨【{1}】 站点【{2}】 分配指令已发送成功!", tenHangerNo, outMainTrackNumber, tenNextStatingNo);
                tcpLogInfo.Info(susAllocatingMessage);
            }
        }
        /// <summary>
        /// F2指定异常通知
        /// </summary>
        /// <param name="sourceMainTrackNuber"></param>
        /// <param name="sourceStatingNo"></param>
        /// <param name="tag"></param>
        internal void F2AssginExceptionNotice(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int tag, TcpClient tcpClient = null)
        {
            lock (lockObject)
            {
                //F2指定异常通知
                CANTcpServer.server.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                var susAllocatingMessage = string.Format($"【F2指定异常通知】 衣架{0} 主轨【{1}】 站点【{2}】 F2指定异常通知发送成功! tag---->{tag}", hangerNo, sourceMainTrackNuber, sourceStatingNo);
                tcpLogInfo.Info(susAllocatingMessage);
            }
        }
        /// <summary>
        /// F2指定出战指令
        /// </summary>
        /// <param name="mainTrackNuber"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag"></param>
        internal void F2AssginOutSite(int hangerNo, int mainTrackNuber, int statingNo, int tag, TcpClient tcpClient = null)
        {
            lock (lockObject)
            {
                //F2指定出战指令
                CANTcpServer.server.F2AssginExceptionNotice(hangerNo, mainTrackNuber, statingNo, tag, tcpClient);
                var susAllocatingMessage = string.Format("【F2指定出战指令】 衣架{0} 主轨【{1}】 站点【{2}】 F2指定出战指令发送成功!", hangerNo, mainTrackNuber, statingNo);
                tcpLogInfo.Info(susAllocatingMessage);
            }
        }
        /// <summary>
        /// 清除下位机分配缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="mainTrackNuber"></param>
        /// <param name="statingNo"></param>
        public void ClearHangerCache(int hangerNo, int mainTrackNuber, int statingNo)
        {
            lock (lockObject)
            {
                CANTcpServer.server.ClearHangerCache(mainTrackNuber, statingNo, hangerNo);
            }
        }
        ///// <summary>
        ///// 异常提示(pc--->硬件)
        ///// </summary>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="hangerNo"></param>
        ///// <param name="tag"></param>
        ///// <param name="promptData">数据位</param>
        ///// <param name="xor"></param>
        //public void SendExcpetionOrPromptInfo(int mainTrackNo, int statingNo, int tag, List<byte> promptData, string xor = null)
        //{
        //    if (string.IsNullOrEmpty(xor))
        //    {
        //        xor = SuspeConstants.XOR;
        //    }
        //    var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
        //        HexHelper.TenToHexString2Len(mainTrackNo),
        //        HexHelper.TenToHexString2Len(statingNo),
        //        SuspeConstants.cmd_ExcpetionOrPromptInfo,
        //        xor,
        //        SuspeConstants.address_ExcpetionOrPromptInfo,
        //        HexHelper.TenToHexString2Len(tag),
        //        HexHelper.BytesToHexString(promptData.ToArray()));

        //    var sendDataList = new List<byte>();
        //    var data = HexHelper.StringToHexByte(message);
        //    tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送开始,消息:--->{0}", message));
        //    //server.Send(tcpClient, data);
        //    CANTcpServer.server.SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
        //    tcpLogInfo.Info(string.Format("【异常提示推送 pc---->硬件】发送完成，消息:--->{0}", message));
        //}
       
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
                CANTcpServer.server.SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)));
            else
                CANTcpServer.server.Send(tcpClient, data);
            tcpLogInfo.Info(string.Format("【异常推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }


        /// <summary>
        /// 获取byte
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private List<byte> FaultCodesBytes(string info)
        {
            var infoByte = UnicodeUtils.CharacterToCoding(info);
            var infoByteList = HexHelper.StringToHexByte(infoByte);

            return new System.Collections.Generic.List<byte>(infoByteList);
        }
        public void SendFaultCodeMessageList(int mid, int id, int clothingVehicleTypeCode, IList<FaultCodeTable> faultCodeTableList)
        {
            foreach (var fct in faultCodeTableList)
            {
                SendFaultCodeMessage(mid, id, clothingVehicleTypeCode, fct.SerialNumber?.Trim());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="id"></param>
        public void SendFaultCodeMessage(int mid, int id, int clothingVehicleTypeCode, string serialNumber)
        {
            // var strLackMaterial = "";

            //byte list
            //var bytes = FaultCodesBytes(strLackMaterial);

            ////CANTcpServer.server.SendShowData(bytes,mid, id);
            //if (null == bytes || bytes.Count == 0)
            //{
            //    var ex = new ApplicationException("发送内容不能为空!");
            //    tcpLogError.Error("【发送内容不能为空】", ex);
            //    throw ex;
            //}

            string xor = SuspeConstants.XOR;

            //var j = 0;
            //var times = 0;
            //if (bytes.Count % 6 == 0)
            //{
            //    times = bytes.Count / 6;
            //}
            //else
            //{
            //    times = 1 + bytes.Count / 6;
            //}
            var mapKey = string.Format($"{clothingVehicleTypeCode}-{serialNumber}");
            var dicFaultCodeAndSecondAddressMappingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_Second_Address_Mapping);
            if (!dicFaultCodeAndSecondAddressMappingCache.ContainsKey(mapKey))
            {
                tcpLogError.Error("缓存中无衣车故障代码!");
                return;
            }
            var addressList = dicFaultCodeAndSecondAddressMappingCache[mapKey];
            var decBeginAddress = addressList[0];//SuspeConstants.address_fault_code_send_start;// 0x0160;//0160
            var decEndAddress = addressList[addressList.Count - 1];//0x017F;//017F

            for (var addIndex = decBeginAddress; addIndex <= decEndAddress; addIndex++)
            {
                var sendDataList = new List<byte>();
                var sData = this.GetHeaderBytesExt(HexHelper.TenToHexString2Len(mid), HexHelper.TenToHexString2Len(id), HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                for (var index = 0; index < 6; index++)
                {
                    sendDataList.AddRange(HexHelper.StringToHexByte("00"));
                }
                CANTcpServer.server.SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
                Console.WriteLine(string.Format("【故障报修--故障代码 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【故障报修--故障代码 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
            }
            //for (var i = 0; i < times; i++)
            //{
            //    if (decBeginAddress > decEndAddress)
            //    {
            //        var ex = new ApplicationException(string.Format("发送缺料信息超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
            //        tcpLogError.Error("【故障报修】", ex);
            //    }
            //    var sendDataList = new List<byte>();
            //    var sData = this.GetHeaderBytesExt(mid.ToString(), id.ToString(), HexHelper.TenToHexString4Len(decBeginAddress), xor);
            //    sendDataList.AddRange(sData);
            //    if (j < bytes.Count)
            //    {
            //        for (int b = j; j < bytes.Count; j++)
            //        {
            //            if (sendDataList.Count == 12)
            //            {
            //                break;
            //            }
            //            sendDataList.Add(bytes[j]);
            //        }
            //    }
            //    var teLen = sendDataList.Count;
            //    for (var ii = 0; ii < 12 - teLen; ii++)
            //    {
            //        if (sendDataList.Count == 12)
            //        {
            //            break;
            //        }
            //        sendDataList.AddRange(HexHelper.StringToHexByte("00"));
            //    }
            //    decBeginAddress++;
            //    tcpLogInfo.Info(string.Format("【故障报修--故障类别 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

            //    var bs = sendDataList.ToArray();//sendDataList.ToArray();
            //    //Array.Reverse(bs);
            //    //CANTcpServer.server.SendShowData
            //    //client.Send(bs);
            //    CANTcpServer.server.SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
            //    Console.WriteLine(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
            //    tcpLogInfo.Info(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            //}

            tcpLogInfo.Info(string.Format("【故障报修--故障类别pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mid, id)));
        }

        public void SendClothingVehicleTypeMessageList(int mid, int id, IList<ClothingVehicleType> clothingVehicleTypeList)
        {
            foreach (var cvt in clothingVehicleTypeList)
            {
                SendClothingVehicleTypeMessage(mid, id, cvt.Code?.Trim());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="id"></param>
        public void SendClothingVehicleTypeMessage(int mid, int id, string clothingVehicleTypeCode)
        {
            //var strLackMaterial = "";

            ////byte list
            //var bytes = FaultCodesBytes(strLackMaterial);

            ////CANTcpServer.server.SendShowData(bytes,mid, id);
            //if (null == bytes || bytes.Count == 0)
            //{
            //    var ex = new ApplicationException("发送内容不能为空!");
            //    tcpLogError.Error("【发送内容不能为空】", ex);
            //    throw ex;
            //}

            string xor = SuspeConstants.XOR;

            //var j = 0;
            //var times = 0;
            //if (bytes.Count % 6 == 0)
            //{
            //    times = bytes.Count / 6;
            //}
            //else
            //{
            //    times = 1 + bytes.Count / 6;
            //}
            var dicFaultCodeAndFirstAddressMappingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_First_Address_Mapping);
            if (!dicFaultCodeAndFirstAddressMappingCache.ContainsKey(clothingVehicleTypeCode))
            {
                tcpLogError.Error("缓存中无衣车类别!");
                return;
            }
            var addressList = dicFaultCodeAndFirstAddressMappingCache[clothingVehicleTypeCode];
            var decBeginAddress = addressList[0];//SuspeConstants.address_fault_code_send_start;// 0x0160;//0160
            var decEndAddress = addressList[addressList.Count - 1];//0x017F;//017F

            for (var addIndex = decBeginAddress; addIndex <= decEndAddress; addIndex++)
            {
                var sendDataList = new List<byte>();
                var sData = this.GetHeaderBytesExt(HexHelper.TenToHexString2Len(mid), HexHelper.TenToHexString2Len(id), HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                for (var index = 0; index < 6; index++)
                {
                    sendDataList.AddRange(HexHelper.StringToHexByte("00"));
                }
                CANTcpServer.server.SendMessageByByte(sendDataList.ToArray(), new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
                Console.WriteLine(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
            }
            //for (var i = 0; i < times; i++)
            //{
            //    if (decBeginAddress > decEndAddress)
            //    {
            //        var ex = new ApplicationException(string.Format("发送缺料信息超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
            //        tcpLogError.Error("【故障报修】", ex);
            //    }
            //    var sendDataList = new List<byte>();
            //    var sData = this.GetHeaderBytesExt(mid.ToString(), id.ToString(), HexHelper.TenToHexString4Len(decBeginAddress), xor);
            //    sendDataList.AddRange(sData);
            //    if (j < bytes.Count)
            //    {
            //        for (int b = j; j < bytes.Count; j++)
            //        {
            //            if (sendDataList.Count == 12)
            //            {
            //                break;
            //            }
            //            sendDataList.Add(bytes[j]);
            //        }
            //    }
            //    var teLen = sendDataList.Count;
            //    for (var ii = 0; ii < 12 - teLen; ii++)
            //    {
            //        if (sendDataList.Count == 12)
            //        {
            //            break;
            //        }
            //        sendDataList.AddRange(HexHelper.StringToHexByte("00"));
            //    }
            //    decBeginAddress++;
            //    tcpLogInfo.Info(string.Format("【故障报修--故障类别 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

            //    var bs = sendDataList.ToArray();//sendDataList.ToArray();
            //    //Array.Reverse(bs);
            //    //CANTcpServer.server.SendShowData
            //    //client.Send(bs);
            //    CANTcpServer.server.SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
            //    Console.WriteLine(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
            //    tcpLogInfo.Info(string.Format("【故障报修--故障类别 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            //}

            tcpLogInfo.Info(string.Format("【故障报修--故障类别pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mid, id)));
        }

        private byte[] GetHeaderBytesExt(string hexMainTrackNo, string hexStatingNo, string hexAddress, string xor = null)
        {
            if (string.IsNullOrEmpty(xor))
                xor = "00";

            string hexStr = string.Format("{0} {1} {2} {3} {4}", hexMainTrackNo, hexStatingNo, SuspeConstants.cmd_Lack_Materials_Reponse, xor, hexAddress);
            return HexHelper.StringToHexByte(hexStr);
        }
        /// <summary>
        /// 中止报修
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        /// <param name="tcpClient"></param>
        public void SendStopFaultRepair(string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_Fault_Repair_Stop_Res,
                xor,
                SuspeConstants.address_Fault_Repair_Stop,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString10Len(0));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【故障报修中止报修推送 pc---->硬件】发送开始,消息:--->{0}", message));
            //server.Send(tcpClient, data);
            CANTcpServer.server.SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)), tcpClient);
            tcpLogInfo.Info(string.Format("【故障报修中止报修推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        /// <summary>
        /// 开始报修
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        /// <param name="tcpClient"></param>
        public void SendStartFaultRepair(string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_fault_repair_start,
                xor,
                SuspeConstants.address_fault_repair_start,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString10Len(0));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【故障报修开始报修推送 pc---->硬件】发送开始,消息:--->{0}", message));
            //server.Send(tcpClient, data);
            CANTcpServer.server.SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)), tcpClient);
            tcpLogInfo.Info(string.Format("【故障报修开始报修推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
        /// <summary>
        /// 完成报修
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="xor"></param>
        /// <param name="tcpClient"></param>
        public void SendSuccessFaultRepair(string mainTrackNo, string statingNo, string xor = null, TcpClient tcpClient = null)
        {
            if (string.IsNullOrEmpty(xor))
            {
                xor = SuspeConstants.XOR;
            }
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                HexHelper.TenToHexString2Len(mainTrackNo),
                HexHelper.TenToHexString2Len(statingNo),
                SuspeConstants.cmd_fault_repair_success,
                xor,
                SuspeConstants.address_fault_repair_success,
                HexHelper.TenToHexString2Len(0),
                HexHelper.TenToHexString10Len(0));

            var sendDataList = new List<byte>();
            var data = HexHelper.StringToHexByte(message);
            tcpLogInfo.Info(string.Format("【故障报修完成报修推送 pc---->硬件】发送开始,消息:--->{0}", message));
            //server.Send(tcpClient, data);
            CANTcpServer.server.SendMessageByByte(data, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mainTrackNo)), tcpClient);
            tcpLogInfo.Info(string.Format("【故障报修完成报修推送 pc---->硬件】发送完成，消息:--->{0}", message));
        }
    }
}

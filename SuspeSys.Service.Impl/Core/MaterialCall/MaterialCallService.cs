using Sus.Net.Common.Constant;
using Sus.Net.Common.Entity;
using Sus.Net.Common.SusBusMessage;
using Sus.Net.Server;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{

    /// <summary>
    /// 缺料呼叫
    /// </summary>
    public class MaterialCallService : SusLog
    {
        private MaterialCallService() { }
        public static readonly MaterialCallService Instance = new MaterialCallService();

        internal void MaterialCallHandler(MaterialCallRequestMessage request)
        {
            var message = request;

            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);

            //获取缺料信息
            var lackMater = SusCacheProductService.Instance.LackMaterialsTable();
            if (lackMater == null)
            {
                CANTcpServer.server.SendExceptionNotMaterial(XID, ID);
            }
            else
            {

                this.SendMessage(XID, ID, lackMater);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="id"></param>
        private void SendMessage(int mid, int id, List<Domain.LackMaterialsTable> lackMater)
        {
            var strLackMaterial = LackMaterials(lackMater);

            //byte list
            var bytes = LackMeterialBytes(strLackMaterial);

            //CANTcpServer.server.SendShowData(bytes,mid, id);
            if (null == bytes || bytes.Count == 0)
            {
                var ex = new ApplicationException("发送内容不能为空!");
                tcpLogError.Error("【发送内容不能为空】", ex);
                throw ex;
            }

            string xor = SuspeConstants.XOR;

            var j = 0;
            var times = 0;
            if (bytes.Count % 6 == 0)
            {
                times = bytes.Count / 6;
            }
            else
            {
                times = 1 + bytes.Count / 6;
            }
            var decBeginAddress = SuspeConstants.address_lack_meterials_response_begin;// 0x0160;//0160
            var decEndAddress = SuspeConstants.address_lack_meterials_response_end;//0x017F;//017F

            for (var i = 0; i < times; i++)
            {
                if (decBeginAddress > decEndAddress)
                {
                    var ex = new ApplicationException(string.Format("发送缺料信息超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【缺料呼叫】", ex);
                }
                var sendDataList = new List<byte>();
                var sData = this.GetHeaderBytesExt(mid.ToString(), id.ToString(), HexHelper.TenToHexString4Len(decBeginAddress), xor);
                sendDataList.AddRange(sData);
                if (j < bytes.Count)
                {
                    for (int b = j; j < bytes.Count; j++)
                    {
                        if (sendDataList.Count == 12)
                        {
                            break;
                        }
                        sendDataList.Add(bytes[j]);
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
                tcpLogInfo.Info(string.Format("【缺料呼叫 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                //CANTcpServer.server.SendShowData
                //client.Send(bs);
                CANTcpServer.server.SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
                Console.WriteLine(string.Format("【缺料呼叫 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【缺料呼叫 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【缺料呼叫pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mid, id)));
        }

        private  byte[] GetHeaderBytesExt(string mainTrackNo, string statingNo, string hexAddress, string xor = null)
        {
            if (string.IsNullOrEmpty(xor))
                xor = "00";

            string hexStr = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_Lack_Materials_Reponse, xor, hexAddress);
            return HexHelper.StringToHexByte(hexStr);
        }


        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="lackMater"></param>
        /// <returns></returns>
        private string LackMaterials(List<Domain.LackMaterialsTable> lackMater)
        {
            //获取

            StringBuilder builder = new StringBuilder();
            foreach (var item in lackMater)
            {
                builder.Append($"{item.LackMaterialsCode}-{item.LackMaterialsName};");
            }

            return builder.ToString();
        }

        /// <summary>
        /// 获取byte
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private List<byte> LackMeterialBytes(string info)
        {
            var infoByte = UnicodeUtils.CharacterToCoding(info);
            var infoByteList = HexHelper.StringToHexByte(infoByte);

            return new System.Collections.Generic.List<byte>(infoByteList);
        }


    }
}

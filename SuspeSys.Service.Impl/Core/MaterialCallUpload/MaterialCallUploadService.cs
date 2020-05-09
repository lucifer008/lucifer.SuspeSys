using Sus.Net.Common.Constant;
using Sus.Net.Common.Entity;
using Sus.Net.Common.SusBusMessage;
using Sus.Net.Server;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{

    public class MaterialCallUploadService : SusLog
    {
        private MaterialCallUploadService() { }
        public static readonly MaterialCallUploadService Instance = new MaterialCallUploadService();

        internal void MaterialCallUploadHandler(MaterialCallUploadRequestMessage request, TcpClient tcpClient = null)
        {
            var message = request;

            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);

            var materialCode = request.MaterialCode;

            var LackMaterials = SusCacheProductService.Instance.LackMaterialsTable().FirstOrDefault(o => o.LackMaterialsCode.Trim().Equals(materialCode.ToString()));
            if (LackMaterials == null)
            {
                CANTcpServer.server.SendExceptionNotMaterial(XID, ID);
                return;
            }

            var materialName = LackMaterials.LackMaterialsName.Trim();


            if (!this.Valid(XID, ID))
                return;
            else
            {
                this.UploadSuccess(XID, ID, materialCode, materialName, tcpClient);
            }

        }
            
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        private bool Valid(int mainTrackNumber, int statingNo, TcpClient tcpClient = null)
        {
            bool valid = true;
            //var statingCache = NewCacheService.Instance.GetStatingCache(mainTrackNumber, statingNo);
            //if (null == statingCache)
            //{
            //    //站点不存在
            //    CANTcpServer.server.MaterialCallUploadNotStating(mainTrackNumber, statingNo, tcpClient);
            //}

            var CallNoticeStNo = SystemParameterService.Instance.GetHangUpLineOtherValue(Domain.SusEnum.SystemParameterHangUpOther.CallNoticeStNo, mainTrackNumber.ToString(), statingNo.ToString());

            if (string.IsNullOrEmpty(CallNoticeStNo))
            {
                CANTcpServer.server.MaterialCallUploadNotStating(mainTrackNumber, statingNo, tcpClient);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// 呼叫成功
        /// </summary>
        /// <remarks>
        /// 1、上产成功回复硬件指令05 0220 +六位00
        /// 2、缺料写入站点，同步到产线实时信息（表Stating字段(FaultInfo)）及看板写入
        /// 3、发送缺料到缺料接收站点
        /// </remarks>
        private void UploadSuccess(int mainTrackNumber, int statingNo, int materialCode,string materialName , TcpClient tcpClient = null)
        {
            // 1、上传成功回复硬件指令05 0220 +六位00
            CANTcpServer.server.MaterialCallUploadSuccess(mainTrackNumber, statingNo, tcpClient);

            //2、缺料写入站点，同步到产线实时信息（表Stating字段(FaultInfo)）及看板写入
            var kanbanInfo = this.BuildKanbanInfo(mainTrackNumber, statingNo, materialCode, materialName);
            KanbanInfoServiceImpl.Instance.Add(kanbanInfo);

            //3、发送缺料信息到收站点
            this.SendInfo(mainTrackNumber, statingNo, materialCode, materialName, tcpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private KanbanInfo BuildKanbanInfo(int mainTrackNumber, int statingNo, int materialCode, string materialName)
        {
            var statingDto = KanbanInfoServiceImpl.Instance.GetStating(mainTrackNumber, statingNo.ToString());

            return new KanbanInfo()
            {
                CallTime = DateTime.Now,
                Fault = materialName,
                FaultCode = materialCode.ToString(),
                LogId =  Guid.NewGuid().ToString().Replace("-",""),
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                GroupNo = statingDto.GroupNO,
                Mechanic = string.Empty,
                StationNo = statingNo.ToString(),
                Status = (int)KanbanInfoStatus.Pending,
                WorkShop = statingDto.WorkshopCode,
            };
        }

        /// <summary>
        /// 发送缺料到缺料接收站点
        /// 发送缺料信息到接收站：1-8 缺少辅料
        /// 01 01 FF 03 021A 00 00 00 00 01 08
        /// </summary>
        /// <param name="maTrackNumber"></param>
        /// <param name="statingno"></param>
        /// <param name="request"></param>
        private void SendInfo(int mainTrackNumber, int statingNo, int materialCode, string materialName, TcpClient tcpClient = null)
        {
            var CallNoticeStNo = SystemParameterService.Instance.GetHangUpLineOtherValue(Domain.SusEnum.SystemParameterHangUpOther.CallNoticeStNo, mainTrackNumber.ToString(), statingNo.ToString());

            //发送缺料到缺料接收站点
            CANTcpServer.server.MaterialCallUploadSendToStating(mainTrackNumber, statingNo, CallNoticeStNo,tcpClient);

            //缺料信息
            var lackMater = SusCacheProductService.Instance.LackMaterialsTable().Where(o => o.LackMaterialsCode.Trim() == materialCode.ToString()).ToList();

            SendMessage(mainTrackNumber, int.Parse(CallNoticeStNo), lackMater);
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
                    var ex = new ApplicationException(string.Format("发送缺料信息到接收站超出最大地址:{0}", HexHelper.TenToHexString4Len(decEndAddress)));
                    tcpLogError.Error("【发送缺料信息到接收站】", ex);
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
                tcpLogInfo.Info(string.Format("【发送缺料信息到接收站 pc---->硬件】发送开始,【 消息:--->{0}", HexHelper.BytesToHexString(sendDataList.ToArray())));

                var bs = sendDataList.ToArray();//sendDataList.ToArray();
                //Array.Reverse(bs);
                //CANTcpServer.server.SendShowData
                //client.Send(bs);
                CANTcpServer.server.SendMessageByByte(bs, new ClientUserInfo("1", HexHelper.TenToHexString2Len(mid)), null);
                Console.WriteLine(string.Format("【发送缺料信息到接收站 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));
                tcpLogInfo.Info(string.Format("【发送缺料信息到接收站 pc---->硬件】发送完成，【消息:--->{0}", SusNet.Common.Utils.HexHelper.BytesToHexString(sendDataList.ToArray())));

            }

            tcpLogInfo.Info(string.Format("【发送缺料信息到接收站 ，发送制品信息pc---->硬件】发送完成，【主轨号+站号】 消息:--->{0}", string.Format("{0},{1}", mid, id)));
        }

        private byte[] GetHeaderBytesExt(string mainTrackNo, string statingNo, string hexAddress, string xor = null)
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

using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.SusBusMessage
{
    /// <summary>
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    /// 【协议2.0】 挂片站衣架上传请求
    /// 上传出站RFID读到的ID号	硬件-->pc	01 01 06 XX 00 59 05 AA BB CC DD EE	00+5字节ID号码【排产号+衣架号】
    /// </summary>
    public class HangingPieceHangerUploadRequestMessage : MessageBody
    {
        public HangingPieceHangerUploadRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static HangingPieceHangerUploadRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "59".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            {
                IList<byte> bList = new List<byte>();
                for (var index = 7; index < 12; index++)
                {
                    bList.Add(resBytes[index]);
                }
                var m = new HangingPieceHangerUploadRequestMessage(resBytes)
                {
                    HangerNo = HexHelper.BytesToHexString(bList.ToArray()),
                    ProductionNumber = HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
                };
                return m;
            }
            return null;
        }
        /// <summary>
        /// 衣架【十六进制】
        /// </summary>
        public string HangerNo { set; get; }
        /// <summary>
        /// 排产号【十六进制】
        /// </summary>
        public string ProductionNumber { set; get; }
    }
}

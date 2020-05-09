using Sus.Net.Common.Constant;
using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusNet.Common.SusBusMessage
{
    /// <summary>
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    /// 衣架进站硬件终端请求--->PC
    /// 01 44 06 XX 00 50 00 AA BB CC DD EE 衣架入站
    /// </summary>
    public class HangerArrivalStatingRequestMessage : MessageBody
    {
        public HangerArrivalStatingRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static HangerArrivalStatingRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 7; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //// var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //if ("02".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "50".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    return new HangerArrivalStatingRequestMessage(resBytes)
            //    {
            //        HangerNo = HexHelper.BytesToHexString(bList.ToArray())
            //    };
            //}
            //return null;

            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Hanger_InSite.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Hanger_InSite_Req.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new HangerArrivalStatingRequestMessage(resBytes)
            {
                HangerNo = HexHelper.BytesToHexString(bList.ToArray())
            };
        }
        public string HangerNo { set; get; }
    }
}

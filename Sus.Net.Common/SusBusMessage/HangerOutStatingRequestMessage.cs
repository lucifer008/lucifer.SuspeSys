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
    /// 【普通站】衣架出战，硬件请求pc
    /// 01 44 06 XX 00 55 05 AA BB CC DD EE 第5个款式
    /// 用站号区分是否为普通站还是挂片站
    /// 如果是挂片站发出的请求，则取第7个个字节为排产号
    /// </summary>
    public class HangerOutStatingRequestMessage : MessageBody
    {
        public HangerOutStatingRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static HangerOutStatingRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "55".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    IList<byte> bList = new List<byte>();
            //    for (var index = 7; index < 12; index++)
            //    {
            //        bList.Add(resBytes[index]);
            //    }
            //    var m= new HangerOutStatingRequestMessage(resBytes)
            //    {
            //        HangerNo = HexHelper.BytesToHexString(bList.ToArray()),
            //        ProductionNumber = HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
            //    };
            //    return m;
            //}
            //return null;

            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Hanger_OutSite.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Hanger_OutSite_Req.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new HangerOutStatingRequestMessage(resBytes)
            {
                HangerNo = HexHelper.BytesToHexString(bList.ToArray()),
                outType = HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
            };
        }
        public string HangerNo { set; get; }
        /// <summary>
        /// 排产号
        /// </summary>
        public string outType { set; get; }
    }
}

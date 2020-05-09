using Sus.Net.Common.Message;
using SusNet.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusNet.Common.SusBusMessage
{
    /// <summary>
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    ///01 02 06 XX 00 57 02 AA BB CC DD EE
    /// 返工疵点代码请求【硬件请求-->pc】
    /// </summary>
    public class ReworkDefectRequestMessage : MessageBody
    {
        public ReworkDefectRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static ReworkDefectRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            if ("06".Equals(Utils.HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "57".Equals(Utils.HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            {
                IList<byte> bList = new List<byte>();
                for (var index = 7; index < 12; index++)
                {
                    bList.Add(resBytes[index]);
                }
                var m = new ReworkDefectRequestMessage(resBytes)
                {
                    HangerNo = Utils.HexHelper.BytesToHexString(bList.ToArray()),
                    ReworkDefectCode = Utils.HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
                };
                return m;
            }
            return null;
        }
        public string HangerNo { set; get; }
        /// <summary>
        /// 返工代码
        /// </summary>
        public string ReworkDefectCode { set; get; }
    }
}

using Sus.Net.Common.Constant;
using Sus.Net.Common.Message;
using Sus.Net.Common.Utils;
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
    ///01 02 06 XX 00 56 01 AA BB CC DD EE
    /// 返工【硬件请求-->pc】
    /// </summary>
    public class ReworkRequestMessage : MessageBody
    {
        public ReworkRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static ReworkRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //if ("06".Equals(Utils.HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "56".Equals(Utils.HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    IList<byte> bList = new List<byte>();
            //    for (var index = 7; index < 12; index++)
            //    {
            //        bList.Add(resBytes[index]);
            //    }
            //    var m = new ReworkRequestMessage(resBytes)
            //    {
            //        HangerNo = Utils.HexHelper.BytesToHexString(bList.ToArray()),
            //        ReworkFlowCode = Utils.HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
            //    };
            //    return m;
            //}
            //return null;
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_ReturnWork_Request.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_ReturnWork_Req.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            var message = new ReworkRequestMessage(resBytes)
            {
                HangerNo = Utils.HexHelper.BytesToHexString(bList.ToArray())
            };
            
            //ascii 解码,这里好像不传，具体看测试
            message.ReworkFlowCode = AssicUtils.DecodeByBytes(new byte[] { resBytes[6] });
            return message;
        }
        public string HangerNo { set; get; }
        /// <summary>
        /// 返工代码(assic解码后的)
        /// </summary>
        public string ReworkFlowCode { set; get; }
    }
}

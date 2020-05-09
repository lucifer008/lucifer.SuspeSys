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
    /// 【衣架站内工序比较】
    /// 
    /// 衣架落入读卡器发送的请求，硬件发pc端
    /// 【衣架在04站内进行前后衣架对比的命令】
    /// 01 04 06 XX 00 54 00 AA BB CC DD EE
    /// </summary>
    public class HangerDropCardRequestMessage : MessageBody
    {
        public HangerDropCardRequestMessage(byte[] resBytes) : base(resBytes)
        {
            
        }
        public static HangerDropCardRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "00".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[4] })) && "54".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    IList<byte> bList = new List<byte>();
            //    for (var index = 7; index < 12; index++)
            //    {
            //        bList.Add(resBytes[index]);
            //    }
            //    return new HangerDropCardRequestMessage(resBytes)
            //    {
            //        HangerNo = HexHelper.BytesToHexString(bList.ToArray())
            //    };
            //}
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4],resBytes[5]});
            if (!SuspeConstants.address_Flow_Compare.Equals(address)) {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Flow_Compare_Res.Equals(cmd)) {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new HangerDropCardRequestMessage(resBytes)
            {
                HangerNo = HexHelper.BytesToHexString(bList.ToArray())
            };
        }
        public string HangerNo { set; get; }
      
    }
}

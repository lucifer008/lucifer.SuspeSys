using Sus.Net.Common.Constant;
using Sus.Net.Common.Utils;
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
    /// 01 04 06 XX 00 60 00 AA BB CC DD EE
    /// 终端读到衣架卡，员工卡，衣车卡，
    /// 机修卡向PC发送命令
    /// 卡片【请求】
    /// </summary>
    public class CardRequestMessage : MessageBody
    {
        public CardRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { set; get; }
        public string MainTrackNo { set; get; }
        public string StatingNo { set; get; }

        public static CardRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 7; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //// var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            //if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "0060".Equals(address))
            //{
            //    return new CardRequestMessage(resBytes)
            //    {
            //        CardNo = StringUtils.ToFixLenStringFormat(HexHelper.BytesToHexString(bList.ToArray())),
            //        MainTrackNo = HexHelper.BytesToHexString(resBytes[0]),
            //        StatingNo = HexHelper.BytesToHexString(resBytes[1])
            //    };
            //}
            //return null;

            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Card_Login.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Card_Login_Req.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            var message = new CardRequestMessage(resBytes)
            {
                CardNo = StringUtils.ToFixLenStringFormat(HexHelper.BytesToHexString(bList.ToArray())),
                MainTrackNo = HexHelper.BytesToHexString(resBytes[0]),
                StatingNo = HexHelper.BytesToHexString(resBytes[1])
            };
            return message;
        }
    }

}

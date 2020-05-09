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
    /// 衣架出站清理缓存 请求
    /// pc--->硬件
    /// 01 04 03 XX 00 52 00 AA BB CC DD EE
    //清除01主轨04挂片站衣架【AABBCCDDEE】 缓存
    /// </summary>
    public class ClearHangerCacheRequestMessage : MessageBody
    {
        private const string cmd = "03";
        private const string addh = "00";
        private const string addl = "52";

        public ClearHangerCacheRequestMessage(byte[] resBytes) : base(resBytes)
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainTrackNo">主轨号</param>
        /// <param name="statingNo">站号</param>
        ///<param name="hangerNo">衣架号</param>
        /// <param name="xor"></param>
        public ClearHangerCacheRequestMessage(string mainTrackNo, string statingNo,int hangerNo, string xor = null)
        {
            XID = mainTrackNo;
            ID = statingNo;
            CMD = cmd;
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH = addh;
            ADDL = addl;
            DATA1 = "00";
            var bHanger = HexHelper.StringToHexByte(StringUtils.ToFixLenStringFormat(HexHelper.TenToHexString2Len(hangerNo)));
            if (bHanger.Length != 5)
            {
                tcpLogError.Error("ClearHangerCacheRequestMessage", new ApplicationException("衣架号长度有误!"));
            }
            DATA2 = HexHelper.BytesToHexString(new byte[] { bHanger[0] });
            DATA3 = HexHelper.BytesToHexString(new byte[] { bHanger[1] });
            DATA4 = HexHelper.BytesToHexString(new byte[] { bHanger[2] });
            DATA5 = HexHelper.BytesToHexString(new byte[] { bHanger[3] });
            DATA6 = HexHelper.BytesToHexString(new byte[] { bHanger[4] });
        }
    }
}

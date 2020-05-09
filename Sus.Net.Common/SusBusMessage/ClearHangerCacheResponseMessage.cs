using Sus.Net.Common.Constant;
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
    /// 衣架出站清理缓存 响应
    /// pc--->硬件
    /// 01 04 04 XX 00 52 00 AA BB CC DD EE
    //清除01主轨04挂片站衣架【AABBCCDDEE】 缓存 响应
    /// </summary>
    public class ClearHangerCacheResponseMessage : MessageBody
    {
        //private const string cmd = "04";
        //private const string addh = "00";
        //private const string addl = "52";
        public ClearHangerCacheResponseMessage(byte[] resBytes) : base(resBytes)
        { }
        public static ClearHangerCacheResponseMessage isEqual(Byte[] resBytes)
        {
            //var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            //if (!cmd.Equals(rCmd)) return null;

            //var rAddl = HexHelper.BytesToHexString(new byte[] { resBytes[4] });
            //if (!addh.Equals(rAddl)) return null;

            //var rAddh = HexHelper.BytesToHexString(new byte[] { resBytes[5] });
            //if (!addh.Equals(rAddl)) return null;

            //IList<byte> bList = new List<byte>();
            //for (var index = 7; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //return new ClearHangerCacheResponseMessage(resBytes)
            //{
            //    HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            //};
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Clear_HangerCache.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Clear_HangerCache_Res.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            var message = new ClearHangerCacheResponseMessage(resBytes)
            {
                HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };

            message.MainTrackNo = HexHelper.HexToTen(message.XID);
            message.StatingNo = HexHelper.HexToTen(message.ID);
            return message;
        }
        public int HangerNo { set; get; }
        public int StatingNo { set; get; }
        public int MainTrackNo { set; get; }
    }
}

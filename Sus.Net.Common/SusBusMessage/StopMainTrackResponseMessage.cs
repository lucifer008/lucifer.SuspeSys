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
    /// 01 44 04 XX 00 37 00 00 00 00 00 11
    /// 停止主轨【响应】
    /// </summary>
    public class StopMainTrackResponseMessage : MessageBody
    {
        const string address = SuspeConstants.address_MainTrack;
        const string data_tag = SuspeConstants.data_MainTrack_StopTag;
        public static StopMainTrackResponseMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 4; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //var hexStr = HexHelper.BytesToHexString(bList.ToArray());
            //if (hexStr.Replace(" ", "").Equals("0037000000000011") && "04".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })))
            //{
            //    return new StopMainTrackResponseMessage(resBytes);
            //}
            var rAddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var rDataTag = HexHelper.BytesToHexString(new byte[] { resBytes[10],resBytes[11]});
            if (address.Equals(rAddress) && data_tag.Equals(rDataTag)) {
                return new StopMainTrackResponseMessage(resBytes);
            }
            return null;
        }
       
        public StopMainTrackResponseMessage(byte[] resBytes):base(resBytes)
        { }
    }
}

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
    /// 急停主轨 【回复】:01 44 04 XX 00 37 00 00 00 00 00 12
    /// </summary>
    public class EmergencyStopMainTrackResponseMessage : MessageBody
    {
        public static EmergencyStopMainTrackResponseMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 4; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //var hexStr = HexHelper.BytesToHexString(bList.ToArray());
            //if (hexStr.Replace(" ", "").Equals("0037000000000012") && "04".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })))
            //{
            //    return new EmergencyStopMainTrackResponseMessage(resBytes);
            //}
            //return null;
            //var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var data = HexHelper.BytesToHexString(new byte[2] { resBytes[10], resBytes[11] });
            if (!SuspeConstants.address_MainTrack.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_MainTrack_Response.Equals(rCmd))
            {
                return null;
            }
            if (!SuspeConstants.data_MainTrack_EmergencyTag.Equals(data))
            {
                return null;
            }
            return new EmergencyStopMainTrackResponseMessage(resBytes);
        }

        public EmergencyStopMainTrackResponseMessage(byte[] resBytes):base(resBytes)
        { }
    }
}

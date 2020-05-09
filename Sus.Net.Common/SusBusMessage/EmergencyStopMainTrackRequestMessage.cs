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
    /// 急停主轨道【请求】01 00 03 XX 00 37 00 00 00 00 00 12
    /// </summary>
    public class EmergencyStopMainTrackRequestMessage : MessageBody
    {
        const string address = SuspeConstants.address_MainTrack;
        const string data_tag = SuspeConstants.data_MainTrack_EmergencyTag;
        const string cmdReq = SuspeConstants.cmd_MainTrack_Request;
        const string cmdRes = SuspeConstants.cmd_MainTrack_Response;

        public EmergencyStopMainTrackRequestMessage(string mainTrackNo, string xor = null)
        {
            XID = mainTrackNo;
            ID = "00";
            CMD = cmdReq;
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH =address.Substring(0,2);
            ADDL = address.Substring(2,2);
            DATA1 = "00";
            DATA2 = "00";
            DATA3 = "00";
            DATA4 = "00";
            DATA5 = data_tag.Substring(0,2);
            DATA6 =data_tag.Substring(2,2);
        }
        public static EmergencyStopMainTrackRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 4; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //var hexStr = HexHelper.BytesToHexString(bList.ToArray());
            //if (hexStr.Replace(" ", "").Equals("0037000000000012") && "03".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })))
            //{
            //    return new EmergencyStopMainTrackRequestMessage(resBytes);
            //}
            //return null;

            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            var rAddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var rDataTag = HexHelper.BytesToHexString(new byte[] { resBytes[10], resBytes[11] });
            if (rCmd.Equals(cmdRes) && address.Equals(rAddress) && data_tag.Equals(rDataTag))
            {
                return new EmergencyStopMainTrackRequestMessage(resBytes);
            }
            return null;
        }

        public EmergencyStopMainTrackRequestMessage(byte[] resBytes):base(resBytes)
        { }
    }
}

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
    /// 01 00 03 XX 00 37 00 00 00 00 00 10
    /// 启动主轨【请求】
    /// </summary>
    public class StartMainTrackRequestMessage : MessageBody
    {
        const string address = SuspeConstants.address_MainTrack;
        const string data_tag = SuspeConstants.data_MainTrack_StartTag;
        const string cmdReq = SuspeConstants.cmd_MainTrack_Request;
        const string cmdRes = SuspeConstants.cmd_MainTrack_Response;
        public StartMainTrackRequestMessage(string mainTrackNo,string xor=null)
        {
            XID = mainTrackNo;
            ID = "00";
            CMD = cmdReq;
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH = address.Substring(0,2);
            ADDL = address.Substring(2, 2); ;
            DATA1 = "00";
            DATA2 = "00";
            DATA3 = "00";
            DATA4 = "00";
            DATA5 = data_tag.Substring(0,2);
            DATA6 = data_tag.Substring(2,2);
            //   var data = string.Format("{0} 00 {1} {2} {3} 00000000{4}", mainTrackNo,cmdReq,address,data_tag);
            // this = new StartMainTrackRequestMessage(null);
        }

        /// <summary>
        /// 启动主轨 格式：01 00 03 XX 00 37 00 00 00 00 00 10
        /// </summary>
        /// <param name="xid">主轨号</param>
        public StartMainTrackRequestMessage(byte[] resBytes) : base(resBytes)
        { }
        public static StartMainTrackRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            //IList<byte> bList = new List<byte>();
            //for (var index = 4; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            //var hexStr = HexHelper.BytesToHexString(bList.ToArray());
            //if (hexStr.Replace(" ", "").Equals("0037000000000010") && "03".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })))
            //{
            //    return new StartMainTrackRequestMessage(resBytes);
            //}
            var rCmd=HexHelper.BytesToHexString(new byte[] { resBytes[2]});
            var rAddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var rDataTag = HexHelper.BytesToHexString(new byte[] { resBytes[10], resBytes[11] });
            if (rCmd.Equals(cmdRes) && address.Equals(rAddress) && data_tag.Equals(rDataTag))
            {
                return new StartMainTrackRequestMessage(resBytes);
            }
            return null;
        }

    }
}

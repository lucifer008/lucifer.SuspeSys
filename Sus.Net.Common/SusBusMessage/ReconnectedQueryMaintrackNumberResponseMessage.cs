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
    /// 重连后主轨查询上传
    /// </summary>
   public class ReconnectedQueryMaintrackNumberResponseMessage : MessageBody
    {

        public string MainTrackNo { get; private set; }

        /// <summary>
        /// 主轨上传
        /// </summary>
        /// <param name="resBytes"></param>
        public ReconnectedQueryMaintrackNumberResponseMessage(byte[] resBytes) : base(resBytes)
        { }

        /// <summary>
        /// 主轨上传
        /// </summary>
        /// <param name="resBytes"></param>
        /// <returns></returns>
        public static ReconnectedQueryMaintrackNumberResponseMessage isEqual(Byte[] resBytes)
        {

            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var data = HexHelper.BytesToHexString(new byte[2] { resBytes[10], resBytes[11] });
            if (!SuspeConstants.address_Reconnected_Query_MaintrackNumber.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_Reconnected_Query_MaintrackNumber_Res.Equals(rCmd))
            {
                return null;
            }

            return new ReconnectedQueryMaintrackNumberResponseMessage(resBytes) { MainTrackNo = rXID };
        }
    }
}

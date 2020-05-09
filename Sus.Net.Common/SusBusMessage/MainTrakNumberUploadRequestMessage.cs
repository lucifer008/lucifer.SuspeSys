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
    /// 主轨上传
    /// </summary>
    public class MainTrakNumberUploadRequestMessage : MessageBody
    {
        public string MainTrackNo { get; private set; }

        /// <summary>
        /// 主轨上传
        /// </summary>
        /// <param name="resBytes"></param>
        public MainTrakNumberUploadRequestMessage(byte[] resBytes) : base(resBytes)
        { }

        /// <summary>
        /// 主轨上传
        /// </summary>
        /// <param name="resBytes"></param>
        /// <returns></returns>
        public static MainTrakNumberUploadRequestMessage isEqual(Byte[] resBytes)
        {
         
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var data = HexHelper.BytesToHexString(new byte[2] { resBytes[10], resBytes[11] });
            if (!SuspeConstants.address_MainTrakNumberUpload.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_MainTrakNumberUpload_Req.Equals(rCmd))
            {
                return null;
            }
          
            return new MainTrakNumberUploadRequestMessage(resBytes) { MainTrackNo = rXID };
        }

    }
}

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
    public class StatingTypeResponseMessage : MessageBody
    {
        public StatingTypeResponseMessage(byte[] resBytes) : base(resBytes)
        { }

        public static StatingTypeResponseMessage isEqual(Byte[] resBytes)
        {
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var opType = HexHelper.BytesToHexString(resBytes[10]);
            var data = HexHelper.BytesToHexString(resBytes[11]);

            string _address = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingType_ADDH) + HexHelper.TenToHexString2Len(SuspeConstants.address_StatingType_ADDL);
            if (!_address.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_StatingTyp_Res.Equals(rCmd))
            {
                return null;
            }

            //if (!SuspeConstants.data_MainTrack_StartTag.Equals(data))
            //{
            //    return null;
            //}
            return new StatingTypeResponseMessage(resBytes) { MainTrackNo = rXID , OpType= opType, StatingType = data};
        }

        public string MainTrackNo { get; set; }

        /// <summary>
        /// 操作类型  添加/修改
        /// </summary>
        public string OpType { get; set; }

        /// <summary>
        /// 站点类型
        /// </summary>
        public string StatingType { get; set; }
    }
}

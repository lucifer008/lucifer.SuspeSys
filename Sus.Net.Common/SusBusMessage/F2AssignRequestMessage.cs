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
    /// F2指定业务
    /// </summary>
    public class F2AssignRequestMessage : MessageBody
    {
        public F2AssignRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
        public static F2AssignRequestMessage isEqual(byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_F2_Assign_Req.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_F2_Assign_Action.Equals(raddress)) return null;
            var isCrossMainTrack = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[10])) > 0 ? true : false;
            var targetMainTrackNumber = 0;
            var tartgetStatingNo = 0;
            targetMainTrackNumber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0]));
            tartgetStatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11]));
            if (isCrossMainTrack)
            {
                targetMainTrackNumber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[10]));
                tartgetStatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11]));
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new F2AssignRequestMessage(resBytes)
            {
                TargertMainTrackNumber = targetMainTrackNumber,
                SourceMainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0])),
                SourceStatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1])),
                TargertStatingNo =tartgetStatingNo,
                HangerNo =-1,
                IsCrossMainTrack = isCrossMainTrack
            };
        }
        /// <summary>
        /// 发起主轨
        /// </summary>
        public int SourceMainTrackNuber { set; get; }
        /// <summary>
        /// 发起站点
        /// </summary>
        public int SourceStatingNo { set; get; }
        /// <summary>
        /// 指定主轨
        /// </summary>
        public int TargertMainTrackNumber { set; get; }
        /// <summary>
        /// 指定站点
        /// </summary>
        public int TargertStatingNo { set; get; }
        public int HangerNo { set; get; }

        /// <summary>
        ///是否 跨主轨
        /// </summary>
        public bool IsCrossMainTrack { set; get; }
    }

}

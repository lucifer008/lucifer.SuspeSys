using Sus.Net.Common.Constant;
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
    /// 中止故障报修
    /// </summary>
    public class FaultRepairStopRequestMessage : MessageBody
    {
        public FaultRepairStopRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
        public static FaultRepairStopRequestMessage isEqual(byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_Fault_Repair_Stop.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Fault_Repair_Stop.Equals(raddress)) return null;
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new FaultRepairStopRequestMessage(resBytes)
            {
                MainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0])),
                StatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1])),
                CardNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };
        }
        /// <summary>
        /// 发起主轨
        /// </summary>
        public int MainTrackNuber { set; get; }
        /// <summary>
        /// 发起站点
        /// </summary>
        public int StatingNo { set; get; }
        /// <summary>
        /// 机修卡号
        /// </summary>
        public int CardNo { set; get; }
    }
}

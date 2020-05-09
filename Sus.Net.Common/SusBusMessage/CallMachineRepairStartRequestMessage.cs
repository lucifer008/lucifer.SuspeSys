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
    /// 呼叫机修开始
    /// </summary>
    public class CallMachineRepairStartRequestMessage : MessageBody
    {
        public CallMachineRepairStartRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
        public static CallMachineRepairStartRequestMessage isEqual(byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_call_machine_repair_req.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_call_machine_repair_start.Equals(raddress)) return null;
            return new CallMachineRepairStartRequestMessage(resBytes)
            {
                MainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0])),
                StatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1])),
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
    }
}

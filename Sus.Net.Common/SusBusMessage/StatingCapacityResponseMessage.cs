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
    public class StatingCapacityResponseMessage : MessageBody
    {
        public StatingCapacityResponseMessage(byte[] resBytes) : base(resBytes)
        { }

        public static StatingCapacityResponseMessage isEqual(Byte[] resBytes)
        {
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var data = HexHelper.BytesToHexString(new byte[2] { resBytes[10], resBytes[11] });

            string _address = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingCapacity_ADDH) + HexHelper.TenToHexString2Len(SuspeConstants.address_StatingCapacity_ADDL);
            if (!_address.Equals(address))
            {
                return null;
            }
            //if (!SuspeConstants.cmd_StatingCapacity_Res.Equals(rCmd))
            //{
            //    return null;
            //}

            var capacity = HexHelper.HexToTen(data);
   
            return new StatingCapacityResponseMessage(resBytes) {  Capacity = capacity};
        }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; set; }

        public string LogMsg { get; set; }
    }
}

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
    public class PowerSupplyInitRequestMessage : MessageBody
    {
        public int MainTrackNo { set; get; }
        public int StatingNo { set; get; }
        public PowerSupplyInitRequestMessage(byte[] resBytes) : base(resBytes)
        { }

        public static PowerSupplyInitRequestMessage isEqual(Byte[] resBytes)
        {
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rID = HexHelper.BytesToHexString(resBytes[1]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            // var opType = HexHelper.BytesToHexString(resBytes[10]);
            //var data = HexHelper.BytesToHexString(resBytes[11]);

            string _address = SuspeConstants.address_Power_Supply_Init;
            if (!_address.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_Power_Supply_Init.Equals(rCmd))
            {
                return null;
            }
            return new PowerSupplyInitRequestMessage(resBytes)
            {
                MainTrackNo = HexHelper.HexToTen(rXID),
                StatingNo = HexHelper.HexToTen(rID)
            };
        }
    }
}

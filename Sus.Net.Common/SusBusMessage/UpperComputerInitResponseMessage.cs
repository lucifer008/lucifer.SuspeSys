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
    public class UpperComputerInitResponseMessage : MessageBody
    {
        public int MainTrackNo { set; get; }
        public int StatingNo { set; get; }
        public int SN { set; get; }
        public int MainboardVersion { set; get; }
        public UpperComputerInitResponseMessage(byte[] resBytes) : base(resBytes)
        { }

        public static UpperComputerInitResponseMessage isEqual(Byte[] resBytes)
        {
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rid = HexHelper.BytesToHexString(resBytes[1]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            // var opType = HexHelper.BytesToHexString(resBytes[10]);
            //var data = HexHelper.BytesToHexString(resBytes[11]);

            string _address = SuspeConstants.address_Power_Supply_Init_UpperComputer;
            if (!_address.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_Power_Supply_Init_UpperComputer_Res.Equals(rCmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 6; index < 10; index++)
            {
                bList.Add(resBytes[index]);
            }
            IList<byte> bList2 = new List<byte>();
            for (var index = 10; index < 12; index++)
            {
                bList2.Add(resBytes[index]);
            }
            return new UpperComputerInitResponseMessage(resBytes)
            {
                MainTrackNo = HexHelper.HexToTen(rXID),
                StatingNo = HexHelper.HexToTen(rid),
                SN =HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray())),
                MainboardVersion = HexHelper.HexToTen(HexHelper.BytesToHexString(bList2.ToArray()))
            };
        }
    }
}

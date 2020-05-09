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
    /// 主板版本号
    /// </summary>
    public class MainboardVersionRequestMessage : MessageBody
    {
        public int MainTrackNo { set; get; }
        public int StatingNo { set; get; }
        public string MainboardVersion { set; get; }
        public MainboardVersionRequestMessage(byte[] resBytes) : base(resBytes)
        { }

        public static MainboardVersionRequestMessage isEqual(Byte[] resBytes)
        {
            var rXID = HexHelper.BytesToHexString(resBytes[0]);
            var rid = HexHelper.BytesToHexString(resBytes[1]);
            var rCmd = HexHelper.BytesToHexString(resBytes[2]);
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            // var opType = HexHelper.BytesToHexString(resBytes[10]);
            //var data = HexHelper.BytesToHexString(resBytes[11]);

            string _address = SuspeConstants.address_Mainboard_Version;
            if (!_address.Equals(address))
            {
                return null;
            }
            if (!SuspeConstants.cmd_Mainboard_Version.Equals(rCmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 6; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new MainboardVersionRequestMessage(resBytes)
            {
                MainTrackNo = HexHelper.HexToTen(rXID),
                StatingNo = HexHelper.HexToTen(rid),
                MainboardVersion= HexHelper.BytesToHexString(bList.ToArray())
            };
        }
    }
}
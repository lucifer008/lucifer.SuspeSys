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
    public class MonitorMessage: MessageBody
    {
        private const string address = SuspeConstants.address_Monitor;
        public MonitorMessage(byte[] resBytes) : base(resBytes)
        { }
        public static MonitorMessage isEqual(Byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_Monitor.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Monitor.Equals(raddress)) return null;

           
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new MonitorMessage(resBytes)
            {
                HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };
        }
        public int HangerNo { set; get; }
      
    }
}

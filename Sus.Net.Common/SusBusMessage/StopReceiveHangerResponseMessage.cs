using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusNet.Common.SusBusMessage
{
    /// <summary>
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    /// 01 44 06 XX 00 05 00 00 00 00 00 02 
    /// 软件 停止接收衣架【响应】
    /// </summary>
    public class StopReceiveHangerResponseMessage : MessageBody
    {
        public StopReceiveHangerResponseMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public StopReceiveHangerResponseMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            IList<byte> bList = new List<byte>();
            for (var index = 4; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            var hexStr = HexHelper.BytesToHexString(bList.ToArray());
            if (hexStr.Replace(" ", "").Equals("0005000000000002") && "05".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })))
            {
                return new StopReceiveHangerResponseMessage(resBytes);
            }
            return null;
        }
    }
}

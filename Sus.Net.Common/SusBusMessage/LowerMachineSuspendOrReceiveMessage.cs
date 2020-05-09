using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.SusBusMessage
{
    public class LowerMachineSuspendOrReceiveMessage : SusNet.Common.Message.MessageBody
    {
        public LowerMachineSuspendOrReceiveMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static LowerMachineSuspendOrReceiveMessage isEqual(Byte[] resBytes)
        {
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Suspend_OR_Receive_Hanger.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Lower_Machine_Suspend_OR_Receive_Hanger_Request.Equals(cmd))
            {
                return null;
            }

            var message = new LowerMachineSuspendOrReceiveMessage(resBytes)
            {
                Tag = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11]))
            };
            return message;
        }
        /// <summary>
        /// 0:接收衣架;1:暂停接收衣架
        /// </summary>
        public int Tag { set; get; }
    }
}

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
    /// 缺料呼叫 返回消息
    /// </summary>
    public class MaterialCallResponseMessage: MessageBody
    {
        public MaterialCallResponseMessage(byte[] bytes) :base(bytes)
        {

        }

        /// <summary>
        /// 创建无缺料消息
        /// </summary>
        /// <returns></returns>
        public static MaterialCallResponseMessage CreateEmptyMsg(string xid, string id)
        {
            //var msg = new MaterialCallResponseMessage(null)
            //{

            //    XID = xid,
            //    ID = id,
            //    CMD = ,
            //    XOR = HexHelper.BytesToHexString(new byte[] { resBytes[3] }),
            //    ADDH = HexHelper.BytesToHexString(new byte[] { resBytes[4] }),
            //    ADDL = HexHelper.BytesToHexString(new byte[] { resBytes[5] }),
            //    DATA1 = HexHelper.BytesToHexString(new byte[] { resBytes[6] }),
            //    DATA2 = HexHelper.BytesToHexString(new byte[] { resBytes[7] }),
            //    DATA3 = HexHelper.BytesToHexString(new byte[] { resBytes[8] }),
            //    DATA4 = HexHelper.BytesToHexString(new byte[] { resBytes[9] }),
            //    DATA5 = HexHelper.BytesToHexString(new byte[] { resBytes[10] }),
            //    DATA6 = HexHelper.BytesToHexString(new byte[] { resBytes[11] }),
            //};

            return null;
        }
    }
}

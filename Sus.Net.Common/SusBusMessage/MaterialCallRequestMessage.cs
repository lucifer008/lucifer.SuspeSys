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
    /// 缺料呼叫消息
    /// </summary>
    public class MaterialCallRequestMessage : MessageBody
    {


        public MaterialCallRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }

        

        public static MaterialCallRequestMessage isEqual(Byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_Lack_Materials_Call_Upload.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Lack_Materials_Call.Equals(raddress)) return null;


            //IList<byte> bList = new List<byte>();
            //for (var index = 10; index < 12; index++)
            //{
            //    bList.Add(resBytes[index]);
            //}
            return new MaterialCallRequestMessage(resBytes)
            {
                //MaterialCode = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
                //HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };
        }
    }
}

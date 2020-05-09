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
    /// 缺料终止消息
    /// </summary>
    public class MaterialCallStopRequestMessaage : MessageBody
    {
        /// <summary>
        /// 缺料代码
        /// </summary>
        public int MaterialCode { get; set; }

        public MaterialCallStopRequestMessaage(byte[] resBytes) : base(resBytes)
        {

        }



        public static MaterialCallStopRequestMessaage isEqual(Byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_Lack_Materials_Call_Stop.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Lack_Materials_Call_Stop.Equals(raddress)) return null;


            IList<byte> bList = new List<byte>();
            for (var index = 11; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new MaterialCallStopRequestMessaage(resBytes)
            {
                MaterialCode = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
                //HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };
        }
    }
}

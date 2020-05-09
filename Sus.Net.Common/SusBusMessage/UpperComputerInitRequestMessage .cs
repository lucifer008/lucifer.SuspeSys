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
    /// 上位机初始化站点消息
    /// </summary>
    public class UpperComputerInitRequestMessage:MessageBody
    {
        public static byte[] GetUpperComputerInitRequestMessage(int mainTrackNumber) {
            var bytes = new List<byte>() { 0,0,0,0,0,0};
            var messgae = string.Format("{0} {1} {2} 00 {3} {4}",
                HexHelper.TenToHexString2Len(mainTrackNumber),
                "00",
                SuspeConstants.cmd_Power_Supply_Init_UpperComputer_Req,
                SuspeConstants.address_Power_Supply_Init_UpperComputer,
                HexHelper.BytesToHexString(bytes.ToArray()));
            return HexHelper.StringToHexByte(messgae);
        }
    }
}

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
    public class EmployeeResponseMessage : MessageBody
    {
        public static byte[] GetHeaderBytesExt(string mainTrackNo, string statingNo, string hexAddress, string xor = null)
        {
            if (string.IsNullOrEmpty(xor))
                xor = "00";
            string hexStr = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_Send_Employee_Login_Info, xor, hexAddress);
            return HexHelper.StringToHexByte(hexStr);
        }
    }
}

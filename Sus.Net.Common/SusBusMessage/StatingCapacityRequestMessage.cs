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
    /// 修改站点容量
    /// </summary>
    public class StatingCapacityRequestMessage : MessageBody
    {
        

        public StatingCapacityRequestMessage(string mainTrackNo, string statingId,  string data, string cmd, string xor=null)
        {
            base.XID = mainTrackNo;
            base.ID = statingId;
            base.CMD = cmd; //SuspeConstants.cmd_StatingCapacity_Req; //cmdReq;
            base.XOR = "FF";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            base.ADDH = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingCapacity_ADDH);
            base.ADDL = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingCapacity_ADDL);
            base.DATA1 = "00";
            base.DATA2 = "00";
            base.DATA3 = "00";
            base.DATA4 = "00";
            base.DATA5 = data.Substring(0,2);
            base.DATA6 = data.Substring(2, 2);
        }
    }
}

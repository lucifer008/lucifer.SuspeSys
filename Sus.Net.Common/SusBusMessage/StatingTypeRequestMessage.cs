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
    public class StatingTypeRequestMessage : MessageBody
    {


        public StatingTypeRequestMessage(string mainTrackNo, string statingId, string data5, string data6, string xor=null)
        {
            base.XID = mainTrackNo;
            base.ID = statingId;
            base.CMD = SuspeConstants.cmd_StatingTyp_Req; //cmdReq;
            base.XOR = "FF";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            base.ADDH = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingType_ADDH);
            base.ADDL = HexHelper.TenToHexString2Len(SuspeConstants.address_StatingType_ADDL);
            base.DATA1 = "00";
            base.DATA2 = "00";
            base.DATA3 = "00";
            base.DATA4 = "00";
            base.DATA5 = data5;
            base.DATA6 = data6;
        }
    }

    
}

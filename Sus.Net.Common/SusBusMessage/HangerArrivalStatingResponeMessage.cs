﻿using SusNet.Common.Message;
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
    /// 衣架进站PC回复
    /// 01 44 05 XX 00 50 00 AA BB CC DD EE 回复
    /// </summary>
    public class HangerArrivalStatingResponeMessage : MessageBody
    {
        public HangerArrivalStatingResponeMessage(string mainTrackNo, string statingNo, string addh, string addl, string hangerNo, string xor = null)
        {
            XID = mainTrackNo;
            ID = statingNo;
            CMD = "05";
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH = addh;
            ADDL = addl;
            var bHanger = HexHelper.StringToHexByte(hangerNo);
            if (bHanger.Length != 5)
            {
                log.Error("HangerOutStatingResponseMessage", new ApplicationException("衣架号长度有误!"));
            }
            DATA1 = "00";
            DATA2 = HexHelper.BytesToHexString(new byte[] { bHanger[0] });
            DATA3 = HexHelper.BytesToHexString(new byte[] { bHanger[1] });
            DATA4 = HexHelper.BytesToHexString(new byte[] { bHanger[2] });
            DATA5 = HexHelper.BytesToHexString(new byte[] { bHanger[3] });
            DATA6 = HexHelper.BytesToHexString(new byte[] { bHanger[4] });
            //DATA3 = "00";
            //DATA4 = "00";
            //DATA5 = "00";
            //DATA6 = "12";
        }
        public HangerArrivalStatingResponeMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public HangerArrivalStatingResponeMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            IList<byte> bList = new List<byte>();
            for (var index = 6; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            if ("05".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "50".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            {
                return new HangerArrivalStatingResponeMessage(resBytes)
                {
                    HangerNo = HexHelper.BytesToHexString(bList.ToArray())
                };
            }
            return null;
        }
        public string HangerNo { set; get; }
    }
}

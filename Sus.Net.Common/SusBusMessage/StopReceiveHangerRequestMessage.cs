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
    /// 硬件请求【停止接收衣架请求】
    /// 01 44 06 XX 00 05 00 00 00 00 00 02 
    /// </summary>
    public class StopReceiveHangerRequestMessage : MessageBody
    {
        public StopReceiveHangerRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static HangerArrivalStatingRequestMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;
            IList<byte> bList = new List<byte>();
            for (var index = 6; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "50".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            {
                return new HangerArrivalStatingRequestMessage(resBytes)
                {
                    HangerNo = HexHelper.BytesToHexString(bList.ToArray())
                };
            }
            return null;
        }
        public string HangerNo { set; get; }
    }
}

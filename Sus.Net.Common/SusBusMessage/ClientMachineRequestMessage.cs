using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusNet.Common.SusBusMessage
{
    /// <summary>
    /// 制品界面，直接选择产品上线
    /// 01 01 03 XX 00 35 00 00 00 00 00 05
    //给01产线01挂片站点指定05产品上线
    /// </summary>
    public class ClientMachineRequestMessage:SusNet.Common.Message.MessageBody
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainTrackNo">主轨号</param>
        /// <param name="statingNo">挂片站</param>
        /// <param name="addh"></param>
        /// <param name="addl"></param>
        /// <param name="productNumber">排产号</param>
        /// <param name="xor"></param>
        public ClientMachineRequestMessage(string mainTrackNo, string statingNo, string productNumber, string xor = null)
        {
            XID = mainTrackNo;
            ID = statingNo;
            CMD = SuspeConstants.cmd_Clinet_Machine_Online_Req;//"03";
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH = SuspeConstants.address_Client_Manche_Online.Substring(0,2);
            ADDL = SuspeConstants.address_Client_Manche_Online.Substring(2,2);
            DATA1 = "00";
            DATA2 ="00";
            DATA3 = "00";
            DATA4 = "00";
            DATA5 = "00";
            DATA6 = productNumber;
        }
        public ClientMachineRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        //public static ClientMachineRequestMessage isEqual(Byte[] resBytes)
        //{
        //    // Array ar = null;
          
        //    //ProductNumber = HexHelper.byteToHexStr(bList.ToArray());

        //    // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
        //    if ("03".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "00".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[4] })) && "35".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
        //    {
        //        IList<byte> bList = new List<byte>();
        //        bList.Add(resBytes[11]);
        //        return new ClientMachineRequestMessage(resBytes)
        //        {
        //            ProductNumber = HexHelper.BytesToHexString(bList.ToArray())
        //        };
        //    }
        //    return null;
        //}
        public string ProductNumber { set; get; }
    }
}

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
    /// 制品界面，直接选择产品上线 硬件响应
    /// 01 01 03 XX 00 35 00 00 00 00 00 05
    //给01产线01挂片站点指定05产品上线
    /// </summary>
    public class ClientMachineResponseMessage : SusNet.Common.Message.MessageBody
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="mainTrackNo">主轨号</param>
        ///// <param name="statingNo">挂片站</param>
        ///// <param name="addh"></param>
        ///// <param name="addl"></param>
        ///// <param name="productNumber">排产号</param>
        ///// <param name="xor"></param>
        //public ClientMachineResponseMessage(string mainTrackNo, string statingNo, string addh, string addl, string productNumber, string xor = null)
        //{
        //    XID = mainTrackNo;
        //    ID = statingNo;
        //    CMD = "04";
        //    XOR = "00";
        //    if (!string.IsNullOrEmpty(xor))
        //        XOR = xor;
        //    ADDH = addh;
        //    ADDL = addl;
        //    DATA1 = "00";
        //    DATA2 = "00";
        //    DATA3 = "00";
        //    DATA4 = "00";
        //    DATA5 = "00";
        //    DATA6 = productNumber;
        //}
        public ClientMachineResponseMessage(byte[] resBytes) : base(resBytes)
        {}
        public static ClientMachineResponseMessage isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            //ProductNumber = HexHelper.byteToHexStr(bList.ToArray());

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            //if ("04".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "00".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[4] })) && "35".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    IList<byte> bList = new List<byte>();
            //    bList.Add(resBytes[11]);
            //    var clientMachineResponseMessage = new ClientMachineResponseMessage(resBytes)
            //    {
            //        ProductNumber = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            //    };
            //    clientMachineResponseMessage.MainTrackNo =HexHelper.HexToTen(clientMachineResponseMessage.XID);
            //    clientMachineResponseMessage.StatingNo=HexHelper.HexToTen(clientMachineResponseMessage.ID);
            //    return clientMachineResponseMessage;
            //}
            //return null;


            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Client_Manche_Online.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_Client_Manchine_Online_Res.Equals(cmd))
            {
                return null;
            }
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            var message= new ClientMachineResponseMessage(resBytes)
            {
                ProductNumber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11]))
            };

            message.MainTrackNo = HexHelper.HexToTen(message.XID);
            message.StatingNo = HexHelper.HexToTen(message.ID);
            return message;
        }
        public int ProductNumber { set; get; }
        public int StatingNo { set; get; }
        public int MainTrackNo { set; get; }
    }
}

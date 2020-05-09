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
    /// 挂片站终端上线请求
    /// </summary>
   public class HangingPieceStatingOnlineRequestMessage : SusNet.Common.Message.MessageBody
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
        //public HangingPieceStatingOnlineRequestMessage(string mainTrackNo, string statingNo, string addh, string addl, string productNumber, string xor = null)
        //{
        //    XID = mainTrackNo;
        //    ID = statingNo;
        //    CMD = "03";
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
        public HangingPieceStatingOnlineRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }

        /// <summary>
        /// 过滤上线请求起源是否由挂片站发起
        /// </summary>
        /// <param name="resBytes"></param>
        /// <returns></returns>
        public static HangingPieceStatingOnlineRequestMessage isEqual(Byte[] resBytes)
        {
            //if ("06".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[2] })) && "00".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[4] })) && "35".Equals(HexHelper.BytesToHexString(new byte[] { resBytes[5] })))
            //{
            //    IList<byte> bList = new List<byte>();
            //    bList.Add(resBytes[11]);
            //    return new HangingPieceStatingOnlineRequestMessage(resBytes)
            //    {
            //        ProductNumber = HexHelper.BytesToHexString(bList.ToArray())
            //    };
            //}
            //return null;
            var address = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_HangpieceOnline.Equals(address))
            {
                return null;
            }
            var cmd = HexHelper.BytesToHexString(resBytes[2]);
            if (!SuspeConstants.cmd_HangPieceOnline_Req.Equals(cmd))
            {
                return null;
            }
           
            var message = new HangingPieceStatingOnlineRequestMessage(resBytes)
            {
                ProductNumber =HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11]))
            };
            return message;
        }
        public int ProductNumber { set; get; }
    }
}

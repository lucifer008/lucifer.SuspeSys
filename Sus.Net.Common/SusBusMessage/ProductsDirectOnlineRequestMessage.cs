
using Sus.Net.Common.Constant;
using SusNet.Common.Message;
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
    /// 制品界面直接上线pc向硬件发送上线信息【 pc--->硬件 】
    /// 
    /// </summary>
    public class ProductsDirectOnlineRequestMessage : MessageBody
    {
        public ProductsDirectOnlineRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        ///// <summary>
        ///// 制品界面直接上线pc向硬件发送上线信息
        ///// </summary>
        ///// <param name="mainTrackNo"></param>
        ///// <param name="statingNo"></param>
        ///// <param name="addh"></param>
        ///// <param name="addl"></param>
        ///// <param name="productNumber"></param>
        ///// <param name="index"></param>
        ///// <param name="xor"></param>
        ///// <returns></returns>
        //public static byte[] GetHeaderBytes(string mainTrackNo, string statingNo, string addh, string addl, int index)
        //{
        //    //if (string.IsNullOrEmpty(xor))
        //    //    xor = "00";
        //    string hexStr = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_Client_Manche_Online_Return_Display, SuspeConstants.XOR, SuspeConstants.address_Client_Manche_Online_Return_Display);
        //    return HexHelper.StringToHexByte(hexStr);
        //}
        /// <summary>
        /// 【协议2.0】 客户机上线与挂片站上线产量数据推送
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="addh"></param>
        /// <param name="addl"></param>
        /// <param name="productNumber"></param>
        /// <param name="index"></param>
        /// <param name="xor"></param>
        /// <returns></returns>
        public static byte[] GetHeaderBytes(string mainTrackNo, string statingNo)
        {
            string hexStr = string.Format("{0} {1} {2} {3} {4}", mainTrackNo, statingNo, SuspeConstants.cmd_Client_Manche_Online_Return_Display, SuspeConstants.XOR, SuspeConstants.address_Client_Manche_Online_Return_Display);
            return HexHelper.StringToHexByte(hexStr);
        }
        /// <summary>
        /// 是否是制品界面直接上线p请求
        /// </summary>
        /// <param name="resBytes"></param>
        /// <returns></returns>
        public static bool isEqual(Byte[] resBytes, out List<byte> productsInfo)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            var cmd = HexHelper.BytesToHexString(new byte[] { resBytes[0], resBytes[1], resBytes[2] });
            if (cmd.Equals("010405"))
            {
                productsInfo = new List<byte>();
                
                for (int b = 6; b < resBytes.Length; b++)
                {
                    productsInfo.Add(resBytes[b]);
                }
                //productNumber = null;
                return true;
            }
            productsInfo = null;
            return false;
        }
        /// <summary>
        /// 是否是制品界面直接上线请求结束
        /// </summary>
        /// <param name="resBytes"></param>
        /// <returns></returns>
        public static bool isEnd(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            var cmd = HexHelper.BytesToHexString(new byte[] { resBytes[0], resBytes[1], resBytes[2] });
            //var endFlag = new byte[7];
            //resBytes.CopyTo(endFlag, 5);
            var list = new List<byte>();
            for (var i = 6; i < resBytes.Length; i++)
            {
                list.Add(resBytes[i]);
            }
            var endCmdHex = HexHelper.BytesToHexString(list.ToArray());
            IList<byte> bList = new List<byte>();
            if (cmd.Equals("01 04 05".Replace(" ", "")) && "00 00 00 00 00 00".Replace(" ", "").Equals(endCmdHex))
            {
                return true;
            }
            return false;
        }

    }
}

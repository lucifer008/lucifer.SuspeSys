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
    ///01 44 05 XX 00 56 02 AA BB CC DD EE 返工工序
        /*
         * 00＝允许出站；
         01＝不允许出站，站点不存在
        02＝不允许出站，疵点错误，请重新输入疵点
        03＝不允许出站，站点或疵点错误
        04＝不允许出站，站点满或停止工作
        05＝不允许出站，不允许返工操作

         */
    /// 返工【pc响应到--->硬件】
    /// </summary>
    public class ReworkResponseMessage : MessageBody
    {
        /// <summary>
        ///  返工【pc响应到--->硬件】 格式：01 44 05 XX 00 54 02 AA BB CC DD EE 返工工序
        /// </summary>
        /// <param name="xid">主轨号</param>
        public ReworkResponseMessage(byte[] resBytes) : base(resBytes)
        { }

        /// <summary>
        ///返工消息
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag">
        /// 00＝允许出站；
        /// 01＝不允许出站，站点不存在
        /// 02＝不允许出站，疵点错误，请重新输入疵点
        /// 03＝不允许出站，站点或疵点错误
        /// 04＝不允许出站，站点满或停止工作
        /// 05＝不允许出站，不允许返工操作
        /// </param>
        /// <param name="hangerNo"></param>
        /// <param name="xor"></param>
        public ReworkResponseMessage(string mainTrackNo, string statingNo,int tag, string hangerNo, string xor = null)
        {
            XID = HexHelper.TenToHexString2Len(mainTrackNo);
            ID = HexHelper.TenToHexString2Len(statingNo);
            CMD = SuspeConstants.cmd_ReturnWork_Res;
            XOR = "00";
            if (!string.IsNullOrEmpty(xor))
                XOR = xor;
            ADDH = SuspeConstants.address_ReturnWork_Request.Substring(0, 2);
            ADDL = SuspeConstants.address_ReturnWork_Request.Substring(2, 2);
            var bHanger = HexHelper.StringToHexByte(hangerNo);
            if (bHanger.Length != 5)
            {
                log.Error("ReworkResponseMessage", new ApplicationException("衣架号长度有误!"));
            }
            DATA1 = HexHelper.TenToHexString2Len(tag);//类型
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
    }
}

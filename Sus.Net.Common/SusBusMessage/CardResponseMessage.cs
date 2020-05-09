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
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    /// 01 04 06 XX 00 60 00 AA BB CC DD EE
    /// 终端读到衣架卡，员工卡，衣车卡，
    /// 机修卡向PC发送命令的响应
    /// PC向04站点终端的回复命令	
    /// 01 04 05 XX 00 60 00 AA BB CC DD EE	回复数据为00+卡号，则提示该卡出错。随后发送终端显示出错内容
    //	01 04 05 XX 00 60 01 AA BB CC DD EE 回复数据为01+卡号，则该卡为衣架卡。
    //	01 04 05 XX 00 60 02 AA BB CC DD EE 回复数据为02+卡号，则该卡为衣车卡。
    //	01 04 05 XX 00 60 03 AA BB CC DD EE 回复数据为03+卡号，则该卡为机修卡。
    //	01 04 05 XX 00 60 04 AA BB CC DD EE	"回复数据为04+卡号，则该卡为员工卡，且已登录。此命令为员工
    //正常登录的回复"
    //	01 04 05 XX 00 60 05 AA BB CC DD EE 回复数据为05+卡号，则该卡为员工卡，员工重复登录。
    //	01 04 05 XX 00 60 06 AA BB CC DD EE 回复数据为06+卡号，则该卡为员工卡，员工已下班。
    //PC向终端回复完，给终端发送显示内容	"01 02 05 XX 01 00 AB CD EF AB CD EF
    //01 02 05 XX 01 01 AB CD EF AB CD EF
    //....
    //01 02 05 XX 01 20 00 00 00 00 00 00"	"如是员工卡，则显示（工号+姓名）外加1：登录成功；2：重复登录；
    //3：员工已下班；
    //如是衣架卡，则显示衣架信息（制单号+颜色+尺码+单位+下一工序号
    //+下一站点号）；
    //出错信息的显示整理中
    /// </summary>
    public class CardResponseMessage : MessageBody
    {
        public CardResponseMessage(byte[] resBytes) : base(resBytes)
        {

        }
        /// <summary>
        /// 卡片响应
        /// </summary>
        /// <param name="mainTrackNo">十六进制</param>
        /// <param name="statingNo">十六进制</param>
        /// <param name="type">
        /// 0：提示该卡出错。随后发送终端显示出错内容
        /// 1：该卡为衣架卡
        /// 2：卡为衣车卡。
        /// 3: 该卡为机修卡。
        /// 4.:卡为员工卡，且已登录。此命令为员工 正常登录的回复"
        /// 5：则该卡为员工卡，员工重复登录
        /// 6：该卡为员工卡，员工已下班。
        /// </param>
        /// <param name="cardNo">十进制</param>
        /// <param name="xor"></param>
        public CardResponseMessage(string mainTrackNo,string statingNo,int type,string cardNo,string xor=null)
        {
            XID = mainTrackNo;
            ID = statingNo;
            XOR = "00";
            if (null!=xor) {
                XOR = xor;
            }
            var resBytes = HexHelper.StringToHexByte(cardNo);
            if (5!=resBytes.Length) {
                var ex = new ApplicationException(string.Format("卡号长度错误:长度:{0},卡号:{1}", resBytes.Length, cardNo));
                log.Error(ex);
                throw ex;
            }
            CMD = "05";
            ADDH = "00";
            ADDL = "60";
            DATA1 = HexHelper.TenToHexString2Len(type);
            DATA2 = HexHelper.BytesToHexString(resBytes[0]);
            DATA3 = HexHelper.BytesToHexString(resBytes[1]);
            DATA4 = HexHelper.BytesToHexString(resBytes[2]);
            DATA5 = HexHelper.BytesToHexString(resBytes[3]);
            DATA6 = HexHelper.BytesToHexString(resBytes[4]);
        }
    }
    
}

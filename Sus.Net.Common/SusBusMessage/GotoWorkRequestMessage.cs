using SusNet.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusNet.Common.SusBusMessage
{
    /// <summary>
    /// 【XID+ID+CMD+XOR+ADDH+ADDL+DATA1+DATA2+DATA3+DATA4+DATA5+DATA6】
    /// 01 04 06 XX 00 60 00 AA BB CC DD EE
    /// 终端读到衣架卡，员工卡，衣车卡，
    /// 机修卡向PC发送命令
    /// 上班【请求】
    /// </summary>
    public class GotoWorkRequestMessage : MessageBody
    {
        public GotoWorkRequestMessage(byte []resBytes) : base(resBytes) { }
    }
}

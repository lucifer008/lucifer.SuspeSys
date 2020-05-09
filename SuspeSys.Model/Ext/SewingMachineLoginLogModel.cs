using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 衣车登录日志 扩展Model
    /// </summary>
    [Serializable]
    public class SewingMachineLoginLogModel : SewingMachineLoginLog {
        public bool LoginStatusV { set; get; }
    }
}

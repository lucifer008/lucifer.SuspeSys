using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 用户客户机流水线
    /// </summary>
    [Serializable]
    public partial class UserClientMachinesPipelinings : MetaData {
        public virtual string Id { get; set; }
        public virtual UserClientMachines UserClientMachines { get; set; }
        public virtual Pipelining Pipelining { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class UserClientMachines : MetaData {
        public UserClientMachines() { }
        public virtual string Id { get; set; }
        public virtual string UserId { get; set; }
        /// <summary>
        /// 客户机器Id
        /// </summary>
        [Description("客户机器Id")]
        public virtual string ClientMachineId { get; set; }
    }
}

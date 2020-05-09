using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 用户角色表
    /// </summary>
    [Serializable]
    public partial class UserRoles : MetaData {
        public virtual string Id { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Users Users { get; set; }
    }
}

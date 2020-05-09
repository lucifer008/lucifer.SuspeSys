using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 站点角色
    /// </summary>
    [Serializable]
    public partial class StatingRoles : MetaData {
        public StatingRoles() { }
        public virtual string Id { get; set; }
        public virtual string RoleCode { get; set; }
        public virtual string RoleName { get; set; }
    }
}

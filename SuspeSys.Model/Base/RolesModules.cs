using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 角色模块
    /// </summary>
    [Serializable]
    public partial class RolesModules : MetaData {
        public virtual string Id { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Modules Modules { get; set; }
    }
}

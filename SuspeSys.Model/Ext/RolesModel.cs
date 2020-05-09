using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 权限的角色 扩展Model
    /// </summary>
    public class RolesModel : Roles {
        public bool Checked { get; set; }
    }
}

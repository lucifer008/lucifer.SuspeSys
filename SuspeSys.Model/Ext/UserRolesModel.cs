using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 用户角色表 扩展Model
    /// </summary>
    [Serializable]
    public class UserRolesModel : UserRoles {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}

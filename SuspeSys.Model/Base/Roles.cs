using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 权限的角色
    /// </summary>
    [Serializable]
    public partial class Roles : MetaData {
        public Roles() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        [Description("动作名称")]
        public virtual string ActionName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public virtual string Description { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 组织机构
    /// </summary>
    [Serializable]
    public partial class Organizations : MetaData {
        public Organizations() { }
        public virtual string Id { get; set; }
        public virtual Organizations OrganizationsVal { get; set; }
        public virtual string Code { get; set; }
        public virtual string ParentCode { get; set; }
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

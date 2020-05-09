using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 职务
    /// </summary>
    [Serializable]
    public partial class Position : MetaData {
        public Position() { }
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 职务编号
        /// </summary>
        [Description("职务编号")]
        public virtual string PosCode { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        [Description("职务名称")]
        public virtual string PosName { get; set; }
    }
}

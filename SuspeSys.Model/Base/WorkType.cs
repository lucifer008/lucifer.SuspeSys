using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工种信息
    /// </summary>
    [Serializable]
    public partial class WorkType : MetaData {
        public WorkType() { }
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual string WTypeCode { get; set; }
        public virtual string WTypeName { get; set; }
    }
}

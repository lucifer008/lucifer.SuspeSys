using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 站点方向
    /// </summary>
    [Serializable]
    public partial class StatingDirection : MetaData {
        public StatingDirection() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 方向Key
        /// </summary>
        [Description("方向Key")]
        public virtual string DirectionKey { get; set; }
        /// <summary>
        /// 方向描述
        /// </summary>
        [Description("方向描述")]
        public virtual string DirectionDesc { get; set; }
    }
}

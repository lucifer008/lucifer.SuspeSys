using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 地区
    /// </summary>
    [Serializable]
    public partial class Area : MetaData {
        public Area() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual City City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [Description("地区")]
        public virtual string AreaName { get; set; }
        public virtual string AreaCode { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [Description("详细地址")]
        public virtual string Addess { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

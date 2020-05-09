using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 颜色
    /// </summary>
    [Serializable]
    public partial class PoColor : MetaData {
        public PoColor() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual string SNo { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string ColorValue { get; set; }
        /// <summary>
        /// 尺码描述
        /// </summary>
        [Description("尺码描述")]
        public virtual string ColorDescption { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Rmark { get; set; }
    }
}

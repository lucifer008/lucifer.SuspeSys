using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 尺码
    /// </summary>
    [Serializable]
    public partial class PSize : MetaData {
        public PSize() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual string PsNo { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string Size { get; set; }
        /// <summary>
        /// 尺码描述
        /// </summary>
        [Description("尺码描述")]
        public virtual string SizeDesption { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

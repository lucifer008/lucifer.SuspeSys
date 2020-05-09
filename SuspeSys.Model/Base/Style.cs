using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 款式工艺
    /// </summary>
    [Serializable]
    public partial class Style : MetaData {
        public Style() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [Description("款号")]
        public virtual string StyleNo { get; set; }
        /// <summary>
        /// 款式名称
        /// </summary>
        [Description("款式名称")]
        public virtual string StyleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Rmark { get; set; }
    }
}

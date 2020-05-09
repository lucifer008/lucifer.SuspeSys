using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制单明细颜色尺码明细
    /// </summary>
    [Serializable]
    public partial class ProcessOrderColorSizeItem : MetaData {
        public virtual string Id { get; set; }
        public virtual ProcessOrderColorItem ProcessOrderColorItem { get; set; }
        public virtual PSize PSize { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string SizeDesption { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Description("数量")]
        public virtual string Total { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

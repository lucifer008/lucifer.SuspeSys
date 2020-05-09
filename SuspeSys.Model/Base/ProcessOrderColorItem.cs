using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制单明细颜色
    /// </summary>
    [Serializable]
    public partial class ProcessOrderColorItem : MetaData {
        public ProcessOrderColorItem() { }
        public virtual string Id { get; set; }
        public virtual ProcessOrder ProcessOrder { get; set; }
        public virtual PoColor PoColor { get; set; }
        public virtual CustomerPurchaseOrder CustomerPurchaseOrder { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public virtual string MOrderItemNo { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Description("颜色")]
        public virtual string Color { get; set; }
        /// <summary>
        /// 颜色描述
        /// </summary>
        [Description("颜色描述")]
        public virtual string ColorDescription { get; set; }
        /// <summary>
        /// 尺码合计
        /// </summary>
        [Description("尺码合计")]
        public virtual int? Total { get; set; }
    }
}

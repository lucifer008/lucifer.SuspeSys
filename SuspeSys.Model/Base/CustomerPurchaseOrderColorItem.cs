using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 客户外贸订单明细颜色
    /// </summary>
    [Serializable]
    public partial class CustomerPurchaseOrderColorItem : MetaData {
        public CustomerPurchaseOrderColorItem() { }
        public virtual string Id { get; set; }
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
        /// 数量
        /// </summary>
        [Description("数量")]
        public virtual string Total { get; set; }
    }
}

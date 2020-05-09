using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 加工单产品明细
    /// </summary>
    [Serializable]
    public partial class OrderProductItem : MetaData {
        public virtual string Id { get; set; }
        public virtual ProductOrder ProductOrder { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual string SequenceNumber { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Description("产品编号")]
        public virtual string ProductNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [Description("产品名称")]
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Description("颜色")]
        public virtual string Color { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string Rule { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        [Description("产品数量")]
        public virtual long? ProductNum { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Description("单位")]
        public virtual string ProductUnit { get; set; }
        /// <summary>
        /// 每箱数量
        /// </summary>
        [Description("每箱数量")]
        public virtual int? BoxNum { get; set; }
    }
}

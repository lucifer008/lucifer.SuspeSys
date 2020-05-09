using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 生产加工单
    /// </summary>
    [Serializable]
    public partial class ProductOrder : MetaData {
        public ProductOrder() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        [Description("订单名称")]
        public virtual string OrderName { get; set; }
        /// <summary>
        /// 包装类型
        ///   1：允许布匹混装
        ///   2:允许产品混装
        ///   3：允许超装
        ///   4：允许工单混装
        /// </summary>
        [Description("包装类型\r\n   1：允许布匹混装\r\n   2:允许产品混装\r\n   3：允许超装\r\n   4：允许工单混装")]
        public virtual double? OrderPackgeType { get; set; }
        /// <summary>
        /// 加工单类型
        ///   1:货单
        ///   2：样品单
        /// </summary>
        [Description("加工单类型\r\n   1:货单\r\n   2：样品单")]
        public virtual double? OrderType { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Description("版本号")]
        public virtual string VersionNo { get; set; }
        /// <summary>
        /// 制单日期
        /// </summary>
        [Description("制单日期")]
        public virtual DateTime? ProcessDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        [Description("制单人")]
        public virtual string ProcessPerson { get; set; }
        /// <summary>
        /// 系统单号
        /// </summary>
        [Description("系统单号")]
        public virtual string SystemOrderNo { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        [Description("客户单号")]
        public virtual string CustomerOrderNo { get; set; }
        /// <summary>
        /// 加工单号
        /// </summary>
        [Description("加工单号")]
        public virtual string ProcessOrderNo { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [Description("客户编号")]
        public virtual string CustomerNo { get; set; }
        /// <summary>
        /// 完成日期
        /// </summary>
        [Description("完成日期")]
        public virtual DateTime? SucessDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Remark { get; set; }
    }
}

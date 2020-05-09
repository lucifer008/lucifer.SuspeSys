using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制单
    /// </summary>
    [Serializable]
    public partial class ProcessOrder : MetaData {
        public ProcessOrder() { }
        public virtual string Id { get; set; }
        public virtual Style Style { get; set; }
        public virtual Customer Customer { get; set; }
        /// <summary>
        /// 制单号
        /// </summary>
        [Description("制单号")]
        public virtual string POrderNo { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual long? POrderNum { get; set; }
        /// <summary>
        /// 制单工序号
        /// </summary>
        [Description("制单工序号")]
        public virtual string MOrderNo { get; set; }
        /// <summary>
        /// 制单类型(1.散客;2.包装;3.无客户制单)
        /// </summary>
        [Description("制单类型(1.散客;2.包装;3.无客户制单)")]
        public virtual byte? POrderType { get; set; }
        /// <summary>
        /// 制单类型描述
        /// </summary>
        [Description("制单类型描述")]
        public virtual string POrderTypeDesption { get; set; }
        /// <summary>
        /// 生成通知单号
        /// </summary>
        [Description("生成通知单号")]
        public virtual string ProductNoticeOrderNo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Description("数量")]
        public virtual int? Num { get; set; }
        /// <summary>
        /// 制单状态
        ///   1:已审核
        ///   2:制作完成
        ///   3:生产中
        ///   4：完成
        /// </summary>
        [Description("制单状态\r\n   1:已审核\r\n   2:制作完成\r\n   3:生产中\r\n   4：完成")]
        public virtual byte? Status { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [Description("款号")]
        public virtual string StyleCode { get; set; }
        /// <summary>
        /// 款式描述
        /// </summary>
        [Description("款式描述")]
        public virtual string StyleName { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [Description("客户编号")]
        public virtual string CustomerNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Description("客户名称")]
        public virtual string CustomerName { get; set; }
        /// <summary>
        /// 客户款号
        /// </summary>
        [Description("客户款号")]
        public virtual string CustomerStyle { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Description("客户订单号")]
        public virtual string CustOrderNo { get; set; }
        /// <summary>
        /// 客户PO号
        /// </summary>
        [Description("客户PO号")]
        public virtual string CustPurchaseOrderNo { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        [Description("交货日期")]
        public virtual DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 下单日期
        /// </summary>
        [Description("下单日期")]
        public virtual DateTime? GenaterOrderDate { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Description("订单号")]
        public virtual string OrderNo { get; set; }
    }
}

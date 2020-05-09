using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 客户外贸单
    /// </summary>
    [Serializable]
    public partial class CustomerPurchaseOrder : MetaData {
        public CustomerPurchaseOrder() { }
        public virtual string Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Style Style { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [Description("客户编号")]
        public virtual string CusNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Description("客户名称")]
        public virtual string CusName { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual long? CusPurOrderNum { get; set; }
        /// <summary>
        /// PO编号(外贸订单号)
        /// </summary>
        [Description("PO编号(外贸订单号)")]
        public virtual string PurchaseOrderNo { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Description("客户订单号")]
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 下单日期
        /// </summary>
        [Description("下单日期")]
        public virtual DateTime? GeneratorDate { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        [Description("交货日期")]
        public virtual DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        [Description("发货地址")]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Description("公司地址")]
        public virtual string DeliverAddress { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Description("公司地址")]
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [Description("联系人")]
        public virtual string LinkMan { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        public virtual string Tel { get; set; }
    }
}

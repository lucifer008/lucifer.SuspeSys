using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 客户
    /// </summary>
    [Serializable]
    public partial class Customer : MetaData {
        public Customer() { }
        public virtual string Id { get; set; }
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
        /// PO编号(外贸订单号)
        /// </summary>
        [Description("PO编号(外贸订单号)")]
        public virtual string PurchaseOrderNo { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [Description("客户地址")]
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

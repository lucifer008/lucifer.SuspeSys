using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 生产加工单
    /// </summary>
    public class Order {
        public Order() { }
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public virtual string OrderName { get; set; }
        /// <summary>
        /// 包装类型
        ///   1：允许布匹混装
        ///   2:允许产品混装
        ///   3：允许超装
        ///   4：允许工单混装
        /// </summary>
        public virtual double? OrderPackgeType { get; set; }
        /// <summary>
        /// 加工单类型
        ///   1:货单
        ///   2：样品单
        /// </summary>
        public virtual double? OrderType { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public virtual string VersionNo { get; set; }
        /// <summary>
        /// 制单日期
        /// </summary>
        public virtual DateTime? ProcessDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public virtual string ProcessPerson { get; set; }
        /// <summary>
        /// 系统单号
        /// </summary>
        public virtual string SystemOrderNo { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public virtual string CustomerOrderNo { get; set; }
        /// <summary>
        /// 加工单号
        /// </summary>
        public virtual string ProcessOrderNo { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string CustomerNo { get; set; }
        /// <summary>
        /// 完成日期
        /// </summary>
        public virtual DateTime? SucessDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public virtual DateTime? InsertDateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateDateTime { get; set; }
        /// <summary>
        /// 插入用户
        /// </summary>
        public virtual string InsertUser { get; set; }
        /// <summary>
        /// 更新用户
        /// </summary>
        public virtual string UpdateUser { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public virtual byte? Deleted { get; set; }
    }
}

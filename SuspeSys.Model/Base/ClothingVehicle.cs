using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣车
    /// </summary>
    [Serializable]
    public partial class ClothingVehicle : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual ClothingVehicleType ClothingVehicleType { get; set; }
        /// <summary>
        /// 衣车编号
        /// </summary>
        [Description("衣车编号")]
        public virtual string No { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Description("卡号")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [Description("品牌")]
        public virtual string Brand { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [Description("型号")]
        public virtual string Model { get; set; }
        /// <summary>
        /// 出厂编号
        /// </summary>
        [Description("出厂编号")]
        public virtual string ProductsNo { get; set; }
        /// <summary>
        /// 购入日期
        /// </summary>
        [Description("购入日期")]
        public virtual DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// 所属车间
        /// </summary>
        [Description("所属车间")]
        public virtual string WorkShop { get; set; }
        /// <summary>
        /// 使用
        /// </summary>
        [Description("使用")]
        public virtual byte? IsUse { get; set; }
        /// <summary>
        /// 多点登录
        /// </summary>
        [Description("多点登录")]
        public virtual byte? IsMultLogin { get; set; }
        /// <summary>
        /// 报废日期
        /// </summary>
        [Description("报废日期")]
        public virtual DateTime? ScrapDate { get; set; }
        /// <summary>
        /// 报废原因
        /// </summary>
        [Description("报废原因")]
        public virtual string ScrapReason { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [Description("组别")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 站号
        /// </summary>
        [Description("站号")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [Description("登录时间")]
        public virtual DateTime? LoginTime { get; set; }
        /// <summary>
        /// 时长(秒)
        /// </summary>
        [Description("时长(秒)")]
        public virtual long? Times { get; set; }
    }
}

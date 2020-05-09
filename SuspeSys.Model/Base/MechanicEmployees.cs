using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 机修人员表
    /// </summary>
    [Serializable]
    public partial class MechanicEmployees : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [Description("工号")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public virtual string RealName { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Description("卡号")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        public virtual string WorkShop { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [Description("组别")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public virtual string Status { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        public virtual DateTime? InsertDateTime1 { get; set; }
    }
}

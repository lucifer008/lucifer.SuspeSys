using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣车登录日志
    /// </summary>
    [Serializable]
    public partial class SewingMachineLoginLog : MetaData {
        public virtual string Id { get; set; }

        /// <summary>
        /// 插入时间
        /// </summary>
        public virtual DateTime? InsertDateTime1 { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        public virtual string Workshop { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [Description("组别")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 主轨
        /// </summary>
        [Description("主轨")]
        public virtual string MainTrackNumber { get; set; }
        /// <summary>
        /// 站号
        /// </summary>
        [Description("站号")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 衣车号
        /// </summary>
        [Description("衣车号")]
        public virtual string SewingMachineNo { get; set; }
        /// <summary>
        /// 衣车卡号
        /// </summary>
        [Description("衣车卡号")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        [Description("打卡时间")]
        public virtual DateTime? ReadCardDateTime { get; set; }
        /// <summary>
        /// 登录状态(0:登录;1:登出)
        /// </summary>
        [Description("登录状态(0:登录;1:登出)")]
        public virtual short? LoginStatus { get; set; }
    }
}

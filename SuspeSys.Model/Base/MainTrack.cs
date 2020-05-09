using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 主轨
    /// </summary>
    [Serializable]
    public partial class MainTrack : MetaData {
        public MainTrack() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 组编号
        /// </summary>
        [Description("组编号")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// (0--255)
        /// </summary>
        [Description("(0--255)")]
        public virtual short? Num { get; set; }
        /// <summary>
        /// 运行状态(0:运行中;1:已停止)
        /// </summary>
        [Description("运行状态(0:运行中;1:已停止)")]
        public virtual byte? Status { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Description("启动时间")]
        public virtual DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 急停时间
        /// </summary>
        [Description("急停时间")]
        public virtual DateTime? EmergencyStopDateTime { get; set; }
        /// <summary>
        /// 停止时间
        /// </summary>
        [Description("停止时间")]
        public virtual DateTime? StopDateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

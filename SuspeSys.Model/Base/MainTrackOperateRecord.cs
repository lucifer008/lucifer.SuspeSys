using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 主轨操作记录
    /// </summary>
    [Serializable]
    public partial class MainTrackOperateRecord : MetaData {
        public virtual string Id { get; set; }
        public virtual MainTrack MainTrack { get; set; }
        /// <summary>
        /// 操作类型(0:启动;1：急停;2：停止)
        /// </summary>
        [Description("操作类型(0:启动;1：急停;2：停止)")]
        public virtual short? MType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

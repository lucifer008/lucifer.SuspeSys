using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣架返工请求队列
    /// </summary>
    [Serializable]
    public partial class HangerReworkRequestQueue : MetaData {
        public HangerReworkRequestQueue() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 衣架号
        /// </summary>
        [Description("衣架号")]
        public virtual string HangerNo { get; set; }
        /// <summary>
        /// 主轨号
        /// </summary>
        [Description("主轨号")]
        public virtual short? MainTrackNumber { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        [Description("站点编号")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 状态【0:收到的下位机的返工请求;1;正在返工;2:返工完成;-1:返工出错】
        /// </summary>
        [Description("状态【0:收到的下位机的返工请求;1;正在返工;2:返工完成;-1:返工出错】")]
        public virtual short? Status { get; set; }
    }
}

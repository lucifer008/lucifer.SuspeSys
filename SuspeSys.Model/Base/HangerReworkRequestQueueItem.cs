using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣架返工请求明细队列明细
    /// </summary>
    [Serializable]
    public partial class HangerReworkRequestQueueItem : MetaData {
        public virtual string Id { get; set; }
        public virtual HangerReworkRequestQueue HangerReworkRequestQueue { get; set; }
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
        /// 工序号
        /// </summary>
        [Description("工序号")]
        public virtual string FlowNo { get; set; }
        /// <summary>
        /// 返工工序代码
        /// </summary>
        [Description("返工工序代码")]
        public virtual string FlowCode { get; set; }
        /// <summary>
        /// 返工工序疵点代码
        /// </summary>
        [Description("返工工序疵点代码")]
        public virtual string DefectCode { get; set; }
    }
}

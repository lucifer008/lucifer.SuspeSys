using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工序制作站变更履历
    /// </summary>
    [Serializable]
    public partial class FlowStatingResume : MetaData {
        public virtual string Id { get; set; }
        public virtual Stating Stating { get; set; }
        public virtual ProcessFlowChartFlowRelation ProcessFlowChartFlowRelation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

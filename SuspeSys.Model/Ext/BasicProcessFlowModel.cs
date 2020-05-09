using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 基本工序库 扩展Model
    /// </summary>
    public partial class BasicProcessFlowModel : BasicProcessFlow {
        /// <summary>
        /// 工序段
        /// </summary>
        public virtual string ProSectionName { set; get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string SortField { set; get; }
        public virtual string SAM { set; get; }
        /// <summary>
        /// 基本工序Id或者款式基本工序Id
        /// </summary>
        public virtual string FlowId { set; get; }
        /// <summary>
        /// 工序来源：基本工序或者是款式工序
        /// </summary>
        public string FlowSourceGrid { get; set; }

        /// <summary>
        /// 标准工时（分）
        /// </summary>
        public decimal? StanardMinute { get; set; }

        new public  decimal? StanardHours { get; set; }
    }
}

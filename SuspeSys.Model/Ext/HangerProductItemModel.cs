using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace SuspeSys.Domain {
    
    /// <summary>
    /// 衣架生产明细 扩展Model
    /// </summary>
    [Serializable]
    public class HangerProductItemModel : HangerProductItem {
        /// <summary>
        /// 标准工时
        /// </summary>
        [Description("标准工时")]
        public string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        [Description("标准工价")]
        public decimal? StandardPrice { get; set; }


        /// <summary>
        /// 是否往前合并
        /// </summary>
        [Description("是否往前合并")]
        public bool IsMergeForward { get; set; }
        /// <summary>
        /// 合并工序制单工序Id
        /// </summary>
        public string MergeProcessFlowChartFlowRelationId { get; set; }
        public string ProcessFlowChartFlowRelationId { get; set; }
    }
}

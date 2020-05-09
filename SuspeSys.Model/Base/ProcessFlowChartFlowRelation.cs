using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工艺路线图制单工序
    /// </summary>
    [Serializable]
    public partial class ProcessFlowChartFlowRelation : MetaData {
        public ProcessFlowChartFlowRelation() { }
        public virtual string Id { get; set; }
        public virtual ProcessFlow ProcessFlow { get; set; }
        public virtual ProcessFlowChart ProcessFlowChart { get; set; }
        /// <summary>
        /// 顺序编号
        /// </summary>
        [Description("顺序编号")]
        public virtual string CraftFlowNo { get; set; }
        /// <summary>
        /// 生效
        /// </summary>
        [Description("生效")]
        public virtual short? IsEnabled { get; set; }
        /// <summary>
        /// 生效文本
        /// </summary>
        [Description("生效文本")]
        public virtual string EnabledText { get; set; }
        /// <summary>
        /// 是否往前合并
        /// </summary>
        [Description("是否往前合并")]
        public virtual bool? IsMergeForward { get; set; }
        /// <summary>
        /// 往前合并文本
        /// </summary>
        [Description("往前合并文本")]
        public virtual string MergeForwardText { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Description("工序号")]
        public virtual string FlowNo { get; set; }
        /// <summary>
        /// 工序代码
        /// </summary>
        [Description("工序代码")]
        public virtual string FlowCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Description("工序名称")]
        public virtual string FlowName { get; set; }
        /// <summary>
        /// 是否是产出工序
        /// </summary>
        [Description("是否是产出工序")]
        public virtual byte? IsProduceFlow { get; set; }
        /// <summary>
        /// 合并工序制单工序Id
        /// </summary>
        [Description("合并工序制单工序Id")]
        public virtual string MergeProcessFlowChartFlowRelationId { get; set; }
        /// <summary>
        /// 合并工序号
        /// </summary>
        [Description("合并工序号")]
        public virtual string MergeFlowNo { get; set; }
    }
}

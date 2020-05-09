using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工艺路线图:
    ///   产品在吊挂生产线的生产工序排序及员工任务分配的数据集合
    /// </summary>
    [Serializable]
    public partial class ProcessFlowChart : MetaData {
        public ProcessFlowChart() { }
        public virtual string Id { get; set; }
        public virtual ProcessFlowVersion ProcessFlowVersion { get; set; }
        /// <summary>
        /// 路线名称
        /// </summary>
        [Description("路线名称")]
        public virtual string LinkName { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual long? PFlowChartNum { get; set; }
        /// <summary>
        /// 产品部位
        /// </summary>
        [Description("产品部位")]
        public virtual string ProductPosition { get; set; }
        /// <summary>
        /// 目标产量
        /// </summary>
        [Description("目标产量")]
        public virtual int? TargetNum { get; set; }
        /// <summary>
        /// 产出工序
        /// </summary>
        [Description("产出工序")]
        public virtual string OutputProcessFlowId { get; set; }
        /// <summary>
        /// 挂片开始生产工序
        /// </summary>
        [Description("挂片开始生产工序")]
        public virtual string BoltProcessFlowId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Remark { get; set; }
    }
}

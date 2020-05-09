using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制单工序
    /// </summary>
    [Serializable]
    public partial class ProcessFlow : MetaData {
        public ProcessFlow() { }
        public virtual string Id { get; set; }
        public virtual ProcessFlowVersion ProcessFlowVersion { get; set; }
        public virtual BasicProcessFlow BasicProcessFlow { get; set; }
        /// <summary>
        /// 工序编号
        /// </summary>
        [Description("工序编号")]
        public virtual string ProcessNo { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Description("排序字段")]
        public virtual string ProcessOrderField { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Description("工序名称")]
        public virtual string ProcessName { get; set; }
        /// <summary>
        /// 工序代码
        /// </summary>
        [Description("工序代码")]
        public virtual string ProcessCode { get; set; }
        /// <summary>
        /// 工序状态
        ///   0:待走流程
        ///   1:流程已经完成
        ///   
        /// </summary>
        [Description("工序状态\r\n   0:待走流程\r\n   1:流程已经完成\r\n   ")]
        public virtual byte? ProcessStatus { get; set; }
        public virtual int? StanardSecond { get; set; }
        public virtual decimal? StanardMinute { get; set; }
        /// <summary>
        /// 标准工时
        /// </summary>
        [Description("标准工时")]
        public virtual decimal? StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        [Description("标准工价")]
        public virtual decimal? StandardPrice { get; set; }
        /// <summary>
        /// 默认工序号
        /// </summary>
        [Description("默认工序号")]
        public virtual int? DefaultFlowNo { get; set; }
        /// <summary>
        /// 工艺说明
        /// </summary>
        [Description("工艺说明")]
        public virtual string PrcocessRemark { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Description("颜色")]
        public virtual string ProcessColor { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        [Description("生效日期")]
        public virtual DateTime? EffectiveDate { get; set; }
    }
}

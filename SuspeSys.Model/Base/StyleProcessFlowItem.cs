using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 款式工序明细
    /// </summary>
    [Serializable]
    public partial class StyleProcessFlowItem : MetaData {
        public virtual string Id { get; set; }
        public virtual BasicProcessFlow BasicProcessFlow { get; set; }
        public virtual Style Style { get; set; }
        /// <summary>
        /// 版本编号
        /// </summary>
        [Description("版本编号")]
        public virtual string ProVersionNo { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        [Description("版本名称")]
        public virtual string ProcessVersionName { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Description("工序号")]
        public virtual string FlowNo { get; set; }
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
        /// <summary>
        /// SAM
        /// </summary>
        [Description("SAM")]
        public virtual string Sam { get; set; }
        /// <summary>
        /// 标准工时
        /// </summary>
        [Description("标准工时")]
        public virtual string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        [Description("标准工价")]
        public virtual decimal? StandardPrice { get; set; }
    }
}

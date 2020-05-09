using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 基本工序库
    /// </summary>
    [Serializable]
    public partial class BasicProcessFlow : MetaData {
        public BasicProcessFlow() { }
        public virtual string Id { get; set; }
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
        /// 工序名称
        /// </summary>
        [Description("工序名称")]
        public virtual string ProcessName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Description("排序号")]
        public virtual string SortNo { get; set; }
        public virtual int? StanardSecond { get; set; }
        /// <summary>
        /// SAM
        /// </summary>
        [Description("SAM")]
        public virtual string Sam { get; set; }
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
        /// 工艺说明
        /// </summary>
        [Description("工艺说明")]
        public virtual string PrcocessRmark { get; set; }
        /// <summary>
        /// 默认工序号
        /// </summary>
        [Description("默认工序号")]
        public virtual int? DefaultFlowNo { get; set; }
    }
}

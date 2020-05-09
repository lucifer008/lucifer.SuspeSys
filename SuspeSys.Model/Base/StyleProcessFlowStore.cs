using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    
    /// <summary>
    /// 款式工序库
    /// </summary>
    public class StyleProcessFlowStore : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual ProcessFlowSection ProcessFlowSection { get; set; }
        /// <summary>
        /// 基本工序库_Id
        /// </summary>
        public virtual string BasicprocessflowId { get; set; }
        /// <summary>
        /// 工序段序号
        /// </summary>
        public virtual string ProSectionNo { get; set; }
        /// <summary>
        /// 工序段名称
        /// </summary>
        public virtual string ProSectionName { get; set; }
        /// <summary>
        /// 工序段代码
        /// </summary>
        public virtual string ProSectionCode { get; set; }
        /// <summary>
        /// 工序状态
        ///   0:待走流程
        ///   1:流程已经完成
        ///   
        /// </summary>
        public virtual byte? ProcessStatus { get; set; }
        /// <summary>
        /// 标准工时
        /// </summary>
        public virtual string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        public virtual decimal? StandardPrice { get; set; }
        /// <summary>
        /// 工艺说明
        /// </summary>
        public virtual string PrcocessRemark { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public virtual string ProcessColor { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public virtual string Size { get; set; }
        public virtual string Discriminator { get; set; }
    }
}

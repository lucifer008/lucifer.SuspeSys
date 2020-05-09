using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制单工序版本
    /// </summary>
    [Serializable]
    public partial class ProcessFlowVersion : MetaData {
        public ProcessFlowVersion() { }
        public virtual string Id { get; set; }
        public virtual ProcessOrder ProcessOrder { get; set; }
        /// <summary>
        /// 版本序号
        /// </summary>
        [Description("版本序号")]
        public virtual string ProVersionNum { get; set; }
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
        /// 生效日期
        /// </summary>
        [Description("生效日期")]
        public virtual DateTime? EffectiveDate { get; set; }
        /// <summary>
        /// 总工价
        /// </summary>
        [Description("总工价")]
        public virtual string TotalStandardPrice { get; set; }
        /// <summary>
        /// 总SAM
        /// </summary>
        [Description("总SAM")]
        public virtual string TotalSam { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        [Description("插入时间")]
        public virtual DateTime? InsertDate { get; set; }
    }
}

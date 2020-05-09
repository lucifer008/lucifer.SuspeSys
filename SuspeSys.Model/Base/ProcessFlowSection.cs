using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工序段
    /// </summary>
    [Serializable]
    public partial class ProcessFlowSection : MetaData {
        public ProcessFlowSection() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 工序段序号
        /// </summary>
        [Description("工序段序号")]
        public virtual string ProSectionNo { get; set; }
        /// <summary>
        /// 工序段名称
        /// </summary>
        [Description("工序段名称")]
        public virtual string ProSectionName { get; set; }
        /// <summary>
        /// 工序段代码
        /// </summary>
        [Description("工序段代码")]
        public virtual string ProSectionCode { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工艺图制作动作
    /// </summary>
    [Serializable]
    public partial class ProcessCraftAction : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 动作编号
        /// </summary>
        [Description("动作编号")]
        public virtual string ActionNo { get; set; }
        /// <summary>
        /// 动作描述
        /// </summary>
        [Description("动作描述")]
        public virtual string ActionDesc { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        [Description("是否生效")]
        public virtual byte? IsEnabled { get; set; }
        /// <summary>
        /// 工序段序号
        /// </summary>
        [Description("工序段序号")]
        public virtual string ProSectionNo { get; set; }
        /// <summary>
        /// 工序段代码
        /// </summary>
        [Description("工序段代码")]
        public virtual string ProSectionCode { get; set; }
        /// <summary>
        /// 工序段名称
        /// </summary>
        [Description("工序段名称")]
        public virtual string ProSectionName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Remark2 { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class FlowAction : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 动作编号
        /// </summary>
        [Description("动作编号")]
        public virtual string ActionCode { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        [Description("动作名称")]
        public virtual string ActionName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public virtual byte? IsEnabled { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工艺路线图使用组
    /// </summary>
    [Serializable]
    public partial class ProcessFlowChartGrop : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
        public virtual ProcessFlowChart ProcessFlowChart { get; set; }
        /// <summary>
        /// 组编号
        /// </summary>
        [Description("组编号")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [Description("组名称")]
        public virtual string GroupName { get; set; }
    }
}

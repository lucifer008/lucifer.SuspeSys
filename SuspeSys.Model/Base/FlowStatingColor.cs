using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工艺工序制作站点指定的颜色
    /// </summary>
    [Serializable]
    public partial class FlowStatingColor : MetaData {
        public virtual string Id { get; set; }
        public virtual ProcessFlowStatingItem ProcessFlowStatingItem { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        [Description("站点名称")]
        public virtual string StatingName { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        [Description("站点编号")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Description("角色")]
        public virtual string ColorValue { get; set; }
        /// <summary>
        /// 尺码描述
        /// </summary>
        [Description("尺码描述")]
        public virtual string ColorDesption { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 菜单 模块表
    /// </summary>
    [Serializable]
    public partial class Modules : MetaData {
        public Modules() { }
        public virtual string Id { get; set; }
        public virtual Modules ModulesVal { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        [Description("动作名称")]
        public virtual string ActionName { get; set; }
        /// <summary>
        /// 动作Key
        /// </summary>
        [Description("动作Key")]
        public virtual string ActionKey { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public virtual string Description { get; set; }
        /// <summary>
        /// 1:菜单
        ///   2:按钮
        /// </summary>
        [Description("1:菜单\r\n   2:按钮")]
        public virtual int ModulesType { get; set; }
        public virtual int ModuleLevel { get; set; }
        public virtual decimal OrderField { get; set; }
    }
}

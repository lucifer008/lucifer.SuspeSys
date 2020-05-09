using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 系统参数
    /// </summary>
    [Serializable]
    public partial class SystemModuleParameter : MetaData {
        public SystemModuleParameter() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 父级模块参数Id
        /// </summary>
        [Description("父级模块参数Id")]
        public virtual string SystemModuleParameterId { get; set; }
        /// <summary>
        /// 参数编号
        /// </summary>
        [Description("参数编号")]
        public virtual string SysNo { get; set; }
        /// <summary>
        /// 参数类型(0:用户参数;1:客户机参数:2：吊挂线;3:系统)
        /// </summary>
        [Description("参数类型(0:用户参数;1:客户机参数:2：吊挂线;3:系统)")]
        public virtual short? ModuleType { get; set; }
        /// <summary>
        /// 参数Key
        /// </summary>
        [Description("参数Key")]
        public virtual string ModuleText { get; set; }
        /// <summary>
        /// 二级模块参数类型
        /// </summary>
        [Description("二级模块参数类型")]
        public virtual short? SecondModuleType { get; set; }
        /// <summary>
        /// 二级模块参数描述
        /// </summary>
        [Description("二级模块参数描述")]
        public virtual string SecondModuleText { get; set; }
        /// <summary>
        /// 参数Value
        /// </summary>
        [Description("参数Value")]
        public virtual string ParamterKey { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        [Description("默认值")]
        public virtual short? ParamterValue { get; set; }
        /// <summary>
        /// 参数控件类型(0：Checkbox;,1:Text;2:其他)
        /// </summary>
        [Description("参数控件类型(0：Checkbox;,1:Text;2:其他)")]
        public virtual string ParamterControlType { get; set; }
        /// <summary>
        /// 参数控件标题
        /// </summary>
        [Description("参数控件标题")]
        public virtual string ParamterControlTitle { get; set; }
        /// <summary>
        /// 参数控件描述
        /// </summary>
        [Description("参数控件描述")]
        public virtual string ParamterControlDescribe { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo2 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo3 { get; set; }
    }
}

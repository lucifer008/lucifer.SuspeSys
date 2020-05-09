using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工厂
    /// </summary>
    [Serializable]
    public partial class Factory : MetaData {
        public Factory() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 工厂编号
        /// </summary>
        [Description("工厂编号")]
        public virtual string FacCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        [Description("工厂名称")]
        public virtual string FacName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

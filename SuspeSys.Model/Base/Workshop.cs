using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 车间
    /// </summary>
    [Serializable]
    public partial class Workshop : MetaData {
        public Workshop() { }
        public virtual string Id { get; set; }
        public virtual Factory Factory { get; set; }
        /// <summary>
        /// 车间编号
        /// </summary>
        [Description("车间编号")]
        public virtual string WorCode { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        [Description("车间名称")]
        public virtual string WorName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

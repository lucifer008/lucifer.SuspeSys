using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣架
    /// </summary>
    [Serializable]
    public partial class Hanger : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 衣架号
        /// </summary>
        [Description("衣架号")]
        public virtual long? HangerNo { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        [Description("注册日期")]
        public virtual DateTime? RegisterDate { get; set; }
    }
}

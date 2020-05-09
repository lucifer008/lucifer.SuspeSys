using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 换卡履历
    /// </summary>
    [Serializable]
    public partial class ChangeCardResume : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Description("卡号")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 换卡原因
        /// </summary>
        [Description("换卡原因")]
        public virtual string ChangeCardReason { get; set; }
    }
}

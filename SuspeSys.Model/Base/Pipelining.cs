using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 流水线
    /// </summary>
    [Serializable]
    public partial class Pipelining : MetaData {
        public Pipelining() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
        public virtual ProdType ProdType { get; set; }
        /// <summary>
        /// 流水线号
        /// </summary>
        [Description("流水线号")]
        public virtual string PipeliNo { get; set; }
        /// <summary>
        /// 推杆数量
        /// </summary>
        [Description("推杆数量")]
        public virtual int? PushRodNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

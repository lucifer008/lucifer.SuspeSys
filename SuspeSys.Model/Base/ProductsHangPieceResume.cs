using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制品挂片履历
    /// </summary>
    [Serializable]
    public partial class ProductsHangPieceResume : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual Products Products { get; set; }
        /// <summary>
        /// 挂片站编号
        /// </summary>
        [Description("挂片站编号")]
        public virtual string HangPieceNo { get; set; }
        /// <summary>
        /// 挂片站名称
        /// </summary>
        [Description("挂片站名称")]
        public virtual string HangName { get; set; }
    }
}

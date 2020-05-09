using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 疵点代码表
    /// </summary>
    [Serializable]
    public partial class DefectCodeTable : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public virtual string DefectNo { get; set; }
        /// <summary>
        /// 疵点代码
        /// </summary>
        [Description("疵点代码")]
        public virtual string DefectCode { get; set; }
        /// <summary>
        /// 疵点名称
        /// </summary>
        [Description("疵点名称")]
        public virtual string DefectName { get; set; }
    }
}

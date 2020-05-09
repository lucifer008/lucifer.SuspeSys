using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 缺料代码表
    /// </summary>
    [Serializable]
    public partial class LackMaterialsTable : MetaData {
        public virtual string Id { get; set; }

        /// <summary>
        /// 缺料序号
        /// </summary>
        [Description("缺料序号")]
        public virtual int LackMaterialsNo { get; set; }
        /// <summary>
        /// 缺料代码
        /// </summary>
        [Description("缺料代码")]
        public virtual string LackMaterialsCode { get; set; }
        /// <summary>
        /// 缺料名称
        /// </summary>
        [Description("缺料名称")]
        public virtual string LackMaterialsName { get; set; }
    }
}

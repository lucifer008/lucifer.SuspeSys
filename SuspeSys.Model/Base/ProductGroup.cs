using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 生产组别
    /// </summary>
    [Serializable]
    public partial class ProductGroup : MetaData {
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual string ProductGroupCode { get; set; }
        public virtual string ProductGroupName { get; set; }
    }
}

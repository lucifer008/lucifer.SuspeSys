using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 产线类别
    /// </summary>
    [Serializable]
    public partial class ProdType : MetaData {
        public ProdType() { }
        public virtual string Id { get; set; }
        public virtual string PorTypeCode { get; set; }
        public virtual string PorTypeName { get; set; }
        public virtual string Memo { get; set; }
    }
}

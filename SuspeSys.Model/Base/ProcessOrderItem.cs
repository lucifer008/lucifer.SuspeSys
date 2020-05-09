using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class ProcessOrderItem : MetaData {
        public virtual string Id { get; set; }
        public virtual string Discriminator { get; set; }
        public virtual string ProcessorderId { get; set; }
        public virtual string MOrderItemNo { get; set; }
        public virtual string Color { get; set; }
        public virtual string ColorDescription { get; set; }
        public virtual int? Xxxl { get; set; }
        public virtual int? M { get; set; }
        public virtual int? Xll { get; set; }
        public virtual int? Total { get; set; }
        public virtual byte? Type { get; set; }
    }
}

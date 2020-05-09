using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class ProcessOrderPurchaseOrderItem : MetaData {
        public virtual string Id { get; set; }
        public virtual string ProcessorderId { get; set; }
        public virtual string CustomerpurchaseorderId { get; set; }
        public virtual string Memo { get; set; }
    }
}

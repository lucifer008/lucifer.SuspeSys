using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class ProcessFlowSectionItem : MetaData {
        public virtual string Id { get; set; }
        public virtual string BasicprocessflowId { get; set; }
        public virtual string ProcessflowsectionId { get; set; }
        public virtual string Memo { get; set; }
    }
}

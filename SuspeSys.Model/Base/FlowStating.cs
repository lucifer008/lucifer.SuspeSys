using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class FlowStating : MetaData {
        public virtual string Id { get; set; }
        public virtual string StatingId { get; set; }
        public virtual string ProcessflowchartflowrelationId { get; set; }
        public virtual string StatingName { get; set; }
        public virtual string StatingNo { get; set; }
        public virtual string Role { get; set; }
    }
}

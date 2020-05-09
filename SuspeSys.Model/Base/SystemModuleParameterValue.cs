using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class SystemModuleParameterValue : MetaData {
        public virtual string Id { get; set; }
        public virtual SystemModuleParameter SystemModuleParameter { get; set; }
        public virtual string ParameterValue { get; set; }
        public virtual string ProductLineId { get; set; }
        public virtual string ParameterDomainCode { get; set; }
    }
}

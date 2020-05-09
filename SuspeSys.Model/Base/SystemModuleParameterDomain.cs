using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class SystemModuleParameterDomain : MetaData {
        public virtual string Id { get; set; }
        public virtual SystemModuleParameter SystemModuleParameter { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Enable { get; set; }
        public virtual string Memo { get; set; }
    }
}

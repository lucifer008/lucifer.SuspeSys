using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class PipeliningSiteGroupRelation : MetaData {
        public virtual string Id { get; set; }
        public virtual string SitegroupId { get; set; }
        public virtual string PipeliningId { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class ClientMachines : MetaData {
        public virtual string Id { get; set; }
        public virtual string ClientMachineName { get; set; }
        public virtual string AuthorizationInformation { get; set; }
        public virtual string Description { get; set; }
        public virtual short ClientMachineType { get; set; }
    }
}

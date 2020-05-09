using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class EmployeeRoleRelation : MetaData {
        public virtual string Id { get; set; }
        public virtual string RolesId { get; set; }
        public virtual string EmployeeId { get; set; }
    }
}

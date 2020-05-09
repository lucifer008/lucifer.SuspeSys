using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 员工职务
    /// </summary>
    [Serializable]
    public partial class EmployeePositions : MetaData {
        public virtual string Id { get; set; }
        public virtual Position Position { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

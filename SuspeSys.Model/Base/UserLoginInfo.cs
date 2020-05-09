using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class UserLoginInfo : MetaData {
        public virtual string SessionId { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string EmployeeId { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual DateTime LoginDate { get; set; }
        public virtual DateTime? LoginOutDate { get; set; }
    }
}

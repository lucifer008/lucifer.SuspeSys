using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class Users : MetaData {
        public Users() { }
        public virtual string Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string CardNo { get; set; }
    }
}

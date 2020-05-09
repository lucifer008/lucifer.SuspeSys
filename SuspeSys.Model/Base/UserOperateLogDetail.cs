using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class UserOperateLogDetail : MetaData {
        public virtual string Id { get; set; }
        public virtual UserOperateLogs UserOperateLogs { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string FieldCode { get; set; }
        public virtual string BeforeChange { get; set; }
        public virtual string Changed { get; set; }
    }
}

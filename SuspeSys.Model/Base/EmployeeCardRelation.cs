using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 员工卡关系
    /// </summary>
    [Serializable]
    public partial class EmployeeCardRelation : MetaData {
        public virtual string Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CardInfo CardInfo { get; set; }
    }
}

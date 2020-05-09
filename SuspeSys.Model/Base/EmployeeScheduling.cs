using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 员工排班信息
    /// </summary>
    [Serializable]
    public partial class EmployeeScheduling : MetaData {
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
    }
}

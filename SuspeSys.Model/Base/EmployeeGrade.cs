using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 员工等级
    /// </summary>
    [Serializable]
    public partial class EmployeeGrade : MetaData {
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual string GradeCode { get; set; }
        public virtual string GradeName { get; set; }
    }
}

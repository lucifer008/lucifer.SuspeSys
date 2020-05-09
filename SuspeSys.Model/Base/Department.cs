using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 部门
    /// </summary>
    [Serializable]
    public partial class Department : MetaData {
        public Department() { }
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [Description("部门编号")]
        public virtual string DepNo { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Description("部门名称")]
        public virtual string DepName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

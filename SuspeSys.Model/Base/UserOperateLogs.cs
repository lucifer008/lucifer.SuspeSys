using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 用户操作日志
    /// </summary>
    [Serializable]
    public partial class UserOperateLogs : MetaData {
        public UserOperateLogs() { }
        public virtual string Id { get; set; }
        public virtual string OpFormName { get; set; }
        public virtual string OpFormCode { get; set; }
        public virtual string OpTableName { get; set; }
        public virtual string OpTableCode { get; set; }
        public virtual string OpDataCode { get; set; }
        /// <summary>
        /// 1: insert
        ///   2: delete
        ///   3: select
        ///   4: update
        /// </summary>
        [Description("1: insert\r\n   2: delete\r\n   3: select\r\n   4: update")]
        public virtual int OpType { get; set; }
    }
}

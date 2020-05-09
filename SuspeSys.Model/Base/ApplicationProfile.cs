using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 系统初始化信息
    /// </summary>
    [Serializable]
    public partial class ApplicationProfile : MetaData {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ParaValue { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string Memo { get; set; }
    }
}

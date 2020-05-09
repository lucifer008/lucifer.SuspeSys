using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 号码段的分类
    ///   @needMeta=false
    /// </summary>
    [Serializable]
    public partial class IdGenerator : MetaData {
        /// <summary>
        /// @generateColumnConfig=sequence
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 标示符
        /// </summary>
        [Description("标示符")]
        public virtual string FlagNo { get; set; }
        /// <summary>
        /// 开始值
        /// </summary>
        [Description("开始值")]
        public virtual long BeginValue { get; set; }
        /// <summary>
        /// 目前值
        /// </summary>
        [Description("目前值")]
        public virtual long CurrentValue { get; set; }
        /// <summary>
        /// 结束值
        /// </summary>
        [Description("结束值")]
        public virtual long EndValue { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public virtual long? SortValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

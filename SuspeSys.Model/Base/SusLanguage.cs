using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 语言
    /// </summary>
    [Serializable]
    public partial class SusLanguage : MetaData {
        public SusLanguage() { }
        public virtual string Id { get; set; }
        /// <summary>
        /// 语言简称
        /// </summary>
        [Description("语言简称")]
        public virtual string LanguageKey { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        [Description("语言")]
        public virtual string LanguageValue { get; set; }
    }
}

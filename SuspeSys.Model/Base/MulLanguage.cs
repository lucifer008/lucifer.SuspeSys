using SuspeSys.Domain.SusAttr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Base
{
    [Serializable]
    public partial class MulLanguage : MetaData
    
    {
        [ColumnAttribute("ResKey")]
        public virtual string ResKey { set; get; }
        public virtual int? ResType { set; get; }
        public virtual string ResItem { set; get; }
        public virtual string SimplifiedChinese { set; get; }
        public virtual string TraditionalChinese { set; get; }
        public virtual string English { set; get; }
        /// <summary>
        ///  柬埔寨
        /// </summary>
        public virtual string Cambodia { set; get; }
        /// <summary>
        /// 越南
        /// </summary>
        public virtual string Vietnamese { set; get; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class Province : MetaData {
        public Province() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 省编号
        /// </summary>
        [Description("省编号")]
        public virtual string ProvinceCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public virtual string ProvinceName { get; set; }
    }
}

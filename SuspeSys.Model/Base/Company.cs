using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class Company : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Description("公司编号")]
        public virtual string CompanyCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [Description("公司名称")]
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Description("公司地址")]
        public virtual string Address { get; set; }
    }
}

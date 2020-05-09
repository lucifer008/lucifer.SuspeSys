using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 系统参数 扩展Model
    /// </summary>
    [Serializable]
    public class SystemModuleParameterModel : SystemModuleParameter {

        /// <summary>
        /// 吊挂线参数
        /// </summary>
        public IEnumerable<SystemModuleParameterValue> SystemModuleParameterValueList { get; set; }

        /// <summary>
        /// 下拉列表
        /// </summary>
        public IEnumerable<SystemModuleParameterDomain> SystemModuleParameterDomainList { get; set; }
    }
}

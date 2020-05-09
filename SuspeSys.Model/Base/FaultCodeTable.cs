using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 故障代码表
    /// </summary>
    [Serializable]
    public partial class FaultCodeTable : MetaData {
        public virtual string Id { get; set; }
        public virtual ClothingVehicleType ClothingVehicleType { get; set; }
        /// <summary>
        /// 故障序号
        /// </summary>
        [Description("故障序号")]
        public virtual string SerialNumber { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        [Description("故障代码")]
        public virtual string FaultCode { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        [Description("故障名称")]
        public virtual string FaultName { get; set; }
    }
}

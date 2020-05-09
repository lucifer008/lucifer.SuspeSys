using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣车类别
    /// </summary>
    [Serializable]
    public partial class ClothingVehicleType : MetaData {
        public ClothingVehicleType() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 类别代码
        /// </summary>
        [Description("类别代码")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [Description("类别名称")]
        public virtual string Name { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 站点组:
    ///   为客户企业管理中的行政划分单位，与吊挂生产线可以是一对一关系，也可以是一对多，多对多的关系，视实际情况安装
    /// </summary>
    [Serializable]
    public partial class SiteGroup : MetaData {
        public SiteGroup() { }
        public virtual string Id { get; set; }
        public virtual Workshop Workshop { get; set; }
        /// <summary>
        /// 组编号
        /// </summary>
        [Description("组编号")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [Description("组名称")]
        public virtual string GroupName { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        [Description("工厂")]
        public virtual string FactoryCode { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        public virtual string WorkshopCode { get; set; }
        /// <summary>
        /// 一个组对应一个主轨号(范围1--255)
        /// </summary>
        [Description("一个组对应一个主轨号(范围1--255)")]
        public virtual short? MainTrackNumber { get; set; }
    }
}

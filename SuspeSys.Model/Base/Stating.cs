using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 站点:
    ///   即一个包括进站、出站功能，有终端显示操作面板的集合，一般安装一名生产员工及一台衣车
    /// </summary>
    [Serializable]
    public partial class Stating : MetaData {
        public Stating() { }
        public virtual string Id { get; set; }
        public virtual SusLanguage SusLanguage { get; set; }
        public virtual StatingRoles StatingRoles { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
        public virtual StatingDirection StatingDirection { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        [Description("站点名称")]
        public virtual string StatingName { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        [Description("站点编号")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 站点语言
        /// </summary>
        [Description("站点语言")]
        public virtual string Language { get; set; }
        /// <summary>
        /// 一个组对应一个主轨号(范围1--255)
        /// </summary>
        [Description("一个组对应一个主轨号(范围1--255)")]
        public virtual short? MainTrackNumber { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        [Description("容量")]
        public virtual int? Capacity { get; set; }
        /// <summary>
        /// 是否接收衣架
        /// </summary>
        [Description("是否接收衣架")]
        public virtual bool? IsReceivingHanger { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Description("角色")]
        public virtual string ColorValue { get; set; }
        /// <summary>
        /// 负载监(:0正常，1:不可用)
        /// </summary>
        [Description("负载监(:0正常，1:不可用)")]
        public virtual bool? IsLoadMonitor { get; set; }
        /// <summary>
        /// 链式提升
        /// </summary>
        [Description("链式提升")]
        public virtual bool? IsChainHoist { get; set; }
        /// <summary>
        /// 提升行程缓存满
        /// </summary>
        [Description("提升行程缓存满")]
        public virtual bool? IsPromoteTripCachingFull { get; set; }
        /// <summary>
        /// 站点条码
        /// </summary>
        [Description("站点条码")]
        public virtual string SiteBarCode { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Description("是否可用")]
        public virtual bool? IsEnabled { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        [Description("方向")]
        public virtual short? Direction { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
        /// <summary>
        /// 主板版本号
        /// </summary>
        [Description("主板版本号")]
        public virtual string MainboardNumber { get; set; }
        /// <summary>
        /// SN号
        /// </summary>
        [Description("SN号")]
        public virtual string SerialNumber { get; set; }

        /// <summary>
        /// 故障信息
        /// </summary>
        [Description("故障信息")]
        public virtual string FaultInfo { get; set; }
    }
}

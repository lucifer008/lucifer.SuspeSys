using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 工序制作站点明细
    /// </summary>
    [Serializable]
    public partial class ProcessFlowStatingItem : MetaData {
        public ProcessFlowStatingItem() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual ProcessFlowChartFlowRelation ProcessFlowChartFlowRelation { get; set; }
        public virtual Stating Stating { get; set; }
        /// <summary>
        /// 是否接收衣架
        /// </summary>
        [Description("是否接收衣架")]
        public virtual byte? IsReceivingHanger { get; set; }
        /// <summary>
        /// 接收内容(默认是衣架)
        /// </summary>
        [Description("接收内容(默认是衣架)")]
        public virtual string ReceingContent { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        [Description("组号")]
        public virtual string SiteGroupNo { get; set; }
        /// <summary>
        /// 主轨号
        /// </summary>
        [Description("主轨号")]
        public virtual int? MainTrackNumber { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        [Description("顺序")]
        public virtual string No { get; set; }
        /// <summary>
        /// 站点角色(备用查询字段)
        /// </summary>
        [Description("站点角色(备用查询字段)")]
        public virtual string StatingRoleName { get; set; }

        /// <summary>
        /// 站点角色代码
        /// 【非共享角色】车缝站，存储站，多功能站，返工站，收尾站
        /// </summary>
        [Description("站点角色代码")]
        public virtual string StatingRoleCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
        /// <summary>
        /// 是否全部接收尺码
        /// </summary>
        [Description("是否全部接收尺码")]
        public virtual bool? IsReceivingAllSize { get; set; }
        /// <summary>
        /// 是否全部接收颜色
        /// </summary>
        [Description("是否全部接收颜色")]
        public virtual bool? IsReceivingAllColor { get; set; }
        /// <summary>
        /// 是否全部接收PO号码
        /// </summary>
        [Description("是否全部接收PO号码")]
        public virtual bool? IsReceivingAllPoNumber { get; set; }
        /// <summary>
        /// 是否是收尾站
        /// </summary>
        [Description("是否是收尾站")]
        public virtual bool? IsEndStating { get; set; }
        /// <summary>
        /// 接收比例
        /// </summary>
        [Description("接收比例")]
        public virtual decimal? Proportion { get; set; }
        /// <summary>
        /// 接收颜色
        /// </summary>
        [Description("接收颜色")]
        public virtual string ReceivingColor { get; set; }
        /// <summary>
        /// 接收尺码
        /// </summary>
        [Description("接收尺码")]
        public virtual string ReceivingSize { get; set; }
        /// <summary>
        /// 接收PO号码
        /// </summary>
        [Description("接收PO号码")]
        public virtual string ReceivingPoNumber { get; set; }
    }
}

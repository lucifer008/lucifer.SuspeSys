using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 站点衣架生产明细
    /// </summary>
    [Serializable]
    public partial class StatingHangerProductItem : MetaData {
        public virtual string Id { get; set; }
        /// <summary>
        /// 生产组编号
        /// </summary>
        [Description("生产组编号")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 一个组对应一个主轨号(范围1--255)
        /// </summary>
        [Description("一个组对应一个主轨号(范围1--255)")]
        public virtual short? MainTrackNumber { get; set; }
        /// <summary>
        /// 制品Id
        /// </summary>
        [Description("制品Id")]
        public virtual string ProductsId { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Description("批次")]
        public virtual long? BatchNo { get; set; }
        /// <summary>
        /// 请求出站站号
        /// </summary>
        [Description("请求出站站号")]
        public virtual string SiteNo { get; set; }
        /// <summary>
        /// 衣架号
        /// </summary>
        [Description("衣架号")]
        public virtual string HangerNo { get; set; }
        /// <summary>
        /// 制单Id
        /// </summary>
        [Description("制单Id")]
        public virtual string ProcessOrderId { get; set; }
        /// <summary>
        /// 制单号
        /// </summary>
        [Description("制单号")]
        public virtual string ProcessOrderNo { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Description("颜色")]
        public virtual string PColor { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string PSize { get; set; }
        /// <summary>
        /// 工艺路线图Id
        /// </summary>
        [Description("工艺路线图Id")]
        public virtual string FlowChartd { get; set; }
        /// <summary>
        /// 工艺图名称
        /// </summary>
        [Description("工艺图名称")]
        public virtual string LineName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Description("数量")]
        public virtual int? SizeNum { get; set; }
        /// <summary>
        /// 制单工序Id
        /// </summary>
        [Description("制单工序Id")]
        public virtual string ProcessFlowId { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Description("工序号")]
        public virtual string FlowNo { get; set; }
        /// <summary>
        /// 工序代码
        /// </summary>
        [Description("工序代码")]
        public virtual string ProcessFlowCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Description("工序名称")]
        public virtual string ProcessFlowName { get; set; }
        /// <summary>
        /// 工序索引
        /// </summary>
        [Description("工序索引")]
        public virtual short? FlowIndex { get; set; }
        /// <summary>
        /// 工序是否完成
        /// </summary>
        [Description("工序是否完成")]
        public virtual bool? IsSucessedFlow { get; set; }
        /// <summary>
        /// 站Id
        /// </summary>
        [Description("站Id")]
        public virtual string SiteId { get; set; }
        /// <summary>
        /// 路线图是否改变
        /// </summary>
        [Description("路线图是否改变")]
        public virtual bool? IsFlowChatChange { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
        /// <summary>
        /// 是否进站
        /// </summary>
        [Description("是否进站")]
        public virtual bool? IsIncomeSite { get; set; }
        /// <summary>
        /// 是否是返工工序
        /// </summary>
        [Description("是否是返工工序")]
        public virtual bool? IsReturnWorkFlow { get; set; }
        /// <summary>
        /// 是否是返工发起站点
        /// </summary>
        [Description("是否是返工发起站点")]
        public virtual bool? IsReworkSourceStating { get; set; }
        /// <summary>
        /// 客户机Id
        /// </summary>
        [Description("客户机Id")]
        public virtual string ClientMachineId { get; set; }
        /// <summary>
        /// 吊挂线Id
        /// </summary>
        [Description("吊挂线Id")]
        public virtual string SusLineId { get; set; }
        /// <summary>
        /// 进站时间
        /// </summary>
        [Description("进站时间")]
        public virtual DateTime? IncomeSiteDate { get; set; }
        /// <summary>
        /// 出战时间
        /// </summary>
        [Description("出战时间")]
        public virtual DateTime? OutSiteDate { get; set; }
        /// <summary>
        /// 比较时间
        /// </summary>
        [Description("比较时间")]
        public virtual DateTime? CompareDate { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 制品
    /// </summary>
    [Serializable]
    public partial class Products : MetaData {
        public Products() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual ProcessFlowChart ProcessFlowChart { get; set; }
        public virtual ProcessOrder ProcessOrder { get; set; }
        /// <summary>
        /// 生产组编号
        /// </summary>
        [Description("生产组编号")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 排产号
        /// </summary>
        [Description("排产号")]
        public virtual int? ProductionNumber { get; set; }
        /// <summary>
        /// 上线日期
        /// </summary>
        [Description("上线日期")]
        public virtual DateTime? ImplementDate { get; set; }
        /// <summary>
        /// 挂片站点
        /// </summary>
        [Description("挂片站点")]
        public virtual string HangingPieceSiteNo { get; set; }
        /// <summary>
        /// 制单号
        /// </summary>
        [Description("制单号")]
        public virtual string ProcessOrderNo { get; set; }
        /// <summary>
        /// 状态:0:未分配;1:已分配;3.上线;4.已完成
        /// </summary>
        [Description("状态:0:未分配;1:已分配;3.上线;4.已完成")]
        public virtual byte? Status { get; set; }
        /// <summary>
        /// 客户外贸单Id
        /// </summary>
        [Description("客户外贸单Id")]
        public virtual string CustomerPurchaseOrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Description("订单号")]
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [Description("款号")]
        public virtual string StyleNo { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Description("颜色")]
        public virtual string PColor { get; set; }
        /// <summary>
        /// PO号
        /// </summary>
        [Description("PO号")]
        public virtual string Po { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string PSize { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Description("尺码")]
        public virtual string LineName { get; set; }
        /// <summary>
        /// 工段
        /// </summary>
        [Description("工段")]
        public virtual string FlowSection { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Description("单位")]
        public virtual string Unit { get; set; }
        /// <summary>
        /// 任务数量
        /// </summary>
        [Description("任务数量")]
        public virtual int? TaskNum { get; set; }
        /// <summary>
        /// 在线数
        /// </summary>
        [Description("在线数")]
        public virtual int? OnlineNum { get; set; }
        /// <summary>
        /// 今日挂片
        /// </summary>
        [Description("今日挂片")]
        public virtual int? TodayHangingPieceSiteNum { get; set; }
        /// <summary>
        /// 今日产出
        /// </summary>
        [Description("今日产出")]
        public virtual int? TodayProdOutNum { get; set; }
        /// <summary>
        /// 累计产出
        /// </summary>
        [Description("累计产出")]
        public virtual int? TotalProdOutNum { get; set; }
        /// <summary>
        /// 今日绑卡
        /// </summary>
        [Description("今日绑卡")]
        public virtual int? TodayBindCard { get; set; }
        /// <summary>
        /// 今日返工
        /// </summary>
        [Description("今日返工")]
        public virtual int? TodayRework { get; set; }
        /// <summary>
        /// 累计挂片
        /// </summary>
        [Description("累计挂片")]
        public virtual int? TotalHangingPieceSiteNum { get; set; }
        /// <summary>
        /// 累计返工
        /// </summary>
        [Description("累计返工")]
        public virtual int? TotalRework { get; set; }
        /// <summary>
        /// 累计绑卡
        /// </summary>
        [Description("累计绑卡")]
        public virtual int? TotalBindNum { get; set; }
    }
}

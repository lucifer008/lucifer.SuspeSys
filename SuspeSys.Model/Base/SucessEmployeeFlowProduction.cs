using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 完成员工工序产量明细
    /// </summary>
    [Serializable]
    public partial class SucessEmployeeFlowProduction : MetaData {
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
        /// 员工Id
        /// </summary>
        [Description("员工Id")]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 工序生产的员工姓名
        /// </summary>
        [Description("工序生产的员工姓名")]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 员工卡号
        /// </summary>
        [Description("员工卡号")]
        public virtual string CardNo { get; set; }
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
        /// 0:正常衣架;1:返工衣架
        /// </summary>
        [Description("0:正常衣架;1:返工衣架")]
        public virtual short? HangerType { get; set; }
        /// <summary>
        /// 0:未完成;1.已完成
        /// </summary>
        [Description("0:未完成;1.已完成")]
        public virtual short? Status { get; set; }
    }
}

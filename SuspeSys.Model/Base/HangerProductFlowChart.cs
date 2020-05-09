using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣架生产工艺图
    /// </summary>
    [Serializable]
    public partial class HangerProductFlowChart : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 主轨号(0-255)
        /// </summary>
        [Description("主轨号(0-255)")]
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
        /// 生产组
        /// </summary>
        [Description("生产组")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Description("单位")]
        public virtual string Unit { get; set; }
        /// <summary>
        /// 衣架号
        /// </summary>
        [Description("衣架号")]
        public virtual string HangerNo { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [Description("款号")]
        public virtual string StyleNo { get; set; }
        /// <summary>
        /// 衣架是否生产完成
        /// </summary>
        [Description("衣架是否生产完成")]
        public virtual bool? IsHangerSucess { get; set; }
        /// <summary>
        /// PO号
        /// </summary>
        [Description("PO号")]
        public virtual string Po { get; set; }
        /// <summary>
        /// 制单号
        /// </summary>
        [Description("制单号")]
        public virtual string ProcessOrderNo { get; set; }
        /// <summary>
        /// 工艺图Id
        /// </summary>
        [Description("工艺图Id")]
        public virtual string ProcessChartId { get; set; }
        /// <summary>
        /// 工序索引
        /// </summary>
        [Description("工序索引")]
        public virtual int? FlowIndex { get; set; }
        /// <summary>
        /// 工序Id
        /// </summary>
        [Description("工序Id")]
        public virtual string FlowId { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Description("工序号")]
        public virtual string FlowNo { get; set; }
        /// <summary>
        /// 工序代码
        /// </summary>
        [Description("工序代码")]
        public virtual string FlowCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Description("工序名称")]
        public virtual string FlowName { get; set; }
        /// <summary>
        /// 工序制作站点Id
        /// </summary>
        [Description("工序制作站点Id")]
        public virtual string StatingId { get; set; }
        /// <summary>
        /// 工序制作站点(来自工艺路线图中的站点)
        /// </summary>
        [Description("工序制作站点(来自工艺路线图中的站点)")]
        public virtual short? StatingNo { get; set; }
        /// <summary>
        /// 工序制作站点容量
        /// </summary>
        [Description("工序制作站点容量")]
        public virtual long? StatingCapacity { get; set; }
        /// <summary>
        /// 工序下一站点(计算出的下一道工序制作站点)
        /// </summary>
        [Description("工序下一站点(计算出的下一道工序制作站点)")]
        public virtual short? NextStatingNo { get; set; }
        /// <summary>
        /// 工序实际生产站点
        /// </summary>
        [Description("工序实际生产站点")]
        public virtual short? FlowRealyProductStatingNo { get; set; }
        /// <summary>
        /// 生产状态(0:待生产:1:生产中:2:生产完成)
        /// </summary>
        [Description("生产状态(0:待生产:1:生产中:2:生产完成)")]
        public virtual short? Status { get; set; }
        /// <summary>
        /// 0:正常工序;1:返工工序:2:其他
        /// </summary>
        [Description("0:正常工序;1:返工工序:2:其他")]
        public virtual byte? FlowType { get; set; }
        /// <summary>
        /// 工序是否生产完成
        /// </summary>
        [Description("工序是否生产完成")]
        public virtual bool? IsFlowSucess { get; set; }
        /// <summary>
        /// 是否是返工发起站点
        /// </summary>
        [Description("是否是返工发起站点")]
        public virtual bool? IsReworkSourceStating { get; set; }
        /// <summary>
        /// 疵点代码
        /// </summary>
        [Description("疵点代码")]
        public virtual string DefectCode { get; set; }
        /// <summary>
        /// 疵点名称
        /// </summary>
        [Description("疵点名称")]
        public virtual string DefcectName { get; set; }
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
        /// 员工工号
        /// </summary>
        [Description("员工工号")]
        public virtual string EmployeeNo { get; set; }
        /// <summary>
        /// 进站时间
        /// </summary>
        [Description("进站时间")]
        public virtual DateTime? IncomeSiteDate { get; set; }
        /// <summary>
        /// 比较时间
        /// </summary>
        [Description("比较时间")]
        public virtual DateTime? CompareDate { get; set; }
        /// <summary>
        /// 出战时间
        /// </summary>
        [Description("出战时间")]
        public virtual DateTime? OutSiteDate { get; set; }
        /// <summary>
        /// 返工员工工号
        /// </summary>
        [Description("返工员工工号")]
        public virtual string ReworkEmployeeNo { get; set; }
        /// <summary>
        /// 返工员工名称
        /// </summary>
        [Description("返工员工名称")]
        public virtual string ReworkEmployeeName { get; set; }
        /// <summary>
        /// 返工发起时间
        /// </summary>
        [Description("返工发起时间")]
        public virtual DateTime? ReworkDate { get; set; }
        /// <summary>
        /// 返工发起主轨
        /// </summary>
        [Description("返工发起主轨")]
        public virtual short? ReworkMaintrackNumber { get; set; }
        /// <summary>
        /// 返工发起站点
        /// </summary>
        [Description("返工发起站点")]
        public virtual short? ReworkStatingNo { get; set; }
        /// <summary>
        /// 检验结果
        /// </summary>
        [Description("检验结果")]
        public virtual string CheckResult { get; set; }
        /// <summary>
        /// 检验信息
        /// </summary>
        [Description("检验信息")]
        public virtual string CheckInfo { get; set; }
        /// <summary>
        /// 标准工时
        /// </summary>
        [Description("标准工时")]
        public virtual string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        [Description("标准工价")]
        public virtual decimal? StandardPrice { get; set; }
        /// <summary>
        /// 衣架状态
        /// </summary>
        [Description("衣架状态")]
        public virtual int? HangerStatus { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        [Description("备注2")]
        public virtual string Memo { get; set; }

        /// <summary>
        /// 产出工序Id
        /// </summary>
        [Description("产出工序Id")]
        public virtual string OutFlowId { get; set; }
        /// <summary>
        /// 产出工序号
        /// </summary>
        [Description("产出工序号")]
        public virtual string OutFlowNo { get; set; }
        /// <summary>
        /// 产出工序名称
        /// </summary>
        [Description("产出工序名称")]
        public virtual string OutFlowName { get; set; }


        /// <summary>
        /// 产出工序名称
        /// </summary>
        [Description("返工发起组别")]
        public virtual string ReworkGroupNo { get; set; }
        /// <summary>
        /// 产出工序名称
        /// </summary>
        [Description("返工发起工序号")]
        public virtual string CheckReworkNo { get; set; }
        /// <summary>
        /// 产出工序名称
        /// </summary>
        [Description("发起返工工序代码")]
        public virtual string CheckReworkCode { get; set; }
    }
}

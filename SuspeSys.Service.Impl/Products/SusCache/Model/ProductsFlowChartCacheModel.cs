using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class ProductsFlowChartCacheModel: ProcessFlowChartFlowRelation
    {

        public virtual string ProcessFlowChartId { set; get; }

        new public List<ProcessFlowStatingItemModel> ProcessFlowStatingItemList = new List<ProcessFlowStatingItemModel>();

    }
    public class ProductsFlowChartCacheTempModel {
        public virtual string Id { set; get; }

        public virtual string ProcessFlowChartId { set; get; }
        public virtual string BoltProcessFlowId { set; get; }
        public virtual string ProcessFlowChartFlowRelationId { set; get; }
        public virtual string CraftFlowNo { set; get; }
        public virtual string FlowNo { set; get; }
        public virtual string FlowCode { set; get; }
        public virtual string FlowName { set; get; }
        public virtual string StatingNo { set; get; }
        public virtual int MainTrackNumber { set; get; }
        public virtual int StatingCapacity { set; get; }
        public virtual string StatingId { get;  set; }
        public virtual string ProcessFlowId { get; set; }
        /// 是否接收衣架
        /// </summary>
        [Description("是否接收衣架")]
        public virtual byte? IsReceivingHanger { get; set; }
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
        /// 顺序编号
        /// </summary>
        [Description("是否接收衣架")]
        public virtual short? IsEnabled { get; set; }
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
        /// <summary>
        /// 站点是否能接收衣架(硬件)
        /// </summary>
        public virtual bool IsReceivingHangerStating { get; set; }

        /// <summary>
        /// 是否往前合并
        /// </summary>
        [Description("是否往前合并")]
        public virtual bool? IsMergeForward { get; set; }
        /// <summary>
        /// 合并工序制单工序Id
        /// </summary>
        public virtual string MergeProcessFlowChartFlowRelationId { get; set; }
        /// <summary>
        /// 是否是产出工序
        /// </summary>
        [Description("是否是产出工序")]
        public virtual byte? IsProduceFlow { get; set; }
        
        /// <summary>
        /// 站点角色
        /// </summary>
        public virtual string StatingRoleCode { get; set; }

        /// <summary>
        /// 合并工序号
        /// </summary>
        [Description("合并工序号")]
        public virtual string MergeFlowNo { get; set; }
        #region 临时扩展
        public int HangerNo { get; set; }
        public string ProductId { get;  set; }
        public string ProcessOrderNo { get; set; }
        public string StyleNo { get;  set; }
        public string PColor { get;  set; }
        public string LineName { get;  set; }
        public string Num { get;  set; }
        public string PSize { get;  set; }
        #endregion
    }
}

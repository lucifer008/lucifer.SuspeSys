using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
   public partial class HangerProductFlowChart
    {
        /// <summary>
        /// 是否全部接收尺码
        /// </summary>
        [Description("是否全部接收尺码")]
        public  bool? IsReceivingAllSize { get; set; }
        /// <summary>
        /// 是否全部接收颜色
        /// </summary>
        [Description("是否全部接收颜色")]
        public  bool? IsReceivingAllColor { get; set; }
        /// <summary>
        /// 是否全部接收PO号码
        /// </summary>
        [Description("是否全部接收PO号码")]
        public  bool? IsReceivingAllPoNumber { get; set; }
        /// <summary>
        /// 是否是收尾站
        /// </summary>
        [Description("是否是收尾站")]
        public  bool? IsEndStating { get; set; }
        /// <summary>
        /// 接收比例
        /// </summary>
        [Description("接收比例")]
        public  decimal? Proportion { get; set; }
        /// <summary>
        /// 接收颜色
        /// </summary>
        [Description("接收颜色")]
        public  string ReceivingColor { get; set; }
        /// <summary>
        /// 接收尺码
        /// </summary>
        [Description("接收尺码")]
        public  string ReceivingSize { get; set; }
        /// <summary>
        /// 接收PO号码
        /// </summary>
        [Description("接收PO号码")]
        public  string ReceivingPoNumber { get; set; }
        
        /// 是否接收衣架
        /// </summary>
        [Description("是否接收衣架")]
        public  byte? IsReceivingHanger { get; set; }
        [Description("是否接收衣架")]
        public  short? IsEnabled { get; set; }
        /// <summary>
        /// 硬件站点是否是接收衣架状态
        /// </summary>
        public bool? IsReceivingHangerStating { get; set; }

        /// <summary>
        /// 是否往前合并
        /// </summary>
        [Description("是否往前合并")]
        public  bool? IsMergeForward { get; set; }
        /// <summary>
        /// 合并工序制单工序Id
        /// </summary>
        public  string MergeProcessFlowChartFlowRelationId { get; set; }
        /// <summary>
        /// 是否是产出工序
        /// </summary>
        [Description("是否是产出工序")]
        public  byte? IsProduceFlow { get; set; }

        /// <summary>
        /// 合并工序号
        /// </summary>
        [Description("合并工序号")]
        public  string MergeFlowNo { get; set; }
        /// <summary>
        /// 工序工序Id
        /// </summary>
        public  string ProcessFlowChartFlowRelationId { set; get; }
    }
}

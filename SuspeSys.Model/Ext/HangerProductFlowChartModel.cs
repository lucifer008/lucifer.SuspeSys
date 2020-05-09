using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    [Serializable]
    public class HangerProductFlowChartModel: HangerProductFlowChart
    {
        /// <summary>
        /// 标准工时
        /// </summary>
        [Description("标准工时")]
        public string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        [Description("标准工价")]
        public decimal? StandardPrice { get; set; }
        /// <summary>
        /// 是否是挂片站
        /// </summary>
        public bool isHangingPiece { get; set; }
        public string LineName { get; set; }
        public string StyleNo { get; set; }
        public string Num { get; set; }
        /// <summary>
        /// 是否已分配
        /// </summary>
        public bool isAllocationed { set; get; }
        /// <summary>
        /// 分配日期
        /// </summary>
        public DateTime? AllocationedDate { set; get; }
        /// <summary>
        /// 站点角色
        /// </summary>
        public  string StatingRoleCode { get; set; }

        /// <summary>
        /// 是否是存储站再分配给本工序的车缝站
        /// </summary>
        public bool IsStorageStatingAgainAllocationedSeamsStating { get; set; }
        /// <summary>
        /// 是否是存储站出战
        /// </summary>
        public bool IsStorageStatingOutSite { get; set; }
        /// <summary>
        /// 实际站点角色
        /// </summary>
        public string FlowRealyProductStatingRoleCode { get; set; }

        public bool IsFlowChartChangeUpdate { get; set; }
        public DateTime? FlowChartChangeUpdateDate { get; set; }


        public  string CheckResult { set; get; }
        public  string CheckInfo { set; get; }
        public string GroupNo { get; set; }
        /// <summary>
        /// 0:已分配
        /// 1:已进站
        /// 2:已出战
        /// 3:已删除
        /// 4:发起返工
        /// 5:监测点已分配作废
        /// 6:监测点已进站作废
        /// </summary>
        public int HangerStatus { set; get; }

        public int LastStatingNo { set; get; }
        /// <summary>
        /// 桥接是否出战
        /// </summary>
        public bool BridgeOutSiteSucessed { set; get; }
        public string ReworkDate1 { get; set; }
    }
}

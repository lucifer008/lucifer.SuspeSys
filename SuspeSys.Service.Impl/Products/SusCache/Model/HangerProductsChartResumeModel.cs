using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class HangerProductsChartResumeModel
    {
        public int MainTrackNumber { set; get; }
        public string HangerNo { set; get; }
        public string StatingNo { set; get; }
        public string NextStatingNo { set; get; }
        public HangerProductFlowChartModel HangerProductFlowChart { set; get; }
        public IList<HangerProductFlowChartModel> HangerProductFlowChartList { set; get; }
        /// <summary>
        /// 0:挂片
        /// 1:普通工序
        /// 2:返工工序
        /// </summary>
        public int Tag { set; get; }

        /// <summary>
        /// 0:注册/挂片;
        /// 1:分配;
        /// 2:进站;
        /// 3:出战;
        /// 4:返工
        /// -1:重复挂片
        /// 5:站点删除
        /// 6:工序删除
        /// 7:工序及站点移动
        /// 8:桥接不携带工序进站 或者 桥接站且在携带工序，且工序已完成又进站
        /// 9:桥接携带工序且工序已完成，再次分配
        /// 10:F2指定衣架分配
        /// 11.F2衣架进站
        /// 12: F2指定站出战衣架轨迹修正
        /// 
        /// </summary>
        public int Action { set; get; }
        public string ReworkFlowNos { get;  set; }
        public string FlowNo { get; set; }
        public bool IsBridgeAllocation { get;  set; }
    }
}

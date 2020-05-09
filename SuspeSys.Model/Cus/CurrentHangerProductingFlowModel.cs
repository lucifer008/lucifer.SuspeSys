using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Ext;

namespace SuspeSys.Domain.Cus
{
    public class CurrentHangerProductingFlowModel
    {
        public string HangerNo { set; get; }
        public  int MainTrackNumber { set; get; }
        public  int StatingNo { set; get; }
        public string FlowNo { set; get; }
        public int FlowIndex { set; get; }
        /// <summary>
        /// 1:返工衣架
        /// 0：正常衣架
        /// </summary>
        public int FlowType { get; set; }
        public HangerProductFlowChartModel HangerProductFlowChart { get; set; }
        public List<HangerProductFlowChartModel> HangerProductFlowChartList { get; set; }
        public string ReworkFlowNos { get; set; }
        /// <summary>
        /// 是否是F2指定
        /// </summary>
        public bool IsF2Assgn { get; set; }
    }
}

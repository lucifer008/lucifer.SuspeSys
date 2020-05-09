using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    [Serializable]
    public class ReworkModel
    {
        public List<HangerProductFlowChartModel> CpHangerProductFlowChartList { get; set; }
        public int CurrentIndex { get; set; }
        public int FlowIndex { get; set; }
        public List<ProductsProcessOrderModel> NextStatingList { set; get; }
        public HangerProductFlowChartModel ReworkFlow { get; set; }
        public List<string> ReworkFlowNoList { get; set; }
        public List<HangerProductFlowChartModel> ReworkResultHangerProductsFlowChartList { get; set; }
        public HangerProductFlowChartModel SourceRewokFlowChart { get; set; }
    }
}

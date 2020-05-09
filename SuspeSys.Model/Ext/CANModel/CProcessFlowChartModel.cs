using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    public class CProcessFlowChartModel
    {
        public ProcessFlowChart ProcessFlowChart = new ProcessFlowChart();
        public IList<ProcessFlowChartFlowRelation> ProcessFlowChartFlowRelationList = new List<ProcessFlowChartFlowRelation>();
        public List<ProcessFlowStatingItem> ProcessFlowStatingItemList = new List<ProcessFlowStatingItem>();
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class CopyProcessOrderModel
    {
        public ProcessOrder ProcessOrder = new ProcessOrder();

        public IList<ProcessOrderColorItemModel> ProcessOrderColorItemModelList = new List<ProcessOrderColorItemModel>();
        public IList<ProcessFlowVersion> ProcessFlowVersionList = new List<ProcessFlowVersion>();

        public IList<ProcessFlowChart> ProcessFlowChartlList = new List<ProcessFlowChart>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusThread
{
    public class BindHangerFlowChartThread
    {
        private BindHangerFlowChartThread() { }
        public static BindHangerFlowChartThread Instance { get { return new BindHangerFlowChartThread(); } }
        public string FlowChartId { set; get; }
        public void BindFlowChartToHanger()
        {

        }
    }
}

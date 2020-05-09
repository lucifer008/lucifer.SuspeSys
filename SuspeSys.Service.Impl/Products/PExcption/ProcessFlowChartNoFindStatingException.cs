using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 工艺图未发现站点异常
    /// </summary>
    public class ProcessFlowChartNoFindStatingException:ApplicationException
    {
        public ProcessFlowChartNoFindStatingException() : base() { }
        public ProcessFlowChartNoFindStatingException(string message) : base(message) { }
        public ProcessFlowChartNoFindStatingException(string message, Exception innerException) : base(message,innerException) { }
    }
}

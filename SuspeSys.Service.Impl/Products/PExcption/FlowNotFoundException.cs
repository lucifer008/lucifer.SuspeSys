using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 工序未找到异常
    /// </summary>
    public class FlowNotFoundException : ApplicationException
    {
        public FlowNotFoundException() : base() { }
        public FlowNotFoundException(string message) : base(message) { }
        public FlowNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}

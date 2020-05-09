using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 返工衣架未找到异常
    /// </summary>
    public class ReworkHangerNotFoundException: ApplicationException
    {
        public ReworkHangerNotFoundException() : base() { }
        public ReworkHangerNotFoundException(string message) : base(message) { }
        public ReworkHangerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}

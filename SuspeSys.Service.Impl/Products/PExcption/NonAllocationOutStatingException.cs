using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 衣架原分配站点与本站不匹配，不允许出站，请联系管理员处理！
    /// </summary>
    public class NonAllocationOutStatingException: ApplicationException
    {

        public NonAllocationOutStatingException() : base() { }
        public NonAllocationOutStatingException(string message) : base(message) { }
        public NonAllocationOutStatingException(string message, Exception innerException) : base(message,innerException) { }
    }
}

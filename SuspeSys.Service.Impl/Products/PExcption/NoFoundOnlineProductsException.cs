using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 未发现上线产品
    /// </summary>
    public class NoFoundOnlineProductsException:ApplicationException
    {
        public NoFoundOnlineProductsException() : base() { }
        public NoFoundOnlineProductsException(string message) : base(message) { }
        public NoFoundOnlineProductsException(string message, Exception innerException) : base(message,innerException) { }
    }
}

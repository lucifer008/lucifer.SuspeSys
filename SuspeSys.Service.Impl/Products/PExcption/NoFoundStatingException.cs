using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class NoFoundStatingException: ApplicationException
    {
        public NoFoundStatingException() : base() { }
        public NoFoundStatingException(string message) : base(message) { }
        public NoFoundStatingException(string message, Exception innerException) : base(message, innerException) { }
        public string FlowNo { set; get; }
    }
}

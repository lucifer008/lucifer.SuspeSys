using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class ReworkFlowNoNotFoundException : ApplicationException
    {
        public ReworkFlowNoNotFoundException() : base() { }
        public ReworkFlowNoNotFoundException(string message) : base(message) { }
        public ReworkFlowNoNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}

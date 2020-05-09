using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class BoltProcessFlowNoStatingException: ApplicationException
    {
        public BoltProcessFlowNoStatingException() : base() { }
        public BoltProcessFlowNoStatingException(string message) : base(message) { }
        public BoltProcessFlowNoStatingException(string message, Exception innerException) : base(message,innerException) { }
    }
}

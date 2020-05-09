using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
   public class ReworkDefectNotFoundException : ApplicationException
    {
        public ReworkDefectNotFoundException() : base() { }
        public ReworkDefectNotFoundException(string message) : base(message) { }
        public ReworkDefectNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}

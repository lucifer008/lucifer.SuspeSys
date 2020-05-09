using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
   public class HangerNoProductException:ApplicationException
    {
        public HangerNoProductException() : base() { }
        public HangerNoProductException(string message) : base(message) { }
        public HangerNoProductException(string message, Exception innerException) : base(message, innerException) { }
    }
}

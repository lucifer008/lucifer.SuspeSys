using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
   public class NonExistStatingException : ApplicationException
    {
        public NonExistStatingException() : base() { }
        public NonExistStatingException(string message) : base(message) { }
        public NonExistStatingException(string message, Exception innerException) : base(message,innerException) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class StatingNoLoginEmployeeException : ApplicationException
    {
        public StatingNoLoginEmployeeException() : base() { }
        public StatingNoLoginEmployeeException(string message) : base(message) { }
        public StatingNoLoginEmployeeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

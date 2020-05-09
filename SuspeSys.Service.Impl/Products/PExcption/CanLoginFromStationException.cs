using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class CanLoginFromStationException : ApplicationException
    {
        public CanLoginFromStationException() : base() { }
        public CanLoginFromStationException(string message) : base(message) { }
        public CanLoginFromStationException(string message, Exception innerException) : base(message, innerException) { }
    }
}

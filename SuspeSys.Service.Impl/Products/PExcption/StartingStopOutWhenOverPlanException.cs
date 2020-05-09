using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 
    /// </summary>
    public class StartingStopOutWhenOverPlanException : ApplicationException
    {
        public StartingStopOutWhenOverPlanException() : base() { }
        public StartingStopOutWhenOverPlanException(string message) : base(message) { }
        public StartingStopOutWhenOverPlanException(string message, Exception innerException) : base(message, innerException) { }
    }
}

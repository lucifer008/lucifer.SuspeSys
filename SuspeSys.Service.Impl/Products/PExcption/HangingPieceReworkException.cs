using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    /// <summary>
    /// 挂片站返工异常
    /// </summary>
    public class HangingPieceReworkException : ApplicationException
    {

        public HangingPieceReworkException() : base() { }
        public HangingPieceReworkException(string message) : base(message) { }
        public HangingPieceReworkException(string message, Exception innerException) : base(message, innerException) { }
    }
}

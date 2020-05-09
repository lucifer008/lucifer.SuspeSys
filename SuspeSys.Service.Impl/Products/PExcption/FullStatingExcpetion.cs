using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class FullStatingExcpetion:ApplicationException
    {
        public FullStatingExcpetion() : base() { }
        public FullStatingExcpetion(string message) : base(message) { }
        public FullStatingExcpetion(string message, Exception innerException) : base(message,innerException) { }

        public List<string> FullStatingList { get; set; }
        public string FullFlowNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class FlowDeleteOrStatingDeletedException : ApplicationException
    {
        public bool EqualFlowIsExistOtherStating { set; get; }
        public int HangerNo { set; get; }
        public int MainTrackNumber { set; get; }
        public int StatingNo { set; get; }
        public int NextFlowIndex { set; get; }
        public string FlowNo { get;  set; }
        public bool IsOnlyMoveFlow { get; set; }

        public FlowDeleteOrStatingDeletedException() : base() { }
        public FlowDeleteOrStatingDeletedException(string message) : base(message) { }
        public FlowDeleteOrStatingDeletedException(string message, Exception innerException) : base(message,innerException) { }
    }
}

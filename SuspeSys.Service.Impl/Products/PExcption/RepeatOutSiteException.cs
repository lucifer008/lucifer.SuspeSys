using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.PExcption
{
    public class RepeatOutSiteException: ApplicationException
    {
        public int HangerNo { set; get; }
        public int MainTrackNumber { set; get; }
        public int StatingNo { set; get; }
        public int FlowIndex { set; get; }
        public string FlowNo { get; set; }
        public RepeatOutSiteException() : base() { }
        public RepeatOutSiteException(string message) : base(message) { }
        public RepeatOutSiteException(string message, Exception innerException) : base(message,innerException) { }
    }
}

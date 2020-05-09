using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class HangerInSiteResult
    {
        public int MainTrackNumber { set; get; }
        public string HangerNo { set; get; }
        public string StatingNo { set; get; }
        public WaitProcessOrderHanger WaitProcessOrderHanger { set; get; }
    }
}

using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class HangerOutSiteResult
    {
        public int MainTrackNumber { set; get; }
        public string HangerNo { set; get; }
        public string StatingNo { set; get; }
        public string NextStatingNo { set; get; }
        public HangerProductFlowChartModel HangerProductFlowChart { set; get; }
        public ProcessFlowChartFlowRelation pfcFlowRelation { set; get; }
    }
}

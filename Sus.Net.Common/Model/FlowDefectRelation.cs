using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Model
{
    public class FlowDefectRelation
    {
        public string FlowCode { set; get; }
        public string DefectCode { set; get; }
        public int MainTrackNumber { set; get; }
        public int StatingNo { set; get; }
        public string FlowNo { set; get; }
    }
}

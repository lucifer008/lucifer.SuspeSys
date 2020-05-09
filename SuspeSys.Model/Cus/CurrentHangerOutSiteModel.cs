using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Ext;

namespace SuspeSys.Domain.Cus
{
    public class CurrentHangerOutSiteModel
    {
        public string HangerNo { set; get; }
        public  int MainTrackNumber { set; get; }
        public  int StatingNo { set; get; }
        public string FlowNo { set; get; }
        public int FlowIndex { set; get; }
        /// <summary>
        /// 1:返工衣架
        /// 0：正常衣架
        /// </summary>
        public int FlowType { get; set; }
        public DateTime OutSite { get; set; }

    }
}

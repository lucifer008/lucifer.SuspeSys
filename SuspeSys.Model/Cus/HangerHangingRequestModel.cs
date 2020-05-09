using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Cus
{
    public class HangerHangingRequestModel
    {
        public virtual int MainTrackNumber { set; get; }
        public virtual int StatingNo { set; get; }
        public virtual DateTime CompareDate { set; get; }
        public virtual DateTime OutSiteDate { set; get; }
        public virtual decimal StanardHours { set; get; }
    }
}

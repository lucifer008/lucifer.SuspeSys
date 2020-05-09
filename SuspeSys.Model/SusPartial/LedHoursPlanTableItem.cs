using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public partial class LedHoursPlanTableItem
    {
        public virtual string BeginDateShortDate { set; get; }
        public virtual string EndDateShortDate { set; get; }
        public virtual string PlanDate { set; get; }
    }
}

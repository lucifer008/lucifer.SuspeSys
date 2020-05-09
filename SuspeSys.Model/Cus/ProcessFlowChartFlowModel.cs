using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class ProcessFlowChartFlowModel: ProcessFlowChartFlowRelation
    {
        public virtual string Statings { set; get; }

        public virtual int StatingTotal { set; get; }
    }
}

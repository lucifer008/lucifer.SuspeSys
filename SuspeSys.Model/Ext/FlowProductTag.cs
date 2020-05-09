using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class FlowProductTag
    {
        public virtual int FlowIndex { set; get; }
        public virtual int TotalCount { set; get; }
        public virtual int NonProductsFlowCount { set; get; }
    }
}

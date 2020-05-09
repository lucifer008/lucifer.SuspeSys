using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public partial class BasicProcessFlowModel
    {
        public virtual bool IsProcessVersionFlow { set; get; }
        public virtual string ProcessOrderField { get; set; }
    }
}

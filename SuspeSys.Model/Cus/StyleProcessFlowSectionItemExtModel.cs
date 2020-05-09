using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Cus
{
    public class StyleProcessFlowSectionItemExtModel: StyleProcessFlowSectionItem
    {
        public virtual string StyleNo { set; get; }
        public virtual string StyleName { set; get; }
        public virtual string StyleProcessFlowSectionItemId { set; get; }
        public virtual string StyleId2 { set; get; }
    }
}

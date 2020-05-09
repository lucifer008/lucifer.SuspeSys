using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class ProcessOrderColorItemModel: ProcessOrderColorItem
    {
      // new public string Total { get; set; }
        // = new List<ProcessOrderColorSizeItem>()
        public virtual string CustomerpurchaseorderId { set; get; }
        public virtual IList<ProcessOrderColorSizeItem> ProcessOrderColorSizeItemList { set; get; }
    }
}

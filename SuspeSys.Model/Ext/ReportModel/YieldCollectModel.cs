using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    public class YieldCollectModel: Products
    {
        public virtual int? OutYield { set; get; }
        public virtual int? ReturnYield { set; get; }
        public virtual string ReturnRate { set; get; }
        public virtual string QueryDate { set; get; }
        public virtual int? HangingPieceCount { set; get; }
    }
}

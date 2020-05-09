using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class ProductsFlowModel
    {
        public virtual decimal Rate { set; get; }
        public virtual int CurrentFlowUseTime { set; get; }

        public virtual int TodayHangingPieceCount { set; get; }
        public virtual int TotalHangingPieceCount { set; get; }
        public virtual int TaskNum { set; get; }
    }
}

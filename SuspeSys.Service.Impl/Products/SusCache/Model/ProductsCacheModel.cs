using SuspeSys.Domain.Ext.CANModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class ProductsCacheModel
    {
        public virtual int ProductNumber { set; get; }
        public virtual int MainTrackNumber { set; get; }
        public virtual string GrouNo { set; get; }
        public virtual SuspeSys.Domain.Products OnlineProducts { set; get; }
        public virtual List<CProcessFlowChartModel> CProcessFlowChartModelList { set; get; }
    }
}

using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.ProductionLineSet.Model
{
    public class ProductionLineSetModel
    {
        public DaoModel.Pipelining Pipelining = new DaoModel.Pipelining();
        public IList<DaoModel.Stating> StatingList = new List<DaoModel.Stating>();
        public DaoModel.SiteGroup SiteGroup = new DaoModel.SiteGroup();
        public IList<ProductRealtimeInfoModel> ProductRealtimeInfoModelList = new List<ProductRealtimeInfoModel>();
    }
}

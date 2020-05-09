using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusTcp
{
    public class ReloadCacheServiceImpl : Base.ServiceBase, IReloadCacheService
    {
        public void ReloadBridgeSet()
        {
            SusCacheProductService.Instance.LoadBridgeSet();
        }

        public void ReloadSystemParameter()
        {
            SusCacheProductService.Instance.LoadSystemParameter();
            //throw new NotImplementedException();
        }
    }
}

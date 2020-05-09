using SuspeSys.Service.Impl.Products.SusCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache
{
    public class ProductsCache
    {

        public static ProductsCacheModel CurrentOnlineProductCacheModel = null;
        public static Dictionary<string, ProductsCacheModel> OnlineProductsList = new Dictionary<string, ProductsCacheModel>();

    }
}

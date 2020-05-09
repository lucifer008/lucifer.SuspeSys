using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Cache
{
    public class CurrentHangerCacheHandler
    {
        private CurrentHangerCacheHandler() { }
        public readonly static CurrentHangerCacheHandler Instance = new CurrentHangerCacheHandler();
        public void CurrentHangerFlowCorrect(SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel currentHangerProductingFlow)
        {
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerProductingFlow);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

        }
    }
}

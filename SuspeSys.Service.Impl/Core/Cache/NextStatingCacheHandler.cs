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
    public class NextStatingCacheHandler
    {
        private NextStatingCacheHandler() { }
        public readonly static NextStatingCacheHandler Instance = new NextStatingCacheHandler();
        public void NextStatingHangerResumeCorrect(HangerProductsChartResumeModel nextStatingHPResume)
        {
            var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
        }
    }
}

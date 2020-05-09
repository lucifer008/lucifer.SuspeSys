using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusRedis
{
    public class SusRedisUtils
    {
        private static SusRedisUtils Instance
        {
            get { return new SusRedisUtils(); }
        }
        private SusRedisUtils() { }

        public T GetRedisCach<T>(string key, string value)
        {
            var rd = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, T>(key);
            if (rd.ContainsKey(value))
            {
                return rd[value];
            }
            return default(T);
        }
    }
}

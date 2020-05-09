using SuspeSys.Service.Impl.SusRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl
{
    public class SusRedisUtil
    {
        /// <summary>
        /// 添加到Redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mainkey">主缓存key</param>
        /// <param name="subKey">子缓存key</param>
        /// <param name="obj">缓存</param>
        /// <param name="dbObj">数据库数据</param>
        public static void Add<T>(string mainKey,string subKey, T obj , IEnumerable<T> dbObj)
        {
            //获取主缓存
            var mainCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, T>(mainKey);

            foreach (T item in dbObj)
            {

            }

        }
    }
}

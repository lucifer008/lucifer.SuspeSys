using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache
{
    public class SusCacheBootstarp
    {
        public static SusCacheBootstarp Instance { get { return new SusCacheBootstarp(); } }
        private SusCacheBootstarp() { }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        public void Init()
        {
            SusCacheProductService.Instance.LoadOnLineProductsFlowChart();
            SusCacheProductService.Instance.UpdateHangerFlowChartCache();
            SusCacheProductService.Instance.LoadStating();
            SusCacheProductService.Instance.LoadUserRole();
            SusCacheProductService.Instance.LoadCardInfo();
            SusCacheProductService.Instance.LoadOnlineProductsToCache();
            SusCacheProductService.Instance.LoadDefectCodeToCache();
            SusCacheProductService.Instance.LoadProductsToCache();
            SusCacheProductService.Instance.LoadSystemParameter();
            SusCacheProductService.Instance.LoadBridgeSet();
            SusCacheProductService.Instance.LoadFaultCodeFirstAddressMapping();
            SusCacheProductService.Instance.LoadFaultCodeSecondAddressMapping();
        }

        /// <summary>
        /// 清除衣架所有缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        public void ClearHangerAllCache(string hangerNo)
        {
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Remove(hangerNo);
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION).Remove(hangerNo);
            NewCacheService.Instance.RemoveHangerOutSiteRecord(hangerNo);
            ClearBridgeStatingHangerOutSiteItem(hangerNo);
            ClearBridgeStatingHangerInSiteItem(hangerNo);
        }

        public void ClearBridgeStatingHangerOutSiteItem(string hangerNo)
        {
            var bridgeStatingHanerOutSiteResume = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerProductFlowChartModel>(SusRedisConst.BRIDGE_STATING_HANGER_OUT_SITE_RESUME);
            foreach (var key in bridgeStatingHanerOutSiteResume.Keys)
            {
                if (key.Split(':').Length > 2)
                {
                    var keyList = new List<string>() { key.Split(':')[2] };
                    if (keyList.Contains(hangerNo))
                    {
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerProductFlowChartModel>(SusRedisConst.BRIDGE_STATING_HANGER_OUT_SITE_RESUME).Remove(key);
                    }
                }
                else {
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerProductFlowChartModel>(SusRedisConst.BRIDGE_STATING_HANGER_OUT_SITE_RESUME).Remove(key);
                }
            }
        }
        public void ClearBridgeStatingHangerInSiteItem(string hangerNo)
        {
            var bridgeStatingInSiteCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, BridgeCacheModel>(SusRedisConst.BRIDGE_STATING_HANGER_IN_COME_ITEM);
            foreach (var key in bridgeStatingInSiteCache.Keys)
            {
                if (key.Split(':').Length > 2)
                {
                    var keyList = new List<string>() { key.Split(':')[2] };
                    if (keyList.Contains(hangerNo))
                    {
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, BridgeCacheModel>(SusRedisConst.BRIDGE_STATING_HANGER_IN_COME_ITEM).Remove(key);
                    }
                }
                else
                {
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, BridgeCacheModel>(SusRedisConst.BRIDGE_STATING_HANGER_IN_COME_ITEM).Remove(key);
                }
            }
        }
        /// <summary>
        /// 清除衣架分配明细缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        public void ClearHangerStatingAllocationItemCache(string hangerNo)
        {
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME).Remove(hangerNo);
        }
        /// <summary>
        /// 清除衣架工艺图缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        public void ClearHangerStatingHangerProductFlowChartCache(string hangerNo)
        {
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART).Remove(hangerNo);
        }
        /// <summary>
        /// 清除衣架生产明细缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        public void ClearHangerHangerProductItemCache(string hangerNo)
        {
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM).Remove(hangerNo);
        }
        ///// <summary>
        ///// 清除衣架待生产缓存
        ///// </summary>
        ///// <param name="hangerNo"></param>
        //public void ClearHangerWaitProcessOrderHangerCache(string hangerNo)
        //{
        //    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER).Remove(hangerNo);
        //}
    }
}

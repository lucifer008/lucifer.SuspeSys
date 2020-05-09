using Newtonsoft.Json;
using StackExchange.Redis;
using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MonitorUpload
{
   public class MonitorUploadService : SusLog
    {
        public static readonly MonitorUploadService Instance = new MonitorUploadService();
        /// <summary>
        /// 是否是存储站出战经过监测点的衣架
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="hangerNo"></param>
        /// <param name="fChartList"></param>
        /// <param name="allocationItem"></param>
        /// <returns></returns>
        public bool IsStorageStatingOutSite(int mainTrackNumber, string hangerNo, List<HangerProductFlowChartModel> fChartList, ref HangerStatingAllocationItem allocationItem)
        {
           // var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingAllocationItem.ContainsKey(hangerNo))
            {
                var allocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingAllocationItem[hangerNo];
                var lastHangerStatingAllocationItemList = allocationList.Where(f => f.Status.Value != 1 && ((f.Memo != null && f.Memo.Equals("-1")) || f.Memo == null) && f.AllocatingStatingDate != null);
                if (lastHangerStatingAllocationItemList.Count() > 0)
                {
                    var lastHangerStatingAllocationItem = lastHangerStatingAllocationItemList.OrderByDescending(f => f.AllocatingStatingDate).First();
                    var isStatingStorageStatingOutSite = CANProductsService.Instance.IsStatingStorage(lastHangerStatingAllocationItem.MainTrackNumber.Value, int.Parse(lastHangerStatingAllocationItem.SiteNo));
                    allocationItem = lastHangerStatingAllocationItem;
                    return isStatingStorageStatingOutSite;
                }
            }
            return false;
        }


        public bool CheckFlowIsSuccess(int mainTrackNumber, string hangerNo, List<HangerProductFlowChartModel> fChartList)
        {
            // return true;
            var isNonFlowSuccess = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0;
            if (isNonFlowSuccess) return false;
            var successedFlowIndexList = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value == HangerProductFlowChartStaus.Successed.Value)
                .Select(k => k.FlowIndex.Value).Distinct().ToList();
            var allFlowIndexList = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1)
                .Select(k => k.FlowIndex.Value).Distinct().ToList();
            if (successedFlowIndexList.Count > 0 && allFlowIndexList.Count > 0)
            {
                return successedFlowIndexList.Max() == allFlowIndexList.Max();
            }
            return isNonFlowSuccess;

        }

        public int GetNonProductsProcessFlowChartListByReworkFlowIndex(List<HangerProductFlowChartModel> hfcList, string hangerNo)
        {

           // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo))
            {
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo);//dicCurrentHangerProductingFlowModelCache[hangerNo];
                //如果工序被删除
                var flowIsExist = hfcList.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
                if (!flowIsExist)
                {
                    var nextFlowList = hfcList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        && f.FlowIndex.Value > 1
        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        && (((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        ).Select(f => f.FlowIndex);
                    if (nextFlowList.Count() > 0)
                    {
                        return nextFlowList.Min().Value;
                    }
                }
                return current.FlowIndex;
            }
            return -1;
        }


        public int GetNonProductsProcessFlowChartList(List<HangerProductFlowChartModel> hfcList, string hangerNo)
        {

           // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo))
            {
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo);//dicCurrentHangerProductingFlowModelCache[hangerNo];
                //如果工序被删除
                var flowIsExist = hfcList.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
                if (!flowIsExist)
                {
                    var nextFlowList = hfcList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        && f.FlowIndex.Value > 1
        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        && ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        || ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        ).Select(f => f.FlowIndex);
                    if (nextFlowList.Count() > 0)
                    {
                        return nextFlowList.Min().Value;
                    }
                }
                return current.FlowIndex;
            }
            return -1;
        }

    }
}

using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusCache
{
    public class ProcessFlowChartCacheServiceImpl : ServiceBase, IProcessFlowChartCacheService
    {
        public void UpdateOnlineFlowChartCache(string flowChartId)
        {
            cacheInfo.InfoFormat("工艺图 flowchartId={0}变更,更新缓存开始",flowChartId);
            SusCacheProductService.Instance.LoadOnLineProductsFlowChart(flowChartId);
            cacheInfo.InfoFormat("工艺图 flowchartId={0}变更,更新缓存结束", flowChartId);

            cacheInfo.InfoFormat("清除分配比例记录缓存开始．．．．．", flowChartId);
            SusCacheProductService.Instance.ClearStatingAllocation(flowChartId);
            cacheInfo.InfoFormat("清除分配比例记录缓存结束．．．．", flowChartId);
        }

        /// <summary>
        /// 同步工艺图到生产环境中生产中的衣架工艺缓存
        /// </summary>
        /// <param name="flowChartId"></param>
        public void UpdateProductingHangerFlowChartCache(string flowChartId)
        {
            cacheInfo.InfoFormat("【同步工艺图到生产环境中生产中的衣架工艺缓存】 flowchartId={0}变更,更新缓存开始", flowChartId);
            SusCacheProductService.Instance.UpdateProductingHangerFlowChartCache(flowChartId);
            cacheInfo.InfoFormat("【同步工艺图到生产环境中生产中的衣架工艺缓存】 flowchartId={0}变更,更新缓存结束", flowChartId);
        }
    }
}

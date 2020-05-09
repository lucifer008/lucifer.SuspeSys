using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.SusTcp
{
   public interface IProcessFlowChartCacheService
    {
        void UpdateOnlineFlowChartCache(string flowChartId);

        /// <summary>
        /// 同步工艺图到生产环境中生产中的衣架工艺缓存
        /// </summary>
        /// <param name="flowChartId"></param>
        void UpdateProductingHangerFlowChartCache(string flowChartId);
    }
}

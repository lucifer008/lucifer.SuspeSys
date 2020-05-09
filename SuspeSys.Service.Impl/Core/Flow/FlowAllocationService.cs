using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Core.OutSite;
using SuspeSys.Service.Impl.Core.Rework;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Flow
{
    public class FlowAllocationService : SusLog
    {
        private FlowAllocationService() { }
        public readonly static FlowAllocationService Instance = new FlowAllocationService();
        private static readonly object locObject = new object();
        public void NextFlowHandler(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
            lock (locObject)
            {
                var hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo.ToString());
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo.ToString());
                var isReworkHanger = current.FlowType == 1;
                var currentIndex = current.FlowIndex;
                var ppChart = NewCacheService.Instance.GetFlowIndexFlowChart(hangerProcessFlowChartList, currentIndex, tenMaintracknumber, tenStatingNo);
                if (isReworkHanger)
                {
                    ReworkService.Instance.ReworkHangerHandler(tenMaintracknumber, tenStatingNo, tenHangerNo.ToString(), hangerProcessFlowChartList, ppChart);
                    return;
                }
                OutSiteCommonHangerHandler.Instance.HangerOutSiteHandler(tenMaintracknumber, tenStatingNo, tenHangerNo.ToString(), hangerProcessFlowChartList, ppChart);
            }
        }
    }
}

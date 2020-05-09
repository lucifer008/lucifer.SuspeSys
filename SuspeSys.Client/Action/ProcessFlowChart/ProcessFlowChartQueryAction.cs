using SuspeSys.Service.Impl.ProcessOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessFlowChart
{
    public class ProcessFlowChartQueryAction: BaseAction
    {
        
        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string versionId = null)
        {
            return ProcessFlowChartSercice.GetProcessFlowChartList(versionId);
        }

        public IList<DaoModel.ProcessFlowChartFlowRelationModel> GetProcessFlowChartLineItem(string flowChartId)
        {
            return ProcessFlowChartSercice.GetProcessFlowChartLineItem(flowChartId);
        }
        public IList<DaoModel.ProcessFlowChartGrop> GetProcessFlowChartGroupList(string flowChartId)
        {
            return ProcessFlowChartSercice.GetProcessFlowChartGroupList(flowChartId);
        }
        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartListByOnlineProducts(string GroupNo = null)
        {
            return ProcessFlowChartSercice.GetProcessFlowChartListByOnlineProducts(GroupNo);
        }

        internal string GetOnlineFlowChartId(string GroupNo)
        {
            return ProcessFlowChartSercice.GetOnlineFlowChartId(GroupNo);
        }
    }
}

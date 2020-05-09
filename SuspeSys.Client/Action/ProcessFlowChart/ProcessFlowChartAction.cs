using SuspeSys.Client.Action.ProcessFlowChart.Model;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Service.Impl.ProcessOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Client.Action.SuspeRemotingClient;

namespace SuspeSys.Client.Action.ProcessFlowChart
{
    public class ProcessFlowChartAction : BaseAction
    {
        public ProcessFlowChartModel Model = new ProcessFlowChartModel();
        // private ProcessFlowChartServiceImpl ProcessFlowChartSercice = new ProcessFlowChartServiceImpl();
        public void AddProcessFlowChart()
        {
            ProcessFlowChartSercice.AddProcessFlowChart(Model.ProcessFlowChart, Model.ProcessFlowChartGropList, Model.ProcessFlowChartFlowRelationList);
        }
        public void UpdateProcessFlowChart()
        {
            ProcessFlowChartSercice.UpdateProcessFlowChart(Model.ProcessFlowChart, Model.ProcessFlowChartGropList, Model.ProcessFlowChartFlowRelationList);

            //更新工艺图到缓存
            cacheInfo.InfoFormat("工艺图 flowchartId={0}变更,更新缓存开始", Model.ProcessFlowChart.Id);
            SuspeRemotingService.processFlowChartCacheService.UpdateOnlineFlowChartCache(Model.ProcessFlowChart.Id);
            SuspeRemotingService.processFlowChartCacheService.UpdateProductingHangerFlowChartCache(Model.ProcessFlowChart.Id);
            cacheInfo.InfoFormat("工艺图 flowchartId={0}变更,更新缓存结束", Model.ProcessFlowChart.Id);
        }
        public string GetCurrentMaxProcessFlowChartNo(string processFlowVersionId)
        {
            return ProcessFlowChartSercice.GetCurrentMaxProcessFlowChartNo(processFlowVersionId);
        }
        public bool CheckProcessFlowChartNameIsExist(string lineName)
        {
            return ProcessFlowChartSercice.CheckProcessFlowChartNameIsExist(lineName);
        }
        public IList<DaoModel.ProcessOrderColorItem> GetProcessOrderColorList(string pOrderId, ref IList<string> sizeList)
        {
            return ProcessFlowChartSercice.GetProcessOrderColorList(pOrderId, ref sizeList);
        }
        public DaoModel.ProcessFlowVersion GetProcessFlowVerson(string flowChartId)
        {
            return ProcessFlowChartSercice.GetProcessFlowVerson(flowChartId);
        }

        /// <summary>
        /// 通过制单版本号获取PO列表
        /// </summary>
        /// <param name="processFlowVersionId"></param>
        /// <returns></returns>
        public List<string> GetPoList(string processFlowVersionId)
        {
            return ProcessFlowChartSercice.GetPoList(processFlowVersionId);
        }
    }
}

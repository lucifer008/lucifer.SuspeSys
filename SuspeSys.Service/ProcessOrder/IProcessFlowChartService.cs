using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.ProcessOrder
{
    public interface IProcessFlowChartService
    {
        void AddProcessFlowChart(DaoModel.ProcessFlowChart processFlowChart, IList<DaoModel.ProcessFlowChartGrop> ProcessFlowChartGroupList, IList<DaoModel.ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList);
        void UpdateProcessFlowChart(DaoModel.ProcessFlowChart processFlowChart,
              IList<DaoModel.ProcessFlowChartGrop> ProcessFlowChartGroupList,
            IList<DaoModel.ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList);
        IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string flowVersionId);
        IList<DaoModel.ProcessFlowChartFlowRelationModel> GetProcessFlowChartLineItem(string flowChartId);
        IList<DaoModel.ProcessFlowChartGrop> GetProcessFlowChartGroupList(string flowChartId);
        string GetCurrentMaxProcessFlowChartNo(string processFlowVersionId);
        long GetMaxProcessFlowChartNo(string processFlowVersionId);
        IList<DaoModel.ProcessFlowChart> GetProcessFlowChartListByOnlineProducts(string GroupNo);
        bool CheckProcessFlowChartNameIsExist(string lineName);
        string GetOnlineFlowChartId(string groupNo);
        IList<DaoModel.ProcessOrderColorItem> GetProcessOrderColorList(string pfChartId,ref IList<string> sizeList);
        DaoModel.ProcessFlowVersion GetProcessFlowVerson(string flowChartId);

        /// <summary>
        /// 通过制单版本号获取PO列表
        /// </summary>
        /// <param name="processFlowVersionId"></param>
        /// <returns></returns>
        List<string> GetPoList(string processFlowVersionId);
    }
}

using System.Collections.Generic;

using DaoModel = SuspeSys.Domain;
using System.Collections;

namespace SuspeSys.Service.ProcessOrder
{
    public interface IProcessOrderQueryService
    {
        IList<DaoModel.ProcessOrder> SearchProcessOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, DaoModel.ProcessOrderModel conModel);
        IList<DaoModel.ProcessOrderModel> SearchProcessOrder();
        DaoModel.ProcessOrder GetProcessOrder(string orderId);
        DaoModel.ProcessOrderModel GetProcessOrder2(string orderId);
        IList<DaoModel.StyleProcessFlowSectionItemModel> GetProcessOrderStyleFlow(string styleNo);
        IList<SuspeSys.Domain.ProcessOrder> SearchProcessOrder(DaoModel.ProcessOrder pOrder);
        IList<DaoModel.ProcessFlowVersion> GetProcessOrderFlowVersionList(string processOrderId);
        IList<DaoModel.ProcessFlow> GetProcessOrderFlowList(string processFlowVersionId);
        IList<DaoModel.ProcessOrderColorItemModel> GetProcessOrderItem(string processOrderId);

        IList<DaoModel.ProcessFlowVersionModel> GetProcessOrderFlowVersionList();
        IList<DaoModel.StatingModel> GetGroupStatingList(string groups);
        long GetMaxProcessOrderFlowVersion(string processOrderId);
        string GetCurrentMaxProcessFlowVersionNo(string processOrderId);
        bool CheckProcessOrderNoIsExist(string pOrderNo);
        /// <summary>
        /// 检查制单是否已经上线生产
        /// </summary>
        /// <param name="processOrderId"></param>
        /// <returns></returns>
        bool CheckProcessOrderIsProducts(string processOrderId);
    }
}

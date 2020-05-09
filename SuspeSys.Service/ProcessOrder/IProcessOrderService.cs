using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service
{
    public interface IProcessOrderService
    {
        void AddProcessOrder(SuspeSys.Domain.ProcessOrder pOrder, IList<ProcessOrderColorItemModel> pOrderColorItemList);
        void UpdateProcessOrder(SuspeSys.Domain.ProcessOrder pOrder, IList<ProcessOrderColorItemModel> pOrderColorItemList);
        void AddProcessOrderFlow(DaoModel.ProcessOrder pOrder, DaoModel.ProcessFlowVersion pfVersion, IList<ProcessFlow> pfList);
        void UpdateProcessOrderFlow(DaoModel.ProcessOrder pOrder, DaoModel.ProcessFlowVersion pfVersion, IList<ProcessFlow> pfList);
        /// <summary>
        /// 复制制单
        /// </summary>
        /// <param name="copyProcessOrder"></param>
        /// <param name="copyProcessOrderItem"></param>
        /// <param name="copyProcessFlow"></param>
        /// <param name="copyProcessFlowChart"></param>
        void CopyProcessOrderInfo(string pOrderId,bool copyProcessOrder, bool copyProcessOrderItem, bool copyProcessFlow, bool copyProcessFlowChart);
    }
}

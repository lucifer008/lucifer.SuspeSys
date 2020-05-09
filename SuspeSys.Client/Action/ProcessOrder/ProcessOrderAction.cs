using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.ProcessOrder
{
    public class ProcessOrderAction: BaseAction
    {
        public ProcessOrderModel Model = new ProcessOrderModel();
       // private ProcessOrderServiceImpl processOrderSerice = new ProcessOrderServiceImpl();
        public void AddProcessOrder()
        {
            processOrderSerice.AddProcessOrder(Model.ProcessOrder, Model.ProcessOrderItemList);
        }
        public void UpdateProcessOrder()
        {
            processOrderSerice.UpdateProcessOrder(Model.ProcessOrder, Model.ProcessOrderItemList);
        }

        /// <summary>
        /// 复制制单
        /// </summary>
        public void CopyProcessOrderInfo() {
            processOrderSerice.CopyProcessOrderInfo(Model.ProcessOrderId,Model.CopyProcessOrder,Model.CopyProcessOrderItem,Model.CopyProcessFlow,Model.CopyProcessFlowChart);
        }
    }
}

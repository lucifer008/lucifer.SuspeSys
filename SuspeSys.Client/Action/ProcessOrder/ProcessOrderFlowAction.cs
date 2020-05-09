using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.ProcessOrder
{
   public class ProcessOrderFlowAction : BaseAction
    {
        public ProcessFlowModel Model = new ProcessFlowModel();
       
        public void AddProcessOrderFlow()
        {
            processOrderSerice.AddProcessOrderFlow(Model.ProcessOrder, Model.ProcessFlowVersion, Model.ProcessFlowList);
        }
        public void UpdateProcessOrderFlow()
        {
            processOrderSerice.UpdateProcessOrderFlow(Model.ProcessOrder, Model.ProcessFlowVersion, Model.ProcessFlowList);
        }
    }
}

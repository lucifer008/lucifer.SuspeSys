using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessOrder.Model
{
    public class ProcessFlowModel
    {
        public DaoModel.ProcessOrder ProcessOrder = new DaoModel.ProcessOrder();
        public DaoModel.ProcessFlowVersion ProcessFlowVersion = new DaoModel.ProcessFlowVersion();
        public IList<DaoModel.ProcessFlow> ProcessFlowList = new List<DaoModel.ProcessFlow>();
    }
}

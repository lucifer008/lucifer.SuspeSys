using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessOrder.Model
{
    public class ProcessOrderQueryModel
    {
        public IList<DaoModel.ProcessOrderModel> ProcessOrderList { set; get; }
        public IList<DaoModel.ProcessOrderColorItem> ProcessOrderColorItemList = new List<DaoModel.ProcessOrderColorItem>();
        public IList<DaoModel.ProcessOrderColorItemModel> ProcessOrderColorSizeItemList= new List<DaoModel.ProcessOrderColorItemModel>();
        public IList<DaoModel.BasicProcessFlowModel> BasicProcessFlowModelList = new List<DaoModel.BasicProcessFlowModel>();
    }
}

using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.ProcessOrder;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessOrder
{
    public class ProcessOrderQueryAction : BaseAction
    {
        public ProcessOrderQueryModel Model = new ProcessOrderQueryModel();
        
        public IList<DaoModel.ProcessOrder> SearchProcessOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, DaoModel.ProcessOrderModel conModel) {
            return processOrderQuery.SearchProcessOrder(currentPageIndex,pageSize,out totalCount,ordercondition,conModel);
        }
        public void SearchProcessOrder(string orderId = null)
        {
            if (null == orderId)
            {
                Model.ProcessOrderList = processOrderQuery.SearchProcessOrder();
                return;
            }
        }

        public DaoModel.ProcessOrder GetProcessOrder(string pOrderId)
        {
            return processOrderQuery.GetProcessOrder(pOrderId);
        }
        public void GetProcessOrderItem(string pOrderId)
        {
            Model.ProcessOrderColorSizeItemList = processOrderQuery.GetProcessOrderItem(pOrderId);
        }
        public void GetProcessOrderStyleFlow(string styleNo)
        {
            var list = processOrderQuery.GetProcessOrderStyleFlow(styleNo);
            var rst = new List<DaoModel.BasicProcessFlowModel>();
            foreach (var m in list)
            {
                var t = BeanUitls<DaoModel.BasicProcessFlowModel, DaoModel.StyleProcessFlowSectionItemModel>.Mapper(m);
                t.DefaultFlowNo = m.FlowNo;
                t.FlowId = m.BASICPROCESSFLOW_Id;
                rst.Add(t);
            }

            Model.BasicProcessFlowModelList = rst; //processOrderQuery.GetProcessOrderStyleFlow(styleNo);
        }
        public IList<DaoModel.ProcessFlowVersion> GetProcessOrderFlowVersionList(string processOrderId)
        {
            return processOrderQuery.GetProcessOrderFlowVersionList(processOrderId);
        }
        public IList<DaoModel.ProcessFlow> GetProcessOrderFlowList(string processFlowVersionId)
        {
            var list = processOrderQuery.GetProcessOrderFlowList(processFlowVersionId).ToList<DaoModel.ProcessFlow>();
            list.ForEach(f=>f.IsProcessVersionFlow=true);
            return list;
        }
        public IList<DaoModel.ProcessFlowVersionModel> GetProcessOrderFlowVersionList()
        {
            return processOrderQuery.GetProcessOrderFlowVersionList();
        }
        public IList<DaoModel.StatingModel> GetGroupStatingList(string groups)
        {
            return processOrderQuery.GetGroupStatingList(groups);
        }
        public string GetCurrentMaxProcessFlowVersionNo(string processOrderId)
        {
            return processOrderQuery.GetCurrentMaxProcessFlowVersionNo(processOrderId);
        }
        public bool CheckProcessOrderNoIsExist(string pOrderNo)
        {
            return processOrderQuery.CheckProcessOrderNoIsExist(pOrderNo);
        }
        public bool CheckProcessOrderIsProducts(string processOrderId) {
            return processOrderQuery.CheckProcessOrderIsProducts(processOrderId);
        }
    }
}

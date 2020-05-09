using SuspeSys.Client.Action.CustPurchaseOrder.Model;
using SuspeSys.Service.Impl.CustPurchaseOrder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.CustPurchaseOrder
{
   public class CustPurchaseOrderAction: BaseAction
    {
        public CustPurchaseOrderModel Model = new CustPurchaseOrderModel();
        
        public void AddCustPurchaseOrder() {
            custPurchaseOrderService.AddCustPurchaseOrder(Model.CustomerPurchaseOrder,Model.CustomerPurchaseOrderColorItemModelList);
        }
        public void UpdateCustPurchaseOrder()
        {
            custPurchaseOrderService.UpdateCustPurchaseOrder(Model.CustomerPurchaseOrder, Model.CustomerPurchaseOrderColorItemModelList);
        }

        public DaoModel.CustomerPurchaseOrder GetCustPurchaseOrder(string cpOrderId)
        {
           return custPurchaseOrderService.GetCustPurchaseOrder(cpOrderId);
        }

        internal void GetCustomerOrderItem(string id)
        {
            Model.CustomerPurchaseOrderColorItemModelList = custPurchaseOrderService.GetCustomerOrderItem(id);
        }
        public string GeneratorOrderNo() {
            return null;
        }
        internal IList<SuspeSys.Domain.CustomerPurchaseOrder> SearchCustomerOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return custPurchaseOrderService.SearchCustomerOrder(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }
    }
}

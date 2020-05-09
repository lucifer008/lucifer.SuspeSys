using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
using System.Collections;
using SuspeSys.Service.CustPurchaseOrder;

namespace SuspeSys.Service.CustPurchaseOrder
{
    /// <summary>
    /// 客户订单服务
    /// </summary>
    public interface ICustPurchaseOrderService
    {
        void AddCustPurchaseOrder(CustomerPurchaseOrder customerPurchaseOrder, IList<CustomerPurchaseOrderColorItemModel> cPOrderColorItemList);
        List<CustomerPurchaseOrderColorItemModel> GetCustomerOrderItem(string id);
        void UpdateCustPurchaseOrder(CustomerPurchaseOrder customerPurchaseOrder, IList<CustomerPurchaseOrderColorItemModel> cPOrderColorItemList);

        DaoModel.CustomerPurchaseOrder GetCustPurchaseOrder(string cpOrderId);
        IList<CustomerPurchaseOrder> SearchCustomerOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
    }
}

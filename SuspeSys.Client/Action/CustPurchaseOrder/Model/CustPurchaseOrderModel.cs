using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.CustPurchaseOrder.Model
{
    public class CustPurchaseOrderModel
    {
        public DaoModel.CustomerPurchaseOrder CustomerPurchaseOrder = new DaoModel.CustomerPurchaseOrder();
        public IList<DaoModel.CustomerPurchaseOrderColorItemModel> CPOrderColorItemList = new List<DaoModel.CustomerPurchaseOrderColorItemModel>();

        public List<DaoModel.CustomerPurchaseOrderColorItemModel> CustomerPurchaseOrderColorItemModelList = new List<DaoModel.CustomerPurchaseOrderColorItemModel>();

       // public IList<DaoModel.CustomerPurchaseOrderColorItemModel> CustomerOrderColorSizeItemList { get; internal set; }
    }
}

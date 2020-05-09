using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 客户外贸订单明细颜色 扩展Model
    /// </summary>
    public class CustomerPurchaseOrderColorItemModel : CustomerPurchaseOrderColorItem {

        public IList<CustomerPurchaseOrderColorSizeItem> cpOrderColorSizeItemList = new List<CustomerPurchaseOrderColorSizeItem>();

        public IList<CustomerPurchaseOrderColorSizeItem> CustomerPurchaseOrderColorSizeItemList = new List<CustomerPurchaseOrderColorSizeItem>();
    }
}

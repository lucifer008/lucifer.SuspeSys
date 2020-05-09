using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 客户外贸订单明细颜色Dao
    /// </summary>
    public class CustomerPurchaseOrderColorItemDao : DataBase<CustomerPurchaseOrderColorItem> {
        private static readonly CustomerPurchaseOrderColorItemDao customerpurchaseordercoloritemDao=new CustomerPurchaseOrderColorItemDao();
        private CustomerPurchaseOrderColorItemDao() { }
        public static  CustomerPurchaseOrderColorItemDao Instance {
            get {
                return  customerpurchaseordercoloritemDao;
            }
        }
    }
}

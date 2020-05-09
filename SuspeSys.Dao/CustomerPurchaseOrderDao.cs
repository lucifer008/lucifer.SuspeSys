using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 客户外贸单Dao
    /// </summary>
    public class CustomerPurchaseOrderDao : DataBase<CustomerPurchaseOrder> {
        private static readonly CustomerPurchaseOrderDao customerpurchaseorderDao=new CustomerPurchaseOrderDao();
        private CustomerPurchaseOrderDao() { }
        public static  CustomerPurchaseOrderDao Instance {
            get {
                return  customerpurchaseorderDao;
            }
        }
    }
}

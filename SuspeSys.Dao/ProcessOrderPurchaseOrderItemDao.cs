using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessOrderPurchaseOrderItemDao : DataBase<ProcessOrderPurchaseOrderItem> {
        private static readonly ProcessOrderPurchaseOrderItemDao processorderpurchaseorderitemDao=new ProcessOrderPurchaseOrderItemDao();
        private ProcessOrderPurchaseOrderItemDao() { }
        public static  ProcessOrderPurchaseOrderItemDao Instance {
            get {
                return  processorderpurchaseorderitemDao;
            }
        }
    }
}

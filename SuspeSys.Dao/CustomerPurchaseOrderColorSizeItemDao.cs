using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 客户外贸订单明细颜色尺码明细Dao
    /// </summary>
    public class CustomerPurchaseOrderColorSizeItemDao : DataBase<CustomerPurchaseOrderColorSizeItem> {
        private static readonly CustomerPurchaseOrderColorSizeItemDao customerpurchaseordercolorsizeitemDao=new CustomerPurchaseOrderColorSizeItemDao();
        private CustomerPurchaseOrderColorSizeItemDao() { }
        public static  CustomerPurchaseOrderColorSizeItemDao Instance {
            get {
                return  customerpurchaseordercolorsizeitemDao;
            }
        }
    }
}

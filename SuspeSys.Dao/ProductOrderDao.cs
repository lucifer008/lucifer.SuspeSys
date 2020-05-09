using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 生产加工单Dao
    /// </summary>
    public class ProductOrderDao : DataBase<ProductOrder> {
        private static readonly ProductOrderDao productorderDao=new ProductOrderDao();
        private ProductOrderDao() { }
        public static  ProductOrderDao Instance {
            get {
                return  productorderDao;
            }
        }
    }
}

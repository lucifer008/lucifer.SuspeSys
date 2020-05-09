using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 加工单产品明细Dao
    /// </summary>
    public class OrderProductItemDao : DataBase<OrderProductItem> {
        private static readonly OrderProductItemDao orderproductitemDao=new OrderProductItemDao();
        private OrderProductItemDao() { }
        public static  OrderProductItemDao Instance {
            get {
                return  orderproductitemDao;
            }
        }
    }
}

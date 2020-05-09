using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 订单Dao
    /// </summary>
    public class OrderDao : DataBase<Order> {
        private static readonly OrderDao orderDao=new OrderDao();
        private OrderDao() { }
        public  OrderDao Instance {
            get {
                return  orderDao;
            }
        }
    }
}

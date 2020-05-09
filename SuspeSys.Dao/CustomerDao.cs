using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 客户Dao
    /// </summary>
    public class CustomerDao : DataBase<Customer> {
        private static readonly CustomerDao customerDao=new CustomerDao();
        private CustomerDao() { }
        public static  CustomerDao Instance {
            get {
                return  customerDao;
            }
        }
    }
}

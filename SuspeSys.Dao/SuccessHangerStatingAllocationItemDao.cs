using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成衣架站点分配明细Dao
    /// </summary>
    public class SuccessHangerStatingAllocationItemDao : DataBase<SuccessHangerStatingAllocationItem> {
        private static readonly SuccessHangerStatingAllocationItemDao successhangerstatingallocationitemDao=new SuccessHangerStatingAllocationItemDao();
        private SuccessHangerStatingAllocationItemDao() { }
        public static  SuccessHangerStatingAllocationItemDao Instance {
            get {
                return  successhangerstatingallocationitemDao;
            }
        }
    }
}

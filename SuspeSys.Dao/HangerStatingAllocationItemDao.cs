using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架站点分配明细Dao
    /// </summary>
    public class HangerStatingAllocationItemDao : DataBase<HangerStatingAllocationItem> {
        private static readonly HangerStatingAllocationItemDao hangerstatingallocationitemDao=new HangerStatingAllocationItemDao();
        private HangerStatingAllocationItemDao() { }
        public static  HangerStatingAllocationItemDao Instance {
            get {
                return  hangerstatingallocationitemDao;
            }
        }
    }
}

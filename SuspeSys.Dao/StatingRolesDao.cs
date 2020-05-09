using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 站点角色Dao
    /// </summary>
    public class StatingRolesDao : DataBase<StatingRoles> {
        private static readonly StatingRolesDao statingrolesDao=new StatingRolesDao();
        private StatingRolesDao() { }
        public static  StatingRolesDao Instance {
            get {
                return  statingrolesDao;
            }
        }
    }
}

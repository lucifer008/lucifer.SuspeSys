using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 权限的角色Dao
    /// </summary>
    public class RolesDao : DataBase<Roles> {
        private static readonly RolesDao rolesDao=new RolesDao();
        private RolesDao() { }
        public static  RolesDao Instance {
            get {
                return  rolesDao;
            }
        }
    }
}

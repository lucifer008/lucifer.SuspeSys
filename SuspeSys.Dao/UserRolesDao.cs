using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 用户角色表Dao
    /// </summary>
    public class UserRolesDao : DataBase<UserRoles> {
        private static readonly UserRolesDao userrolesDao=new UserRolesDao();
        private UserRolesDao() { }
        public static  UserRolesDao Instance {
            get {
                return  userrolesDao;
            }
        }
    }
}

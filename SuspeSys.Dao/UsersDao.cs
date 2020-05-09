using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class UsersDao : DataBase<Users> {
        private static readonly UsersDao usersDao=new UsersDao();
        private UsersDao() { }
        public static  UsersDao Instance {
            get {
                return  usersDao;
            }
        }
    }
}

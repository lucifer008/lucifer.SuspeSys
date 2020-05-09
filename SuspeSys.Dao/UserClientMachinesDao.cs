using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class UserClientMachinesDao : DataBase<UserClientMachines> {
        private static readonly UserClientMachinesDao userclientmachinesDao=new UserClientMachinesDao();
        private UserClientMachinesDao() { }
        public static  UserClientMachinesDao Instance {
            get {
                return  userclientmachinesDao;
            }
        }
    }
}

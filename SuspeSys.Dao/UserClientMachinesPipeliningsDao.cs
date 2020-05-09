using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 用户客户机流水线Dao
    /// </summary>
    public class UserClientMachinesPipeliningsDao : DataBase<UserClientMachinesPipelinings> {
        private static readonly UserClientMachinesPipeliningsDao userclientmachinespipeliningsDao=new UserClientMachinesPipeliningsDao();
        private UserClientMachinesPipeliningsDao() { }
        public static  UserClientMachinesPipeliningsDao Instance {
            get {
                return  userclientmachinespipeliningsDao;
            }
        }
    }
}

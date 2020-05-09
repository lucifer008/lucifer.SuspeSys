using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class UserLoginInfoDao : DataBase<UserLoginInfo> {
        private static readonly UserLoginInfoDao userlogininfoDao=new UserLoginInfoDao();
        private UserLoginInfoDao() { }
        public static  UserLoginInfoDao Instance {
            get {
                return  userlogininfoDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class UserOperateLogDetailDao : DataBase<UserOperateLogDetail> {
        private static readonly UserOperateLogDetailDao useroperatelogdetailDao=new UserOperateLogDetailDao();
        private UserOperateLogDetailDao() { }
        public static  UserOperateLogDetailDao Instance {
            get {
                return  useroperatelogdetailDao;
            }
        }
    }
}

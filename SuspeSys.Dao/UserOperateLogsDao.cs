using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 用户操作日志Dao
    /// </summary>
    public class UserOperateLogsDao : DataBase<UserOperateLogs> {
        private static readonly UserOperateLogsDao useroperatelogsDao=new UserOperateLogsDao();
        private UserOperateLogsDao() { }
        public static  UserOperateLogsDao Instance {
            get {
                return  useroperatelogsDao;
            }
        }
    }
}

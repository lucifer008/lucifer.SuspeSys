using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣车登录日志Dao
    /// </summary>
    public class SewingMachineLoginLogDao : DataBase<SewingMachineLoginLog> {
        private static readonly SewingMachineLoginLogDao sewingmachineloginlogDao=new SewingMachineLoginLogDao();
        private SewingMachineLoginLogDao() { }
        public static  SewingMachineLoginLogDao Instance {
            get {
                return  sewingmachineloginlogDao;
            }
        }
    }
}

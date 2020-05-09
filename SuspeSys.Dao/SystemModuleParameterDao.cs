using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 系统参数Dao
    /// </summary>
    public class SystemModuleParameterDao : DataBase<SystemModuleParameter> {
        private static readonly SystemModuleParameterDao systemmoduleparameterDao=new SystemModuleParameterDao();
        private SystemModuleParameterDao() { }
        public static  SystemModuleParameterDao Instance {
            get {
                return  systemmoduleparameterDao;
            }
        }
    }
}

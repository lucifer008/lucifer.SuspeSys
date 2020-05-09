using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class SystemModuleParameterValueDao : DataBase<SystemModuleParameterValue> {
        private static readonly SystemModuleParameterValueDao systemmoduleparametervalueDao=new SystemModuleParameterValueDao();
        private SystemModuleParameterValueDao() { }
        public static  SystemModuleParameterValueDao Instance {
            get {
                return  systemmoduleparametervalueDao;
            }
        }
    }
}

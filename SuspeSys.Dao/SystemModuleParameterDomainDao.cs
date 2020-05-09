using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class SystemModuleParameterDomainDao : DataBase<SystemModuleParameterDomain> {
        private static readonly SystemModuleParameterDomainDao systemmoduleparameterdomainDao=new SystemModuleParameterDomainDao();
        private SystemModuleParameterDomainDao() { }
        public static  SystemModuleParameterDomainDao Instance {
            get {
                return  systemmoduleparameterdomainDao;
            }
        }
    }
}

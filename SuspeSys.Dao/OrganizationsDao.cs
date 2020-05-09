using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 组织机构Dao
    /// </summary>
    public class OrganizationsDao : DataBase<Organizations> {
        private static readonly OrganizationsDao organizationsDao=new OrganizationsDao();
        private OrganizationsDao() { }
        public static  OrganizationsDao Instance {
            get {
                return  organizationsDao;
            }
        }
    }
}

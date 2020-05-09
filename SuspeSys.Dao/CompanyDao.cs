using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class CompanyDao : DataBase<Company> {
        private static readonly CompanyDao companyDao=new CompanyDao();
        private CompanyDao() { }
        public static  CompanyDao Instance {
            get {
                return  companyDao;
            }
        }
    }
}

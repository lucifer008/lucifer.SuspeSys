using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class EmployeeRoleRelationDao : DataBase<EmployeeRoleRelation> {
        private static readonly EmployeeRoleRelationDao employeerolerelationDao=new EmployeeRoleRelationDao();
        private EmployeeRoleRelationDao() { }
        public static  EmployeeRoleRelationDao Instance {
            get {
                return  employeerolerelationDao;
            }
        }
    }
}

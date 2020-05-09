using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工卡关系Dao
    /// </summary>
    public class EmployeeCardRelationDao : DataBase<EmployeeCardRelation> {
        private static readonly EmployeeCardRelationDao employeecardrelationDao=new EmployeeCardRelationDao();
        private EmployeeCardRelationDao() { }
        public static  EmployeeCardRelationDao Instance {
            get {
                return  employeecardrelationDao;
            }
        }
    }
}

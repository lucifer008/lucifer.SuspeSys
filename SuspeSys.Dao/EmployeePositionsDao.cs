using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工职务Dao
    /// </summary>
    public class EmployeePositionsDao : DataBase<EmployeePositions> {
        private static readonly EmployeePositionsDao employeepositionsDao=new EmployeePositionsDao();
        private EmployeePositionsDao() { }
        public static  EmployeePositionsDao Instance {
            get {
                return  employeepositionsDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工Dao
    /// </summary>
    public class EmployeeDao : DataBase<Employee> {
        private static readonly EmployeeDao employeeDao=new EmployeeDao();
        private EmployeeDao() { }
        public static  EmployeeDao Instance {
            get {
                return  employeeDao;
            }
        }
    }
}

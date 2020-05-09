using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工排班信息Dao
    /// </summary>
    public class EmployeeSchedulingDao : DataBase<EmployeeScheduling> {
        private static readonly EmployeeSchedulingDao employeeschedulingDao=new EmployeeSchedulingDao();
        private EmployeeSchedulingDao() { }
        public static  EmployeeSchedulingDao Instance {
            get {
                return  employeeschedulingDao;
            }
        }
    }
}

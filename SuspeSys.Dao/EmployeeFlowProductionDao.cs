using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工工序产量明细Dao
    /// </summary>
    public class EmployeeFlowProductionDao : DataBase<EmployeeFlowProduction> {
        private static readonly EmployeeFlowProductionDao employeeflowproductionDao=new EmployeeFlowProductionDao();
        private EmployeeFlowProductionDao() { }
        public static  EmployeeFlowProductionDao Instance {
            get {
                return  employeeflowproductionDao;
            }
        }
    }
}

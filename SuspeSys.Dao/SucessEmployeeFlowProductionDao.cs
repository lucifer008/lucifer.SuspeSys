using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成员工工序产量明细Dao
    /// </summary>
    public class SucessEmployeeFlowProductionDao : DataBase<SucessEmployeeFlowProduction> {
        private static readonly SucessEmployeeFlowProductionDao sucessemployeeflowproductionDao=new SucessEmployeeFlowProductionDao();
        private SucessEmployeeFlowProductionDao() { }
        public static  SucessEmployeeFlowProductionDao Instance {
            get {
                return  sucessemployeeflowproductionDao;
            }
        }
    }
}

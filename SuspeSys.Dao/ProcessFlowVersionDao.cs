using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制单工序版本Dao
    /// </summary>
    public class ProcessFlowVersionDao : DataBase<ProcessFlowVersion> {
        private static readonly ProcessFlowVersionDao processflowversionDao=new ProcessFlowVersionDao();
        private ProcessFlowVersionDao() { }
        public static  ProcessFlowVersionDao Instance {
            get {
                return  processflowversionDao;
            }
        }
    }
}

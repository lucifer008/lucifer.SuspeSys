using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制单工序Dao
    /// </summary>
    public class ProcessFlowDao : DataBase<ProcessFlow> {
        private static readonly ProcessFlowDao processflowDao=new ProcessFlowDao();
        private ProcessFlowDao() { }
        public static  ProcessFlowDao Instance {
            get {
                return  processflowDao;
            }
        }
    }
}

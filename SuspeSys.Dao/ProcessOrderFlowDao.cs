using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessOrderFlowDao : DataBase<ProcessOrderFlow> {
        private static readonly ProcessOrderFlowDao processorderflowDao=new ProcessOrderFlowDao();
        private ProcessOrderFlowDao() { }
        public  ProcessOrderFlowDao Instance {
            get {
                return  processorderflowDao;
            }
        }
    }
}

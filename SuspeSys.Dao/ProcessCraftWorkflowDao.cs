using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessCraftWorkflowDao : DataBase<ProcessCraftWorkflow> {
        private static readonly ProcessCraftWorkflowDao processcraftworkflowDao=new ProcessCraftWorkflowDao();
        private ProcessCraftWorkflowDao() { }
        public  ProcessCraftWorkflowDao Instance {
            get {
                return  processcraftworkflowDao;
            }
        }
    }
}

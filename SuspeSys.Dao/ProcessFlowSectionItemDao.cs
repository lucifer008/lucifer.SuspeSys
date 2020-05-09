using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessFlowSectionItemDao : DataBase<ProcessFlowSectionItem> {
        private static readonly ProcessFlowSectionItemDao processflowsectionitemDao=new ProcessFlowSectionItemDao();
        private ProcessFlowSectionItemDao() { }
        public static  ProcessFlowSectionItemDao Instance {
            get {
                return  processflowsectionitemDao;
            }
        }
    }
}

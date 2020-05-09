using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工序段Dao
    /// </summary>
    public class ProcessFlowSectionDao : DataBase<ProcessFlowSection> {
        private static readonly ProcessFlowSectionDao processflowsectionDao=new ProcessFlowSectionDao();
        private ProcessFlowSectionDao() { }
        public static  ProcessFlowSectionDao Instance {
            get {
                return  processflowsectionDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工序制作站点明细Dao
    /// </summary>
    public class ProcessFlowStatingItemDao : DataBase<ProcessFlowStatingItem> {
        private static readonly ProcessFlowStatingItemDao processflowstatingitemDao=new ProcessFlowStatingItemDao();
        private ProcessFlowStatingItemDao() { }
        public static  ProcessFlowStatingItemDao Instance {
            get {
                return  processflowstatingitemDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessOrderItemDao : DataBase<ProcessOrderItem> {
        private static readonly ProcessOrderItemDao processorderitemDao=new ProcessOrderItemDao();
        private ProcessOrderItemDao() { }
        public static  ProcessOrderItemDao Instance {
            get {
                return  processorderitemDao;
            }
        }
    }
}

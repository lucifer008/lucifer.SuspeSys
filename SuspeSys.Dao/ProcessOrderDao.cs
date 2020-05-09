using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制单Dao
    /// </summary>
    public class ProcessOrderDao : DataBase<ProcessOrder> {
        private static readonly ProcessOrderDao processorderDao=new ProcessOrderDao();
        private ProcessOrderDao() { }
        public static  ProcessOrderDao Instance {
            get {
                return  processorderDao;
            }
        }
    }
}

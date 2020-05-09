using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制单明细颜色Dao
    /// </summary>
    public class ProcessOrderColorItemDao : DataBase<ProcessOrderColorItem> {
        private static readonly ProcessOrderColorItemDao processordercoloritemDao=new ProcessOrderColorItemDao();
        private ProcessOrderColorItemDao() { }
        public static  ProcessOrderColorItemDao Instance {
            get {
                return  processordercoloritemDao;
            }
        }
    }
}

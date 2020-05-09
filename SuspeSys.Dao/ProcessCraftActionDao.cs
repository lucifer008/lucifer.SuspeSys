using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺图制作动作Dao
    /// </summary>
    public class ProcessCraftActionDao : DataBase<ProcessCraftAction> {
        private static readonly ProcessCraftActionDao processcraftactionDao=new ProcessCraftActionDao();
        private ProcessCraftActionDao() { }
        public static  ProcessCraftActionDao Instance {
            get {
                return  processcraftactionDao;
            }
        }
    }
}

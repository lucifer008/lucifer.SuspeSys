using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工种信息Dao
    /// </summary>
    public class WorkTypeDao : DataBase<WorkType> {
        private static readonly WorkTypeDao worktypeDao=new WorkTypeDao();
        private WorkTypeDao() { }
        public static  WorkTypeDao Instance {
            get {
                return  worktypeDao;
            }
        }
    }
}

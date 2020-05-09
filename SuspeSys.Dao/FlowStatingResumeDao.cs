using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工序制作站变更履历Dao
    /// </summary>
    public class FlowStatingResumeDao : DataBase<FlowStatingResume> {
        private static readonly FlowStatingResumeDao flowstatingresumeDao=new FlowStatingResumeDao();
        private FlowStatingResumeDao() { }
        public static  FlowStatingResumeDao Instance {
            get {
                return  flowstatingresumeDao;
            }
        }
    }
}

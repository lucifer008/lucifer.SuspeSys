using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架返工请求队列Dao
    /// </summary>
    public class HangerReworkRequestQueueDao : DataBase<HangerReworkRequestQueue> {
        private static readonly HangerReworkRequestQueueDao hangerreworkrequestqueueDao=new HangerReworkRequestQueueDao();
        private HangerReworkRequestQueueDao() { }
        public static  HangerReworkRequestQueueDao Instance {
            get {
                return  hangerreworkrequestqueueDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架返工请求明细队列明细Dao
    /// </summary>
    public class HangerReworkRequestItemDao : DataBase<HangerReworkRequestItem> {
        private static readonly HangerReworkRequestItemDao hangerreworkrequestitemDao=new HangerReworkRequestItemDao();
        private HangerReworkRequestItemDao() { }
        public static  HangerReworkRequestItemDao Instance {
            get {
                return  hangerreworkrequestitemDao;
            }
        }
    }
}

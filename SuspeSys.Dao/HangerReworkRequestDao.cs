using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架返工请求Dao
    /// </summary>
    public class HangerReworkRequestDao : DataBase<HangerReworkRequest> {
        private static readonly HangerReworkRequestDao hangerreworkrequestDao=new HangerReworkRequestDao();
        private HangerReworkRequestDao() { }
        public static  HangerReworkRequestDao Instance {
            get {
                return  hangerreworkrequestDao;
            }
        }
    }
}

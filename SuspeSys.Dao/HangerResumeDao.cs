using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架轨迹Dao
    /// </summary>
    public class HangerResumeDao : DataBase<HangerResume> {
        private static readonly HangerResumeDao hangerresumeDao=new HangerResumeDao();
        private HangerResumeDao() { }
        public static  HangerResumeDao Instance {
            get {
                return  hangerresumeDao;
            }
        }
    }
}

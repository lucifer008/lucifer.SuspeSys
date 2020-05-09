using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// LED显示屏页面Dao
    /// </summary>
    public class LedScreenPageDao : DataBase<LedScreenPage> {
        private static readonly LedScreenPageDao ledscreenpageDao=new LedScreenPageDao();
        private LedScreenPageDao() { }
        public static  LedScreenPageDao Instance {
            get {
                return  ledscreenpageDao;
            }
        }
    }
}

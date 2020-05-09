using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// LED屏配置Dao
    /// </summary>
    public class LedScreenConfigDao : DataBase<LedScreenConfig> {
        private static readonly LedScreenConfigDao ledscreenconfigDao=new LedScreenConfigDao();
        private LedScreenConfigDao() { }
        public static  LedScreenConfigDao Instance {
            get {
                return  ledscreenconfigDao;
            }
        }
    }
}

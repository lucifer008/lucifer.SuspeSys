using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 主轨Dao
    /// </summary>
    public class MainTrackDao : DataBase<MainTrack> {
        private static readonly MainTrackDao maintrackDao=new MainTrackDao();
        private MainTrackDao() { }
        public static  MainTrackDao Instance {
            get {
                return  maintrackDao;
            }
        }
    }
}

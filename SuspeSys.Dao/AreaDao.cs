using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 地区Dao
    /// </summary>
    public class AreaDao : DataBase<Area> {
        private static readonly AreaDao areaDao=new AreaDao();
        private AreaDao() { }
        public static  AreaDao Instance {
            get {
                return  areaDao;
            }
        }
    }
}

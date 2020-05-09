using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 职务Dao
    /// </summary>
    public class PositionDao : DataBase<Position> {
        private static readonly PositionDao positionDao=new PositionDao();
        private PositionDao() { }
        public static  PositionDao Instance {
            get {
                return  positionDao;
            }
        }
    }
}

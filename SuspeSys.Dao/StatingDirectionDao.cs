using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 站点方向Dao
    /// </summary>
    public class StatingDirectionDao : DataBase<StatingDirection> {
        private static readonly StatingDirectionDao statingdirectionDao=new StatingDirectionDao();
        private StatingDirectionDao() { }
        public static  StatingDirectionDao Instance {
            get {
                return  statingdirectionDao;
            }
        }
    }
}

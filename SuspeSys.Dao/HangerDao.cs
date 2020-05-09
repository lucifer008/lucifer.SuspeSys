using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架Dao
    /// </summary>
    public class HangerDao : DataBase<Hanger> {
        private static readonly HangerDao hangerDao=new HangerDao();
        private HangerDao() { }
        public static  HangerDao Instance {
            get {
                return  hangerDao;
            }
        }
    }
}

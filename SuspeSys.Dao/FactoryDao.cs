using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工厂Dao
    /// </summary>
    public class FactoryDao : DataBase<Factory> {
        private static readonly FactoryDao factoryDao=new FactoryDao();
        private FactoryDao() { }
        public static  FactoryDao Instance {
            get {
                return  factoryDao;
            }
        }
    }
}

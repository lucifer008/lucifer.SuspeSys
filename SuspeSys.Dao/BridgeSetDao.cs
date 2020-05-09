using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 桥接配置Dao
    /// </summary>
    public class BridgeSetDao : DataBase<BridgeSet> {
        private static readonly BridgeSetDao bridgesetDao=new BridgeSetDao();
        private BridgeSetDao() { }
        public static  BridgeSetDao Instance {
            get {
                return  bridgesetDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 尺码Dao
    /// </summary>
    public class PSizeDao : DataBase<PSize> {
        private static readonly PSizeDao psizeDao=new PSizeDao();
        private PSizeDao() { }
        public static  PSizeDao Instance {
            get {
                return  psizeDao;
            }
        }
    }
}

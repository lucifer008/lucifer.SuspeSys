using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 流水线Dao
    /// </summary>
    public class PipeliningDao : DataBase<Pipelining> {
        private static readonly PipeliningDao pipeliningDao=new PipeliningDao();
        private PipeliningDao() { }
        public static  PipeliningDao Instance {
            get {
                return  pipeliningDao;
            }
        }
    }
}

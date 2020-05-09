using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 故障代码表Dao
    /// </summary>
    public class FaultCodeTableDao : DataBase<FaultCodeTable> {
        private static readonly FaultCodeTableDao faultcodetableDao=new FaultCodeTableDao();
        private FaultCodeTableDao() { }
        public static  FaultCodeTableDao Instance {
            get {
                return  faultcodetableDao;
            }
        }
    }
}

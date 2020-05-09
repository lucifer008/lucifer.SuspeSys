using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 产线类别Dao
    /// </summary>
    public class ProdTypeDao : DataBase<ProdType> {
        private static readonly ProdTypeDao prodtypeDao=new ProdTypeDao();
        private ProdTypeDao() { }
        public static  ProdTypeDao Instance {
            get {
                return  prodtypeDao;
            }
        }
    }
}

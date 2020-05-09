using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 生产完成制品Dao
    /// </summary>
    public class SucessProductsDao : DataBase<SucessProducts> {
        private static readonly SucessProductsDao sucessproductsDao=new SucessProductsDao();
        private SucessProductsDao() { }
        public static  SucessProductsDao Instance {
            get {
                return  sucessproductsDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 站点衣架生产明细Dao
    /// </summary>
    public class StatingHangerProductItemDao : DataBase<StatingHangerProductItem> {
        private static readonly StatingHangerProductItemDao statinghangerproductitemDao=new StatingHangerProductItemDao();
        private StatingHangerProductItemDao() { }
        public static  StatingHangerProductItemDao Instance {
            get {
                return  statinghangerproductitemDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架生产明细Dao
    /// </summary>
    public class HangerProductItemDao : DataBase<HangerProductItem> {
        private static readonly HangerProductItemDao hangerproductitemDao=new HangerProductItemDao();
        private HangerProductItemDao() { }
        public static  HangerProductItemDao Instance {
            get {
                return  hangerproductitemDao;
            }
        }
    }
}

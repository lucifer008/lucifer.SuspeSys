using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 生产组别Dao
    /// </summary>
    public class ProductGroupDao : DataBase<ProductGroup> {
        private static readonly ProductGroupDao productgroupDao=new ProductGroupDao();
        private ProductGroupDao() { }
        public static  ProductGroupDao Instance {
            get {
                return  productgroupDao;
            }
        }
    }
}

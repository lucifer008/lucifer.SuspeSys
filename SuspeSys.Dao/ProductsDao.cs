using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制品Dao
    /// </summary>
    public class ProductsDao : DataBase<Products> {
        private static readonly ProductsDao productsDao=new ProductsDao();
        private ProductsDao() { }
        public static  ProductsDao Instance {
            get {
                return  productsDao;
            }
        }
    }
}

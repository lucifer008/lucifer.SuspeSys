using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制品挂片履历Dao
    /// </summary>
    public class ProductsHangPieceResumeDao : DataBase<ProductsHangPieceResume> {
        private static readonly ProductsHangPieceResumeDao productshangpieceresumeDao=new ProductsHangPieceResumeDao();
        private ProductsHangPieceResumeDao() { }
        public static  ProductsHangPieceResumeDao Instance {
            get {
                return  productshangpieceresumeDao;
            }
        }
    }
}

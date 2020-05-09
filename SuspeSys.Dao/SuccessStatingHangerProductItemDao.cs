using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成的站点衣架生产明细Dao
    /// </summary>
    public class SuccessStatingHangerProductItemDao : DataBase<SuccessStatingHangerProductItem> {
        private static readonly SuccessStatingHangerProductItemDao successstatinghangerproductitemDao=new SuccessStatingHangerProductItemDao();
        private SuccessStatingHangerProductItemDao() { }
        public static  SuccessStatingHangerProductItemDao Instance {
            get {
                return  successstatinghangerproductitemDao;
            }
        }
    }
}

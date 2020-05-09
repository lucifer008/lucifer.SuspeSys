using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成的衣架生产明细Dao
    /// </summary>
    public class SucessHangerProductItemDao : DataBase<SucessHangerProductItem> {
        private static readonly SucessHangerProductItemDao sucesshangerproductitemDao=new SucessHangerProductItemDao();
        private SucessHangerProductItemDao() { }
        public static  SucessHangerProductItemDao Instance {
            get {
                return  sucesshangerproductitemDao;
            }
        }
    }
}

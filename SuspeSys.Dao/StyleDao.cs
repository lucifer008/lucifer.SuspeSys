using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 款式工艺Dao
    /// </summary>
    public class StyleDao : DataBase<Style> {
        private static readonly StyleDao styleDao=new StyleDao();
        private StyleDao() { }
        public static  StyleDao Instance {
            get {
                return  styleDao;
            }
        }
    }
}

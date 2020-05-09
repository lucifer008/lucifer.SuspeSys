using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 颜色Dao
    /// </summary>
    public class PoColorDao : DataBase<PoColor> {
        private static readonly PoColorDao pocolorDao=new PoColorDao();
        private PoColorDao() { }
        public static  PoColorDao Instance {
            get {
                return  pocolorDao;
            }
        }
    }
}

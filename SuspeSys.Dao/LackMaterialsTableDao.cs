using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 缺料代码表Dao
    /// </summary>
    public class LackMaterialsTableDao : DataBase<LackMaterialsTable> {
        private static readonly LackMaterialsTableDao lackmaterialstableDao=new LackMaterialsTableDao();
        private LackMaterialsTableDao() { }
        public static  LackMaterialsTableDao Instance {
            get {
                return  lackmaterialstableDao;
            }
        }
    }
}

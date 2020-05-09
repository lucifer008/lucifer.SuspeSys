using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 生产完成制单衣架Dao
    /// </summary>
    public class SucessProcessOrderHangerDao : DataBase<SucessProcessOrderHanger> {
        private static readonly SucessProcessOrderHangerDao sucessprocessorderhangerDao=new SucessProcessOrderHangerDao();
        private SucessProcessOrderHangerDao() { }
        public static  SucessProcessOrderHangerDao Instance {
            get {
                return  sucessprocessorderhangerDao;
            }
        }
    }
}

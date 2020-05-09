using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 待生产制单衣架Dao
    /// </summary>
    public class WaitProcessOrderHangerDao : DataBase<WaitProcessOrderHanger> {
        private static readonly WaitProcessOrderHangerDao waitprocessorderhangerDao=new WaitProcessOrderHangerDao();
        private WaitProcessOrderHangerDao() { }
        public static  WaitProcessOrderHangerDao Instance {
            get {
                return  waitprocessorderhangerDao;
            }
        }
    }
}

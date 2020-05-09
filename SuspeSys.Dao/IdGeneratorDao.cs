using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 号码段的分类
    ///   @needMeta=falseDao
    /// </summary>
    public class IdGeneratorDao : DataBase<IdGenerator> {
        private static readonly IdGeneratorDao idgeneratorDao=new IdGeneratorDao();
        private IdGeneratorDao() { }
        public static  IdGeneratorDao Instance {
            get {
                return  idgeneratorDao;
            }
        }
    }
}

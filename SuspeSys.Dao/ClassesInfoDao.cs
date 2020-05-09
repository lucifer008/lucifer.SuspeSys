using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 班次信息Dao
    /// </summary>
    public class ClassesInfoDao : DataBase<ClassesInfo> {
        private static readonly ClassesInfoDao classesinfoDao=new ClassesInfoDao();
        private ClassesInfoDao() { }
        public static  ClassesInfoDao Instance {
            get {
                return  classesinfoDao;
            }
        }
    }
}

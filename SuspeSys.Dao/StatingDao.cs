using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 站点:
    ///   即一个包括进站、出站功能，有终端显示操作面板的集合，一般安装一名生产员工及一台衣车Dao
    /// </summary>
    public class StatingDao : DataBase<Stating> {
        private static readonly StatingDao statingDao=new StatingDao();
        private StatingDao() { }
        public static  StatingDao Instance {
            get {
                return  statingDao;
            }
        }
    }
}

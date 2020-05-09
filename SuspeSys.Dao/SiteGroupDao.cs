using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 站点组:
    ///   为客户企业管理中的行政划分单位，与吊挂生产线可以是一对一关系，也可以是一对多，多对多的关系，视实际情况安装Dao
    /// </summary>
    public class SiteGroupDao : DataBase<SiteGroup> {
        private static readonly SiteGroupDao sitegroupDao=new SiteGroupDao();
        private SiteGroupDao() { }
        public static  SiteGroupDao Instance {
            get {
                return  sitegroupDao;
            }
        }
    }
}

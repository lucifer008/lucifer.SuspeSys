using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 测试站点Dao
    /// </summary>
    public class TestSiteTableDao : DataBase<TestSiteTable> {
        private static readonly TestSiteTableDao testsitetableDao=new TestSiteTableDao();
        private TestSiteTableDao() { }
        public static  TestSiteTableDao Instance {
            get {
                return  testsitetableDao;
            }
        }
    }
}

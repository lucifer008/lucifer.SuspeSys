using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 语言Dao
    /// </summary>
    public class SusLanguageDao : DataBase<SusLanguage> {
        private static readonly SusLanguageDao suslanguageDao=new SusLanguageDao();
        private SusLanguageDao() { }
        public static  SusLanguageDao Instance {
            get {
                return  suslanguageDao;
            }
        }
    }
}

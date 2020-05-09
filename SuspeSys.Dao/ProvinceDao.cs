using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProvinceDao : DataBase<Province> {
        private static readonly ProvinceDao provinceDao=new ProvinceDao();
        private ProvinceDao() { }
        public static  ProvinceDao Instance {
            get {
                return  provinceDao;
            }
        }
    }
}

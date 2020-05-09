using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class CityDao : DataBase<City> {
        private static readonly CityDao cityDao=new CityDao();
        private CityDao() { }
        public static  CityDao Instance {
            get {
                return  cityDao;
            }
        }
    }
}

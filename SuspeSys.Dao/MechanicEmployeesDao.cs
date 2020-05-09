using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 机修人员表Dao
    /// </summary>
    public class MechanicEmployeesDao : DataBase<MechanicEmployees> {
        private static readonly MechanicEmployeesDao mechanicemployeesDao=new MechanicEmployeesDao();
        private MechanicEmployeesDao() { }
        public static  MechanicEmployeesDao Instance {
            get {
                return  mechanicemployeesDao;
            }
        }
    }
}

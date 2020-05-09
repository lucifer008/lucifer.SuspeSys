using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 车间Dao
    /// </summary>
    public class WorkshopDao : DataBase<Workshop> {
        private static readonly WorkshopDao workshopDao=new WorkshopDao();
        private WorkshopDao() { }
        public static  WorkshopDao Instance {
            get {
                return  workshopDao;
            }
        }
    }
}

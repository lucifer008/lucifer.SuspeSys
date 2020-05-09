using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// LED小时计划表明细Dao
    /// </summary>
    public class LedHoursPlanTableItemDao : DataBase<LedHoursPlanTableItem> {
        private static readonly LedHoursPlanTableItemDao ledhoursplantableitemDao=new LedHoursPlanTableItemDao();
        private LedHoursPlanTableItemDao() { }
        public static  LedHoursPlanTableItemDao Instance {
            get {
                return  ledhoursplantableitemDao;
            }
        }
    }
}

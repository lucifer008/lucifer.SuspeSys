using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 假日信息Dao
    /// </summary>
    public class HolidayInfoDao : DataBase<HolidayInfo> {
        private static readonly HolidayInfoDao holidayinfoDao=new HolidayInfoDao();
        private HolidayInfoDao() { }
        public static  HolidayInfoDao Instance {
            get {
                return  holidayinfoDao;
            }
        }
    }
}

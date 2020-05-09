using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 主轨操作记录Dao
    /// </summary>
    public class MainTrackOperateRecordDao : DataBase<MainTrackOperateRecord> {
        private static readonly MainTrackOperateRecordDao maintrackoperaterecordDao=new MainTrackOperateRecordDao();
        private MainTrackOperateRecordDao() { }
        public static  MainTrackOperateRecordDao Instance {
            get {
                return  maintrackoperaterecordDao;
            }
        }
    }
}

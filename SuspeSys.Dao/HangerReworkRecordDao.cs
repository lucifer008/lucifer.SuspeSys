using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架返工记录Dao
    /// </summary>
    public class HangerReworkRecordDao : DataBase<HangerReworkRecord> {
        private static readonly HangerReworkRecordDao hangerreworkrecordDao=new HangerReworkRecordDao();
        private HangerReworkRecordDao() { }
        public static  HangerReworkRecordDao Instance {
            get {
                return  hangerreworkrecordDao;
            }
        }
    }
}

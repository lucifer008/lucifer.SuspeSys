using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成的衣架返工记录Dao
    /// </summary>
    public class SucessHangerReworkRecordDao : DataBase<SucessHangerReworkRecord> {
        private static readonly SucessHangerReworkRecordDao sucesshangerreworkrecordDao=new SucessHangerReworkRecordDao();
        private SucessHangerReworkRecordDao() { }
        public static  SucessHangerReworkRecordDao Instance {
            get {
                return  sucesshangerreworkrecordDao;
            }
        }
    }
}

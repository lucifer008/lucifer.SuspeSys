using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 换卡履历Dao
    /// </summary>
    public class ChangeCardResumeDao : DataBase<ChangeCardResume> {
        private static readonly ChangeCardResumeDao changecardresumeDao=new ChangeCardResumeDao();
        private ChangeCardResumeDao() { }
        public static  ChangeCardResumeDao Instance {
            get {
                return  changecardresumeDao;
            }
        }
    }
}

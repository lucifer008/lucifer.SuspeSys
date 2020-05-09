using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ProcessStationRelationDao : DataBase<ProcessStationRelation> {
        private static readonly ProcessStationRelationDao processstationrelationDao=new ProcessStationRelationDao();
        private ProcessStationRelationDao() { }
        public  ProcessStationRelationDao Instance {
            get {
                return  processstationrelationDao;
            }
        }
    }
}

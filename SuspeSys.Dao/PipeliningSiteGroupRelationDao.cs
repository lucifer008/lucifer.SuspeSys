using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class PipeliningSiteGroupRelationDao : DataBase<PipeliningSiteGroupRelation> {
        private static readonly PipeliningSiteGroupRelationDao pipeliningsitegrouprelationDao=new PipeliningSiteGroupRelationDao();
        private PipeliningSiteGroupRelationDao() { }
        public static  PipeliningSiteGroupRelationDao Instance {
            get {
                return  pipeliningsitegrouprelationDao;
            }
        }
    }
}

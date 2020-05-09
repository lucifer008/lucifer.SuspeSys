using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺路线图制单工序Dao
    /// </summary>
    public class ProcessFlowChartFlowRelationDao : DataBase<ProcessFlowChartFlowRelation> {
        private static readonly ProcessFlowChartFlowRelationDao processflowchartflowrelationDao=new ProcessFlowChartFlowRelationDao();
        private ProcessFlowChartFlowRelationDao() { }
        public static  ProcessFlowChartFlowRelationDao Instance {
            get {
                return  processflowchartflowrelationDao;
            }
        }
    }
}

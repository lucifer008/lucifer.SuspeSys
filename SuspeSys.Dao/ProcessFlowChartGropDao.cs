using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺路线图使用组Dao
    /// </summary>
    public class ProcessFlowChartGropDao : DataBase<ProcessFlowChartGrop> {
        private static readonly ProcessFlowChartGropDao processflowchartgropDao=new ProcessFlowChartGropDao();
        private ProcessFlowChartGropDao() { }
        public static  ProcessFlowChartGropDao Instance {
            get {
                return  processflowchartgropDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺路线图:
    ///   产品在吊挂生产线的生产工序排序及员工任务分配的数据集合Dao
    /// </summary>
    public class ProcessFlowChartDao : DataBase<ProcessFlowChart> {
        private static readonly ProcessFlowChartDao processflowchartDao=new ProcessFlowChartDao();
        private ProcessFlowChartDao() { }
        public static  ProcessFlowChartDao Instance {
            get {
                return  processflowchartDao;
            }
        }
    }
}

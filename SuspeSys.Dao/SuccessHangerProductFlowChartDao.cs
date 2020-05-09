using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 完成的衣架生产工艺图Dao
    /// </summary>
    public class SuccessHangerProductFlowChartDao : DataBase<SuccessHangerProductFlowChart> {
        private static readonly SuccessHangerProductFlowChartDao successhangerproductflowchartDao=new SuccessHangerProductFlowChartDao();
        private SuccessHangerProductFlowChartDao() { }
        public static  SuccessHangerProductFlowChartDao Instance {
            get {
                return  successhangerproductflowchartDao;
            }
        }
    }
}

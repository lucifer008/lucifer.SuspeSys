using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣架生产工艺图Dao
    /// </summary>
    public class HangerProductFlowChartDao : DataBase<HangerProductFlowChart> {
        private static readonly HangerProductFlowChartDao hangerproductflowchartDao=new HangerProductFlowChartDao();
        private HangerProductFlowChartDao() { }
        public static  HangerProductFlowChartDao Instance {
            get {
                return  hangerproductflowchartDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 款式工序明细Dao
    /// </summary>
    public class StyleProcessFlowItemDao : DataBase<StyleProcessFlowItem> {
        private static readonly StyleProcessFlowItemDao styleprocessflowitemDao=new StyleProcessFlowItemDao();
        private StyleProcessFlowItemDao() { }
        public static  StyleProcessFlowItemDao Instance {
            get {
                return  styleprocessflowitemDao;
            }
        }
    }
}

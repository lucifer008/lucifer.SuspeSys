using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工序段工序明细Dao
    /// </summary>
    public class StyleProcessFlowSectionItemDao : DataBase<StyleProcessFlowSectionItem> {
        private static readonly StyleProcessFlowSectionItemDao styleprocessflowsectionitemDao=new StyleProcessFlowSectionItemDao();
        private StyleProcessFlowSectionItemDao() { }
        public static  StyleProcessFlowSectionItemDao Instance {
            get {
                return  styleprocessflowsectionitemDao;
            }
        }
    }
}

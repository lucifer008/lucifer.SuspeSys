using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 款式工序库Dao
    /// </summary>
    public class StyleProcessFlowStoreDao : DataBase<StyleProcessFlowStore> {
        private static readonly StyleProcessFlowStoreDao styleprocessflowstoreDao=new StyleProcessFlowStoreDao();
        private StyleProcessFlowStoreDao() { }
        public static  StyleProcessFlowStoreDao Instance {
            get {
                return  styleprocessflowstoreDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 制单明细颜色尺码明细Dao
    /// </summary>
    public class ProcessOrderColorSizeItemDao : DataBase<ProcessOrderColorSizeItem> {
        private static readonly ProcessOrderColorSizeItemDao processordercolorsizeitemDao=new ProcessOrderColorSizeItemDao();
        private ProcessOrderColorSizeItemDao() { }
        public static  ProcessOrderColorSizeItemDao Instance {
            get {
                return  processordercolorsizeitemDao;
            }
        }
    }
}

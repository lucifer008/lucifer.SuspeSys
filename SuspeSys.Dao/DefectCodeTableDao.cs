using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 疵点代码表Dao
    /// </summary>
    public class DefectCodeTableDao : DataBase<DefectCodeTable> {
        private static readonly DefectCodeTableDao defectcodetableDao=new DefectCodeTableDao();
        private DefectCodeTableDao() { }
        public static  DefectCodeTableDao Instance {
            get {
                return  defectcodetableDao;
            }
        }
    }
}

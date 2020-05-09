using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 基本工序库Dao
    /// </summary>
    public class BasicProcessFlowDao : DataBase<BasicProcessFlow> {
        private static readonly BasicProcessFlowDao basicprocessflowDao=new BasicProcessFlowDao();
        private BasicProcessFlowDao() { }
        public static  BasicProcessFlowDao Instance {
            get {
                return  basicprocessflowDao;
            }
        }
    }
}

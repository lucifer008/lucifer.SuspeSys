using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class FlowStatingDao : DataBase<FlowStating> {
        private static readonly FlowStatingDao flowstatingDao=new FlowStatingDao();
        private FlowStatingDao() { }
        public static  FlowStatingDao Instance {
            get {
                return  flowstatingDao;
            }
        }
    }
}

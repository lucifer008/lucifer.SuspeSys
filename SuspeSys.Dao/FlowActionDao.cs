using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class FlowActionDao : DataBase<FlowAction> {
        private static readonly FlowActionDao flowactionDao=new FlowActionDao();
        private FlowActionDao() { }
        public static  FlowActionDao Instance {
            get {
                return  flowactionDao;
            }
        }
    }
}

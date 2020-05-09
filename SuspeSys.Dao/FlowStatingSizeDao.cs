using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺工序制作站点指定的尺码Dao
    /// </summary>
    public class FlowStatingSizeDao : DataBase<FlowStatingSize> {
        private static readonly FlowStatingSizeDao flowstatingsizeDao=new FlowStatingSizeDao();
        private FlowStatingSizeDao() { }
        public static  FlowStatingSizeDao Instance {
            get {
                return  flowstatingsizeDao;
            }
        }
    }
}

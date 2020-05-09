using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 工艺工序制作站点指定的颜色Dao
    /// </summary>
    public class FlowStatingColorDao : DataBase<FlowStatingColor> {
        private static readonly FlowStatingColorDao flowstatingcolorDao=new FlowStatingColorDao();
        private FlowStatingColorDao() { }
        public static  FlowStatingColorDao Instance {
            get {
                return  flowstatingcolorDao;
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 菜单 模块表Dao
    /// </summary>
    public class ModulesDao : DataBase<Modules> {
        private static readonly ModulesDao modulesDao=new ModulesDao();
        private ModulesDao() { }
        public static  ModulesDao Instance {
            get {
                return  modulesDao;
            }
        }
    }
}

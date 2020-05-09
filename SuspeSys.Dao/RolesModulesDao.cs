using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao
{
    using NHibernate;
    using SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    using NHibernate.Type;

    /// <summary>
    /// 角色模块Dao
    /// </summary>
    public class RolesModulesDao : DataBase<RolesModules>
    {
        private static readonly RolesModulesDao rolesmodulesDao = new RolesModulesDao();
        private RolesModulesDao() { }
        public static RolesModulesDao Instance
        {
            get
            {
                return rolesmodulesDao;
            }
        }
    }
}

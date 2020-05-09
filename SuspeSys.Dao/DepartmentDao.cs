using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 部门Dao
    /// </summary>
    public class DepartmentDao : DataBase<Department> {
        private static readonly DepartmentDao departmentDao=new DepartmentDao();
        private DepartmentDao() { }
        public static  DepartmentDao Instance {
            get {
                return  departmentDao;
            }
        }
    }
}

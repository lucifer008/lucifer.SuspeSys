using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 员工等级Dao
    /// </summary>
    public class EmployeeGradeDao : DataBase<EmployeeGrade> {
        private static readonly EmployeeGradeDao employeegradeDao=new EmployeeGradeDao();
        private EmployeeGradeDao() { }
        public static  EmployeeGradeDao Instance {
            get {
                return  employeegradeDao;
            }
        }
    }
}

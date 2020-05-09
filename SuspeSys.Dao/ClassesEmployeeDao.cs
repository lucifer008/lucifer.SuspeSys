using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    using SuspeSys.Domain.Ext;

    /// <summary>
    /// 班次员工Dao
    /// </summary>
    public class ClassesEmployeeDao : DataBase<ClassesEmployee> {
        private static readonly ClassesEmployeeDao classesemployeeDao=new ClassesEmployeeDao();
        private ClassesEmployeeDao() { }
        public static  ClassesEmployeeDao Instance {
            get {
                return  classesemployeeDao;
            }
        }


        public void Insert(List<ClassesEmployeeModel> classesEmployees)
        {

        }
    }
}

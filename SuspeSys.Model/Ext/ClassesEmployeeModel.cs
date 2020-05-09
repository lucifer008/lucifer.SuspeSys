using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class ClassesEmployeeModel: ClassesEmployee
    {
        public string EMPLOYEE_Id { get { return base.Employee.Id; } }

        public string CLASSESINFO_Id { get { return base.ClassesInfo.Id; } }

        
    }
}

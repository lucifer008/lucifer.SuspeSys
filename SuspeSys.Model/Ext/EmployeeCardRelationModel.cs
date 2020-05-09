using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class EmployeeCardRelationModel: EmployeeCardRelation
    {
        public string EMPLOYEE_Id { get; set; }

        public string CARDINFO_Id { get; set; }
    }
}

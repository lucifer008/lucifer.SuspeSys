using SuspeSys.Domain.SusEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Business
{
    /// <summary>
    /// 排班员工
    /// </summary>
    public class SchedulingEmployee
    {
        public bool Checked { get; set; }

        public string Code { get; set; }

        public string Id
        {
            get
            {
                return Employee.Id;
            }
        }

        public string Name { get; set; }

        public Domain.Employee Employee {get;set;}
    }


    public class SchedulingRule
    {
        /// <summary>
        /// 是否是修改模式
        /// </summary>
        public bool IsEditMode { get; set; } = false;

        /// <summary>
        /// 班次信息
        /// </summary>
        public Domain.ClassesInfo ClassesInfoModel { get; set; }

        /// <summary>
        /// 排班员工
        /// </summary>
        public List<SchedulingEmployee> SchedulingEmployees { get; set; }

        /// <summary>
        /// 排班日期
        /// </summary>
        public DateTime SchedulingDateTime { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public Weeks Week { get; set; }

        /// <summary>
        /// 已排班次信息
        /// </summary>
        public IList<Domain.ClassesEmployee> ClassesEmployees { get; set; }


        /// <summary>
        /// 排班Id
        /// </summary>
        public string ClassesEmployeeId { get; set; }
    }
}

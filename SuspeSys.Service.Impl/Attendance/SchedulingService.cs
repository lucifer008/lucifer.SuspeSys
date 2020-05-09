using SuspeSys.Domain.Business;
using SuspeSys.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Attendance
{
    public class SchedulingService
    {

        private SchedulingService() { }

        private SchedulingRule schedulingRule { get; set; }

        public SchedulingService(SchedulingRule schedulingRule)
        {
            this.schedulingRule = schedulingRule;
        }

        public List<Domain.ClassesEmployee> Scheduling()
        {

            //验证参数
            this.Valid();

            //Check是否已经排过班
            this.Check();

            return BuildSchedule();
        }

  

        /// <summary>
        /// 验证参数
        /// </summary>
        private void Valid()
        {
            //TODO 多语言
            if (this.schedulingRule == null)
                throw new BusinessException("排班计划不能为空");

            if (this.schedulingRule.ClassesInfoModel == null)
                throw new  BusinessException("请选择班次信息");

            if (this.schedulingRule.SchedulingDateTime < DateTime.Now.Date)
                throw new BusinessException("请选择正确的排班日期");

            if (this.schedulingRule.SchedulingEmployees == null || !this.schedulingRule.SchedulingEmployees.Any(o => o.Checked) )
                throw new BusinessException("请选择员工");

            
        }

        /// <summary>
        /// 冲突检测，检测已有排班信息是否重复
        /// </summary>
        private void Check()
        {
            //已有排班记录
            if (this.schedulingRule.ClassesEmployees == null || this.schedulingRule.ClassesEmployees.Count == 0)
                return;

            //检测同一时间，同一员工 是否已经排班

            var classesEmployees = this.schedulingRule.ClassesEmployees;

            if (this.schedulingRule.IsEditMode)
            {
                if (this.schedulingRule.SchedulingEmployees.Count > 1)
                    //TODO 多语言
                    throw new BusinessException("只能选择一个员工");

                //获取
                var current = classesEmployees.FirstOrDefault(o => o.Id.Equals(this.schedulingRule.ClassesEmployeeId, StringComparison.OrdinalIgnoreCase));

                if (current != null && 
                    current.AttendanceDate == this.schedulingRule.SchedulingDateTime && 
                    current.ClassesInfo.Id == this.schedulingRule.ClassesInfoModel.Id && 
                    current.Employee.Id == this.schedulingRule.SchedulingEmployees.First().Id &&
                    current.Week == (int)this.schedulingRule.Week)
                {
                    //TODO 多语言
                    throw new BusinessException("排班信息没有改变，不需要修改");
                }


                //判处当前
                classesEmployees.Remove(current);
            }

            var dtDate = this.schedulingRule.SchedulingDateTime;
            var employeesIds = this.schedulingRule.SchedulingEmployees.Where(o => o.Checked).Select(o => o.Id.ToUpper()).ToList();

            StringBuilder builder = new StringBuilder();



            foreach (var item in classesEmployees)
            {
                if (item.AttendanceDate.Value.Date == dtDate && employeesIds.Contains(item.Employee.Id.ToUpper()))
                {
                    if (builder.Length > 0)
                        builder.Append(",");

                    builder.Append(item.Employee.RealName);
                }
                
            }

            if (builder.Length > 0)
            {
                throw new BusinessException(string.Format("员工{0} 已经排过班",builder.ToString()));
            }
        }

        /// <summary>
        /// 生成排班
        /// </summary>
        private List<Domain.ClassesEmployee> BuildSchedule()
        {
            List<Domain.ClassesEmployee> classesEmployees = new List<Domain.ClassesEmployee>();

            foreach (var item in this.schedulingRule.SchedulingEmployees)
            {
                if (!item.Checked)
                    continue;


                classesEmployees.Add(new Domain.ClassesEmployee()
                {
                    AttendanceDate = this.schedulingRule.SchedulingDateTime.Date,
                    ClassesInfo = this.schedulingRule.ClassesInfoModel as Domain.ClassesInfo,
                    Employee = item.Employee,
                    Week = (int)this.schedulingRule.Week
                }) ;
            }

            return classesEmployees;
        }

    }
}

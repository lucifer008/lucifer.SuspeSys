using SuspeSys.Domain.Business;
using SuspeSys.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.Attendance
{
    public class AttendanceAction : BaseAction
    {
        private AttendanceAction() { }
        public static AttendanceAction Instance
        {
            get
            {
                return new AttendanceAction();
            }
        }
        public DaoModel.ClassesInfo Model = new DaoModel.ClassesInfo();
        public IList<DaoModel.Employee> SearchEmployee(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, IDictionary<string, string> serachCondition = null)
        {
            return AttendanceService.SearchEmployee(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, serachCondition);
        }

        public void SaveClassesInfo()
        {
            AttendanceService.SaveClassesInfo(Model);
        }
        public IList<DaoModel.ClassesInfo> SearchClassesInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return AttendanceService.SearchClassesInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.HolidayInfo> SeacherHolidayInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return AttendanceService.SearchHolidayInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        /// <summary>
        /// 添加排班记录
        /// </summary>
        /// <param name="schedulingRule"></param>
        public void AddClassesEmployee(SchedulingRule schedulingRule)
        {
           AttendanceService.AddClassesEmployee(schedulingRule);
        }

        /// <summary>
        /// 查询排班记录
        /// </summary>
        /// <param name="siteGroupId"></param>
        /// <param name="week"></param>
        /// <param name="classInfoId"></param>
        /// <param name="employeCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Domain.PaginationResult<ClassesEmployeeDto> SearchClassEmployeeInfo(string siteGroupId, int week, string classInfoId, string employeCode, DateTime? begin, DateTime? end, int pageNumber, int pageSize)
        {
            return AttendanceService.SearchClassInfo(siteGroupId, week, classInfoId, employeCode, begin, end, pageNumber, pageSize);
        }
    }
}

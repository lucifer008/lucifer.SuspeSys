using SuspeSys.Domain.Business;
using SuspeSys.Domain.Dto;
using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Attendance
{
    public interface IAttendanceService
    {
        IList<DaoModel.Employee> SearchEmployee(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, IDictionary<string, string> searchCondition = null);
        void SaveClassesInfo(DaoModel.ClassesInfo model);
        IList<DaoModel.ClassesInfo> SearchClassesInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        IList<DaoModel.HolidayInfo> SearchHolidayInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        /// <summary>
        /// 添加排班记录
        /// </summary>
        /// <param name="schedulingRule"></param>
        void AddClassesEmployee(SchedulingRule schedulingRule);

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
        Domain.PaginationResult<ClassesEmployeeDto> SearchClassInfo(string siteGroupId, int week, string classInfoId, string employeCode, DateTime? begin, DateTime? end, int pageNumber, int pageSize);
    }
}

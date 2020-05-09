using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Business;
using SuspeSys.Domain.Dto;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Attendance;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Attendance
{
    public class AttendanceServiceImpl : ServiceBase, IAttendanceService
    {
        public IList<DaoModel.Employee> SearchEmployee(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, IDictionary<string, string> searchCondition = null)
        {
            string queryString = "select * from Employee where deleted=0 ";
            //string[] paramValues = null;
            List<string> paramValues = new List<string>();

            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (Code like ? OR RealName like ?)");
                paramValues.Add(string.Format("%{0}%", searchKey));
                paramValues.Add(string.Format("%{0}%", searchKey));
            }

            if (searchCondition != null)
            {
                foreach (var item in searchCondition)
                {
                    queryString += $" AND {item.Key } LIKE ? ";
                    paramValues.Add(string.Format("%{0}%", item.Value));
                }
            }

            paramValues = paramValues.Count == 0 ? null : paramValues;
            var list = Query<DaoModel.Employee>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues?.ToArray());
            return list;
        }
        public void SaveClassesInfo(DaoModel.ClassesInfo model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                DapperHelp.Edit(model);

                //ClassesInfoDao.Instance.Update(model);
                return;
            }
            ClassesInfoDao.Instance.Save(model);
        }
        public IList<DaoModel.ClassesInfo> SearchClassesInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from ClassesInfo where deleted=0 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND ( Num like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var list = Query<DaoModel.ClassesInfo>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return list;
        }

        public IList<DaoModel.HolidayInfo> SearchHolidayInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from HolidayInfo where deleted=0 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND ( Num like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var list = Query<DaoModel.HolidayInfo>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return list;
        }

        /// <summary>
        /// 获取已有排班记录
        /// </summary>
        /// <returns></returns>
        public IList<ClassesEmployee> GetClassesEmployeeModels()
        {

            string sql = $"SELECT * FROM ClassesEmployee WHERE Deleted = 0 and AttendanceDate >= ?";

            return this.Query<ClassesEmployee>(new StringBuilder(sql), null, false, DateTime.Today );
        }


        /// <summary>
        /// 添加排班记录
        /// </summary>
        /// <param name="schedulingRule"></param>
        public void AddClassesEmployee(SchedulingRule schedulingRule)
        {
            schedulingRule.ClassesEmployees = this.GetClassesEmployeeModels();

            SchedulingService schedulingService = new SchedulingService(schedulingRule);

            var classesEmployee = schedulingService.Scheduling();

            if (schedulingRule.IsEditMode)
            {
                var current = Dao.ClassesEmployeeDao.Instance.GetById(schedulingRule.ClassesEmployeeId); 
                if (current == null)
                    throw new BusinessException("没有找到要更新的数据");

                current.Employee = schedulingRule.SchedulingEmployees.First().Employee;
                current.ClassesInfo = schedulingRule.ClassesInfoModel;
                current.AttendanceDate = schedulingRule.SchedulingDateTime;
                current.Week = (int)schedulingRule.Week;

                ClassesEmployeeDao.Instance.Update(current);
            }
            else
            {
                foreach (var item in classesEmployee)
                {
                    ClassesEmployeeDao.Instance.Update(item);
                }
            }
        }

        public Domain.PaginationResult<ClassesEmployeeDto> SearchClassInfo (string siteGroupId, int week, string classInfoId, string employeCode, DateTime? begin, DateTime? end, int pageNumber, int pageSize)
        {

            Domain.Pagination pagination = new DaoModel.Pagination();

            pagination.Colomns = @"a.AttendanceDate, A.Week, d.GroupName, b.Code EmployeeCode, b.RealName,c.Id, a.Id as ClassesEmployeeId,
                                    c.Num, c.CType, c.Time1GoToWorkDate, c.Time1GoOffWorkDate, c.Time2GoToWorkDate,         c.Time2GoOffWorkDate, c.Time3GoToWorkDate, c.Time3GoOffWorkDate, c.OverTimeIn, c.OverTimeOut";

            pagination.Tables = @"ClassesEmployee a 
                                    left join Employee b on a.EMPLOYEE_Id = b.Id
                                    left join ClassesInfo c on a.CLASSESINFO_Id = c.Id
                                    left join SiteGroup d on b.SITEGROUP_Id = d.Id";
            pagination.OrderBy = " a.AttendanceDate desc";
            pagination.PageNumber = pageNumber;
            pagination.PageSize = pageSize;

            pagination.WhereStr = " AND a.Deleted = 0 ";

            if (!string.IsNullOrWhiteSpace(siteGroupId))
            {
                pagination.WhereStr += $" AND B.SITEGROUP_Id = '{ siteGroupId}'";
            }
            if (week > 0)
            {
                pagination.WhereStr += $" AND a.Week = {week}";
            }
            if (!string.IsNullOrWhiteSpace(classInfoId))
            {
                pagination.WhereStr += $" AND  A.CLASSESINFO_Id = '{classInfoId}'";
            }
            if (!string.IsNullOrWhiteSpace(employeCode))
            {
                pagination.WhereStr += $" AND B.Code = '{employeCode}'";
            }
            if (begin.HasValue && begin.Value.Year > 1900)
            {
                pagination.WhereStr += $" AND a.AttendanceDate >= '{begin}'";
            }
            if (end.HasValue && end.Value.Year >= 1900)
            {
                pagination.WhereStr += $" AND a.AttendanceDate <= '{begin}'";
            }


            return Dao.DapperHelp.Paging<ClassesEmployeeDto>(pagination);

            /*
             select

from ClassesEmployee a 
left join Employee b on a.EMPLOYEE_Id = b.Id
left join ClassesInfo c on a.CLASSESINFO_Id = c.Id
left join SiteGroup d on b.SITEGROUP_Id = d.Id

             */


        }

        /// <summary>
        ///  是否在排班时间登陆
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool IsSchdule(string employeeId)
        {
            var sql = @"
select
    a.AttendanceDate, A.Week,  c.Id, a.Id as ClassesEmployeeId,
    c.Num, c.CType, c.Time1GoToWorkDate, c.Time1GoOffWorkDate, c.Time2GoToWorkDate, c.Time2GoOffWorkDate, c.Time3GoToWorkDate, c.Time3GoOffWorkDate, c.OverTimeIn, c.OverTimeOut
from ClassesEmployee a
    left join ClassesInfo c on a.CLASSESINFO_Id = c.Id
where a.EMPLOYEE_Id = @employeeId";

            //获取当前员工的所有未来（包括当天的排班记录）
            var schduleList = this.QueryForList<ClassesEmployeeDto>(sql, new { employeeId = employeeId });

            if (schduleList == null || schduleList.Count == 0)
                return false;

            if (!schduleList.Any(o => o.AttendanceDate.Date == DateTime.Today))
                return false;

            return true;
        }

    }
}

using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Ext;

namespace SuspeSys.Service.PersonnelManagement
{
    public interface IPersonnelManagementService
    {
        /// <summary>
        /// 组别信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<Domain.ProductGroupModel> SearchProductGroup(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        IList<Domain.DepartmentModel> SearchDepartment(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        CardInfoModel GetEmployeeCardInfoBy(string cardInfoId);
        IList<CardInfoModel> SearchEmployeeCardInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<Domain.WorkTypeModel> SearchWorkType(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<WorkshopModel> SearchWorkshop(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<Domain.PositionModel> SearchPosition(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        IList<Domain.SiteGroupModel> SearchSiteGroup(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        IList<Factory> SearchFactory(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        IList<Domain.EmployeeModel> SearchEmployee(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        void RemoveAllEmployeePositionByEmployeeId(string employeeId);

        IList<Domain.EmployeePositions> GetPositionsByEmployeeId(string employeeId);

        /// <summary>
        /// 获取所有客户机
        /// </summary>
        /// <returns></returns>
        IList<ClientMachinesModel> GetAllClientMachines();

        IList<UserOperateLogsModel> SearchUserOperatorLog(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        /// <summary>
        ///获取所有员工
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployeeList();

        void SaveEmployeeCardInfo(List<CardInfoModel> cardInfoModel);
    }
}

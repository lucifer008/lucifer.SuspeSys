using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.PersonnelManagement
{
    public class EmployeeIndexAction : BaseAction
    {
        public IList<EmployeeModel> GetAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string roleName)
        {
            return _PersonnelManagementService.SearchEmployee(currentPageIndex, pageSize, out totalCount, ordercondition, roleName);
        }
        public IList<CardInfoModel> SearchEmployeeCardInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return _PersonnelManagementService.SearchEmployeeCardInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey); ;
        }

        public CardInfoModel GetEmployeeCardInfoBy(string cardInfoId)
        {
            return _PersonnelManagementService.GetEmployeeCardInfoBy(cardInfoId);
        }
    }
}

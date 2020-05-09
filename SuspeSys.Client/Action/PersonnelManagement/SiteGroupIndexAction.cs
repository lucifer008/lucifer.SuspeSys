
using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.PersonnelManagement
{
    public class SiteGroupIndexAction : BaseAction
    {
        public IList<Domain.SiteGroupModel> GetAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string roleName)
        {
            return _PersonnelManagementService.SearchSiteGroup(currentPageIndex, pageSize, out totalCount, ordercondition, roleName);
        }
        public IList<Domain.Factory> SearchFactory(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey) {
            return _PersonnelManagementService.SearchFactory(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<WorkshopModel> SearchWorkshop(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            var list= _PersonnelManagementService.SearchWorkshop(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
           
            return list;
        }
    }
}

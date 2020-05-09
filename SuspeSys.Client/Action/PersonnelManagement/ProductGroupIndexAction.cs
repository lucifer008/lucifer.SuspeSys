using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.PersonnelManagement
{
    public class ProductGroupIndexAction : BaseAction
    {
        public IList<Domain.ProductGroupModel> GetAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string roleName)
        {
            return _PersonnelManagementService.SearchProductGroup(currentPageIndex, pageSize, out totalCount, ordercondition, roleName);
        }
    }
}

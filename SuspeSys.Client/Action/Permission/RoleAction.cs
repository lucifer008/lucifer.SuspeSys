using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Permission
{
    public class RoleAction : BaseAction
    {
        public IList<Roles> GetAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string roleName)
        {
            return permissionService.SearchRole(currentPageIndex, pageSize, out totalCount, ordercondition, roleName); 
        }
    }
}

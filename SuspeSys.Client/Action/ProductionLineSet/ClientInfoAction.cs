using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.ProductionLineSet
{
    public class ClientInfoAction:BaseAction
    {
        public IList<Domain.ClientMachinesModel> GetAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string roleName)
        {
            
            return ProductionLineSetQueryService.SearchClientMachines(currentPageIndex, pageSize, out totalCount, ordercondition, roleName);
        }
    }
}

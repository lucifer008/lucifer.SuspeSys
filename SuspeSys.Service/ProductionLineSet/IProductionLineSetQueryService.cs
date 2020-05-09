using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.ProductionLineSet
{
    public interface IProductionLineSetQueryService
    {
        IList<DaoModel.SiteGroup> GetPipeliningSiteGroupList(string siteGroupId);
        IList<DaoModel.Stating> GetStatingList(string siteGroupId);

        IList<Domain.ClientMachinesModel> SearchClientMachines(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

    }
}

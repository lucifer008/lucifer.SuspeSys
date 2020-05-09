using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.ProductionLineSet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Impl.ProductionLineSet
{
    public class ProductionLineSetQueryServiceImpl: ServiceBase, IProductionLineSetQueryService
    {
        public IList<DaoModel.SiteGroup> GetPipeliningSiteGroupList(string siteGroupId) {
            var sql = new StringBuilder(@"select * from SiteGroup where Id=?");
           return Query<DaoModel.SiteGroup>(sql, null, false, siteGroupId);
        }
        public IList<DaoModel.Stating> GetStatingList(string siteGroupId) {
            var sql = new StringBuilder(@"  SELECT T1.* FROM Stating T1
              Inner Join SiteGroup T2 On T1.SITEGROUP_Id=T2.Id
              WHERE T2.Id=?
            ");
            var htOrder = new Dictionary<string,string>();
            htOrder.Add("MainTrackNumber", "Asc");
            htOrder.Add("StatingNo", "Asc");
            return Query<DaoModel.Stating>(sql, htOrder, false, siteGroupId);
        }

        public IList<DaoModel.ClientMachinesModel> SearchClientMachines(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from ClientMachines where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (ClientMachines like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.ClientMachinesModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }
    }
}

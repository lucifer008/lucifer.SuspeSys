using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.ProductionLineSet
{
    public interface IProductionLineSetService
    {
        List<StatingMsg> AddPipelining(DaoModel.Pipelining pipelining, IList<DaoModel.Stating> StatingList, DaoModel.SiteGroup SiteGroup);
        List<StatingMsg> UpdatePipelining(DaoModel.Pipelining pipelining, IList<DaoModel.Stating> statingList, DaoModel.SiteGroup siteGroup);

        /// <summary>
        /// 添加或更新客户端授权信息
        /// </summary>
        /// <param name="clientName">客户端信息</param>
        /// <param name="grant">授权信息</param>
        void AddOrUpdateClientMachine(string clientName, string grant);
        IList<DaoModel.BridgeSet> SearchBridgeSet(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string v);
    }
}

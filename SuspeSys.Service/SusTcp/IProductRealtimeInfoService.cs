using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.SusTcp
{
    public interface IProductRealtimeInfoService
    {
        SuspeSys.Domain.ProductsModel GetOnLineProduct(string groupNo);
        IList<ProductRealtimeInfoModel> SearchProductRealtimeInfo(string flowChartId, string groupNo);
        IList<ProductRealtimeInfoModel> SearchProductRealtimeInfoByServer(string flowChartId, string groupNo);
        IList<OnlineOrInStationItemModel> SearchOnlineOrInStationItem(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string statingNo,string productId,int mainTrackNumber);
        IList<OnlineOrInStationItemModel> SearchOnlineOrInStationItemByServer(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string statingNo, string productId, int mainTrackNumber);
        IList<SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel> SearchCoatHangerInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string conModel,ref SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel endFlow, string hangerNo = null);
        IList<HangerStatingAllocationItemModel> GetAllocationFlow(string flowChartId);
        string GetAllocationFlow(string statingNo,int mainTrakNumber, IList<HangerStatingAllocationItemModel> allList);
    }
}

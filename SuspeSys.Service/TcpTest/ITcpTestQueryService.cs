using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.TcpTest
{
    public interface ITcpTestQueryService
    {
        IList<DaoModel.Products> GetWaitBindHangerProductList(string hangerPieceNo);
        void RegisterHangerToProducts(string hangingStationNo, string hangerNo);
        IList<DaoModel.ProcessFlowStatingItem> GetHangerNextProcessFlowStatingList(string pfChartId, string processFlowId);
        IList<DaoModel.ProcessFlowStatingItem> GetProcessChartStatingList(string productionNumber, string hangingPieceSiteNo);
        DaoModel.Products GetProducts(string productionNumber, string hangingPieceSiteNo);
    }
}

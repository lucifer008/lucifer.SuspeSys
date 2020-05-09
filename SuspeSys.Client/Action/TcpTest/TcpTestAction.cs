using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.TcpTest
{
    public class TcpTestAction : BaseAction
    {
        public IList<DaoModel.ProcessFlowStatingItem> GetProcessChartStatingList(string productionNumber, string hangingPieceSiteNo)
        {
            return TcpTestQueryService.GetProcessChartStatingList(productionNumber, hangingPieceSiteNo);
        }
        public DaoModel.Products GetProducts(string productionNumber, string hangingPieceSiteNo)
        {
            return TcpTestQueryService.GetProducts(productionNumber,hangingPieceSiteNo);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.Products.Model
{
    public class ProductsModel
    {
        public IList<DaoModel.ProcessOrderModel> ProderOrderModelList = new List<DaoModel.ProcessOrderModel>();
        public IList<DaoModel.ProcessFlowSection> ProcessFlowSectionList = new List<DaoModel.ProcessFlowSection>();
        public IList<DaoModel.ProcessFlowChart> ProcessFlowChartList = new List<DaoModel.ProcessFlowChart>();
        public IList<DaoModel.ProcessOrderExtModel> ProcessOrderExtModelList = new List<DaoModel.ProcessOrderExtModel>();
        public IList<DaoModel.ProcessFlowChartFlowModel> ProcessFlowChartFlowModelList = new List<DaoModel.ProcessFlowChartFlowModel>();
        public IList<DaoModel.Products> ProductsList = new List<DaoModel.Products>();
        public string HangingPieceStatingNo { set; get; }
        public string MainTrackNo { set; get; }
        public int ProductNumber { set; get; }
    }
}

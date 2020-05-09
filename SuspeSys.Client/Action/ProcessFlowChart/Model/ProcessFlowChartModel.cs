using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessFlowChart.Model
{
    public class ProcessFlowChartModel
    {

        public DaoModel.ProcessFlowChart ProcessFlowChart = new DaoModel.ProcessFlowChart();

        public IList<DaoModel.ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationList = new List<DaoModel.ProcessFlowChartFlowRelationModel>();
        public IList<DaoModel.ProcessFlowChartGrop> ProcessFlowChartGropList = new List<DaoModel.ProcessFlowChartGrop>();


    }
}

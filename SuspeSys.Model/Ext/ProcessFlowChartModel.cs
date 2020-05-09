using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{

    /// <summary>
    /// 工艺路线图:
    ///   产品在吊挂生产线的生产工序排序及员工任务分配的数据集合 扩展Model
    /// </summary>
    public class ProcessFlowChartModel : ProcessFlowChart
    {
        public IList<ProcessFlowChartGrop> ProcessFlowChartGroupList = new List<ProcessFlowChartGrop>();
        public IList<ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList = new List<ProcessFlowChartFlowRelationModel>();
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{

    /// <summary>
    /// 工序制作站点明细 扩展Model
    /// </summary>
    public class ProcessFlowStatingItemModel : ProcessFlowStatingItem
    {
        public virtual string ProcessFlowChartFlowRelationId { set; get; }
    }
}

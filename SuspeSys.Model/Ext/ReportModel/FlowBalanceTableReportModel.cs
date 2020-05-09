using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    /// <summary>
    /// 工序平衡
    /// </summary>
    public class FlowBalanceTableReportModel
    {
        public virtual string FlowNo { set; get; }
        public virtual string FlowCode { set; get; }
        public virtual string FlowName { set; get; }
        public virtual string SAM { set; get; }
        public virtual string AllocationStatings { set; get; }
        public virtual int? TodayYield { set; get; }
        public virtual int? TotalYield { set; get; }
        public virtual long? OnlineHangerCount { set; get; }
        public virtual long? InStatingHangerCount { set; get; }
        public virtual decimal CurrentHangerCount { set; get; }
        public virtual string AllocationMainTrackeStatings { set; get; }
        public virtual string Capacity { get; set; }
    }
}

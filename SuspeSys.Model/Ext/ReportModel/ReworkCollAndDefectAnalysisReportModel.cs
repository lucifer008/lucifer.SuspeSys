using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    /// <summary>
    /// 返工汇总
    /// </summary>
    public class ReworkCollAndDefectAnalysisReportModel
    {
        public virtual string InspectionDate { set; get; }
        public virtual string ProcessOrderNo { set; get; }
        public virtual string FlowNo { set; get; }
        public virtual string FlowName { set; get; }
        public virtual int Yield { set; get; }
        public virtual int ReworkYield { set; get; }
        public virtual string RewokRate { set; get; }
        public virtual string GroupNo { set; get; }

}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    public class WorkingHoursAnalysisReportModel: HangerProductItem
    {
        public virtual string EmployeeName { set; get; }
        public virtual int AvgTimes { set; get; }
        public virtual int? StanardHours { set; get; }
        public virtual string StyleCode { set; get; }
        /// <summary>
        /// 工序索引
        /// </summary>
        [Description("工序索引")]
       new public virtual int? FlowIndex { get; set; }

    }
}

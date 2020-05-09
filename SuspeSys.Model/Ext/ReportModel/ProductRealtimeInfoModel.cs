using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    public class ProductRealtimeInfoModel
    {
        public virtual string Id { set; get; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public virtual string Code { set; get; }
        public virtual string GroupNO { set; get; }

        public virtual string EmployeeName { set; get; }

        public virtual string ProcessFlowName { set; get; }

        public virtual int Capacity { set; get; }
        public virtual int OnlineHangerCount { set; get; }
        public virtual int StatingInCount { set; get; }
    }
}

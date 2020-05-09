using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    public class EmployeeYieldReportModel : SucessEmployeeFlowProduction
    {
        /// <summary>
        /// 款号
        /// </summary>
        public virtual string StyleCode { set; get; }

        /// <summary>
        /// 款号
        /// </summary>
        public virtual string StyleNo { set; get; }

        /// <summary>
        /// 产出量
        /// </summary>
        public virtual int? YieldCount { set; get; }

        /// <summary>
        /// PO编号
        /// </summary>
        public virtual string PurchaseOrderNo { set; get; }

        /// <summary>
        /// 工段
        /// </summary>
        public virtual string FlowSection { set; get; }
        public virtual decimal? StanardHours { set; get; }
        /// <summary>
        /// 标准工价
        /// </summary>
        public virtual decimal? StandardPrice { get; set; }
        new public virtual string InsertDateTime { get; set; }
        new public virtual short? SiteNo { get; set; }
        new public virtual int? FlowIndex { get; set; }
        public virtual int? ReworkCount { get; set; }
        public virtual string ReworkRate { get; set; }
        public virtual int? RealyWorkMin { get; set; }
        public virtual decimal? Income { get; set; }
        public virtual string SeamsRate { get; set; }
        public virtual int? UnitCunt { get; set; }
        public virtual string EmployeeNo { get; set; }
        public virtual string GroupSites { get; set; }
    }
}

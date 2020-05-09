using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    /// <summary>
    /// 返工详情Model
    /// </summary>
    public class ReworkDetailReportModel: HangerProductFlowChart
    {
        /// <summary>
        /// 品检时间
        /// </summary>
        public virtual string InspectionDate { set; get; }
        /// <summary>
        /// 工序号
        /// </summary>
        new public virtual  string ProcessOrderNo { set; get; }

        /// <summary>
        /// 款号
        /// </summary>
        public virtual string StyleNo { set; get; }

        /// <summary>
        /// 工段
        /// </summary>
        public virtual string FlowSection { set; get; }

        /// <summary>
        /// PO号
        /// </summary>
        public virtual string PO { set; get; }
        /// <summary>
        /// 布匹号
        /// </summary>
        public virtual string ClothNumber { set; get; }

        /// <summary>
        /// 条码
        /// </summary>
        public virtual string BarCode { set; get; }

        /// <summary>
        /// 任务数量
        /// </summary>
        public virtual int? Num { set; get; }

        /// <summary>
        /// 疵点名称
        /// </summary>
        public virtual string DefectName { set; get; }

        /// <summary>
        /// 生产组
        /// </summary>
        //  public virtual string GroupNo { set; get; }
        //public virtual string ReworkEmployeeName { set; get; }
        //public virtual string ReworkStatingNo { set; get; }

        new public virtual string ReworkDate { set; get; }
        public virtual Int64? ReworkIndex { set; get; }

    }
}

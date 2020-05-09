using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    public class ProductItemReportModel: SucessHangerProductItem
    {
        public virtual string EmployeeName { set; get; }
        public virtual string PieceCount { set; get; }
        public virtual string StyleCode { set; get; }
        public virtual string PO { set; get; }
        /// <summary>
        /// 车缝用时(秒)
        /// </summary>
        public virtual Int32? WorkHours { set; get; }
        /// <summary>
        /// 工时(秒)
        /// </summary>
        public virtual Int32? StanardSecond { set; get; }
        public virtual string WorkRate { set; get; }
       new public virtual string InsertDateTime { get; set; }
        new public virtual Int64? Id { get; set; }

    }
}

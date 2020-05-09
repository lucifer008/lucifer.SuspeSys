using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    /// <summary>
    /// 工序平衡Model
    /// </summary>
    public class DefectAnalysisReportModel
    {
        public IList<string> DefectNameList = new List<string>();
        /// <summary>
        /// 总数量
        /// </summary>
        public virtual long TotalCount { set; get; }
        /// <summary>
        /// 疵点数量明细
        /// </summary>
        public  IList<DefectAnalysisReportDetailModel> DefectAnalysisReportDetailModelList = new List<DefectAnalysisReportDetailModel>();
    }
    public class DefectAnalysisReportDetailModel {
        public virtual string DefectName { set; get; }
        public virtual int DefectCount { set; get; }
        public virtual string DefectRate { set; get; }
    }
}

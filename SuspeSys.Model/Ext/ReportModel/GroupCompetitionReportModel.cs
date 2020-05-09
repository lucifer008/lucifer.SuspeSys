using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    /// <summary>
    /// 产量达标详情表 Model
    /// </summary>
    public class GroupCompetitionReportModel
    {
        /// <summary>
        ///// 每日产量
        ///// </summary>
        //public IList<GroupCompetitionReportItemModel> OutProductDataDailyYieldList = new List<GroupCompetitionReportItemModel>();
        ///// <summary>
        ///// 目标产量
        ///// </summary>
        //public IList<GroupCompetitionReportItemModel> OutProductDataTargetYieldList = new List<GroupCompetitionReportItemModel>();
        ///// <summary>
        ///// 不良数
        ///// </summary>
        //public IList<GroupCompetitionReportItemModel> OutProductDataDefectCountList = new List<GroupCompetitionReportItemModel>();
        public IList<GroupCompetitionReportItemModel> OutProductDataList = new List<GroupCompetitionReportItemModel>();

        /// <summary>
        /// 达成率
        /// </summary>
        public IList<GroupCompetitionReportItemModel> YieldRateList = new List<GroupCompetitionReportItemModel>();
        /// <summary>
        /// 产出数据
        /// </summary>
        public IList<GroupCompetitionReportItemModel> OutProductDataTableList = new List<GroupCompetitionReportItemModel>();
    }
    public class GroupCompetitionReportItemModel {
        /// <summary>
        /// 日期
        /// </summary>
        public virtual string StatDate { set; get; }
        /// <summary>
        /// 日产量
        /// </summary>
        public virtual int DailyYield { set; get; }
        /// <summary>
        /// 目标产量
        /// </summary>
        public virtual int TargetYield { set; get; }
        /// <summary>
        /// 不良数
        /// </summary>
        public virtual int DefectCount { set; get; }
        /// <summary>
        /// 出勤人数
        /// </summary>
        public virtual int Attendance { set; get; }
        /// <summary>
        /// 请假/矿工
        /// </summary>
        public virtual int leaveOrAbsenteeism { set; get; }
        /// <summary>
        /// 达成率
        /// </summary>
        public virtual decimal YieldRate { set; get; }
    }
}

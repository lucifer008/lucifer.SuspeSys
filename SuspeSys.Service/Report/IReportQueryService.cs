using SuspeSys.Domain.Ext.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Report
{
    public interface IReportQueryService
    {
        /// <summary>
        /// 查询员工产量
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<EmployeeYieldReportModel> SearchEmployeeYield(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        /// <summary>
        /// 查询员工产量
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<EmployeeYieldReportModel> SearchEmployeeYield(IDictionary<string, string> ordercondition, string searchKey);


        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey,string flowChartId=null);
        /// <summary>
        /// 返工汇总
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
       
        GroupCompetitionReportModel SearchGroupCompetitionReport(string searchKey);

        /// <summary>
        /// 产量汇总报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<YieldCollectModel> SearchYieldCollect(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic,string inCondition);

        /// <summary>
        /// 工时分析
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic,string inCondition,string buidInCondition);

        /// <summary>
        /// 产出明细报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<ProductItemReportModel> SearchProductItemReportReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition = null);
        /// <summary>
        /// 返工详情报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<ReworkDetailReportModel> SearchReworkDetailReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        /// <summary>
        /// 疵点分析图
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        DefectAnalysisReportModel SearchDefectAnalysisReport(string searchKey);
        /// <summary>
        /// 产量汇总报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        IList<YieldCollectModel> SearchYieldCollect(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition);
        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(IDictionary<string, string> ordercondition, string searchKey, string flowChartId);
        /// <summary>
        /// 产出明细报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        IList<ProductItemReportModel> SearchProductItemReportReport(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition);
        /// <summary>
        /// 返工详情报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<ReworkDetailReportModel> SearchReworkDetailReport(IDictionary<string, string> ordercondition, string searchKey);
        /// <summary>
        /// 返工汇总
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(IDictionary<string, string> ordercondition, string searchKey);
        /// <summary>
        /// 工时分析报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition, string buidInCondition);
    }
}

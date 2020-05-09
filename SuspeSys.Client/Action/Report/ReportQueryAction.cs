using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext.ReportModel;
using SuspeSys.Service.Impl.SusCache;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.SusRedisService.SusRedis;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Report
{
    public class ReportQueryAction : BaseAction
    {
        private ReportQueryAction() { }

        public readonly static ReportQueryAction Instance = new ReportQueryAction();

        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, string flowChartId = null)
        {
            var sql = "select * from Stating";
            var statingList = CommonAction.Instance.QueryBySql<Stating>(sql, null).ToList<Stating>();
            var dicStatingInNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);

            var dicAllocationNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);

            var rslt1 = ReportQueryService.SearchFlowBalanceTableReportModel(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, flowChartId);
            //在线数及站内数修正
            rslt1.ToList().ForEach(delegate (FlowBalanceTableReportModel pbtr)
            {
                var capacity = 0;
                long inStatingHangerCount = 0;
                long onlineHangerCount = 0;
                if (null != pbtr.AllocationMainTrackeStatings)
                {
                    var mainStatings = pbtr.AllocationMainTrackeStatings?.Split(',');
                    foreach (var ms in mainStatings)
                    {

                        if (dicStatingInNumCache.Keys.Contains(ms))
                        {
                            var iStatingCount = dicStatingInNumCache[ms];
                            inStatingHangerCount += iStatingCount;
                        }
                        if (dicAllocationNumCache.Keys.Contains(ms))
                        {
                            var onlineCount = dicAllocationNumCache[ms];
                            onlineHangerCount += onlineCount;
                        }
                        var mainTrackNumber = int.Parse(ms.Split(':')[0]);
                        var statingNo = int.Parse(ms.Split(':')[1]);
                        var statingFilter = statingList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && int.Parse(f.StatingNo) == statingNo);
                        capacity += statingFilter.Count() > 0 ? statingFilter.First().Capacity.Value : 0;

                    }

                    pbtr.InStatingHangerCount = inStatingHangerCount;
                    pbtr.OnlineHangerCount = onlineHangerCount;
                    pbtr.CurrentHangerCount = Convert.ToDecimal(pbtr.OnlineHangerCount + pbtr.InStatingHangerCount) / capacity;
                    pbtr.Capacity = capacity.ToString();
                }
            });
            return rslt1;
        }
        /// <summary>
        /// 返工汇总
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return ReportQueryService.SearchReworkCollAndDefectAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }
        /// <summary>
        /// 衣架信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="conModel"></param>
        /// <param name="endFlow"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        internal IList<CoatHangerIndexModel> SearchCoatHangerInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string conModel, ref SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel endFlow, string hangerNo = null)
        {
            return ProductRealtimeInfoServiceImpl.Instance.SearchCoatHangerInfo(currentPageIndex, pageSize, out totalCount, ordercondition, conModel, ref endFlow, hangerNo);
        }

        /// <summary>
        /// 疵点分析图
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public DefectAnalysisReportModel SearchDefectAnalysisReport(string searchKey)
        {
            return ReportQueryService.SearchDefectAnalysisReport(searchKey);
        }
        /// <summary>
        /// 产量达标详情表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>

        internal GroupCompetitionReportModel SearchGroupCompetitionReport(string searchKey)
        {
            return ReportQueryService.SearchGroupCompetitionReport(searchKey);
        }

        /// <summary>
        /// 返工详情汇总
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ReworkDetailReportModel> SearchReworkDetailReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return ReportQueryService.SearchReworkDetailReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        /// <summary>
        /// 员工产量报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<EmployeeYieldReportModel> SearchEmployeeYield(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            var list = ReportQueryService.SearchEmployeeYield(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            list.ToList().ForEach(delegate (EmployeeYieldReportModel eyr)
            {
                var total = 0;
                if (eyr.YieldCount != null)
                {
                    total += eyr.YieldCount.Value;
                }
                if (eyr.ReworkCount != null)
                {
                    total += eyr.ReworkCount.Value;
                }
                if (total > 0)
                {
                    var r = ((Convert.ToDecimal(eyr.ReworkCount)) / total) * 100;
                    eyr.ReworkRate = string.Format("{0}%", r.ToString("#0.00"));
                }
                var outTotal = eyr.YieldCount;
                if (null != eyr.StanardHours && outTotal != null && eyr.RealyWorkMin != null)
                {
                    var r = ((Convert.ToDecimal(eyr.StanardHours) * outTotal.Value) / eyr.RealyWorkMin.Value) * 100;
                    eyr.SeamsRate = string.Format("{0}%", r.ToString("#0.00"));
                }
                if (eyr.StandardPrice != null && eyr.UnitCunt != null)
                {
                    eyr.Income = (eyr.StandardPrice.Value * eyr.UnitCunt.Value);//.ToString("#0.00");
                }
            });
            return list;
        }
        /// <summary>
        /// 员工产量报表(透视图)
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<EmployeeYieldReportModel> SearchEmployeeYield(IDictionary<string, string> ordercondition, string searchKey)
        {
            var list = ReportQueryService.SearchEmployeeYield(ordercondition, searchKey);
            list.ToList().ForEach(delegate (EmployeeYieldReportModel eyr)
            {
                var total = 0;
                if (eyr.YieldCount != null)
                {
                    total += eyr.YieldCount.Value;
                }
                if (eyr.ReworkCount != null)
                {
                    total += eyr.ReworkCount.Value;
                }
                if (total > 0)
                {
                    var r = ((Convert.ToDecimal(eyr.ReworkCount)) / total) * 100;
                    eyr.ReworkRate = string.Format("{0}%", r.ToString("#0.00"));
                }
                var outTotal = eyr.YieldCount;
                if (null != eyr.StanardHours && outTotal != null && eyr.RealyWorkMin != null)
                {
                    var r = ((Convert.ToDecimal(eyr.StanardHours) * outTotal.Value) / eyr.RealyWorkMin.Value) * 100;
                    eyr.SeamsRate = string.Format("{0}%", r.ToString("#0.00"));
                }
                if (eyr.StandardPrice != null && eyr.UnitCunt != null)
                {
                    eyr.Income = (eyr.StandardPrice.Value * eyr.UnitCunt.Value);//.ToString("#0.00");
                }
            });
            return list;
        }

        /// <summary>
        /// 工时分析报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="processOrderNo"></param>
        /// <param name="styleCode"></param>
        /// <param name="pO"></param>
        /// <param name="flowSelection"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="workshop"></param>
        /// <param name="groupNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(IDictionary<string, string> ordercondition, string processOrderNo, string styleCode, string pO, string flowSelection, string color, string size, string workshop, string groupNo, string beginDate, string endDate)
        {
            var inCondition = string.Empty;
            var buidInCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(pO))
            {
                searchDic.Add("Po=", pO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            if (!string.IsNullOrEmpty(color))
            {
                buidInCondition+=$" AND T1.PColor='{color}'";
            }
            if (!string.IsNullOrEmpty(size))
            {
                //searchDic.Add("PSize=", size);
                buidInCondition += $" AND T1.PSize='{size}'";
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime<={0}", endDate.FormatDBValue());
            }
            return ReportQueryService.SearchWorkingHoursAnalysisReport(ordercondition, searchDic, inCondition,buidInCondition);
        }

      
        /// <summary>
        /// 返工汇总
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        internal IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(IDictionary<string, string> ordercondition, string searchKey)
        {
            return ReportQueryService.SearchReworkCollAndDefectAnalysisReport( ordercondition, searchKey);
        }
        /// <summary>
        /// 返工详情报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        internal IList<ReworkDetailReportModel> SearchReworkDetailReport(IDictionary<string, string> ordercondition, string searchKey)
        {
            return ReportQueryService.SearchReworkDetailReport(ordercondition, searchKey);
        }

        /// <summary>
        /// 产量汇总报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<YieldCollectModel> SearchYieldCollect(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string processOrderNo = null, string styleCode = null, string PO = null, string flowSelection = null, string color = null, string size = null, string workshop = null, string groupNo = null, string beginDate = null, string endDate = null)
        {
            var inCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(PO))
            {
                searchDic.Add("Po=", PO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            if (!string.IsNullOrEmpty(color))
            {
                searchDic.Add("PColor=", color);
            }
            if (!string.IsNullOrEmpty(size))
            {
                searchDic.Add("PSize=", size);
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND InsertDateTime<={0}", (DateTime.Parse(endDate).ToString("yyyy-MM-dd") + " 23:59").FormatDBValue());
                //searchDic.Add("QueryDate<=", endDate);
            }
            return ReportQueryService.SearchYieldCollect(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, inCondition);
        }
       

        /// <summary>
        /// 工时分析
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string processOrderNo = null, string styleCode = null, string PO = null, string flowSelection = null, string color = null, string size = null, string workshop = null, string groupNo = null, string beginDate = null, string endDate = null)
        {
            string buidInCondition = string.Empty;
            var inCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(PO))
            {
                searchDic.Add("Po=", PO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            //if (!string.IsNullOrEmpty(color))
            //{
            //    searchDic.Add("PColor=", color);
            //}
            //if (!string.IsNullOrEmpty(size))
            //{
            //    searchDic.Add("PSize=", size);
            //}
            if (!string.IsNullOrEmpty(color))
            {
               
                buidInCondition += $" AND T1.PColor='{color}'";
            }
            if (!string.IsNullOrEmpty(size))
            {
                //searchDic.Add("PSize=", size);
                buidInCondition += $" AND T1.PSize='{size}'";
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime<={0}", endDate.FormatDBValue());
            }
            return ReportQueryService.SearchWorkingHoursAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, inCondition,buidInCondition);
        }
        /// <summary>
        /// 产量汇总报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="processOrderNo"></param>
        /// <param name="styleCode"></param>
        /// <param name="pO"></param>
        /// <param name="flowSelection"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="workshop"></param>
        /// <param name="groupNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal IList<YieldCollectModel> SearchYieldCollect(IDictionary<string, string> ordercondition, string processOrderNo, string styleCode, string pO, string flowSelection, string color, string size, string workshop, string groupNo, string beginDate, string endDate)
        {
            var inCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(pO))
            {
                searchDic.Add("Po=", pO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            if (!string.IsNullOrEmpty(color))
            {
                searchDic.Add("PColor=", color);
            }
            if (!string.IsNullOrEmpty(size))
            {
                searchDic.Add("PSize=", size);
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND InsertDateTime<={0}", (DateTime.Parse(endDate).ToString("yyyy-MM-dd") + " 23:59").FormatDBValue());
                //searchDic.Add("QueryDate<=", endDate);
            }
            return ReportQueryService.SearchYieldCollect(ordercondition, searchDic, inCondition);
        }
        /// <summary>
        /// 产出明细报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="processOrderNo"></param>
        /// <param name="styleCode"></param>
        /// <param name="pO"></param>
        /// <param name="flowSelection"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="workshop"></param>
        /// <param name="groupNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal IList<ProductItemReportModel> SearchProductItemReportReport(IDictionary<string, string> ordercondition, string processOrderNo, string styleCode, string pO, string flowSelection, string color, string size, string workshop, string groupNo, string beginDate, string endDate)
        {
            string inCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(pO))
            {
                searchDic.Add("Po=", pO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            if (!string.IsNullOrEmpty(color))
            {
                searchDic.Add("PColor=", color);
            }
            if (!string.IsNullOrEmpty(size))
            {
                searchDic.Add("PSize=", size);
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime<={0}", endDate.FormatDBValue());
            }
            var list = ReportQueryService.SearchProductItemReportReport(ordercondition, searchDic, inCondition);
            list.ToList().ForEach(delegate (ProductItemReportModel pir)
            {
                if (pir.StanardSecond != null && pir.StanardSecond.Value != 0 && null != pir.WorkHours)
                {
                    var rResult = Convert.ToDecimal(pir.StanardSecond.Value.ToString()) / pir.WorkHours.Value;
                    pir.WorkRate = string.Format("{0}%", (rResult * 100).ToString("#0.00"));
                }
            });
            return list;
        }
        /// <summary>
        /// 产出明细报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ProductItemReportModel> SearchProductItemReportReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string processOrderNo = null, string styleCode = null, string PO = null, string flowSelection = null, string color = null, string size = null, string workshop = null, string groupNo = null, string beginDate = null, string endDate = null)
        {
            string inCondition = string.Empty;
            IDictionary<string, string> searchDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(processOrderNo))
            {
                searchDic.Add("ProcessOrderNo=", processOrderNo);
            }
            if (!string.IsNullOrEmpty(styleCode))
            {
                searchDic.Add("StyleNo=", styleCode);
            }
            if (!string.IsNullOrEmpty(PO))
            {
                searchDic.Add("Po=", PO);
            }
            if (!string.IsNullOrEmpty(flowSelection))
            {
                searchDic.Add("FlowSection=", flowSelection);
            }
            if (!string.IsNullOrEmpty(color))
            {
                searchDic.Add("PColor=", color);
            }
            if (!string.IsNullOrEmpty(size))
            {
                searchDic.Add("PSize=", size);
            }
            if (!string.IsNullOrEmpty(workshop))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO IN(SELECT GroupNO from SiteGroup where WorkshopCode like '%{0}%'))", workshop);
            }
            if (!string.IsNullOrEmpty(groupNo))
            {
                inCondition += string.Format(" AND T1.ProductsId in(SELECT ID FROM Products WHERE GroupNO like '%{0}%')", groupNo);
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime>={0}", DateTime.Parse(beginDate).ToString("yyyy-MM-dd").FormatDBValue());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                inCondition += string.Format(" AND T1.InsertDateTime<={0}", endDate.FormatDBValue());
            }
            var list = ReportQueryService.SearchProductItemReportReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, inCondition);
            list.ToList().ForEach(delegate (ProductItemReportModel pir)
            {
                if (pir.StanardSecond != null && pir.StanardSecond.Value != 0 && null != pir.WorkHours)
                {
                    var rResult = Convert.ToDecimal(pir.StanardSecond.Value.ToString()) / pir.WorkHours.Value;
                    pir.WorkRate = string.Format("{0}%", (rResult * 100).ToString("#0.00"));
                }
            });
            return list;
        }
        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        internal IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(IDictionary<string, string> ordercondition, string searchKey, string flowChartId)
        {
            var sql = "select * from Stating";
            var statingList = CommonAction.Instance.QueryBySql<Stating>(sql, null).ToList<Stating>();
            var dicStatingInNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);

            var dicAllocationNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);

            var rslt1 = ReportQueryService.SearchFlowBalanceTableReportModel(ordercondition, searchKey, flowChartId);
            //在线数及站内数修正
            rslt1.ToList().ForEach(delegate (FlowBalanceTableReportModel pbtr)
            {
                var capacity = 0;
                long inStatingHangerCount = 0;
                long onlineHangerCount = 0;
                if (null != pbtr.AllocationMainTrackeStatings)
                {
                    var mainStatings = pbtr.AllocationMainTrackeStatings?.Split(',');
                    foreach (var ms in mainStatings)
                    {

                        if (dicStatingInNumCache.Keys.Contains(ms))
                        {
                            var iStatingCount = dicStatingInNumCache[ms];
                            inStatingHangerCount += iStatingCount;
                        }
                        if (dicAllocationNumCache.Keys.Contains(ms))
                        {
                            var onlineCount = dicAllocationNumCache[ms];
                            onlineHangerCount += onlineCount;
                        }
                        var mainTrackNumber = int.Parse(ms.Split(':')[0]);
                        var statingNo = int.Parse(ms.Split(':')[1]);
                        var statingFilter = statingList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && int.Parse(f.StatingNo) == statingNo);
                        capacity += statingFilter.Count() > 0 ? statingFilter.First().Capacity.Value : 0;

                    }

                    pbtr.InStatingHangerCount = inStatingHangerCount;
                    pbtr.OnlineHangerCount = onlineHangerCount;
                    pbtr.CurrentHangerCount = Convert.ToDecimal(pbtr.OnlineHangerCount + pbtr.InStatingHangerCount) / capacity;
                    pbtr.Capacity = capacity.ToString();
                }
            });
            return rslt1;
        }
    }
}

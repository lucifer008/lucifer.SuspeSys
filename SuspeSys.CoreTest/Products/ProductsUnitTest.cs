using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Report;
using System.Collections.Generic;
using SuspeSys.Client.Action.Report;

namespace SuspeSys.CoreTest.Products
{
    [TestClass]
    public class ProductsUnitTest : QueryTestBase
    {
        [TestMethod]
        public void TestQueryAll()
        {
            TestOnlineProductsInfo();
            TestYieldCollect();
            TestEmployeeYield();
            TestWorkingHoursAnalysisReport();
            TestSearchProductItemReportReport();
            TestSearchReworkDetailReport();
            TestSearchReworkCollAndDefectAnalysisReport();
            TestSearchGroupCompetitionReport();
            TestSearchFlowBalanceTableReportModel();
        }
        [TestMethod]
        public void TestOnlineProductsInfo()
        {
            log.Info("-----------------------TestOnlineProductsInfo----------------------------");
            var pService = new ProductsQueryServiceImpl();
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            string searchKey = null;
            string groupNo = null;

            var list = pService.SearchProductsList(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, groupNo);
            foreach (var item in list)
            {
                log.Info($"制单号:{item.ProcessOrderNo?.Trim()} 状态:{item.StatusText?.Trim()} 订单号:{item.OrderNo?.Trim()} 款号:{item.StyleNo?.Trim()} PO:{item.Po} 颜色:{item.PColor} 尺码:{item.PSize} 组别:{item.GroupNo?.Trim()} 排产号:{item.ProductionNumber} 挂片站:{item.HangingPieceSiteNo} 任务数量:{item.TaskNum} 在线数:{item.OnlineNum} 今日挂片:{item.TotalHangingPieceSiteNum} 今日产出:{item.TodayProdOutNum} 今日返工:{item.TodayRework} 累计挂片:{item.TotalHangingNum} 累计产出:{item.TotalProdOutNum} 累计返工:{item.TodayRework}");
            }
            log.Info("-----------------------TestOnlineProductsInfo----------------------------");
        }
        [TestMethod]
        public void TestYieldCollect()
        {
            log.Info("-----------------------TestYieldCollect----------------------------");
            var pService = new ProductsQueryServiceImpl();
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var reportQueryService = new ReportQueryServiceImpl();
            string inCondition = null;
            IDictionary<string, string> searchDic = null;
            var listData = reportQueryService.SearchYieldCollect(ordercondition, searchDic, null);
            var list = reportQueryService.SearchYieldCollect(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, inCondition);
            foreach (var item in list)
            {
                log.Info($"制单号:{item.ProcessOrderNo?.Trim()} 订单号:{item.OrderNo?.Trim()} 款号:{item.StyleNo?.Trim()} PO:{item.Po} 颜色:{item.PColor} 尺码:{item.PSize} 组别:{item.GroupNo?.Trim()} 工段:{item.FlowSection} 投入量:{item.HangingPieceCount} 产出量:{item.OutYield} 返工量:{item.ReturnYield} 返工率:{item.ReturnRate}");
            }
            log.Info("-----------------------TestYieldCollect----------------------------");
        }
        [TestMethod]
        public void TestEmployeeYield()
        {
            log.Info("-----------------------TestEmployeeYield----------------------------");
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var pService = new ReportQueryServiceImpl();
            string searchKey = null;
            var list = pService.SearchEmployeeYield(ordercondition, searchKey);
            var listData = pService.SearchEmployeeYield(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            foreach (var item in list)
            {
                log.Info($"日期:{item.InsertDateTime?.Trim()} 站点:{item.GroupNo?.Trim() + "-" + item.SiteNo} 员工:{item.EmployeeName?.Trim()} 工号:{item.EmployeeNo} 制单号:{item.ProcessOrderNo} 款号:{item.StyleCode} 颜色:{item.PColor?.Trim()} 尺码:{item.PSize} PO号:{item.PurchaseOrderNo} 工段:{item.FlowSection} 工序号:{item.FlowNo?.Trim()} 工序代码:{item.ProcessFlowCode?.Trim()} 工序名称:{item.ProcessFlowName?.Trim()} 产出量:{item.YieldCount} 返工量:{item.ReworkCount} 返工率:{item.ReworkRate} 标准工时(分钟):{item.StanardHours} 实时总工时(分钟):{item.RealyWorkMin} 车缝效率:{item.SeamsRate} 工价(元):{item.StandardPrice} 收入(元):{item.Income}");
            }
            log.Info("-----------------------TestEmployeeYield----------------------------");
        }

        [TestMethod]
        public void TestWorkingHoursAnalysisReport()
        {
            log.Info("-----------------------TestWorkingHoursAnalysisReport----------------------------");
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var pService = new ReportQueryServiceImpl();
            string searchKey = null;
            IDictionary<string, string> searchDic = null;
            var list = pService.SearchWorkingHoursAnalysisReport(ordercondition, searchDic, null, null);
            var listData = pService.SearchWorkingHoursAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, searchKey,null);
            foreach (var item in listData)
            {
                log.Info($"制单号:{item.ProcessOrderNo} 款号:{item.StyleCode} 加工顺序:{item.FlowIndex} 工序号:{item.FlowNo?.Trim()} 工序代码:{item.ProcessFlowCode?.Trim()} 工序名称:{item.ProcessFlowName?.Trim()} 员工:{item.EmployeeName} 标准工时(秒):{item.StanardHours} 平均用时(秒):{item.AvgTimes} ");
            }
            log.Info("-----------------------TestWorkingHoursAnalysisReport----------------------------");
        }
        [TestMethod]
        public void TestSearchProductItemReportReport()
        {
            log.Info("-----------------------TestSearchProductItemReportReport----------------------------");
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var pService = new ReportQueryServiceImpl();
            string searchKey = null;
            IDictionary<string, string> searchDic = null;
            var list = pService.SearchProductItemReportReport(ordercondition, searchDic, null);
            var listData = pService.SearchProductItemReportReport(currentPageIndex, pageSize, out totalCount, ordercondition, searchDic, searchKey);
            foreach (var item in listData)
            {
                log.Info($"制单号:{item.ProcessOrderNo} 款号:{item.StyleCode} 加工顺序:{item.FlowIndex} 工序号:{item.FlowNo?.Trim()} 工序代码:{item.ProcessFlowCode?.Trim()} 工序名称:{item.ProcessFlowName?.Trim()} 员工:{item.EmployeeName} ");
            }
            log.Info("----------------------TestSearchProductItemReportReport-----------------------------");
        }
        [TestMethod]
        public void TestSearchReworkDetailReport()
        {
            log.Info("-----------------------TestSearchReworkDetailReport----------------------------");
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var pService = new ReportQueryServiceImpl();
            string searchKey = null;
            IDictionary<string, string> searchDic = null;
            var list = pService.SearchReworkDetailReport(ordercondition, searchKey);
            var listData = pService.SearchReworkDetailReport(currentPageIndex, pageSize, out totalCount, ordercondition, null);
            foreach (var item in listData)
            {
                log.Info($"品检时间:{item.ReworkDate} 制单号:{item.ProcessOrderNo} 款号:{item.StyleNo} 颜色:{item.PColor} 尺码:{item.PSize} PO号:{item.PO} 工段:{item.FlowSection} 工序号:{item.FlowNo?.Trim()} 工序代码:{item.FlowCode?.Trim()} 布匹号:{item.ClothNumber?.Trim()} 条码:{item.BarCode} 衣架号:{item.HangerNo} 数量:{item.Num} 疵点:{item.DefectCode} 疵点名称:{item.DefectName} 返工序号:{item.ReworkIndex} 生产组别:{item.GroupNo} 生产站点:{item.StatingNo} 品检员工:{item.ReworkEmployeeName} 品检组别:{item.ReworkGroupNo} 品检站点:{item.ReworkStatingNo} 品检工序号:{item.CheckReworkNo} 品检工序代码:{item.CheckReworkCode}");
            }
            log.Info("----------------------TestSearchReworkDetailReport-----------------------------");
        }
        [TestMethod]
        public void TestSearchReworkCollAndDefectAnalysisReport()
        {
            log.Info("-----------------------TestSearchReworkCollAndDefectAnalysisReport----------------------------");
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            var pService = new ReportQueryServiceImpl();
            string searchKey = null;
            IDictionary<string, string> searchDic = null;
            var list = pService.SearchReworkCollAndDefectAnalysisReport(ordercondition, searchKey);
            var listData = pService.SearchReworkCollAndDefectAnalysisReport(currentPageIndex, pageSize, out totalCount, ordercondition, null);
            foreach (var item in listData)
            {
                log.Info($"时间:{item.InspectionDate} 组别:{item.GroupNo} 制单号:{item.ProcessOrderNo} 工序号:{item.FlowNo} 工序名称:{item.FlowName} 产量:{item.Yield} 返工量:{item.ReworkYield} 返工率:{item.RewokRate}");
            }
            log.Info("----------------------TestSearchReworkCollAndDefectAnalysisReport-----------------------------");
        }

        [TestMethod]
        public void TestSearchGroupCompetitionReport()
        {
            log.Info("-----------------------TestSearchGroupCompetitionReport----------------------------");
            var pService = new ReportQueryServiceImpl();

            var model = pService.SearchGroupCompetitionReport(null);
            foreach (var item in model.OutProductDataList)
            {
                var rate = item.TargetYield == 0 ? 0 : (double.Parse(item.DailyYield.ToString()) / item.TargetYield);
                log.Info($"日产量:{item.DailyYield} 目标产量:{item.TargetYield} 不良数:{item.DefectCount} 达成率:{rate}");
            }
            log.Info("----------------------TestSearchGroupCompetitionReport-----------------------------");
        }

        [TestMethod]
        public void TestSearchFlowBalanceTableReportModel()
        { 
            log.Info("-----------------------TestSearchFlowBalanceTableReportModel----------------------------");
            var pAction = ReportQueryAction.Instance;
            int currentPageIndex = 1;
            int pageSize = 10;
            long totalCount = 0;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;

            string searchKey = null;
            IDictionary<string, string> searchDic = null;
            //var listData2 = pAction.SearchFlowBalanceTableReportModel(ordercondition, null, "39b76b82c50c4cb8a191d0be5a240ac6");
            var listData = pAction.SearchFlowBalanceTableReportModel(currentPageIndex, pageSize, out totalCount, ordercondition, null, "39b76b82c50c4cb8a191d0be5a240ac6");
            foreach (var item in listData)
            {
                log.Info($"工序号:{item.FlowNo} 工序代码:{item.FlowCode} 工序名称:{item.FlowName} SAM(分钟):{item.SAM} 分配的站点:{item.AllocationStatings} 工序代码:{item.FlowCode} 今日产量:{item.TodayYield} 累计产量:{item.TotalYield} 线上衣数:{item.OnlineHangerCount} 站内衣数:{item.InStatingHangerCount} 当前总衣数:{item.CurrentHangerCount}");
            }
            log.Info("----------------------TestSearchFlowBalanceTableReportModel-----------------------------");
        }
    }
}

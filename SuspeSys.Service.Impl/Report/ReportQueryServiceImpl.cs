using SuspeSys.Domain.Ext.ReportModel;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Service.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Report
{
    public class ReportQueryServiceImpl : ServiceBase, Service.Report.IReportQueryService
    {
        /// <summary>
        /// 疵点分析图
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public DefectAnalysisReportModel SearchDefectAnalysisReport(string searchKey)
        {
            var model = new DefectAnalysisReportModel();
            var sql = string.Format(@"
                SELECT DefectCode,DefectName,SUM(DefectCount) DefectCount FROM(
                SELECT T1.DefectCode,T2.DefectName,COUNT(*) DefectCount
                  FROM [HangerProductFlowChart] T1
                  INNER JOIN [dbo].[DefectCodeTable] T2 ON T1.DefectCode=T2.DefectCode
                   where T1.FlowType=1 {0}
                   GROUP BY T1.DefectCode,T2.DefectName
                   UNION ALL 
                   SELECT T1.DefectCode,T2.DefectName,COUNT(*) DefectCount
                  FROM [SuccessHangerProductFlowChart] T1
                  INNER JOIN [dbo].[DefectCodeTable] T2 ON T1.DefectCode=T2.DefectCode
                   where T1.FlowType=1 {0}
                   GROUP BY T1.DefectCode,T2.DefectName)Res
                   GROUP BY DefectCode,DefectName
                ", string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            //sql = string.Format(sql, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<DefectAnalysisReportDetailModel>(sql, true, null);
            model.DefectAnalysisReportDetailModelList = rslt1;
            model.TotalCount = rslt1.Sum(f => f.DefectCount);
            foreach (var dc in model.DefectAnalysisReportDetailModelList)
            {
                dc.DefectRate = ((decimal.Parse(dc.DefectCount.ToString()) / model.TotalCount) * 100).ToString("#0.00") + "%";
            }

            model.DefectNameList = rslt1.Select(f => f.DefectName).Distinct().ToList<string>();
            return model;
        }
        /// <summary>
        /// 返工详情报表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ReworkDetailReportModel> SearchReworkDetailReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            //            string queryString = @"
            ////SELECT ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode, Num,DefectCode,DefectName,StatingNo,GroupNo,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by Num ASC)) as ID FROM(

            ////select ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,COUNT(*) as Num,DefectCode,DefectName,StatingNo,GroupNo from (

            //// select T1.HangerNo,CONVERT(VARCHAR(19),T1.ReworkDate,121)ReworkDate,T1.ReworkEmployeeName,T1.ReworkStatingNo,T2.ProcessOrderNo,T2.StyleNo,T2.PColor,T2.PO,T2.PSize,T2.FlowSection,T1.FlowNo,T1.FlowCode,
            ////                                1 as Num,T1.DefectCode,(SELECT top 1 dc.DefectName From DefectCodeTable dc where dc.DefectCode=t1.DefectCode and dc.deleted=0) DefectName
            ////                                ,T1.StatingNo,T2.GroupNo,ROW_NUMBER() over(PARTITION by HangerNo order by T1.CompareDate desc) ReworkIndex
            ////                                 from HangerProductFlowChart T1
            ////                                INNER JOIN Products T2 ON T1.ProductsId=T2.Id
            ////                                WHERE T1.FlowType=1 and T1.Status=2 and (CAST (T1.HangerNo as int))>0 {0}
            ////                                UNION ALL
            ////                                select  T1.HangerNo,CONVERT(VARCHAR(19),T1.ReworkDate,121)ReworkDat,T1.ReworkEmployeeName,T1.ReworkStatingNo,T2.ProcessOrderNo,T2.StyleNo,T2.PColor,T2.PO,T2.PSize,T2.FlowSection,T1.FlowNo,T1.FlowCode,
            ////                                1 as Num,T1.DefectCode,(SELECT top 1 dc.DefectName From DefectCodeTable dc where dc.DefectCode=t1.DefectCode and dc.deleted=0) DefectName
            ////                                ,T1.StatingNo,T2.GroupNo,ROW_NUMBER() over(PARTITION by HangerNo order by T1.CompareDate desc) ReworkIndex
            ////                                 from SuccessHangerProductFlowChart T1
            ////                                INNER JOIN SucessProducts T2 ON T1.ProductsId=T2.Id
            ////                                WHERE T1.FlowType=1 and T1.Status=2 and (CAST (T1.HangerNo as int))>0 {0}
            ////)Res GROUP BY ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,DefectCode,DefectName,StatingNo,GroupNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,HangerNo,ReworkIndex
            ////                                )ZZ WHERE 1=1

            //";
            string queryString = @"
SELECT ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode, Num,DefectCode,DefectName,StatingNo,GroupNo,ReworkGroupNo,CheckReworkCode,CheckReworkNo,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by Num ASC)) as ID FROM(
                              
select ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,COUNT(*) as Num,DefectCode,DefectName,StatingNo,GroupNo,ReworkGroupNo,CheckReworkCode,CheckReworkNo from (

 select T1.HangerNo,CONVERT(VARCHAR(19),T1.ReworkDate,121)ReworkDate,T1.ReworkEmployeeName,T1.ReworkStatingNo,T2.ProcessOrderNo,T2.StyleNo,T2.PColor,T2.PO,T2.PSize,T2.FlowSection,T1.FlowNo,T1.FlowCode,
                                1 as Num,T1.DefectCode,(SELECT top 1 dc.DefectName From DefectCodeTable dc where dc.DefectCode=t1.DefectCode and dc.deleted=0) DefectName
                                ,T1.StatingNo,T1.GroupNo,ROW_NUMBER() over(PARTITION by HangerNo order by T1.CompareDate desc) ReworkIndex,T1.ReworkGroupNo,T1.CheckReworkCode,T1.CheckReworkNo
                                 from View_HangerFlowChart T1
                                INNER JOIN Products T2 ON T1.ProductsId=T2.Id
                                WHERE T1.FlowType=1 and T1.Status=2 and (CAST (T1.HangerNo as int))>0 {0}
)Res GROUP BY ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,DefectCode,DefectName,StatingNo,GroupNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,HangerNo,ReworkIndex,ReworkGroupNo,CheckReworkCode,CheckReworkNo
                                )ZZ WHERE 1=1
";
            string[] paramValues = null;
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    queryString = string.Format(queryString, @" AND (ProcessOrderNo like ? OR StyleNo like ? OR FlowNo like ? OR DefectCode like ? OR StatingNo like ?)");
            //    paramValues = new string[] {
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //    string.Format("%{0}%", searchKey) };
            //    // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //}
            //else
            //{
            //    queryString = string.Format(queryString, string.Empty);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<ReworkDetailReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        /// <summary>
        /// 查询员工产量
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<EmployeeYieldReportModel> SearchEmployeeYield(IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"
 select Res_Main.*,Res_Out.YieldCount,Res_Rework.ReworkCount,RealyWorkMin,UnitCunt,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by YieldCount ASC)) as ID from(
 SELECT distinct CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
  T5.PO PurchaseOrderNo,T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardMinute StanardHours,T8.StandardPrice,T1.GroupNo,T5.StyleNo,T1.FlowNo,T1.EmployeeNo 
                                FROM View_HangerFlowChart T1  
                                LEFT JOIN ProcessOrder T2 ON T1.ProcessOrderNo=T2.POrderNo
                                LEFT JOIN ProcessOrderColorItem T3 ON T3.PROCESSORDER_Id=T2.Id
                                LEFT JOIN CustomerPurchaseOrder T4 ON T4.Id=T3.CUSTOMERPURCHASEORDER_Id
                                LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
                                LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
                                LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
                                WHERE t1.Status=2  {0}
								)Res_Main
								left join (
									
									select  InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,COUNT(*) YieldCount,SUM(RealyWorkMin) RealyWorkMin,COUNT(*)*SUM(Unit) UnitCunt,FlowNo from(
										select  CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
		 T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo,datediff(second, T1.CompareDate,T1.OutSiteDate) RealyWorkMin,CAST (T5.Unit as int) Unit,T1.FlowNo
										FROM View_HangerFlowChart T1
										LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
										LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
										LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
										
										WHERE T1.Status=2 and FlowType in(0,2) and HangerNo>0  {0}
										
									)T_OUt Group by InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,FlowNo
									
								) Res_Out on (Res_Main.InsertDateTime=Res_Out.InsertDateTime and
									Res_Main.EmployeeName=Res_Out.EmployeeName and
									Res_Main.ProcessOrderNo=Res_Out.ProcessOrderNo and
									Res_Main.PColor=Res_Out.PColor and
									Res_Main.PSize=Res_Out.PSize and
									Res_Main.ProcessFlowCode=Res_Out.ProcessFlowCode and
									Res_Main.ProcessFlowName=Res_Out.ProcessFlowName and
									Res_Main.SiteNo=Res_Out.SiteNo and
									Res_Main.FlowSection=Res_Out.FlowSection and
									Res_Main.FlowNo=Res_Out.FlowNo and
									Res_Main.GroupNo=Res_Out.GroupNo 
								)
								left join (
									select  InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,COUNT(*) ReworkCount,FlowNo from(
										select  CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
		 T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo,T1.FlowNo
										FROM View_HangerFlowChart T1 
										LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
										LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
										LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
										WHERE T1.Status=2 and FlowType=1 and HangerNo>0 {0}
									)T_Rework Group by InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,FlowNo

								) Res_Rework on (Res_Main.InsertDateTime=Res_Rework.InsertDateTime and
									Res_Main.EmployeeName=Res_Rework.EmployeeName and
									Res_Main.ProcessOrderNo=Res_Rework.ProcessOrderNo and
									Res_Main.PColor=Res_Rework.PColor and
									Res_Main.PSize=Res_Rework.PSize and
									Res_Main.ProcessFlowCode=Res_Rework.ProcessFlowCode and
									Res_Main.ProcessFlowName=Res_Rework.ProcessFlowName and
									Res_Main.SiteNo=Res_Rework.SiteNo and
									
									Res_Main.FlowSection=Res_Rework.FlowSection and
									Res_Main.FlowNo=Res_Rework.FlowNo and
									Res_Main.GroupNo=Res_Rework.GroupNo)";
            string[] paramValues = null;

            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    //queryString += string.Format(@" AND (ProductionNumber like ? OR HangingPieceSiteNo like ? OR ProcessOrderNo like ? OR PColor like ? OR PSize like ? OR LineName like ?)");
            //    //paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            //    //// queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //    queryString = string.Format(queryString,string.IsNullOrEmpty(searchKey)?string.Empty:searchKey);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<EmployeeYieldReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            return rslt1;
        }

        /// <summary>
        /// 查询员工产量：工序出战就算不管衣架是否生产完成
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<EmployeeYieldReportModel> SearchEmployeeYield(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {

            //            string queryString = @"SELECT *,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by YieldCount ASC)) as ID FROM(
            //                                SELECT InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize,ProcessFlowCode,ProcessFlowName,SiteNo,SUM(YieldCount) YieldCount,
            //                                PurchaseOrderNo,FlowSection,FlowIndex,ProcessFlowId,StanardHours,StandardPrice,GroupNo
            //                                 FROM(
            //                                SELECT distinct CONVERT(varchar(10),T1.InsertDateTime,120)InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.ProcessFlowCode,T1.ProcessFlowName,T1.SiteNo,(T1.SizeNum)YieldCount,
            //                                T4.PurchaseOrderNo,T5.FlowSection,T1.FlowIndex,T1.ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo
            //                                FROM SucessEmployeeFlowProduction T1  
            //                                LEFT JOIN ProcessOrder T2 ON T1.ProcessOrderId=T2.Id
            //                                LEFT JOIN ProcessOrderColorItem T3 ON T3.PROCESSORDER_Id=T2.Id
            //                                LEFT JOIN CustomerPurchaseOrder T4 ON T4.Id=T3.CUSTOMERPURCHASEORDER_Id
            //                                LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
            //                                LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.FlowChartd
            //                                LEFT JOIN ProcessFlow T8 ON T8.Id=T1.ProcessFlowId 
            //                                WHERE 1=1  {0}
            //                                UNION ALL
            //                                SELECT distinct CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.ProcessFlowCode,T1.ProcessFlowName,T1.SiteNo,(T1.SizeNum)YieldCount,
            //                                T4.PurchaseOrderNo,T5.FlowSection,T1.FlowIndex,T1.ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo
            //                                FROM SucessEmployeeFlowProduction T1  
            //                                LEFT JOIN ProcessOrder T2 ON T1.ProcessOrderId=T2.Id
            //                                INNER JOIN ProcessOrderColorItem T3 ON T3.PROCESSORDER_Id=T2.Id
            //                                INNER JOIN CustomerPurchaseOrder T4 ON T4.Id=T3.CUSTOMERPURCHASEORDER_Id
            //                                INNER JOIN SucessProducts T5 ON T5.Id=T1.ProductsId
            //                                INNER JOIN ProcessFlowChart T6 ON T6.Id=T1.FlowChartd
            //                                INNER JOIN ProcessFlow T8 ON T8.Id=T1.ProcessFlowId 
            //                                WHERE 1=1  {0}
            //								  UNION ALL
            //                                SELECT distinct CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.ProcessFlowCode,T1.ProcessFlowName,T1.SiteNo,(T1.SizeNum)YieldCount,
            //                                T4.PurchaseOrderNo,T5.FlowSection,T1.FlowIndex,T1.ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo
            //                                FROM EmployeeFlowProduction T1  
            //                                LEFT JOIN ProcessOrder T2 ON T1.ProcessOrderId=T2.Id
            //                                LEFT JOIN ProcessOrderColorItem T3 ON T3.PROCESSORDER_Id=T2.Id
            //                                LEFT JOIN CustomerPurchaseOrder T4 ON T4.Id=T3.CUSTOMERPURCHASEORDER_Id
            //                                LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
            //                                LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.FlowChartd
            //                                LEFT JOIN ProcessFlow T8 ON T8.Id=T1.ProcessFlowId 
            //                                WHERE 1=1  {0}
            //                                )Z
            //                                GROUP BY InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize,ProcessFlowCode,ProcessFlowName,SiteNo,
            //                                PurchaseOrderNo,FlowSection,FlowIndex,ProcessFlowId,StanardHours,StandardPrice,GroupNo
            //                                )ZZ
            //";
            string queryString = @"
 select Res_Main.*,Res_Out.YieldCount,Res_Rework.ReworkCount,RealyWorkMin,UnitCunt,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by YieldCount ASC)) as ID from(
 SELECT distinct CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
  T5.PO PurchaseOrderNo,T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardMinute StanardHours,T8.StandardPrice,T1.GroupNo,T5.StyleNo,T1.FlowNo,T1.EmployeeNo 
                                FROM View_HangerFlowChart T1  
                                LEFT JOIN ProcessOrder T2 ON T1.ProcessOrderNo=T2.POrderNo
                                LEFT JOIN ProcessOrderColorItem T3 ON T3.PROCESSORDER_Id=T2.Id
                                LEFT JOIN CustomerPurchaseOrder T4 ON T4.Id=T3.CUSTOMERPURCHASEORDER_Id
                                LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
                                LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
                                LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
                                WHERE t1.Status=2  {0}
								)Res_Main
								left join (
									
									select  InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,COUNT(*) YieldCount,SUM(RealyWorkMin) RealyWorkMin,COUNT(*)*SUM(Unit) UnitCunt,FlowNo from(
										select  CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
		 T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo,datediff(second, T1.CompareDate,T1.OutSiteDate) RealyWorkMin,CAST (T5.Unit as int) Unit,T1.FlowNo
										FROM View_HangerFlowChart T1
										LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
										LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
										LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
										
										WHERE T1.Status=2 and FlowType in(0,2) and HangerNo>0  {0}
										
									)T_OUt Group by InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,FlowNo
									
								) Res_Out on (Res_Main.InsertDateTime=Res_Out.InsertDateTime and
									Res_Main.EmployeeName=Res_Out.EmployeeName and
									Res_Main.ProcessOrderNo=Res_Out.ProcessOrderNo and
									Res_Main.PColor=Res_Out.PColor and
									Res_Main.PSize=Res_Out.PSize and
									Res_Main.ProcessFlowCode=Res_Out.ProcessFlowCode and
									Res_Main.ProcessFlowName=Res_Out.ProcessFlowName and
									Res_Main.SiteNo=Res_Out.SiteNo and
									Res_Main.FlowSection=Res_Out.FlowSection and
									Res_Main.FlowNo=Res_Out.FlowNo and
									Res_Main.GroupNo=Res_Out.GroupNo 
								)
								left join (
									select  InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,COUNT(*) ReworkCount,FlowNo from(
										select  CONVERT(varchar(10),T1.InsertDateTime,120) InsertDateTime,T1.EmployeeName,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.FlowCode ProcessFlowCode,T1.FlowName ProcessFlowName,T1.StatingNo SiteNo,
		 T5.FlowSection,T1.FlowIndex,T1.FlowId ProcessFlowId,T8.StanardHours,T8.StandardPrice,T1.GroupNo,T1.FlowNo
										FROM View_HangerFlowChart T1 
										LEFT JOIN Products T5 ON T5.Id=T1.ProductsId
										LEFT JOIN ProcessFlowChart T6 ON T6.Id=T1.ProcessChartId
										LEFT JOIN ProcessFlow T8 ON T8.Id=T1.FlowId 
										WHERE T1.Status=2 and FlowType=1 and HangerNo>0 {0}
									)T_Rework Group by InsertDateTime,EmployeeName,ProcessOrderNo,PColor,PSize, ProcessFlowCode,ProcessFlowName,SiteNo,FlowSection, ProcessFlowId,GroupNo,FlowNo

								) Res_Rework on (Res_Main.InsertDateTime=Res_Rework.InsertDateTime and
									Res_Main.EmployeeName=Res_Rework.EmployeeName and
									Res_Main.ProcessOrderNo=Res_Rework.ProcessOrderNo and
									Res_Main.PColor=Res_Rework.PColor and
									Res_Main.PSize=Res_Rework.PSize and
									Res_Main.ProcessFlowCode=Res_Rework.ProcessFlowCode and
									Res_Main.ProcessFlowName=Res_Rework.ProcessFlowName and
									Res_Main.SiteNo=Res_Rework.SiteNo and
									
									Res_Main.FlowSection=Res_Rework.FlowSection and
									Res_Main.FlowNo=Res_Rework.FlowNo and
									Res_Main.GroupNo=Res_Rework.GroupNo)
								";
            string[] paramValues = null;

            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    //queryString += string.Format(@" AND (ProductionNumber like ? OR HangingPieceSiteNo like ? OR ProcessOrderNo like ? OR PColor like ? OR PSize like ? OR LineName like ?)");
            //    //paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            //    //// queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //    queryString = string.Format(queryString,string.IsNullOrEmpty(searchKey)?string.Empty:searchKey);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<EmployeeYieldReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            rslt1.ToList().ForEach(delegate (EmployeeYieldReportModel eyrModel)
            {
                eyrModel.GroupSites = string.Format($"{eyrModel.GroupNo?.Trim()}-{eyrModel.SiteNo}");
            });
            return rslt1;
        }
        /// <summary>
        /// 产量汇总报表:
        /// 产出量: 只统计工艺图中设置了产出工序的产量，只要设置的产出工序出战，产出+1
        /// 返工量：只统计工艺图中设置了产出工序返工了的次数
        /// 投入量：挂片数【(制单，颜色，尺码，po】
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<YieldCollectModel> SearchYieldCollect(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition)
        {
            //            string queryString = @"    select *,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by OutYield ASC)) as ID FROM(
            //		 select ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,SUM(OutYield)OutYield,SUM(HangingPieceCount) HangingPieceCount from(
            //			 select ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,InsertDateTime QueryDate,SUM(OutYield)OutYield,SUM(HangingPieceCount) HangingPieceCount from(
            //				 SELECT ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,OutYield,CONVERT(varchar(10),T.InsertDateTime,121)InsertDateTime,HangingPieceCount FROM( 
            //					 select CONVERT(varchar(10),InsertDateTime,121) InsertDateTime,COUNT(1) OutYield,ProductsId from SucessProcessOrderHanger WHERE 1=1 {0}
            //					 Group by  CONVERT(varchar(10),InsertDateTime,121),ProductsId
            //				)T Right JOIN Products T2 ON T2.Id=T.ProductsId
            //				INNER JOIN( 
            //					SELECT InsertDateTime,ProductsId,SUM(HangingPieceCount)HangingPieceCount FROM(
            //					SELECT CONVERT(varchar(10),InsertDateTime,121) InsertDateTime,COUNT(1) HangingPieceCount,ProductsId FROM dbo.HangerStatingAllocationItem WHERE Memo = '-1' GROUP BY  CONVERT(varchar(10),InsertDateTime,121),ProductsId)TH1
            //					GROUP BY InsertDateTime,ProductsId
            //				)TH2 ON TH2.InsertDateTime=CONVERT(varchar(10),T.InsertDateTime,121) AND T2.Id=TH2.ProductsId
            //			)Z GROUP BY ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,InsertDateTime
            //			union all
            //			 select ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,InsertDateTime QueryDate,SUM(OutYield)OutYield,SUM(HangingPieceCount) HangingPieceCount from(
            //				 select ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,SUM(OutYield)OutYield,InsertDateTime,SUM(HangingPieceCount) HangingPieceCount from(
            //					 SELECT ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,OutYield,CONVERT(varchar(10),T.InsertDateTime,121)InsertDateTime,HangingPieceCount FROM( 
            //						 select CONVERT(varchar(10),InsertDateTime,121) InsertDateTime,COUNT(1) OutYield,ProductsId from SucessProcessOrderHanger  WHERE 1=1 {0}
            //						 Group by  CONVERT(varchar(10),InsertDateTime,121),ProductsId
            //					)T Right JOIN Products T2 ON T2.Id=T.ProductsId
            //					INNER JOIN( 
            //						SELECT InsertDateTime,ProductsId,SUM(HangingPieceCount)HangingPieceCount FROM(
            //						SELECT CONVERT(varchar(10),InsertDateTime,121) InsertDateTime,COUNT(1) HangingPieceCount,ProductsId FROM dbo.SuccessHangerStatingAllocationItem WHERE Memo = '-1' AND CAST(HangerNo AS INT)>0 GROUP BY  CONVERT(varchar(10),InsertDateTime,121),ProductsId)TH1
            //						GROUP BY InsertDateTime,ProductsId
            //					)TH2 ON TH2.InsertDateTime=CONVERT(varchar(10),T.InsertDateTime,121) AND T2.Id=TH2.ProductsId
            //				)Z GROUP BY ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,InsertDateTime
            //			)R 
            //			GROUP BY ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection,InsertDateTime
            //	)Res GROUP BY ProcessOrderNo,StyleNo,PColor,Po,PSize,FlowSection
            //)TRes WHERE 1=1 
            // ";
            var queryString = @"
                   SELECT ProcessOrderNo, StyleNo, PColor, Po, PSize
	                    , FlowSection, HangingPieceCount, OutYield,ReturnYield, PS.GroupNo
	                    , CONVERT(varchar(100), ROW_NUMBER() OVER (ORDER BY ProcessOrderNo ASC)) AS Id
                    FROM Products PS
	                    LEFT JOIN (
		                    SELECT ProductsId, SUM(HangingPieceCount) AS HangingPieceCount,GroupNo
		                    FROM (
			                    SELECT ProductsId, COUNT(1) AS HangingPieceCount,GroupNo
			                    FROM SuccessHangerStatingAllocationItem
			                    WHERE Memo = '-1' 
				                    AND CAST(HangerNo AS INT) > 0 {0}
			                    GROUP BY ProductsId,GroupNo
			                    UNION ALL
			                    SELECT ProductsId, COUNT(1) AS HangingPieceCount,GroupNo
			                    FROM HangerStatingAllocationItem
			                    WHERE Memo = '-1' 
				                    AND CAST(HangerNo AS INT) > 0 {0}
			                    GROUP BY ProductsId,GroupNo
		                    ) Res
		                    GROUP BY ProductsId,GroupNo
	                    ) Res_HangingPieceCount
	                    ON PS.Id = Res_HangingPieceCount.ProductsId and ps.GroupNo=Res_HangingPieceCount.GroupNo
	                    LEFT JOIN (
		                    SELECT COUNT(1) AS OutYield, ProductsId,GroupNo
		                    FROM SucessProcessOrderHanger
		                    WHERE CAST(HangerNo AS INT) > 0 and outFlowId is not null {0}
		                    GROUP BY ProductsId,GroupNo
	                    ) Res_Prod_OUT
	                    ON Res_Prod_OUT.ProductsId = PS.Id AND Res_Prod_OUT.GroupNo=PS.GroupNo
	                    LEFT JOIN (
		                    SELECT COUNT(1) AS ReturnYield, ProductsId,GroupNo
		                    FROM (
													select distinct ProductsId,HangerNo,BatchNo,GroupNo from View_HangerFlowChart where FlowType=1 
													{0}
													GROUP BY ProductsId,HangerNo,BatchNo,GroupNo
												)Ress GROUP BY ProductsId,GroupNo
												  
	                    ) Res_Prod_Rework
	                    ON Res_Prod_Rework.ProductsId = PS.Id AND Res_Prod_Rework.GroupNo=PS.GroupNo  where 1=1 
                    ";
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
                //queryString += string.Format(@" AND (ProductionNumber like ? OR HangingPieceSiteNo like ? OR ProcessOrderNo like ? OR PColor like ? OR PSize like ? OR LineName like ?)");
                //paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
                // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<YieldCollectModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            rslt1.ToList().ForEach(delegate (YieldCollectModel ycModel)
            {
                if (ycModel.HangingPieceCount != 0)
                {
                    var rWorkCount = ycModel.ReturnYield;
                    if (rWorkCount != null)
                    {
                        ycModel.ReturnRate = ((Convert.ToDecimal(rWorkCount.ToString()) / ycModel.HangingPieceCount) * 100).Value.ToString("0.00") + "%";
                    }
                }
            });
            return rslt1;
        }

        /// <summary>
        /// 工时分析(分页)
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition, string buidInCondition)
        {
            string queryString = @"
SELECT * FROM(
select *,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by ProcessOrderNo ASC)) as ID  from(
select T1.ProcessOrderNo,T1.FlowIndex,T1.ProcessFlowCode,T1.ProcessFlowName,T1.FlowNo,T1.EmployeeName,T3.StanardSecond StanardHours,Pts.styleNo StyleCode
,AVG(datediff(second, T1.CompareDate,T1.OutSiteDate)) AvgTimes
from View_HangerFlowChart T1
INNER JOIN ProcessFlow T3 ON T3.Id=T1.ProcessFlowId
Left Join Products Pts On pts.Id=T1.ProductsId
WHERE 1=1 {0}
GROUP BY T1.ProcessOrderNo,T1.FlowIndex,T1.ProcessFlowCode,T1.ProcessFlowName,T1.FlowNo,T1.EmployeeName,T3.StanardSecond,Pts.styleNo

)Res)ZRES WHERE 1=1

 ";
            if (!string.IsNullOrEmpty(buidInCondition))
            {
                queryString = string.Format(queryString, buidInCondition);
            }
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<WorkingHoursAnalysisReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }
        /// <summary>
        /// 产出明细报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        public IList<ProductItemReportModel> SearchProductItemReportReport(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition)
        {
            string queryString = @"
SELECT * FROM(
select  
T1.InsertDateTime,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.CompareDate,T1.FlowCode ProcessFlowCode,T1.FlowNo,T1.OutSiteDate,T1.HangerNo,DateDiff(SECOND,T1.CompareDate,T1.OutSiteDate) WorkHours 
,T1.EmployeeName,T3.FlowSection,T4.StyleCode,T3.PO,T3.Unit PieceCount
,T33.StanardSecond,T3.GroupNo 
from View_HangerFlowChart T1  

INNER JOIN ProcessFlow T33 ON T33.Id=T1.FlowId
INNER JOIN  Products T3 ON T3.Id=T1.ProductsId
INNER JOIN ProcessOrder T4 ON T4.POrderNo=T3.ProcessOrderNo
WHERE HangerNo>0 And FlowType in(0,2) and T1.Status=2  {0}
)ZRES WHERE 1=1


";
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    //if(k.Equals("") && k.Equals(""))
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<ProductItemReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            return rslt1;
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
        public IList<ProductItemReportModel> SearchProductItemReportReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition = null)
        {
            string queryString = @"
SELECT * FROM(
select 
row_number() over(order by T1.InsertDateTime) Id, 
T1.InsertDateTime,T1.ProcessOrderNo,T1.PColor,T1.PSize,T1.CompareDate,T1.FlowCode ProcessFlowCode,T1.FlowNo,T1.OutSiteDate,T1.HangerNo,DateDiff(SECOND,T1.CompareDate,T1.OutSiteDate) WorkHours 
,T1.EmployeeName,T3.FlowSection,T4.StyleCode,T3.PO,T3.Unit PieceCount
,T33.StanardSecond,T1.GroupNo 
from View_HangerFlowChart T1  

INNER JOIN ProcessFlow T33 ON T33.Id=T1.FlowId
INNER JOIN  Products T3 ON T3.Id=T1.ProductsId
INNER JOIN ProcessOrder T4 ON T4.POrderNo=T3.ProcessOrderNo
WHERE HangerNo>0 And FlowType in(0,2) and T1.Status=2  {0}
)ZRES WHERE 1=1


";
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    //if(k.Equals("") && k.Equals(""))
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<ProductItemReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"
               							select Res_OutYield.*,Res_ReworkYield.ReworkYield,(ROW_NUMBER() over(ORDER by (Res_OutYield.ProcessOrderNo+Res_OutYield.FlowNo+Res_OutYield.productsId) ASC)) as ID  from(
									select productsId,Count(*) Yield,GroupNO,CONVERT(varchar(100), InsertDateTime, 23) InspectionDate,
								ProcessOrderNo,FlowNo,FlowName	from View_HangerFlowChart
										where status=2
									GROUP BY productsId,GroupNO,CONVERT(varchar(100), InsertDateTime, 23),ProcessOrderNo,GroupNo,FlowNo,FlowName
								)Res_OutYield 
								inner join(
									select productsId,Count(*) ReworkYield,GroupNO,CONVERT(varchar(100), InsertDateTime, 23) InspectionDate,
								ProcessOrderNo,FlowNo,FlowName from View_HangerFlowChart
										where status=2 and FlowType=1
									GROUP BY productsId,GroupNO,CONVERT(varchar(100), InsertDateTime, 23),ProcessOrderNo,GroupNo,FlowNo,FlowName
								)Res_ReworkYield on (
								Res_OutYield.productsId=Res_ReworkYield.productsId 
								and Res_OutYield.GroupNO=Res_ReworkYield.GroupNO 
								and Res_OutYield.InspectionDate=Res_ReworkYield.InspectionDate 
								and Res_OutYield.ProcessOrderNo=Res_ReworkYield.ProcessOrderNo 
								and Res_OutYield.FlowNo=Res_ReworkYield.FlowNo 
								and Res_OutYield.FlowName=Res_ReworkYield.FlowName 
								) where 1=1 
								 ";
            string[] paramValues = null;
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    queryString = string.Format(queryString, @" AND (ProcessOrderNo like ? OR StyleNo like ? OR FlowNo like ? OR DefectCode like ? OR StatingNo like ?)");
            //    paramValues = new string[] {
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //    string.Format("%{0}%", searchKey) };
            //    // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //}
            //else
            //{
            //    queryString = string.Format(queryString, string.Empty);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<ReworkCollAndDefectAnalysisReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            rslt1.ToList<ReworkCollAndDefectAnalysisReportModel>().ForEach(f => f.RewokRate = (100 * (decimal.Parse(f.ReworkYield.ToString()) / int.Parse(f.Yield.ToString()))).ToString("#0.00") + "%");
            return rslt1;
        }
        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, string flowChartId = null)
        {
            //            string queryString = @"select Res.*,
            //			(
            //				select 
            //				stuff((select ','+SiteNo 
            //				from (
            //				select Distinct SiteNo from(
            //					select 
            //					T3.FlowNo,T3.ProcessFlowCode FlowCode,T3.ProcessFlowName FlowName,T3.SiteNo from dbo.HangerStatingAllocationItem T3
            //				)Res_FlowInfo Where Res_FlowInfo.FlowCode=Res.FlowCode AND Res_FlowInfo.FlowNo=Res.FlowNo
            //				)T for xml path('')),1,1,'')
            //			)AllocationStatings,Res_Today_Yield.TodayYield,Res_Total_Yield.TotalYield,Res_OnlineHangerCount.OnlineHangerCount,Res_InStatingHangerCount.InStatingHangerCount,
            //		(Res_InStatingHangerCount.InStatingHangerCount/(ISNULL(Res_OnlineHangerCount.OnlineHangerCount,0)+ISNULL(Res_InStatingHangerCount.InStatingHangerCount,0)))	
            //        CurrentHangerCount,
            //(ROW_NUMBER() over(ORDER by Res.FlowNo ASC)) as ID 
            //	from(
            //		select distinct 
            //		T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName
            //			from HangerProductFlowChart T1
            //			WHERE T1.InsertDateTime is not null {0}
            //	)Res
            //	LEFT JOIN(
            //				SELECT FlowNo,FlowCode,SUM(TodayYield) TodayYield FROM(
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) TodayYield
            //						 from HangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null
            //						 AND
            //						 (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
            //														  DATEADD(day, 1, GETDATE()), 120)) AND Status=2
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)
            //				UNION ALL
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) TodayYield
            //						 from SuccessHangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null
            //						 AND
            //						 (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
            //														  DATEADD(day, 1, GETDATE()), 120)) AND Status=2
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)
            //		)Res_Today GROUP BY FlowNo,FlowCode
            //	)Res_Today_Yield ON Res_Today_Yield.FlowCode=Res.FlowCode AND Res_Today_Yield.FlowNo=Res.FlowNo
            //	LEFT JOIN(
            //				SELECT FlowNo,FlowCode,SUM(TotalYield) TotalYield FROM(
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) TotalYield
            //						 from HangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null AND Status=2
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)
            //				UNION ALL
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) TotalYield
            //						 from SuccessHangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null AND Status=2
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)
            //		)Res_Today GROUP BY FlowNo,FlowCode
            //	)Res_Total_Yield ON Res_Total_Yield.FlowCode=Res.FlowCode AND Res_Total_Yield.FlowNo=Res.FlowNo
            //	LEFT JOIN(

            //SELECT FlowNo,FlowCode,SUM(OnlineHangerCount) OnlineHangerCount FROM(
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) OnlineHangerCount
            //						 from HangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null AND Status=0 AND T1.IncomeSiteDate is null
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)

            //		)Res_OnlineHangerCount GROUP BY FlowNo,FlowCode
            //	) Res_OnlineHangerCount ON Res_OnlineHangerCount.FlowCode=Res.FlowCode AND Res_OnlineHangerCount.FlowNo=Res.FlowNo
            //	LEFT JOIN(

            //SELECT FlowNo,FlowCode,SUM(InStatingHangerCount) InStatingHangerCount FROM(
            //				select T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)FlowName,COUNT(*) InStatingHangerCount
            //						 from HangerProductFlowChart T1
            //						 WHERE T1.InsertDateTime is not null AND Status=0 AND T1.IncomeSiteDate is null
            //				GROUP BY T1.FlowNo,T1.FlowCode,RTRIM(T1.FlowName)

            //		)Res_InStatingHangerCount GROUP BY FlowNo,FlowCode
            //	) Res_InStatingHangerCount ON Res_InStatingHangerCount.FlowCode=Res.FlowCode AND Res_InStatingHangerCount.FlowNo=Res.FlowNo
            //";
            string queryString = string.Format(@"select T2.FlowNo,T2.FlowCode,T2.FlowName,
(select StanardMinute from ProcessFlow where Id=T2.PROCESSFLOW_Id)StanardHours ,
(select StandardPrice from ProcessFlow where Id=T2.PROCESSFLOW_Id)StandardPrice ,
T2.PROCESSFLOW_Id ProcessFlowId,(
stuff((select ','+No 
				from (
				select Distinct No from(
					select 
					T3.No,t3.PROCESSFLOWCHARTFLOWRELATION_Id from dbo.ProcessFlowStatingItem T3
				)PFItem Where PFItem.PROCESSFLOWCHARTFLOWRELATION_Id=T2.Id 
				)T for xml path('')),1,1,'')
			)AllocationStatings,
			(stuff((select ','+MStatingNo 
				from (
				select Distinct MStatingNo from(
					select 
					(CAST(t3.mainTrackNumber as varchar(10))+':'+T3.No) MStatingNo,t3.PROCESSFLOWCHARTFLOWRELATION_Id from dbo.ProcessFlowStatingItem T3
				)PFItem Where PFItem.PROCESSFLOWCHARTFLOWRELATION_Id=T2.Id 
				)T for xml path('')),1,1,'')
			)AllocationMainTrackeStatings,
			(
			 select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from View_HangerFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) 
					AND FlowId in(T2.PROCESSFLOW_Id)
				     And Status=2 And FlowType IN(0,2)
				    AND ProcessChartId=T1.Id  and HangerNo>0
                   ) TodayYield
			) TodayYield
			,(
			 select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from View_HangerFlowChart
				    WHERE FlowId in(T2.PROCESSFLOW_Id)
				     And Status=2 And FlowType IN(0,2) and HangerNo>0
				   ) TodayYield
			) TotalYield
			,(ROW_NUMBER() over(ORDER by T2.FlowNo ASC)) as ID 
FROM ProcessFlowChart T1 
INNER JOIN ProcessFlowChartFlowRelation T2 ON  T1.Id=T2.PROCESSFLOWCHART_Id
--LEFT JOIN  ProcessFlowStatingItem T3 ON T2.Id=T3.PROCESSFLOWCHARTFLOWRELATION_Id
---Left Join Stating T4 ON t3.STATING_Id=t4.Id
Where T1.ID='{0}' AND T2.IsEnabled=1

", flowChartId);
            string[] paramValues = null;
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    queryString = string.Format(queryString, @" AND (ProcessOrderNo like ? OR StyleNo like ? OR FlowNo like ? OR DefectCode like ? OR StatingNo like ?)");
            //    paramValues = new string[] {
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //    string.Format("%{0}%", searchKey) };
            //    // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //}
            //else
            //{
            //    queryString = string.Format(queryString, string.Empty);
            //}

            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<FlowBalanceTableReportModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);

            // rslt1.ToList<FlowBalanceTableReportModel>().ForEach(f => f.RewokRate = (100 * (decimal.Parse(f.ReworkYield.ToString()) / int.Parse(f.Yield.ToString()))).ToString("#0.00") + "%");
            return rslt1;
        }


        /// <summary>
        /// 产量达标详情表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public GroupCompetitionReportModel SearchGroupCompetitionReport(string searchKey)
        {
            var sql = string.Format(@"
SELECT StatDate,SUM(Res.PlanNum)TargetYield,SUM(RealiyCount) DailyYield,SUM(DefectCount) DefectCount   FROM (
 SELECT CONVERT(VARCHAR(10),T_Times.BeginDate,121) StatDate,
        T_Times.PlanNum ,
        ( SELECT    COUNT(ProcessFlowCode) TodayCount
          FROM      SucessProcessOrderHanger
          WHERE     InsertDateTime BETWEEN T_Times.BeginDate
                                   AND     T_Times.EndDate
        ) RealiyCount ,
        ( SELECT    COUNT(BatchNo) DefectCount
          FROM      ( SELECT    BatchNo ,
                                InsertDateTime
                      FROM     View_HangerFlowChart
                      WHERE     FlowType = 1
                                AND Status = 2 AND InsertDateTime BETWEEN T_Times.BeginDate
                                   AND     T_Times.EndDate
																	 and FlowCode is not null
                    ) T_FlowCode
        ) DefectCount
 FROM   ( SELECT *
          FROM      [dbo].[LEDHoursPlanTableItem]
        ) T_Times)Res
		GROUP BY Res.StatDate");
            var outProductDataList = Query<GroupCompetitionReportItemModel>(sql, true, null);
            var result = new GroupCompetitionReportModel();
            result.OutProductDataList = outProductDataList;
            return result;
        }
        /// <summary>
        /// 产量汇总报表(打印)
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        public IList<YieldCollectModel> SearchYieldCollect(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition)
        {
            var queryString = @"
                   SELECT ProcessOrderNo, StyleNo, PColor, Po, PSize
	                    , FlowSection, HangingPieceCount, OutYield,ReturnYield, PS.GroupNo
	                    , CONVERT(varchar(100), ROW_NUMBER() OVER (ORDER BY ProcessOrderNo ASC)) AS Id
                    FROM Products PS
	                    LEFT JOIN (
		                    SELECT ProductsId, SUM(HangingPieceCount) AS HangingPieceCount,GroupNo
		                    FROM (
			                    SELECT ProductsId, COUNT(1) AS HangingPieceCount,GroupNo
			                    FROM SuccessHangerStatingAllocationItem
			                    WHERE Memo = '-1' 
				                    AND CAST(HangerNo AS INT) > 0 {0}
			                    GROUP BY ProductsId,GroupNo
			                    UNION ALL
			                    SELECT ProductsId, COUNT(1) AS HangingPieceCount,GroupNo
			                    FROM HangerStatingAllocationItem
			                    WHERE Memo = '-1' 
				                    AND CAST(HangerNo AS INT) > 0 {0}
			                    GROUP BY ProductsId,GroupNo
		                    ) Res
		                    GROUP BY ProductsId,GroupNo
	                    ) Res_HangingPieceCount
	                    ON PS.Id = Res_HangingPieceCount.ProductsId and ps.GroupNo=Res_HangingPieceCount.GroupNo
	                    LEFT JOIN (
		                    SELECT COUNT(1) AS OutYield, ProductsId,GroupNo
		                    FROM SucessProcessOrderHanger
		                    WHERE CAST(HangerNo AS INT) > 0 and outFlowId is not null {0}
		                    GROUP BY ProductsId,GroupNo
	                    ) Res_Prod_OUT
	                    ON Res_Prod_OUT.ProductsId = PS.Id AND Res_Prod_OUT.GroupNo=PS.GroupNo
	                    LEFT JOIN (
		                    SELECT COUNT(1) AS ReturnYield, ProductsId,GroupNo
		                    FROM (
													select distinct ProductsId,HangerNo,BatchNo,GroupNo from View_HangerFlowChart where FlowType=1 
													{0}
													GROUP BY ProductsId,HangerNo,BatchNo,GroupNo
												)Ress GROUP BY ProductsId,GroupNo
												  
	                    ) Res_Prod_Rework
	                    ON Res_Prod_Rework.ProductsId = PS.Id AND Res_Prod_Rework.GroupNo=PS.GroupNo where 1=1 
                    ";
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
                //queryString += string.Format(@" AND (ProductionNumber like ? OR HangingPieceSiteNo like ? OR ProcessOrderNo like ? OR PColor like ? OR PSize like ? OR LineName like ?)");
                //paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
                // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<YieldCollectModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            rslt1.ToList().ForEach(delegate (YieldCollectModel ycModel)
            {
                if (ycModel.HangingPieceCount != 0)
                {
                    var rWorkCount = ycModel.ReturnYield;
                    if (rWorkCount != null)
                    {
                        ycModel.ReturnRate = ((Convert.ToDecimal(rWorkCount.ToString()) / ycModel.HangingPieceCount) * 100).Value.ToString("0.00") + "%";
                    }
                }
            });
            return rslt1;
        }
        /// <summary>
        /// 工序平衡表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<FlowBalanceTableReportModel> SearchFlowBalanceTableReportModel(IDictionary<string, string> ordercondition, string searchKey, string flowChartId)
        {
            string queryString = string.Format(@"select T2.FlowNo,T2.FlowCode,T2.FlowName,
(select StanardMinute from ProcessFlow where Id=T2.PROCESSFLOW_Id)StanardHours ,
(select StandardPrice from ProcessFlow where Id=T2.PROCESSFLOW_Id)StandardPrice ,
T2.PROCESSFLOW_Id ProcessFlowId,(
stuff((select ','+No 
				from (
				select Distinct No from(
					select 
					T3.No,t3.PROCESSFLOWCHARTFLOWRELATION_Id from dbo.ProcessFlowStatingItem T3
				)PFItem Where PFItem.PROCESSFLOWCHARTFLOWRELATION_Id=T2.Id 
				)T for xml path('')),1,1,'')
			)AllocationStatings,
			(stuff((select ','+MStatingNo 
				from (
				select Distinct MStatingNo from(
					select 
					(CAST(t3.mainTrackNumber as varchar(10))+':'+T3.No) MStatingNo,t3.PROCESSFLOWCHARTFLOWRELATION_Id from dbo.ProcessFlowStatingItem T3
				)PFItem Where PFItem.PROCESSFLOWCHARTFLOWRELATION_Id=T2.Id 
				)T for xml path('')),1,1,'')
			)AllocationMainTrackeStatings,
			(
			 select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from View_HangerFlowChart
				    WHERE (OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				    AND CONVERT(varchar(10), 
				    DATEADD(day, 1, GETDATE()), 120)) 
					AND FlowId in(T2.PROCESSFLOW_Id)
				     And Status=2 And FlowType IN(0,2)
				    AND ProcessChartId=T1.Id  and HangerNo>0
                   ) TodayYield
			) TodayYield
			,(
			 select sum(OutSiteNoCount) FROM(
				    select COUNT(*) OutSiteNoCount from View_HangerFlowChart
				    WHERE FlowId in(T2.PROCESSFLOW_Id)
				     And Status=2 And FlowType IN(0,2) and HangerNo>0
				   ) TodayYield
			) TotalYield
			,(ROW_NUMBER() over(ORDER by T2.FlowNo ASC)) as ID 
FROM ProcessFlowChart T1 
INNER JOIN ProcessFlowChartFlowRelation T2 ON  T1.Id=T2.PROCESSFLOWCHART_Id
--LEFT JOIN  ProcessFlowStatingItem T3 ON T2.Id=T3.PROCESSFLOWCHARTFLOWRELATION_Id
---Left Join Stating T4 ON t3.STATING_Id=t4.Id
Where T1.ID='{0}' AND T2.IsEnabled=1

", flowChartId);
            string[] paramValues = null;
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<FlowBalanceTableReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);

            // rslt1.ToList<FlowBalanceTableReportModel>().ForEach(f => f.RewokRate = (100 * (decimal.Parse(f.ReworkYield.ToString()) / int.Parse(f.Yield.ToString()))).ToString("#0.00") + "%");
            return rslt1;
        }

        /// <summary>
        /// 返工详情报表
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ReworkDetailReportModel> SearchReworkDetailReport(IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"
SELECT ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode, Num,DefectCode,DefectName,StatingNo,GroupNo,ReworkGroupNo,CheckReworkCode,CheckReworkNo,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by Num ASC)) as ID FROM(
                              
select ReworkIndex,HangerNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,COUNT(*) as Num,DefectCode,DefectName,StatingNo,GroupNo,ReworkGroupNo,CheckReworkCode,CheckReworkNo from (

 select T1.HangerNo,CONVERT(VARCHAR(19),T1.ReworkDate,121)ReworkDate,T1.ReworkEmployeeName,T1.ReworkStatingNo,T2.ProcessOrderNo,T2.StyleNo,T2.PColor,T2.PO,T2.PSize,T2.FlowSection,T1.FlowNo,T1.FlowCode,
                                1 as Num,T1.DefectCode,(SELECT top 1 dc.DefectName From DefectCodeTable dc where dc.DefectCode=t1.DefectCode and dc.deleted=0) DefectName
                                ,T1.StatingNo,T1.GroupNo,ROW_NUMBER() over(PARTITION by HangerNo order by T1.CompareDate desc) ReworkIndex,T1.ReworkGroupNo,T1.CheckReworkCode,T1.CheckReworkNo
                                 from View_HangerFlowChart T1
                                INNER JOIN Products T2 ON T1.ProductsId=T2.Id
                                WHERE T1.FlowType=1 and T1.Status=2 and (CAST (T1.HangerNo as int))>0 {0}
)Res GROUP BY ProcessOrderNo,StyleNo,PColor,PO,PSize,FlowSection,FlowNo,FlowCode,DefectCode,DefectName,StatingNo,GroupNo,ReworkDate,ReworkEmployeeName,ReworkStatingNo,HangerNo,ReworkIndex,ReworkGroupNo,CheckReworkCode,CheckReworkNo
                                )ZZ WHERE 1=1
";
            string[] paramValues = null;
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    queryString = string.Format(queryString, @" AND (ProcessOrderNo like ? OR StyleNo like ? OR FlowNo like ? OR DefectCode like ? OR StatingNo like ?)");
            //    paramValues = new string[] {
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //    string.Format("%{0}%", searchKey) };
            //    // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //}
            //else
            //{
            //    queryString = string.Format(queryString, string.Empty);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<ReworkDetailReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            return rslt1;
        }
        /// <summary>
        /// 返工汇总
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<ReworkCollAndDefectAnalysisReportModel> SearchReworkCollAndDefectAnalysisReport(IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"
                							select Res_OutYield.*,Res_ReworkYield.ReworkYield,(ROW_NUMBER() over(ORDER by (Res_OutYield.ProcessOrderNo+Res_OutYield.FlowNo+Res_OutYield.productsId) ASC)) as ID  from(
									select productsId,Count(*) Yield,GroupNO,CONVERT(varchar(100), InsertDateTime, 23) InspectionDate,
								ProcessOrderNo,FlowNo,FlowName	from View_HangerFlowChart
										where status=2
									GROUP BY productsId,GroupNO,CONVERT(varchar(100), InsertDateTime, 23),ProcessOrderNo,GroupNo,FlowNo,FlowName
								)Res_OutYield 
								inner join(
									select productsId,Count(*) ReworkYield,GroupNO,CONVERT(varchar(100), InsertDateTime, 23) InspectionDate,
								ProcessOrderNo,FlowNo,FlowName from View_HangerFlowChart
										where status=2 and FlowType=1
									GROUP BY productsId,GroupNO,CONVERT(varchar(100), InsertDateTime, 23),ProcessOrderNo,GroupNo,FlowNo,FlowName
								)Res_ReworkYield on (
								Res_OutYield.productsId=Res_ReworkYield.productsId 
								and Res_OutYield.GroupNO=Res_ReworkYield.GroupNO 
								and Res_OutYield.InspectionDate=Res_ReworkYield.InspectionDate 
								and Res_OutYield.ProcessOrderNo=Res_ReworkYield.ProcessOrderNo 
								and Res_OutYield.FlowNo=Res_ReworkYield.FlowNo 
								and Res_OutYield.FlowName=Res_ReworkYield.FlowName 
								) where 1=1 
								";
            string[] paramValues = null;
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    queryString = string.Format(queryString, @" AND (ProcessOrderNo like ? OR StyleNo like ? OR FlowNo like ? OR DefectCode like ? OR StatingNo like ?)");
            //    paramValues = new string[] {
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //        string.Format("%{0}%", searchKey),
            //    string.Format("%{0}%", searchKey) };
            //    // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            //}
            //else
            //{
            //    queryString = string.Format(queryString, string.Empty);
            //}
            queryString = string.Format(queryString, string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey);
            var rslt1 = Query<ReworkCollAndDefectAnalysisReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            rslt1.ToList<ReworkCollAndDefectAnalysisReportModel>().ForEach(f => f.RewokRate = (100 * (decimal.Parse(f.ReworkYield.ToString()) / int.Parse(f.Yield.ToString()))).ToString("#0.00") + "%");
            return rslt1;
        }
        /// <summary>
        /// 工时分析报表(打印)
        /// </summary>
        /// <param name="ordercondition"></param>
        /// <param name="searchDic"></param>
        /// <param name="inCondition"></param>
        /// <returns></returns>
        public IList<WorkingHoursAnalysisReportModel> SearchWorkingHoursAnalysisReport(IDictionary<string, string> ordercondition, IDictionary<string, string> searchDic, string inCondition, string buidInCondition)
        {
            string queryString = @"
SELECT * FROM(
select *,CONVERT(varchar(100),ROW_NUMBER() over(ORDER by ProcessOrderNo ASC)) as ID  from(
select T1.ProcessOrderNo,T1.FlowIndex,T1.ProcessFlowCode,T1.ProcessFlowName,T1.FlowNo,T1.EmployeeName,T3.StanardSecond StanardHours,Pts.styleNo StyleCode
,AVG(datediff(second, T1.CompareDate,T1.OutSiteDate)) AvgTimes
from View_HangerFlowChart T1
INNER JOIN ProcessFlow T3 ON T3.Id=T1.ProcessFlowId
Left Join Products Pts On pts.Id=T1.ProductsId
WHERE 1=1 {0}
GROUP BY T1.ProcessOrderNo,T1.FlowIndex,T1.ProcessFlowCode,T1.ProcessFlowName,T1.FlowNo,T1.EmployeeName,T3.StanardSecond,Pts.styleNo

)Res)ZRES WHERE 1=1
 ";
            if (!string.IsNullOrEmpty(buidInCondition))
            {
                queryString = string.Format(queryString, buidInCondition);
            }
            string[] paramValues = null;
            if (null != searchDic)
            {
                foreach (var k in searchDic.Keys)
                {
                    queryString += string.Format(" AND {0} ?", k);
                }
                paramValues = searchDic.Values.ToArray();
            }
            queryString = string.Format(queryString, string.IsNullOrEmpty(inCondition) ? string.Empty : inCondition);
            var rslt1 = Query<WorkingHoursAnalysisReportModel>(new System.Text.StringBuilder(queryString), ordercondition, true, paramValues);
            return rslt1;
        }
    }
}

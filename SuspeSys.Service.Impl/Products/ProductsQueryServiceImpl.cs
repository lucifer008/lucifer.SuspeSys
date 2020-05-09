using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Products
{
    public class ProductsQueryServiceImpl : ServiceBase, IProductsQueryService
    {

        public static ProductsQueryServiceImpl Instance
        {
            get { return new ProductsQueryServiceImpl(); }
        }
        public EmployeeLoginInfo GetStatingInfo(int mainTrackNumber, int statingNo)
        {
            var sql = string.Format(@"select top 1 T4.LoginStatingNo,T3.Id EmployeeId,T3.RealName,T1.CardNo,T4.MainTrackNumber,T3.Code  
from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  AND T1.CardType=4 AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
And T4.MainTrackNumber=@MainTrackNumber
Order by LoginDate desc
");
            EmployeeLoginInfo statingLogInfo = DapperHelp.QueryForObject<EmployeeLoginInfo>(sql, new { MainTrackNumber = mainTrackNumber, LoginStatingNo = statingNo });
            return statingLogInfo;
        }
        /// <summary>
        /// 获取上线制单列表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessOrderModel> GetProcessOrderList(IDictionary<string, string> ht)
        {
            var sql = new StringBuilder("select Id,POrderNo,StyleCode,StyleName from ProcessOrder");
            return Query<DaoModel.ProcessOrderModel>(sql, ht, true, null);
        }
        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string pOrderId)
        {
            var sql = new StringBuilder(@"select 
                T3.*
                from ProcessOrder T1 
                INNER JOIN ProcessFlowVersion T2 ON T1.Id=T2.PROCESSORDER_Id
                INNER JOIN ProcessFlowChart T3 ON T2.Id=T3.PROCESSFLOWVERSION_Id where T1.Id=?");
            return Query<DaoModel.ProcessFlowChart>(sql, null, true, pOrderId);
        }
        public IList<DaoModel.ProcessOrderExtModel> GetProcessOrderItemList(string pOrderId)
        {
            var sql = new StringBuilder(@"SELECT t1.*,ISNULL(T2.AllocationedTotal,0)AllocationedTotal,ISNULL((T1.Total-T2.AllocationedTotal),0) NonAllocationTotal  from(
				SELECT T1.Id,T1.POrderNo,T1.StyleCode,T4.PurchaseOrderNo,T2.Color,T3.SizeDesption,SUM(cast(ISNULL(T3.Total,0) as int))Total,1 As Unit
				FROM ProcessOrder T1
				INNER JOIN ProcessOrderColorItem T2 ON T1.Id=T2.PROCESSORDER_Id
				INNER JOIN ProcessOrderColorSizeItem T3 ON T2.Id=T3.PROCESSORDERCOLORITEM_Id
				INNER JOIN CustomerPurchaseOrder T4 ON T4.Id=T2.CUSTOMERPURCHASEORDER_Id
				WHERE T1.Id=?
				GROUP BY  T1.Id,T1.POrderNo,T1.StyleCode,T4.PurchaseOrderNo,T2.Color,T3.SizeDesption
			) t1 
			left join(
			SELECT SUM(TaskNum*Unit) AllocationedTotal,PROCESSORDER_Id,PColor,PSize FROM Products GROUP BY PROCESSORDER_Id,PColor,PSize
			) t2 on t1.Id=t2.PROCESSORDER_Id and t2.PColor=t1.Color and t2.PSize=t1.SizeDesption
			WHERE t1.Total>0");
            return Query<DaoModel.ProcessOrderExtModel>(sql, null, true, pOrderId);
        }
        public IList<DaoModel.ProcessFlowChartFlowModel> GetProcessFlowChartFlowModelList(string pfChartId)
        {
            var pfcFlowSql = new StringBuilder(@"select * from ProcessFlowChartFlowRelation where PROCESSFLOWCHART_Id=?");
            var pfChartFlowList = Query<DaoModel.ProcessFlowChartFlowModel>(pfcFlowSql, null, true, pfChartId);
            foreach (var pf in pfChartFlowList)
            {
                var pstatingList = GetProcessFlowStatingItemList(pf.Id);
                pf.Statings = string.Join(",", pstatingList.Select(s => s.No?.Trim()));
                pf.StatingTotal = pstatingList.Count;
            }
            return pfChartFlowList;
        }
        //获取挂片站
        public IList<DaoModel.StatingModel> GetHangerPieceStatingList(string groupNo = null, string statingNo = null, string mainTrackNo = null)
        {
            var sql = new StringBuilder(@"
            select distinct  T1.Id,T1.StatingNo,T2.RoleName,T3.GroupNo
            from Stating T1
            INNER JOIN StatingRoles T2 ON T1.STATINGROLES_Id=T2.Id
            INNER JOIN SiteGroup T3 ON T3.Id=T1.SITEGROUP_Id
            where T2.RoleName='挂片站' AND T1.Deleted=0 ");
            var listValues = new List<object>();
            if (!string.IsNullOrEmpty(groupNo))
            {
                sql.AppendFormat(" AND T3.GroupNO=?");
                listValues.Add(groupNo);
            }
            if (!string.IsNullOrEmpty(statingNo))
            {
                sql.AppendFormat(" AND T1.StatingNo=?");
                listValues.Add(statingNo);
            }
            if (!string.IsNullOrEmpty(mainTrackNo))
            {
                sql.AppendFormat(" AND T3.MainTrackNumber=?");
                listValues.Add(mainTrackNo);
            }
            var statingList = Query<DaoModel.StatingModel>(sql, null, true, listValues.ToArray());
            return statingList;
        }

        /// <summary>
        /// 检查是否是挂片站
        /// </summary>
        /// <param name="groupNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="mainTrackNo"></param>
        /// <returns></returns>
        public bool isHangingPiece(string groupNo = null, string statingNo = null, string mainTrackNo = null)
        {
            // timersLog.Info("isHangingPiece开始:");

            var sql = new StringBuilder(@"
            select COUNT(1) tCount
            from Stating T1
            INNER JOIN StatingRoles T2 ON T1.STATINGROLES_Id=T2.Id
            INNER JOIN SiteGroup T3 ON T3.Id=T1.SITEGROUP_Id
            where T2.RoleName='挂片站'");
            //var listValues = new List<object>();
            //if (!string.IsNullOrEmpty(groupNo))
            //{
            //    sql.AppendFormat(" AND T3.GroupNO=?");
            //    listValues.Add(groupNo);
            //}
            //if (!string.IsNullOrEmpty(statingNo))
            //{
            //    sql.AppendFormat(" AND T1.StatingNo=?");
            //    listValues.Add(statingNo);
            //}
            //if (!string.IsNullOrEmpty(mainTrackNo))
            //{
            //    sql.AppendFormat(" AND T3.MainTrackNumber=?");
            //    listValues.Add(mainTrackNo);
            //}
            //var tCount = QueryForObject<int>(sql, null, true, listValues.ToArray());
            var listValues = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(groupNo))
            {
                sql.AppendFormat(" AND T3.GroupNO=@GroupNO");
                listValues.Add("GroupNO", groupNo);
            }
            if (!string.IsNullOrEmpty(statingNo))
            {
                sql.AppendFormat(" AND T1.StatingNo=@StatingNo");
                listValues.Add("StatingNo", statingNo);
            }
            if (!string.IsNullOrEmpty(mainTrackNo))
            {
                sql.AppendFormat(" AND T3.MainTrackNumber=@MainTrackNumber");
                listValues.Add("MainTrackNumber", mainTrackNo);
            }

            var tCount = QueryForObjectNative<Int32>(sql.ToString(), listValues); //QueryForObject<int>(sql, null, true, listValues.ToArray());

            return tCount > 0;
        }
        private IList<DaoModel.ProcessFlowStatingItem> GetProcessFlowStatingItemList(string pfcFlowId)
        {
            var sql = new StringBuilder("select * from ProcessFlowStatingItem where PROCESSFLOWCHARTFLOWRELATION_Id=?");
            var processFlowStatingItemList = Query<DaoModel.ProcessFlowStatingItem>(sql, null, true, pfcFlowId);
            return processFlowStatingItemList;
        }
        /// <summary>
        /// 在制品信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        public IList<DaoModel.ProductsModel> SearchProductsList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, string groupNo)
        {
            //:0:未分配;1:已分配;2.上线;3.已完成
            string queryString = @"SELECT  Id, PROCESSFLOWCHART_Id, PROCESSORDER_Id, ProductionNumber, ImplementDate, HangingPieceSiteNo, 
                   ProcessOrderNo, Status, CustomerPurchaseOrderId, OrderNo, StyleNo, PColor, PO, PSize, LineName, FlowSection, Unit, 
                   TaskNum,T.GroupNo,
                       OnlineNum=(SELECT SUM(Expr1) FROM(
					   SELECT  COUNT(1) AS Expr1
                        FROM       dbo.WaitProcessOrderHanger
                        WHERE    (ProductsId = T.Id and GroupNo=T.GroupNo)
						UNION all
						SELECT COUNT(1) AS Expr1 FROM dbo.SucessProcessOrderHanger WHERE  (ProductsId = T.Id) AND CAST(HangerNo AS INT)<0 AND MEMO='半成品衣架' and  GroupNo=T.GroupNo
					   )TT)
                    , TodayHangingPieceSiteNum
                    , StatusText=(CASE ISNULL([Status], 0) 
                   WHEN 0 THEN '未分配' WHEN 1 THEN '已分配' WHEN 2 THEN '上线中' WHEN 3 THEN '已完成' END)
                       ,TodayProdOutNum=(SELECT  SUM(ISNULL(SizeNum, 0))
                        FROM       dbo.SucessProcessOrderHanger
                        WHERE    ( CAST(HangerNo AS INT)>0 AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120)) AND (ProductsId = T.Id  and GroupNo=T.GroupNo)  and outFlowId is not null)
                    ,TodayBindCard, 
                  TodayRework=(select Count(*) TodayReworkNum from(
                                select distinct ProductsId,HangerNo,BatchNo from View_HangerFlowChart where FlowType=1 and GroupNo=T.GroupNo and ProductsId=T.Id
                                 And OutSiteDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
                             )Res_Today_Rework_Num)
                       ,TotalHangingPieceSiteNum=(SELECT  ISNULL(SUM(HCount), 0) AS HCount
                        FROM       (SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM       dbo.HangerStatingAllocationItem
                                            WHERE    (Memo = '-1') AND (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND 
                                                               CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120))
																															 and GroupNo=T.GroupNo
                                            GROUP BY ProductsId
                                            UNION ALL
                                            SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM      dbo.SuccessHangerStatingAllocationItem
                                            WHERE   (Memo = '-1') AND (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND 
                                                               CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND CAST(HangerNo AS INT)>0
																															 and GroupNo=T.GroupNo
                                            GROUP BY ProductsId) AS Z
                        WHERE    (ProductsId = T.Id  and GroupNo=T.GroupNo)) 
                        ,TotalRework=(
                            select Count(*) TotalReworkNum from(
                                select distinct ProductsId,HangerNo,BatchNo from View_HangerFlowChart where FlowType=1 and GroupNo=T.GroupNo and ProductsId=T.Id
                             )Res_Total_Rework_Num
                        )
                        , TotalBindNum
                       ,TotalProdOutNum=(SELECT  SUM(ISNULL(SizeNum, 0)) AS TodayProdOutNum
                        FROM       dbo.SucessProcessOrderHanger AS SucessProcessOrderHanger_1
                        WHERE    (ProductsId = T.Id and GroupNo=T.GroupNo) AND CAST(HangerNo AS INT)>0  and outFlowId is not null)
,TotalHangingNum=(
SELECT  ISNULL(SUM(HCount), 0) AS HCount
                        FROM       (SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM       dbo.HangerStatingAllocationItem
                                            WHERE    (Memo = '-1')  and GroupNo=T.GroupNo
                                            GROUP BY ProductsId
                                            UNION ALL
                                            SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM      dbo.SuccessHangerStatingAllocationItem
                                            WHERE   (Memo = '-1')  AND CAST(HangerNo AS INT)>0  and GroupNo=T.GroupNo
                                            GROUP BY ProductsId) AS Z
                        WHERE    (ProductsId = T.Id)
) 
FROM      dbo.Products AS T
WHERE   (1 = 1)";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (ProductionNumber like ? OR HangingPieceSiteNo like ? OR ProcessOrderNo like ? OR PColor like ? OR PSize like ? OR LineName like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
                // queryString += string.Format(" AND Status= "+ ProductsStatusType.Allocationed.Value);
            }

            if (!string.IsNullOrEmpty(groupNo))
            {
                queryString += " AND GroupNo = ? ";
                if (paramValues == null)
                    paramValues = new string[] { groupNo };
                else
                    paramValues = paramValues.ToList().Concat<string>((new string[] { groupNo }).ToList()).ToArray();
            }
            var rslt1 = Query<DaoModel.ProductsModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }
        public long GetMaxProductionNumber()
        {
            string queryString = string.Format("select ISNULL(MAX(ProductionNumber),0) currentMaxNum from [dbo].[Products]");
            using (var session = SessionFactory.OpenSession())
            {
                var reslt = session.CreateSQLQuery(queryString).UniqueResult();
                return Convert.ToInt64(reslt);
            }
        }
        public long GetCurrentMaxProductionNumber()
        {
            var maxNum = GetMaxProductionNumber();
            return (maxNum + 1);
            //var isExt=
        }
        public IList<EmployeeLoginInfo> GetEmployeeLoginInfoList(int mainTrackNo, string statingNo, string cardNo)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select T4.Id,T4.CARDINFO_Id,T4.LoginStatingNo,T3.Id EmployeeId,T3.RealName,T1.CardNo 
from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1 AND T1.CardType=4 AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=?  and T1.CardNo=? AND MainTrackNumber=?
");
            var paramValues = new string[] { statingNo, cardNo, mainTrackNo.ToString() };
            var list = Query<EmployeeLoginInfo>(sql, null, true, paramValues);
            return list;
        }
        public IList<EmployeeLoginInfo> GetEmployeeLoginInfoList(string statingNo,int MainTrackNumber)
        {
            //            var sql = new StringBuilder();
            //            //            sql.AppendFormat(@"select T4.Id,T4.CARDINFO_Id,T4.LoginStatingNo,T3.Id EmployeeId,T3.RealName,T1.CardNo 
            //            //from CardInfo T1 
            //            //INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
            //            //INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
            //            //INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
            //            //WHERE  T4.IsOnline=1  AND T1.CardType=4 AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=?
            //            //");

            //            sql.AppendFormat(@"select T4.Id,T4.CARDINFO_Id,T4.LoginStatingNo,T3.Id EmployeeId,T3.RealName,T1.CardNo 
            //from CardInfo T1 
            //INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
            //INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
            //INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
            //WHERE  T4.IsOnline=1  AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=?
            //");
            //            var paramValues = new string[] { statingNo };
            //            var list = Query<EmployeeLoginInfo>(sql, null, true, paramValues);
            //            return list;
            var sql = string.Format(@"select T4.Id,T4.CARDINFO_Id,T4.LoginStatingNo,T3.Id EmployeeId,T3.RealName,T1.CardNo,T3.Code 
from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
and t4.MainTrackNumber=@MainTrackNumber");
            var list = DapperHelp.QueryForList<EmployeeLoginInfo>(sql, new { LoginStatingNo =statingNo, MainTrackNumber = MainTrackNumber });
            return list;
        }
        public bool CheckStatingIsLogin(string statingNo, int MainTrackNumber)
        {
            var sql = string.Format(@"select ISNULL(COUNT(1),0) tCount from CardInfo T1 
INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.CARDINFO_Id
INNER JOIN Employee T3 ON T3.Id=T2.EMPLOYEE_Id
INNER JOIN CardLoginInfo T4 ON T4.CARDINFO_Id=T1.Id
WHERE  T4.IsOnline=1  AND (T4.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND T4.LoginStatingNo=@LoginStatingNo
and t4.MainTrackNumber=@MainTrackNumber");
            var paramValues = new string[] { statingNo };
            var c = DapperHelp.QueryForObject<int>(sql, new { LoginStatingNo = statingNo, MainTrackNumber = MainTrackNumber });
            return c != 0;
        }
        public Int32 GetNextIndex(string hangerNo, string tableName)
        {
            var sql = new StringBuilder(string.Format("select ISNULL(COUNT(1),0) tCount from {0} where HangerNo=?", tableName));
            var tCount = QueryForObject<Int32>(sql, null, true, hangerNo);
            return (++tCount);
        }
    }
}

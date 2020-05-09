using Newtonsoft.Json;
using SusNet.Common.Utils;
using SuspeSys.Dao;
using SuspeSys.Domain.Cus;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Products
{
    public class CANProductsQueryService : ServiceBase
    {
        private CANProductsQueryService() { }
        private static readonly object lockObject = new object();
        private readonly static CANProductsQueryService _Instance = new CANProductsQueryService();
        public static CANProductsQueryService Instance
        {
            get
            {
                return _Instance;
            }
        }
        //public string GetProducts(int mainTrackNo, int statingNo, int productNumber)
        //{
        //    //933304-9BUY,010,28,任务1863件,单位1件,累计出1117件,今日出213件
        //    var sql = new StringBuilder("select top 1 ProcessOrderNo,PColor,PSize,Unit,TaskNum,ISNULL(TodayProdOutNum,0)TodayProdOutNum,ISNULL(OnlineNum,0)OnlineNum from Products where Status=? AND ProductionNumber=? AND HangingPieceSiteNo=? ");
        //    var list = Query<DaoModel.Products>(sql, null, true, ProductsStatusType.Onlineed.Value, productNumber, statingNo);
        //    var sbData = new StringBuilder();
        //    foreach (DaoModel.Products p in list)
        //    {
        //        sbData.AppendFormat("{0},{1},{2},任务{3}件,单位{4},累计出{5}件,今日出{6}件", p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim(), p.TaskNum, p.Unit?.Trim(), p.TodayProdOutNum, p.OnlineNum);
        //    }
        //    // var hexBytes = UnicodeUtils.GetBytesByUnicode(sbData.ToString());
        //    return sbData.ToString();
        //}
        public string GetProductsExt(int mainTrackNo, int statingNo)
        {
            //933304-9BUY,010,28,任务1863件,单位1件,累计出1117件,今日出213件
            var sql = new StringBuilder("select top 1 ProcessOrderNo,PColor,PSize,Unit,TaskNum,ISNULL(TodayProdOutNum,0)TodayProdOutNum,ISNULL(OnlineNum,0)OnlineNum from Products where Status=? AND HangingPieceSiteNo=? ");
            var list = Query<DaoModel.Products>(sql, null, true, ProductsStatusType.Onlineed.Value, statingNo);
            var sbData = new StringBuilder();
            foreach (DaoModel.Products p in list)
            {
                sbData.AppendFormat("{0},{1},{2},任务{3}件,单位{4},累计出{5}件,今日出{6}件", p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim(), p.TaskNum, p.Unit?.Trim(), p.TodayProdOutNum, p.OnlineNum);
            }
            // var hexBytes = UnicodeUtils.GetBytesByUnicode(sbData.ToString());
            return sbData.ToString();
        }
        /// <summary>
        /// 【协议2.0】 获取 【产量数据推送】 内容
        /// 挂片站终端显示内容【不够2字节用00代替】
        ///任务数（2字节）
        ///累计数（2字节）
        ///本日数（2字节）
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public DaoModel.Products GetProductsNew(int mainTrackNo, int statingNo, int productNumber)
        {
            //任务1863件,累计出1117件,今日出213件
            var sql = new StringBuilder(@"select top 1 T.TaskNum,(SELECT  SUM(ISNULL(SizeNum, 0)) AS TodayProdOutNum
                        FROM       dbo.SucessProcessOrderHanger
                        WHERE    (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120)) AND (ProductsId = T.Id)) AS TodayProdOutNum,ISNULL(T.TotalProdOutNum,0)TotalProdOutNum from Products T where  T.ProductionNumber=? AND T.HangingPieceSiteNo=? ");
            var products = QueryForObject<DaoModel.Products>(sql, null, true, productNumber, statingNo);
            //var sbData = new StringBuilder();
            //foreach (DaoModel.Products p in list)
            //{
            //    sbData.AppendFormat("{0},{1},{2}",  p.TaskNum, p.TotalProdOutNum, p.TodayProdOutNum);
            //}
            //// var hexBytes = UnicodeUtils.GetBytesByUnicode(sbData.ToString());
            //return sbData.ToString();
            return products;
        }
        public ProductsFlowModel GetHangingPieceStatingData(int mainTrackNo, int statingNo, int productNumber)
        {
            var sqlGroup = string.Format(@"
	select distinct Sg.GroupNO from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where  St.MainTrackNumber=@MainTrackNumber
"); //new StringBuilder("select GroupNO from SiteGroup where MainTrackNumber=@MainTrackNumber");
            var siteGroup = DapperHelp.QueryForObject<string>(sqlGroup.ToString(), new { MainTrackNumber = mainTrackNo });// QueryForObject<DaoModel.SiteGroup>(sqlGroup, null, true, mainTrackNo);
            if (null == siteGroup)
            {
                var ex = new ApplicationException(string.Format("主轨号:{0}找不到站点组！ ", mainTrackNo));
                tcpLogError.Error(ex);
                throw ex;
            }

            //今日挂片
            var sqlToday = new StringBuilder(@"
            SELECT SUM(TodayHangingPieceCount) TodayHangingPieceCount FROM(
            select COUNT(1) TodayHangingPieceCount from HangerStatingAllocationItem Where SiteNo=@SiteNo AND MainTrackNumber=@MainTrackNumber 
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                                       DATEADD(day, 1, GETDATE()), 120) 
            AND Memo='-1' AND  ProductsId=(select Id from Products where productionNumber=@ProductionNumber and groupNo=@groupNo)
            UNION ALL   
            select COUNT(1) TodayHangingPieceCount from SuccessHangerStatingAllocationItem Where SiteNo=@SiteNo AND MainTrackNumber=@MainTrackNumber 
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
            AND Memo='-1' AND CAST(HangerNo AS INT)>0 AND  ProductsId=(select Id from Products where productionNumber=@ProductionNumber  and groupNo=@groupNo)
            )T_TodayHangingPieceCount ");

            //var dicCondition = new Dictionary<string, string>();
            //dicCondition.Add("SiteNo", statingNo.ToString());
            //dicCondition.Add("MainTrackNumber", mainTrackNo.ToString());
            var todayNum = DapperHelp.QueryForObject<int>(sqlToday.ToString(), new { SiteNo = statingNo, MainTrackNumber = mainTrackNo, ProductionNumber = productNumber, groupNo = siteGroup?.Trim() }); //QueryForObject<int>(sqlToday, null, true, dicCondition);

            //            var sqlTotal = new StringBuilder(@"SELECT SUM(TotalHangingPieceCount) TotalHangingPieceCount FROM(
            //select COUNT(1) TotalHangingPieceCount from HangerStatingAllocationItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
            //AND Memo='-1' 
            //UNION ALL   
            //select COUNT(1) TotalHangingPieceCount from SuccessHangerStatingAllocationItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber 
            //AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
            //AND Memo='-1' 
            //)T_TotalHangingPieceCount ");


            var sqlTotal = new StringBuilder(@" 
                      SELECT SUM(TotalHangingPieceCount) TotalHangingPieceCount FROM(
select COUNT(1) TotalHangingPieceCount from HangerStatingAllocationItem Where SiteNo=@SiteNo AND MainTrackNumber=@MainTrackNumber
AND Memo='-1'  AND ProductsId=(SELECT top 1 id FROM Products WHERE ProductionNumber=@ProductionNumber  and groupNo=@groupNo)
UNION ALL
SELECT
COUNT(1) AS HCount FROM      dbo.SuccessHangerStatingAllocationItem
                                            WHERE   (Memo = '-1')  AND CAST(HangerNo AS INT)>0
                                            AND SiteNo=@SiteNo AND MainTrackNumber=@MainTrackNumber
                                            AND ProductsId=(SELECT top 1 id FROM Products WHERE ProductionNumber=@ProductionNumber  and groupNo=@groupNo)
)T_TotalHangingPieceCount
 ");

            //dicCondition.Add("ProductionNumber", productNumber.ToString());

            var totalNum = DapperHelp.QueryForObject<int>(sqlTotal.ToString(), new { SiteNo = statingNo, MainTrackNumber = mainTrackNo, ProductionNumber = productNumber, groupNo = siteGroup?.Trim() }); //QueryForObject<int>(sqlTotal, null, true, dicCondition);
            //dicCondition = new Dictionary<string, string>();
            //dicCondition.Add("HangingPieceSiteNo", statingNo.ToString());
            //dicCondition.Add("ProductionNumber", productNumber.ToString());
            //dicCondition.Add("GroupNO", siteGroup.GroupNo?.Trim());
            var sqlTask = new StringBuilder("select isNull(T.TaskNum,0) TaskNum from Products T where T.GroupNO=@GroupNO AND T.ProductionNumber=@ProductionNumber AND T.HangingPieceSiteNo=@SiteNo");
            var taskNum = DapperHelp.QueryForObject<int>(sqlTask.ToString(), new { SiteNo = statingNo, MainTrackNumber = mainTrackNo, ProductionNumber = productNumber, GroupNO = siteGroup?.Trim() });// QueryForObject<int>(sqlTask, null, true, dicCondition);
            return new ProductsFlowModel() { TaskNum = taskNum, TodayHangingPieceCount = todayNum, TotalHangingPieceCount = totalNum };
        }
        /// <summary>
        /// 【协议2.0】 获取 【产量数据推送】 内容
        /// 挂片站终端显示内容【不够2字节用00代替】
        ///任务数（2字节）
        ///累计数（2字节）
        ///本日数（2字节）
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public List<byte> GetProductByBytes(int mainTrackNo, int statingNo, int productNumber, ref string data)
        {
            var result = new List<byte>();
            var p = GetHangingPieceStatingData(mainTrackNo, statingNo, productNumber); //GetProductsNew(mainTrackNo, statingNo, productNumber);
            // var hexChs = UnicodeUtils.GetHexFromChs(data);
            var taskNumBytes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(p.TaskNum));
            var todayProdOutBytes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(p.TodayHangingPieceCount));
            var totalProdOutBytes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(p.TotalHangingPieceCount));

            data = string.Format("任务数:{0},累计数:{1},本日数:{2}", p.TaskNum, p.TotalHangingPieceCount, p.TodayHangingPieceCount);
            result.AddRange(taskNumBytes);
            result.AddRange(totalProdOutBytes);
            result.AddRange(todayProdOutBytes);

            //if (taskNumBytes.Length > 2)
            //{
            //    var ex = new ApplicationException(string.Format("任务数超出2字节! 主轨:{0} 站点:{1} 任务数(十进制):{2}", mainTrackNo, statingNo, p.TaskNum));
            //    tcpLogError.Error("【产量数据推送】", ex);
            //}
            //if (todayProdOutBytes.Length > 2)
            //{
            //    var ex = new ApplicationException(string.Format("今日产出超出2字节! 主轨:{0} 站点:{1} 今日产出(十进制):{2}", mainTrackNo, statingNo, p.TodayProdOutNum));
            //    tcpLogError.Error("【产量数据推送】", ex);
            //}
            //if (totalProdOutBytes.Length > 2)
            //{
            //    var ex = new ApplicationException(string.Format("累计产出超出2字节! 主轨:{0} 站点:{1} 累计产出(十进制):{2}", mainTrackNo, statingNo, p.TodayProdOutNum));
            //    tcpLogError.Error("【产量数据推送】", ex);
            //}
            ////任务数
            //if (taskNumBytes.Length == 0)
            //{
            //    result.AddRange(HexHelper.StringToHexByte("00 00"));
            //}
            //else if (taskNumBytes.Length == 1)
            //{
            //    result.AddRange(taskNumBytes);
            //    result.AddRange(HexHelper.StringToHexByte("00"));
            //}
            //else
            //{
            //    result.AddRange(taskNumBytes);
            //}
            ////累计完成
            //if (totalProdOutBytes.Length == 0)
            //{
            //    result.AddRange(HexHelper.StringToHexByte("00 00"));
            //}
            //else if (totalProdOutBytes.Length == 1)
            //{
            //    result.AddRange(totalProdOutBytes);
            //    result.AddRange(HexHelper.StringToHexByte("00"));
            //}
            //else
            //{
            //    result.AddRange(totalProdOutBytes);
            //}
            ////今日产出
            //if (todayProdOutBytes.Length == 0)
            //{
            //    result.AddRange(HexHelper.StringToHexByte("00 00"));
            //}
            //else if (taskNumBytes.Length == 1)
            //{
            //    result.AddRange(todayProdOutBytes);
            //    result.AddRange(HexHelper.StringToHexByte("00"));
            //}
            //else
            //{
            //    result.AddRange(todayProdOutBytes);
            //}

            return result;
        }

        public int GetCurrentOnlineProductNumber(int xID, int statingNo)
        {
            lock (lockObject)
            {
                var sqlStating = new StringBuilder(@"  select distinct top 1  GroupNO from SiteGroup sg
  inner join Stating st on st.SITEGROUP_Id=sg.Id
  where st.Deleted=0 and st.MainTrackNumber=?
");
                //var sqlGroup = new StringBuilder("select GroupNO from SiteGroup where MainTrackNumber=?");
                var siteGroup = QueryForObject<DaoModel.SiteGroup>(sqlStating, null, true, xID);
                if (null == siteGroup)
                {
                    var ex = new NoFoundOnlineProductsException(string.Format("主轨号:{0}找不到站点组", xID));
                    tcpLogError.Error(ex);
                    throw ex;
                }
                var sql = new StringBuilder("select ProductionNumber from Products where Status=? and GroupNo=?");
                var productOnLine = QueryForObject<int>(sql, null, true, ProductsStatusType.Onlineed.Value, siteGroup.GroupNo?.Trim());
                if (0 == productOnLine)
                {
                    var ex = new NoFoundOnlineProductsException(string.Format("主轨号:{0} 站点:{1} 找不到上线的产品！", xID, statingNo));
                    tcpLogError.Error(ex);
                    throw ex;
                }
                return productOnLine;
            }
        }
        /// <summary>
        /// 返工出站推送数据
        ///今日数（2字节）+
        ///效率（2字节）
        ///本次工序时间（2字节）
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public List<byte> GetReworkHangerOutSiteFlowInfo(int xID, int iD, int hangerNo, ref string info)
        {
            var data = new List<Byte>();
            //本工序效率及用时
            //            var sqlRateAndFlowUseTime = new StringBuilder(@"select * from(
            //select  top 1 Convert(decimal(18,2), (CAST (DATEDIFF(ss,CompareDate,OutSiteDate) AS Decimal)/T2.StanardHours))Rate,DATEDIFF(ss,CompareDate,OutSiteDate) CurrentFlowUseTime from StatingHangerProductItem T 
            //INNER JOIN ProcessFlow T2 ON T2.Id=T.ProcessFlowId
            //Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber AND HangerNo=:HangerNo AND IsReturnWorkFlow=1
            //ORDER BY T.InsertDateTime DESC
            //)T_RATE_And_UseTime");
            //var dicOrders = new Dictionary<string, string>();
            //dicOrders.Add("InsertDateTime", "DESC");
            //lucifer/2018年10月12日

            var statingHangerKey = string.Format("{0}:{1}", xID, iD);
            DaoModel.HangerProductItemModel outSiteHangerInfo = null;
            var dicHangerProductItemListExt = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT);
            //var statingHangerList = dicHangerProductItemListExt.Values.Where(f=>f.);
            if (dicHangerProductItemListExt.ContainsKey(statingHangerKey))
            {
                var oList = dicHangerProductItemListExt[statingHangerKey].Where(f => f.OutSiteDate != null && f.HangerNo.Equals(hangerNo.ToString()) && f.MainTrackNumber.Value == xID && iD == int.Parse(f.SiteNo) && (f.IsReturnWorkFlow != null && f.IsReturnWorkFlow.Value));
                if (oList.Count() > 0)
                    outSiteHangerInfo = oList.OrderByDescending(f => f.OutSiteDate).First();
            }

            // var dicHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
            if (null == outSiteHangerInfo)
            {
                //data.AddRange(new byte[] { 0, 0, 0, 0, 0, 0 });
                info = string.Format("【返工出站推送数据】站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo);
                tcpLogError.Info(info);
                return data = new List<byte>() { 0, 0, 0, 0, 0, 0 };
            }
            //data = new List<Byte>();
            int currentFlowUseTime = 0;
            var rate = decimal.Parse("0");
            //var hangerProductItemList = dicHangerProductItemCache[hangerNo.ToString()].Where(f => f.MainTrackNumber.Value == xID && f.SiteNo.Equals(iD.ToString())).OrderByDescending(f => f.OutSiteDate).OrderByDescending(k=>k.CompareDate);
            //var currentHanger = hangerProductItemList.Count() > 0 ? hangerProductItemList.First() : null;
            if (null != outSiteHangerInfo)
            {
                if (outSiteHangerInfo.OutSiteDate == null)//返工出站不发送产量
                {
                    tcpLogError.Info(string.Format("【返工出站推送数据】出站时间为空，异常衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
                    return data = new List<byte>() { 0, 0, 0, 0, 0, 0 };
                }

                if (outSiteHangerInfo.CompareDate == null)
                {
                    currentFlowUseTime = 0;
                }
                else
                {
                    currentFlowUseTime = (outSiteHangerInfo.OutSiteDate.Value - outSiteHangerInfo.CompareDate.Value).Seconds;
                }

                if (!string.IsNullOrEmpty(outSiteHangerInfo.StanardHours))
                    rate = Decimal.Parse(currentFlowUseTime.ToString()) / Convert.ToDecimal(outSiteHangerInfo.StanardHours);
                // rate = decimal.Parse(rate.ToString("#0.00"));
            }

            //今日返工数量
            var sqlOut = new StringBuilder(@"SELECT SUM(TodayCount) TodayCount FROM(
select COUNT(1) TodayCount from SuccessStatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120) AND IsReturnWorkFlow=1 
UNION ALL   
select COUNT(1) TodayCount from StatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120)  AND IsReturnWorkFlow=1 
)T_TodayCount  ");
            var dicCondition = new Dictionary<string, string>();
            dicCondition.Add("MainTrackNumber", xID.ToString());
            dicCondition.Add("SiteNo", iD.ToString());

            var todayStatingOut = QueryForObject<int>(sqlOut, null, true, dicCondition);
            var bTodaySatingOut = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(todayStatingOut));
            data.AddRange(bTodaySatingOut);

            //dicCondition.Add("HangerNo", hangerNo.ToString());
            //var productFlow = QueryForObject<ProductsFlowModel>(sqlRateAndFlowUseTime, null, true, dicCondition);
            //if (null == productFlow)
            //{
            //    var ex = new ApplicationException(string.Format("站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    tcpLogError.Error("【普通站衣架出站信息计算】", ex);
            //    throw ex;
            //}
            var bRate = HexHelper.StringToHexByte(HexHelper.DecimalToHexString4Len(rate * 10000));//productFlow.Rate * 10000));
            if (bRate.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bRate);
            }

            var bFlowUseTimes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(currentFlowUseTime));//productFlow.CurrentFlowUseTime));
            if (bFlowUseTimes.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bFlowUseTimes);
            }
            // info = string.Format("【返工衣架】 返工数:{0} 返工效率:{1} 本次工序用时{2}", todayStatingOut, productFlow.Rate, productFlow.CurrentFlowUseTime);
            info = string.Format("【返工衣架】 返工数:{0} 返工效率:{1} 本次工序用时{2}", todayStatingOut, rate, currentFlowUseTime);
            return data;
        }

        /// <summary>
        /// 检查是否是返工发起站点
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <returns></returns>
        public bool isReworkSourceStating(int mainTrackNumber, int statingNo, string hangerNo)
        {
            var dicCondition = new Dictionary<string, string>();
            dicCondition.Add("MainTrackNumber", mainTrackNumber.ToString());
            dicCondition.Add("StatingNo", statingNo.ToString());
            dicCondition.Add("HangerNo", hangerNo);
            var sql = string.Format("select Count(1) TCount from HangerProductFlowChart where FlowType=1 AND IsReworkSourceStating=1 AND MainTrackNumber=:MainTrackNumber and StatingNo=:StatingNo AND HangerNo=:HangerNo");
            var count = QueryForObject<int>(sql, true, dicCondition);
            return count > 0;
        }

        /// <summary>
        /// 获取上电初始化 站点产量数据
        ///今日数（2字节）+
        ///效率（2字节）
        ///本次工序时间（2字节）
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public bool GetPowerSupplyStatingYield(int xID, int iD, ref List<Byte> data, ref string info)
        {
            //var dicHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);

            //var currentHanger = dicHangerProductItemCache[hangerNo.ToString()].Where(f => f.MainTrackNumber.Value == xID && f.SiteNo.Equals(iD.ToString())).OrderByDescending(f => f.OutSiteDate).First();
            //if (currentHanger.OutSiteDate == null)//返工出站不发送产量
            //{
            //    tcpLogError.Info(string.Format("返工发起站点不发送产量! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    return false;
            //}
            data = new List<Byte>();
            var currentFlowUseTime = 0;//= (currentHanger.OutSiteDate.Value - currentHanger.CompareDate.Value).Seconds;
            var rate = decimal.Parse("0"); //Decimal.Parse(currentFlowUseTime.ToString()) / Convert.ToDecimal(currentHanger.StanardHours);
            rate = decimal.Parse(rate.ToString("#0.00"));

            var sqlOut = new StringBuilder(@"
            select COUNT(1) TodayCount from StatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                                       DATEADD(day, 1, GETDATE()), 120)");
            var dicCondition = new Dictionary<string, string>();
            dicCondition.Add("MainTrackNumber", xID.ToString());
            dicCondition.Add("SiteNo", iD.ToString());

            var todayStatingOut = QueryForObject<int>(sqlOut, null, true, dicCondition);
            //

            //todayStatingOut += cacheOutNum;
            var bTodaySatingOut = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(todayStatingOut));
            data.AddRange(bTodaySatingOut);

            //dicCondition.Add("HangerNo", hangerNo.ToString());
            //var productFlow = QueryForObject<ProductsFlowModel>(sqlRateAndFlowUseTime, null, true, dicCondition);
            //if (null == productFlow)
            //{
            //    var ex = new ApplicationException(string.Format("站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    tcpLogError.Error("【普通站衣架出站信息计算】", ex);
            //    throw ex;
            //}
            var bRate = HexHelper.StringToHexByte(HexHelper.DecimalToHexString4Len(rate * 10000));
            if (bRate.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bRate);
            }

            var bFlowUseTimes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(currentFlowUseTime));
            if (bFlowUseTimes.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bFlowUseTimes);
            }
            info = string.Format("今日数:{0} 效率:{1} 本次工序用时{2}", todayStatingOut, rate, currentFlowUseTime);
            return true;
        }
        /// <summary>
        ///  获取上电初始化 返工出站推送数据
        ///今日数（2字节）+
        ///效率（2字节）
        ///本次工序时间（2字节）
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public List<byte> GetPowerSupplyStatingYieldRework(int xID, int iD, ref string info)
        {
            var data = new List<Byte>();
            //本工序效率及用时
            var sqlRateAndFlowUseTime = new StringBuilder(@"select * from(
select  top 1 Convert(decimal(18,2), (CAST (DATEDIFF(ss,CompareDate,OutSiteDate) AS Decimal)/T2.StanardHours))Rate,DATEDIFF(ss,CompareDate,OutSiteDate) CurrentFlowUseTime from StatingHangerProductItem T 
INNER JOIN ProcessFlow T2 ON T2.Id=T.ProcessFlowId
Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber AND HangerNo=:HangerNo AND IsReturnWorkFlow=1
ORDER BY T.InsertDateTime DESC
)T_RATE_And_UseTime");
            //var dicOrders = new Dictionary<string, string>();
            //dicOrders.Add("InsertDateTime", "DESC");

            //今日返工数量
            var sqlOut = new StringBuilder(@"SELECT SUM(TodayCount) TodayCount FROM(
select COUNT(1) TodayCount from SuccessStatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120) AND IsReturnWorkFlow=1 
UNION ALL   
select COUNT(1) TodayCount from StatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120)  AND IsReturnWorkFlow=1 
)T_TodayCount  ");
            var dicCondition = new Dictionary<string, string>();
            dicCondition.Add("MainTrackNumber", xID.ToString());
            dicCondition.Add("SiteNo", iD.ToString());

            var todayStatingOut = QueryForObject<int>(sqlOut, null, true, dicCondition);
            var bTodaySatingOut = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(todayStatingOut));
            data.AddRange(bTodaySatingOut);

            //dicCondition.Add("HangerNo", hangerNo.ToString());
            //var productFlow = QueryForObject<ProductsFlowModel>(sqlRateAndFlowUseTime, null, true, dicCondition);
            //if (null == productFlow)
            //{
            //    var ex = new ApplicationException(string.Format("站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    tcpLogError.Error("【普通站衣架出站信息计算】", ex);
            //    throw ex;
            //}
            var bRate = HexHelper.StringToHexByte(HexHelper.DecimalToHexString4Len(0 * 10000));
            if (bRate.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bRate);
            }

            var bFlowUseTimes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(0));
            if (bFlowUseTimes.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bFlowUseTimes);
            }
            info = string.Format("【返工衣架】 返工数:{0} 返工效率:{1} 本次工序用时{2}", todayStatingOut, 0, 0);
            return data;
        }


        /// <summary>
        /// 普通站出站推送数据
        ///今日数（2字节）+
        ///效率（2字节）
        ///本次工序时间（2字节）
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public bool GetOutSiteHangerFlowInfo(int xID, int iD, int hangerNo, ref List<Byte> data, ref string info)
        {
            var statingHangerKey = string.Format("{0}:{1}", xID, iD);
            DaoModel.HangerProductItemModel outSiteHangerInfo = null;
            var dicHangerProductItemListExt = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT);
            //var statingHangerList = dicHangerProductItemListExt.Values.Where(f=>f.);
            if (dicHangerProductItemListExt.ContainsKey(statingHangerKey))
            {
                var oList = dicHangerProductItemListExt[statingHangerKey].Where(f => f.OutSiteDate != null && f.HangerNo.Equals(hangerNo.ToString()) && f.MainTrackNumber.Value == xID && iD == int.Parse(f.SiteNo));
                if (oList.Count() > 0)
                    outSiteHangerInfo = oList.OrderByDescending(f => f.OutSiteDate).First();
            }

            if (null == outSiteHangerInfo)
            {
                //data.AddRange(new byte[] { 0, 0, 0, 0, 0, 0 });
                tcpLogError.Error(new ApplicationException(string.Format("【普通站出站推送数据】站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo)));
                return false;
            }
            data = new List<Byte>();
            int currentFlowUseTime = 0;
            var rate = decimal.Parse("0");

            // var currentHanger = hangerProductItemList.Count() > 0 ? hangerProductItemList.First() : null;

            if (outSiteHangerInfo.OutSiteDate == null)//返工出站不发送产量
            {
                tcpLogError.Error(new ApplicationException(string.Format("普通站出站推送数据【】返工发起站点不发送产量! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo)));
                return false;
            }

            if (outSiteHangerInfo.OutSiteDate == null || outSiteHangerInfo.CompareDate == null)
            {
                currentFlowUseTime = 0;
                tcpLogError.Error(new ApplicationException(string.Format("【出战推送】异常衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo)));
            }
            else
            {
                currentFlowUseTime = (outSiteHangerInfo.OutSiteDate.Value - outSiteHangerInfo.CompareDate.Value).Seconds;
            }

            if (!string.IsNullOrEmpty(outSiteHangerInfo.StanardHours))
                rate = Decimal.Parse(currentFlowUseTime.ToString()) / Convert.ToDecimal(outSiteHangerInfo.StanardHours);


            // //同工序一个站点进站再从另一站出站(人为手工) 获取出站生产顺序号
            //var flowIndex = dicHangerProductItemCache[hangerNo.ToString()].Where(f => f.MainTrackNumber.Value == xID && null != f.OutSiteDate).OrderByDescending(f => f.OutSiteDate).OrderByDescending(f => f.FlowIndex).Select(f => f.FlowIndex).Distinct().First();
            // HangerProductFlowChartModel fcCurrentHanger = null;
            //同工序不同站出站
            //var dicHangerFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo + ""))//dicHangerFlowChartCache.ContainsKey(hangerNo.ToString()))
            {
                var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo + ""); //dicHangerFlowChartCache[hangerNo.ToString()];
                var flowIndexList = hangerFlowChartList.Where(f => null != f.StatingNo && f.StatingNo.Value == iD && f.MainTrackNumber.Value == xID).Select(f => f.FlowIndex);
                var flowIndex = flowIndexList.Count() > 0 ? flowIndexList.Distinct().First() : null;
                if (null != flowIndex)
                {
                    foreach (var hfc in hangerFlowChartList)
                    {
                        if (hfc.FlowIndex.Value == flowIndex.Value && hfc.Status.Value == HangerProductFlowChartStaus.Producting.Value && hfc.StatingNo != null && hfc.StatingNo.Value != iD)
                        {
                            ////站内数-1
                            //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hfc.MainTrackNumber.Value, StatingNo = hfc.StatingNo.Value, OnLineSum = -1 };
                            //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            //var logMessage = string.Format("【同工序不同站出战】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【生产序号:{3} 站点:{4}】衣架站内衣架数据!", hangerNo, xID, iD, flowIndex.Value, inNumModel?.StatingNo);
                            //tcpLogInfo.Info(logMessage);

                            hfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                            hfc.IncomeSiteDate = null;
                            hfc.AllocationedDate = null;
                            hfc.isAllocationed = false;
                            tcpLogError.Info(string.Format("【同工序不同站出站】主轨:{0} 站点:{1} 衣架号:{2} 修正站内衣架明细!", xID, iD, hangerNo));
                        }
                        //if (fcCurrentHanger == null && hfc.FlowIndex.Value == flowIndex.Value && hfc.Status.Value == HangerProductFlowChartStaus.Successed.Value && hfc.StatingNo != null && hfc.CompareDate != null && null != hfc.OutSiteDate)
                        //{
                        //    fcCurrentHanger = hfc;
                        //}
                    }
                    // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo.ToString()] = hangerFlowChartList;
                    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo + "", hangerFlowChartList);
                }

                //if (fcCurrentHanger != null)
                //{
                //    if (fcCurrentHanger.OutSiteDate == null || fcCurrentHanger.CompareDate == null)
                //    {
                //        currentFlowUseTime = 0;
                //    }
                //    else
                //    {
                //        currentFlowUseTime = (fcCurrentHanger.OutSiteDate.Value - fcCurrentHanger.CompareDate.Value).Seconds;
                //    }
                //    if (!string.IsNullOrEmpty(fcCurrentHanger.StanardHours))
                //        rate = Decimal.Parse(currentFlowUseTime.ToString()) / Convert.ToDecimal(fcCurrentHanger.StanardHours);
                //}
            }
            rate = decimal.Parse(rate.ToString("#0.00"));
            //            //本工序效率及用时
            //            var sqlRateAndFlowUseTime = new StringBuilder(@"select * from(
            //select  top 1 Convert(decimal(18,2), (CAST (DATEDIFF(ss,CompareDate,OutSiteDate) AS Decimal)/T2.StanardHours))Rate,DATEDIFF(ss,CompareDate,OutSiteDate) CurrentFlowUseTime from StatingHangerProductItem T 
            //INNER JOIN ProcessFlow T2 ON T2.Id=T.ProcessFlowId
            //Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber AND HangerNo=:HangerNo
            //ORDER BY T.InsertDateTime DESC
            //)T_RATE_And_UseTime");
            //var dicOrders = new Dictionary<string, string>();
            //dicOrders.Add("InsertDateTime", "DESC");

            //今日站出
            //            var sqlOut = new StringBuilder(@"SELECT SUM(TodayCount) TodayCount FROM(
            //select COUNT(1) TodayCount from SuccessStatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
            //AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
            //                                           DATEADD(day, 1, GETDATE()), 120)  
            //UNION ALL   
            //select COUNT(1) TodayCount from StatingHangerProductItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber
            //AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
            //                                           DATEADD(day, 1, GETDATE()), 120)
            //)T_TodayCount  ");
            //            var dicCondition = new Dictionary<string, string>();
            //            dicCondition.Add("MainTrackNumber", xID.ToString());
            //            dicCondition.Add("SiteNo", iD.ToString());

            //            var todayStatingOut = QueryForObject<int>(sqlOut, null, true, dicCondition);

            //var cacheOutNum = 0;
            //var cKey = string.Format("{0}:{1}", xID, iD);
            //var dicStatingHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.StatingHangerProductItem>>(SusRedisConst.STATING_HANGER_PRODUCT_ITEM);
            //if (!dicHangerProductItemCache.ContainsKey(cKey))
            //{
            //    var statingOutList = dicStatingHangerProductItemCache[cKey];
            //    cacheOutNum = statingOutList.Where(f => f.OutSiteDate != null && f.OutSiteDate>DateTime.Parse(DateTime.Now.ToShortDateString()) && 
            //    f.OutSiteDate < DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString()) && f.MainTrackNumber.Value==(short)xID && f.SiteNo.Equals(iD.ToString())
            //    ).ToList<DaoModel.StatingHangerProductItem>().Count;
            //}


            var sqlOut = new StringBuilder(@"
            select COUNT(1) TodayCount from StatingHangerProductItem Where SiteNo=@SiteNo AND MainTrackNumber=@MainTrackNumber
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                                       DATEADD(day, 1, GETDATE()), 120)");
            //var dicCondition = new Dictionary<string, string>();
            //dicCondition.Add("MainTrackNumber", xID.ToString());
            //dicCondition.Add("SiteNo", iD.ToString());

            var todayStatingOut = DapperHelp.QueryForObject<int>(sqlOut.ToString(), new { MainTrackNumber = xID.ToString(), SiteNo = iD.ToString() }); //QueryForObject<int>(sqlOut, null, true, dicCondition);
            //
            if (todayStatingOut == 0)
            {
                todayStatingOut = 1;
            }
            //todayStatingOut += cacheOutNum;
            var bTodaySatingOut = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(todayStatingOut));
            data.AddRange(bTodaySatingOut);

            //dicCondition.Add("HangerNo", hangerNo.ToString());
            //var productFlow = QueryForObject<ProductsFlowModel>(sqlRateAndFlowUseTime, null, true, dicCondition);
            //if (null == productFlow)
            //{
            //    var ex = new ApplicationException(string.Format("站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    tcpLogError.Error("【普通站衣架出站信息计算】", ex);
            //    throw ex;
            //}
            var bRate = HexHelper.StringToHexByte(HexHelper.DecimalToHexString4Len(rate * 10000));
            if (bRate.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bRate);
            }

            var bFlowUseTimes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(currentFlowUseTime));
            if (bFlowUseTimes.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bFlowUseTimes);
            }
            info = string.Format("今日数:{0} 效率:{1} 本次工序用时{2}", todayStatingOut, rate, currentFlowUseTime);
            return true;
        }

        /// <summary>
        /// 挂片站出站推送数据
        ///今日数（2字节）+
        ///效率（2字节）
        ///本次工序时间（2字节）
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public bool GetOutSiteHagingPieceFlowInfo(int xID, int iD, int hangerNo, ref List<Byte> data, ref string info)
        {

            var dicHangingPieceHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerHangingRequestModel>(SusRedisConst.HANGER_HNGING_PIECE_REQUEST_ACTION);
            if (!dicHangingPieceHanger.ContainsKey(hangerNo.ToString()))
            {
                tcpLogError.Info(string.Format("【挂片站出站推送产量】未找到挂片读卡衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
                return false;
            }
            var hangingPieceHanger = dicHangingPieceHanger[hangerNo.ToString()];

            data = new List<Byte>();
            var currentFlowUseTime = (hangingPieceHanger.OutSiteDate - hangingPieceHanger.CompareDate).Seconds;
            var rate = 0 == hangingPieceHanger.StanardHours ? 0 : (Decimal.Parse(currentFlowUseTime.ToString()) / hangingPieceHanger.StanardHours);
            rate = decimal.Parse(rate.ToString("#0.00"));


            //今日挂片
            var sqlToday = new StringBuilder(@"
            SELECT SUM(TodayHangingPieceCount) TodayHangingPieceCount FROM(
            select COUNT(1) TodayHangingPieceCount from HangerStatingAllocationItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber 
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                                       DATEADD(day, 1, GETDATE()), 120) 
            AND Memo='-1' 
            UNION ALL   
            select COUNT(1) TodayHangingPieceCount from SuccessHangerStatingAllocationItem Where SiteNo=:SiteNo AND MainTrackNumber=:MainTrackNumber 
            AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
            AND Memo='-1' AND CAST(HangerNo AS INT)>0
            )T_TodayHangingPieceCount ");

            var dicCondition = new Dictionary<string, string>();
            dicCondition.Add("SiteNo", iD.ToString());
            dicCondition.Add("MainTrackNumber", xID.ToString());
            var todayNum = QueryForObject<int>(sqlToday, null, true, dicCondition);

            //todayStatingOut += cacheOutNum;
            var bTodaySatingOut = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(todayNum));
            data.AddRange(bTodaySatingOut);

            //dicCondition.Add("HangerNo", hangerNo.ToString());
            //var productFlow = QueryForObject<ProductsFlowModel>(sqlRateAndFlowUseTime, null, true, dicCondition);
            //if (null == productFlow)
            //{
            //    var ex = new ApplicationException(string.Format("站内找不到该衣架! 主轨:{0} 站点:{1} 衣架号:{2}", xID, iD, hangerNo));
            //    tcpLogError.Error("【普通站衣架出站信息计算】", ex);
            //    throw ex;
            //}
            var bRate = HexHelper.StringToHexByte(HexHelper.DecimalToHexString4Len(rate * 10000));
            if (bRate.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bRate);
            }

            var bFlowUseTimes = HexHelper.StringToHexByte(HexHelper.TenToHexString4Len(currentFlowUseTime));
            if (bFlowUseTimes.Length > 2)
            {
                data.AddRange(new byte[] { 0, 0 });
            }
            else
            {
                data.AddRange(bFlowUseTimes);
            }
            info = string.Format("今日数:{0} 效率:{1} 本次工序用时{2}", todayNum, rate, currentFlowUseTime);
            return true;
        }
        /// <summary>
        /// 挂片站站点处理
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        internal void HangingPieceHandler(int xID, int iD, int hangerNo)
        {

            lock (lockObject)
            {
                ////站内数+1
                //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = xID, StatingNo = iD, OnLineSum = 1 };
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                //var logMessage = string.Format("【挂片站站点处理】衣架号:{0} 主轨:{1} 站点:{2} -->衣架站内衣架数据!", hangerNo, xID, iD);
                //tcpLogInfo.Info(logMessage);

                var p = CANProductsValidService.Instance.GetWaitBindHangerProductList(iD + "");
                if (null == p)
                {
                    tcpLogInfo.ErrorFormat("主轨:{0} 站点:{1} 无上线制品!", xID, iD);
                    return;
                }
                //获取在线制品
                var onlineProductsFlowChartList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);

                var key = string.Format("{0}:{1}", xID, p.ProductionNumber.Value);
                IList<ProductsFlowChartCacheTempModel> onLineProcessFlowChartList = null;  //SusCacheMain.Cache.Get(key) as List<ProductsFlowChartCacheTempModel>;
                if (!onlineProductsFlowChartList.ContainsKey(key))
                {
                    tcpLogInfo.ErrorFormat("主轨:{0} 站点:{1} 无上线制品工艺图!", xID, iD);
                    return;
                }
                onLineProcessFlowChartList = onlineProductsFlowChartList[key];
                if (onLineProcessFlowChartList.Count() == 0)
                {
                    tcpLogInfo.ErrorFormat("主轨:{0} 站点:{1} 无上线制品工艺图!", xID, iD);
                    return;
                }
                var mainStatingKey = string.Format("{0}:{1}", xID, iD);
                var dicHangingPieceStating = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);
                var hangingProcessFlowChart = onLineProcessFlowChartList.Where(f => short.Parse(f.CraftFlowNo) == 1).Count() > 0 ? onLineProcessFlowChartList.Where(f => short.Parse(f.CraftFlowNo) == 1).First() : null;
                if (null == hangingProcessFlowChart)
                {
                    tcpLogInfo.ErrorFormat("主轨:{0} 站点:{1} 在线制品无挂片工序!", xID, iD);
                    return;
                }
                if (!dicHangingPieceStating.ContainsKey(mainStatingKey))
                {
                    hangingProcessFlowChart.ProductId = p.Id;
                    hangingProcessFlowChart.HangerNo = hangerNo;
                    hangingProcessFlowChart.MainTrackNumber = xID;
                    hangingProcessFlowChart.StatingNo = iD.ToString();
                    hangingProcessFlowChart.PColor = p.PColor;
                    hangingProcessFlowChart.PSize = p.PSize;
                    hangingProcessFlowChart.StyleNo = p.StyleNo;
                    hangingProcessFlowChart.ProcessOrderNo = p.ProcessOrderNo;
                    hangingProcessFlowChart.PColor = p.PColor;
                    hangingProcessFlowChart.LineName = p.LineName;
                    hangingProcessFlowChart.Num = p.Unit?.ToString();

                    dicHangingPieceStating.Add(mainStatingKey, new List<ProductsFlowChartCacheTempModel>() { hangingProcessFlowChart });
                    return;
                }
                var isHangingRepeat = false;
                var incomeStatingHangerList = dicHangingPieceStating[mainStatingKey];
                foreach (var isHanger in incomeStatingHangerList)
                {
                    if (isHanger.HangerNo == hangerNo && isHanger.MainTrackNumber == xID && null != isHanger.StatingNo && isHanger.StatingNo.Equals(iD + ""))
                    {
                        isHangingRepeat = true;
                        break;
                    }
                }
                if (!isHangingRepeat)
                {
                    var list = dicHangingPieceStating[mainStatingKey];
                    hangingProcessFlowChart.HangerNo = hangerNo;
                    hangingProcessFlowChart.MainTrackNumber = xID;
                    hangingProcessFlowChart.StatingNo = iD.ToString();
                    hangingProcessFlowChart.ProductId = p.Id;
                    hangingProcessFlowChart.PColor = p.PColor;
                    hangingProcessFlowChart.PSize = p.PSize;
                    hangingProcessFlowChart.StyleNo = p.StyleNo;
                    hangingProcessFlowChart.ProcessOrderNo = p.ProcessOrderNo;
                    hangingProcessFlowChart.PColor = p.PColor;
                    hangingProcessFlowChart.LineName = p.LineName;
                    hangingProcessFlowChart.Num = p.Unit?.ToString();
                    list.Add(hangingProcessFlowChart);
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER)[mainStatingKey] = list;
                }
            }
        }
    }
}

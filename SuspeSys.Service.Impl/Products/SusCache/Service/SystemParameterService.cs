using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Service
{
    public class SystemParameterService
    {
        private SystemParameterService() { }
        public static SystemParameterService Instance { get { return new SystemParameterService(); } }

        #region 私有函数
        /// <summary>
        /// 通过参数key获取参数信息
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        private string GetSystemModuleParameterByParameterKey(string parameterKey)
        {
            var parameter = NewSusRedisClient.RedisTypeFactory.GetList<SystemModuleParameterModel>(SusRedisConst.SYSTEM_PARAMETER_QUEUE);

            if (parameter == null)
                return string.Empty;

            var paraValue = parameter.Where(o => !string.IsNullOrEmpty(o.ParamterKey) && o.ParamterKey.Equals(parameterKey, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (paraValue == null)
                return string.Empty;

            if (paraValue.SystemModuleParameterValueList == null || paraValue.SystemModuleParameterValueList.Count() == 0)
                return string.Empty;
            else
                return paraValue.SystemModuleParameterValueList.First().ParameterValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        private string GetSystemModuleParameterByParameterKeyAndProductId(string parameterKey, string productLineId)
        {
            var parameter = NewSusRedisClient.RedisTypeFactory.GetList<SystemModuleParameterModel>(SusRedisConst.SYSTEM_PARAMETER_QUEUE);

            if (parameter == null)
                return string.Empty;

            var paraValue = parameter.Where(o => !string.IsNullOrEmpty(o.ParamterKey) && o.ParamterKey.Equals(parameterKey, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (paraValue == null)
                return string.Empty;
            if (paraValue.SystemModuleParameterValueList == null || paraValue.SystemModuleParameterValueList.Count() == 0)
                return string.Empty;
            else
                return paraValue.SystemModuleParameterValueList?.Where(o => o.ProductLineId.Equals(productLineId))?.First().ParameterValue;
        }

        private string GetSystemModuleParameterByParameterKeyAndProductId(string parameterKey, string mainTrackNo, string statingNo)
        {
            var parameter = NewSusRedisClient.RedisTypeFactory.GetList<SystemModuleParameterModel>(SusRedisConst.SYSTEM_PARAMETER_QUEUE);

            if (parameter == null)
                return string.Empty;

            var paraValue = parameter.Where(o => !string.IsNullOrEmpty(o.ParamterKey) && o.ParamterKey.Equals(parameterKey, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (paraValue == null)
                return string.Empty;

            string productLineId = string.Empty;

            //缓存生产线相关数据
            var pipeliningStating = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, PipeliningCache>(SusRedisConst.PIPELINING_STATING_QUEUE);//.RedisList<string, CardInfo>(SusRedisConst.PIPELINING_STATING_QUEUE);
            if (pipeliningStating == null)
                return string.Empty;

            string key = $"{mainTrackNo.Trim()}:{statingNo.Trim()}";

            if (!pipeliningStating.ContainsKey(key))
                return string.Empty;

            productLineId = pipeliningStating[key].PipelingId;

            if (paraValue.SystemModuleParameterValueList == null || paraValue.SystemModuleParameterValueList.Count() == 0)
                return string.Empty;
            else
            {
                if (paraValue.SystemModuleParameterValueList?.Where(o => o.ProductLineId.Equals(productLineId)).Count() == 0) return "";
                return paraValue.SystemModuleParameterValueList?.Where(o => o.ProductLineId.Equals(productLineId))?.First().ParameterValue;
            }
        }

        #endregion


        /// <summary>
        /// 获取吊挂 挂片站参数值
        /// </summary>
        /// <returns></returns>
        public string GetHangUpLineParameterValue(SystemParameterHangUpLineHanger hanger, string mainTrackNo, string statingNo)
        {
            return this.GetSystemModuleParameterByParameterKeyAndProductId(hanger.ToString(), mainTrackNo, statingNo);
        }

        /// <summary>
        /// 获取吊挂 生产线参数值
        /// </summary>
        /// <param name="productline"></param>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        public string GetHangUpLineProductsLineValue(SystemParameterHangUpProductsLine productline,  string mainTrackNo, string statingNo)
        {
            return this.GetSystemModuleParameterByParameterKeyAndProductId(productline.ToString(), mainTrackNo, statingNo);

        }

        /// <summary>
        /// 获取吊挂 其他参数值
        /// </summary>
        /// <param name="productline"></param>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        public string GetHangUpLineOtherValue(SystemParameterHangUpOther other, string mainTrackNo, string statingNo)
        {
            return this.GetSystemModuleParameterByParameterKeyAndProductId(other.ToString(), mainTrackNo, statingNo);
        }

        /// <summary>
        /// 获取 系统-- 考勤参数值
        /// </summary>
        /// <param name="attendance"></param>
        /// <returns></returns>
        public string GetSystemAttendance(SystemParameterSystemAttendance attendance)
        {
            return this.GetSystemModuleParameterByParameterKey(attendance.ToString());
        }

        /// <summary>
        /// 获取 系统--生产
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string GetSystemProduct(SystemAttendanceProduct product)
        {
            return this.GetSystemModuleParameterByParameterKey(product.ToString());
        }

        /// <summary>
        /// 获取 系统--其他
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public string GetSystemOther(SystemAttendanceOther other)
        {
            return this.GetSystemModuleParameterByParameterKey(other.ToString());

        }

        #region 参数判断
        /// <summary>
        /// 出站判断  “挂片站出衣架达到计划数后停止出衣”
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <returns>true:可以出站，false： 不可以出站</returns>
        public bool StartingStopOutWhenOverPlan(int mainTrackNo, int statingNo)
        {
            string paraValue = SystemParameterService.Instance.GetHangUpLineParameterValue(SystemParameterHangUpLineHanger.StartingStopOutWhenOverPlan, mainTrackNo.ToString(), statingNo.ToString());
            bool judge = !string.IsNullOrEmpty(paraValue) && paraValue.Equals("1");
            if (judge)
            {
                var productModel = CANProductsValidService.Instance.GetWaitBindHangerProductList(statingNo.ToString());

                if (productModel == null)
                {
                    return true;
                }
                else
                {
                    ProductsQueryServiceImpl impl = new ProductsQueryServiceImpl();
                    //获取生产数
                    #region 获取制品生产信息
                    //:0:未分配;1:已分配;2.上线;3.已完成
                    string queryString = @"SELECT  Id, PROCESSFLOWCHART_Id, PROCESSORDER_Id, ProductionNumber, ImplementDate, HangingPieceSiteNo, 
                   ProcessOrderNo, Status, CustomerPurchaseOrderId, OrderNo, StyleNo, PColor, PO, PSize, LineName, FlowSection, Unit, 
                   TaskNum,T.GroupNo,
                       (SELECT SUM(Expr1) FROM(
					   SELECT  COUNT(1) AS Expr1
                        FROM       dbo.WaitProcessOrderHanger
                        WHERE    (ProductsId = T.Id)
						UNION all
						SELECT COUNT(1) AS Expr1 FROM dbo.SucessProcessOrderHanger WHERE  (ProductsId = T.Id) AND CAST(HangerNo AS INT)<0 AND MEMO='半成品衣架'
					   )TT) AS OnlineNum, TodayHangingPieceSiteNum, (CASE ISNULL([Status], 0) 
                   WHEN 0 THEN '未分配' WHEN 1 THEN '已分配' WHEN 2 THEN '上线中' WHEN 3 THEN '已完成' END) AS StatusText,
                       (SELECT  SUM(ISNULL(SizeNum, 0)) AS TodayProdOutNum
                        FROM       dbo.SucessProcessOrderHanger
                        WHERE    ( CAST(HangerNo AS INT)>0 AND InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), 
                                           DATEADD(day, 1, GETDATE()), 120)) AND (ProductsId = T.Id)) AS TodayProdOutNum, TodayBindCard, 
                   TodayRework,
                       (SELECT  ISNULL(SUM(HCount), 0) AS HCount
                        FROM       (SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM       dbo.HangerStatingAllocationItem
                                            WHERE    (Memo = '-1') AND (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND 
                                                               CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120))
                                            GROUP BY ProductsId
                                            UNION ALL
                                            SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM      dbo.SuccessHangerStatingAllocationItem
                                            WHERE   (Memo = '-1') AND (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND 
                                                               CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)) AND CAST(HangerNo AS INT)>0
                                            GROUP BY ProductsId) AS Z
                        WHERE    (ProductsId = T.Id)) AS TotalHangingPieceSiteNum, TotalRework, TotalBindNum,
                       (SELECT  SUM(ISNULL(SizeNum, 0)) AS TodayProdOutNum
                        FROM       dbo.SucessProcessOrderHanger AS SucessProcessOrderHanger_1
                        WHERE    (ProductsId = T.Id) AND CAST(HangerNo AS INT)>0) AS TotalProdOutNum,
(
SELECT  ISNULL(SUM(HCount), 0) AS HCount
                        FROM       (SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM       dbo.HangerStatingAllocationItem
                                            WHERE    (Memo = '-1') 
                                            GROUP BY ProductsId
                                            UNION ALL
                                            SELECT  ProductsId, COUNT(1) AS HCount
                                            FROM      dbo.SuccessHangerStatingAllocationItem
                                            WHERE   (Memo = '-1')  AND CAST(HangerNo AS INT)>0
                                            GROUP BY ProductsId) AS Z
                        WHERE    (ProductsId = T.Id)
) TotalHangingNum
FROM      dbo.Products AS T
WHERE   Id=@Id";
                    #endregion

                    productModel = Dao.DapperHelp.QueryForObject<ProductsModel>(queryString, new { Id = productModel.Id });

                    if (productModel == null)
                    {
                        return true;
                    }

                    if (productModel.TotalHangingPieceSiteNum >= productModel.TaskNum)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            else
            {
                return true;
            }
            
        }
        #endregion
    }
}

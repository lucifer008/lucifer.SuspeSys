using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products
{
    public class SusReworkQueryService : ServiceBase
    {
        private SusReworkQueryService() { }
        public static SusReworkQueryService Instance
        {
            get
            {
                return new SusReworkQueryService();
            }
        }
        private ProductsQueryServiceImpl productsQueryService = new ProductsQueryServiceImpl();
        /// <summary>
        /// 检查衣架是否具备返工条件
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag">
        //        00＝允许出站；
        //01＝不允许出站，站点不存在
        //02＝不允许出站，疵点错误，请重新输入疵点
        //03＝不允许出站，工序代码错误或者未找到生产的工序代码
        //04＝不允许出站，站点满或停止工作
        //05＝不允许出站，不允许返工操作
        //06=不允许出站，站点员工未登录
        //07=不允许出站,桥接站非携带工序
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CheckIsCanRework(int mainTrackNumber, string statingNo,string hangerNo,ref int tag,  ref string error)
        {
            var isHangingPieceStating = productsQueryService.isHangingPiece(null, statingNo.ToString(), mainTrackNumber.ToString());
            if (isHangingPieceStating)//如果出战消息来源于挂片站
            {
                tag = 5;
                var ex = new HangingPieceReworkException(string.Format("【衣架返工】 挂片站不能返工! 主轨:{0} 衣架:{1} 站点:{2}", mainTrackNumber, hangerNo, statingNo));
                tcpLogError.Error(ex);
                throw ex;
            }
            bool isLogin = productsQueryService.CheckStatingIsLogin(statingNo,mainTrackNumber);
            if (!isLogin)
            {
                tag = 6;
                var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】没有员工登录,不能返工!", mainTrackNumber, statingNo));
                tcpLogError.Error(ex);
                throw ex;
            }
           // var dicHangerProductFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProductFlowChartCache.ContainsKey(hangerNo)) {
            {   tag = 3;
                error = string.Format("【衣架返工】 该衣架【{0}】没有生产记录，不能返工--->主轨:{1} 返工发起站点:{2}", hangerNo, mainTrackNumber, statingNo);
                var ex = new HangerNoProductException(error);
                errorLog.Error(ex);
                throw ex;
            }
            if (NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo).Where(f=> f.IsFlowSucess!=null && f.IsFlowSucess.Value).ToList<HangerProductFlowChartModel>().Count==0)
            {
                tag = 3;
                error = string.Format("【衣架返工】 该衣架【{0}】没有生产记录，不能返工--->主轨:{1} 返工发起站点:{2}", hangerNo, mainTrackNumber, statingNo);
                var ex = new HangerNoProductException(error);
                errorLog.Error(ex);
                throw ex;
            }
            var isNonInBridgeByCompareFlow = CANProductsService.Instance.IsNonInBridgeByCompareFlowAction(mainTrackNumber, int.Parse(statingNo), int.Parse(hangerNo));
            if (isNonInBridgeByCompareFlow) {
                tag = 7;
                error = string.Format("【衣架返工】 该衣架【{0}】桥接站不能返工--->主轨:{1} 返工发起站点:{2}", hangerNo, mainTrackNumber, statingNo);
                var ex = new HangerNoProductException(error);
                errorLog.Error(ex);
                throw ex;
            }
            //var dicCondition = new Dictionary<string, string>();
            //dicCondition.Add("HangerNo", hangerNo);
            ////检查衣架是否生产过工序
            //var sqlCheck = string.Format("select Count(1)PCount from HangerProductFlowChart where HangerNo=:HangerNo AND IsFlowSucess=1");
            //var tCount = QueryForObject<int>(sqlCheck, true, dicCondition);
            //if (tCount == 0)
            //{
            //    tag = 3;
            //    error = string.Format("【衣架返工】 该衣架【{0}】没有生产记录，不能返工--->主轨:{1} 返工发起站点:{2}", hangerNo, mainTrackNumber, statingNo);
            //    var ex = new HangerNoProductException(error);
            //    errorLog.Error(ex);
            //    throw ex;
            //    //return false;
            //}
            //var dicQueueCondition = new Dictionary<string, string>();
            //dicQueueCondition.Add("StatingNo", statingNo);
            //dicQueueCondition.Add("HangerNo", hangerNo);
            //dicQueueCondition.Add("MainTrackNumber", mainTrackNumber.ToString());
            //var sql = string.Format("select * from HangerReworkRequestQueue where hangerNo=:HangerNo and MainTrackNumber=:MainTrackNumber and statingNo=:StatingNo");
            //var listReQueue = Query<HangerReworkRequestQueue>(sql, true, dicQueueCondition);
            //if (listReQueue.Where(f => f.Status == 0).ToList<HangerReworkRequestQueue>().Count > 0)
            //{
            //    tag = 7;
            //    error = string.Format("【衣架返工】 该衣架【{0}】正在返工中，一个衣架不能重复返工--->主轨:{1} 返工发起站点:{2}", hangerNo, mainTrackNumber, statingNo);
            //    return false;
            //}

            return true;
        }
    }
}

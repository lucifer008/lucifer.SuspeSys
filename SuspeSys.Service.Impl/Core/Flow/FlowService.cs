using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.AuxiliaryTools;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Flow
{
    public class FlowService : SusLog
    {

        private FlowService() { }
        public readonly static FlowService Instance = new FlowService();
        private readonly static object lockObjct = new object();
        //  /// <summary>
        //  /// 获取当前生产到第几道工序
        //  /// </summary>
        //  /// <param name="hangerProuctList"></param>
        //  /// <returns></returns>
        //  public int GetCurrentProductingIndex(int hangerNo, string mainTrackNo, string statingNo)
        //  {
        //      //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //      var hangerProuctList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo+""); //dicHangerProcessFlowChart[hangerNo.ToString()];

        //     // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
        //      if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo+"")) //dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo.ToString()))
        //      {
        //          //衣架衣架生产完成!
        //          tcpLogError.ErrorFormat("衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", hangerNo, mainTrackNo, statingNo);
        //          return -2;
        //      }
        //      var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo+""); //dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

        //      //当前站点衣架未生产的工序
        //      var nonProductsStatingFlow = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        //  && f.StatingNo.Value == current.StatingNo
        //  && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //  && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //  || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))));
        //      if (nonProductsStatingFlow.Count() == 0)
        //      {
        //          //衣架生产完成
        //          if (current.FlowIndex == 1)
        //          {
        //              return current.FlowIndex;
        //          }

        //          var ex = new FlowDeleteOrStatingDeletedException();
        //          ex.MainTrackNumber = int.Parse(mainTrackNo);
        //          ex.StatingNo = int.Parse(statingNo);
        //          ex.NextFlowIndex = current.FlowIndex;
        //          ex.FlowNo = current.FlowNo;
        //          ex.HangerNo = hangerNo;
        //          var eqFlowStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != -1).Select(k => k.StatingNo).Distinct();
        //          var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo && !f.IsFlowSucess.Value).Count() > 0;
        //          ex.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
        //          throw ex;
        //      }

        //      var currentHangerFlowisExist = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
        //      //当前工序不存在
        //      if (!currentHangerFlowisExist)
        //      {
        //          var ex = new FlowDeleteOrStatingDeletedException();
        //          ex.MainTrackNumber = int.Parse(mainTrackNo);
        //          ex.StatingNo = int.Parse(statingNo);
        //          ex.NextFlowIndex = current.FlowIndex;
        //          ex.HangerNo = hangerNo;
        //          ex.FlowNo = current.FlowNo;
        //          var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo).Count() > 0;
        //          ex.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
        //          throw ex;
        //      }
        //      var currentHangerFlowIsMatching = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value == current.StatingNo).Count() > 0;
        //      //匹配走正常流程
        //      if (currentHangerFlowIsMatching)
        //      {
        //          return current.FlowIndex;
        //      }
        //      var currentHangerFlowIsNoMatching = nonProductsStatingFlow.Where(f => !f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value == current.StatingNo).Count() > 0;
        //      //不匹配取最小的未生产的工序
        //      if (!currentHangerFlowIsNoMatching)
        //      {
        //          var nextFlowList = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        ////&& f.FlowIndex.Value > current.FlowIndex
        //&& ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //&& ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //|| ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        //).Select(f => f.FlowIndex);
        //          if (nextFlowList.Count() > 0)
        //          {
        //              return nextFlowList.Min().Value;
        //          }
        //      }
        //      //工序一致，站点不一致
        //      var isFlowEqualStatingNoEqual = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value != current.StatingNo).Count() > 0;
        //      if (isFlowEqualStatingNoEqual)
        //      {
        //          return current.FlowIndex;
        //      }
        //      //工序不一致，站点不一致
        //      var isFlowNonEqualStatingNoEqual = nonProductsStatingFlow.Where(f => !f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value != current.StatingNo).Count() == 0;
        //      if (isFlowEqualStatingNoEqual)
        //      {
        //          var nonAllocationOutStatingException = new NonAllocationOutStatingException(string.Format("衣架原分配站点与本站不匹配，不允许出站，请联系管理员处理！ 主轨:{0} 站点:{1} 衣架:{2}", mainTrackNo, statingNo, hangerNo));
        //          tcpLogError.Error(nonAllocationOutStatingException);
        //          throw nonAllocationOutStatingException;
        //      }

        //      var currentHangerProductFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo+"");//dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

        //      return currentHangerProductFlow.FlowIndex;
        //  }

        /// <summary>
        /// 是否是未分配出战
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <returns></returns>
        internal bool IsNonAllocationOutSite(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
            lock (lockObjct)
            {
                // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                var hangerProuctList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo + ""); //dicHangerProcessFlowChart[tenHangerNo.ToString()];
                                                                                                                  // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo + ""); //dicCurrentHangerProductingFlowModelCache[tenHangerNo.ToString()];
                var isEqualFlowStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.MainTrackNumber.Value == current.MainTrackNumber && f.StatingNo != null && f.StatingNo.Value != current.StatingNo).Count() > 0;
                if (!isEqualFlowStating)
                {
                    var isBridgeStating = CANProductsService.Instance.IsBridge(tenMaintracknumber, tenStatingNo);
                    if (!isBridgeStating)
                    {
                        //var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                        var hangerStatingAlllocationItemList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo + "");
                        var hangerAllocation = hangerStatingAlllocationItemList.Where(f => f.Status == (byte)HangerStatingAllocationItemStatus.Allocationed.Value && f.NextSiteNo != null).OrderByDescending(f => f.AllocatingStatingDate).First();

                        if (tenStatingNo != int.Parse(hangerAllocation.NextSiteNo))
                        {
                            var info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】站点未分配衣架!", tenMaintracknumber, tenStatingNo, tenHangerNo);
                            tcpLogInfo.Info(info);
                            //出站
                            CANTcpServer.server.AutoHangerOutStating(HexHelper.TenToHexString2Len(tenMaintracknumber), HexHelper.TenToHexString2Len(tenStatingNo), true, tenHangerNo);

                            var sucessOutSiteMessage = string.Format("【异常衣架】 {0}-->【出站指令发送成功！】 主轨:{1} 本站:{2} 衣架:{3}", info, tenMaintracknumber, tenStatingNo, tenHangerNo);
                            //if (!isServiceStart)
                            //    lbMessage.Invoke(new EventHandler(this.AddMessage), sucessOutSiteMessage, null);
                            tcpLogInfo.Info(sucessOutSiteMessage);
                            if (ToolsBase.isUnitTest)
                            {
                                Thread.CurrentThread.Join(10000);
                                Environment.Exit(Environment.ExitCode);
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 工序移动或者站点移动
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public bool IsFlowMoveAndStatingMove(string mainTrackNo, string statingNo, string hangerNo)
        {
            //var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var onlineHangerProcessFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
                                                                                            //   var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo);//dicCurrentHangerProductingFlowModelCache[hangerNo];
            var onlineDicProducts = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.ProductsModel>(SusRedisConst.ON_LINE_PRODUCTS);

            if (onlineDicProducts.Keys.Count == 0)
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var onProduct = onlineDicProducts.First().Value;

            //桥接站兼容有问题
            var dicHangingPieceMainTrackNumber = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, string>(SusRedisConst.HANGER_HANGING_PIECE_MAINTRACK_NUMBER);
            if (!dicHangingPieceMainTrackNumber.ContainsKey(hangerNo))
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var hangerHangingPieceStatingMaintrackNumber = dicHangingPieceMainTrackNumber[hangerNo];

            var onlinepKey = string.Format("{0}:{1}", hangerHangingPieceStatingMaintrackNumber, onProduct.ProductionNumber.Value);
            if (!onlineHangerProcessFlowChartCache.ContainsKey(onlinepKey))
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var onlineHangerProcessFlowChart = onlineHangerProcessFlowChartCache[onlinepKey];
            //工序已经由别的站点生产完成
            var isStatingExist = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo) && f.StatingNo.Value == current.StatingNo
            && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            var isStatingExist2 = onlineHangerProcessFlowChart.Where(f => f.StatingNo != null && int.Parse(f.StatingNo) == int.Parse(statingNo) && int.Parse(f.StatingNo) == current.StatingNo).Count() > 0;
            var isFlowExist = fcList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            var isChangeFlowIndex = fcList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
             && f.FlowIndex.Value != current.FlowIndex
            ).Count() > 0;
            var flowNonSuccess = current.FlowIndex != 1;
            var currentHangerStatingFlowIsChange = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo) && f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
            ).Count() == 0;
            if ((isStatingExist || isStatingExist2) && isFlowExist && currentHangerStatingFlowIsChange && flowNonSuccess)
            {
                //var oldFlowIndex = fcList.Where(f => f.FlowNo.Equals(current.FlowNo)).Select(k => k.FlowIndex.Value).First();
                //if (oldFlowIndex < current.FlowIndex)
                //{

                //}
                var flowIndex = 0;
                var nextFlowList = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
      && f.FlowIndex.Value != 1
      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
      && ((fcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
      || ((fcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
      ).Select(f => f.FlowIndex);
                if (nextFlowList.Count() > 0)
                {
                    flowIndex = nextFlowList.Min().Value;
                }
                var fsEx = new FlowMoveAndStatingMoveException("工序及站点同时移动");
                fsEx.StatingNo = int.Parse(statingNo);
                fsEx.HangerNo = int.Parse(hangerNo);
                fsEx.FlowNo = current.FlowNo;
                fsEx.NextFlowIndex = flowIndex;
                tcpLogError.Error(fsEx);
                //throw fsEx;
                return true;
                //return true;
            }
            return false;
        }

        /// <summary>
        /// 工序删除或者站点删除
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="fdEx"></param>
        /// <returns></returns>
        public bool FlowDeleteOrStatingDelete(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, ref FlowDeleteOrStatingDeletedException fdEx)
        {
            var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(tenMainTrackNo, tenStatingNo.ToString());
            if (isBridgeOutSite) return false;


            //当前衣架工序及站点
            var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo);//dicCurrentHangerProductingFlowModelCache[tenHangerNo.ToString()];
            var currentIndex = current.FlowIndex;
            // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            //检查站点是否被删除
            var hangerProuctList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);//dicHangerProcessFlowChart[tenHangerNo];

            //当前衣架的工序站点是否在工艺图
            var nonProductsStatingFlow = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
  && f.StatingNo.Value == current.StatingNo && f.MainTrackNumber.Value == current.MainTrackNumber
  && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
  && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
  || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))));
            
            //当前衣架的站点不存在
            if (nonProductsStatingFlow.Count() == 0)
            {

                fdEx = new FlowDeleteOrStatingDeletedException();
                fdEx.MainTrackNumber = int.Parse(tenMainTrackNo);
                fdEx.StatingNo = int.Parse(tenStatingNo);
                fdEx.NextFlowIndex = current.FlowIndex;
                fdEx.FlowNo = current.FlowNo;
                fdEx.HangerNo = int.Parse(tenHangerNo);
                var eqFlowStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != -1).Select(k => k.StatingNo).Distinct();
                var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo && !f.IsFlowSucess.Value).Count() > 0;
                fdEx.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;

                return true;
                //  throw ex;
            }
            var currentHangerFlowisExist = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
            //当前站点存在且当前工序不存在
            if (!currentHangerFlowisExist)
            {
                fdEx = new FlowDeleteOrStatingDeletedException();
                fdEx.MainTrackNumber = int.Parse(tenMainTrackNo);
                fdEx.StatingNo = int.Parse(tenStatingNo);
                fdEx.NextFlowIndex = current.FlowIndex;
                fdEx.HangerNo = int.Parse(tenHangerNo);
                fdEx.FlowNo = current.FlowNo;
                var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo).Count() > 0;
                fdEx.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
                return true;
            }

            return false;
        }
        public bool FlowIsSuccess(int hangerNo, List<HangerProductFlowChartModel> hangerProuctList, string mainTrackNo, string statingNo)
        {
            // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");//dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

            //当前站点衣架未生产的工序
            //var nonProductsStatingFlow = hangerProuctList.Where(f => f.StatingNo != null
            //&& f.StatingNo.Value != -1
            //&& f.StatingNo.Value != current.StatingNo && f.MainTrackNumber.Value != current.MainTrackNumber && f.FlowIndex.Value != current.FlowIndex
            //&& (
            //    (null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
            //&& (
            //    (hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
            //        ||
            //     (hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)

            //    )
            //    ).Count() == 0;

            foreach (var f in hangerProuctList)
            {
                if (f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value != current.StatingNo && !f.FlowNo.Equals(current.FlowNo)
                     && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                    && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                    && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward))
                {
                    var commomFlow = hangerProuctList.Where(ff => (null != ff.IsReceivingHanger && ff.IsReceivingHanger.Value == 1)
                    && (null != ff.IsReceivingHangerStating && ff.IsReceivingHangerStating.Value) && ff.FlowIndex.Value != 1 && ff.StatingNo.Value != -1 && ff.FlowNo == f.FlowNo && !f.FlowNo.Equals(current.FlowNo) && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0;
                    var noRework = hangerProuctList.Where(ff =>
                      (null != ff.IsReceivingHanger && ff.IsReceivingHanger.Value == 1)
                    && (null != ff.IsReceivingHangerStating && ff.IsReceivingHangerStating.Value)
                   && ff.FlowIndex.Value != 1 && ff.StatingNo.Value != -1 && ff.FlowNo == f.FlowNo && !f.FlowNo.Equals(current.FlowNo) && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0;
                    if (noRework)
                    {
                        return false;
                    }
                    if (commomFlow) return false;

                }
            }
            return true;
            //var nn = hangerProuctList.Where(f => f.StatingNo != null 
            //&& f.StatingNo.Value != -1 
            //&& f.StatingNo.Value != current.StatingNo 
            //&& f.MainTrackNumber.Value != current.MainTrackNumber 
            //&& f.FlowIndex.Value != current.FlowIndex 
            //&& ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));
            //var nonProductsStatingFlow = nn.Where(nn.Where(ff => ff.FlowIndex.Value == ff.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0) ||
            //   nn.Where(ff => ff.FlowIndex.Value == ff.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0 ));
            //if (nonProductsStatingFlow)
            //{
            //    return true;
            //}
            //return false;
        }
    }
}

using Newtonsoft.Json;
using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MonitorUpload
{
    public class ReworkHangerMontorService : SusLog
    {
        public static readonly ReworkHangerMontorService Instance = new ReworkHangerMontorService();
        public void MonitoringReworkHangerHandler(List<HangerProductFlowChartModel> fChartList, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current,
    MainTrackStatingMontorModel mainTrackStatingMontor)
        {
            // // return; MonitorBridgeService.Instance.Process(mainTrackStatingMontor, outMainTrackNumber, nextStatingNo);
            //var logMessage = string.Format($"【监测点日志】衣架号:{mainTrackStatingMontor?.HangerNo} 主轨:{ mainTrackStatingMontor?.MainTrackNumber} 站点:{ mainTrackStatingMontor?.StatingNo} 监测-->桥接完成!");
            // montorLog.Info(logMessage);
            var hangerNo = mainTrackStatingMontor.HangerNo;// current.HangerNo;
            var mainTrackNumber = current.MainTrackNumber;
            //是否是未过桥接站而下位机丢失分配缓存等信息
            //var dicBridgeReowrkNextStatingCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM);
            //if (dicBridgeReowrkNextStatingCache.ContainsKey(hangerNo))
            //{
            //    var reworkModel = dicBridgeReowrkNextStatingCache[hangerNo];
            //    int outMainTrackNumber = 0;
            //    string nextStatingNo = null;
            //    //桥接站需要再分配
            //    OutSiteService.Instance.AllocationNextProcessFlowStating(reworkModel.NextStatingList, ref outMainTrackNumber, ref nextStatingNo);
            //    var mMessage = string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接站分配计算结束】 主轨:{0} 监测站点:{1} 下一站主轨{1}:站点【{2}】 ",
            //            monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo);
            //    tcpLogInfo.InfoFormat(mMessage);
            //    if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
            //    {
            //        var ex = new FlowNotFoundException(string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接站分配计算异常】 主轨:{0} 监测站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ",
            //            monitoringMainTrackNumber, monitoringStatingNo, current.MainTrackNumber, current.StatingNo));
            //        tcpLogError.Error(ex);
            //        throw ex;
            //    }
            //    //需要桥接
            //    if (mainTrackNumber != outMainTrackNumber)
            //    {
            //        MonitorStatingReworkHangerBridgeHandler(current, monitoringMainTrackNumber, monitoringStatingNo, hangerNo, mainTrackNumber, reworkModel, outMainTrackNumber, nextStatingNo);
            //        //var ex = new ApplicationException(string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->衣架返工工序及疵点代码】 主轨:{0} 监测站点:{1} 桥接处理异常! 下一站主轨:{2} 站点:{3} ", monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo));
            //        //tcpLogError.Error(ex);
            //        // throw ex;
            //        return;
            //    }

            //    return;
            //}
            var flowIndex = MonitorUploadService.Instance.GetNonProductsProcessFlowChartListByReworkFlowIndex(fChartList, hangerNo);
            // var reworkResultHangerProductsFlowChartList= fChartList
            var nextStatingList = fChartList.Where(k => k.FlowIndex.Value == flowIndex && k.StatingNo != null
           && k.StatingNo.Value != -1 && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
           && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)).Select(f => new ProductsProcessOrderModel()
           {
               StatingNo = f.StatingNo.ToString(),
               MainTrackNumber = (int)f.MainTrackNumber,
               StatingCapacity = f.StatingCapacity.Value,
               Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
               ProcessChartId = f.ProcessChartId,
               FlowNo = f.FlowNo,
               StatingRoleCode = f.StatingRoleCode//,
                                                  //FlowIndex = flowIndex,
                                                  //ReworkFlowNoList = strFlowNoList2

           }).ToList<ProductsProcessOrderModel>();
            if (nextStatingList.Count() == 0)
            {
                var logMessage = string.Format($"【监测点日志】衣架号:{mainTrackStatingMontor.HangerNo} 主轨:{mainTrackStatingMontor.MainTrackNumber} 站点:{mainTrackStatingMontor.StatingNo} 监测-->衣架找不到返工的下一站点!");
                montorLog.Info(logMessage);
                return;
            }
            //已过监测点下位机断电
            MonitorStatingReworkHangerNonRequireBridgeHandler(nextStatingList, fChartList, current,
             mainTrackStatingMontor);
        }
        /// <summary>
        ///未过桥接站而下位机丢失分配缓存等信息,监测点对返工衣架的处理
        /// </summary>
        /// <param name="current"></param>
        /// <param name="monitoringMainTrackNumber"></param>
        /// <param name="monitoringStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="mainTrackNumber"></param>
        /// <param name="reworkModel"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        public void MonitorStatingReworkHangerBridgeHandler(Domain.Cus.CurrentHangerProductingFlowModel current, int monitoringMainTrackNumber, string monitoringStatingNo, string hangerNo, int mainTrackNumber, ReworkModel reworkModel, int outMainTrackNumber, string nextStatingNo)
        {
            string bridgeType = string.Empty;
            SuspeSys.Domain.BridgeSet bridge = null;
            var sourceRewokFlowChart = reworkModel.SourceRewokFlowChart;
            //var nStatingNo = int.Parse(nextStatingNo);
            bool isBridge = CANProductsService.Instance.IsBridgeByOutSiteRequestAction(mainTrackNumber, current.StatingNo, outMainTrackNumber, nextStatingNo, int.Parse(hangerNo)
                , ref bridgeType, ref bridge);
            if (isBridge)
            {
                if (bridgeType == BridgeType.Bridge_Stating_Non_Flow_Chart_ALL || bridgeType == BridgeType.Bridge_Stating_One_In_Flow_Chart
                    || bridgeType == BridgeType.Bridge_Stating_IN_Flow_Chart_ALL)
                {
                    var tenBridgeMaintrackNumber = bridge.AMainTrackNumber.Value.ToString();
                    var tenBridgeStatingNo = bridge.ASiteNo.Value;
                    var aMainTracknumberBridgeStatingIsInFlowChart = false;

                    //处理分配关系
                    CANProductsService.Instance.BridgeAllocationRelation(mainTrackNumber, int.Parse(monitoringStatingNo), outMainTrackNumber, nextStatingNo, int.Parse(hangerNo),
                        tenBridgeStatingNo.ToString(), aMainTracknumberBridgeStatingIsInFlowChart, true);
                    var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
                    //给下一站分配衣架
                    CANTcpServer.server.AllocationHangerToNextStating(tenBridgeMaintrackNumber, tenBridgeStatingNo.ToString(), hexHangerNo);

                    var susAllocatingMessage = string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenBridgeMaintrackNumber, bridge.ASiteNo.Value);
                    var info = susAllocatingMessage;
                    tcpLogInfo.Info(susAllocatingMessage);

                    //更新返工衣架工艺图到缓存
                    //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = sourceRewok;
                    //清除桥接站出战缓存
                    SusCacheBootstarp.Instance.ClearBridgeStatingHangerOutSiteItem(hangerNo.ToString());
                    //清除桥接站进站缓存
                    SusCacheBootstarp.Instance.ClearBridgeStatingHangerInSiteItem(hangerNo.ToString());


                    return;
                }
                var exBridgeApp = new ApplicationException(string.Format("【监测点-->衣架返工-->衣架返工工序及疵点代码】 主轨:{0} 监测站点:{1}桥接类型未找到! 下一站主轨:{2} 站点:{3}", monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo));
                tcpLogError.Error(exBridgeApp);
                //throw exBridgeApp;

            }
        }

        /// <summary>
        /// 返工衣架已过桥接站，下位机断电
        /// </summary>
        /// <param name="fChartList"></param>
        /// <param name="current"></param>
        /// <param name="monitoringMainTrackNumber"></param>
        /// <param name="monitoringStatingNo"></param>

        public void MonitorStatingReworkHangerNonRequireBridgeHandler(IList<ProductsProcessOrderModel> nextStatingList, List<HangerProductFlowChartModel> fChartList, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current,
            MainTrackStatingMontorModel mainTrackStatingMontor)
        {
            // var statingList = fChartList.Where(k => k.FlowIndex.Value == current.FlowIndex && k.StatingNo != null
            // && k.StatingNo.Value == current.StatingNo
            //&& k.StatingNo.Value != -1 && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
            //&& (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)).Select(f => new ProductsProcessOrderModel()
            //{
            //    StatingNo = f.StatingNo.ToString(),
            //    MainTrackNumber = (int)f.MainTrackNumber,
            //    StatingCapacity = f.StatingCapacity.Value,
            //    Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
            //    ProcessChartId = f.ProcessChartId,
            //    FlowNo = f.FlowNo,
            //    StatingRoleCode = f.StatingRoleCode
            //}).ToList<ProductsProcessOrderModel>();

            #region 清除桥接点已分配缓存及在线数，站内数/非桥接站内数等等
            //清除已分配桥接缓存及站内数
            // var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (dicHangerStatingInfo.ContainsKey(mainTrackStatingMontor.HangerNo))
            {
                var fIndexCacheList = dicHangerStatingInfo[mainTrackStatingMontor.HangerNo];
                var dicMontorBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (dicMontorBridgeCache.ContainsKey(mainTrackStatingMontor.MainTrackNumber))
                {
                    #region 桥接部分
                    var montorBridge = dicMontorBridgeCache[mainTrackStatingMontor.MainTrackNumber];
                    tcpLogInfo.InfoFormat("【监测点日志-->站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  桥接站站内数处理!", mainTrackStatingMontor.HangerNo, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo);

                    var fIndexBridgeList = fIndexCacheList.Where(f => f.MainTrackNumber.Value == montorBridge.AMainTrackNumber.Value && f.StatingNo.Value == montorBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1));
                    //清除已分配下位机缓存
                    foreach (var item in fIndexBridgeList)
                    {
                        var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        if (item.HangerStatus == 0)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, AllocationNum = -1 };
                            //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        if (item.HangerStatus == 1)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, OnLineSum = -1 };
                            //   NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                    }
                    var fIndexResuBridgeList = fIndexCacheList.Where(f => f.MainTrackNumber.Value == montorBridge.BMainTrackNumber.Value && f.StatingNo.Value == montorBridge.BSiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1));
                    //清除已分配下位机缓存
                    foreach (var item in fIndexResuBridgeList)
                    {
                        var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        if (item.HangerStatus == 0)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, AllocationNum = -1 };
                            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        if (item.HangerStatus == 1)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                    }
                    var resvBridge = dicMontorBridgeCache[montorBridge.BMainTrackNumber.Value];
                    fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == montorBridge.AMainTrackNumber && f.StatingNo.Value == montorBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1));
                    fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == resvBridge.AMainTrackNumber.Value && f.StatingNo.Value == resvBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1));
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[mainTrackStatingMontor.HangerNo] = fIndexCacheList;
                    #endregion
                }
                
                #region 非桥接部分
                var fIndexCommonList = fIndexCacheList.Where(f => f.MainTrackNumber.Value == current.MainTrackNumber && f.StatingNo.Value == current.StatingNo && (f.HangerStatus == 0 || f.HangerStatus == 1));
                //清除已分配下位机缓存
                foreach (var item in fIndexCommonList)
                {
                    var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                    montorLog.Info(logMessage);
                    CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                    montorLog.Info(logMessage);
                    if (item.HangerStatus == 0)
                    {
                        //站点分配数-1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, AllocationNum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    }
                    if (item.HangerStatus == 1)
                    {
                        //站内数-1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, OnLineSum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    }
                }

                fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == current.MainTrackNumber && f.StatingNo.Value == current.StatingNo && (f.HangerStatus == 0 || f.HangerStatus == 1));
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[mainTrackStatingMontor.HangerNo] = fIndexCacheList;
                #endregion

                // return;
            }
            #endregion
            var nextFlow = nextStatingList.First();
            var reworkFlow = fChartList.Where(f => f.FlowNo.Equals(nextStatingList.First().FlowNo) && f.Status.Value!=HangerProductFlowChartStaus.Successed.Value).First();
            int outMainTrackNumber = 0;
            string nextStatingNo = null;
            //桥接站需要再分配
            var flowIndex = nextFlow.FlowIndex;
            var statingNo = nextFlow.StatingNo;
            var hangerNo = current.HangerNo;
            var mainTrackNumber = nextFlow.MainTrackNumber;

            OutSiteService.Instance.AllocationNextProcessFlowStating(nextStatingList.ToList(), ref outMainTrackNumber, ref nextStatingNo);
            if (mainTrackStatingMontor.MainTrackNumber != outMainTrackNumber)
            {
                MonitorBridgeService.Instance.Process(mainTrackStatingMontor, outMainTrackNumber, nextStatingNo);
                var logMessage = string.Format($"【监测点日志】衣架号:{mainTrackStatingMontor?.HangerNo} 主轨:{ mainTrackStatingMontor?.MainTrackNumber} 站点:{ mainTrackStatingMontor?.StatingNo} 监测-->桥接完成!");
                montorLog.Info(logMessage);
                return;
            }
            var mMessage = string.Format("【监测点-->返工衣架处理-->下一站分配计算完成】 主轨:{0} 监测站点:{1} 下一站主轨{2}:站点【{3}】 ",
                        mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, outMainTrackNumber, nextStatingNo);
            tcpLogInfo.InfoFormat(mMessage);
            if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
            {
                var ex = new FlowNotFoundException(string.Format("【监测点-->返工衣架处理-->桥接站分配计算异常】 主轨:{0} 监测站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ",
                    mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, current.MainTrackNumber, current.StatingNo));
                tcpLogError.Error(ex);
                throw ex;
            }
            var dicHangerAllocationListList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo];
            var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
            nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
            nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
            nextHangerStatingAllocationItem.SiteNo = statingNo.ToString();
            nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
            nextHangerStatingAllocationItem.HangerNo = hangerNo.ToString();
            nextHangerStatingAllocationItem.HangerType = 1;
            nextHangerStatingAllocationItem.IsReworkSourceStating = true;
            nextHangerStatingAllocationItem.IsReturnWorkFlow = true;
            nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
            nextHangerStatingAllocationItem.OutMainTrackNumber = mainTrackNumber;
            nextHangerStatingAllocationItem.FlowNo = reworkFlow.FlowNo;
            nextHangerStatingAllocationItem.ProcessFlowCode = reworkFlow.FlowCode;
            nextHangerStatingAllocationItem.ProcessFlowName = reworkFlow.FlowName;
            nextHangerStatingAllocationItem.ProcessFlowId = reworkFlow.FlowId;
            nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
            nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
            nextHangerStatingAllocationItem.LastFlowIndex = -1;
            nextHangerStatingAllocationItem.isMonitoringAllocation = true;
            nextHangerStatingAllocationItem.IsStatingStorageOutStating = CANProductsService.Instance.IsStoreStating(reworkFlow);

            dicHangerAllocationListList.Add(nextHangerStatingAllocationItem);
            //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()] = dicHangerAllocationListList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo,dicHangerAllocationListList);

              //发布 待生产衣架信息
              var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            var tenMainTrackNumber = outMainTrackNumber;//HexHelper.TenToHexString2Len(outMainTrackNumber);
            CANTcpServer.server.AllocationHangerToNextStating(tenMainTrackNumber.ToString(), nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), null);
            var susAllocatingMessage = string.Format("【监测点-->返工衣架处理-->主轨:{0} 监测站点:{1} 衣架往主轨【{2}】 站点【{3}】 分配指令已发送成功!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, tenMainTrackNumber, nextStatingNo);
            tcpLogInfo.Info(susAllocatingMessage);

            //修正删除的站内数及明细、缓存
            var hnsAllocationUpdate = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnsAllocationUpdate.Action = 0;
            hnsAllocationUpdate.HangerNo = hangerNo;
            hnsAllocationUpdate.MainTrackNumber = outMainTrackNumber;
            hnsAllocationUpdate.StatingNo = int.Parse(nextStatingNo);
            hnsAllocationUpdate.FlowNo = reworkFlow.FlowNo;
            hnsAllocationUpdate.FlowIndex = reworkFlow.FlowIndex.Value;
            hnsAllocationUpdate.HangerProductFlowChartModel = reworkFlow;
            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnsAllocationUpdate);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );

            //【衣架生产履历】下一站分配Cache写入
            //reworkFlow.IsHangerSucess = false;
            //reworkFlow.IsFlowSucess = false;
            var nextStatingHPResume = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo,
                StatingNo = nextStatingNo,
                MainTrackNumber = outMainTrackNumber,
                HangerProductFlowChart = reworkFlow,
                Action = 1,
                NextStatingNo = nextStatingNo
            };
            var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
            //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
        }
    }
}

using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.DataTypes;
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
    public class MontorHandler : SusLog
    {
        public static readonly MontorHandler Instance = new MontorHandler();
        public void Process(MainTrackStatingMontorModel mainTrackStatingMontor)
        {
            var logMessage = string.Empty;
            //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(mainTrackStatingMontor.HangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(mainTrackStatingMontor.HangerNo))
            {
                //衣架衣架生产完成!
                tcpLogError.ErrorFormat("【监测点日志】衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, mainTrackStatingMontor.HangerNo);
                return;
            }
            var current = NewCacheService.Instance.GetHangerCurrentFlow(mainTrackStatingMontor.HangerNo); //dicCurrentHangerProductingFlowModelCache[mainTrackStatingMontor.HangerNo];

            //1.找出生产到第几道工序，然后计算下一站点,给下一站点发送分配命令
            var hangerNo = mainTrackStatingMontor.HangerNo.ToString();
            //var vcHFCList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//vcHFCList.ContainsKey(hangerNo))
            {
                var ex = new ApplicationException(string.Format("主轨:{0} 衣架:{1} 未找到工艺图!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.HangerNo));
                montorLog.Error(ex);
                return;
            }

            var fChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //vcHFCList[hangerNo];
            if (NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo + ""))
            {
                var currentHanger = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");

                if (currentHanger.IsF2Assgn)
                {
                    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->F2指定衣架不处理!",
                        mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                    montorLog.Info(logMessage);
                    return;
                }
            }
            //工序是否完成
            var flowIsSuccess = MonitorUploadService.Instance.CheckFlowIsSuccess(mainTrackStatingMontor.MainTrackNumber, hangerNo, fChartList);
            if (flowIsSuccess)
            {
                tcpLogError.ErrorFormat("【监测点日志】衣架已生产完成!衣架:{0} 主轨:{1} 站点：{2}", mainTrackStatingMontor.HangerNo, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo);
                //NewCacheService.Instance.GetHangingPieceStatingCache
                //工序已完成过监测点再分配到挂片站
                HaingPeiceNonIncomeClearAollcationHandler(mainTrackStatingMontor);
                HaingPeiceNonIncomeReAollcationHandler(mainTrackStatingMontor);
                return;
            }
            #region 返工重新分配
            var isExistReworkNoSucess = fChartList.Where(f => f.FlowType.Value == 1
              && f.MainTrackNumber.Value == mainTrackStatingMontor.MainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1
              && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            if (isExistReworkNoSucess)
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->返工衣架处理开始!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                ReworkHangerMontorService.Instance.MonitoringReworkHangerHandler(fChartList, current, mainTrackStatingMontor);
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->返工衣架处理结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                return;
            }
            #endregion


            if (!flowIsSuccess)
            {
                #region 存储站出战经过监测点处理
                HangerStatingAllocationItem allocationItem = null;
                var isStorageStatingOutSiteTag = MonitorUploadService.Instance.IsStorageStatingOutSite(mainTrackStatingMontor.MainTrackNumber, hangerNo, fChartList, ref allocationItem);
                if (isStorageStatingOutSiteTag)
                {
                    /*
                    ：如果存储站出战 
                    1.已分配车缝站且车缝站不满站就再分一次且清除之前的缓存；
                    2.已分配车缝站且车缝站满站就看其他车缝站是否满站，如果不满站就再分给其他车缝站，且清除之前分的缓存；
                    3.如果车缝站都满站就分存储站,且存储站不满站就清除之前的缓存。
                    4.如果所有站都满站，则提示满站异常
                    */

                    CommonHangerMontorService.Instance.StoreStatingOutSiteMontorHandler(fChartList, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo.ToString(), mainTrackStatingMontor.HangerNo, allocationItem);

                    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                    montorLog.Info(logMessage);
                    return;
                }
                #endregion
            }
            var nonProductFlowIndex = MonitorUploadService.Instance.GetNonProductsProcessFlowChartList(fChartList, hangerNo);//GetNonProductsProcessFlowChartList(fChartList, mainTrackStatingMontor);
            var fIndexList = fChartList.Where(f => f.FlowIndex.Value == nonProductFlowIndex && (
                    (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value && f.IncomeSiteDate == null) || (f.Status.Value == HangerProductFlowChartStaus.Producting.Value && f.OutSiteDate == null && f.IncomeSiteDate != null)
                ) && f.FlowIndex.Value != 1 &&
            null != f.StatingNo && f.StatingNo.Value != -1 && f.AllocationedDate != null);
            //var fChartAllocationedNonInStatingList = fIndexList.OrderBy(o => o.FlowIndex).Select(f => f.FlowIndex);
            if (nonProductFlowIndex == -1)
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->衣架已生产完成!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                return;
            }
            //if (fChartAllocationedNonInStatingList.Count() > 0)
            //{

            #region 【已分配数据修正】 清除已分配衣架站内明细/清除已分配缓存/站点分配数/衣架工艺图分配信息恢复

            //修正删除的站内数及明细、缓存
            var hnsAllocationStatingNumCacheUpdate = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnsAllocationStatingNumCacheUpdate.Action = 7;
            hnsAllocationStatingNumCacheUpdate.HangerNo = hangerNo;
            hnsAllocationStatingNumCacheUpdate.MainTrackNumber = current.MainTrackNumber;
            hnsAllocationStatingNumCacheUpdate.StatingNo = current.StatingNo;
            hnsAllocationStatingNumCacheUpdate.FlowNo = current.FlowNo;
            hnsAllocationStatingNumCacheUpdate.FlowIndex = current.FlowIndex;
            var hnsAllocationStatingNumCacheUpdateJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnsAllocationStatingNumCacheUpdate);
            // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnsAllocationStatingNumCacheUpdateJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new RedisChannel(), hnsAllocationStatingNumCacheUpdateJson);

            //清除桥接点已分配缓存及在线数，站内数
            #region 清除桥接点已分配缓存及在线数，站内数
            //清除已分配桥接缓存及站内数
            // var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (dicHangerStatingInfo.ContainsKey(mainTrackStatingMontor.HangerNo))
            {
                var dicMontorBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (dicMontorBridgeCache.ContainsKey(mainTrackStatingMontor.MainTrackNumber))
                {
                    var montorBridge = dicMontorBridgeCache[mainTrackStatingMontor.MainTrackNumber];
                    tcpLogInfo.InfoFormat("【监测点日志-->站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  桥接站站内数处理!", mainTrackStatingMontor.HangerNo, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo);
                    var fIndexCacheList = dicHangerStatingInfo[hangerNo];
                    var fIndexBridgeList = fIndexCacheList.Where(f => f.MainTrackNumber.Value == montorBridge.AMainTrackNumber.Value && f.StatingNo.Value == montorBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1) && f.FlowNo.Equals(""));
                    //清除已分配下位机缓存
                    foreach (var item in fIndexBridgeList)
                    {
                        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                        montorLog.Info(logMessage);
                        if (item.HangerStatus == 0)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, AllocationNum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
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
                    fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == montorBridge.AMainTrackNumber && f.StatingNo.Value == montorBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1) && f.FlowNo.Equals(""));
                    fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == resvBridge.AMainTrackNumber.Value && f.StatingNo.Value == resvBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1) && f.FlowNo.Equals(""));
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[mainTrackStatingMontor.HangerNo] = fIndexCacheList;
                }

                // return;
            }
            #endregion
            //清除已分配衣架站内明细
            List<HangerStatingAllocationItem> allocationedHangerList = null;
            // var dicHangerStatingALloListCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingALloListCache.ContainsKey(hangerNo))
            {
                allocationedHangerList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo);//dicHangerStatingALloListCache[hangerNo];
            }

            //清除已分配下位机缓存
            foreach (var item in fIndexList)
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                montorLog.Info(logMessage);
                CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                montorLog.Info(logMessage);
            }

            //更新衣架缓存分配信息
            foreach (var fc in fChartList)
            {
                if (fc.FlowIndex.Value == nonProductFlowIndex)
                {
                    fc.AllocationedDate = null;
                    fc.isAllocationed = false;
                    fc.IncomeSiteDate = null;
                    fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【生产序号:{3} 站点:{4}】 《衣架工艺图》-->衣架缓存分配信息及站内衣架数据!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, nonProductFlowIndex, fc.StatingNo);
                    montorLog.Info(logMessage);
                }
            }
            #endregion

            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->计算下一站开始:!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
            montorLog.Info(logMessage);

            var nonSucessCurrentFlowIndex = nonProductFlowIndex;//fChartAllocationedNonInStatingList.First();

            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->监测点重新分配工序生产顺序号:{3}!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, nonSucessCurrentFlowIndex);
            montorLog.Info(logMessage);

            var fChart = fChartList.Where(f => f.FlowIndex.Value == nonSucessCurrentFlowIndex).First();
            var nonSucessFlowStatingList = fChartList.Where(f => f.FlowIndex.Value == nonSucessCurrentFlowIndex
             && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
            && f.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                                                     .Select(f => new ProductsProcessOrderModel()
                                                     {
                                                         StatingNo = f.StatingNo.ToString(),
                                                         MainTrackNumber = (int)f.MainTrackNumber,
                                                         StatingCapacity = f.StatingCapacity.Value,
                                                         Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                                                         ProcessChartId = f.ProcessChartId,
                                                         FlowNo = f.FlowNo,
                                                         StatingRoleCode = f.StatingRoleCode
                                                     }).ToList<ProductsProcessOrderModel>();
            if (nonSucessFlowStatingList.Count == 0)
            {
                //下一道没有可以接收衣架的站
                var exx = new NoFoundStatingException(string.Format("【监测点】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, hangerNo))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == nonSucessCurrentFlowIndex)?.First()?.FlowNo
                };
                montorLog.Error(exx);
                return;
            }
            int outMainTrackNumber = 0;
            //var nextStatingNo = OutSiteService.Instance.CalcateStatingNo(nonSucessFlowStatingList, ref outMainTrackNumber, true);
            var nextStatingNo = string.Empty;
            OutSiteService.Instance.AllocationNextProcessFlowStating(nonSucessFlowStatingList, ref outMainTrackNumber, ref nextStatingNo, true);
            if (string.IsNullOrEmpty(nextStatingNo))
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->衣架已生产完成!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                return;
            }
            var isBridgeRequire = false;
            if (outMainTrackNumber != 0 && outMainTrackNumber != mainTrackStatingMontor.MainTrackNumber)
            {
                logMessage = string.Format($"【监测点日志】衣架号:{mainTrackStatingMontor?.HangerNo} 主轨:{ mainTrackStatingMontor?.MainTrackNumber} 站点:{ mainTrackStatingMontor?.StatingNo} 监测-->桥接处理..!");
                montorLog.Info(logMessage);
                isBridgeRequire = true;
            }
            if (isBridgeRequire)
            {
                MonitorBridgeService.Instance.Process(mainTrackStatingMontor, outMainTrackNumber, nextStatingNo);
                logMessage = string.Format($"【监测点日志】衣架号:{mainTrackStatingMontor?.HangerNo} 主轨:{ mainTrackStatingMontor?.MainTrackNumber} 站点:{ mainTrackStatingMontor?.StatingNo} 监测-->桥接完成!");
                montorLog.Info(logMessage);
                return;
            }
            
            //记录衣架分配

            if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingALloListCache.ContainsKey(hangerNo))
            {
                var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingALloListCache[hangerNo];
                var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
                nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                nextHangerStatingAllocationItem.FlowIndex = (short)nonSucessCurrentFlowIndex;
                nextHangerStatingAllocationItem.SiteNo = null;
                nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                nextHangerStatingAllocationItem.HangerNo = hangerNo;
                nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
                nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
                nextHangerStatingAllocationItem.FlowNo = fChart?.FlowNo;
                nextHangerStatingAllocationItem.FlowChartd = fChart?.ProcessChartId;
                nextHangerStatingAllocationItem.ProductsId = fChart?.ProductsId;
                nextHangerStatingAllocationItem.ProcessFlowCode = fChart?.FlowCode;
                nextHangerStatingAllocationItem.ProcessFlowName = fChart?.FlowName;
                nextHangerStatingAllocationItem.ProcessFlowId = fChart?.FlowId;
                nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
                nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
                nextHangerStatingAllocationItem.Memo = "监测点衣架重新分配";
                nextHangerStatingAllocationItem.isMonitoringAllocation = true;
                nextHangerStatingAllocationItem.LastFlowIndex = nonSucessCurrentFlowIndex;
                nextHangerStatingAllocationItem.IsStatingStorageOutStating = CANProductsService.Instance.IsStoreStating(fChart);

                dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
                //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);

                var tenMainTrackNumber = outMainTrackNumber.ToString();
                var susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送开始!", tenMainTrackNumber, nextStatingNo);
                tcpLogInfo.Info(susAllocatingMessage);
                //CANTcp.client.AllocationHangerToNextStating(hexOutID, HexHelper.TenToHexString2Len(nextStatingNo), HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);
                CANTcpServer.server.AllocationHangerToNextStating(tenMainTrackNumber, nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);
                susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenMainTrackNumber, nextStatingNo);
                montorLog.Info(susAllocatingMessage);


                //记录衣架分配
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);

                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                //再次分配修正工艺图分配日期和状态
                //更新衣架缓存分配信息
                foreach (var fc in fChartList)
                {
                    if (fc.FlowIndex.Value == nonProductFlowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(nextStatingNo))
                    {
                        fc.AllocationedDate = DateTime.Now;
                        fc.isAllocationed = true;
                        break;
                    }
                }
                //发布衣架状态
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo;
                chpf.MainTrackNumber = outMainTrackNumber;
                chpf.StatingNo = int.Parse(string.IsNullOrEmpty(nextStatingNo) ? "-1" : nextStatingNo);
                chpf.FlowNo = fChart?.FlowNo;
                chpf.FlowIndex = nonProductFlowIndex;
                chpf.FlowType = null == fChart?.FlowType ? 0 : fChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

                //修正删除的站内数及明细、缓存
                var hnsAllocationUpdate = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnsAllocationUpdate.Action = 0;
                hnsAllocationUpdate.HangerNo = hangerNo;
                hnsAllocationUpdate.MainTrackNumber = outMainTrackNumber;
                hnsAllocationUpdate.StatingNo = int.Parse(nextStatingNo);
                hnsAllocationUpdate.FlowNo = fChart.FlowNo;
                hnsAllocationUpdate.FlowIndex = fChart.FlowIndex.Value;
                hnsAllocationUpdate.HangerProductFlowChartModel = fChart;
                var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnsAllocationUpdate);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
                //HangerStatingOrAllocationAction(new RedisChannel(), hnssocDeleteStatingJson);

                //【衣架生产履历】下一站分配Cache写入
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = nextStatingNo,
                    MainTrackNumber = outMainTrackNumber,
                    HangerProductFlowChart = fChart,
                    Action = 1,
                    NextStatingNo = nextStatingNo
                };
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
            }

            //更新衣架工艺图
            //   NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = fChartList;
            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, fChartList);
        }

        private void HaingPeiceNonIncomeClearAollcationHandler(MainTrackStatingMontorModel mainTrackStatingMontor)
        {
            var hangingPieceCaceh = NewCacheService.Instance.GetHangingPieceStatingCache(mainTrackStatingMontor.MainTrackNumber);
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = -4;
            hnssoc.HangerNo = mainTrackStatingMontor.HangerNo;
            hnssoc.MainTrackNumber = mainTrackStatingMontor.MainTrackNumber;
            hnssoc.StatingNo =int.Parse(hangingPieceCaceh.StatingNo);
            hnssoc.FlowNo = string.Empty;
           // hnssoc.FlowIndex = -2;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);

            //清除已分缓存
            LowerPlaceInstr.Instance.ClearHangerCache(int.Parse(mainTrackStatingMontor.HangerNo), mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo);

        }
        private void HaingPeiceNonIncomeReAollcationHandler(MainTrackStatingMontorModel mainTrackStatingMontor)
        {
            var hangingPieceCaceh = NewCacheService.Instance.GetHangingPieceStatingCache(mainTrackStatingMontor.MainTrackNumber);
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = -2;
            hnssoc.HangerNo = mainTrackStatingMontor.HangerNo;
            hnssoc.MainTrackNumber = mainTrackStatingMontor.MainTrackNumber;
            hnssoc.StatingNo = int.Parse(hangingPieceCaceh.StatingNo);
            hnssoc.FlowNo = string.Empty;
            hnssoc.FlowIndex = -2;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);

            var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            {
                HangerNo = mainTrackStatingMontor.HangerNo + "",
                MainTrackNumber = (short)mainTrackStatingMontor.MainTrackNumber,
                SiteNo = hangingPieceCaceh.StatingNo + ""
                 ,
                AllocatingStatingDate = DateTime.Now
            });
            //分配入库
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

            //给下一站分配衣架
            LowerPlaceInstr.Instance.AllocationHangerToNextStating(mainTrackStatingMontor.HangerNo, hangingPieceCaceh.StatingNo + "", mainTrackStatingMontor.MainTrackNumber);
        }
    }
}

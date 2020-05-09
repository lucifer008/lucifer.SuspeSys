using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Utils;
using SuspeSys.Domain.Ext;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using Suspe.CAN.Action.CAN;
using Sus.Net.Common.Constant;
using Newtonsoft.Json;
using SuspeSys.Service.Impl.Core.Cache;

namespace SuspeSys.Service.Impl.Core.MonitorUpload
{
    public class MonitorBridgeService : SusLog
    {
        public static readonly MonitorBridgeService Instance = new MonitorBridgeService();


        internal void Process(MainTrackStatingMontorModel mainTrackStatingMontor, int outMainTrackNumber, string nextStatingNo)
        {
            var monitorHangerNo = mainTrackStatingMontor.HangerNo;
            var monitorMainTrackNumber = mainTrackStatingMontor.MainTrackNumber;
            var monitorStatingNo = mainTrackStatingMontor.StatingNo;
            var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (!dicHangerStatingInfo.ContainsKey(mainTrackStatingMontor.HangerNo))
            {
                montorLog.Error($"【监测点--->站内数及明细修正】 衣架:{monitorHangerNo} 主轨:{monitorMainTrackNumber} 站点:{monitorStatingNo}  无站内数不处理!");
                return;
            }
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(monitorMainTrackNumber))
            {
                var exNonFoundBridgeSet = new ApplicationException(string.Format($"【监测点--->桥接错误】。衣架:{monitorHangerNo} 主轨:{monitorMainTrackNumber} 站点:{monitorStatingNo}  无桥接配置!"));
                montorLog.Error(exNonFoundBridgeSet);
                // throw exNonFoundBridgeSet;
                return;
            }
            var bridegStating = dicBridgeCache[monitorMainTrackNumber];
            var tenBridgeMaintrackNumber = bridegStating.AMainTrackNumber.Value;
            var tenBridgeStatingNo = bridegStating.ASiteNo.Value;
            //var dicHangerProcessFlowChartList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(monitorHangerNo))//dicHangerProcessFlowChartList.ContainsKey(monitorHangerNo))
            {
                var ex = new ApplicationException(string.Format("主轨:{0} 衣架:{1} 未找到工艺图!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.HangerNo));
                montorLog.Error(ex);
                return;
            }
           // var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(monitorHangerNo); //dicHangerProcessFlowChartList[monitorHangerNo.ToString()];
            var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(monitorHangerNo); //dicHangerStatingAllocationItem[monitorHangerNo.ToString()];

           // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(monitorHangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(monitorHangerNo))
            {
                //衣架衣架生产完成!
                tcpLogError.ErrorFormat("【监测点日志】衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, mainTrackStatingMontor.HangerNo);
                return;
            }
            var current = NewCacheService.Instance.GetHangerCurrentFlow(monitorHangerNo); //dicCurrentHangerProductingFlowModelCache[monitorHangerNo];
            var nextPPChartList = fcList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && (f.FlowType.Value == 0 || f.FlowType.Value == 1) && f.MainTrackNumber.Value == outMainTrackNumber && f.StatingNo.Value == int.Parse(nextStatingNo));
            if (0 == nextPPChartList.Count())
            {
                //衣架衣架生产完成!
                tcpLogError.ErrorFormat("【监测点日志】衣架找不到下一站!!衣架:{0} 主轨:{1} 站点：{2}", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, mainTrackStatingMontor.HangerNo);
                return;
            }
            //清除已分配桥接缓存及站内数
            // var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (dicHangerStatingInfo.ContainsKey(monitorHangerNo))
            {
                tcpLogInfo.InfoFormat("【监测点日志】【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  桥接站站内数处理!", monitorHangerNo, monitorMainTrackNumber, monitorStatingNo);
                var fIndexCacheList = dicHangerStatingInfo[monitorHangerNo];
                var fIndexList = fIndexCacheList.Where(f => f.MainTrackNumber.Value == monitorMainTrackNumber && f.StatingNo.Value == tenBridgeStatingNo && (f.HangerStatus == 0 || f.HangerStatus == 1) );
                //清除已分配下位机缓存
                foreach (var item in fIndexList)
                {
                    var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                    montorLog.Info(logMessage);
                    CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
                    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
                    montorLog.Info(logMessage);
                    if (item.HangerStatus == 0)
                    {

                        //站点分配数-1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = tenBridgeMaintrackNumber, StatingNo = tenBridgeStatingNo, AllocationNum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    }
                    if (item.HangerStatus == 1)
                    {
                        //站内数-1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = tenBridgeMaintrackNumber, StatingNo = tenBridgeStatingNo, OnLineSum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    }
                }
                var resvBridge = dicBridgeCache[bridegStating.BMainTrackNumber.Value];
                fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == monitorMainTrackNumber && f.StatingNo.Value == tenBridgeStatingNo && (f.HangerStatus == 0 || f.HangerStatus == 1) );
                fIndexCacheList.RemoveAll(f => f.MainTrackNumber.Value == resvBridge.AMainTrackNumber.Value && f.StatingNo.Value == resvBridge.ASiteNo.Value && (f.HangerStatus == 0 || f.HangerStatus == 1) );
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[monitorHangerNo] = fIndexCacheList;
                // return;
            }



            var nextPPChart = nextPPChartList.First();
            BridgeService.Instance.BridgeHandler(monitorMainTrackNumber, monitorStatingNo, monitorHangerNo, fcList, outMainTrackNumber, nextStatingNo, nextPPChart, true);


            //var bFlow = CANProductsService.Instance.GetBirdgeFlow(bridegStating.AMainTrackNumber.Value + "", tenBridgeStatingNo + "", fcList);

            //var allocation = new HangerStatingAllocationItem();
            //allocation.BatchNo =BridgeService.Instance.GetBatchNo(monitorHangerNo);
            //allocation.MainTrackNumber = (short)tenBridgeMaintrackNumber;
            //allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
            //allocation.AllocatingStatingDate = DateTime.Now;
            //allocation.NextSiteNo = tenBridgeStatingNo + "";//下一站
            //allocation.SiteNo = tenBridgeStatingNo.ToString(); //上一站
            //allocation.OutMainTrackNumber = tenBridgeMaintrackNumber;
            //allocation.isMonitoringAllocation = false;
            //allocation.ProcessFlowId = bFlow?.FlowId;
            //allocation.GroupNo = BridgeService.Instance.GetGroupNo(tenBridgeMaintrackNumber, tenBridgeStatingNo);
            //allocation.HangerNo = monitorHangerNo;
            //allocation.ProductsId = bFlow?.ProductsId;
            //allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
            //allocation.FlowChartd = bFlow?.ProcessChartId;
            //allocation.isMonitoringAllocation = true;
            //allocation.PColor = bFlow?.PColor;
            //allocation.PSize = bFlow?.PSize;
            //allocation.LineName = bFlow?.LineName;
            //allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
            //allocation.FlowNo = bFlow?.FlowNo;
            //allocation.ProcessFlowCode = bFlow?.FlowCode;
            //allocation.ProcessFlowName = bFlow?.FlowName;
            //allocation.FlowIndex = (short)(null != bFlow?.FlowIndex ? bFlow.FlowIndex.Value : -1);

            //hangerStatingAllocationList.Add(allocation);
            //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[monitorHangerNo] = hangerStatingAllocationList;

            //LowerPlaceInstr.Instance.AllocationHangerToNextStating(monitorHangerNo, tenBridgeStatingNo + "", tenBridgeMaintrackNumber);

        }
    }
}

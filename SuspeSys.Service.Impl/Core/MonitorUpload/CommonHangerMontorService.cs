using Newtonsoft.Json;
using StackExchange.Redis;
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
   public class CommonHangerMontorService:SusLog
    {
        public static readonly CommonHangerMontorService Instance = new CommonHangerMontorService();
        public void StoreStatingOutSiteMontorHandler(List<HangerProductFlowChartModel> fChartList, int mainTrackNumber, string statingNo, string hangerNo, HangerStatingAllocationItem allocationItem)
        {
            /*
                      ：如果存储站出战 
                      1.已分配车缝站且车缝站不满站就再分一次且清除之前的缓存；
                      2.已分配车缝站且车缝站满站就看其他车缝站是否满站，如果不满站就再分给其他车缝站，且清除之前分的缓存；
                      3.如果车缝站都满站就分存储站,且存储站不满站就清除之前的缓存。
                      4.如果所有站都满站，则提示满站异常
                      */
            //1
            var nextFlowIndexList = fChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(allocationItem.SiteNo)).Select(f => f.FlowIndex).Distinct();
            if (nextFlowIndexList.Count() == 0)
            {
                var exx = new ApplicationException(string.Format("【监测点存储站出战】主轨{0} 站点:{1} 衣架:{2} 找不到分配的站点信息!", mainTrackNumber, statingNo, hangerNo));
                montorLog.Error(exx);
                return;
            }
            if (nextFlowIndexList.Count() != 1)
            {
                var exx = new ApplicationException(string.Format("【监测点存储站出战】主轨{0} 站点:{1} 衣架:{2} 工序大于2个!", mainTrackNumber, statingNo, hangerNo));
                montorLog.Error(exx);
                return;
            }
            var flowIndex = nextFlowIndexList.First();
            //找下一站点
            var nextFlowStatlist = fChartList.Where(k => k.FlowIndex.Value == flowIndex
            && k.StatingNo != null && k.StatingNo.Value != -1
            && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
            && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
            && null != k.StatingRoleCode
            //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
            ).Select(f => new ProductsProcessOrderModel()
            {
                StatingNo = f.StatingNo.ToString(),
                MainTrackNumber = (int)f.MainTrackNumber,
                StatingCapacity = f.StatingCapacity.Value,
                Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                ProcessChartId = f.ProcessChartId,
                FlowNo = f.FlowNo,
                StatingRoleCode = f.StatingRoleCode

            }).ToList<ProductsProcessOrderModel>();
            if (nextFlowStatlist.Count == 0)
            {
                //下一道没有可以接收衣架的站
                var exx = new NoFoundStatingException(string.Format("【监测点存储站出战】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNumber, statingNo, hangerNo))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First().FlowNo
                };
                montorLog.Error(exx);
                return;
            }
            int outMainTrackNumber = 0;
            string nextStatingNo = null;
            //【获取下一站】
            OutSiteService.Instance.AllocationNextProcessFlowStating(nextFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
            if (string.IsNullOrEmpty(nextStatingNo))
            {
                var exx = new NoFoundStatingException(string.Format("【监测点存储站出战】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNumber, statingNo, hangerNo))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First().FlowNo
                };
                montorLog.Error(exx);
                return;
            }

            //---已分配缓存清除，站内数修正，衣架工艺图站点分配状态恢复
            #region 已分配缓存及站内数修正
            var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【主轨:{3} 站点:{4}】开始!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
            montorLog.Info(logMessage);

            ////站点分配数-1
            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = allocationItem.MainTrackNumber.Value, StatingNo = int.Parse(allocationItem.NextSiteNo), AllocationNum = -1 };
            //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
            NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));

            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【主轨:{3} 站点:{4}】结束!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
            montorLog.Info(logMessage);

            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配主轨:{3} 站点:{4}】!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
            montorLog.Info(logMessage);

            //CANTcp.client.ClearHangerCache(allocationItem.MainTrackNumber.Value, int.Parse(allocationItem.NextSiteNo), int.Parse(allocationItem.HangerNo), SuspeConstants.XOR);
            CANTcpServer.server.ClearHangerCache(allocationItem.MainTrackNumber.Value, int.Parse(allocationItem.NextSiteNo), int.Parse(allocationItem.HangerNo), SuspeConstants.XOR);
            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配主轨:{3} 站点:{4}】!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
            montorLog.Info(logMessage);


            //---清除已分配缓存


            //修正：如果本次分配和上次不同，则修正上次的工艺图状态为未分配?????/lucifer/2018年10月15日 21:09:09
            if (null != allocationItem.NextSiteNo && !allocationItem.NextSiteNo.Equals(nextStatingNo))
            {
                foreach (var fc in fChartList)
                {
                    if (fc.FlowIndex.Value == flowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(allocationItem.NextSiteNo) && allocationItem.MainTrackNumber.Value == fc.MainTrackNumber.Value)
                    {
                        fc.AllocationedDate = null;
                        fc.isAllocationed = false;
                        break;
                    }
                }
            }
            #endregion

            var fChart = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First();
            var tenMainTrackNumber = outMainTrackNumber.ToString();
           
            //记录衣架分配
           // var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingAllocationItem.ContainsKey(hangerNo))
            {
                var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingAllocationItem[hangerNo];
                var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
                nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
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
                dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);

                var susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送开始!", tenMainTrackNumber, nextStatingNo);
                tcpLogInfo.Info(susAllocatingMessage);

                //  CANTcp.client.AllocationHangerToNextStating(hexOutID, HexHelper.TenToHexString2Len(nextStatingNo), HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);
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
                    if (fc.FlowIndex.Value == flowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(nextStatingNo) && fc.MainTrackNumber.Value == outMainTrackNumber)
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
                chpf.FlowIndex = flowIndex.Value;
                chpf.FlowType = null == fChart?.FlowType ? 0 : fChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

                //修正站点缓存
                var hnssocAll = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocAll.Action = 0;
                hnssocAll.HangerNo = hangerNo;
                hnssocAll.MainTrackNumber = outMainTrackNumber;
                hnssocAll.StatingNo = int.Parse(nextStatingNo);
                hnssocAll.FlowNo = fChart.FlowNo;
                hnssocAll.FlowIndex = fChart.FlowIndex.Value;
                hnssocAll.HangerProductFlowChartModel = fChart;
                var hnssocAllJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocAll);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocAllJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocAllJson );
            }

            //更新衣架工艺图
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = fChartList;
            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo,fChartList);
        }

    }
}

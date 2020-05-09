using SuspeSys.AuxiliaryTools;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.F2
{
    public class F2Service : SusLog
    {
        private F2Service() { }
        public static readonly F2Service Instance = new F2Service();

        internal void F2AssgnValid(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int targertMainTrackNumber, int targertStatingNo, ref int tag, TcpClient tcpClient = null)
        {
            var statingCache = NewCacheService.Instance.GetStatingCache(targertMainTrackNumber, targertStatingNo);
            if (null == statingCache)
            {
                tag = 1;
                tcpLogInfo.Error(string.Format($"【F2指定业务衣架上传】主轨:{sourceMainTrackNuber} 站号:{sourceStatingNo} 衣架号:{hangerNo} 指定目的站【{targertMainTrackNumber}--{targertStatingNo}】站点不存在！"));
                //站点不存在
                LowerPlaceInstr.Instance.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return;
            }
            //满站，不可用，停止
            var statingCapacity = statingCache.Capacity.Value;
            var statingInNum = NewCacheService.Instance.GetStatingInNum(targertMainTrackNumber, targertStatingNo);
            var statingOnLineNum = NewCacheService.Instance.GetStatingOnlineNum(targertMainTrackNumber, targertStatingNo);
            var isFullStating = (statingCapacity - statingInNum - statingOnLineNum) <= 0;
            if (statingCache.IsEnabled == null || (statingCache.IsEnabled != null && !statingCache.IsEnabled.Value) || isFullStating)
            {
                tag = 2;
                tcpLogInfo.Error(string.Format($"【F2指定业务衣架上传】主轨:{sourceMainTrackNuber} 站号:{sourceStatingNo} 衣架号:{hangerNo} 指定目的站【{targertMainTrackNumber}--{targertStatingNo}】满站，不可用，停止！"));
                //站点不存在
                LowerPlaceInstr.Instance.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return;
            }
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo + ""))
            {
                tag = 3;
                tcpLogInfo.Error(string.Format($"【F2指定业务衣架上传】主轨:{sourceMainTrackNuber} 站号:{sourceStatingNo} 衣架号:{hangerNo} 指定目的站【{targertMainTrackNumber}--{targertStatingNo}】衣架未生产过！"));
                //衣架未生产过
                LowerPlaceInstr.Instance.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return;
            }
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo + "");
            var existNonSuccessList = fcList.Where(f => f.StatingNo != null && f.MainTrackNumber.Value == targertMainTrackNumber && f.StatingNo.Value == targertStatingNo);
            if (existNonSuccessList.Count() > 0)
            {
                var isNonSuccessed = existNonSuccessList.Where(f => f.Status.Value != 2).Count() > 0;
                if (isNonSuccessed)
                {
                    tag = 4;
                    //目标站点工序未完成
                    tcpLogInfo.Error(string.Format($"【F2指定业务衣架上传】主轨:{sourceMainTrackNuber} 站号:{sourceStatingNo} 衣架号:{hangerNo} 指定目的站【{targertMainTrackNumber}--{targertStatingNo}】目标站点工序未完成！"));
                    LowerPlaceInstr.Instance.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                    if (ToolsBase.isUnitTest)
                    {
                        Thread.CurrentThread.Join(10000);
                        Environment.Exit(Environment.ExitCode);
                    }
                    return;
                }
            }
            var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(targertMainTrackNumber.ToString(), targertStatingNo.ToString());
            if (isBridgeOutSite)
            {
                tag = 5;
                //桥接站不能指定
                tcpLogInfo.Error(string.Format($"【F2指定业务衣架上传】主轨:{sourceMainTrackNuber} 站号:{sourceStatingNo} 衣架号:{hangerNo} 桥接站【{targertMainTrackNumber}--{targertStatingNo}】不能指定！"));
                LowerPlaceInstr.Instance.F2AssginExceptionNotice(hangerNo, sourceMainTrackNuber, sourceStatingNo, tag, tcpClient);
                if (ToolsBase.isUnitTest)
                {
                    Thread.CurrentThread.Join(10000);
                    Environment.Exit(Environment.ExitCode);
                }
                return;
            }
            tag = 0;
        }

        internal void F2AssignHangerNoUpload(int hangerNo, int launchMainTrackNuber, int launchStatingNo, TcpClient tcpClient)
        {
            NewCacheService.Instance.PutF2HangerUpload(launchMainTrackNuber, launchStatingNo, hangerNo);
            tcpLogInfo.Info(string.Format($"【F2指定业务衣架上传】主轨:{launchStatingNo} 站号:{launchStatingNo} 衣架号:{hangerNo} 衣架绑定成功,等待下一步指令......"));
        }

        /// <summary>
        /// 是否是F2指定返回
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        internal bool F2Backflow(int tenMaintracknumber, int tenStatingNo, int hangerNo)
        {
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo + "")) return false;
            var currentHanger = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
            var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(tenMaintracknumber.ToString(), tenStatingNo.ToString());
            if (!currentHanger.IsF2Assgn)
            {
                return false;
            }

            var bFlow = NewCacheService.Instance.GetHangerMainFlowInfo(hangerNo + "");
            var currentF2AssignInfo = NewCacheService.Instance.GetCurrentF2AssignInfo(hangerNo);
            var sourceMainTrackNuber = currentF2AssignInfo.TargertMainTrackNumber;
            var targertMainTrackNumber = currentF2AssignInfo.LaunchMainTrackNumber;
            var targertStatingNo = currentF2AssignInfo.LaunchMainStatingNo;
            var sourceStatingNo = currentF2AssignInfo.TargertStatingNo;
            var f2AssignTag = currentF2AssignInfo.F2AssignTag;


            //F2指定目的站未处理过桥接
            if (isBridgeOutSite && f2AssignTag == 0)
            {
                #region 处理逻辑
                //
                targertMainTrackNumber = currentF2AssignInfo.TargertMainTrackNumber;
                targertStatingNo = currentF2AssignInfo.TargertStatingNo;
                sourceStatingNo = tenStatingNo;
                sourceMainTrackNuber = tenMaintracknumber;

                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (!dicBridgeCache.ContainsKey(sourceMainTrackNuber) || !dicBridgeCache.ContainsKey(dicBridgeCache[sourceMainTrackNuber].BMainTrackNumber.Value))
                {
                    var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, sourceStatingNo, sourceStatingNo, targertMainTrackNumber));
                    tcpLogError.Error(exNonFoundBridgeSet);
                    throw exNonFoundBridgeSet;
                }
                var reverseBridge = dicBridgeCache[dicBridgeCache[sourceMainTrackNuber].BMainTrackNumber.Value];
                //逆向桥接站缓存
                LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, reverseBridge.AMainTrackNumber.Value, reverseBridge.ASiteNo.Value);

                //逆向桥接站内数
                var reverseBridgeHS = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                reverseBridgeHS.Action = 16;
                reverseBridgeHS.HangerNo = hangerNo + "";
                reverseBridgeHS.MainTrackNumber = reverseBridge.AMainTrackNumber.Value;
                reverseBridgeHS.StatingNo = reverseBridge.ASiteNo.Value;
                reverseBridgeHS.FlowNo = string.Empty;
                reverseBridgeHS.FlowIndex = -1;
                reverseBridgeHS.IsBridgeAllocation = true;
                var reverseBridgeHnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(reverseBridgeHS);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, reverseBridgeHnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), reverseBridgeHnssocJson);

                //清除下位机分配缓存
                LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, sourceMainTrackNuber, sourceStatingNo);
                //给下一站分配缓存
                LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo + "", targertStatingNo + "", targertMainTrackNumber);

                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                {
                    HangerNo = hangerNo+"",
                    MainTrackNumber =(short) targertMainTrackNumber,
                    SiteNo = targertStatingNo + ""
                     ,
                    AllocatingStatingDate = DateTime.Now
                });
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);


                //下一站在线数+1及缓存分配
                var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + "");

                var allocation = new HangerStatingAllocationItem();
                allocation.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                allocation.MainTrackNumber = (short)targertMainTrackNumber;
                allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
                allocation.AllocatingStatingDate = DateTime.Now;
                allocation.NextSiteNo = targertStatingNo + "";//下一站
                allocation.SiteNo = sourceStatingNo.ToString(); //上一站
                allocation.OutMainTrackNumber = sourceMainTrackNuber;
                allocation.isMonitoringAllocation = false;
                allocation.ProcessFlowId = string.Empty;
                allocation.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                allocation.HangerNo = hangerNo + "";
                allocation.ProductsId = bFlow?.ProductsId;
                allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
                allocation.FlowChartd = bFlow?.ProcessChartId;
                allocation.PColor = bFlow?.PColor;
                allocation.PSize = bFlow?.PSize;
                allocation.LineName = bFlow?.LineName;
                allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
                allocation.FlowNo = string.Empty;
                allocation.ProcessFlowCode = string.Empty;
                allocation.ProcessFlowName = string.Empty;
                allocation.isF2AssgnAllocation = false;
                allocation.FlowIndex = (short)(-1);

                hangerStatingAllocationList.Add(allocation);
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", hangerStatingAllocationList);
                //发布衣架分配信息
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                //下一站分配数
                var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocNext.HangerProductFlowChartModel = new HangerProductFlowChartModel();
                hnssocNext.HangerProductFlowChartModel.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                hnssocNext.HangerProductFlowChartModel.HangerNo = hangerNo + "";
                hnssocNext.HangerProductFlowChartModel.ProductsId = bFlow?.ProductsId;
                hnssocNext.HangerProductFlowChartModel.ProcessOrderNo = bFlow?.ProcessOrderNo;
                hnssocNext.HangerProductFlowChartModel.ProcessChartId = bFlow?.ProcessChartId;
                hnssocNext.HangerProductFlowChartModel.PColor = bFlow?.PColor;
                hnssocNext.HangerProductFlowChartModel.PSize = bFlow?.PSize;
                hnssocNext.HangerProductFlowChartModel.LineName = bFlow?.LineName;
                hnssocNext.HangerProductFlowChartModel.Num = Convert.ToInt32(bFlow?.Num) + "";

                hnssocNext.Action = 17;
                hnssocNext.HangerNo = hangerNo + "";
                hnssocNext.MainTrackNumber = targertMainTrackNumber;
                hnssocNext.StatingNo = targertStatingNo;
                hnssocNext.FlowNo = string.Empty;
                hnssocNext.FlowIndex = -1;
                hnssocNext.IsBridgeAllocation = false;
                var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocNextJson );

                //当前站站内数修正
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 16;
                hnssoc.HangerNo = hangerNo + "";
                hnssoc.MainTrackNumber = sourceMainTrackNuber;
                hnssoc.StatingNo = sourceStatingNo;
                hnssoc.FlowNo = string.Empty;
                hnssoc.FlowIndex = -1;
                hnssoc.IsBridgeAllocation = true;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);

                var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                var currentHangerResume = new HangerProductsChartResumeModel();
                currentHangerResume.HangerNo = hangerNo + "";
                currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                currentHangerResume.StatingNo = sourceStatingNo + "";
                currentHangerResume.Action = 12;
                currentHangerResume.FlowNo = string.Empty;
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                //下一站衣架履历
                var fc = new HangerProductFlowChartModel();
                fc.HangerNo = hangerNo + "";
                fc.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                fc.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                fc.StatingNo = (short)targertStatingNo;
                fc.MainTrackNumber = (short)targertMainTrackNumber;
                fc.ProductsId = bFlow?.ProductsId;
                fc.ProcessOrderNo = bFlow?.ProcessOrderNo;
                fc.ProcessChartId = bFlow?.ProcessChartId;
                fc.PColor = bFlow?.PColor;
                fc.PSize = bFlow?.PSize;
                fc.LineName = bFlow?.LineName;
                fc.Num = bFlow?.Num;
                fc.FlowType = bFlow?.FlowType;
                fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(targertMainTrackNumber + "", targertStatingNo + "", hangerNo + "", fc, 1);

                //F2指定下一站履历
                F2AssignModel assign = new F2AssignModel();
                assign.SourceMainTrackNuber = sourceMainTrackNuber;
                assign.SourceStatingNo = sourceStatingNo;
                assign.TargertMainTrackNumber = targertMainTrackNumber;
                assign.TargertStatingNo = targertStatingNo;
                NewCacheService.Instance.PutF2AssignHanger(hangerNo, assign);

                //F2指定当前站信息修正
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo + "";
                chpf.MainTrackNumber = targertMainTrackNumber;
                chpf.StatingNo = targertStatingNo;
                chpf.FlowNo = "-5";
                chpf.FlowIndex = -5;
                chpf.FlowType = 0;
                chpf.IsF2Assgn = true;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
                NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo + "", chpf);


                #endregion
                return true;
            }
            //指定返回过桥接
            else if (f2AssignTag == -1 && tenMaintracknumber != currentF2AssignInfo.LaunchMainTrackNumber)
            {
                #region 处理逻辑
                sourceMainTrackNuber = tenMaintracknumber;
                sourceStatingNo = tenStatingNo;
                targertMainTrackNumber = currentF2AssignInfo.LaunchMainTrackNumber;
                targertStatingNo = currentF2AssignInfo.LaunchMainStatingNo;

                F2AssgnCrossMainTrack(hangerNo, sourceMainTrackNuber, sourceStatingNo, targertMainTrackNumber, targertStatingNo, bFlow);
                var currentF2HangerFlow = NewCacheService.Instance.GetCurrentF2AssignInfo(hangerNo);
                currentF2HangerFlow.F2AssignTag = -1;
                NewCacheService.Instance.UpdateCurrentF2AssignInfo(hangerNo, currentF2HangerFlow);
                #endregion
                return true;
            }
            //F2指定目的站已处理过桥接
            //其他情况直接让衣架回流发起指定的站点
            //
            else if (isBridgeOutSite && currentF2AssignInfo.TargertMainTrackNumber != tenMaintracknumber)//F2
            {
                #region 处理逻辑
                //
                targertMainTrackNumber = currentF2AssignInfo.LaunchMainTrackNumber;
                targertStatingNo = currentF2AssignInfo.LaunchMainStatingNo;
                sourceStatingNo = tenStatingNo;
                sourceMainTrackNuber = tenMaintracknumber;

                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (!dicBridgeCache.ContainsKey(sourceMainTrackNuber) || !dicBridgeCache.ContainsKey(dicBridgeCache[sourceMainTrackNuber].BMainTrackNumber.Value))
                {
                    var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, sourceStatingNo, sourceStatingNo, targertMainTrackNumber));
                    tcpLogError.Error(exNonFoundBridgeSet);
                    throw exNonFoundBridgeSet;
                }
                var reverseBridge = dicBridgeCache[dicBridgeCache[sourceMainTrackNuber].BMainTrackNumber.Value];
                //逆向桥接站缓存
                LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, reverseBridge.AMainTrackNumber.Value, reverseBridge.ASiteNo.Value);

                //逆向桥接站内数
                var reverseBridgeHS = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                reverseBridgeHS.Action = 16;
                reverseBridgeHS.HangerNo = hangerNo + "";
                reverseBridgeHS.MainTrackNumber = reverseBridge.AMainTrackNumber.Value;
                reverseBridgeHS.StatingNo = reverseBridge.ASiteNo.Value;
                reverseBridgeHS.FlowNo = string.Empty;
                reverseBridgeHS.FlowIndex = -1;
                reverseBridgeHS.IsBridgeAllocation = true;
                var reverseBridgeHnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(reverseBridgeHS);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, reverseBridgeHnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), reverseBridgeHnssocJson);

                //清除下位机分配缓存
                LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, sourceMainTrackNuber, sourceStatingNo);
                //给下一站分配缓存
                LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo + "", targertStatingNo + "", targertMainTrackNumber);

                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                {
                    HangerNo = hangerNo+"",
                    MainTrackNumber = (short)targertMainTrackNumber,
                    SiteNo = targertStatingNo + ""
                     ,
                    AllocatingStatingDate = DateTime.Now
                });
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

                //下一站在线数+1及缓存分配
                var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + "");

                var allocation = new HangerStatingAllocationItem();
                allocation.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                allocation.MainTrackNumber = (short)targertMainTrackNumber;
                allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
                allocation.AllocatingStatingDate = DateTime.Now;
                allocation.NextSiteNo = targertStatingNo + "";//下一站
                allocation.SiteNo = sourceStatingNo.ToString(); //上一站
                allocation.OutMainTrackNumber = sourceMainTrackNuber;
                allocation.isMonitoringAllocation = false;
                allocation.ProcessFlowId = string.Empty;
                allocation.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                allocation.HangerNo = hangerNo + "";
                allocation.ProductsId = bFlow?.ProductsId;
                allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
                allocation.FlowChartd = bFlow?.ProcessChartId;
                allocation.PColor = bFlow?.PColor;
                allocation.PSize = bFlow?.PSize;
                allocation.LineName = bFlow?.LineName;
                allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
                allocation.FlowNo = string.Empty;
                allocation.ProcessFlowCode = string.Empty;
                allocation.ProcessFlowName = string.Empty;
                //   allocation.isF2AssgnAllocation = false;
                allocation.FlowIndex = (short)(-1);

                hangerStatingAllocationList.Add(allocation);
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", hangerStatingAllocationList);
                //发布衣架分配信息
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                //下一站分配数
                var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocNext.HangerProductFlowChartModel = new HangerProductFlowChartModel();
                hnssocNext.HangerProductFlowChartModel.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                hnssocNext.HangerProductFlowChartModel.HangerNo = hangerNo + "";
                hnssocNext.HangerProductFlowChartModel.ProductsId = bFlow?.ProductsId;
                hnssocNext.HangerProductFlowChartModel.ProcessOrderNo = bFlow?.ProcessOrderNo;
                hnssocNext.HangerProductFlowChartModel.ProcessChartId = bFlow?.ProcessChartId;
                hnssocNext.HangerProductFlowChartModel.PColor = bFlow?.PColor;
                hnssocNext.HangerProductFlowChartModel.PSize = bFlow?.PSize;
                hnssocNext.HangerProductFlowChartModel.LineName = bFlow?.LineName;
                hnssocNext.HangerProductFlowChartModel.Num = Convert.ToInt32(bFlow?.Num) + "";

                hnssocNext.Action = 17;
                hnssocNext.HangerNo = hangerNo + "";
                hnssocNext.MainTrackNumber = targertMainTrackNumber;
                hnssocNext.StatingNo = targertStatingNo;
                hnssocNext.FlowNo = string.Empty;
                hnssocNext.FlowIndex = -1;
                hnssocNext.IsBridgeAllocation = false;
                var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocNextJson);

                //当前站站内数修正
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 16;
                hnssoc.HangerNo = hangerNo + "";
                hnssoc.MainTrackNumber = sourceMainTrackNuber;
                hnssoc.StatingNo = sourceStatingNo;
                hnssoc.FlowNo = string.Empty;
                hnssoc.FlowIndex = -1;
                hnssoc.IsBridgeAllocation = true;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);

                var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                var currentHangerResume = new HangerProductsChartResumeModel();
                currentHangerResume.HangerNo = hangerNo + "";
                currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                currentHangerResume.StatingNo = sourceStatingNo + "";
                currentHangerResume.Action = 12;
                currentHangerResume.FlowNo = string.Empty;
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);

                //下一站衣架履历
                var fc = new HangerProductFlowChartModel();
                fc.HangerNo = hangerNo + "";
                fc.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                fc.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                fc.StatingNo = (short)targertStatingNo;
                fc.MainTrackNumber = (short)targertMainTrackNumber;
                fc.ProductsId = bFlow?.ProductsId;
                fc.ProcessOrderNo = bFlow?.ProcessOrderNo;
                fc.ProcessChartId = bFlow?.ProcessChartId;
                fc.PColor = bFlow?.PColor;
                fc.PSize = bFlow?.PSize;
                fc.LineName = bFlow?.LineName;
                fc.Num = bFlow?.Num;
                fc.FlowType = bFlow?.FlowType;
                fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(targertMainTrackNumber + "", targertStatingNo + "", hangerNo + "", fc, 1);

                //F2指定下一站履历
                F2AssignModel assign = new F2AssignModel();
                assign.SourceMainTrackNuber = sourceMainTrackNuber;
                assign.SourceStatingNo = sourceStatingNo;
                assign.TargertMainTrackNumber = targertMainTrackNumber;
                assign.TargertStatingNo = targertStatingNo;
                NewCacheService.Instance.PutF2AssignHanger(hangerNo, assign);

                //F2指定当前站信息修正
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo + "";
                chpf.MainTrackNumber = targertMainTrackNumber;
                chpf.StatingNo = targertStatingNo;
                chpf.FlowNo = "-5";
                chpf.FlowIndex = -5;
                chpf.FlowType = 0;
                chpf.IsF2Assgn = false;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
                NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo + "", chpf);


                #endregion
                return true;
            }
            if (currentF2AssignInfo.LaunchMainTrackNumber == tenMaintracknumber)
            {
                #region //不走桥接

                if (isBridgeOutSite)
                {
                    var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                    if (!dicBridgeCache.ContainsKey(tenMaintracknumber) || !dicBridgeCache.ContainsKey(dicBridgeCache[tenMaintracknumber].BMainTrackNumber.Value))
                    {
                        var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, sourceStatingNo, sourceStatingNo, targertMainTrackNumber));
                        tcpLogError.Error(exNonFoundBridgeSet);
                        throw exNonFoundBridgeSet;
                    }
                    var reverseBridge = dicBridgeCache[dicBridgeCache[tenMaintracknumber].BMainTrackNumber.Value];
                    //逆向桥接站缓存
                    LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, reverseBridge.AMainTrackNumber.Value, reverseBridge.ASiteNo.Value);

                    var allocationJson2 = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                    {
                        HangerNo = hangerNo + "",
                        MainTrackNumber = reverseBridge.AMainTrackNumber,
                        SiteNo =reverseBridge.ASiteNo+""
                         ,
                        AllocatingStatingDate = DateTime.Now
                    });
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson2);


                    //逆向桥接站内数
                    var reverseBridgeHS = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    reverseBridgeHS.Action = 16;
                    reverseBridgeHS.HangerNo = hangerNo + "";
                    reverseBridgeHS.MainTrackNumber = reverseBridge.AMainTrackNumber.Value;
                    reverseBridgeHS.StatingNo = reverseBridge.ASiteNo.Value;
                    reverseBridgeHS.FlowNo = string.Empty;
                    reverseBridgeHS.FlowIndex = -1;
                    reverseBridgeHS.IsBridgeAllocation = true;
                    var reverseBridgeHnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(reverseBridgeHS);
                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, reverseBridgeHnssocJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),reverseBridgeHnssocJson );
                }

                //清除下位机分配缓存
                LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, sourceMainTrackNuber, sourceStatingNo);
                //给下一站分配缓存
                LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo + "", targertStatingNo + "", targertMainTrackNumber);

                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                {
                    HangerNo = hangerNo + "",
                    MainTrackNumber = (short)targertMainTrackNumber,
                    SiteNo = targertStatingNo + ""
                     ,
                    AllocatingStatingDate = DateTime.Now
                });
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);


                //下一站在线数+1及缓存分配
                var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + "");

                var allocation = new HangerStatingAllocationItem();
                allocation.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                allocation.MainTrackNumber = (short)targertMainTrackNumber;
                allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
                allocation.AllocatingStatingDate = DateTime.Now;
                allocation.NextSiteNo = targertStatingNo + "";//下一站
                allocation.SiteNo = tenStatingNo.ToString(); //上一站
                allocation.OutMainTrackNumber = tenMaintracknumber;
                allocation.isMonitoringAllocation = false;
                allocation.ProcessFlowId = string.Empty;
                allocation.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                allocation.HangerNo = hangerNo + "";
                allocation.ProductsId = bFlow?.ProductsId;
                allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
                allocation.FlowChartd = bFlow?.ProcessChartId;
                allocation.PColor = bFlow?.PColor;
                allocation.PSize = bFlow?.PSize;
                allocation.LineName = bFlow?.LineName;
                allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
                allocation.FlowNo = string.Empty;
                allocation.ProcessFlowCode = string.Empty;
                allocation.ProcessFlowName = string.Empty;
                // allocation.isF2AssgnAllocation = true;
                allocation.FlowIndex = (short)(-1);

                hangerStatingAllocationList.Add(allocation);
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", hangerStatingAllocationList);
                //发布衣架分配信息
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                //下一站分配数
                var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocNext.HangerProductFlowChartModel = new HangerProductFlowChartModel();
                hnssocNext.HangerProductFlowChartModel.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                hnssocNext.HangerProductFlowChartModel.HangerNo = hangerNo + "";
                hnssocNext.HangerProductFlowChartModel.ProductsId = bFlow?.ProductsId;
                hnssocNext.HangerProductFlowChartModel.ProcessOrderNo = bFlow?.ProcessOrderNo;
                hnssocNext.HangerProductFlowChartModel.ProcessChartId = bFlow?.ProcessChartId;
                hnssocNext.HangerProductFlowChartModel.PColor = bFlow?.PColor;
                hnssocNext.HangerProductFlowChartModel.PSize = bFlow?.PSize;
                hnssocNext.HangerProductFlowChartModel.LineName = bFlow?.LineName;
                hnssocNext.HangerProductFlowChartModel.Num = Convert.ToInt32(bFlow?.Num) + "";

                hnssocNext.Action = 17;
                hnssocNext.HangerNo = hangerNo + "";
                hnssocNext.MainTrackNumber = targertMainTrackNumber;
                hnssocNext.StatingNo = targertStatingNo;
                hnssocNext.FlowNo = currentF2AssignInfo.CurrentNonFlow.FlowNo;
                hnssocNext.FlowIndex = currentF2AssignInfo.CurrentNonFlow.FlowIndex;
                hnssocNext.IsBridgeAllocation = false;
                var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocNextJson );

                //当前站站内数修正
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 16;
                hnssoc.HangerNo = hangerNo + "";
                hnssoc.MainTrackNumber = tenMaintracknumber;
                hnssoc.StatingNo = tenStatingNo;
                hnssoc.FlowNo = string.Empty;
                hnssoc.FlowIndex = -1;
                hnssoc.IsBridgeAllocation = false;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
                //是否是发起F2指定源头站点
                var isLaunchSourceStating = !NewCacheService.Instance.F2AssignIsContains(hangerNo);

                //是否记录产量,根据参数设置
                var currentHangerResume = new HangerProductsChartResumeModel();
                currentHangerResume.HangerNo = hangerNo + "";
                currentHangerResume.MainTrackNumber = tenMaintracknumber;
                currentHangerResume.StatingNo = tenStatingNo + "";
                currentHangerResume.Action = 13;
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);

                //下一站衣架履历
                var fc = new HangerProductFlowChartModel();
                fc.HangerNo = hangerNo + "";
                fc.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
                fc.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
                fc.StatingNo = (short)targertStatingNo;
                fc.MainTrackNumber = (short)targertMainTrackNumber;
                fc.ProductsId = bFlow?.ProductsId;
                fc.ProcessOrderNo = bFlow?.ProcessOrderNo;
                fc.ProcessChartId = bFlow?.ProcessChartId;
                fc.PColor = bFlow?.PColor;
                fc.PSize = bFlow?.PSize;
                fc.LineName = bFlow?.LineName;
                fc.Num = bFlow?.Num;
                fc.FlowType = bFlow?.FlowType;
                fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(targertMainTrackNumber + "", targertStatingNo + "", hangerNo + "", fc, 1);

                //var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                //chpf.HangerNo = hangerNo + "";
                //chpf.MainTrackNumber = targertMainTrackNumber;
                //chpf.StatingNo = targertStatingNo;
                //chpf.FlowNo = "-5";
                //chpf.FlowIndex = -5;
                //chpf.FlowType = 0;
                //chpf.IsF2Assgn = false;
                var chpf = currentF2AssignInfo.CurrentNonFlow;
                chpf.IsF2Assgn = false;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
                NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo + "", chpf);

                F2AssignModel assign = new F2AssignModel();
                assign.SourceMainTrackNuber = tenMaintracknumber;
                assign.SourceStatingNo = tenStatingNo;
                assign.TargertMainTrackNumber = targertMainTrackNumber;
                assign.TargertStatingNo = targertStatingNo;
                NewCacheService.Instance.RemoveF2HangerAssign(hangerNo);

                #endregion
                return true;
            }

            #region //走桥接
            #endregion
            return true;
        }

        /// <summary>
        /// F2指定逻辑处理：分为跨主轨和不跨主轨的情况
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="sourceMainTrackNuber"></param>
        /// <param name="sourceStatingNo"></param>
        /// <param name="targertMainTrackNumber"></param>
        /// <param name="targertStatingNo"></param>
        /// <param name="isCrossMainTrack"></param>
        /// <param name="tcpClient"></param>
        internal void F2AssgnAction(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int targertMainTrackNumber, int targertStatingNo, bool isCrossMainTrack, TcpClient tcpClient = null)
        {
            tcpLogInfo.Info(string.Format($"【F2指定业务】源头主轨:{sourceMainTrackNuber} 源头站号:{sourceStatingNo} 衣架号:{hangerNo} 目的主轨:{targertMainTrackNumber} 目的站号:{targertStatingNo}  F2指定Action处理开始......"));
            var bFlow = NewCacheService.Instance.GetHangerMainFlowInfo(hangerNo + "");

            if (!isCrossMainTrack)
            {
                #region 不跨主轨
                F2AssgnNonCrossMainTrack(hangerNo, sourceMainTrackNuber, sourceStatingNo, targertMainTrackNumber, targertStatingNo, bFlow);
                return;
                #endregion
            }
            F2AssgnCrossMainTrack(hangerNo, sourceMainTrackNuber, sourceStatingNo, targertMainTrackNumber, targertStatingNo, bFlow, true);
            tcpLogInfo.Info(string.Format($"【F2指定业务】源头主轨:{sourceMainTrackNuber} 源头站号:{sourceStatingNo} 衣架号:{hangerNo} 目的主轨:{targertMainTrackNumber} 目的站号:{targertStatingNo}  F2指定Action处理结束......"));
        }

        private static void F2AssgnNonCrossMainTrack(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int targertMainTrackNumber, int targertStatingNo, HangerProductFlowChartModel bFlow)
        {
            //清除下位机分配缓存
            LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, sourceMainTrackNuber, sourceStatingNo);
            //给下一站分配缓存
            LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo + "", targertStatingNo + "", targertMainTrackNumber);

            var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            {
                HangerNo = hangerNo + "",
                MainTrackNumber = (short)targertMainTrackNumber,
                SiteNo = targertStatingNo + ""
                 ,
                AllocatingStatingDate = DateTime.Now
            });
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

            //下一站在线数+1及缓存分配
            var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + "");

            var allocation = new HangerStatingAllocationItem();
            allocation.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
            allocation.MainTrackNumber = (short)targertMainTrackNumber;
            allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
            allocation.AllocatingStatingDate = DateTime.Now;
            allocation.NextSiteNo = targertStatingNo + "";//下一站
            allocation.SiteNo = sourceStatingNo.ToString(); //上一站
            allocation.OutMainTrackNumber = sourceMainTrackNuber;
            allocation.isMonitoringAllocation = false;
            allocation.ProcessFlowId = string.Empty;
            allocation.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            allocation.HangerNo = hangerNo + "";
            allocation.ProductsId = bFlow?.ProductsId;
            allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
            allocation.FlowChartd = bFlow?.ProcessChartId;
            allocation.PColor = bFlow?.PColor;
            allocation.PSize = bFlow?.PSize;
            allocation.LineName = bFlow?.LineName;
            allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
            allocation.FlowNo = string.Empty;
            allocation.ProcessFlowCode = string.Empty;
            allocation.ProcessFlowName = string.Empty;
            allocation.isF2AssgnAllocation = true;
            allocation.FlowIndex = (short)(-1);

            hangerStatingAllocationList.Add(allocation);
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", hangerStatingAllocationList);
            //发布衣架分配信息
            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            //下一站分配数
            var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocNext.HangerProductFlowChartModel = new HangerProductFlowChartModel();
            hnssocNext.HangerProductFlowChartModel.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            hnssocNext.HangerProductFlowChartModel.HangerNo = hangerNo + "";
            hnssocNext.HangerProductFlowChartModel.ProductsId = bFlow?.ProductsId;
            hnssocNext.HangerProductFlowChartModel.ProcessOrderNo = bFlow?.ProcessOrderNo;
            hnssocNext.HangerProductFlowChartModel.ProcessChartId = bFlow?.ProcessChartId;
            hnssocNext.HangerProductFlowChartModel.PColor = bFlow?.PColor;
            hnssocNext.HangerProductFlowChartModel.PSize = bFlow?.PSize;
            hnssocNext.HangerProductFlowChartModel.LineName = bFlow?.LineName;
            hnssocNext.HangerProductFlowChartModel.Num = Convert.ToInt32(bFlow?.Num) + "";

            hnssocNext.Action = 17;
            hnssocNext.HangerNo = hangerNo + "";
            hnssocNext.MainTrackNumber = targertMainTrackNumber;
            hnssocNext.StatingNo = targertStatingNo;
            hnssocNext.FlowNo = string.Empty;
            hnssocNext.FlowIndex = -1;
            hnssocNext.IsBridgeAllocation = true;
            var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocNextJson);

            //当前站站内数修正
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = 16;
            hnssoc.HangerNo = hangerNo + "";
            hnssoc.MainTrackNumber = sourceMainTrackNuber;
            hnssoc.StatingNo = sourceStatingNo;
            hnssoc.FlowNo = string.Empty;
            hnssoc.FlowIndex = -1;
            hnssoc.IsBridgeAllocation = true;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            //是否是发起F2指定源头站点
            var isLaunchSourceStating = !NewCacheService.Instance.F2AssignIsContains(hangerNo);

            //是否记录产量,根据参数设置
            var isRecordsYield = false;
            if (!isRecordsYield)
            {
                if (isLaunchSourceStating)//F2指定发起源头站
                {
                    var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 12;
                    currentHangerResume.FlowNo = currentHangerFlow.FlowNo;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
                else
                {
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 13;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
            }
            else
            {
                if (isLaunchSourceStating)
                {
                    var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 3;
                    currentHangerResume.FlowNo = currentHangerFlow.FlowNo;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
                else
                {
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 13;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);

                }
            }

            //下一站衣架履历
            var fc = new HangerProductFlowChartModel();
            fc.HangerNo = hangerNo + "";
            fc.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            fc.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
            fc.StatingNo = (short)targertStatingNo;
            fc.MainTrackNumber = (short)targertMainTrackNumber;
            fc.ProductsId = bFlow?.ProductsId;
            fc.ProcessOrderNo = bFlow?.ProcessOrderNo;
            fc.ProcessChartId = bFlow?.ProcessChartId;
            fc.PColor = bFlow?.PColor;
            fc.PSize = bFlow?.PSize;
            fc.LineName = bFlow?.LineName;
            fc.Num = bFlow?.Num;
            fc.FlowType = bFlow?.FlowType;
            fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
            BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(targertMainTrackNumber + "", targertStatingNo + "", hangerNo + "", fc, 1);

            F2AssignModel assign = new F2AssignModel();
            assign.SourceMainTrackNuber = sourceMainTrackNuber;
            assign.SourceStatingNo = sourceStatingNo;
            assign.TargertMainTrackNumber = targertMainTrackNumber;
            assign.TargertStatingNo = targertStatingNo;
            NewCacheService.Instance.PutF2AssignHanger(hangerNo, assign);

            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
            chpf.HangerNo = hangerNo + "";
            chpf.MainTrackNumber = targertMainTrackNumber;
            chpf.StatingNo = targertStatingNo;
            chpf.FlowNo = "-5";
            chpf.FlowIndex = -5;
            chpf.FlowType = 0;
            chpf.IsF2Assgn = true;
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
            //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
            NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo + "", chpf);


        }
        private static void F2AssgnCrossMainTrack(int hangerNo, int sourceMainTrackNuber, int sourceStatingNo, int targertMainTrackNumber, int targertStatingNo, HangerProductFlowChartModel bFlow, bool isF2AssgnAllocation = false)
        {
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(sourceMainTrackNuber) || !dicBridgeCache.ContainsKey(targertMainTrackNumber))
            {
                var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, sourceStatingNo, sourceStatingNo, targertMainTrackNumber));
                tcpLogError.Error(exNonFoundBridgeSet);
                throw exNonFoundBridgeSet;
            }
            var bridegStating = dicBridgeCache[sourceMainTrackNuber];
            var tenBridgeMaintrackNumber = bridegStating.AMainTrackNumber.Value;
            var tenBridgeStatingNo = bridegStating.ASiteNo.Value;

            //清除下位机分配缓存
            LowerPlaceInstr.Instance.ClearHangerCache(hangerNo, sourceMainTrackNuber, sourceStatingNo);
            //给下一站分配缓存
            LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo + "", tenBridgeStatingNo + "", tenBridgeMaintrackNumber);

            var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            {
                HangerNo = hangerNo + "",
                MainTrackNumber = (short)sourceMainTrackNuber,
                SiteNo = sourceStatingNo + ""
                 ,
                AllocatingStatingDate = DateTime.Now
            });
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);


            //下一站在线数+1及缓存分配
            var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + "");

            var allocation = new HangerStatingAllocationItem();
            allocation.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
            allocation.MainTrackNumber = (short)tenBridgeMaintrackNumber;
            allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
            allocation.AllocatingStatingDate = DateTime.Now;
            allocation.NextSiteNo = tenBridgeStatingNo + "";//下一站
            allocation.SiteNo = sourceStatingNo.ToString(); //上一站
            allocation.OutMainTrackNumber = sourceMainTrackNuber;
            allocation.isMonitoringAllocation = false;
            allocation.ProcessFlowId = string.Empty;
            allocation.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            allocation.HangerNo = hangerNo + "";
            allocation.ProductsId = bFlow?.ProductsId;
            allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
            allocation.FlowChartd = bFlow?.ProcessChartId;
            allocation.PColor = bFlow?.PColor;
            allocation.PSize = bFlow?.PSize;
            allocation.LineName = bFlow?.LineName;
            allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
            allocation.FlowNo = string.Empty;
            allocation.ProcessFlowCode = string.Empty;
            allocation.ProcessFlowName = string.Empty;
            allocation.isF2AssgnAllocation = isF2AssgnAllocation;
            allocation.FlowIndex = (short)(-1);

            hangerStatingAllocationList.Add(allocation);
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", hangerStatingAllocationList);
            //发布衣架分配信息
            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            //下一站分配数
            var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocNext.HangerProductFlowChartModel = new HangerProductFlowChartModel();
            hnssocNext.HangerProductFlowChartModel.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            hnssocNext.HangerProductFlowChartModel.HangerNo = hangerNo + "";
            hnssocNext.HangerProductFlowChartModel.ProductsId = bFlow?.ProductsId;
            hnssocNext.HangerProductFlowChartModel.ProcessOrderNo = bFlow?.ProcessOrderNo;
            hnssocNext.HangerProductFlowChartModel.ProcessChartId = bFlow?.ProcessChartId;
            hnssocNext.HangerProductFlowChartModel.PColor = bFlow?.PColor;
            hnssocNext.HangerProductFlowChartModel.PSize = bFlow?.PSize;
            hnssocNext.HangerProductFlowChartModel.LineName = bFlow?.LineName;
            hnssocNext.HangerProductFlowChartModel.Num = Convert.ToInt32(bFlow?.Num) + "";
            hnssocNext.Action = 17;
            hnssocNext.HangerNo = hangerNo + "";
            hnssocNext.MainTrackNumber = tenBridgeMaintrackNumber;
            hnssocNext.StatingNo = tenBridgeStatingNo;
            hnssocNext.FlowNo = string.Empty;
            hnssocNext.FlowIndex = -1;
            hnssocNext.IsBridgeAllocation = true;
            var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocNextJson );

            //当前站站内数修正
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = 16;
            hnssoc.HangerNo = hangerNo + "";
            hnssoc.MainTrackNumber = sourceMainTrackNuber;
            hnssoc.StatingNo = sourceStatingNo;
            hnssoc.FlowNo = string.Empty;
            hnssoc.FlowIndex = -1;
            hnssoc.IsBridgeAllocation = true;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            //是否是发起F2指定源头站点
            var isLaunchSourceStating = !NewCacheService.Instance.F2AssignIsContains(hangerNo);

            //是否记录产量,根据参数设置
            var isRecordsYield = false;
            if (!isRecordsYield)
            {
                if (isLaunchSourceStating)//F2指定发起源头站
                {
                    var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 12;
                    currentHangerResume.FlowNo = currentHangerFlow.FlowNo;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    //  NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
                else
                {
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 13;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
            }
            else
            {
                if (isLaunchSourceStating)
                {
                    var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 3;
                    currentHangerResume.FlowNo = currentHangerFlow.FlowNo;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
                else
                {
                    var currentHangerResume = new HangerProductsChartResumeModel();
                    currentHangerResume.HangerNo = hangerNo + "";
                    currentHangerResume.MainTrackNumber = sourceMainTrackNuber;
                    currentHangerResume.StatingNo = sourceStatingNo + "";
                    currentHangerResume.Action = 13;
                    var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(currentHangerResume);
                    //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                }
            }

            //下一站衣架履历
            var fc = new HangerProductFlowChartModel();
            fc.HangerNo = hangerNo + "";
            fc.GroupNo = BridgeService.Instance.GetGroupNo(targertMainTrackNumber, targertStatingNo);
            fc.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo + "");
            fc.StatingNo = (short)tenBridgeStatingNo;
            fc.MainTrackNumber = (short)tenBridgeMaintrackNumber;
            fc.ProductsId = bFlow?.ProductsId;
            fc.ProcessOrderNo = bFlow?.ProcessOrderNo;
            fc.ProcessChartId = bFlow?.ProcessChartId;
            fc.PColor = bFlow?.PColor;
            fc.PSize = bFlow?.PSize;
            fc.LineName = bFlow?.LineName;
            fc.Num = bFlow?.Num;
            fc.FlowType = bFlow?.FlowType;
            fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
            BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(fc.MainTrackNumber.Value + "", fc.StatingNo.Value + "", hangerNo + "", fc, 1);


            F2AssignModel assign = new F2AssignModel();
            assign.SourceMainTrackNuber = sourceMainTrackNuber;
            assign.SourceStatingNo = sourceStatingNo;
            assign.TargertMainTrackNumber = targertMainTrackNumber;
            assign.TargertStatingNo = targertStatingNo;
            NewCacheService.Instance.PutF2AssignHanger(hangerNo, assign);

            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
            chpf.HangerNo = hangerNo + "";
            chpf.MainTrackNumber = targertMainTrackNumber;
            chpf.StatingNo = targertStatingNo;
            chpf.FlowNo = "-5";
            chpf.FlowIndex = -5;
            chpf.FlowType = 0;
            chpf.IsF2Assgn = true;
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
            //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
            NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo + "", chpf);

        }

    }
}

using Newtonsoft.Json;

using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Core.Bridge
{
    public class BridgeService : SusLog
    {
        public readonly static BridgeService Instance = new BridgeService();
        private BridgeService() { }

        //    public bool IsBridgeOutSiteAndInFlowChart(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, ref int currentIndex, ref bool isBSiteInFlowChart,
        //ref DaoModel.BridgeSet bBirdeSet, ref DaoModel.BridgeSet aBirdeSet)
        //    {
        //        var mainTrackNumber = int.Parse(tenMainTrackNo);
        //        var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
        //        if (!dicBridgeCache.ContainsKey(mainTrackNumber))
        //        {
        //            return false;
        //        }
        //        var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
        //        if (mainTrackNumber != outStatingMaintrackNumberBridge.AMainTrackNumber.Value && int.Parse(tenStatingNo) != outStatingMaintrackNumberBridge.ASiteNo.Value)
        //            return false;

        //        // var nextStatingMaintrackBridge = dicBridgeCache[outMainTrackNumber];
        //        var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //        if (!dicHangerFlowChartCache.ContainsKey(tenHangerNo))
        //        {
        //            return false;
        //        }
        //        var hangerFlowChartList = dicHangerFlowChartCache[tenHangerNo];
        //        //b站桥接站
        //        bBirdeSet = dicBridgeCache[outStatingMaintrackNumberBridge.BMainTrackNumber.Value];
        //        var bbBridgeSet = bBirdeSet;
        //        aBirdeSet = outStatingMaintrackNumberBridge;
        //        //b站桥接站是否在工艺图上
        //        isBSiteInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == bbBridgeSet.ASiteNo.Value && f.MainTrackNumber.Value == bbBridgeSet.AMainTrackNumber.Value && f.Status.Value != 2).Count() > 0;
        //        var isBridgeOutSiteAndInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == mainTrackNumber && f.Status.Value != 2).Count() > 0;
        //        if (isBridgeOutSiteAndInFlowChart)
        //        {
        //            currentIndex = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == mainTrackNumber && f.Status.Value != 2)
        //                .Select(ff => ff.FlowIndex.Value).Min();
        //        }
        //        return isBridgeOutSiteAndInFlowChart;
        //    }
        private static readonly object locObject = new object();
        internal void BridgeStatingOutSiteAndNotFlowHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo)
        {
            lock (locObject)
            {
                string nextFlowStatingNo = null;
                int outMainTrackNumber = 0;
                bool isFlowSucess = false;
                string info = null;
                CANProductsService.Instance.BridgeStatingNoToNextSatingHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, ref nextFlowStatingNo, ref outMainTrackNumber, ref isFlowSucess, ref info);
                CANProductsService.Instance.CorrectBridgeInStatingNum(tenMainTrackNo, tenStatingNo, outMainTrackNumber, tenHangerNo);
                if (outMainTrackNumber == 0)
                {
                    var ex = new ApplicationException(string.Format("主轨:{0} 站点:{1} 衣架:{2}  找不到下一站!", tenHangerNo, tenStatingNo, tenHangerNo));
                    tcpLogError.Error(ex);
                    throw ex;
                }
            }

        }
        public string GetGroupNo(int tenMaintracknumber, int tenStatingNo)
        {
           
            var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format("{0}:{1}", tenMaintracknumber, tenStatingNo);
            if (!dicStatingCache.ContainsKey(key))
            {
                tcpLogError.ErrorFormat("主轨:{0} 站点:{1} 没有找到组!", tenMaintracknumber, tenStatingNo);
            }
            return dicStatingCache[key].GroupNO?.Trim();
            //return "";
        }
        public long? GetBatchNo(string tenHangerNo) {
            //var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(tenHangerNo)) {//dicHangerStatingAllocationItem.ContainsKey(tenHangerNo)) {
                return NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo).First().BatchNo;
            }
            return null;
        }
        internal void BridgeHandler(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, int outMainTrackNumber, string nextStatingNo, HangerProductFlowChartModel nextPPChart,bool isMontor=false)
        {
            lock (locObject)
            {
                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (!dicBridgeCache.ContainsKey(tenMaintracknumber) || !dicBridgeCache.ContainsKey(outMainTrackNumber))
                {
                    var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", tenHangerNo, tenMaintracknumber, tenStatingNo, outMainTrackNumber));
                    tcpLogError.Error(exNonFoundBridgeSet);
                    throw exNonFoundBridgeSet;
                }
                var bridegStating = dicBridgeCache[tenMaintracknumber];
                var tenBridgeMaintrackNumber = bridegStating.AMainTrackNumber.Value;
                var tenBridgeStatingNo = bridegStating.ASiteNo.Value;

                // var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);

                var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo);// dicHangerStatingAllocationItem[tenHangerNo.ToString()];
                var bFlow = CANProductsService.Instance.GetBirdgeFlow(tenMaintracknumber + "", tenBridgeStatingNo + "", hangerProcessFlowChartList);

                var allocation = new DaoModel.HangerStatingAllocationItem();
                allocation.BatchNo = GetBatchNo(tenHangerNo);
                allocation.MainTrackNumber = (short)tenMaintracknumber;
                allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
                allocation.AllocatingStatingDate = DateTime.Now;
                allocation.NextSiteNo = tenBridgeStatingNo + "";//下一站
                allocation.SiteNo = tenStatingNo.ToString(); //上一站
                allocation.OutMainTrackNumber = tenMaintracknumber;
                allocation.isMonitoringAllocation = false;
                allocation.ProcessFlowId = bFlow?.FlowId;
                allocation.GroupNo = GetGroupNo(tenMaintracknumber, tenBridgeStatingNo);
                allocation.HangerNo = tenHangerNo;
                allocation.ProductsId = bFlow?.ProductsId;
                allocation.ProcessOrderNo = bFlow?.ProcessOrderNo;
                allocation.FlowChartd = bFlow?.ProcessChartId;
                allocation.PColor = bFlow?.PColor;
                allocation.PSize = bFlow?.PSize;
                allocation.LineName = bFlow?.LineName;
                allocation.SizeNum = Convert.ToInt32(bFlow?.Num);
                allocation.FlowNo = bFlow?.FlowNo;
                allocation.ProcessFlowCode = bFlow?.FlowCode;
                allocation.ProcessFlowName = bFlow?.FlowName;
                allocation.isMonitoringAllocation = isMontor;
                allocation.FlowIndex = (short)(null != bFlow?.FlowIndex ? bFlow.FlowIndex.Value : -1);

                hangerStatingAllocationList.Add(allocation);
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo + "", hangerStatingAllocationList);
                //发布 待生产衣架信息
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);


                var nextBridgeStatingIsContainsFlow = hangerProcessFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == bridegStating.BSiteNo.Value && f.MainTrackNumber.Value == bridegStating.BMainTrackNumber.Value && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
                if (nextBridgeStatingIsContainsFlow)
                {
                    nextPPChart = hangerProcessFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == bridegStating.BSiteNo.Value && f.MainTrackNumber.Value == bridegStating.BMainTrackNumber.Value && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
                }
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo); //SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo];
                var currentFlowNo = current.FlowNo?.Trim();

                //发布衣架下一站工序状态
                CANProductsService.Instance.CorrectHangerNextFlowHandler(tenHangerNo, nextPPChart);

                LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo, tenBridgeStatingNo + "", tenBridgeMaintrackNumber);
                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new DaoModel.HangerStatingAllocationItem()
                {
                    HangerNo = tenHangerNo,
                    MainTrackNumber = tenBridgeMaintrackNumber,
                    SiteNo = tenBridgeStatingNo + ""
                     ,
                    AllocatingStatingDate = DateTime.Now
                });
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

                var bridgeStatingIsInFlowChart = CANProductsService.Instance.IsBridgeInverseBridge(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", hangerProcessFlowChartList);
                HangerProductFlowChartModel nonSucessedFlow = null;
                var bridgeStatingFlowSuccess = CANProductsService.Instance.IsBridgeInverseBridgeAndFlowSuccess(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", hangerProcessFlowChartList, ref nonSucessedFlow);
                if (!bridgeStatingIsInFlowChart)
                {
                    var fc = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(nextPPChart);
                    fc.CheckInfo = string.Empty;
                    fc.CheckResult = string.Empty;
                    fc.FlowIndex = -1;
                    fc.FlowNo = string.Empty;
                    fc.FlowName = string.Empty;
                    fc.FlowCode = string.Empty;
                    fc.ReworkDate = null;
                    fc.ReworkEmployeeName = string.Empty;
                    fc.ReworkEmployeeNo = string.Empty;
                    fc.FlowType = 0;
                    fc.MainTrackNumber = tenBridgeMaintrackNumber;
                    CorrectNextStatingStatingNumHandler(tenHangerNo, tenBridgeMaintrackNumber, tenBridgeStatingNo + "", fc, 0);
                    NextStatingAlloctionResumeCacheHandler(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", tenHangerNo, fc, 1);
                }
                //在工艺图且工序未完成
                if (bridgeStatingIsInFlowChart && !bridgeStatingFlowSuccess)
                {
                    var fc = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(nonSucessedFlow);
                    //fc.CheckInfo = string.Empty;
                    //fc.CheckResult = string.Empty;
                    //fc.FlowIndex = -1;
                    //fc.FlowNo = string.Empty;
                    //fc.FlowName = string.Empty;
                    //fc.FlowCode = string.Empty;
                    //fc.ReworkDate = null;
                    //fc.ReworkEmployeeName = string.Empty;
                    //fc.ReworkEmployeeNo = string.Empty;
                    //fc.FlowType = 0;
                    fc.MainTrackNumber = tenBridgeMaintrackNumber;
                    CorrectNextStatingStatingNumHandler(tenHangerNo, tenBridgeMaintrackNumber, tenBridgeStatingNo + "", fc, 0);
                    NextStatingAlloctionResumeCacheHandler(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", tenHangerNo, fc, 1);
                }
                //在工艺图且工序已经完成
                if (bridgeStatingIsInFlowChart && bridgeStatingFlowSuccess)
                {
                    //针对桥接携带工序出战再进站的需特殊处理

                    var fc = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(nextPPChart);
                    fc.FlowIndex = -1;
                    fc.FlowNo = string.Empty;
                    fc.FlowName = string.Empty;
                    fc.FlowCode = string.Empty;
                    fc.CheckInfo = string.Empty;
                    fc.CheckResult = string.Empty;
                    fc.ReworkDate = null;
                    fc.ReworkEmployeeName = string.Empty;
                    fc.ReworkEmployeeNo = string.Empty;
                    fc.FlowType = 0;
                    fc.MainTrackNumber = tenBridgeMaintrackNumber;
                    CorrectNextStatingStatingNumHandler(tenHangerNo, tenBridgeMaintrackNumber, tenBridgeStatingNo + "", fc, 13);
                    NextStatingAlloctionResumeCacheHandler(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", tenHangerNo, fc, 9);
                }
                ////站点分配数+1
                //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = tenBridgeMaintrackNumber, StatingNo = tenBridgeStatingNo, AllocationNum = 1 };
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
            }
        }
        /// <summary>
        /// 下一站 站内数，分配数修正
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        public  void CorrectNextStatingStatingNumHandler(string hangerNo, int outMainTrackNumber, string nextStatingNo, HangerProductFlowChartModel nextHangerFlowChart, int action)
        {
            lock (locObject)
            {
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = action;
                hnssoc.HangerNo = hangerNo;
                hnssoc.MainTrackNumber = outMainTrackNumber;
                hnssoc.StatingNo = int.Parse(nextStatingNo);
                hnssoc.FlowNo = nextHangerFlowChart?.FlowNo;
                hnssoc.FlowIndex = nextHangerFlowChart != null ? nextHangerFlowChart.FlowIndex.Value : -1;
                hnssoc.HangerProductFlowChartModel = nextHangerFlowChart;
                hnssoc.IsBridgeAllocation = true;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
        }
        /// <summary>
        /// 【衣架生产履历】下一站分配Cache写入
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        public  void NextStatingAlloctionResumeCacheHandler(string mainTrackNo, string statingNo, string hangerNo, HangerProductFlowChartModel nextHangerFlowChart, int action)
        {
            lock (locObject)
            {
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = statingNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    HangerProductFlowChart = nextHangerFlowChart,
                    Action = action,
                    NextStatingNo = statingNo,
                    IsBridgeAllocation = true
                };
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
            }
        }
    }
}

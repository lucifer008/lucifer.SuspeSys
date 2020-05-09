using SusNet.Common.SusBusMessage;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Domain;
using Suspe.CAN.Action.CAN;
using SusNet.Common.Utils;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.Core.Flow;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using Newtonsoft.Json;
using SuspeSys.Service.Impl.Core.Check;
using SuspeSys.Service.Impl.Core.Cache;

namespace SuspeSys.Service.Impl.Core.OutSite
{
    public class OutSiteCommonHangerHandler : SusLog
    {
        private OutSiteCommonHangerHandler() { }
        public readonly static OutSiteCommonHangerHandler Instance = new OutSiteCommonHangerHandler();
        private static readonly object locObject = new object();



        /// <summary>
        /// 正常衣架处理
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="v"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <param name="ppChart"></param>
        internal void HangerOutSiteHandler(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart)
        {
            lock (locObject)
            {
                var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(tenMaintracknumber.ToString(), tenStatingNo.ToString());
                if (isBridgeOutSite && ppChart == null)
                {//桥接出战且不携带工序/返工后桥接站出战
                    BridgeService.Instance.BridgeStatingOutSiteAndNotFlowHandler(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo);
                    return;
                }
                if (null == ppChart)
                {
                    var err = new ApplicationException($"衣架号:{tenHangerNo} 主轨:{tenMaintracknumber} 站点:{tenStatingNo} 找不到当前工序!ppChart");
                    throw err;
                }
                List<HangerProductFlowChartModel> nextPPChartList = CalNextFlowChart(hangerProcessFlowChartList, ppChart, isBridgeOutSite);
                var nextPPChart = nextPPChartList.Count > 0 ? nextPPChartList.OrderBy(f => f.FlowIndex.Value).First() : null;

                //是否是存储站出战:存储站不记录产量
                var statingRole = ppChart.StatingRoleCode?.Trim();
                var storeStatingRoleCode = StatingType.StatingStorage.Code?.Trim();
                var isStoreStatingOutSite = storeStatingRoleCode.Equals(statingRole);
                var IsStorageStatingAgainAllocationedSeamsStating = false;
                if (isStoreStatingOutSite)
                {
                    nextPPChart = ppChart;
                    IsStorageStatingAgainAllocationedSeamsStating = true;
                }

                //更新本站工序
                ppChart.OutSiteDate = DateTime.Now;
                ppChart.FlowRealyProductStatingNo = (short)tenStatingNo;
                ppChart.IsFlowSucess = true;
                ppChart.Status = HangerProductFlowChartStaus.Successed.Value;//生产完成

                hangerProcessFlowChartList.Where(f => f.FlowNo.Equals(ppChart.FlowNo) && f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value && f.StatingNo != null && f.StatingNo.Value == ppChart.MainTrackNumber.Value && f.Status != HangerProductFlowChartStaus.Successed.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpccc)
                  {
                  //更新本站工序
                  hpccc.OutSiteDate = DateTime.Now;
                      hpccc.FlowRealyProductStatingNo = (short)tenStatingNo;
                      hpccc.IsFlowSucess = true;
                      hpccc.Status = HangerProductFlowChartStaus.Successed.Value;//生产完成

              });

                //合并工序处理
                CANProductsService.Instance.MergeProcessFlowChartFlowHanlder(ppChart, ref hangerProcessFlowChartList);
                var nextFlowIndex = -1; //obj.FlowIndex + (short)1;
                                        //var fchartId = ppChart.ProcessChartId;
                if (nextPPChart != null)
                {
                    nextFlowIndex = nextPPChart.FlowIndex.Value;
                }
                //zxl 2018年7月22日 10:28:14
                //OutSiteService.Instance.GetHangerNextSite(int.Parse(mainTrackNo), hangerNo, flowIndex, ref nextStatingNo, ref pfcFlowRelation, ref outMainTrackNumber, ref hpFlowChart);

                WaitProcessOrderHanger p = CorrectWaitProcessOrderHanger(tenMaintracknumber, tenStatingNo, tenHangerNo);

                //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo); //dicCurrentHangerProductingFlowModelCache[tenHangerNo.ToString()];
                var flowIsSuccess = FlowService.Instance.FlowIsSuccess(int.Parse(tenHangerNo), hangerProcessFlowChartList, tenMaintracknumber + "", tenStatingNo + "");

                //【获取下一站】
                ProcessFlowChartFlowRelation pfcFlowRelation = null;
                string nextStatingNo = null;
                int outMainTrackNumber = 0;
                HangerProductFlowChartModel hpFlowChart = null;
                if (!flowIsSuccess)
                    OutSiteService.Instance.GetHangerNextSiteExt(p, nextFlowIndex, ref nextStatingNo, ref outMainTrackNumber, ref hpFlowChart, ref pfcFlowRelation, isStoreStatingOutSite);
                hpFlowChart = nextPPChart;

                CorrectStoreStatingOutSite(hangerProcessFlowChartList, ppChart, isStoreStatingOutSite);

                CorrectFlowAndStatingNumAndHangerResumeAndFlowYieldAndEtc(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, ppChart, nextStatingNo, outMainTrackNumber, hpFlowChart);
                //更新当前衣架缓存
                // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerProcessFlowChartList;
                NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerProcessFlowChartList);
                var isBridgeRequire = false;
                if (!isStoreStatingOutSite)
                {
                    if (outMainTrackNumber != 0 && outMainTrackNumber != tenMaintracknumber)
                    {
                        isBridgeRequire = true;
                    }
                }
                //需要桥接
                if (isBridgeRequire)
                {
                    BridgeService.Instance.BridgeHandler(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, outMainTrackNumber, nextStatingNo, nextPPChart);
                    return;
                }

                //桥接出战且携带工序，清空反向不携带工序的站内数明细及站内数，及携带工序的站内数
                if (isBridgeOutSite && ppChart != null)
                {
                    var reverseBridge = CANProductsService.Instance.GetReverseBridge(tenMaintracknumber + "", tenStatingNo + "");
                    HangerProductFlowChartModel nonFlow = null;
                    var reverseBridgeInFlowChartAndFlowNonSuccess = CANProductsService.Instance.IsBridgeContainsFlowNonSuccess(reverseBridge.AMainTrackNumber.Value + "", reverseBridge.ASiteNo.Value + "", hangerProcessFlowChartList, ref nonFlow);
                    //桥接不携带工序
                    //修正删除的站内数及明细、缓存
                    var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssoc.Action = reverseBridgeInFlowChartAndFlowNonSuccess ? 15 : 12;
                    hnssoc.HangerNo = tenHangerNo;
                    hnssoc.MainTrackNumber = reverseBridge.AMainTrackNumber.Value;
                    if (reverseBridgeInFlowChartAndFlowNonSuccess)
                    {
                        hnssoc.FlowNo = nonFlow.FlowNo?.Trim();
                    }
                    hnssoc.StatingNo = reverseBridge.ASiteNo.Value;
                    var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);

                }
                //生产完成，对站内数和分配数更新到缓存
                //【衣架回流】生产完成，且给挂片站分配衣架
                if (string.IsNullOrEmpty(nextStatingNo))
                {

                    var isHangingPieceStating = new ProductsQueryServiceImpl().isHangingPiece(null, tenStatingNo + "", tenMaintracknumber + "");
                    if (!isHangingPieceStating)
                    {
                        ////出站站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(statingNo), OnLineSum = -1 };
                        //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        //最后一道工序出战站内数及分配标识修正
                        CANProductsService.LastFlowStatingHandler(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, ppChart);
                    }
                    //还得验证是否需要桥接？

                    //【衣架回流】生产完成，且给挂片站分配衣架
                    outMainTrackNumber = HangerProductSuccessHandler(tenMaintracknumber, tenStatingNo, tenHangerNo, outMainTrackNumber);
                    //修正最后一道高工序衣架轨迹为完成
                    CANProductsService.Instance.CorrectHangerResumeLastFlowSuccess(tenMaintracknumber, tenStatingNo, tenHangerNo, ppChart?.FlowNo?.Trim());
                    //衣架完成修正产出工序
                    CANProductsService.Instance.CorrectHangerSuccessOutFlow(tenHangerNo, ppChart);
                    return;
                }
                // var flowIndex = nextPPChart.FlowIndex.Value;
                nextFlowIndex = CorrectNextFlowStatingAllocationAndStatingNumAndEtc(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, ppChart, nextPPChart, IsStorageStatingAgainAllocationedSeamsStating, nextFlowIndex, nextStatingNo, outMainTrackNumber);

                LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo, nextStatingNo, outMainTrackNumber);

                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                {
                    HangerNo = tenHangerNo + "",
                    MainTrackNumber = (short)outMainTrackNumber,
                    SiteNo = nextStatingNo + ""
                     ,
                    AllocatingStatingDate = DateTime.Now
                });
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

            }
        }

        /// <summary>
        /// 【衣架回流】
        /// 衣架生产完成处理:1.给挂片站分配指令
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <returns></returns>
        public int HangerProductSuccessHandler(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, int outMainTrackNumber)
        {
            var sucessMessage = string.Format("【衣架回流】 主轨【{0}】 站点{1} 衣架【{2}】 生产完成!", tenMaintracknumber, tenStatingNo, tenHangerNo);
            tcpLogInfo.Info(sucessMessage);
            int outSiteStatingNo = 0;
            //【衣架回流】--->【给挂片站分配衣架】
            CANProductsValidService.Instance.GetSucessHangerNoHangingPieceStating(tenHangerNo.ToString(), tenMaintracknumber, tenStatingNo, ref outMainTrackNumber, ref outSiteStatingNo);
            //    var tenSucessHangerHangingPieceMainTrackNumber = outMainTrackNumber.ToString(); //HexHelper.TenToHexString2Len(outMainTrackNumber);
            // var hexOutSiteStatingNo = HexHelper.TenToHexString2Len(outSiteStatingNo);

            //给下一站分配衣架
            LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo, outSiteStatingNo + "", outMainTrackNumber);
            //  CANTcpServer.server.AllocationHangerToNextStating(tenSucessHangerHangingPieceMainTrackNumber, hexOutSiteStatingNo, HexHelper.TenToHexString10Len(tenHangerNo));
            var sucessHangerHangingMessage = string.Format("【衣架回流】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", outMainTrackNumber, outSiteStatingNo);
            tcpLogInfo.Info(sucessHangerHangingMessage);
            //挂片分配数及明细处理
            CorrecHangerReturnStatingStatingNumHandler(tenHangerNo, outMainTrackNumber, outSiteStatingNo + "", -2);

            var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            {
                HangerNo = tenHangerNo + "",
                MainTrackNumber = (short)outMainTrackNumber,
                SiteNo = outSiteStatingNo + ""
                 ,
                AllocatingStatingDate = DateTime.Now
            });
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

            return outMainTrackNumber;
        }
        /// <summary>
        /// 衣架回流挂片站 站内数，分配数修正
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="action">-2:回流分配;-3:回流进站</param>
        public void CorrecHangerReturnStatingStatingNumHandler(string hangerNo, int outMainTrackNumber, string nextStatingNo, int action)
        {
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = action;
            hnssoc.HangerNo = hangerNo;
            hnssoc.MainTrackNumber = outMainTrackNumber;
            hnssoc.StatingNo = int.Parse(nextStatingNo);
            hnssoc.FlowNo = string.Empty;
            hnssoc.FlowIndex = -2;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
        }

        /// <summary>
        /// 当前衣架信息校正
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <returns></returns>
        public  WaitProcessOrderHanger CorrectWaitProcessOrderHanger(int tenMaintracknumber, int tenStatingNo, string tenHangerNo)
        {
            lock (locObject)
            {
                WaitProcessOrderHanger p = null;
                var dicWaitProcessOrderHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER);
                dicWaitProcessOrderHanger.TryGetValue(tenHangerNo?.Trim(), out p);
                p.SiteNo = tenStatingNo + "";
                p.MainTrackNumber = short.Parse(tenMaintracknumber + "");
                return p;
            }
        }

        private List<HangerProductFlowChartModel> CalNextFlowChart(List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart, bool isBridgeOutSite = false)
        {
            if (isBridgeOutSite)
            {
                var nextPPChartList1 = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value//currentFlowIndex
      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
      && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
      || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
      ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
                if (nextPPChartList1.Count() == 0)
                {
                    nextPPChartList1 = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value != ppChart.FlowIndex.Value//currentFlowIndex
       && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
       && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
       || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
       ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
                }

                return nextPPChartList1;
            }
            var nextPPChartList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value//currentFlowIndex
        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
            return nextPPChartList;
        }


        /// <summary>
        /// 当前站点信息校正:1.本站衣架生产履历Cache写入;2.【记录产量】;3.更新等待衣架缓存;4.修正出战 站点的分配数，站内数
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="hpFlowChart"></param>
        public  void CorrectFlowAndStatingNumAndHangerResumeAndFlowYieldAndEtc(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart, string nextStatingNo, int outMainTrackNumber, HangerProductFlowChartModel hpFlowChart)
        {
            //【衣架生产履历】本站衣架生产履历Cache写入
            CANProductsService.Instance.RecordHangerOutSiteProductResume(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, ppChart);


            //【记录产量】
            CANProductsService.Instance.RecordEmployeeFlowYieldHandler(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, hangerProcessFlowChartList, ppChart, nextStatingNo);
            //更新等待衣架缓存
            CANProductsService.Instance.CorrectWaitForHangerCache(tenHangerNo, nextStatingNo, hpFlowChart);

            //修正出战 站点的分配数，站内数
            CANProductsService.Instance.CorrectStatingNumAndCacheByOutSite(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, ppChart);
        }
        /// <summary>
        /// 下一站数据校正:
        /// 1.更新衣架分配记录为处理状态到缓存;讲当前出战衣架的分配记录的出战时间和工序完成状态;
        /// 2.修正下一道工序的站点分配cache及db维护;
        /// 3.记录衣架分配;
        /// 4.//修正删除的站内数及明细、缓存;
        /// 5.【衣架生产履历】下一站分配Cache写入;
        /// 6./更新【衣架工艺图】衣架下一站的站点分配状态和时间
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="IsStorageStatingAgainAllocationedSeamsStating"></param>
        /// <param name="nextFlowIndex"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <returns></returns>
        public static int CorrectNextFlowStatingAllocationAndStatingNumAndEtc(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart, HangerProductFlowChartModel nextPPChart, bool IsStorageStatingAgainAllocationedSeamsStating, int nextFlowIndex, string nextStatingNo, int outMainTrackNumber)
        {
            var currentFlowIndex = ppChart.FlowIndex.Value;
            //更新衣架分配记录为处理状态到缓存
            //讲当前出战衣架的分配记录的出战时间和工序完成状态
            List<HangerStatingAllocationItem> dicHangerStatingALloList = CANProductsService.CorrectCurrentFlowAllotcationStatusToComplete(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, ppChart);

            //var nextFlowIndex = nextPPChart.FlowIndex.Value;
            var nextHangerFlowChartList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value == nextFlowIndex && f.StatingNo.Value == short.Parse(nextStatingNo) && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
            if (nextHangerFlowChartList.Count > 0)
            {
                HangerProductFlowChartModel nextHangerFlowChart;
                HangerStatingAllocationItem nextHangerStatingAllocationItem;
                //修正下一道工序的站点分配cache及db维护
                CANProductsService.Instance.CorrectNextFlowAllocationCache(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, ppChart, nextPPChart, nextFlowIndex, currentFlowIndex, nextStatingNo, dicHangerStatingALloList, out nextFlowIndex, nextHangerFlowChartList, out nextHangerFlowChart, out nextHangerStatingAllocationItem, false);

                ////记录衣架分配
                //var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                // //修正删除的站内数及明细、缓存
                CANProductsService.CorrectStatingNumAndCacheByAllocation(tenHangerNo, outMainTrackNumber, nextStatingNo, nextHangerFlowChart);
                var isRequireBridge = CANProductsService.Instance.IsRequireBridge(tenHangerNo, hangerProcessFlowChartList);
                var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(outMainTrackNumber.ToString(), nextStatingNo);
                if (!isBridgeOutSite || !isRequireBridge)
                {
                    //发布衣架下一站工序状态
                    CANProductsService.Instance.CorrectHangerNextFlowHandler(tenHangerNo, nextPPChart);
                }
                else
                {
                    //下一站桥接的对面桥接携带工序
                    var isBridgeInverseBridge = CANProductsService.Instance.IsBridgeInverseBridge(outMainTrackNumber.ToString(), nextStatingNo, hangerProcessFlowChartList);
                    if (isBridgeInverseBridge)
                    {
                        CANProductsService.Instance.RecalibrationHangerNextFlowHandler(tenHangerNo, outMainTrackNumber, hangerProcessFlowChartList);
                    }
                }


                //【衣架生产履历】下一站分配Cache写入
                CANProductsService.RecordHangerNextSatingAllocationResume(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo, nextPPChart, nextStatingNo);

            }
            //更新【衣架工艺图】衣架下一站的站点分配状态和时间
            CANProductsService.CorrectNextFlowAllocationStatusToCacheByAllocationSucess(tenHangerNo, outMainTrackNumber, IsStorageStatingAgainAllocationedSeamsStating, nextStatingNo, nextFlowIndex);
            return nextFlowIndex;
        }
        /// <summary>
        /// 存储站出战数据校正
        /// </summary>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <param name="ppChart"></param>
        /// <param name="isStoreStatingOutSite"></param>
        public  void CorrectStoreStatingOutSite(List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart, bool isStoreStatingOutSite)
        {
            #region//存储站出战恢复站点状态
            if (isStoreStatingOutSite)
            {
                hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value &&
                  f.StatingNo.Value == ppChart.StatingNo.Value).ToList().ForEach(delegate (HangerProductFlowChartModel fc)
                  {
                      fc.AllocationedDate = null;
                      fc.IncomeSiteDate = null;
                      fc.OutSiteDate = null;
                      fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                      fc.IsFlowSucess = false;
                      fc.IsStorageStatingOutSite = true;
                  });
            }
            else
            {
                //刷新同工序的存储站点的状态
                var fIndex = ppChart.FlowIndex.Value;
                hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value &&
                 f.FlowIndex.Value == fIndex && null != f.StatingRoleCode && f.StatingRoleCode.Equals(StatingType.StatingStorage.Code)).ToList().ForEach(delegate (HangerProductFlowChartModel fc)
                 {
                     fc.AllocationedDate = null;
                     fc.IncomeSiteDate = null;
                     fc.OutSiteDate = null;
                     fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                     fc.IsFlowSucess = false;
                     fc.IsStorageStatingOutSite = false;
                 });
            }
            #endregion
        }
    }
}

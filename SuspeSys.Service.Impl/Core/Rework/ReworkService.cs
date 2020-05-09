using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Core.Flow;
using SuspeSys.Service.Impl.Core.OutSite;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuspeSys.Service.Impl.Core.Rework
{
    public class ReworkService
    {
        public readonly static ReworkService Instance = new ReworkService();
        private static readonly object locObject = new object();
        private ReworkService() { }
        /// <summary>
        /// 返工的衣架的处理
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="hangerProcessFlowChartLis"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="flowIndex"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hpFlowChart"></param>
        public void ReworkHangerHandler(int tenMainTrackNumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, HangerProductFlowChartModel ppChart)
        {
            lock (locObject)
            {
                //var outMainTrackNumber = 0;
                // HangerProductFlowChartModel ppChart = null;
                var nextStatingNo = string.Empty;
                var isBridgeOutSite = CANProductsService.Instance.IsBridgeOutSite(tenMainTrackNumber.ToString(), tenStatingNo.ToString());
                var isReworkFlow = CANProductsService.Instance.IsReworkFlow(tenMainTrackNumber, tenStatingNo, tenHangerNo);

                // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo);//dicCurrentHangerProductingFlowModelCache[tenHangerNo.ToString()];

                //找返工发起站点
                List<HangerProductFlowChartModel> reworkFlowStatlist = GetReworkStatingListOrLaunchReworkStatingList(tenHangerNo, hangerProcessFlowChartLis, current, isBridgeOutSite, isReworkFlow, ref ppChart);
                if (isBridgeOutSite)
                {
                    if (isReworkFlow)//桥接站携带工序被反攻
                    {
                        ReworkHangerOutSiteHandler(tenMainTrackNumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartLis, ppChart, reworkFlowStatlist);
                        return;
                    }
                    OutSiteCommonHangerHandler.Instance.HangerOutSiteHandler(tenMainTrackNumber, tenStatingNo, tenHangerNo, null, null);
                    return;
                }
                ReworkHangerOutSiteHandler(tenMainTrackNumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartLis, ppChart, reworkFlowStatlist);
                return;
                #region decode
                ////下道工序是否存在返工
                //var nextFlowIsReworkFlow = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextPPChart.FlowIndex.Value &&
                // k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1).Count() > 0;
                //if (!nextFlowIsReworkFlow)//不存在直接找发起返工的源头站点
                //{
                //    //找返工发起站点
                //    List<ProductsProcessOrderModel> reworkFlowStatlist = GetReworkStatingListOrLaunchReworkStatingList(tenHangerNo, hangerProcessFlowChartLis, current, isBridgeOutSite, isReworkFlow);
                //    //【获取下一站】
                //    OutSiteService.Instance.AllocationNextProcessFlowStating(reworkFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
                //    //  hpFlowChart = nextPPChart;
                //    //  flowIndex = nextPPChart.FlowIndex.Value;
                //}
                //else
                //{

                //    //找下一返工站点
                //    var reworkFlowStatlist = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextPPChart.FlowIndex.Value &&
                //    k.StatingNo != null && k.StatingNo.Value != -1 && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1
                //    && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                //    ).Select(f => new ProductsProcessOrderModel()
                //    {
                //        StatingNo = f.StatingNo.ToString(),
                //        MainTrackNumber = (int)f.MainTrackNumber,
                //        StatingCapacity = f.StatingCapacity.Value,
                //        Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                //        ProcessChartId = f.ProcessChartId,
                //        FlowNo = f.FlowNo,
                //        StatingRoleCode = f.StatingRoleCode

                //    }).ToList<ProductsProcessOrderModel>();
                //    if (reworkFlowStatlist.Count == 0)
                //    {
                //        //下一道没有可以接收衣架的站
                //        throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", nextPPChart.MainTrackNumber, nextPPChart.StatingNo, tenHangerNo?.Trim()))
                //        {
                //            FlowNo = nextPPChart?.FlowNo?.Trim()
                //        };
                //    }
                //    //【获取下一站】
                //    OutSiteService.Instance.AllocationNextProcessFlowStating(reworkFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
                //    //  hpFlowChart = nextPPChart;
                //    //  flowIndex = nextPPChart.FlowIndex.Value;
                //}
                #endregion
            }

        }

        /// <summary>
        /// 返工衣架处理
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="v"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <param name="ppChart"></param>
        internal void ReworkHangerOutSiteHandler(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, HangerProductFlowChartModel ppChart, List<HangerProductFlowChartModel> nextPPChartList)
        {
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

            WaitProcessOrderHanger p = OutSiteCommonHangerHandler.Instance.CorrectWaitProcessOrderHanger(tenMaintracknumber, tenStatingNo, tenHangerNo);

            //   var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo);// dicCurrentHangerProductingFlowModelCache[tenHangerNo.ToString()];
            var flowIsSuccess = FlowService.Instance.FlowIsSuccess(int.Parse(tenHangerNo), hangerProcessFlowChartList, tenMaintracknumber + "", tenStatingNo + "");

           string nextStatingNo = null;
            int outMainTrackNumber = 0;
            HangerProductFlowChartModel hpFlowChart = null;
            hpFlowChart = nextPPChart;
            outMainTrackNumber = nextPPChart.MainTrackNumber.Value;
            nextStatingNo = nextPPChart.StatingNo.Value+"";
            OutSiteCommonHangerHandler.Instance.CorrectStoreStatingOutSite(hangerProcessFlowChartList, ppChart, isStoreStatingOutSite);

            OutSiteCommonHangerHandler.Instance.CorrectFlowAndStatingNumAndHangerResumeAndFlowYieldAndEtc(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, ppChart, nextStatingNo, outMainTrackNumber, hpFlowChart);
            //更新当前衣架缓存
            //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerProcessFlowChartList;
            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo,hangerProcessFlowChartList);
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
                BridgeService.Instance.BridgeHandler(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, outMainTrackNumber,nextStatingNo,nextPPChart);
                return;
            }

            //生产完成，对站内数和分配数更新到缓存
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
                outMainTrackNumber = OutSiteCommonHangerHandler.Instance.HangerProductSuccessHandler(tenMaintracknumber, tenStatingNo, tenHangerNo, outMainTrackNumber);
                return;
            }
            // var flowIndex = nextPPChart.FlowIndex.Value;
            nextFlowIndex = OutSiteCommonHangerHandler.CorrectNextFlowStatingAllocationAndStatingNumAndEtc(tenMaintracknumber, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, ppChart, nextPPChart, IsStorageStatingAgainAllocationedSeamsStating, nextFlowIndex, nextStatingNo, outMainTrackNumber);

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

        private static List<HangerProductFlowChartModel> GetReworkStatingListOrLaunchReworkStatingList(string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current, bool isBridgeOutSite, bool isReworkFlow, ref HangerProductFlowChartModel ppChartResult)
        {
            HangerProductFlowChartModel ppChart = null;

            if (isBridgeOutSite)
            {
                if (isReworkFlow)
                {
                    ppChart = hangerProcessFlowChartLis.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value == current.StatingNo && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
                    ppChartResult = ppChart;
                    var nextFlowIsReworkFlow = hangerProcessFlowChartLis.Where(k => k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1 && ppChart.FlowIndex.Value != k.FlowIndex.Value).Count() > 0;
                    if (nextFlowIsReworkFlow)
                    {
                        var nextFlowChart = hangerProcessFlowChartLis.Where(k => k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1 && ppChart.FlowIndex.Value != k.FlowIndex.Value).OrderBy(f => f.FlowIndex.Value).First();

                        #region//找下一返工站点
                        var reworkFlowStatlist1 = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextFlowChart.FlowIndex.Value &&
                        k.StatingNo != null && k.StatingNo.Value != -1 && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1
                        && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                        ).Select(f => new HangerProductFlowChartModel()
                        {
                            StatingNo = f.StatingNo,
                            MainTrackNumber = f.MainTrackNumber,
                            StatingCapacity = f.StatingCapacity.Value,
                            Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                            ProcessChartId = f.ProcessChartId,
                            FlowNo = f.FlowNo,
                            StatingRoleCode = f.StatingRoleCode,
                            FlowIndex=f.FlowIndex,
                            FlowType=f.FlowType,
                            CheckInfo=f.CheckInfo,
                            CheckResult=f.CheckResult,
                            Status=f.Status

                        }).ToList<HangerProductFlowChartModel>();
                        if (reworkFlowStatlist1.Count == 0)
                        {
                            //下一道没有可以接收衣架的站
                            throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", nextFlowChart.MainTrackNumber, nextFlowChart.StatingNo, tenHangerNo?.Trim()))
                            {
                                FlowNo = nextFlowChart?.FlowNo?.Trim()
                            };
                        }
                        #endregion

                        return reworkFlowStatlist1;
                    }

                    #region//找发起返工源头站点
                    var reworkFlowStatlist2 = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == current.FlowIndex &&
           k.StatingNo.Value == current.StatingNo && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
           && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
           ).Select(f => new HangerProductFlowChartModel()
           {
               StatingNo = f.StatingNo,
               MainTrackNumber = f.MainTrackNumber,
               StatingCapacity = f.StatingCapacity.Value,
               Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
               ProcessChartId = f.ProcessChartId,
               FlowNo = f.FlowNo,
               StatingRoleCode = f.StatingRoleCode,
               FlowIndex =f.FlowIndex,
               FlowType = f.FlowType,
               CheckInfo = f.CheckInfo,
               CheckResult = f.CheckResult
               ,
               Status = f.Status
           }).ToList<HangerProductFlowChartModel>();
                    if (reworkFlowStatlist2.Count == 0)
                    {
                        //下一道没有可以接收衣架的站
                        throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", current.MainTrackNumber, current.StatingNo, tenHangerNo?.Trim()))
                        {
                            FlowNo = current?.FlowNo?.Trim()
                        };
                    }
                    #endregion

                    return reworkFlowStatlist2;
                }

                #region 找下一道返工工序
                var reworkFlowStatlist3 = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == current.FlowIndex &&
           k.StatingNo.Value == current.StatingNo && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
           && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
           ).Select(f => new HangerProductFlowChartModel()
           {
               StatingNo = f.StatingNo,
               MainTrackNumber = f.MainTrackNumber,
               StatingCapacity = f.StatingCapacity.Value,
               Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
               ProcessChartId = f.ProcessChartId,
               FlowNo = f.FlowNo,
               StatingRoleCode = f.StatingRoleCode,
               FlowIndex = f.FlowIndex,
               FlowType = f.FlowType,
               CheckInfo = f.CheckInfo,
               CheckResult = f.CheckResult
               ,
               Status = f.Status
           }).ToList<HangerProductFlowChartModel>();
                if (reworkFlowStatlist3.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", current.MainTrackNumber, current.StatingNo, tenHangerNo?.Trim()))
                    {
                        FlowNo = current?.FlowNo?.Trim()
                    };
                }
                #endregion

                return reworkFlowStatlist3;
            }

            var nextFlowIsReworkFlow2 = hangerProcessFlowChartLis.Where(k => k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1 && current.FlowIndex != k.FlowIndex.Value).Count() > 0;
            if (nextFlowIsReworkFlow2)
            {
                var nextFlowChart = hangerProcessFlowChartLis.Where(k => k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1 && current.FlowIndex != k.FlowIndex.Value).OrderBy(f => f.FlowIndex.Value).First();

                #region//找下一返工站点
                var reworkFlowStatlist1 = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextFlowChart.FlowIndex.Value &&
                k.StatingNo != null && k.StatingNo.Value != -1 && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1
                && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                ).Select(f => new HangerProductFlowChartModel()
                {
                    StatingNo = f.StatingNo,
                    MainTrackNumber = f.MainTrackNumber,
                    StatingCapacity = f.StatingCapacity.Value,
                    Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                    ProcessChartId = f.ProcessChartId,
                    FlowNo = f.FlowNo,
                    StatingRoleCode = f.StatingRoleCode,
                    FlowIndex = f.FlowIndex,
                    FlowType = f.FlowType,
                    CheckInfo = f.CheckInfo,
                    CheckResult = f.CheckResult
                    ,
                    Status = f.Status
                }).ToList<HangerProductFlowChartModel>();
                if (reworkFlowStatlist1.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", nextFlowChart.MainTrackNumber, nextFlowChart.StatingNo, tenHangerNo?.Trim()))
                    {
                        FlowNo = nextFlowChart?.FlowNo?.Trim()
                    };
                }
                #endregion

                return reworkFlowStatlist1;
            }
            #region//找发起返工源头站点

            var reworkFlowStatlist = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value > current.FlowIndex && !k.FlowNo.Equals(current.FlowNo) 
            && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
            && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
            ).Select(f => new HangerProductFlowChartModel()
            {
                StatingNo = f.StatingNo,
                MainTrackNumber = f.MainTrackNumber,
                StatingCapacity = f.StatingCapacity.Value,
                Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                ProcessChartId = f.ProcessChartId,
                FlowNo = f.FlowNo,
                StatingRoleCode = f.StatingRoleCode,
                FlowIndex = f.FlowIndex,
                FlowType = f.FlowType,
                CheckInfo = f.CheckInfo,
                CheckResult = f.CheckResult
                ,
                Status = f.Status
            }).ToList<HangerProductFlowChartModel>();
            if (reworkFlowStatlist.Count == 0)
            {
                //下一道没有可以接收衣架的站
                throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", current.MainTrackNumber, current.StatingNo, tenHangerNo?.Trim()))
                {
                    FlowNo = current?.FlowNo?.Trim()
                };
            }
            #endregion

            return reworkFlowStatlist;
        }
    }
}

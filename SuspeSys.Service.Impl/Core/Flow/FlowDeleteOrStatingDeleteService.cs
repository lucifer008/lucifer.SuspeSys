using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Bridge;
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

namespace SuspeSys.Service.Impl.Core.Flow
{
    public class FlowDeleteOrStatingDeleteService : SusLog
    {
        private FlowDeleteOrStatingDeleteService() { }
        public readonly static FlowDeleteOrStatingDeleteService Instance = new FlowDeleteOrStatingDeleteService();

        /// <summary>
        /// 工序删除或者站点删除处理
        /// </summary>
        /// <param name="fdsdEx"></param>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        public void FlowDeleteOrStatingDeletedHandler(FlowDeleteOrStatingDeletedException fdsdEx, string mainTrackNo, string statingNo)
        {
            //  int outMainTrackNumber;

            var hangerNo = fdsdEx.HangerNo.ToString();
           // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(fdsdEx.HangerNo+""))//dicHangerProcessFlowChart.ContainsKey(fdsdEx.HangerNo.ToString()))
            {
                var eeInfo = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】衣架不在工艺图上!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString());
                tcpLogError.Error(eeInfo);
                throw new ApplicationException(eeInfo);
            }
            var fChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo+"");//dicHangerProcessFlowChart[fdsdEx.HangerNo.ToString()];
            //同工序是否存在站点
            var equalFlowIsExistOtherStating = false;
            var isDeleteFlow = false;
            var isOnlyMoveFlow = fdsdEx.IsOnlyMoveFlow;//是否仅移动工序
            IEnumerable<int?> nextFlowIndexList = null;
            equalFlowIsExistOtherStating = fdsdEx.EqualFlowIsExistOtherStating;
            //不存在同工序其他站点
            if (!fdsdEx.EqualFlowIsExistOtherStating)
            {

                nextFlowIndexList = fChartList.Where(k => k.FlowIndex.Value >= fdsdEx.NextFlowIndex
                && k.StatingNo != null && k.StatingNo.Value != -1
                && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                && null != k.StatingRoleCode
              //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
              ).Select(f => f.FlowIndex);

                //当前工序删除，生产顺序上移，即下道工序是未完成的,查询当前工序生产顺序是否有未完成的工序
                if (nextFlowIndexList.Count() == 0)
                {
                    nextFlowIndexList = fChartList.Where(k => k.FlowIndex.Value > fdsdEx.NextFlowIndex
                     && k.StatingNo != null && k.StatingNo.Value != -1
                     && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                     && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                     && null != k.StatingRoleCode
                   //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
                   ).Select(f => f.FlowIndex);
                }
            }
            else
            {
                nextFlowIndexList = fChartList.Where(k => k.FlowIndex.Value == fdsdEx.NextFlowIndex
                   && k.StatingNo != null && k.StatingNo.Value != -1
                   && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                   && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                   && null != k.StatingRoleCode
                 //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
                 ).Select(f => f.FlowIndex);
            }
            var nextFlowIndex = nextFlowIndexList.Count() > 0 ? nextFlowIndexList.Min().Value : -1;

            var nextFlowStatlist = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex
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
                var ffList = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex);
                var exx = new NoFoundStatingException(string.Format("【工序或者站点删除】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString()))
                {
                    FlowNo = ffList.Count() > 0 ? ffList.First().FlowNo : "-1"
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            int outMainTrackNumber = 0;
            string nextStatingNo = null;
            //【获取下一站】
            OutSiteService.Instance.AllocationNextProcessFlowStating(nextFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
            if (string.IsNullOrEmpty(nextStatingNo))
            {
                var exx = new NoFoundStatingException(string.Format("【工序或者站点删除】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString()))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex).First().FlowNo
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            var isStoreStatingOutSite = false;
            var isBridgeRequire = false;
            if (!isStoreStatingOutSite)
            {
                if (outMainTrackNumber != 0 && outMainTrackNumber != int.Parse(mainTrackNo))
                {
                    isBridgeRequire = true;
                }
            }
            // var nextPPChart = fChartList.Where(f => f.FlowIndex.Value == nextFlowIndex && f.MainTrackNumber.Value == outMainTrackNumber && f.StatingNo.Value == int.Parse(nextStatingNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
            // var hangerProcessFlowChartList= dicHangerProcessFlowChart[fdsdEx.HangerNo.ToString()];


            //更新衣架分配记录为处理状态到缓存
            //讲当前出战衣架的分配记录的出战时间和工序完成状态
            var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo];
            var ntStatingNo = nextStatingNo;
            //  var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); // dicHangerProcessFlowChart[hangerNo];
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
            var nextHangerFlowChartList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value == nextFlowIndex && f.StatingNo.Value == short.Parse(ntStatingNo) && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
            if (nextHangerFlowChartList.Count == 0)
            {
                var exx = new NoFoundStatingException(string.Format("【工序或者站点删除】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString()))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex).First().FlowNo
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current = null;
            // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            current = NewCacheService.Instance.GetHangerCurrentFlow(fdsdEx.HangerNo+"");//dicCurrentHangerProductingFlowModelCache[fdsdEx.HangerNo.ToString()];
            var cflowChartList = fChartList.Where(f => f.FlowNo.Equals(current.FlowNo));
            var nextHangerFlowChart = nextHangerFlowChartList[0];
            isDeleteFlow = nextHangerFlowChartList.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() == 0;
            //是否记录产量(移动工序的情况)
            var isRecordProducts = nextHangerFlowChartList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value == current.StatingNo).Select(k => k.FlowIndex).Count() > 0;

            //需要桥接
            if (isBridgeRequire)
            {
                BridgeService.Instance.BridgeHandler(int.Parse(mainTrackNo), int.Parse(statingNo), hangerNo, fChartList, outMainTrackNumber, nextStatingNo, nextHangerFlowChart);
                //是否记录产量(移动工序的情况)
                if (isRecordProducts)
                {
                    RecordYieldByMoveFow(mainTrackNo, statingNo, hangerNo, fChartList, current);
                    //var newFlowIndex = dicHangerProcessFlowChart[fdsdEx.HangerNo.ToString()].Where(f => f.FlowNo.Equals(current.FlowNo)).Select(k => k.FlowIndex).First().Value;
                }

                //工序是否可接收

                if (cflowChartList.Count() > 0)
                {
                    CurrentStatingOutSiteCorrectHangerResume(mainTrackNo, statingNo, hangerNo, cflowChartList);
                }

                //修正删除的站内衣架数
                if (equalFlowIsExistOtherStating)
                {
                    /// 1.修正删除的站内数及明细、缓存
                    /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
                    EqualFowExistOtherStatingCorrectStatingNumCacheAndHangerProductResumeCache(hangerNo, current);

                }
                if (isDeleteFlow)
                {
                    /// 删除工序
                    /// 1.修正删除的站内数及明细、缓存
                    /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
                    DeleteFlowCorrectStatingNumCacheAndHangerProductReumeCache(hangerNo, current);
                }
                if (isOnlyMoveFlow)
                {
                    //出战：修正删除的站内数及明细、缓存
                    OnlyMoveFlowCorrectStatingInItemCache(hangerNo, current);

                }
                return;
            }
            HangerAllocationHandler(fdsdEx, statingNo, hangerNo, outMainTrackNumber, nextStatingNo, dicHangerStatingALloList, hangerProcessFlowChartList, nextHangerFlowChart);


            //是否记录产量(移动工序的情况)
            if (isRecordProducts)
            {
                RecordYieldByMoveFow(mainTrackNo, statingNo, hangerNo, fChartList, current);
                //var newFlowIndex = dicHangerProcessFlowChart[fdsdEx.HangerNo.ToString()].Where(f => f.FlowNo.Equals(current.FlowNo)).Select(k => k.FlowIndex).First().Value;
            }


            //【衣架生产履历】本站衣架生产履历Cache写入

            //工序是否可接收

            if (cflowChartList.Count() > 0)
            {
                CurrentStatingOutSiteCorrectHangerResume(mainTrackNo, statingNo, hangerNo, cflowChartList);
            }


            //【衣架生产履历】下一站分配Cache写入
            NextStatingAlloctionResumeCacheHandler(mainTrackNo, statingNo, hangerNo, nextStatingNo, nextHangerFlowChart);

            //发布衣架状态-->衣架下一道工序状态:
            HangerStatusCorrect(hangerNo, outMainTrackNumber, nextStatingNo, nextHangerFlowChart);

            //修正删除的站内衣架数
            if (equalFlowIsExistOtherStating)
            {
                /// 1.修正删除的站内数及明细、缓存
                /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
                EqualFowExistOtherStatingCorrectStatingNumCacheAndHangerProductResumeCache(hangerNo, current);

            }
            if (isDeleteFlow)
            {
                /// 删除工序
                /// 1.修正删除的站内数及明细、缓存
                /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
                DeleteFlowCorrectStatingNumCacheAndHangerProductReumeCache(hangerNo, current);
            }
            if (isOnlyMoveFlow)
            {
                //出战：修正删除的站内数及明细、缓存
                OnlyMoveFlowCorrectStatingInItemCache(hangerNo, current);

            }
            //修正删除的站内数及明细、缓存
            CorrectNextStatingStatingNumHandler(hangerNo, outMainTrackNumber, nextStatingNo, nextHangerFlowChart);


            LowerPlaceInstr.Instance.AllocationHangerToNextStating(hangerNo, nextStatingNo, outMainTrackNumber);

            var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            {
                HangerNo = hangerNo + "",
                MainTrackNumber = (short)outMainTrackNumber,
                SiteNo = nextStatingNo + ""
                 ,
                AllocatingStatingDate = DateTime.Now
            });
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

        }
        /// <summary>
        /// 下一站 站内数，分配数修正
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        private static void CorrectNextStatingStatingNumHandler(string hangerNo, int outMainTrackNumber, string nextStatingNo, HangerProductFlowChartModel nextHangerFlowChart)
        {
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = 0;
            hnssoc.HangerNo = hangerNo;
            hnssoc.MainTrackNumber = outMainTrackNumber;
            hnssoc.StatingNo = int.Parse(nextStatingNo);
            hnssoc.FlowNo = nextHangerFlowChart.FlowNo;
            hnssoc.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
            hnssoc.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
        }

        /// <summary>
        /// 删除工序
        /// 1.修正删除的站内数及明细、缓存
        /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="current"></param>
        private static void DeleteFlowCorrectStatingNumCacheAndHangerProductReumeCache(string hangerNo, Domain.Cus.CurrentHangerProductingFlowModel current)
        {
            //修正删除的站内数及明细、缓存
            var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocDeleteStating.Action = 4;
            hnssocDeleteStating.HangerNo = hangerNo;
            hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
            hnssocDeleteStating.StatingNo = current.StatingNo;
            hnssocDeleteStating.FlowNo = current.FlowNo;
            hnssocDeleteStating.FlowIndex = current.FlowIndex;
            //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );

            //【衣架生产履历】站点删除 修正衣架生产履历Cache
            var hpResume = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo,
                StatingNo = current.StatingNo.ToString(),
                MainTrackNumber = current.MainTrackNumber,
                FlowNo = current.FlowNo,
                Action = 6
            };
            var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(),hangerResumeJson );
        }

        /// <summary>
        /// 1.修正删除的站内数及明细、缓存
        /// 2.【衣架生产履历】站点删除 修正衣架生产履历Cache
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="current"></param>
        private static void EqualFowExistOtherStatingCorrectStatingNumCacheAndHangerProductResumeCache(string hangerNo, Domain.Cus.CurrentHangerProductingFlowModel current)
        {
            //修正删除的站内数及明细、缓存
            var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocDeleteStating.Action = 3;
            hnssocDeleteStating.HangerNo = hangerNo;
            hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
            hnssocDeleteStating.StatingNo = current.StatingNo;
            hnssocDeleteStating.FlowNo = current.FlowNo;
            hnssocDeleteStating.FlowIndex = current.FlowIndex;
            //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );
            //【衣架生产履历】站点删除 修正衣架生产履历Cache
            var hpResume = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo,
                StatingNo = current.StatingNo.ToString(),
                MainTrackNumber = current.MainTrackNumber,
                FlowNo = current.FlowNo,
                Action = 5
            };
            var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
            //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
        }
        /// <summary>
        /// 发布衣架状态-->衣架下一道工序状态:
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        private static void HangerStatusCorrect(string hangerNo, int outMainTrackNumber, string nextStatingNo, HangerProductFlowChartModel nextHangerFlowChart)
        {
            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
            chpf.HangerNo = hangerNo;
            chpf.MainTrackNumber = outMainTrackNumber;
            chpf.StatingNo = int.Parse(nextStatingNo);
            chpf.FlowNo = nextHangerFlowChart.FlowNo;
            chpf.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
            chpf.FlowType = null == nextHangerFlowChart?.FlowType ? 0 : nextHangerFlowChart.FlowType.Value;
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);
        }
        /// <summary>
        /// 【衣架生产履历】下一站分配Cache写入
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        private static void NextStatingAlloctionResumeCacheHandler(string mainTrackNo, string statingNo, string hangerNo, string nextStatingNo, HangerProductFlowChartModel nextHangerFlowChart)
        {
            var nextStatingHPResume = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo,
                StatingNo = statingNo,
                MainTrackNumber = int.Parse(mainTrackNo),
                HangerProductFlowChart = nextHangerFlowChart,
                Action = 1,
                NextStatingNo = nextStatingNo
            };
            var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
        }

        /// <summary>
        /// 当前站点衣架生产履历修正
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="cflowChartList"></param>
        private static void CurrentStatingOutSiteCorrectHangerResume(string mainTrackNo, string statingNo, string hangerNo, IEnumerable<HangerProductFlowChartModel> cflowChartList)
        {
            var hpResumeOut = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo,
                StatingNo = statingNo,
                MainTrackNumber = int.Parse(mainTrackNo),
                HangerProductFlowChart = cflowChartList.First(),
                Action = 3
            };
            var hpResumeOutJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResumeOut);
            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hpResumeOutJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(),hpResumeOutJson );
        }

        private static void RecordYieldByMoveFow(string mainTrackNo, string statingNo, string hangerNo, List<HangerProductFlowChartModel> fChartList, Domain.Cus.CurrentHangerProductingFlowModel current)
        {
            var outSiteResult = new HangerOutSiteResult();
            outSiteResult.MainTrackNumber = int.Parse(mainTrackNo);
            outSiteResult.HangerNo = hangerNo;
            outSiteResult.StatingNo = statingNo;
            outSiteResult.HangerProductFlowChart = fChartList.Where(f => f.FlowNo.Equals(current.FlowNo)).First();
            var outSiteJson = Newtonsoft.Json.JsonConvert.SerializeObject(outSiteResult);
            //SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_OUT_SITE_ACTION, outSiteJson);
            NewSusRedisClient.Instance.HangerOutSiteAction(new StackExchange.Redis.RedisChannel(), outSiteJson);
        }
        /// <summary>
        /// 出战：修正删除的站内数及明细、缓存
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="current"></param>
        private static void OnlyMoveFlowCorrectStatingInItemCache(string hangerNo, Domain.Cus.CurrentHangerProductingFlowModel current)
        {
            //修正删除的站内数及明细、缓存
            var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocDeleteStating.Action = 2;
            hnssocDeleteStating.HangerNo = hangerNo;
            hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
            hnssocDeleteStating.StatingNo = current.StatingNo;
            hnssocDeleteStating.FlowNo = current.FlowNo;
            hnssocDeleteStating.FlowIndex = current.FlowIndex;
            //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
          //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );
        }

        private static void HangerAllocationHandler(FlowDeleteOrStatingDeletedException fdsdEx, string statingNo, string hangerNo, int outMainTrackNumber, string nextStatingNo, List<HangerStatingAllocationItem> dicHangerStatingALloList, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, HangerProductFlowChartModel nextHangerFlowChart)
        {
            var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
            nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
            nextHangerStatingAllocationItem.FlowIndex = (short)nextHangerFlowChart.FlowIndex;
            nextHangerStatingAllocationItem.SiteNo = statingNo;
            nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
            nextHangerStatingAllocationItem.HangerNo = hangerNo;
            nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
            nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
            nextHangerStatingAllocationItem.FlowNo = nextHangerFlowChart.FlowNo;
            nextHangerStatingAllocationItem.ProcessFlowCode = nextHangerFlowChart.FlowCode;
            nextHangerStatingAllocationItem.ProcessFlowName = nextHangerFlowChart.FlowName;
            nextHangerStatingAllocationItem.HangerType = nextHangerFlowChart.FlowType;
            //控制出站产量推送方式(返工还是正常)
            nextHangerStatingAllocationItem.IsReturnWorkFlow = false;
            nextHangerStatingAllocationItem.ProcessFlowId = nextHangerFlowChart.FlowId;
            nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
            nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
            nextHangerStatingAllocationItem.FlowChartd = nextHangerFlowChart.ProcessChartId;
            nextHangerStatingAllocationItem.ProductsId = nextHangerFlowChart.ProductsId;
            nextHangerStatingAllocationItem.PSize = nextHangerFlowChart.PSize;
            nextHangerStatingAllocationItem.PColor = nextHangerFlowChart.PColor;
            nextHangerStatingAllocationItem.ProcessOrderNo = nextHangerFlowChart.ProcessOrderNo;
            nextHangerStatingAllocationItem.StyleNo = nextHangerFlowChart.StyleNo;
            nextHangerStatingAllocationItem.LastFlowIndex = fdsdEx.NextFlowIndex;
            nextHangerStatingAllocationItem.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo);
            nextHangerStatingAllocationItem.GroupNo = BridgeService.Instance.GetGroupNo(outMainTrackNumber, int.Parse(nextStatingNo));
            dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);
             //记录衣架分配
             var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            foreach (var hfc in hangerProcessFlowChartLis)
            {
                if (nextStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0 && nextHangerFlowChart.FlowIndex.Value == hfc.FlowIndex.Value)
                {
                    hfc.isAllocationed = true;
                    hfc.AllocationedDate = DateTime.Now;
                    hfc.IsStorageStatingAgainAllocationedSeamsStating = false;
                    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hangerProcessFlowChartLis);
                    //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = hangerProcessFlowChartLis;
                    break;
                }
            }
        }
    }
}

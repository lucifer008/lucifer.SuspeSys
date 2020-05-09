using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products
{
    public class OutSiteService : Base.ServiceBase
    {
        private OutSiteService() { }
        public readonly static OutSiteService Instance = new OutSiteService();
        private static readonly object locObject = new object();
        /// <summary>
        /// 获取下一站
        /// </summary>
        /// <param name="currentProcessOrderHanger">当前衣架携带的信息</param>
        /// <param name="flowIndx">生产到第几道工序</param>
        /// <param name="nextStatingNo">输出下一站</param>
        /// <param name="outMainTrackNumber">下一站主轨</param>
        /// <param name="nextHangerProductFlowChart">下一站工艺工序信息</param>
        public void GetHangerNextSiteExt(WaitProcessOrderHanger currentProcessOrderHanger, int flowIndex, ref string nextStatingNo, ref int outMainTrackNumber, ref HangerProductFlowChartModel nextHangerProductFlowChart, ref ProcessFlowChartFlowRelation pfFlowRelation, bool isStoreStatingOutSite = false)
        {
            lock (locObject)
            {
                //var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                List<HangerProductFlowChartModel> hangerProductFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(currentProcessOrderHanger.HangerNo);
                // dicHangerProcessFlowChart.TryGetValue(currentProcessOrderHanger.HangerNo?.Trim(), out hangerProductFlowChartList);
                if (hangerProductFlowChartList.Count == 0)
                {
                    throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()));
                }
                //最先生产的工序
                if (0 == flowIndex)
                {
                    //if (hangerProductFlowChartList.Count != 0)
                    //{
                    //在线数待计算
                    //var onlieMainTrackStatingList = new List<SiteInfo>();
                    //第一道工序制作站点
                    HangerProductFlowChartModel nextFlow = null;
                    var currentFlowProcessChartList = hangerProductFlowChartList.Where(f => (f.FlowIndex.Value != 1 && f.FlowIndex.Value != -1)
                    ).OrderBy(ff => ff.FlowIndex);
                    nextFlow = currentFlowProcessChartList.Count() > 0 ? currentFlowProcessChartList.First() : null;
                    nextHangerProductFlowChart = nextFlow;
                    var firstFlowStatInfoFlowIndex = currentFlowProcessChartList.Select(k => k.FlowIndex);
                    if (firstFlowStatInfoFlowIndex.Count() == 0)
                    {
                        throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                        {
                            FlowNo = nextFlow?.FlowNo?.Trim()
                        };
                    }
                    var nextFlowIndex = firstFlowStatInfoFlowIndex.First();

                    var nextFlowStatList = hangerProductFlowChartList.Where(f =>
                        f.FlowIndex.Value != 1 && f.FlowIndex.Value != -1
                        && f.FlowIndex.Value == nextFlowIndex.Value
                        && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                        && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));

                    if (nextFlowStatList.Count() == 0)
                    {
                        throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                        {
                            FlowNo = nextFlow?.FlowNo?.Trim()
                        };
                    }
                    if (nextFlowStatList.Count() == nextFlowStatList.Where(f => null == f.StatingNo).Count())
                    {
                        //下一道没有可以接收衣架的站
                        throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                        {
                            FlowNo = nextFlow?.FlowNo?.Trim()
                        };
                    }
                    //var receivingStatus=

                    //var nextStatingList = nextFlowStatList
                    //   .Select(f => new ProductsProcessOrderModel()
                    //   {
                    //       StatingNo = f.StatingNo.ToString(),
                    //       MainTrackNumber = (int)f.MainTrackNumber,
                    //       StatingCapacity = f.StatingCapacity.Value
                    //   }).ToList<ProductsProcessOrderModel>();
                    tcpLogInfo.InfoFormat("【下一站分配计算】:【{0}】", Newtonsoft.Json.JsonConvert.SerializeObject(nextFlowStatList));
                    var nextStatingList = CalculationNextSiteList(currentProcessOrderHanger, nextFlowStatList);
                    if (nextStatingList.Count == 0)
                    {
                        //下一道没有可以接收衣架的站
                        throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                        {
                            FlowNo = nextFlow?.FlowNo?.Trim()
                        };
                    }

                    //zhangxiaolin 2018年10月1日 23:06:59
                    //nextStatingNo = CalcateStatingNo(nextStatingList, ref outMainTrackNumber);
                    tcpLogInfo.InfoFormat("计算后下一站数据:【{0}】", Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingList));
                    AllocationNextProcessFlowStating(nextStatingList, ref outMainTrackNumber, ref nextStatingNo);
                    return;
                    //  }
                }

                var pfFlowRelationList = hangerProductFlowChartList.
                    Where(f => f.FlowIndex.Value == flowIndex && null != f.Status && f.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                    .Select(f => new ProcessFlowChartFlowRelation()
                    {
                        FlowCode = f.FlowCode,
                        FlowNo = f.FlowNo,
                        FlowName = f.FlowName,
                        Id = f.FlowId
                    }).ToList<ProcessFlowChartFlowRelation>();
                var hangerProductFlowChartList3 = hangerProductFlowChartList.Where(f => f.FlowIndex.Value == flowIndex).ToList<HangerProductFlowChartModel>();
                pfFlowRelation = pfFlowRelationList.Count > 0 ? pfFlowRelationList[0] : new ProcessFlowChartFlowRelation();
                nextHangerProductFlowChart = hangerProductFlowChartList3.Count > 0 ? hangerProductFlowChartList3[0] : new HangerProductFlowChartModel();
                // nextStatingNo = CalcateStatingNo(firstFlowStatlist, ref outMainTrackNumber

                var nextFlowStatList2 = hangerProductFlowChartList.
                        Where(f => f.FlowIndex.Value == flowIndex &&
                        null != f.Status && f.Status.Value != HangerProductFlowChartStaus.Successed.Value &&
                        (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                        && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));

                if (nextFlowStatList2.Count() == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                    {
                        FlowNo = pfFlowRelation?.FlowNo?.Trim()
                    };
                }
                var nextStatingList2 = CalculationNextSiteList(currentProcessOrderHanger, nextFlowStatList2);
                if (nextStatingList2.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                    {
                        FlowNo = pfFlowRelation?.FlowNo?.Trim()
                    };
                }

                if (isStoreStatingOutSite)
                {
                    //2018年10月1日 23:07:58 zhangxiaolin
                    var ntList = nextStatingList2.Where(f => null != f.StatingRoleCode &&
                   !f.StatingRoleCode.Equals(StatingType.StatingStorage.Code)).Select(k => new ProductsProcessOrderModel()
                   {
                       StatingNo = k.StatingNo.ToString(),
                       MainTrackNumber = (int)k.MainTrackNumber,
                       StatingCapacity = k.StatingCapacity,
                       Proportion = k.Proportion,
                       ProcessChartId = k.ProcessChartId,
                       FlowNo = k.FlowNo,
                       StatingRoleCode = k.StatingRoleCode
                   }).ToList();
                    if (0 == ntList.Count)
                    {
                        //下一道没有可以接收衣架的站
                        throw new NoFoundStatingException(string.Format("【下一站分配计算】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", currentProcessOrderHanger.MainTrackNumber, currentProcessOrderHanger.SiteNo?.Trim(), currentProcessOrderHanger.HangerNo?.Trim()))
                        {
                            FlowNo = pfFlowRelation?.FlowNo?.Trim()
                        };
                    }
                    nextStatingNo = CalcateStatingNo(ntList, ref outMainTrackNumber);
                    return;
                }
                AllocationNextProcessFlowStating(nextStatingList2, ref outMainTrackNumber, ref nextStatingNo);
                return;
                // }
                // }
            }
        }


        /// <summary>
        /// 获取当前衣架满足进站条件的站点，限定根据工艺图限定
        /// </summary>
        /// <param name="currentProcessOrderHanger"></param>
        /// <param name="nextFlowStatList"></param>
        /// <returns></returns>
        private List<ProductsProcessOrderModel> CalculationNextSiteList(WaitProcessOrderHanger currentProcessOrderHanger, IEnumerable<HangerProductFlowChartModel> nextFlowStatList)
        {
            var nextStatingList = new List<ProductsProcessOrderModel>();
            foreach (var st in nextFlowStatList)
            {
                #region 注视掉的代码
                ////PO号必须匹配
                //if (!string.IsNullOrEmpty(st.ReceivingPoNumber) && st.ReceivingPoNumber.Equals(currentProcessOrderHanger.Po))
                //{
                //    //颜色必须匹配
                //    if (!string.IsNullOrEmpty(st.ReceivingColor) && st.PColor.Equals(currentProcessOrderHanger.PColor))
                //    {
                //        #region 尺码必须匹配 尺码无须匹配
                //        //尺码必须匹配
                //        if (!string.IsNullOrEmpty(st.ReceivingSize) && st.PSize.Equals(currentProcessOrderHanger.PSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        //尺码无须匹配
                //        if (string.IsNullOrEmpty(st.ReceivingSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        #endregion
                //    }
                //    //颜色无需匹配
                //    if (string.IsNullOrEmpty(st.ReceivingColor))
                //    {
                //        #region 尺码必须匹配 尺码无须匹配
                //        //尺码必须匹配
                //        if (!string.IsNullOrEmpty(st.ReceivingSize) && st.PSize.Equals(currentProcessOrderHanger.PSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        //尺码无须匹配
                //        if (string.IsNullOrEmpty(st.ReceivingSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        #endregion
                //    }

                //}
                //if (string.IsNullOrEmpty(st.ReceivingPoNumber))
                //{
                //    //颜色必须匹配
                //    if (!string.IsNullOrEmpty(st.ReceivingColor) && st.PColor.Equals(currentProcessOrderHanger.PColor))
                //    {
                //        #region 尺码必须匹配 尺码无须匹配
                //        //尺码必须匹配
                //        if (!string.IsNullOrEmpty(st.ReceivingSize) && st.PSize.Equals(currentProcessOrderHanger.PSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        //尺码无须匹配
                //        if (string.IsNullOrEmpty(st.ReceivingSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        #endregion
                //    }
                //    //颜色无需匹配
                //    if (string.IsNullOrEmpty(st.ReceivingColor))
                //    {
                //        #region 尺码必须匹配 尺码无须匹配
                //        //尺码必须匹配
                //        if (!string.IsNullOrEmpty(st.ReceivingSize) && st.PSize.Equals(currentProcessOrderHanger.PSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        //尺码无须匹配
                //        if (string.IsNullOrEmpty(st.ReceivingSize))
                //        {
                //            var ppOrderModel = new ProductsProcessOrderModel()
                //            {
                //                StatingNo = st.StatingNo.ToString(),
                //                MainTrackNumber = (int)st.MainTrackNumber,
                //                StatingCapacity = st.StatingCapacity.Value
                //            };
                //            nextStatingList.Add(ppOrderModel);
                //        }
                //        #endregion
                //    }
                //}
                #endregion

                var currentHangerStr = string.Format("{0}{1}{2}", currentProcessOrderHanger.Po?.Trim(), currentProcessOrderHanger.PColor?.Trim(), currentProcessOrderHanger.PSize?.Trim());
                var flowChartHangerStr = string.Format("{0}{1}{2}", st.ReceivingPoNumber?.Trim(), st.ReceivingColor?.Trim(), st.ReceivingSize?.Trim());
                if (string.IsNullOrEmpty(flowChartHangerStr))
                {
                    var ppOrderModel = new ProductsProcessOrderModel()
                    {
                        StatingNo = st.StatingNo.ToString(),
                        MainTrackNumber = (int)st.MainTrackNumber,
                        StatingCapacity = st.StatingCapacity.Value,
                        Proportion = st.Proportion.HasValue ? st.Proportion.Value : 0,
                        ProcessChartId = st.ProcessChartId,
                        FlowNo = st.FlowNo,
                        StatingRoleCode = st.StatingRoleCode
                    };
                    nextStatingList.Add(ppOrderModel);
                }
                if (currentHangerStr.Equals(flowChartHangerStr))
                {
                    var ppOrderModel = new ProductsProcessOrderModel()
                    {
                        StatingNo = st.StatingNo.ToString(),
                        MainTrackNumber = (int)st.MainTrackNumber,
                        StatingCapacity = st.StatingCapacity.Value,
                        Proportion = st.Proportion.HasValue ? st.Proportion.Value : 0,
                        ProcessChartId = st.ProcessChartId,
                        FlowNo = st.FlowNo,
                        StatingRoleCode = st.StatingRoleCode
                    };
                    nextStatingList.Add(ppOrderModel);
                }
                if (currentHangerStr.Contains(flowChartHangerStr) && !currentHangerStr.Equals(flowChartHangerStr) && !string.IsNullOrEmpty(flowChartHangerStr))
                {
                    var ppOrderModel = new ProductsProcessOrderModel()
                    {
                        StatingNo = st.StatingNo.ToString(),
                        MainTrackNumber = (int)st.MainTrackNumber,
                        StatingCapacity = st.StatingCapacity.Value,
                        Proportion = st.Proportion.HasValue ? st.Proportion.Value : 0,
                        ProcessChartId = st.ProcessChartId,
                        FlowNo = st.FlowNo,
                        StatingRoleCode = st.StatingRoleCode
                    };
                    nextStatingList.Add(ppOrderModel);
                }
                if (!string.IsNullOrEmpty(st.ReceivingPoNumber) && string.IsNullOrEmpty(st.ReceivingColor) && !string.IsNullOrEmpty(st.ReceivingSize))
                {
                    if (currentProcessOrderHanger.Po.Equals(st.ReceivingPoNumber) && currentProcessOrderHanger.PSize.Equals(st.PSize))
                    {
                        var ppOrderModel = new ProductsProcessOrderModel()
                        {
                            StatingNo = st.StatingNo.ToString(),
                            MainTrackNumber = (int)st.MainTrackNumber,
                            StatingCapacity = st.StatingCapacity.Value,
                            Proportion = st.Proportion.HasValue ? st.Proportion.Value : 0,
                            ProcessChartId = st.ProcessChartId,
                            FlowNo = st.FlowNo,
                            StatingRoleCode = st.StatingRoleCode
                        };
                    }
                }
            }
            return nextStatingList;
        }
        //public void GetHangerNextSite(int mainTrackNumber, string hangerNo, int flowIndex, ref string nextStatingNo, ref ProcessFlowChartFlowRelation pfFlowRelation, ref int outMainTrackNumber, ref HangerProductFlowChart hangerProductFlowChart)
        //{
        //    if (0 == flowIndex)
        //    {

        //        var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //        List<HangerProductFlowChartModel> hangerProductFlowChartList;
        //        dicHangerProcessFlowChart.TryGetValue(hangerNo, out hangerProductFlowChartList);
        //        if (hangerProductFlowChartList.Count != 0)
        //        {
        //            //在线数待计算
        //            //var onlieMainTrackStatingList = new List<SiteInfo>();
        //            //第一道工序制作站点
        //            var firstFlowStatlist = hangerProductFlowChartList
        //                                    .Where(f => f.FlowIndex.Value == 2)
        //                                    .Select(f => new ProductsProcessOrderModel()
        //                                    {
        //                                        StatingNo = f.StatingNo.ToString(),
        //                                        MainTrackNumber = (int)f.MainTrackNumber,
        //                                        StatingCapacity = f.StatingCapacity.Value,
        //                                        Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
        //                                        ProcessChartId = f.ProcessChartId,
        //                                        FlowNo = f.FlowNo
        //                                    }).ToList<ProductsProcessOrderModel>();
        //            nextStatingNo = CalcateStatingNo(firstFlowStatlist, ref outMainTrackNumber);

        //            return;
        //        }
        //    }

        //    //获取衣架工艺图
        //    var dicHangerProcessFlowChart2 = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //    List<HangerProductFlowChartModel> hangerProductFlowChartList2;
        //    dicHangerProcessFlowChart2.TryGetValue(hangerNo, out hangerProductFlowChartList2);
        //    if (hangerProductFlowChartList2.Count != 0)
        //    {
        //        //在线数待计算
        //        // var onlieMainTrackStatingList = new List<SiteInfo>();
        //        //FlowIndex道工序制作站点
        //        var firstFlowStatlist = hangerProductFlowChartList2.
        //            Where(f => f.FlowIndex.Value == flowIndex && null != f.Status && f.Status.Value != HangerProductFlowChartStaus.Successed.Value)
        //            .Select(f => new ProductsProcessOrderModel()
        //            {
        //                StatingNo = f.StatingNo.ToString(),
        //                MainTrackNumber = (int)f.MainTrackNumber,
        //                StatingCapacity = f.StatingCapacity.Value,
        //                Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
        //                ProcessChartId = f.ProcessChartId,
        //                FlowNo = f.FlowNo
        //            }).ToList<ProductsProcessOrderModel>();
        //        var pfFlowRelationList = hangerProductFlowChartList2.
        //            Where(f => f.FlowIndex.Value == flowIndex && null != f.Status && f.Status.Value != HangerProductFlowChartStaus.Successed.Value)
        //            .Select(f => new ProcessFlowChartFlowRelation()
        //            {
        //                FlowCode = f.FlowCode,
        //                FlowNo = f.FlowNo,
        //                FlowName = f.FlowName,
        //                Id = f.FlowId,
        //            }).ToList<ProcessFlowChartFlowRelation>();
        //        var hangerProductFlowChartList = hangerProductFlowChartList2.Where(f => f.FlowIndex.Value == flowIndex).ToList<HangerProductFlowChart>();
        //        pfFlowRelation = pfFlowRelationList.Count > 0 ? pfFlowRelationList[0] : new ProcessFlowChartFlowRelation();
        //        hangerProductFlowChart = hangerProductFlowChartList.Count > 0 ? hangerProductFlowChartList[0] : new HangerProductFlowChart();
        //        nextStatingNo = CalcateStatingNo(firstFlowStatlist, ref outMainTrackNumber);
        //        return;
        //    }

        //    //var nextStatingList = GetHangerNextStatingInfo(flowIndex, hangerNo, statingNo);
        //    //if (nextStatingList.Count == 0)
        //    //{
        //    //    var message = string.Format("【衣架出站】--->主轨:【{0}】 站点:{1} 衣架号:{2} 找不着下一站点信息", mainTrackNumber, statingNo, hangerNo);
        //    //    errorLog.Error(message);
        //    //    throw new ProcessFlowChartNoFindStatingException(message);
        //    //}
        //    //var pFlow = nextStatingList[0];
        //    //if (string.IsNullOrEmpty(pFlow.Id))
        //    //{
        //    //    var message = string.Format("【衣架出站】--->主轨:【{0}】 站点:{1} 衣架号:{2} 衣架找不着工艺图!", mainTrackNumber, statingNo, hangerNo);
        //    //    errorLog.Error(message);
        //    //    throw new ProcessFlowChartNoFindStatingException(message);
        //    //}
        //    //hangerProductFlowChart = HangerProductFlowChartDao.Instance.GetById(pFlow.Id);
        //    //pfFlowRelation = new ProcessFlowChartFlowRelation()
        //    //{
        //    //    ProcessFlow = new ProcessFlow() { Id = pFlow.ProcessFlowId },
        //    //    FlowNo = pFlow.FlowNo,
        //    //    FlowCode = pFlow.FlowCode,
        //    //    FlowName = pFlow.FlowName
        //    //};
        //    //var dicCommonParamterValues = new Dictionary<string, object>();
        //    //var qStatingList1 = new List<string>();
        //    //foreach (var s in nextStatingList)
        //    //{
        //    //    qStatingList1.Add(s.StatingNo?.Trim());
        //    //}
        //    //// dicCommonParamterValues.Add("MainTrackNumber", mainTrackNumber);
        //    //dicCommonParamterValues.Add("SiteNo", qStatingList1);
        //    //var oSQL1 = GetStatingCapactitySQL();
        //    //var statingOnlineCountList1 = QueryIn<SiteInfo>(oSQL1, dicCommonParamterValues);
        //    //nextStatingNo = CalcateStatingNo(nextStatingList.ToList<ProductsProcessOrderModel>(), statingOnlineCountList1, ref outMainTrackNumber);
        //}
        ///// <summary>
        ///// 获取衣架下一到工序站点列表
        ///// </summary>
        ///// <param name="mainTrackNumber"></param>
        ///// <param name="hangerNo"></param>
        ///// <param name="productId"></param>
        ///// <param name="sourceStatingNo"></param>
        ///// <returns></returns>
        //public IList<ProductsProcessOrderModel> GetHangerNextStatingInfo(int flowIndex, string hangerNo, string sourceStatingNo)
        //{
        //    //var dicOrders = new Dictionary<string, string>();
        //    //dicOrders.Add("FlowIndex", "ASC");

        //    var dicCondi = new Dictionary<string, string>();
        //    dicCondi.Add("FlowIndex", flowIndex.ToString());
        //    dicCondi.Add("HangerNo", hangerNo);
        //    // dicCondi.Add("StatingNo", sourceStatingNo);
        //    var sql = string.Format(@"select Id,MainTrackNumber,CONVERT(varchar(50),StatingNo) StatingNo,StatingCapacity,FlowId ProcessFlowId,FlowNo,FlowCode,FlowName from HangerProductFlowChart where FlowIndex=:FlowIndex and HangerNo=:HangerNo  
        //    and IsFlowSucess=0");
        //    var pList = Query<ProductsProcessOrderModel>(new StringBuilder(sql), null, true, dicCondi);
        //    return pList;
        //}
        //string GetProcessChartSQL()
        //{
        //    string sql = string.Format(@"select T2.CraftFlowNo,
        //            T1.BoltProcessFlowId,(Select ProcessCode from ProcessFlow where id=T1.BoltProcessFlowId) BoldProcessFlowCode,
        //            T1.OutputProcessFlowId,(SELECT ProcessCode from ProcessFlow where id=T1.OutputProcessFlowId) OutProcessFlowCode,
        //            T2.PROCESSFLOW_Id,
        //            T2.FlowCode,T2.FlowName,(SELECT StatingNo FROM Stating WHERE ID=T3.STATING_Id) StatingNo,
        //            (SELECT Capacity FROM Stating WHERE ID=T3.STATING_Id) StatingCapacity
        //            ,ISNULL(T3.IsReceivingHanger, 0) IsReceivingHanger,ISNULL(T3.IsReceivingAllSize, 0) IsReceivingAllSize,ISNULL(T3.IsReceivingAllColor, 0) IsReceivingAllColor,T3.ReceivingColor,T3.ReceivingSize,T3.ReceivingPONumber,
        //            T3.MainTrackNumber
        //            from ProcessFlowChart T1
        //            INNER JOIN ProcessFlowChartFlowRelation T2 ON T1.Id=T2.PROCESSFLOWCHART_Id
        //            INNER JOIN ProcessFlowStatingItem T3 ON T3.PROCESSFLOWCHARTFLOWRELATION_Id=T2.Id WHERE T1.Id=:ProcessFlowChartId");
        //    return sql;
        //}
        ///// <summary>
        ///// 获取主轨站点内衣架数
        ///// </summary>
        ///// <returns></returns>
        //string GetStatingCapactitySQL()
        //{
        //    var sql = @"select MainTrackNumber,SiteNo,COUNT(1) onLineCount from StatingHangerProductItem where SiteNo in(:SiteNo)
        //        GROUP BY MainTrackNumber,SiteNo ";
        //    return sql;
        //}
        /// <summary>
        /// 获取下一站站点及主轨
        /// </summary>
        /// <param name="nextProcessFlowStatingList"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="isMoMonitor"></param>
        public void AllocationNextProcessFlowStating(List<ProductsProcessOrderModel> nextProcessFlowStatingList,
            ref int outMainTrackNumber, ref string nextStatingNo
            , bool isMoMonitor = false)
        {
            lock (locObject)
            {
                if (nextProcessFlowStatingList == null || nextProcessFlowStatingList.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【衣架分配】 找不到下一站!"));
                }
                var isOnlySeamingStating = false;//只包含车缝站
                isOnlySeamingStating = nextProcessFlowStatingList.Count == nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                 f.StatingRoleCode.Equals(StatingType.StatingSeaming.Code)).Count();

                //只包含车缝站走老逻辑
                if (isOnlySeamingStating)
                {
                    tcpLogInfo.Info($"【下一站分配计算】只含有车缝站处理开始...");
                    nextStatingNo = CalcateStatingNo(nextProcessFlowStatingList, ref outMainTrackNumber, isMoMonitor);
                    tcpLogInfo.InfoFormat($"【下一站分配计算】只含有车缝站处理结束，下一站主轨:{outMainTrackNumber} 下一站站点:{nextStatingNo}");
                    return;
                }
                // tcpLogInfo.InfoFormat("AllocationNextProcessFlowStating:outMainTrackNumber-->【{0}】", outMainTrackNumber);
                //包含存储站
                var isContainsStatingStorage = false;//包含多功能站
                isContainsStatingStorage = (nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                f.StatingRoleCode.Equals(StatingType.StatingStorage.Code)).Count() > 0);
                if (isContainsStatingStorage)
                {
                    try
                    {
                        //分配车缝站
                        nextStatingNo = CalcateStatingNo(nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                 f.StatingRoleCode.Equals(StatingType.StatingSeaming.Code)).ToList()
                            , ref outMainTrackNumber, isMoMonitor);
                        return;
                    }
                    catch (FullStatingExcpetion fsEx)
                    {
                        tcpLogInfo.Error(fsEx);
                        tcpLogInfo.Info($"【下一站分配计算】车缝站满站,分配多功能站");
                        try
                        {
                            //分配多功能站
                            nextStatingNo = CalcateStatingNo(nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                    f.StatingRoleCode.Equals(StatingType.StatingMultiFunction.Code)).ToList()
                               , ref outMainTrackNumber, isMoMonitor);
                            return;
                        }
                        catch (FullStatingExcpetion fsExStatingMultiFunction)
                        {
                            tcpLogInfo.Info($"【下一站分配计算】分配多功能站满站,分配存储站");
                            tcpLogInfo.Error(fsExStatingMultiFunction);

                            //   //直接分配存储站
                            //   var ntStoreStatingList = (nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                            //f.StatingRoleCode.Equals(StatingType.StatingStorage.Code)));
                            //   nextStatingNo = ntStoreStatingList.First().StatingNo;
                            //   outMainTrackNumber = ntStoreStatingList.First().MainTrackNumber;
                            nextStatingNo = CalcateStatingNo(nextProcessFlowStatingList.Where(f => null != f.StatingRoleCode &&
                       (f.StatingRoleCode.Equals(StatingType.StatingStorage.Code))).ToList()
                                  , ref outMainTrackNumber, isMoMonitor);
                        }
                        return;
                    }
                }
                //其他情况走老逻辑
                nextStatingNo = CalcateStatingNo(nextProcessFlowStatingList, ref outMainTrackNumber, isMoMonitor);
            }
        }
        /// <summary>
        /// 计算下一站:
        /// </summary>
        /// <param name="nextProcessFlowStatingList">下一道工序的主轨 站列表</param>
        /// <param name="onlieMainTrackStatingList">在线站内衣架数</param>
        /// <returns></returns>
        public string CalcateStatingNo(List<ProductsProcessOrderModel> nextProcessFlowStatingList, ref int outMainTrackNumber, bool isMoMonitor = false)
        {
            lock (locObject)
            {
                var fullSiteCount = 0;
                var dicNonFullSite = new Dictionary<string, long>();
                var dicStatingInNumCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
                var dicStatingAllocationCahe = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);
                var dicStatingFullSiteCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
                var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);

                tcpLogInfo.InfoFormat("【下一站分配计算】下一站数据:【{0}】", Newtonsoft.Json.JsonConvert.SerializeObject(nextProcessFlowStatingList));
                foreach (var t in nextProcessFlowStatingList)
                {
                    //foreach (var s in onlieMainTrackStatingList)
                    //{
                    //    //记录满站数目
                    //    if (s.MainTrackNumber == t.MainTrackNumber && s.SiteNo.Equals(t.StatingNo?.Trim()) && t.StatingCapacity == s.OnLineCount)
                    //    {
                    //        fullSiteCount++;
                    //        break;
                    //    }
                    //}

                    var mKey = string.Format("{0}:{1}", t.MainTrackNumber, t.StatingNo?.Trim());
                    long statingCapacity = 0; //t.StatingCapacity;
                    if (dicStatingCache.ContainsKey(mKey))
                    {
                        statingCapacity = dicStatingCache[mKey].Capacity.Value;
                    }
                    long allocationNumed = 0;
                    long statingNumed = 0;
                    if (dicStatingAllocationCahe.ContainsKey(mKey))
                    {
                        allocationNumed = dicStatingAllocationCahe[mKey];

                    }
                    if (dicStatingInNumCache.ContainsKey(mKey))
                    {
                        statingNumed = dicStatingInNumCache[mKey];
                    }
                    tcpLogInfo.Info($"【下一站分配计算】站点分配明细--> 主轨:{t.MainTrackNumber} 站点:{t.StatingNo} 站点容量:{statingCapacity} (分配数/在线数):{allocationNumed} 站内数:{statingNumed}");

                    if (dicStatingFullSiteCache.ContainsKey(mKey))
                    {
                        if (dicStatingFullSiteCache[mKey].IsFullSite)
                        {
                            t.IsFull = true;
                            fullSiteCount++;
                            continue;
                        }
                    }

                    if (statingCapacity - allocationNumed - statingNumed <= 0)
                    {
                        t.IsFull = true;
                        fullSiteCount++;
                        tcpLogInfo.Info($"【下一站分配计算】满站--> 主轨:{t.MainTrackNumber} 站点:{t.StatingNo} 站点容量:{statingCapacity} (分配数/在线数):{allocationNumed} 站内数:{statingNumed}");
                        continue;
                    }
                    else
                    {
                        var remainder = (statingCapacity - allocationNumed - statingNumed);
                        //var remainderRate = remainder / Convert.ToDecimal(statingCapacity.ToString());
                        //tcpLogInfo.InfoFormat("主轨:{0} 站点: {1}剩余比例:{2}", t.MainTrackNumber, t.StatingNo?.Trim(),remainderRate);
                        var fKey = string.Format("{0}-{1}", t.MainTrackNumber, t.StatingNo?.Trim());
                        if (dicNonFullSite.ContainsKey(fKey))
                        {
                            dicNonFullSite.Remove(fKey);
                            tcpLogError.ErrorFormat("【下一站站点重复】主轨:{0} 站点:{1}", t.MainTrackNumber, t.StatingNo?.Trim());
                        }
                        dicNonFullSite.Add(fKey, remainder);
                        t.Remainder = remainder;
                    }
                    //foreach (var s in onlieMainTrackStatingList)
                    //{
                    //    if (s.MainTrackNumber == t.MainTrackNumber && s.SiteNo.Equals(t.StatingNo?.Trim()))
                    //    {
                    //        if (s.OnLineCount < t.StatingCapacity)
                    //        {
                    //            //key 为主轨+站号
                    //            if (!dicNonFullSite.Keys.Contains(string.Format("{0}-{1}", t.MainTrackNumber, s.SiteNo.Trim())))
                    //            {
                    //                dicNonFullSite.Add(string.Format("{0}-{1}", t.MainTrackNumber, s.SiteNo.Trim()), s.OnLineCount);
                    //            }
                    //            else
                    //            {
                    //                dicNonFullSite[string.Format("{0}-{1}", t.MainTrackNumber, s.SiteNo.Trim())] = s.OnLineCount;
                    //            }
                    //        }
                    //        break;
                    //    }
                    //}
                }
                if (fullSiteCount == nextProcessFlowStatingList.Count)
                {
                    var fullStatingFlowNo = nextProcessFlowStatingList.Select(f => f.FlowNo)?.Distinct()?.First();
                    var fullStatingList = nextProcessFlowStatingList.Select(f => f.StatingNo).ToList<string>();
                    var message = string.Format("【下一站分配计算】站点【{0}】满站！ 工序号:{1}", string.Join(",", fullStatingList), fullStatingFlowNo);
                    var ex = new FullStatingExcpetion(message);
                    ex.FullStatingList = fullStatingList;
                    ex.FullFlowNo = fullStatingFlowNo;
                    tcpLogError.Error(message, ex);
                    tcpLogInfo.Info(message);
                    throw ex;
                }
                ////线上站内没有衣架
                //if (onlieMainTrackStatingList.Count == 0)
                //{
                //    var firstFlowStating = nextProcessFlowStatingList[0];
                //    outMainTrackNumber = firstFlowStating.MainTrackNumber;
                //    return firstFlowStating.StatingNo?.Trim();
                //}
                //空站点数是1，或者 空站切没有分摊比例的 走原有逻辑
                if (nextProcessFlowStatingList.Count(o => o.IsFull == false) == 1 ||
                    nextProcessFlowStatingList.Count(o => o.IsFull == false && o.Proportion == 0M) == nextProcessFlowStatingList.Count(o => o.IsFull == false))

                {
                    //如果不满站，站点数剩余1，那么按照原有规则进行处理

                    //取站内未满站且衣架最小的站点
                    var nextStatingList = dicNonFullSite.Keys.Select(x => new { x, y = dicNonFullSite[x] }).OrderByDescending(x => x.y);
                    if (nextStatingList.Count() == 0 && isMoMonitor)
                    {
                        montorLog.Info($"【下一站分配计算】监测站分配计算，衣架已生产完成!");
                        return "";
                    }
                    var minTrackNumberStatingNo = nextStatingList?.First()?.x;
                    if (string.IsNullOrEmpty(minTrackNumberStatingNo))
                    {
                        var exMessage = string.Format("【下一站分配计算】数据错误! 没找到满足条件的下一站");
                        var exErr = new ApplicationException(exMessage);
                        tcpLogError.Error(exErr);
                        throw exErr;
                    }
                    var mainTrackStatingArr = minTrackNumberStatingNo.Split('-');
                    outMainTrackNumber = int.Parse(mainTrackStatingArr[0]);
                    return mainTrackStatingArr[1];
                }
                else
                {
                    //如果有多个，按照比例分配

                    string statingNo = string.Empty;

                    nextProcessFlowStatingList = nextProcessFlowStatingList.Where(o => o.IsFull == false).ToList();

                    this.AllocationStating(nextProcessFlowStatingList, ref outMainTrackNumber, ref statingNo);

                    return statingNo;
                }
            }
        }

        /// <summary>
        /// 计算站点分配
        /// </summary>
        /// <param name="nextProcessFlowStatingList"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <returns></returns>
        private void AllocationStating(List<ProductsProcessOrderModel> nextProcessFlowStatingList, ref int outMainTrackNumber, ref string statingNo)
        {
            IList<Allocation> allocationList = new List<Allocation>();

            #region 处理缓存，缓存数据
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, Allocation>(SusRedisConst.STATING_ALLOCATION);

            //本次存在的站点
            var keys = nextProcessFlowStatingList
                       .Where(o => o.Proportion > 0)
                       .Select(o => $"{o.ProcessChartId}:{o.FlowNo}:{o.MainTrackNumber}:{o.StatingNo}")
                       .ToArray();

            var exceptKey = dic.Keys.Except(keys);
            foreach (var key in exceptKey)
            {
                dic.Remove(key);
            }

            foreach (var item in nextProcessFlowStatingList)
            {
                if (item.Proportion > 0)
                {
                    Allocation allocation = new Allocation()
                    {
                        StatingNo = item.StatingNo.Trim(),
                        MainTrackNo = item.MainTrackNumber,
                        HaveProportion = 0,
                        Proportion = item.Proportion,
                        FlowNo = item.FlowNo?.Trim(),
                        ProcessChartId = item.ProcessChartId,
                    };

                    if (dic.Keys.Contains(allocation.Key))
                    {
                        allocation = dic[allocation.Key];

                        allocationList.Add(allocation);
                    }
                    else
                    {
                        dic.Add(allocation.Key, allocation);
                        allocationList.Add(allocation);
                    }
                }
            }
            #endregion

            allocationList = allocationList.OrderBy(o => o.Proportion).ToList();

            //可用数据放入双向列表
            LinkedList<Allocation> linkedList = new LinkedList<Allocation>();
            for (int i = 0; i < allocationList.Count; i++)
            {
                if (linkedList.Count == 0)
                    linkedList.AddFirst(allocationList[i]);
                else
                    linkedList.AddAfter(linkedList.Last, allocationList[i]);
            }

            //获取是否已经分配过(一个工序下的站点只有一个是可分配的)
            if (allocationList.Any(o => o.CanAllocation == true))
            {


                var allocation = allocationList.First(o => o.CanAllocation == true);

                outMainTrackNumber = allocation.MainTrackNo;
                statingNo = allocation.StatingNo;

                //已分配数+1
                allocation.HaveProportion += 1;
                allocation.CanAllocation = false;

                //当前站点，下一个可分配站点写入缓存
                dic[allocation.Key] = allocation;


                var reslutJosn = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.STATING_ALLOCATION_SAVE_CHANGE_ACTION, reslutJosn);

                //获取双向列表中的下一个
                var linkedNode = linkedList.Find(allocation);
                var nextStating = ProcessNextAllocationStating(linkedList, linkedNode);

                //下一个设置为可分配
                nextStating.CanAllocation = true;
                dic[nextStating.Key] = nextStating;

                ////找出可分配数最大的站点
                //var allocation = (from a in nextProcessFlowStatingList
                //               join b in allocationList.Where(o => o.CanAllocation == true) on new { StatingNo = a.StatingNo, MainTrackNo = a.MainTrackNumber }
                //                                                                        equals new { StatingNo = b.StatingNo, MainTrackNo = b.MainTrackNo }
                //                      into Temp
                //               from c in Temp
                //               orderby a.Remainder descending
                //               select c).Distinct().First();

                ////找出缓存中对应的记录
                ////var allocation = allocationList.Where(o => o.StatingNo == stating.StatingNo && o.MainTrackNo == stating.MainTrackNumber).First();

                //outMainTrackNumber = allocation.MainTrackNo;
                //statingNo = allocation.StatingNo;

            }
            else
            {
                //当前工序下的站点没有分配记录

                //获取第一个节点
                var allocation = linkedList.First.Value;
                allocation.CanAllocation = false;
                allocation.HaveProportion += 1;


                var reslutJosn = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.STATING_ALLOCATION_SAVE_CHANGE_ACTION, reslutJosn);

                //获取双向列表中的下一个
                var linkedNode = linkedList.Find(allocation);
                var nextAllocation = ProcessNextAllocationStating(linkedList, linkedNode);

                //var nextAllocation = nextNode.Value;
                nextAllocation.CanAllocation = true;

                dic[allocation.Key] = allocation;
                dic[nextAllocation.Key] = nextAllocation;

                outMainTrackNumber = allocation.MainTrackNo;
                statingNo = allocation.StatingNo;


            }

        }

        private Allocation ProcessNextAllocationStating(LinkedList<Allocation> linkedList, LinkedListNode<Allocation> currentNode)
        {
            //下一个可分配站点
            Allocation nextAllocation = GetCanAllocationStating(linkedList, currentNode, currentNode);

            if (nextAllocation != null)
                return nextAllocation;


            //没有获取到可分配站点，清空所有分配记录，默认返回第一个站点
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, Allocation>(SusRedisConst.STATING_ALLOCATION);
            foreach (var item in linkedList)
            {
                item.CanAllocation = false;
                item.HaveProportion = 0;

                dic[item.Key] = item;
            }


            return linkedList.First.Value;
        }

        /// <summary>
        /// 获取可分配的站点缓存信息
        /// </summary>
        /// <param name="linkedList">链表</param>
        /// <param name="currentNode">当前节点</param>
        /// <param name="baseNode">基准节点 为空 </param>
        /// <returns></returns>
        private Allocation GetCanAllocationStating(LinkedList<Allocation> linkedList, LinkedListNode<Allocation> currentNode, LinkedListNode<Allocation> baseNode)
        {
            //


            LinkedListNode<Allocation> nextNode = null;
            if (currentNode == linkedList.Last)
            {
                nextNode = linkedList.First;
            }
            else
            {
                nextNode = currentNode.Next;
            }



            if (nextNode.Value.Proportion > nextNode.Value.HaveProportion)
            {
                return nextNode.Value;
            }
            else
            {
                if (nextNode == baseNode)
                    return null;

                return GetCanAllocationStating(linkedList, nextNode, baseNode);
            }

        }

    }
}

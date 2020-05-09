using Newtonsoft.Json;
using SusNet.Common.BusModel;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.Cus;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Products
{
    public class CANProductsValidService : ServiceBase
    {
        private readonly static CANProductsValidService _instance = new CANProductsValidService();
        private CANProductsValidService() { }
        public static CANProductsValidService Instance
        {
            get { return _instance; }
        }
        /// <summary>
        /// 获取返工衣架信息
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="defectCode"></param>
        /// <returns></returns>
        public DaoModel.HangerReworkRecord GetReworHangerReworkRecordInfo(int mainTrackNo, int hangerNo, int statingNo, int defectCode)
        {
            var sql = new StringBuilder("select * from HangerReworkRecord where HangerNo =? and returnWorkSiteNo=?");
            var reworHangerReworkRecordInfo = QueryForObject<DaoModel.HangerReworkRecord>(sql, null, false, hangerNo, statingNo);
            if (null == reworHangerReworkRecordInfo)
            {
                var ex = new ApplicationException(string.Format("【衣架返工疵点请求】 找不到返工衣架记录信息! 主轨:{0} 衣架:{1} 站点:{2} 疵点代码:{3}", mainTrackNo, hangerNo, statingNo, defectCode));
                tcpLogError.Error(ex);
                throw ex;
            }

            return reworHangerReworkRecordInfo;
        }
        /// <summary>
        /// 计算返工站是否满足进站条件，如果满足返工到达的站点号
        /// </summary>
        /// <param name="hRecord"></param>
        /// <param name="nextStatingNo"></param>
        /// <returns></returns>
        public bool CalculationWhetherAllowIncomeStatingAndNextStating(DaoModel.HangerReworkRecord hRecord, ref string nextStatingNo)
        {
            nextStatingNo = hRecord.SiteNo?.Trim();
            return true;
        }
        //private static readonly StatingServiceImpl statiningService = new StatingServiceImpl();
        private static readonly object locObj = new Object();
        /// <summary>
        /// 注册衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangingStationNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="p"></param>
        /// <param name="wp"></param>
        /// <param name="hangingPiece"></param>
        /// <param name="hangingTag">
        /// 0：正常衣架
        /// 1:挂片站出站，未进普通站 再挂片
        /// 2:挂片站出站，进了普通站或到了加工点读卡器， 拿来再挂片挂片
        /// 3:挂片站出站，进了普通站，有生产了工序
        /// </param>
        public void BindHangerToProducts(string mainTrackNo, string hangingStationNo,
            string hangerNo, out DaoModel.ProductsModel p, out DaoModel.WaitProcessOrderHanger wp, out HangerProductFlowChartModel hangingPiece, ref int hangingTag)
        {
            lock (locObj) {
                /**
                 * 同一衣架 
                 *  0.正常衣架出站
                    1.挂片站出站，未进普通站 再挂片 挂片数量是否累加？不加
                    2.挂片站出站，进了普通站或到了加工点读卡器， 拿来再挂片挂片 挂片数量都不累加，直接出站，衣架不变负数，去找下一工序分配站点
                    3.挂片站出站，进了普通站，有生产了工序，再出站，再拿回来挂片，挂片数要加，普通站生产记录(衣架变负数)
                 * */

                /// 0：正常衣架
                /// 1:挂片站出站，未进普通站 再挂片
                /// 2:挂片站出站，进了普通站或到了加工点读卡器， 拿来再挂片挂片
                /// 3:挂片站出站，进了普通站，有生产了工序
                hangingTag = GetHangingPieceHangerTag(mainTrackNo, hangingStationNo, hangerNo);
                //清除衣架缓存
                var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                if (dicHangerStatingInfo.ContainsKey(hangerNo))
                {
                    var chList = dicHangerStatingInfo[hangerNo];
                    //清除站内数及分配数
                    var incomeNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == int.Parse(mainTrackNo) && hpfc.StatingNo.Value == int.Parse(hangingStationNo) && hpfc.HangerStatus == 1);
                    var onlineNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == int.Parse(mainTrackNo) && hpfc.StatingNo.Value == int.Parse(hangingStationNo) && hpfc.HangerStatus == 0);
                    // chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    ////站内数-1
                    //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    for (var index = 0; index < incomeNums; index++)
                    {
                        //站内数-1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(hangingStationNo), OnLineSum = -1 };
                        //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    }
                    for (var index = 0; index < onlineNums; index++)
                    {
                        //站点分配数-1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(hangingStationNo), AllocationNum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    }
                }
                SusCacheBootstarp.Instance.ClearHangerAllCache(hangerNo.ToString());
                //switch (hangingTag)
                //{
                //    case 0:
                //        //清除衣架缓存
                //        SusCacheBootstarp.Instance.ClearHangerAllCache(hangerNo.ToString());
                //        break;
                //    case 1:
                //        //清除衣架缓存
                //        SusCacheBootstarp.Instance.ClearHangerAllCache(hangerNo.ToString());
                //        break;
                //    case 2:
                //        //清除衣架缓存
                //        SusCacheBootstarp.Instance.ClearHangerAllCache(hangerNo.ToString());
                //        break;
                //    case 3:
                //        break;
                //}
                var siteGroupNo = StatingServiceImpl.Instance.GetGroupNo(int.Parse(mainTrackNo));
                p = GetWaitBindHangerProductList(hangingStationNo);

                tcpLogInfo.Info(string.Format("【挂片出战】挂片站:【{0}】可绑定产品条数:【{0}】", hangingStationNo, 1));

                //获取在线制品
                var onlineProductsFlowChartList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);

                var key = string.Format("{0}:{1}", short.Parse(mainTrackNo), p.ProductionNumber);
                IList<ProductsFlowChartCacheTempModel> onLineProcessFlowChartList = null;  //SusCacheMain.Cache.Get(key) as List<ProductsFlowChartCacheTempModel>;
                onlineProductsFlowChartList.TryGetValue(key, out onLineProcessFlowChartList);
                if (null == onLineProcessFlowChartList)
                {
                    throw new NoFoundOnlineProductsException(string.Format("【挂片出战】主轨:{0} 站点:{1} 衣架号:{2} 找不到上线产品!", mainTrackNo, hangingStationNo, hangerNo));
                }
                var pFlowChartRelationList = onLineProcessFlowChartList.Select(f => new ProductsFlowChartCacheTempModel()
                {
                    ProcessFlowChartId = f.ProcessFlowChartId,
                    BoltProcessFlowId = f.BoltProcessFlowId,
                    ProcessFlowId = f.ProcessFlowId,
                    ProcessFlowChartFlowRelationId = f.ProcessFlowChartFlowRelationId,
                    CraftFlowNo = f.CraftFlowNo,
                    FlowNo = f.FlowNo,
                    FlowCode = f.FlowCode,
                    FlowName = f.FlowName,
                    StandardPrice = f.StandardPrice,
                    StanardHours = f.StanardHours,
                    MainTrackNumber = f.MainTrackNumber
                }).ToList<ProductsFlowChartCacheTempModel>().SusDistinct(f => f.FlowNo).ToList();//此排序有bug，待查/zxl/2018年6月22日 17:24:08

                wp = new DaoModel.WaitProcessOrderHanger();
                wp.Id = null;
                wp.GroupNo = siteGroupNo?.Trim();
                wp.MainTrackNumber = short.Parse(mainTrackNo);
                wp.ProductsId = p.Id;
                wp.HangerNo = hangerNo?.Trim();
                wp.ProcessOrderId = p.ProcessOrderId;
                wp.ProcessOrderNo = p.ProcessOrderNo?.Trim();
                wp.FlowChartd = p.ProcessFlowChartId;
                wp.LineName = p.LineName?.Trim();
                wp.PColor = p.PColor?.Trim();
                wp.PSize = p.PSize?.Trim();
                wp.FlowIndex = 0;//挂片工序
                wp.SiteNo = p.HangingPieceSiteNo?.Trim();
                var hangingPieceFlow = GetFirstProcessFlowExt(pFlowChartRelationList);
                wp.ProcessFlowId = hangingPieceFlow.ProcessFlowId;
                wp.FlowNo = hangingPieceFlow.FlowNo?.Trim();
                wp.ProcessFlowCode = hangingPieceFlow.FlowCode?.Trim();
                wp.ProcessFlowName = hangingPieceFlow.FlowName?.Trim();
                wp.Po = p.Po;
                wp.IsFlowChatChange = false;
                wp.SizeNum = Convert.ToInt32(string.IsNullOrEmpty(p.Unit) ? "0" : p.Unit);



                //注册衣架
                RegisterHanger(hangerNo);

                //生成批次
                var batchNo = IdGeneratorService.Instance.GetBatchNo(hangerNo?.Trim());
                wp.BatchNo = batchNo;

                //缓存待生产衣架信息
                var dicWaitProcessOrderHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER);
                if (dicWaitProcessOrderHanger.Count > 0)
                {
                    dicWaitProcessOrderHanger.Remove(hangerNo);
                }
             //   dicWaitProcessOrderHanger.Add(hangerNo, wp);
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER)[hangerNo] = wp;

                //记录挂片信息
                var hangingPieceStatingCapacity = 0;
                var mainKey = string.Format("{0}:{1}", mainTrackNo, hangingStationNo);
                var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.StatingModel>(SusRedisConst.STATING_TABLE);
                if (dicStatingCache.ContainsKey(mainKey))
                {
                    hangingPieceStatingCapacity = dicStatingCache[mainKey].Capacity.Value;
                }

                hangingPiece = new HangerProductFlowChartModel()
                {
                    GroupNo = siteGroupNo?.Trim(),
                    HangerNo = hangerNo,
                    StatingNo = short.Parse(hangingStationNo),
                    MainTrackNumber = short.Parse(mainTrackNo),
                    BatchNo = batchNo,
                    FlowIndex = string.IsNullOrEmpty(hangingPieceFlow?.CraftFlowNo) ? -1 : int.Parse((hangingPieceFlow?.CraftFlowNo)),
                    isHangingPiece = true,
                    Status = HangerProductFlowChartStaus.Successed.Value,
                    FlowType = 2,
                    ProductsId = p.Id,
                    ProcessChartId = p.ProcessFlowChartId,
                    IsFlowSucess = false,
                    PSize = wp.PSize,
                    PColor = wp.PColor,
                    FlowNo = hangingPieceFlow?.FlowNo,
                    FlowCode = hangingPieceFlow?.FlowCode,
                    FlowName = hangingPieceFlow?.FlowName,
                    StatingCapacity = hangingPieceStatingCapacity,
                    ProcessOrderNo = p.ProcessOrderNo?.Trim(),
                    LineName = p.LineName,
                    StyleNo = p.StyleNo,
                    Num = p.Unit
                };

                var hangingPieceStandardHours = decimal.Parse("0");
                //更新挂片衣架的出站时间
                var dicHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerHangingRequestModel>(SusRedisConst.HANGER_HNGING_PIECE_REQUEST_ACTION);
                if (!dicHanger.ContainsKey(hangerNo))
                {
                    tcpLogError.Info(string.Format("【挂片出战】未找到挂片读卡衣架! 主轨:{0} 站点:{1} 衣架号:{2}", mainTrackNo, hangingStationNo, hangerNo));
                }
                else
                {
                    var dicHangingPieceHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerHangingRequestModel>(SusRedisConst.HANGER_HNGING_PIECE_REQUEST_ACTION);
                    var hangingPieceHanger = dicHangingPieceHanger[hangerNo];
                    hangingPieceHanger.OutSiteDate = DateTime.Now;

                    hangingPieceStandardHours = decimal.Parse(pFlowChartRelationList.Where(f => short.Parse(f.CraftFlowNo) == 1).First().StanardHours);
                    hangingPieceHanger.StanardHours = hangingPieceStandardHours;
                    dicHangingPieceHanger[hangerNo] = hangingPieceHanger;
                    hangingPiece.StanardHours = hangingPieceStandardHours.ToString();
                    hangingPiece.CompareDate = hangingPieceHanger.CompareDate;
                    hangingPiece.isAllocationed = true;
                    hangingPiece.AllocationedDate = DateTime.Now;
                    hangingPiece.FlowId = hangingPieceFlow.ProcessFlowId;
                }




                //RedisValue.


                //WaitProcessOrderHangerDao.Instance.Insert(wp);//.Save(wp, GUIDHelper.GetGuidString());
                //tcpLogInfo.Info(string.Format("衣架【{0}】已成功绑定到制单【{1}】!", hangerNo, wp.ProcessOrderNo));

                #region//生成衣架的工艺图缓存到服务器
                var hangerProcessFlowChartList = new List<HangerProductFlowChartModel>();

                //            var flowIndex = 1;
                foreach (var pf in pFlowChartRelationList)
                {
                    var statingList = onLineProcessFlowChartList.Where(f => f.ProcessFlowChartFlowRelationId.Equals(pf.ProcessFlowChartFlowRelationId));
                    foreach (var ps in statingList)
                    {
                        var hpfc = new HangerProductFlowChartModel();
                        hpfc.GroupNo = siteGroupNo?.Trim();
                        hpfc.HangerNo = hangerNo;
                        hpfc.BatchNo = batchNo;
                        hpfc.MainTrackNumber = (short)pf.MainTrackNumber;// short.Parse(mainTrackNo);
                        hpfc.ProductsId = p.Id;
                        hpfc.IsHangerSucess = false;
                        hpfc.IsFlowSucess = false;
                        hpfc.ProcessChartId = p.ProcessFlowChartId;
                        hpfc.FlowIndex = short.Parse(pf.CraftFlowNo);
                        hpfc.FlowId = pf.ProcessFlowId;
                        hpfc.FlowNo = pf.FlowNo;
                        hpfc.FlowCode = pf.FlowCode?.Trim();
                        hpfc.FlowName = pf.FlowName?.Trim();
                        hpfc.StanardHours = pf.StanardHours;
                        hpfc.StandardPrice = pf.StandardPrice;
                        hpfc.StatingNo = short.Parse(string.IsNullOrEmpty(ps.StatingNo) ? "-1" : ps.StatingNo);
                        hpfc.StatingId = ps.StatingId;
                        hpfc.StatingCapacity = ps.StatingCapacity;
                        hpfc.IsReworkSourceStating = false;
                        hpfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                        hpfc.FlowType = 0;
                        hpfc.PColor = p.PColor;
                        hpfc.PSize = p.PSize;
                        hpfc.IsReceivingAllColor = ps.IsReceivingAllColor;
                        hpfc.IsReceivingAllSize = ps.IsReceivingAllSize;
                        hpfc.IsReceivingAllPoNumber = ps.IsReceivingAllPoNumber;
                        hpfc.ReceivingColor = ps.ReceivingColor;
                        hpfc.ReceivingPoNumber = ps.ReceivingPoNumber;
                        hpfc.ReceivingSize = ps.ReceivingSize;
                        hpfc.IsEnabled = ps.IsEnabled;
                        hpfc.Proportion = ps.Proportion;  //分摊比例
                                                          //HangerProductFlowChartDao.Instance.Insert(hpfc);
                        hpfc.IsReceivingHanger = ps.IsReceivingHanger;
                        hpfc.IsReceivingHangerStating = ps.IsReceivingHangerStating;

                        hpfc.IsMergeForward = ps.IsMergeForward;
                        hpfc.MergeProcessFlowChartFlowRelationId = ps.MergeProcessFlowChartFlowRelationId;
                        hpfc.ProcessFlowChartFlowRelationId = ps.ProcessFlowChartFlowRelationId;
                        hpfc.IsProduceFlow = ps.IsProduceFlow;
                        hpfc.MergeFlowNo = ps.MergeFlowNo;
                        hpfc.ProcessOrderNo = p.ProcessOrderNo?.Trim();
                        hpfc.LineName = p.LineName;
                        hpfc.StyleNo = p.StyleNo;
                        hpfc.Num = p.Unit;
                        hpfc.StatingRoleCode = ps.StatingRoleCode?.Trim();
                        hangerProcessFlowChartList.Add(hpfc);
                    }
                    //              flowIndex++;
                }

                //记录挂片站
                hangerProcessFlowChartList.Add(hangingPiece);

                // var hpfcJson = Newtonsoft.Json.JsonConvert.SerializeObject(hangerProcessFlowChartList);
                var dicHangerProcessFlowChart = NewCacheService.Instance.GetHangerFlowChartListForRedisCache(); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (dicHangerProcessFlowChart.Count > 0)
                {
                    dicHangerProcessFlowChart.Remove(hangerNo);
                }
#warning 缓存衣架工艺图
                //缓存衣架工艺图
                dicHangerProcessFlowChart.Add(hangerNo, hangerProcessFlowChartList);
                #endregion

                //发布衣架工艺图
                // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PROCESS_FLOW_CHART, hpfcJson);

                // CacheService.Instance.CacheProcessFlowChart(p.ProcessFlowChart?.Id);
                //CacheService.Instance.CacheProcessFlowChart(p.ProcessFlowChart?.Id);
                //foreach (var pc in pFlowChartList)
                //{
                //    //HangerProductFlowChart
                //    var flowIndex = 1;
                //    foreach (var pf in pc.ProcessFlowChartFlowRelationList)
                //    {
                //        foreach (var ps in pf.ProcessFlowStatingItemList)
                //        {
                //            var hpfc = new DaoModel.HangerProductFlowChart() {
                //                HangerNo = hangerNo,
                //                MainTrackNumber = short.Parse(mainTrackNo),
                //                ProductsId = p.Id,
                //                IsHangerSucess = false,
                //                IsFlowSucess = false,
                //                ProcessChartId = p.ProcessFlowChart?.Id,
                //                FlowIndex = flowIndex,//short.Parse(pf.CraftFlowNo),
                //                FlowId=pf.ProcessFlow?.Id,
                //                FlowNo=pf.FlowNo,
                //                FlowCode =pf.FlowCode,
                //                FlowName=pf.FlowName,
                //                StatingNo=short.Parse(ps.No),
                //                StatingId=ps.Stating?.Id,
                //                StatingCapacity=ps.Stating?.Capacity,
                //                IsReworkSourceStating=false,
                //                Status=0,
                //                FlowType=0,
                //                PColor=p.PColor,
                //                PSize=p.PSize
                //            };
                //            HangerProductFlowChartDao.Instance.Insert(hpfc);
                //        }
                //        flowIndex++;
                //    }
                //}
            }
        }
        /// <summary>
        /// 获取挂片站衣架的状态
        /// 
        /// 0：正常衣架
        /// 1:挂片站出站，未进普通站 再挂片
        /// 2:挂片站出站，进了普通站或到了加工点读卡器， 拿来再挂片挂片
        /// 3:挂片站出站，进了普通站，有生产了工序
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangingStationNo"></param>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        public int GetHangingPieceHangerTag(string mainTrackNo, string hangingStationNo, string hangerNo)
        {
            //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChart.ContainsKey(hangerNo))
            {
                return 0;
            }
            var listHanger = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
            ////进普通站了生产完成
            var isExistOutFlow = listHanger.Where(f => null != f.StatingNo
            && f.StatingNo.Value != int.Parse(hangingStationNo) && null != f.Status && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            if (isExistOutFlow)
            {
                var listPartiallyProductsHanger = listHanger.Where(f => null != f.StatingNo
                && f.StatingNo.Value != int.Parse(hangingStationNo)
                && null != f.Status && f.Status.Value == HangerProductFlowChartStaus.Producting.Value);
                foreach (var hs in listPartiallyProductsHanger)
                {
                    var mainTrackNumber = hs.MainTrackNumber.Value;
                    var statingNo = hs.StatingNo.Value;
                    //站内数-1
                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    ////站点分配数-1
                    //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                    //清除硬件缓存
                    //if (CANTcp.client != null)
                    //    CANTcp.client.ClearHangerCache(hs.MainTrackNumber.Value, int.Parse(statingNo.ToString()), int.Parse(hs.HangerNo), "FF");
                    if (CANTcpServer.server != null)
                    {
                        CANTcpServer.server.ClearHangerCache(hs.MainTrackNumber.Value, int.Parse(statingNo.ToString()), int.Parse(hs.HangerNo), "FF");
                    }
                }
                //清除已分配但未进站(手动拿到挂片站挂片的情况)
                var nonInSiteAgainHangpieceListByEffectHalf = listHanger.Where(f => null != f.StatingNo
             && f.StatingNo.Value != int.Parse(hangingStationNo) && null != f.Status && f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value && f.isAllocationed);
                foreach (var nItem in nonInSiteAgainHangpieceListByEffectHalf)
                {
                    //清除硬件缓存
                    if (CANTcp.client != null)
                        CANTcp.client.ClearHangerCache(nItem.MainTrackNumber.Value, nItem.StatingNo.Value, int.Parse(nItem.HangerNo), "FF");
                    if (null != CANTcpServer.server)
                    {
                        CANTcpServer.server.ClearHangerCache(nItem.MainTrackNumber.Value, nItem.StatingNo.Value, int.Parse(nItem.HangerNo), "FF");
                    }
                    ////站点分配数-1
                    var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = nItem.MainTrackNumber.Value, StatingNo = nItem.StatingNo.Value, AllocationNum = -1 };
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                }
                return 3;
            }

            //进普通站了没生产完成
            var inComeSiteNoSucessedList = listHanger.Where(f => null != f.StatingNo
                && f.StatingNo.Value != int.Parse(hangingStationNo) && null != f.Status && f.Status.Value == HangerProductFlowChartStaus.Producting.Value);
            //修正站内数
            foreach (var ics in inComeSiteNoSucessedList)
            {
                var mainTrackNumber = ics.MainTrackNumber.Value;
                var statingNo = ics.StatingNo.Value;
                //站内数-1
                var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                //清除硬件缓存
                if (null != CANTcpServer.server)
                {
                    CANTcpServer.server.ClearHangerCache(ics.MainTrackNumber.Value, int.Parse(statingNo.ToString()), int.Parse(ics.HangerNo), "FF");
                }
                if (null != CANTcp.client)
                    CANTcp.client.ClearHangerCache(ics.MainTrackNumber.Value, int.Parse(statingNo.ToString()), int.Parse(ics.HangerNo), "FF");

                ////修正删除的站内数及明细、缓存
                //var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                //hnssocDeleteStating.Action = 3;
                //hnssocDeleteStating.HangerNo = hangerNo;
                //hnssocDeleteStating.MainTrackNumber = ics.MainTrackNumber.Value;
                //hnssocDeleteStating.StatingNo = ics.StatingNo.Value;
                //hnssocDeleteStating.FlowNo = ics.FlowNo;
                //hnssocDeleteStating.FlowIndex = ics.FlowIndex.Value;
                ////hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                //var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                //SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);

            }
            var isExistInComeSiteNoSuessed = inComeSiteNoSucessedList.Count() > 0;
            if (isExistInComeSiteNoSuessed) return 2;

            //未进普通站 再挂片
            var nonInSiteAgainHangpieceList = listHanger.Where(f => null != f.StatingNo
              && f.StatingNo.Value != int.Parse(hangingStationNo) && null != f.Status && f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value && f.isAllocationed);
            foreach (var nItem in nonInSiteAgainHangpieceList)
            {
                if (null != CANTcp.client)
                {
                    //清除硬件缓存
                    CANTcp.client.ClearHangerCache(nItem.MainTrackNumber.Value, nItem.StatingNo.Value, int.Parse(nItem.HangerNo), "FF");
                }
                if (null != CANTcpServer.server)
                    CANTcpServer.server.ClearHangerCache(nItem.MainTrackNumber.Value, nItem.StatingNo.Value, int.Parse(nItem.HangerNo), "FF");
                ////站点分配数-1
                var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = nItem.MainTrackNumber.Value, StatingNo = nItem.StatingNo.Value, AllocationNum = -1 };
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
            }
            return 1;
        }
        public void RegisterHanger(string hangerNo)
        {
            var hangerRegierThread = new HangerRegierThread();
            hangerRegierThread.Hanger = new DaoModel.Hanger()
            {
                HangerNo = long.Parse(hangerNo),
                RegisterDate = DateTime.Now
            };
            var threadRegister = new Thread(new ThreadStart(hangerRegierThread.RegisterHanger));
            threadRegister.Start();
        }
        public IList<DaoModel.ProductsModel> GetAllOnlineProductList()
        {
            //var sql = new StringBuilder(@"select * from Products where HangingPieceSiteNo=? and Status=? and productionNumber=?");
            //var list = Query<DaoModel.Products>(sql, null, false, SusNet.Common.Utils.HexHelper.HexToTen(hangingStationNo), ProductsStatusType.Onlineed.Value, productNumber);
            var sql = new StringBuilder(@"
select T1.Id,T1.PColor,T1.PSize,T1.Unit,T1.HangingPieceSiteNo,T1.ProductionNumber,T2.Id ProcessFlowChartId,T4.Id ProcessOrderId,T4.POrderNo ProcessOrderNo,T2.LinkName LineName
,t1.PO as Po
 from Products T1 INNER JOIN ProcessFlowChart T2 ON T2.Id=T1.PROCESSFLOWCHART_Id
INNER JOIN ProcessFlowVersion T3 ON T3.Id=T2.PROCESSFLOWVERSION_Id
INNER JOIN ProcessOrder T4 ON T4.Id=T3.PROCESSORDER_Id
 where  T1.Status=?");
            var list = Query<DaoModel.ProductsModel>(sql, null, true, ProductsStatusType.Onlineed.Value);
            return list;
        }

        /// <summary>
        /// 根据挂片站号及排产号获取当前出站的产品
        /// </summary>
        /// <param name="hangingStationNo"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public DaoModel.ProductsModel GetWaitBindHangerProductList(string hangingStationNo)
        {
            //获取在线制品根据挂片站
            var listCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.ProductsModel>(SusRedisConst.ON_LINE_PRODUCTS);
            if (listCache.Count == 0)
            {
                //var sql = new StringBuilder(@"select * from Products where HangingPieceSiteNo=? and Status=? and productionNumber=?");
                //var list = Query<DaoModel.Products>(sql, null, false, SusNet.Common.Utils.HexHelper.HexToTen(hangingStationNo), ProductsStatusType.Onlineed.Value, productNumber);
                var sql = new StringBuilder(@"
select T1.Id,T1.PColor,T1.PSize,T1.Unit,T1.HangingPieceSiteNo,T1.ProductionNumber,T2.Id ProcessFlowChartId,T4.Id ProcessOrderId,T4.POrderNo ProcessOrderNo,T2.LinkName LineName
 from Products T1 INNER JOIN ProcessFlowChart T2 ON T2.Id=T1.PROCESSFLOWCHART_Id
INNER JOIN ProcessFlowVersion T3 ON T3.Id=T2.PROCESSFLOWVERSION_Id
INNER JOIN ProcessOrder T4 ON T4.Id=T3.PROCESSORDER_Id
 where T1.HangingPieceSiteNo=@HangingPieceSiteNo  and T1.Status=@Status");
                var list = DapperHelp.Query<DaoModel.ProductsModel>(sql.ToString(), new { HangingPieceSiteNo = hangingStationNo, Status = ProductsStatusType.Onlineed.Value }).ToList<DaoModel.ProductsModel>(); //Query<DaoModel.ProductsModel>(sql, null, true, SusNet.Common.Utils.HexHelper.HexToTen(hangingStationNo), ProductsStatusType.Onlineed.Value);
                if (list.Count() == 1)
                {
                    return list[0];
                }
                var ex = new NoFoundOnlineProductsException(string.Format("【挂片站出站衣架绑定】 挂片站【{0}】 】 未找到上线的产品!", hangingStationNo));
                errorLog.Error("GetWaitBindHangerProductList", ex);
                throw ex;
            }
            var lc = listCache.Where(f => f.Value.HangingPieceSiteNo.Equals(hangingStationNo)).Select(f => f.Value);
            if (null == lc)
            {
                var ex = new NoFoundOnlineProductsException(string.Format("【挂片站出站衣架绑定】 挂片站【{0}】 】 未找到上线的产品!", hangingStationNo));
                errorLog.Error("GetWaitBindHangerProductList", ex);
                throw ex;
            }
            return lc.Single<DaoModel.ProductsModel>();
        }

        public DaoModel.ProcessFlowChartFlowRelation GetFirstProcessFlow(DaoModel.ProcessFlowChart pfChart)
        {
            var pfcProcessFlowList = GetFlowChartFlowItemList(pfChart?.Id);
            if (string.IsNullOrEmpty(pfChart.BoltProcessFlowId))
            {
                return pfcProcessFlowList.Where(f => f.CraftFlowNo.Equals("1")).Single();
            }
            // DaoModel.ProcessFlowChart
            //存在挂片工序
            return GetFlowChartProcessFlow(pfChart.Id, pfChart.BoltProcessFlowId);
        }
        public ProductsFlowChartCacheTempModel GetFirstProcessFlowExt(List<ProductsFlowChartCacheTempModel> pfcList)
        {
            var first = pfcList.Where(f => f.BoltProcessFlowId != null && f.ProcessFlowId.Equals(f.BoltProcessFlowId));
            if (first.ToList<ProductsFlowChartCacheTempModel>().Count > 0)
            {
                return first.First<ProductsFlowChartCacheTempModel>();
            }
            // DaoModel.ProcessFlowChart
            //存在挂片工序
            return pfcList.Where(f => f.CraftFlowNo.Equals("1")).Single(); ;
        }
        /// <summary>
        /// 获取工艺路线图的所有工序列表
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChartFlowRelation> GetFlowChartFlowItemList(string flowChartId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .List<DaoModel.ProcessFlowChartFlowRelation>();
            return list;
        }
        /// <summary>
        /// 根据路线图+工序Id 获取路线图工序详情
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <param name="processFlowId"></param>
        /// <returns></returns>
        private DaoModel.ProcessFlowChartFlowRelation GetFlowChartProcessFlow(string flowChartId, string processFlowId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId and PROCESSFLOW_Id=:processFlowId");
            var session = SessionFactory.OpenSession();
            var pfcFlowRelation = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .SetString("processFlowId", processFlowId).UniqueResult<DaoModel.ProcessFlowChartFlowRelation>();
            return pfcFlowRelation;
        }

        /// <summary>
        /// 获取工序站点列表
        /// </summary>
        public IList<DaoModel.ProcessFlowStatingItem> GetHangerNextProcessFlowStatingList(string pfChartId, string processFlowId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId and PROCESSFLOW_Id=:processFlowId");
            var session = SessionFactory.OpenSession();
            var pfcFlowRelation = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", pfChartId)
                .SetString("processFlowId", processFlowId).UniqueResult<DaoModel.ProcessFlowChartFlowRelation>();
            if (null == pfcFlowRelation)
            {
                tcpLogInfo.Info(string.Format("【第一道工序错误】--->路线图Id【{0}】,工序Id【{1}】 不存在!", pfChartId, processFlowId));
                return new List<DaoModel.ProcessFlowStatingItem>();
            }
            //获取可接收的衣架
            var sqlProcessFlowItem = string.Format("select * from ProcessFlowStatingItem where IsReceivingHanger=1 and PROCESSFLOWCHARTFLOWRELATION_Id=:ProcessFlowChartFlowRelationId");
            var statingList = session.CreateSQLQuery(sqlProcessFlowItem)
                .AddEntity(typeof(DaoModel.ProcessFlowStatingItem))
                .SetString("ProcessFlowChartFlowRelationId", pfcFlowRelation.Id)
                .List<DaoModel.ProcessFlowStatingItem>();

            return statingList;
        }

        /// <summary>
        /// 获取衣架挂片站
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="outSiteMainTrackNumber"></param>
        /// <param name="outSiteStatingNo"></param>
        public void GetSucessHangerNoHangingPieceStating(string hangerNo, int mainTrackNumber, int statingNo, ref int outSiteMainTrackNumber, ref int outSiteStatingNo)
        {
            //var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChart.ContainsKey(hangerNo))
            {
                tcpLogError.ErrorFormat("衣架:{0} 主轨:{1} 站点:{2} 找不到挂片站!", hangerNo, mainTrackNumber, statingNo);
                return;
            }
            var hangerProcessFlowChart = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo).Where(f => f.isHangingPiece).First();
            outSiteMainTrackNumber = hangerProcessFlowChart.MainTrackNumber.Value;
            outSiteStatingNo = hangerProcessFlowChart.StatingNo.Value;

            ////更新挂片站在线数
            ////下一站分配数+1
            //var allocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = outSiteMainTrackNumber, StatingNo = outSiteStatingNo, AllocationNum = 1 };
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(allocationNumModel));

        }
        /// <summary>
        /// 获取衣架最后一道工序制作站点
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="outSiteMainTrackNumber"></param>
        /// <param name="outSiteStatingNo"></param>
        public void GetSucessHangerEndStating(string hangerNo, int mainTrackNumber, int statingNo, ref int outSiteMainTrackNumber, ref int outSiteStatingNo)
        {
            // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChart.ContainsKey(hangerNo))
            {
                tcpLogError.ErrorFormat("衣架:{0} 主轨:{1} 站点:{2} 获取衣架最后一道工序制作站点异常!", hangerNo, mainTrackNumber, statingNo);
                return;
            }
            var hangerProcessFlowChart = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo).Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value && f.StatingNo.Value != -1 && null == f.MergeProcessFlowChartFlowRelationId).OrderByDescending(f => f.FlowIndex.Value).First();
            outSiteMainTrackNumber = hangerProcessFlowChart.MainTrackNumber.Value;
            outSiteStatingNo = hangerProcessFlowChart.StatingNo.Value;
        }

        /// <summary>
        /// 获取所有未删除的制品
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Domain.ProductsModel> GetAllProducts()
        {
            string sql = @"select Id,GroupNo,ProductionNumber,ImplementDate,HangingPieceSiteNo,ProcessOrderNo,Status,CustomerPurchaseOrderId,OrderNo
                        , StyleNo, PColor, Po, PSize, LineName, FlowSection, Unit, TaskNum, OnlineNum, TodayHangingPieceSiteNum, TodayProdOutNum, TotalProdOutNum
                        , TodayBindCard, TodayRework, TotalHangingPieceSiteNum, TotalRework, TotalBindNum

                        from Products where Deleted <> 1";
            var products = Dao.DapperHelp.Query<Domain.ProductsModel>(sql);

            if (products == null || products.Count() == 0)
                return new List<DaoModel.ProductsModel>();
            else
                return products.ToList();
        }
    }
    public class HangerRegierThread
    {
        public SuspeSys.Domain.Hanger Hanger { set; get; }
        public void RegisterHanger()
        {
            if (null == Hanger)
            {
                return;
            }
            var waitRegisterHanger = DapperHelp.QueryForObject<SuspeSys.Domain.Hanger>("select * from Hanger where HangerNo=@HangerNo", new { HangerNo = Hanger.HangerNo });
            if (null == waitRegisterHanger)
            {
                HangerDao.Instance.Save(Hanger);
            }
        }
    }
}

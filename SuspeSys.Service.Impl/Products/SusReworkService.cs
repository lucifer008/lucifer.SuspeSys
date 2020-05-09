using log4net;
using Newtonsoft.Json;
using Sus.Net.Common.Constant;
using Sus.Net.Common.Model;
using Sus.Net.Common.Utils;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusThread
{
    public class SusReworkService : ServiceBase
    {
        private SusReworkService() { }
        private static readonly object locObj = new object();
        private static readonly SusReworkService _instance = new SusReworkService();
            
        public static SusReworkService Instance
        {
            get
            {
                return _instance;
            }
        }
        public bool RegisterReworkHanger(string hangerNo, int mainTrackNumber, string statingNo, ref string error)
        {

            try
            {
                var dicHangerReworkRequestQueueCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerReworkRequestQueue>(SusRedisConst.HANGER_REWORK_REQUEST_QUEUE); //HANGER_REWORK_REQUEST_QUEUE

                //清除同站返工衣架
                IList<string> hangerNoList = new List<string>();
                foreach (var key in dicHangerReworkRequestQueueCache.Keys)
                {
                    var v = dicHangerReworkRequestQueueCache[key];
                    if (v.MainTrackNumber.Value == short.Parse(mainTrackNumber.ToString()) && v.StatingNo.Equals(statingNo))
                    {
                        if (!hangerNoList.Contains(key?.Trim()))
                        {
                            hangerNoList.Add(key?.Trim());
                        }
                    }
                }
                foreach (var h in hangerNoList)
                {
                    if (dicHangerReworkRequestQueueCache.ContainsKey(h))
                    {
                        dicHangerReworkRequestQueueCache.Remove(h);
                    }
                }
                if (dicHangerReworkRequestQueueCache.ContainsKey(hangerNo))
                {
                    dicHangerReworkRequestQueueCache.Remove(hangerNo);
                }

                var hrrq = new HangerReworkRequestQueue();
                hrrq.HangerNo = hangerNo;
                hrrq.MainTrackNumber = (short)mainTrackNumber;
                hrrq.StatingNo = statingNo;
                hrrq.Status = 0;
                dicHangerReworkRequestQueueCache.Add(hangerNo, hrrq);
                var jsonHangerReworkRequestQueue = JsonConvert.SerializeObject(hrrq);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_REWORK_REQUEST_QUEUE_ACTION, jsonHangerReworkRequestQueue);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                errorLog.Error(string.Format("【衣架返工】 注册衣架失败! 主轨:{0} 衣架号:{1} 站点:{2}", mainTrackNumber, hangerNo, statingNo), ex);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="fcList"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag">
        /// 00＝允许出站；
        /// 01＝不允许出站，站点不存在
        /// 02＝不允许出站，疵点错误，请重新输入疵点
        /// 03＝不允许出站，工序代码错误或者未找到生产的工序代码
        /// 04＝不允许出站，站点满或停止工作
        /// 05＝不允许出站，不允许返工操作
        /// 06=不允许出站，站点员工未登录
        /// 07=不允许出站，（同一站点同一时间内重复返工）同一衣架号已出站，拿回来再返工的情况，针对返工还在进行中，取下衣架又拿到返工发起站点做返工。
        /// 08=不允许出站 其他
        /// </param>
        public void ReworkHander(int mainTrackNumber, int statingNo, List<FlowDefectCodeItem> fcList, ref string nextStatingNo, ref int hangerNo, ref int tag, ref int outMainTrackNumber, ref bool isRequireBridge, ref string info)
        {
            var flowNoList = new List<FlowDefectRelation>();
            var relFccHList = fcList.OrderBy(f => f.HexAddress);
            var bData = new List<byte>();
            foreach (var fdcItem in relFccHList)
            {
                if (!fdcItem.HexAddress.Equals(HexHelper.TenToHexString4Len(SuspeConstants.address_ReturnEnd)))
                {
                    bData.AddRange(fdcItem.Data);
                }
            }
            var flowDefectStr = AssicUtils.DecodeByBytes(bData.ToArray());
            tcpLogHardware.Info(string.Format("【衣架返工工序号及疵点代码】 收到下位机的工序及疵点Assic码:{0}", flowDefectStr));
            var flowOrDeflects = flowDefectStr.Split(';');
            var strFlowNoList2 = new List<string>();
            var strDefectCodeList = new List<string>();
            foreach (var fd in flowOrDeflects)
            {
                var fds = fd.Split(',');
                //var fcFlowCode = fds.Length > 0 ? Convert.ToString(fds[0]) : "";
                var fcFlowNo = fds.Length > 0 ? Convert.ToString(fds[0]) : "";
                var fcDefectCode = fds.Length > 1 ? Convert.ToString(fds[1]) : "";
                if (!string.IsNullOrEmpty(fcDefectCode))
                {
                    fcDefectCode = fcDefectCode.Replace("@", "");
                }
                if (string.IsNullOrEmpty(fcFlowNo))
                {
                    fcList.Clear();
                    ClearReworkHangerRequest(mainTrackNumber, statingNo);
                    tag = 8;
                    var ex = new FlowNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}工序号及疵点(ASSIC码) 工序为空! ", mainTrackNumber, statingNo, flowDefectStr));
                    tcpLogHardware.Error(ex);
                    throw ex;
                }
                //检查工序和疵点代码是否存在
                //ReworkDefectNotFoundException

                //
                var fdRelation = new FlowDefectRelation() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, FlowNo = fcFlowNo?.Trim()?.Replace("@", ""), DefectCode = fcDefectCode };
                flowNoList.Add(fdRelation);

                strFlowNoList2.Add(fcFlowNo?.Trim()?.Replace("@", ""));
                strDefectCodeList.Add(fcDefectCode?.Trim());
            }

            //检查疵点
            var isCheckDefectCode = SystemParameterService.Instance.GetHangUpLineProductsLineValue(SystemParameterHangUpProductsLine.VertifyFailCode, mainTrackNumber.ToString(), statingNo.ToString());
            if (isCheckDefectCode.Equals("1"))
            {
                var isExDefectCode = CheckReworkDefectCode(strDefectCodeList);
                if (!isExDefectCode)
                {
                    fcList.Clear();
                    ClearReworkHangerRequest(mainTrackNumber, statingNo);
                    tag = 2;
                    var ex = new ReworkDefectNotFoundException(string.Format("疵点代码【{0}】不存在!", string.Join(",", strDefectCodeList)));
                    tcpLogHardware.Error(ex);
                    throw ex;
                }
            }
            //var dicCondition = new Dictionary<string, string>();
            //dicCondition.Add("MainTrackNumber", mainTrackNumber + "");
            //dicCondition.Add("StatingNo", statingNo + "");
            //var sql = string.Format("select * from HangerReworkRequestQueue where MainTrackNumber=:MainTrackNumber and StatingNo=:StatingNo AND Status=0");
            var dicHangerReworkRequestQueueCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerReworkRequestQueue>(SusRedisConst.HANGER_REWORK_REQUEST_QUEUE);
            HangerReworkRequestQueue hrQueue = null;
            foreach (var key in dicHangerReworkRequestQueueCache.Keys)
            {
                var hrqTmp = dicHangerReworkRequestQueueCache[key];
                if (hrqTmp.StatingNo.Equals(statingNo.ToString()) && hrqTmp.MainTrackNumber.Value == short.Parse(mainTrackNumber.ToString()))
                {
                    hrQueue = hrqTmp;
                    break;
                }
            }
            // var hrQueue = QueryForObject<HangerReworkRequestQueue>(sql, false, dicCondition);
            if (null == hrQueue)
            {
                fcList.Clear();
                tag = 8;
                var ex = new ReworkHangerNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}未找到返工衣架! ", mainTrackNumber, statingNo));
                ClearReworkHangerRequest(mainTrackNumber, statingNo);
                errorLog.Error(ex);
                throw ex;
            }
            hangerNo = int.Parse(hrQueue.HangerNo);

            //检查返工工序
            var isExFlowNo = CheckReworkFowCode(hangerNo.ToString(), strFlowNoList2);
            if (!isExFlowNo)
            {
                fcList.Clear();
                tag = 3;
                ClearReworkHangerRequest(mainTrackNumber, statingNo);
                var ex = new ReworkFlowNoNotFoundException(string.Format("工序号【{0}】不存在!", string.Join(",", strFlowNoList2)));
                tcpLogHardware.Error(ex);
                throw ex;
            }
            var hangerReworkRequestQueueItemList = new List<HangerReworkRequestQueueItem>();
            var strFlowNoList = new List<string>();
            foreach (var fd in flowNoList)
            {

                var hq = new HangerReworkRequestQueueItem();
                hq.HangerReworkRequestQueue = hrQueue;
                hq.HangerNo = hrQueue.HangerNo;
                hq.MainTrackNumber = hrQueue.MainTrackNumber;
                hq.StatingNo = hrQueue.StatingNo;
                hq.FlowNo = fd.FlowNo;
                hq.DefectCode = fd.DefectCode;
                //HangerReworkRequestQueueItemDao.Instance.Save(hq);
                hangerReworkRequestQueueItemList.Add(hq);
                strFlowNoList.Add(fd.FlowNo);
            }

            CheckReworkFlow(mainTrackNumber, statingNo, hangerNo, strFlowNoList2);


            //var dicHangerProductFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var fccList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo+"");
            var currentIndex = CANProductsService.Instance.GetCurrentProductingIndex(hangerNo, fccList, mainTrackNumber.ToString(), statingNo.ToString());

            List<HangerProductFlowChartModel> cpHangerProductFlowChartList = null;
            List<HangerProductFlowChartModel> reworkResultHangerProductsFlowChartList = null;
            additionalHangerProductFlowChartCache(hrQueue, mainTrackNumber, statingNo, strFlowNoList, hangerReworkRequestQueueItemList, ref cpHangerProductFlowChartList, ref reworkResultHangerProductsFlowChartList);

            var reworkFlow = reworkResultHangerProductsFlowChartList.Where(ff => ff.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value
            && ff.MergeProcessFlowChartFlowRelationId == null && !ff.StatingNo.Value.Equals(-1) && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).OrderBy(f => f.FlowIndex).First();

            var flowIndex = reworkFlow.FlowIndex.Value;

            var statingList = reworkResultHangerProductsFlowChartList.Where(k => k.FlowIndex.Value == flowIndex && k.StatingNo != null
            && k.StatingNo.Value != -1 && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
            && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)).Select(f => new ProductsProcessOrderModel()
            {
                StatingNo = f.StatingNo.ToString(),
                MainTrackNumber = (int)f.MainTrackNumber,
                StatingCapacity = f.StatingCapacity.Value,
                Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                ProcessChartId = f.ProcessChartId,
                FlowNo = f.FlowNo,
                StatingRoleCode = f.StatingRoleCode//,
                //FlowIndex = flowIndex,
                //ReworkFlowNoList = strFlowNoList2

            }).ToList<ProductsProcessOrderModel>();
            // 计算返工站点
            //zhangxiaolin 2018年10月1日 23:09:07
            //nextStatingNo = OutSiteService.Instance.CalcateStatingNo(statingList, ref mainTrackNumberNo);

            if (statingList == null || statingList.Count == 0)
            {
                //下一道没有可以接收衣架的站
                throw new NoFoundStatingException(string.Format("【衣架返工】主轨:{0} 站点:{1} 衣架:{2} 找不到返工站点!", mainTrackNumber, statingNo, hangerNo));
            }

            OutSiteService.Instance.AllocationNextProcessFlowStating(statingList, ref outMainTrackNumber, ref nextStatingNo);
            if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
            {
                var ex = new FlowNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ", mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo));
                tcpLogError.Error(ex);
                throw ex;
            }
            //【衣架生产履历】返工分配履历Cache写入
            var sourceRewokFlowChart = cpHangerProductFlowChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo.Value == statingNo
            && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
            //需要桥接
            if (mainTrackNumber != outMainTrackNumber)
            {
                string bridgeType = string.Empty;
                SuspeSys.Domain.BridgeSet bridge = null;
                //var nStatingNo = int.Parse(nextStatingNo);
                bool isBridge = CANProductsService.Instance.IsBridgeByOutSiteRequestAction(mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo, hangerNo, ref bridgeType, ref bridge);
                if (isBridge)
                {
                    if (bridgeType == BridgeType.Bridge_Stating_Non_Flow_Chart_ALL || bridgeType == BridgeType.Bridge_Stating_One_In_Flow_Chart || bridgeType == BridgeType.Bridge_Stating_IN_Flow_Chart_ALL)
                    {
                        var tenBridgeMaintrackNumber = bridge.AMainTrackNumber.Value.ToString();
                        var tenBridgeStatingNo = bridge.ASiteNo.Value;
                        var aMainTracknumberBridgeStatingIsInFlowChart = false;
                        ReworkBridgeHander(mainTrackNumber, statingNo, nextStatingNo, hangerNo, outMainTrackNumber, out isRequireBridge, out info, strFlowNoList2, hrQueue, currentIndex, cpHangerProductFlowChartList, reworkResultHangerProductsFlowChartList, reworkFlow, flowIndex, statingList, sourceRewokFlowChart, bridge, tenBridgeMaintrackNumber, tenBridgeStatingNo, aMainTracknumberBridgeStatingIsInFlowChart);
                        return;
                    }
                    var exBridgeApp = new ApplicationException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}桥接类型未找到! 下一站主轨:{2} 站点:{3}", mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo));
                    tcpLogError.Error(exBridgeApp);
                    throw exBridgeApp;

                }
                var ex = new ApplicationException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1} 桥接处理异常! 下一站主轨:{2} 站点:{3} ", mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo));
                tcpLogError.Error(ex);
                throw ex;
            }

            ReworkAction(mainTrackNumber, statingNo, nextStatingNo, hangerNo, outMainTrackNumber, strFlowNoList2, currentIndex, cpHangerProductFlowChartList,
                reworkResultHangerProductsFlowChartList, reworkFlow, flowIndex, sourceRewokFlowChart, true);


        }
        /// <summary>
        /// 返工需要桥接处理
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="isRequireBridge"></param>
        /// <param name="info"></param>
        /// <param name="strFlowNoList2"></param>
        /// <param name="hrQueue"></param>
        /// <param name="currentIndex"></param>
        /// <param name="cpHangerProductFlowChartList"></param>
        /// <param name="reworkResultHangerProductsFlowChartList"></param>
        /// <param name="reworkFlow"></param>
        /// <param name="flowIndex"></param>
        /// <param name="statingList"></param>
        /// <param name="sourceRewokFlowChart"></param>
        /// <param name="bridge"></param>
        /// <param name="tenBridgeMaintrackNumber"></param>
        /// <param name="tenBridgeStatingNo"></param>
        /// <param name="aMainTracknumberBridgeStatingIsInFlowChart"></param>
        public void ReworkBridgeHander(int mainTrackNumber, int statingNo, string nextStatingNo, int hangerNo, int outMainTrackNumber, out bool isRequireBridge,
            out string info, List<string> strFlowNoList2, HangerReworkRequestQueue hrQueue, int currentIndex, List<HangerProductFlowChartModel> cpHangerProductFlowChartList,
            List<HangerProductFlowChartModel> reworkResultHangerProductsFlowChartList, HangerProductFlowChartModel reworkFlow, int flowIndex,
            List<ProductsProcessOrderModel> statingList, HangerProductFlowChartModel sourceRewokFlowChart, BridgeSet bridge, string tenBridgeMaintrackNumber,
            short tenBridgeStatingNo, bool aMainTracknumberBridgeStatingIsInFlowChart)
        {
            //处理分配关系
            CANProductsService.Instance.BridgeAllocationRelation(mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo, hangerNo, tenBridgeStatingNo.ToString(), aMainTracknumberBridgeStatingIsInFlowChart);
            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
            //给下一站分配衣架
            CANTcpServer.server.AllocationHangerToNextStating(tenBridgeMaintrackNumber, tenBridgeStatingNo.ToString(), hexHangerNo);

            var susAllocatingMessage = string.Format("【桥接消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenBridgeMaintrackNumber, bridge.ASiteNo.Value);
            info = susAllocatingMessage;
            tcpLogInfo.Info(susAllocatingMessage);
            var dicHangerAllocationListList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + ""); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()];
            var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
            nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
            nextHangerStatingAllocationItem.FlowIndex =-1;
            nextHangerStatingAllocationItem.SiteNo = statingNo.ToString();
            nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
            nextHangerStatingAllocationItem.HangerNo = hangerNo.ToString();
            nextHangerStatingAllocationItem.HangerType = 0;
            nextHangerStatingAllocationItem.IsReworkSourceStating = false;
            nextHangerStatingAllocationItem.IsReturnWorkFlow = false;
            nextHangerStatingAllocationItem.NextSiteNo = tenBridgeStatingNo+"";
            nextHangerStatingAllocationItem.OutMainTrackNumber = int.Parse(tenBridgeMaintrackNumber);
            nextHangerStatingAllocationItem.FlowNo = string.Empty;
            nextHangerStatingAllocationItem.ProcessFlowCode = string.Empty;
            nextHangerStatingAllocationItem.ProcessFlowName = string.Empty;
            nextHangerStatingAllocationItem.ProcessFlowId = string.Empty;
            nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
            nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
            nextHangerStatingAllocationItem.LastFlowIndex = currentIndex;
            nextHangerStatingAllocationItem.IsStatingStorageOutStating = false;

            dicHangerAllocationListList.Add(nextHangerStatingAllocationItem);
            //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()] = dicHangerAllocationListList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", dicHangerAllocationListList);
            //发布 待生产衣架信息
            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            isRequireBridge = true;
            var dicBridgeReowrkNextStatingCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM);
            if (dicBridgeReowrkNextStatingCache.ContainsKey(hangerNo.ToString()))
            {
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM).Remove(hangerNo.ToString());
            }
            //缓存返工的下一站
            var reworkModel = new ReworkModel();
            reworkModel.ReworkFlowNoList = strFlowNoList2;
            reworkModel.NextStatingList = statingList;
            reworkModel.CurrentIndex = currentIndex;
            reworkModel.FlowIndex = flowIndex;
            reworkModel.ReworkFlow = reworkFlow;
            reworkModel.ReworkResultHangerProductsFlowChartList = reworkResultHangerProductsFlowChartList;
            // reworkModel.CpHangerProductFlowChartList = cpHangerProductFlowChartList;
            reworkModel.SourceRewokFlowChart = sourceRewokFlowChart;
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM).Add(hangerNo.ToString(), reworkModel);
            //更新返工衣架工艺图到缓存
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hrQueue.HangerNo] = cpHangerProductFlowChartList;
            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hrQueue.HangerNo,cpHangerProductFlowChartList);
            //清除桥接站出战缓存
            SusCacheBootstarp.Instance.ClearBridgeStatingHangerOutSiteItem(hangerNo.ToString());
            //清除桥接站进站缓存
            SusCacheBootstarp.Instance.ClearBridgeStatingHangerInSiteItem(hangerNo.ToString());

            //站内数处理
            var hpResume = new HangerProductsChartResumeModel()
            {
                HangerNo = hangerNo.ToString(),
                StatingNo = statingNo.ToString(),
                MainTrackNumber = mainTrackNumber,
                HangerProductFlowChart = sourceRewokFlowChart,
                //HangerProductFlowChartList = new List<HangerProductFlowChartModel>() { nextReworkFlowChart },
                ReworkFlowNos = string.Join(",", strFlowNoList2),
                Action = 4
            };
            var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
         //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
            //修正删除的站内数及明细、缓存
            // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo+"");// dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];
            var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocDeleteStating.Action = 6;
            hnssocDeleteStating.HangerNo = hangerNo.ToString();
            hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
            hnssocDeleteStating.StatingNo = current.StatingNo;
            hnssocDeleteStating.FlowNo = current.FlowNo;
            hnssocDeleteStating.FlowIndex = current.FlowIndex;
            //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );
            var reworkChartList = reworkResultHangerProductsFlowChartList.Where(f => f.StatingNo.Value == short.Parse(nextStatingNo) && f.MainTrackNumber.Value == outMainTrackNumber);
            var nextReworkFlowChart = reworkChartList.Count() > 0 ? reworkChartList.First() : null;

            //发布衣架状态
            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
            chpf.HangerNo = hangerNo.ToString();
            chpf.MainTrackNumber = nextReworkFlowChart.MainTrackNumber.Value;
            chpf.StatingNo = int.Parse(nextStatingNo);
            chpf.FlowNo = nextReworkFlowChart.FlowNo;
            chpf.FlowIndex = nextReworkFlowChart.FlowIndex.Value;
            chpf.FlowType = 1;
            chpf.HangerProductFlowChart = sourceRewokFlowChart;
            chpf.HangerProductFlowChartList = new List<HangerProductFlowChartModel>() { nextReworkFlowChart };
            chpf.ReworkFlowNos = string.Join(",", strFlowNoList2);
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

            //站内数及衣架轨迹处理
            var bridgeStatingIsInFlowChart = CANProductsService.Instance.IsBridgeInverseBridge(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", cpHangerProductFlowChartList);
            HangerProductFlowChartModel nonSucessedFlow = null;
            var bridgeStatingFlowSuccess = CANProductsService.Instance.IsBridgeInverseBridgeAndFlowSuccess(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", cpHangerProductFlowChartList,ref nonSucessedFlow);
            if (!bridgeStatingIsInFlowChart)
            {
                var fc = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(nextReworkFlowChart);
                fc.FlowIndex = -1;
                fc.FlowNo = string.Empty;
                fc.FlowName = string.Empty;
                fc.FlowCode = string.Empty;
                fc.CheckInfo = string.Empty;
                fc.CheckResult = string.Empty;
                fc.MainTrackNumber = short.Parse(tenBridgeMaintrackNumber);
                fc.ReworkDate = null;
                fc.ReworkEmployeeName = string.Empty;
                fc.ReworkEmployeeNo = string.Empty;
                fc.FlowType = 0;
                BridgeService.Instance.CorrectNextStatingStatingNumHandler(hangerNo+"", int.Parse(tenBridgeMaintrackNumber), tenBridgeStatingNo + "", fc, 0);
                BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", hangerNo+"", fc, 1);
            }
            //在工艺图且工序已经完成
            if (bridgeStatingIsInFlowChart && bridgeStatingFlowSuccess)
            {
                //针对桥接携带工序出战再进站的需特殊处理

                var fc = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(nextReworkFlowChart);
                fc.FlowIndex = -1;
                fc.FlowNo = string.Empty;
                fc.FlowName = string.Empty;
                fc.FlowCode = string.Empty;
                fc.CheckInfo = string.Empty;
                fc.CheckResult = string.Empty;
                fc.ReworkDate = null;
                fc.ReworkEmployeeName = string.Empty;
                fc.ReworkEmployeeNo = string.Empty;
                fc.MainTrackNumber = short.Parse(tenBridgeMaintrackNumber);
                fc.FlowType = 0;
                BridgeService.Instance.CorrectNextStatingStatingNumHandler(hangerNo+"", int.Parse(tenBridgeMaintrackNumber), tenBridgeStatingNo + "", fc, 13);
                BridgeService.Instance.NextStatingAlloctionResumeCacheHandler(tenBridgeMaintrackNumber + "", tenBridgeStatingNo + "", hangerNo+"", fc, 9);
            }
        }

        /// <summary>
        /// 返工不需要桥接处理
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="strFlowNoList2"></param>
        /// <param name="currentIndex"></param>
        /// <param name="cpHangerProductFlowChartList"></param>
        /// <param name="reworkResultHangerProductsFlowChartList"></param>
        /// <param name="reworkFlow"></param>
        /// <param name="flowIndex"></param>
        /// <param name="sourceRewokFlowChart"></param>
        /// <param name="isUpdateCache"></param>
        public void ReworkAction(int mainTrackNumber, int statingNo, string nextStatingNo, int hangerNo, int outMainTrackNumber, List<string> strFlowNoList2,
            int currentIndex, List<HangerProductFlowChartModel> cpHangerProductFlowChartList, List<HangerProductFlowChartModel> reworkResultHangerProductsFlowChartList,
            HangerProductFlowChartModel reworkFlow, int flowIndex, HangerProductFlowChartModel sourceRewokFlowChart,
            bool isUpdateCache = false)
        {
            lock (locObj)
            {
                var dicHangerAllocationListList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo + ""); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()];
                var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
                nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
                nextHangerStatingAllocationItem.SiteNo = statingNo.ToString();
                nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                nextHangerStatingAllocationItem.HangerNo = hangerNo.ToString();
                nextHangerStatingAllocationItem.HangerType = 1;
                nextHangerStatingAllocationItem.IsReworkSourceStating = true;
                nextHangerStatingAllocationItem.IsReturnWorkFlow = true;
                nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
                nextHangerStatingAllocationItem.OutMainTrackNumber = mainTrackNumber;
                nextHangerStatingAllocationItem.FlowNo = reworkFlow.FlowNo;
                nextHangerStatingAllocationItem.ProcessFlowCode = reworkFlow.FlowCode;
                nextHangerStatingAllocationItem.ProcessFlowName = reworkFlow.FlowName;
                nextHangerStatingAllocationItem.ProcessFlowId = reworkFlow.FlowId;
                nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
                nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
                nextHangerStatingAllocationItem.LastFlowIndex = currentIndex;
                nextHangerStatingAllocationItem.IsStatingStorageOutStating = CANProductsService.Instance.IsStoreStating(reworkFlow);

                dicHangerAllocationListList.Add(nextHangerStatingAllocationItem);
                //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()] = dicHangerAllocationListList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo + "", dicHangerAllocationListList);
                //发布 待生产衣架信息
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                if (isUpdateCache)
                //更新返工衣架工艺图到缓存
                {
                    // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo.ToString()] = cpHangerProductFlowChartList;
                    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo + "", cpHangerProductFlowChartList);
                }

                var nStatingNo = nextStatingNo;
                var reworkChartList = reworkResultHangerProductsFlowChartList.Where(f => f.StatingNo.Value == short.Parse(nStatingNo) && f.MainTrackNumber.Value == outMainTrackNumber);
                var nextReworkFlowChart = reworkChartList.Count() > 0 ? reworkChartList.First() : null;

                ////【衣架生产履历】返工分配履历Cache写入
                //var sourceRewokFlowChart = cpHangerProductFlowChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo.Value == statingNo
                //&& f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
                //var nextStatingHPResume = new HangerProductsChartResumeModel()
                //{
                //    HangerNo = hangerNo.ToString(),
                //    StatingNo = statingNo.ToString(),
                //    MainTrackNumber = mainTrackNumber,
                //    HangerProductFlowChart = sourceRewokFlowChart,
                //    Action = 1,
                //    NextStatingNo = nextStatingNo
                //};
                //var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                //SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);

                //【衣架生产履历】返工当前站履历Cache写入

                var hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo.ToString(),
                    StatingNo = statingNo.ToString(),
                    MainTrackNumber = mainTrackNumber,
                    HangerProductFlowChart = sourceRewokFlowChart,
                    HangerProductFlowChartList = new List<HangerProductFlowChartModel>() { nextReworkFlowChart },
                    ReworkFlowNos = string.Join(",", strFlowNoList2),
                    Action = 4
                };
                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
                var hnssocAll = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocAll.Action = 0;
                hnssocAll.HangerNo = hangerNo.ToString();
                hnssocAll.MainTrackNumber = outMainTrackNumber;
                hnssocAll.StatingNo = int.Parse(nextStatingNo);
                hnssocAll.FlowNo = nextReworkFlowChart.FlowNo;
                hnssocAll.FlowIndex = nextReworkFlowChart.FlowIndex.Value;
                hnssocAll.HangerProductFlowChartModel = nextReworkFlowChart;
                var hnssocAllJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocAll);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocAllJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocAllJson);


                //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");// dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

                //发布衣架状态
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo.ToString();
                chpf.MainTrackNumber = nextReworkFlowChart.MainTrackNumber.Value;
                chpf.StatingNo = int.Parse(nextStatingNo);
                chpf.FlowNo = nextReworkFlowChart.FlowNo;
                chpf.FlowIndex = nextReworkFlowChart.FlowIndex.Value;
                chpf.FlowType = 1;
                chpf.HangerProductFlowChart = sourceRewokFlowChart;
                chpf.HangerProductFlowChartList = new List<HangerProductFlowChartModel>() { nextReworkFlowChart };
                chpf.ReworkFlowNos = string.Join(",", strFlowNoList2);
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);


                //修正删除的站内数及明细、缓存
                var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocDeleteStating.Action = 6;
                hnssocDeleteStating.HangerNo = hangerNo.ToString();
                hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
                hnssocDeleteStating.StatingNo = current.StatingNo;
                hnssocDeleteStating.FlowNo = current.FlowNo;
                hnssocDeleteStating.FlowIndex = current.FlowIndex;
                //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
            }
        }

        /// <summary>
        /// 检查是返工发起站点的工序是否和返工工序一致
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="flowNoList"></param>
        private void CheckReworkFlow(int mainTrackNumber, int statingNo, int hangerNo, List<string> flowNoList)
        {
            //var dicHangerProductFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo+""))//dicHangerProductFlowChartCache.ContainsKey(hangerNo.ToString()))
            {
                var ex = new ReworkHangerNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}未找到返工衣架【{2}】! ", mainTrackNumber, statingNo, hangerNo));
                ClearReworkHangerRequest(mainTrackNumber, statingNo);
                errorLog.Error(ex);
                throw ex;
            }
            var hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo+"");// dicHangerProductFlowChartCache[hangerNo.ToString()];
            var isExistReworkFlowNoInCurrentStatingNo = hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo.Value == statingNo && flowNoList.Contains(f.FlowNo)).Count() > 0;
            if (isExistReworkFlowNoInCurrentStatingNo)
            {
                var ex = new ReworkHangerNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}返工站点的工序和返工工序一致【{2}】! ", mainTrackNumber, statingNo, hangerNo));
                ClearReworkHangerRequest(mainTrackNumber, statingNo);
                errorLog.Error(ex);
                throw ex;
            }

        }

        /// <summary>
        /// 获取返工工序的 衣架工序明细
        /// </summary>
        /// <param name="reworkRequestQueue"></param>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="reworkFlowNoList"></param>
        /// <param name="hangerReworkRequestQueueItemList"></param>
        /// <param name="cpHangerProductsFlowChartList"></param>
        private void additionalHangerProductFlowChartCache(HangerReworkRequestQueue reworkRequestQueue, int mainTrackNumber, int statingNo,
            List<string> reworkFlowNoList, List<HangerReworkRequestQueueItem> hangerReworkRequestQueueItemList,
            ref List<HangerProductFlowChartModel> cpHangerProductsFlowChartList,
            ref List<HangerProductFlowChartModel> reworkResultHangerProductsFlowChartList)
        {
            reworkResultHangerProductsFlowChartList = new List<HangerProductFlowChartModel>();
          //  var dicHangerProductFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(reworkRequestQueue.HangerNo))//dicHangerProductFlowChartCache.ContainsKey(reworkRequestQueue.HangerNo))
            {
                var ex = new ReworkHangerNotFoundException(string.Format("【衣架返工工序及疵点代码】 主轨:{0} 站点:{1}未找到返工衣架! ", mainTrackNumber, reworkRequestQueue));
                ClearReworkHangerRequest(mainTrackNumber, statingNo);
                errorLog.Error(ex);
                throw ex;
            }
            var hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(reworkRequestQueue.HangerNo); //dicHangerProductFlowChartCache[reworkRequestQueue.HangerNo];

            //移除未完成的返工
            hangerProcessFlowChartList.RemoveAll(f => null != f.FlowType && f.FlowType.Value == 1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value);

            cpHangerProductsFlowChartList = new List<HangerProductFlowChartModel>();
            var chpfcArr = new HangerProductFlowChartModel[hangerProcessFlowChartList.Count];
            hangerProcessFlowChartList.CopyTo(chpfcArr);
            cpHangerProductsFlowChartList.AddRange(chpfcArr);

            //返工发起站点员工信息
            var reworkStatingInfo = ProductsQueryServiceImpl.Instance.GetStatingInfo(mainTrackNumber, statingNo);

            var reworkHangerProductsFlowChartList = new List<HangerProductFlowChartModel>();
            var filterFlowNoList = new List<string>();
            var filterMainTrackNumberStatingList = new List<string>();
            //var flowNoUsedList = hangerProductsFlowChartList.Select(f => "" + f.FlowNo).ToList<string>();
            //找发起返工得站点工序
            var sourceFlowChart = hangerProcessFlowChartList.Where(fpfc=> fpfc.MainTrackNumber.Value == mainTrackNumber && fpfc.StatingNo.Value == statingNo)?.First();
            foreach (var fpfc in hangerProcessFlowChartList)
            {
                //修正返工发起站点
                if (fpfc.MainTrackNumber.Value == mainTrackNumber && fpfc.StatingNo.Value == statingNo)
                {
                    fpfc.IsReworkSourceStating = true;
                    //fpfc.ReworkEmployeeNo = reworkStatingInfo?.Code?.Trim();
                    //fpfc.ReworkEmployeeName = reworkStatingInfo?.RealName?.Trim();
                    //fpfc.ReworkDate = DateTime.Now;
                    //fpfc.ReworkStatingNo = (short)statingNo;
                    fpfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                }
                if (reworkFlowNoList.Contains(fpfc.FlowNo))
                {
                    if (!filterFlowNoList.Contains(fpfc.FlowNo?.Trim()))
                    {
                        filterFlowNoList.Add(fpfc.FlowNo?.Trim());
                    }
                }
                if (reworkFlowNoList.Contains(fpfc.FlowNo) && fpfc.Status.Value == HangerProductFlowChartStaus.Successed.Value && fpfc.StatingNo != null && fpfc.StatingNo.Value != -1
                    && !fpfc.StatingRoleCode.Equals(StatingType.StatingStorage.Code))
                {
                    //处理连续返工
                    var mainStatingKey = string.Format("{0}:{1}", fpfc.MainTrackNumber.Value, fpfc.StatingNo.Value);
                    if (filterMainTrackNumberStatingList.Contains(mainStatingKey))
                    {
                        continue;
                    }
                    filterMainTrackNumberStatingList.Add(mainStatingKey);

                    var dCode = hangerReworkRequestQueueItemList.Where(f => f.FlowNo.Equals(fpfc.FlowNo)).First()?.DefectCode;
                    var defectInfo = CANProductsService.Instance.GetDefectInfo(dCode);
                    var mRe = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(fpfc);
                    mRe.Id = null;
                    mRe.DefectCode = dCode;
                    mRe.DefcectName = defectInfo?.DefectName?.Trim();
                    mRe.FlowType = 1;
                    mRe.IsFlowSucess = false;
                    mRe.IsHangerSucess = false;
                    mRe.IsReworkSourceStating = false;
                    mRe.CompareDate = null;
                    mRe.IncomeSiteDate = null;
                    mRe.OutSiteDate = null;
                    mRe.NextStatingNo = null;
                    mRe.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                    mRe.ReworkDate = DateTime.Now;
                    //返工源头信息
                    mRe.ReworkMaintrackNumber = reworkRequestQueue.MainTrackNumber;
                    mRe.ReworkStatingNo = short.Parse(reworkRequestQueue.StatingNo);
                    mRe.ReworkGroupNo = BridgeService.Instance.GetGroupNo(reworkRequestQueue.MainTrackNumber.Value, short.Parse(reworkRequestQueue.StatingNo));
                    mRe.CheckReworkNo = sourceFlowChart?.FlowNo;
                    mRe.CheckReworkCode = sourceFlowChart?.FlowCode;

                    mRe.AllocationedDate = System.DateTime.Now;//很关键，用来寻找连续返工的出战的唯一工序站点

                    //待修正
                    mRe.ReworkEmployeeNo = reworkStatingInfo?.Code?.Trim();
                    mRe.ReworkEmployeeName = reworkStatingInfo?.RealName?.Trim();
                    mRe.IncomeSiteDate = null;
                    mRe.OutSiteDate = null;
                    mRe.CompareDate = null;
                    //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hrQueue.HangerNo].Add(mRe);
                    cpHangerProductsFlowChartList.Add(mRe);
                    reworkHangerProductsFlowChartList.Add(mRe);
                    reworkResultHangerProductsFlowChartList.Add(mRe);
                }
                //合并工序返工
                if (reworkFlowNoList.Contains(fpfc.FlowNo)
                    && fpfc.Status.Value == HangerProductFlowChartStaus.Successed.Value
                    && fpfc.StatingNo != null && fpfc.StatingNo != null && fpfc.MergeFlowNo != null
                     )
                {
                    //处理连续返工
                    var mainStatingKey = string.Format("{0}:{1}", fpfc.MainTrackNumber.Value, fpfc.StatingNo.Value);
                    if (filterMainTrackNumberStatingList.Contains(mainStatingKey))
                    {
                        continue;
                    }
                    filterMainTrackNumberStatingList.Add(mainStatingKey);

                    var dCode = hangerReworkRequestQueueItemList.Where(f => f.FlowNo.Equals(fpfc.FlowNo)).First()?.DefectCode;
                    var defectInfo = CANProductsService.Instance.GetDefectInfo(dCode);

                    HangerProductFlowChartModel mergeFlowSourceFlowChart = null;
                    var meHPChartList = hangerProcessFlowChartList.Where(f =>
                      f.StatingNo != null
                      && f.StatingNo.Value != -1
                      && f.ProcessFlowChartFlowRelationId != null
                      && f.ProcessFlowChartFlowRelationId.Equals(fpfc.MergeProcessFlowChartFlowRelationId)
                      && f.Status.Value == HangerProductFlowChartStaus.Successed.Value
                       && !fpfc.FlowRealyProductStatingRoleCode.Equals(StatingType.StatingStorage.Code));
                    if (meHPChartList.Count() == 0)
                    {

                        var exCheck = new ApplicationException(string.Format("【找不到合并工序的被合并工序】 主轨:{0} 站点:{1} 合并工序号:{2}", mainTrackNumber, statingNo, fpfc.FlowNo?.Trim()));
                        tcpLogError.Error(exCheck);
                        throw exCheck;
                    }
                    if (null == fpfc.FlowRealyProductStatingNo)
                    {
                        var exCheck = new ApplicationException(string.Format("【找不到合并工序的被合并工序】 主轨:{0} 站点:{1} 合并工序号:{2} 合并工序找不到生产完成的站点!", mainTrackNumber, statingNo, fpfc.FlowNo?.Trim()));
                        tcpLogError.Error(exCheck);
                        throw exCheck;
                    }
                    mergeFlowSourceFlowChart = meHPChartList.First();
                    var mRe = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(mergeFlowSourceFlowChart);

                    //修正站点
                    mRe.StatingNo = fpfc.FlowRealyProductStatingNo;
                    mRe.StatingRoleCode = fpfc.FlowRealyProductStatingRoleCode?.Trim();
                    mRe.StatingId = mergeFlowSourceFlowChart.StatingId;
                    mRe.FlowNo = fpfc.FlowNo;
                    mRe.FlowCode = fpfc.FlowCode;
                    mRe.FlowName = fpfc.FlowName;
                    mRe.FlowIndex = fpfc.FlowIndex;
                    mRe.FlowId = fpfc.FlowId;
                    mRe.MergeFlowNo = fpfc.MergeFlowNo;
                    mRe.ProcessFlowChartFlowRelationId = fpfc.ProcessFlowChartFlowRelationId;
                    mRe.StanardHours = fpfc.StanardHours;
                    mRe.StandardPrice = fpfc.StandardPrice;

                    mRe.Id = null;
                    mRe.DefectCode = dCode;
                    mRe.DefcectName = defectInfo?.DefectName?.Trim();
                    mRe.FlowType = 1;
                    mRe.IsFlowSucess = false;
                    mRe.IsHangerSucess = false;
                    mRe.IsReworkSourceStating = false;
                    mRe.CompareDate = null;
                    mRe.IncomeSiteDate = null;
                    mRe.OutSiteDate = null;
                    mRe.NextStatingNo = null;
                    mRe.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                    mRe.ReworkDate = DateTime.Now;
                    mRe.ReworkMaintrackNumber = reworkRequestQueue.MainTrackNumber;
                    mRe.ReworkStatingNo = short.Parse(reworkRequestQueue.StatingNo);
                    mRe.ReworkGroupNo = BridgeService.Instance.GetGroupNo(reworkRequestQueue.MainTrackNumber.Value, short.Parse(reworkRequestQueue.StatingNo));
                    mRe.CheckReworkNo = sourceFlowChart?.FlowNo;
                    mRe.CheckReworkCode = sourceFlowChart?.FlowCode;
                    mRe.AllocationedDate = System.DateTime.Now;//很关键，用来寻找连续返工的出战的唯一工序站点

                    //待修正
                    mRe.ReworkEmployeeNo = reworkStatingInfo?.Code?.Trim();
                    mRe.ReworkEmployeeName = reworkStatingInfo?.RealName?.Trim();
                    mRe.IncomeSiteDate = null;
                    mRe.OutSiteDate = null;
                    mRe.CompareDate = null;
                    //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hrQueue.HangerNo].Add(mRe);
                    cpHangerProductsFlowChartList.Add(mRe);
                    reworkHangerProductsFlowChartList.Add(mRe);
                    reworkResultHangerProductsFlowChartList.Add(mRe);
                }
                //if (reworkFlowNoList.Contains(fpfc.FlowNo) && fpfc.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value && fpfc.StatingNo != null && fpfc.StatingNo.Value != -1)
                //{
                //    var dCode = hangerReworkRequestQueueItemList.Where(f => f.FlowNo.Equals(fpfc.FlowNo)).First()?.DefectCode;
                //    var defectInfo = CANProductsService.Instance.GetDefectInfo(dCode);
                //    fpfc.DefectCode = dCode?.Trim();
                //    fpfc.DefcectName = defectInfo?.DefectName?.Trim();
                //    fpfc.FlowType = 0;
                //    fpfc.IsFlowSucess = false;
                //    fpfc.IsHangerSucess = false;
                //    fpfc.IsReworkSourceStating = false;
                //    fpfc.CompareDate = null;
                //    fpfc.IncomeSiteDate = null;
                //    fpfc.OutSiteDate = null;
                //    fpfc.NextStatingNo = null;
                //    fpfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                //    //fpfc.ReworkDate = DateTime.Now;
                //    //fpfc.ReworkMaintrackNumber = reworkRequestQueue.MainTrackNumber;
                //    //fpfc.ReworkStatingNo = short.Parse(reworkRequestQueue.StatingNo);
                //    //待修正
                //    fpfc.ReworkEmployeeNo = reworkStatingInfo?.Code?.Trim();
                //    fpfc.ReworkEmployeeName = reworkStatingInfo?.RealName?.Trim();
                //    fpfc.IncomeSiteDate = null;
                //    fpfc.OutSiteDate = null;
                //    fpfc.CompareDate = null;
                //    //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hrQueue.HangerNo].Add(mRe);
                //    //cpHangerProductsFlowChartList.Add(mRe);
                //    //reworkHangerProductsFlowChartList.Add(mRe);
                //    // reworkResultHangerProductsFlowChartList.Add(fpfc);
                //    filterFlowNoList.Add(fpfc.FlowNo?.Trim());

                //}
            }

            //var filterFlowNoStatingList = new List<string>();
            ////处理合并工序
            //foreach (var fn in filterFlowNoList)
            //{
            //    var hProcessFlowChartLt = hangerProcessFlowChartList.Where(f => f.FlowNo.Equals(fn));
            //    foreach (var hpc in hProcessFlowChartLt)
            //    {
            //        var flowNoStringNo = string.Format("{0}:{1}", hpc.FlowNo?.Trim(), hpc.StatingNo.Value);
            //        if (!filterFlowNoStatingList.Contains(flowNoStringNo))
            //        {
            //            filterFlowNoStatingList.Add(flowNoStringNo);
            //            var hProcessFlowChartMergeList = hangerProcessFlowChartList.Where(f => f.MergeProcessFlowChartFlowRelationId != null
            //            && null != hpc.MergeProcessFlowChartFlowRelationId
            //            && null != hpc.ProcessFlowChartFlowRelationId
            //       && hpc.ProcessFlowChartFlowRelationId.Equals(f.MergeProcessFlowChartFlowRelationId)).ToList<HangerProductFlowChartModel>();
            //            foreach (var hProChart in hProcessFlowChartMergeList)
            //            {
            //                if (!filterFlowNoList.Contains(hProChart.FlowNo))
            //                {
            //                    var dCode = hangerReworkRequestQueueItemList.Where(f => f.FlowNo.Equals(hpc.FlowNo)).First()?.DefectCode?.Trim();
            //                    var defectInfo = CANProductsService.Instance.GetDefectInfo(dCode);

            //                    var mRe = Utils.Reflection.BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(hProChart);
            //                    mRe.Id = null;
            //                    var hList = hangerReworkRequestQueueItemList.Where(f => f.FlowNo.Equals(hpc.FlowNo));
            //                    mRe.DefectCode = dCode;
            //                    mRe.DefcectName = defectInfo?.DefectName?.Trim();
            //                    mRe.FlowType = 1;
            //                    mRe.IsFlowSucess = false;
            //                    mRe.IsHangerSucess = false;
            //                    mRe.IsReworkSourceStating = false;
            //                    mRe.CompareDate = null;
            //                    mRe.IncomeSiteDate = null;
            //                    mRe.OutSiteDate = null;
            //                    mRe.NextStatingNo = null;
            //                    mRe.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
            //                    mRe.ReworkDate = DateTime.Now;

            //                    mRe.ReworkEmployeeNo = reworkStatingInfo?.Code?.Trim();
            //                    mRe.ReworkEmployeeName = reworkStatingInfo?.RealName?.Trim();
            //                    //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hrQueue.HangerNo].Add(mRe);
            //                    cpHangerProductsFlowChartList.Add(mRe);
            //                }
            //            }
            //        }
            //    }

            //}
        }
        public bool CheckReworkFowCode(string hangerNo, IList<string> flowNoList)
        {
            //var dicFC = new Dictionary<string, object>();
            //dicFC.Add("HangerNo", hangerNo);
            //dicFC.Add("FlowNo", flowNoList);
            //var cSql = string.Format("select COUNT(1) TCount from HangerProductFlowChart where FlowNo in(:FlowNo) and HangerNo=:HangerNo AND IsFlowSucess=1");
            //var tCount = QueryForObjectIn<int>(cSql, dicFC);
           // var dicHangerProductFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProductFlowChartCache.ContainsKey(hangerNo))
            {
                return false;
            }
            var hangerFlowNoList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo).Where(f => null != f.IsFlowSucess && f.IsFlowSucess.Value).Select(f => f.FlowNo);
            foreach (var fn in flowNoList)
            {
                if (!hangerFlowNoList.Contains(fn))
                {
                    return false;
                }
            }

            return true;
            // return tCount > 0;
        }
        public bool CheckReworkDefectCode(IList<string> defectCodeList)
        {
            var dicFC = new Dictionary<string, object>();
            dicFC.Add("DefectCode", defectCodeList);
            var cSql = string.Format("select COUNT(1) TCount from DefectCodeTable where DefectCode in(:DefectCode)");
            var tCount = QueryForObjectIn<int>(cSql, dicFC);
            return tCount > 0;
        }
        public IList<DefectCodeTableModel> GetDefectCodeList()
        {
            var cSql = string.Format("select * from DefectCodeTable where Deleted=0");
            var list = Query<DefectCodeTableModel>(cSql, true, null);
            return list;
        }
        public void ClearReworkHangerRequest(int mainTrackNumber, int statingNo)
        {
            var sql = "DELETE HangerReworkRequestQueue where MainTrackNumber=? and StatingNo=?";
            var sqlItem = "DELETE HangerReworkRequestQueueItem where MainTrackNumber=? and StatingNo=?";
            ExecuteUpdate(sqlItem, mainTrackNumber, statingNo);
            ExecuteUpdate(sql, mainTrackNumber, statingNo);
        }
        public IList<HangerReworkRequestQueue> GetHangerReworkQueueList(int mainTrackNumber, int statingNo, int hangerNo)
        {
            var dicFC = new Dictionary<string, string>();
            dicFC.Add("MainTrackNumber", mainTrackNumber.ToString());
            dicFC.Add("StatingNo", statingNo.ToString());
            dicFC.Add("HangerNo", hangerNo.ToString());
            var sql = string.Format("select * from HangerReworkRequestQueue where MainTrackNumber=:MainTrackNumber and StatingNo=:StatingNo and HangerNo=:HangerNo");
            return Query<HangerReworkRequestQueue>(sql, true, dicFC);
        }
        public IList<HangerReworkRequestQueueItem> GetHangerReworkRequestQueueItemList(string hqId)
        {
            var dicFC = new Dictionary<string, string>();
            dicFC.Add("hqId", hqId);
            var sql = string.Format("select * from HangerReworkRequestQueueItem where HANGERREWORKREQUESTQUEUE_Id=:hqId");
            return Query<HangerReworkRequestQueueItem>(sql, true, dicFC);
        }
    }
    class RecordReworkModel
    {
        protected static readonly ILog log = LogManager.GetLogger("LogLogger");
        protected static readonly ILog errorLog = LogManager.GetLogger("Error");
        public int StatingNo { set; get; }
        public int HangerNo { set; get; }
        public int MainTrackNumber { set; get; }

        //转移返工记录
        public void ReworkSucessTransferData()
        {
            var reqList = SusReworkService.Instance.GetHangerReworkQueueList(MainTrackNumber, StatingNo, HangerNo);
            foreach (var req in reqList)
            {
                var hangerReworkRequest = BeanUitls<HangerReworkRequest, HangerReworkRequestQueue>.Mapper(req);
                HangerReworkRequestDao.Instance.Save(hangerReworkRequest);
                var hangerReworkRequestItemList = SusReworkService.Instance.GetHangerReworkRequestQueueItemList(req.Id);
                foreach (var reqItem in hangerReworkRequestItemList)
                {
                    var hangerReworkRequestItem = BeanUitls<HangerReworkRequestItem, HangerReworkRequestQueueItem>.Mapper(reqItem);
                    HangerReworkRequestItemDao.Instance.Save(hangerReworkRequestItem);
                }
            }
            SusReworkService.Instance.ClearReworkHangerRequest(MainTrackNumber, StatingNo);
        }
    }
}

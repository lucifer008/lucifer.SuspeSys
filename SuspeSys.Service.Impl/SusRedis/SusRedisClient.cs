using log4net;
using Newtonsoft.Json;
using NHibernate.Util;
using StackExchange.Redis;
using StackExchange.Redis.DataTypes;
using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.Service.Impl.ProductionLineSet;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusRedis
{
    /// <summary>
    /// Redis
    /// </summary>
    public class SusRedisClient : ServiceBase
    {
        public readonly static SusRedisClient Instance = new SusRedisClient();
        new protected static readonly ILog log = LogManager.GetLogger("RedisLogInfo");
        public static RedisTypeFactory RedisTypeFactory = null;
        //public static RedisTypeFactory BaseRedisTypeFactory = null;
        public static ConnectionMultiplexer ConnectionMultiplexer = null;
        public static ISubscriber subcriber = null;
        public static readonly string url = System.Configuration.ConfigurationManager.AppSettings["RedisIp"]; //"localhost";
        public static readonly int portNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RedisPort"]);
        public static SusRedisClient Instance2
        {
            get { return new SusRedisClient(); }
        }
        private SusRedisClient() { }
        public void Init(bool isServiceStart = false)
        {
            try
            {
                if (null == RedisTypeFactory)
                {
                    var connConfig = new ConfigurationOptions()
                    {
                        ConnectTimeout = 120000,
                        EndPoints =
            {
                {url,portNumber }
            },
                        AbortOnConnectFail = false // this prevents that error
                    };
                    connConfig.ConnectTimeout = 120000;
                    ConnectionMultiplexer = ConnectionMultiplexer.Connect(connConfig);//.Connect("localhost,abortConnect=false"); // replace localhost with your redis db address
                    var statusInfo = string.Format("Redis开启:{0}", ConnectionMultiplexer.IsConnected ? "成功" : "失败");
                    log.Info(statusInfo);
                    RedisTypeFactory = new RedisTypeFactory(ConnectionMultiplexer);
                    //  BaseRedisTypeFactory = new RedisTypeFactory(ConnectionMultiplexer);
                    //var redisDictionary = RedisTypeFactory.GetDictionary<int, HangerProductFlowChart>("HangerProductFlowChart");
                    ////redisDictionary.Add(1, new HangerProductFlowChart() { HangerNo = "123" });
                    ////redisDictionary.Add(2, new HangerProductFlowChart() { HangerNo = "124" });
                    ////redisDictionary.Add(3, new HangerProductFlowChart() { HangerNo = "125" });
                    //foreach (var hpfc in redisDictionary)
                    //{
                    //    Console.WriteLine("HangerNo: {0}", hpfc.Value.HangerNo);
                    //}

                   // //订阅消息:消息对所有订阅的客户端都有效
                   // subcriber = ConnectionMultiplexer.GetSubscriber();
                   // //subcriber.Subscribe(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, (channel, message) => {
                   // //    Console.WriteLine((string)message);
                   // //});
                   // subcriber.Subscribe(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, WaitProcessOrderHangerAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_PROCESS_FLOW_CHART, HangerProcessFlowChartAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_ALLOCATION_ITME_ACTION, HangerAllocationAction);
                   // subcriber.Subscribe(SusRedisConst.UPDATE_HANGER_PROCESS_FLOW_CHART_ACTION, UpdateHangerProcessFlowChartAction);
                   // subcriber.Subscribe(SusRedisConst.RECORD_STATING_HANGER_PROCESS_ITEM_ACTION, RecordStatingHangerProductItemAction);
                   // subcriber.Subscribe(SusRedisConst.UPDATE_WAIT_PROCESS_ORDER_HANGER_ACTION, UpdateWaitProcessOrderHangerAction);

                   // subcriber.Subscribe(SusRedisConst.UPDATE_HANGER_PRODUCT_ITEM_ACTION, UpdateHangerProductItemAction);

                   // subcriber.Subscribe(SusRedisConst.HANGER_IN_SITE_ACTION, HangerInSiteAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_OUT_SITE_ACTION, HangerOutSiteAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_OUT_HANGING_STATION_ACTION, HangingStationOutSiteAction);
                   // subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_STATUS, MainTrackStatingStatusAction);
                   // subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, UpdateMainTrackStatingAllocationNumAction);
                   //// subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_IN_NUM, UpdateMainTrackStatingInNumAction);
                   // subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM, UpdateMainTrackStatingOnlineNumAction);
                   // subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION, MainTrackStatingMontorUploadAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_REWORK_REQUEST_QUEUE_ACTION, HangerReworkReuestQueueAction);
                   // subcriber.Subscribe(SusRedisConst.STATING_EDIT_ACTION, StatingCapacityEdit);
                   // subcriber.Subscribe(SusRedisConst.STATING_EMPLOYEE_ACTION, StatingEmployeeAction);
                   // subcriber.Subscribe(SusRedisConst.EMPLOYEE_CARD_RELATION_ACTION, EmployeeCardRelationAction);
                   // subcriber.Subscribe(SusRedisConst.PRODUCT_SUCCESS_COPY_DATA_ACTION, ProductSuccessCopyDataAction);
                   // subcriber.Subscribe(SusRedisConst.STATING_ALLOCATION_SAVE_CHANGE_ACTION, ProcessStatingAllocationLog);
                   // subcriber.Subscribe(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, HangerProductsChartResumeAction);
                   // subcriber.Subscribe(SusRedisConst.PUBLIC_TEST, PublicTestAction);
                   // subcriber.Subscribe(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, CurrentHangerProductinFlowAction);
                   // subcriber.Subscribe(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, HangerStatingOrAllocationAction);
                    //              public const string HANGER_IN_SITE_ACTION = "HANGER_IN_SITE_ACTION";
                    //public const string HANGER_OUT_SITE_ACTION = "HANGER_OUT_SITE_ACTION";
                    #region 注册事件
                    ConnectionMultiplexer.ConnectionFailed += MuxerConnectionFailed;
                    ConnectionMultiplexer.ConnectionRestored += MuxerConnectionRestored;
                    ConnectionMultiplexer.ErrorMessage += MuxerErrorMessage;
                    ConnectionMultiplexer.ConfigurationChanged += MuxerConfigurationChanged;
                    ConnectionMultiplexer.HashSlotMoved += MuxerHashSlotMoved;
                    ConnectionMultiplexer.InternalError += MuxerInternalError;
                    #endregion

                    log.Info("redis start sucess!");
                }
            }
            catch (Exception ex)
            {
                log.Error("redis", ex);
            }
        }

        /// <summary>
        /// 非常规站内数及站内数明细，硬件缓存修正
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void HangerStatingOrAllocationAction(RedisChannel arg1, RedisValue arg2)
        {

            var hNonStandModel = JsonConvert.DeserializeObject<SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel>(arg2);
            var hangerNo = hNonStandModel.HangerNo;
            var mainTrackNumber = hNonStandModel.MainTrackNumber;
            var statingNo = hNonStandModel.StatingNo;

            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  开始", hangerNo, mainTrackNumber, statingNo);
            try
            {
                //衣架分配
                if (hNonStandModel.Action == 0)
                {
                    #region 衣架分配
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        hNonStandModel.HangerProductFlowChartModel.HangerStatus = 0;
                        hNonStandModel.HangerProductFlowChartModel.MainTrackNumber = (short)hNonStandModel.MainTrackNumber;
                        hNonStandModel.HangerProductFlowChartModel.StatingNo = (short)hNonStandModel.StatingNo;
                        dicHangerStatingInfo.Add(hangerNo, new List<HangerProductFlowChartModel>() { hNonStandModel.HangerProductFlowChartModel });
                        //return;
                        //站点分配数+1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                        // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    }
                    else
                    {
                        var chList = dicHangerStatingInfo[hangerNo];
                        var isExist = chList.Where(f => f.FlowNo.Equals(hNonStandModel.FlowNo) && f.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && f.StatingNo.Value == hNonStandModel.StatingNo).Count() > 0;
                        if (isExist)
                        {
                            chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                            {
                                if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                                && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                                )
                                {
                                    hpfc.HangerStatus = 0;
                                }
                            });
                        }
                        else
                        {
                            hNonStandModel.HangerProductFlowChartModel.MainTrackNumber = (short)hNonStandModel.MainTrackNumber;
                            hNonStandModel.HangerProductFlowChartModel.StatingNo = (short)hNonStandModel.StatingNo;
                            chList.Add(hNonStandModel.HangerProductFlowChartModel);
                        }
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        //站点分配数+1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                        // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    }
                    //dicHangerStatingInfo[hangerNo].Add(hangerNonStandardStatingOrCacheModel.HangerProductFlowChartModel);
                    //if (!hNonStandModel.IsHanging) {
                    //    //出站站内数-1
                    //    var inNumModel = new MainTrackStatingCacheModel()
                    //    {
                    //        MainTrackNumber = hNonStandModel.MainTrackNumber,
                    //        StatingNo = hNonStandModel.StatingNo,
                    //        OnLineSum = -1
                    //    };
                    //    SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    //}
                    ////清除硬件缓存
                    //var clearHangerNoCache = string.Format("【站内数及明细修正】 正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", hangerNonStandardStatingOrCacheModel.MainTrackNumber,
                    //    hangerNonStandardStatingOrCacheModel.StatingNo, hangerNonStandardStatingOrCacheModel.HangerNo);
                    //CANTcp.client.ClearHangerCache(hangerNonStandardStatingOrCacheModel.MainTrackNumber, hangerNonStandardStatingOrCacheModel.StatingNo,
                    //    int.Parse(hangerNonStandardStatingOrCacheModel.HangerNo), SuspeConstants.XOR);
                    //tcpLogInfo.Info(clearHangerNoCache);
                    ////清除站内数及明细

                    //var logMessage = string.Format("【站内数及明细修正】衣架号:{0} 主轨:{1} 清除【站点:{2}】站内数修正!", hangerNonStandardStatingOrCacheModel.HangerNo,
                    //    hangerNonStandardStatingOrCacheModel.MainTrackNumber, hangerNonStandardStatingOrCacheModel.StatingNo);
                    //tcpLogInfo.Info(logMessage);
                    ////                        tcpLogInfo.Info(logMessage);
                    ////清除已分配衣架站内明细
                    //List<SuspeSys.Domain.HangerStatingAllocationItem> allocationedHangerList = null;
                    //var dicHangerStatingALloListCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                    //if (dicHangerStatingALloListCache.ContainsKey(hangerNonStandardStatingOrCacheModel.HangerNo))
                    //{
                    //    allocationedHangerList = dicHangerStatingALloListCache[hangerNonStandardStatingOrCacheModel.HangerNo];
                    //    allocationedHangerList.RemoveAll(f =>
                    //  f.MainTrackNumber.Value == hangerNonStandardStatingOrCacheModel.MainTrackNumber &&
                    //  f.IncomeSiteDate == null &&
                    //  null != f.NextSiteNo &&
                    //  short.Parse(f.NextSiteNo) == hangerNonStandardStatingOrCacheModel.StatingNo &&
                    //  (!"-1".Equals(f.Memo)) &&
                    //  f.AllocatingStatingDate != null
                    //  );
                    //}
                    #endregion
                }
                //衣架进站
                else if (hNonStandModel.Action == 1)
                {
                    #region 衣架进站
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配进站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    {
                        if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                        )
                        {
                            hpfc.HangerStatus = 1;
                        }
                    });
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    //站点分配数-1
                    var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                    //  SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                    //站内数+1
                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    #endregion
                }
                //衣架出战
                else if (hNonStandModel.Action == 2)
                {
                    #region 衣架出战
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    {
                        if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                        )
                        {
                            hpfc.HangerStatus = 2;
                        }
                    });
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    ////站内数-1
                    //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    #endregion
                }
                //站点删除
                else if (hNonStandModel.Action == 3)
                {
                    #region 站点删除
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.Remove(chList.Where(p => p.FlowNo.Equals(hNonStandModel.FlowNo) && p.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && p.StatingNo.Value == hNonStandModel.StatingNo).FirstOrDefault());
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                    #endregion
                }
                //工序删除
                else if (hNonStandModel.Action == 4)
                {
                    #region 工序删除
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.Where(data => data.FlowNo.Equals(hNonStandModel.FlowNo)).ForEach(delegate (HangerProductFlowChartModel hpc)
                    {
                        //站内数-1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpc.MainTrackNumber.Value, StatingNo = hpc.StatingNo.Value, OnLineSum = -1 };
                        // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    });
                    chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    #endregion
                }
                //衣架返工
                else if (hNonStandModel.Action == 6)
                {
                    #region 衣架返工
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    {
                        if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                        )
                        {
                            hpfc.HangerStatus = 4;
                        }
                    });
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    //站内数-1
                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //    SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    #endregion
                }
                //过监测点已分配或者已进站修正
                else if (hNonStandModel.Action == 7)
                {
                    #region 过监测点已分配或者已进站修正
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    {
                        if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.HangerStatus == 0)
                        {
                            hpfc.HangerStatus = 5;
                            var logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存开始【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                            montorLog.Info(logMessage);
                            if(null!= CANTcp.client)
                                CANTcp.client.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                            if (null != CANTcpServer.server)
                                CANTcpServer.server.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);

                            logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存结束【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                            montorLog.Info(logMessage);

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, AllocationNum = -1 };
                            //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        else if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.HangerStatus == 1)
                        {
                            hpfc.HangerStatus = 6;
                            var logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存开始【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                            montorLog.Info(logMessage);
                            if (null != CANTcp.client)
                                CANTcp.client.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                            if (null != CANTcpServer.server)
                                CANTcpServer.server.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                            logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存结束【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                            montorLog.Info(logMessage);

                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, OnLineSum = -1 };
                            // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                    });
                    chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo) && (data.HangerStatus == 5 || data.HangerStatus == 6));
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                    #endregion
                }
                //工序移动及站点移动
                else if (hNonStandModel.Action == 8)
                {
                    #region 站点删除
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【工序移动及站点移动】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.Where(data => data.FlowNo.Equals(hNonStandModel.FlowNo)).ForEach(delegate (HangerProductFlowChartModel hpc)
                    {
                        //站内数-1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpc.MainTrackNumber.Value, StatingNo = hpc.StatingNo.Value, OnLineSum = -1 };
                        //  SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    });
                    chList.Remove(chList.Where(p => p.FlowNo.Equals(hNonStandModel.FlowNo) && p.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && p.StatingNo.Value == hNonStandModel.StatingNo).FirstOrDefault());
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                    #endregion
                }
                else if (hNonStandModel.Action == 9)//桥接站出战逆向桥接站内数修正(逆向站点无携带工序)
                {
                    //站内数-1
                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                }
                else if (hNonStandModel.Action == 10)//桥接出战逆向桥接站内数修正(逆向站点携带工序)
                {
                    var dicHangerStatingInfo = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    {
                        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                        return;
                    }
                    //站内数-1
                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //  SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                    var chList = dicHangerStatingInfo[hangerNo];
                    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    {
                        if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                        )
                        {
                            hpfc.HangerStatus = 1;
                        }
                    });
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                }
            }
            catch (Exception ex)
            {
                tcpLogError.ErrorFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  错误:{3}", hangerNo, mainTrackNumber, statingNo, ex);
            }
            finally
            {
                tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  结束", hangerNo, mainTrackNumber, statingNo);
            }
        }

        /// <summary>
        /// 维护衣架工序状态
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        //private void CurrentHangerProductinFlowAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    tcpLogInfo.InfoFormat("【维护衣架状态】 开始【{0}】", arg2);
        //    var currentHangerProductingFlowModel = JsonConvert.DeserializeObject<SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(arg2);
        //    var dicCurrentHangerProductingFlowModelCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
        //    if (!dicCurrentHangerProductingFlowModelCache.ContainsKey(currentHangerProductingFlowModel.HangerNo))
        //    {
        //        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW).Add(currentHangerProductingFlowModel.HangerNo, currentHangerProductingFlowModel);
        //        tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", arg2);
        //        return;
        //    }

        //    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[currentHangerProductingFlowModel.HangerNo] = currentHangerProductingFlowModel;
        //    tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", arg2);
        //}

        private void PublicTestAction(RedisChannel arg1, RedisValue arg2)
        {
            redisLog.InfoFormat("Server-->PublicTestAction-->{0}", arg2.ToString());
        }

        #region//衣架生产轨迹记录
        ProductsQueryServiceImpl pQueryService = new ProductsQueryServiceImpl();
        private void HangerProductsChartResumeAction(RedisChannel arg1, RedisValue arg2)
        {
            if (0 == arg2)
            {
                return;
            }
            var hangerProductsChartResumeModel = JsonConvert.DeserializeObject<HangerProductsChartResumeModel>(arg2);
            var hpFlowChart = hangerProductsChartResumeModel.HangerProductFlowChart;
            try
            {
                tcpLogInfo.InfoFormat("【衣架生产轨迹记录】 开始");
                var hangerNo = hangerProductsChartResumeModel.HangerNo?.Trim();
                var dicHangerProductsChartResumeCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME);
                if (!dicHangerProductsChartResumeCache.ContainsKey(hangerProductsChartResumeModel.HangerNo) && null != hpFlowChart)
                {
                    var hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);
                    var hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                    hpFlowChart.InsertDateTime = DateTime.Now;
                    hpFlowChart.UpdateDateTime = DateTime.Now;
                    hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                    hpFlowChart.CardNo = hEmStatingInfo?.CardNo;
                    var hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                    SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);
                    return;
                }

                var hProductsChartResumeList = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo];
                var action = hangerProductsChartResumeModel.Action;
                switch (action)
                {
                    case 0://挂片
                        var hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);

                        var hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;

                        if (dicHangerProductsChartResumeCache.ContainsKey(hangerProductsChartResumeModel.HangerNo))
                        {
                            SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Remove(hangerNo);
                        }
                        hpFlowChart.InsertDateTime = DateTime.Now;
                        hpFlowChart.UpdateDateTime = DateTime.Now;
                        hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                        hpFlowChart.CardNo = hEmStatingInfo?.CardNo;
                        var hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);
                        break;
                    case -1://重复挂片
                        hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);

                        hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                        hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                        hpFlowChart.CardNo = hEmStatingInfo?.CardNo;
                        hpFlowChart.InsertDateTime = DateTime.Now;
                        hpFlowChart.UpdateDateTime = DateTime.Now;
                        hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Remove(hangerNo);
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);
                        break;
                    case 1://分配
                        if (null == hpFlowChart)
                        {
                            tcpLogInfo.InfoFormat("【衣架生产轨迹记录】 衣架生产完成.");
                            return;
                        }
                        //清除同站，同工序已分配的衣架
                        hProductsChartResumeList.RemoveAll(f => f.StatingNo != null
                       && -1 != f.StatingNo.Value
                       && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                       && f.StatingNo.Value == int.Parse(hangerProductsChartResumeModel.NextStatingNo)
                       && f.FlowNo.Equals(hpFlowChart.FlowNo));

                        hpFlowChart.InsertDateTime = DateTime.Now;
                        hpFlowChart.UpdateDateTime = DateTime.Now;
                        hpFlowChart.isAllocationed = true;
                        hpFlowChart.AllocationedDate = DateTime.Now;
                        hpFlowChart.StatingNo = short.Parse(hangerProductsChartResumeModel.NextStatingNo);
                        hProductsChartResumeList.Add(hpFlowChart);
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 2://进站
                        hProductsChartResumeList.Where(f => f.StatingNo != null
                        && -1 != f.StatingNo.Value
                        && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                        && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                        && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            hpfc.Status = HangerProductFlowChartStaus.Producting.Value;
                            hpfc.IncomeSiteDate = DateTime.Now;
                            hpfc.UpdateDateTime = DateTime.Now;
                        }
                        );
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 3://出战

                        //pQueryService.CheckStatingIsLogin(StatingNo);
                        var statingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(),hpFlowChart.MainTrackNumber.Value);

                        var emStatingInfo = statingEmList.Count > 0 ? statingEmList[0] : null;

                        var oDate = DateTime.Now;
                        var uDate = DateTime.Now;
                        //正常流程
                        var hreList = hProductsChartResumeList.Where(f => f.StatingNo != null
                       && -1 != f.StatingNo.Value
                       && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                       && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                       && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList();
                        if (hreList.Count() > 0)
                        {

                            hreList.Where(f => f.StatingNo != null
                          && -1 != f.StatingNo.Value
                          && f.Status.Value == HangerProductFlowChartStaus.Producting.Value
                          && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                          && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                          {
                              //出站站内数-1
                              var inNumModel = new MainTrackStatingCacheModel()
                              {
                                  MainTrackNumber = hpfc.MainTrackNumber.Value,
                                  StatingNo = hpfc.StatingNo.Value,
                                  OnLineSum = -1
                              };
                              //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                              NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                          }
                          );

                            hreList.Where(f => f.StatingNo != null
                        && -1 != f.StatingNo.Value
                        && f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value
                        && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                        && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            //分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, AllocationNum = -1 };
                            //   SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        );

                            hProductsChartResumeList.Where(f => f.StatingNo != null
                           && -1 != f.StatingNo.Value
                           && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                           && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                           && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                           {
                               hpfc.OutSiteDate = oDate;
                               hpfc.CompareDate = hpFlowChart.CompareDate;
                               hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
                               hpfc.UpdateDateTime = uDate;
                               hpfc.IsFlowSucess = true;
                               hpfc.EmployeeName = emStatingInfo?.RealName;
                               hpfc.CardNo = emStatingInfo?.CardNo;
                           }
                           );

                        }
                        //同工序不同站出战:1.未分配出战；2.删除出战
                        else
                        {
                            HangerProductFlowChartModel hpfc = BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(hpFlowChart);
                            hpfc.OutSiteDate = oDate;
                            hpfc.CompareDate = hpFlowChart.CompareDate;
                            hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
                            hpfc.UpdateDateTime = uDate;
                            hpfc.IsFlowSucess = true;
                            hpfc.EmployeeName = emStatingInfo?.RealName;
                            hpfc.CardNo = emStatingInfo?.CardNo;
                            hpfc.StatingNo = Convert.ToInt16(hangerProductsChartResumeModel.StatingNo);
                            hProductsChartResumeList.Add(hpfc);


                            var reList = hProductsChartResumeList.Where(f => f.StatingNo != null
                            && -1 != f.StatingNo.Value
                            && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                            && f.StatingNo.Value != hpFlowChart.StatingNo.Value
                            && f.FlowNo.Equals(hpFlowChart.FlowNo)).ToList();
                            foreach (var t in reList)
                            {
                                //出站站内数-1
                                if (t.Status.Value == HangerProductFlowChartStaus.Producting.Value)
                                {
                                    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = t.MainTrackNumber.Value, StatingNo = t.StatingNo.Value, OnLineSum = -1 };
                                    // SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                                    NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                                }
                                //以分配未进站别的站点出战，分配数-1
                                if (t.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value)
                                {
                                    //站点分配数+1
                                    var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = t.MainTrackNumber.Value, StatingNo = t.StatingNo.Value, AllocationNum = -1 };
                                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                                    NewSusRedisClient.Instance.UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                                }
                                //清除硬件缓存
                                var clearHangerNoCache = string.Format("【同工序不同站出站】 正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", t.MainTrackNumber.Value, t.StatingNo.Value, hangerNo);
                                if (null != CANTcp.client)
                                    CANTcp.client.ClearHangerCache(t.MainTrackNumber.Value, t.StatingNo.Value, int.Parse(hangerNo), SuspeConstants.XOR);
                                if (null != CANTcpServer.server)
                                    CANTcpServer.server.ClearHangerCache(t.MainTrackNumber.Value, t.StatingNo.Value, int.Parse(hangerNo), SuspeConstants.XOR);

                                tcpLogInfo.Info(clearHangerNoCache);

                                //修正删除的站内数及明细、缓存
                                var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                                hnssocDeleteStating.Action = 3;
                                hnssocDeleteStating.HangerNo = hangerNo;
                                hnssocDeleteStating.MainTrackNumber = t.MainTrackNumber.Value;
                                hnssocDeleteStating.StatingNo = t.StatingNo.Value;
                                hnssocDeleteStating.FlowNo = t.FlowNo;
                                hnssocDeleteStating.FlowIndex = t.FlowIndex.Value;
                                //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                                var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                                // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(),hnssocDeleteStatingJson );
                            }
                            //foreach (var t in reList)
                            //{
                            //    //清除硬件缓存
                            //    var clearHangerNoCache = string.Format("【同工序不同站出站】 正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", t.MainTrackNumber.Value, t.StatingNo.Value, hangerNo);
                            //    CANTcp.client.ClearHangerCache(t.MainTrackNumber.Value, t.StatingNo.Value, int.Parse(hangerNo), SuspeConstants.XOR);
                            //    tcpLogInfo.Info(clearHangerNoCache);
                            //    //清除站内数及明细
                            //    //出站站内数-1
                            //    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = t.MainTrackNumber.Value, StatingNo = t.StatingNo.Value, OnLineSum = -1 };
                            //    SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            //    var logMessage = string.Format("【同工序不同站出站】衣架号:{0} 主轨:{1} 清除【站点:{2}】站内数修正!", hangerNo, t.MainTrackNumber.Value, t.StatingNo.Value);
                            //    tcpLogInfo.Info(logMessage);
                            //    //                        tcpLogInfo.Info(logMessage);
                            //    //清除已分配衣架站内明细
                            //    List<SuspeSys.Domain.HangerStatingAllocationItem> allocationedHangerList = null;
                            //    var dicHangerStatingALloListCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                            //    if (dicHangerStatingALloListCache.ContainsKey(hangerNo))
                            //    {
                            //        allocationedHangerList = dicHangerStatingALloListCache[hpFlowChart.HangerNo.ToString()];
                            //        allocationedHangerList.RemoveAll(f =>
                            //      f.MainTrackNumber.Value == t.MainTrackNumber.Value &&
                            //      f.IncomeSiteDate == null &&
                            //      null != f.NextSiteNo &&
                            //      short.Parse(f.NextSiteNo) == t.StatingNo.Value &&
                            //      (!"-1".Equals(f.Memo)) &&
                            //      f.AllocatingStatingDate != null
                            //      );

                            //        SusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = allocationedHangerList;
                            //        logMessage = string.Format("【同工序不同站出站】衣架号:{0} 主轨:{1} 清除【站点:{2}】站内数明细修正!", hangerNo, t.MainTrackNumber.Value, t.StatingNo.Value);
                            //        tcpLogInfo.Info(logMessage);
                            //    }
                            //    var rlogMessage = string.Format("【清除衣架轨迹日志】衣架号:{0} 主轨:{1} 清除【站点:{2}】衣架轨迹修正!", hangerNo, t.MainTrackNumber.Value, t.StatingNo.Value);
                            //    tcpLogInfo.Info(rlogMessage);
                            //}
                            //清除衣架轨迹日志
                            hProductsChartResumeList.RemoveAll(f => f.StatingNo != null
                        && -1 != f.StatingNo.Value
                        && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                        && f.StatingNo.Value != int.Parse(hangerProductsChartResumeModel.StatingNo)
                        && f.FlowNo.Equals(hpFlowChart.FlowNo));

                        }

                        //处理合并工序
                        var hangerProcessFlowChartList = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo];
                        var meregeProcessFlowChartList = hangerProcessFlowChartList.Where(f =>
           (null != f.IsMergeForward && f.IsMergeForward.Value)
           && (hpFlowChart.ProcessFlowChartFlowRelationId.Equals(f.MergeProcessFlowChartFlowRelationId))
           && hpFlowChart.FlowType.Value == f.FlowType.Value
           && hpFlowChart.StatingNo.Value != f.StatingNo.Value

           //&& f.Status.Value != HangerProductFlowChartStaus.Successed.Value
           );
                        meregeProcessFlowChartList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            hpfc.StatingNo = hpFlowChart.StatingNo;
                            hpfc.OutSiteDate = oDate;
                            hpfc.CompareDate = hpFlowChart.CompareDate;
                            hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
                            hpfc.UpdateDateTime = uDate;
                            hpfc.IsFlowSucess = true;
                            hpfc.EmployeeName = emStatingInfo?.RealName;
                            hpfc.CardNo = emStatingInfo?.CardNo;
                            hProductsChartResumeList.Add(hpfc);
                        });
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 4://返工
                        //移除发起站点
                        hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hangerProductsChartResumeModel.StatingNo?.Trim(), hangerProductsChartResumeModel.MainTrackNumber);

                        hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                        hProductsChartResumeList.Where(f =>
                              f.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo)
                              && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                              && f.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber).ForEach(delegate (HangerProductFlowChartModel hpf)
                              {
                                  hpf.CompareDate = DateTime.Now;
                                  hpf.CheckInfo = string.Format("工序{0} 返工", hangerProductsChartResumeModel.ReworkFlowNos);
                                  hpf.EmployeeName = hEmStatingInfo?.RealName;
                                  hpf.CardNo = hEmStatingInfo?.CardNo;
                                  hpf.IsReworkSourceStating = true;
                                  hpf.UpdateDateTime = DateTime.Now;
                                  hpf.Status = HangerProductFlowChartStaus.Successed.Value;
                              });
                        //sourceHangerProductFlowCHart.IsReworkSourceStating = true;
                        //sourceHangerProductFlowCHart.CompareDate = DateTime.Now;
                        //sourceHangerProductFlowCHart.CheckResult = "返工";

                        if (hangerProductsChartResumeModel.HangerProductFlowChartList != null)
                        {
                            hangerProductsChartResumeModel.HangerProductFlowChartList.ForEach(
                                delegate (HangerProductFlowChartModel hpfc)
                                {

                                    hpfc.InsertDateTime = DateTime.Now;
                                    hpfc.FlowType = 1;
                                    hpfc.UpdateDateTime = DateTime.Now;
                                    hpfc.AllocationedDate = DateTime.Now;
                                    // hpfc.ReworkEmployeeNo var sourceHangerProductFlowCHart =
                                    hProductsChartResumeList.Add(hpfc);
                                }
                                );
                        }

                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 5://站点删除
                        hProductsChartResumeList.RemoveAll(data => data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                        && data.StatingNo.Value == int.Parse(hangerProductsChartResumeModel.StatingNo)
                        && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 6://工序删除
                        hProductsChartResumeList.RemoveAll(data => data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                       && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                    case 7://工序及站点移动
                        hProductsChartResumeList.RemoveAll(data => data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                       && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);
                        SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                        break;
                }

            }
            catch (Exception ex)
            {
                tcpLogError.Error(ex);
            }
            finally
            {
                tcpLogInfo.InfoFormat("【衣架生产轨迹记录】 结束");
            }



        }

        #endregion

        #region //生产完成数据转移
        //private void ProductSuccessCopyDataAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var wp = JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
        //    CANProductsService.Instance.CopySucessDataExt(wp);
        //}
        #endregion

        /// <summary>
        /// 记录挂片信息(衣架挂片工序明细)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>

        private void HangingStationOutSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            var hangerProductFlowChartModel = JsonConvert.DeserializeObject<HangerProductFlowChartModel>(arg2);
            var statingEmList = new ProductsQueryServiceImpl().GetEmployeeLoginInfoList(hangerProductFlowChartModel.StatingNo.Value.ToString(), hangerProductFlowChartModel.MainTrackNumber.Value);
            if (statingEmList.Count != 0)
            {
                hangerProductFlowChartModel.CardNo = statingEmList.First().CardNo?.Trim();
                hangerProductFlowChartModel.EmployeeName = statingEmList.First().RealName?.Trim();
                hangerProductFlowChartModel.OutSiteDate = DateTime.Now;
            }
            var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hangerProductFlowChartModel);
            //HangerProductFlowChartDao.Instance.Add(inHangerFlowChart);
            DapperHelp.Add(inHangerFlowChart);

            var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(hangerProductFlowChartModel);
            statingHangerProductItem.Id = null;
            statingHangerProductItem.FlowChartd = hangerProductFlowChartModel.ProcessChartId;
            statingHangerProductItem.ProcessFlowId = hangerProductFlowChartModel.FlowId;
            statingHangerProductItem.ProcessFlowCode = hangerProductFlowChartModel.FlowCode;
            statingHangerProductItem.ProcessFlowName = hangerProductFlowChartModel.FlowName;
            statingHangerProductItem.IsSucessedFlow = true;
            statingHangerProductItem.SiteNo = hangerProductFlowChartModel.StatingNo?.ToString();
            statingHangerProductItem.IsReturnWorkFlow = null == hangerProductFlowChartModel?.FlowType ? false : (hangerProductFlowChartModel?.FlowType == 1 ? true : false);
            statingHangerProductItem.IsReworkSourceStating = hangerProductFlowChartModel?.IsReworkSourceStating;
            statingHangerProductItem.Id = DapperHelp.Add(statingHangerProductItem);
            tcpLogInfo.Info("【挂片站站点挂片产量记录-->记录站点衣架生产记录】");
        }

        private void EmployeeCardRelationAction(RedisChannel arg1, RedisValue arg2)
        {
            var employeeCardRelactionList = EmployeeCardRelationDao.Instance.GetAll();

        }

        //员工登录相关
        private void StatingEmployeeAction(RedisChannel arg1, RedisValue arg2)
        {

        }

        private void HangerReworkReuestQueueAction(RedisChannel arg1, RedisValue arg2)
        {
            var hrrq = JsonConvert.DeserializeObject<HangerReworkRequestQueue>(arg2);
            //HangerReworkRequestQueueDao.Instance.Insert(hrrq);
            DapperHelp.Add(hrrq);
        }
        private int GetNonProductsProcessFlowChartList(List<HangerProductFlowChartModel> hfcList, string hangerNo)
        {
            //var flowIndexNumList = new List<int>();
            //var result = new List<HangerProductFlowChartModel>();
            //var flowIndexList = hfcList.Where(k => k.FlowIndex.Value != 1 && k.MergeProcessFlowChartFlowRelationId == null).Select(f => f.FlowIndex.Value).ToList<int>().OrderBy(f => f).Distinct();
            //foreach (var fIndex in flowIndexList)
            //{
            //    ////没有生产的工序
            //    //var productSuccessCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0).Count();
            //    ////工序下的站点都没生产，或者手动拿出站放到主轨的情况
            //    //var isExiStatingNo = hfcList.Where(f => f.FlowIndex.Value == fIndex && null != f.StatingNo && f.StatingNo.Value != 0 &&
            //    //    (
            //    //        (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value) || (f.Status.Value == HangerProductFlowChartStaus.Producting.Value && f.OutSiteDate == null)
            //    //    )
            //    //).Select(f => f.StatingNo).Count() >= 0;
            //    //if (productSuccessCount > 0 && isExiStatingNo)
            //    //{
            //    //    return fIndex;
            //    //}

            //    var nonProductSuccessReworkCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0 && f.FlowType.Value == 1).Count();
            //    //存在未完成的返工
            //    if (nonProductSuccessReworkCount > 0)
            //    {
            //        if (!flowIndexNumList.Contains(fIndex))
            //        {
            //            flowIndexNumList.Add(fIndex);
            //            continue;
            //        }
            //    }
            //    if (nonProductSuccessReworkCount == 0)
            //    {
            //        var successFlowCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && f.Status.Value == 2 && null != f.StatingNo && f.StatingNo.Value != 0).Count() > 0;
            //        if (!successFlowCount)
            //        {
            //            if (!flowIndexNumList.Contains(fIndex))
            //            {
            //                flowIndexNumList.Add(fIndex);
            //                continue;
            //            }
            //        }

            //    }
            //}
            //if (flowIndexNumList.Count > 0)
            //{
            //    return flowIndexNumList.Min();
            //}
            //var dicCurrentHangerProductingFlowModelCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo))
            {
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo);//dicCurrentHangerProductingFlowModelCache[hangerNo];
                //如果工序被删除
                var flowIsExist = hfcList.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
                if (!flowIsExist)
                {
                    var nextFlowList = hfcList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        && f.FlowIndex.Value > 1
        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        && ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        || ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        ).Select(f => f.FlowIndex);
                    if (nextFlowList.Count() > 0)
                    {
                        return nextFlowList.Min().Value;
                    }
                }
                return current.FlowIndex;
            }
            return -1;
        }
        private int GetNonProductsProcessFlowChartList(List<HangerProductFlowChartModel> hfcList, MainTrackStatingMontorModel montor)
        {
            var nextFlowList = hfcList.Where(f => f.MainTrackNumber == short.Parse(montor.MainTrackNumber.ToString())
            && f.StatingNo != null && f.StatingNo.Value != -1
             && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
             && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
             && ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
             || ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
             ).Select(f => f.FlowIndex);
            //var flowIndexNumList = new List<int>();
            //var result = new List<HangerProductFlowChartModel>();
            //var flowIndexList = hfcList.Where(k => k.FlowIndex.Value != 1 && k.MergeProcessFlowChartFlowRelationId == null).Select(f => f.FlowIndex.Value).ToList<int>().OrderBy(f => f).Distinct();
            //foreach (var fIndex in flowIndexList)
            //{
            //    ////没有生产的工序
            //    //var productSuccessCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0).Count();
            //    ////工序下的站点都没生产，或者手动拿出站放到主轨的情况
            //    //var isExiStatingNo = hfcList.Where(f => f.FlowIndex.Value == fIndex && null != f.StatingNo && f.StatingNo.Value != 0 &&
            //    //    (
            //    //        (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value) || (f.Status.Value == HangerProductFlowChartStaus.Producting.Value && f.OutSiteDate == null)
            //    //    )
            //    //).Select(f => f.StatingNo).Count() >= 0;
            //    //if (productSuccessCount > 0 && isExiStatingNo)
            //    //{
            //    //    return fIndex;
            //    //}

            //    var nonProductSuccessReworkCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0 && f.FlowType.Value == 1).Count();
            //    //存在未完成的返工
            //    if (nonProductSuccessReworkCount > 0)
            //    {
            //        if (!flowIndexNumList.Contains(fIndex))
            //        {
            //            flowIndexNumList.Add(fIndex);
            //            continue;
            //        }
            //    }
            //    if (nonProductSuccessReworkCount == 0)
            //    {
            //        var successFlowCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && f.Status.Value == 2 && null != f.StatingNo && f.StatingNo.Value != 0).Count() > 0;
            //        if (!successFlowCount)
            //        {
            //            if (!flowIndexNumList.Contains(fIndex))
            //            {
            //                flowIndexNumList.Add(fIndex);
            //                continue;
            //            }
            //        }

            //    }
            //}
            //if (flowIndexNumList.Count > 0)
            //{
            //    return flowIndexNumList.Min();
            //}
            if (nextFlowList.Count() > 0)
            {
                return nextFlowList.Distinct().Min().Value;
            }
            return -1;
        }

        #region 监测点
        /// <summary>
        ///// 监测点
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        //private void MainTrackStatingMontorUploadAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var mainTrackStatingMontor = JsonConvert.DeserializeObject<MainTrackStatingMontorModel>(arg2);
        //    var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->开始...", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //    montorLog.Info(logMessage);

        //    try
        //    {

        //       // var dicCurrentHangerProductingFlowModelCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
        //        if (!NewCacheService.Instance.HangerCurrentFlowIsContains(mainTrackStatingMontor.HangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(mainTrackStatingMontor.HangerNo))
        //        {
        //            //衣架衣架生产完成!
        //            tcpLogError.ErrorFormat("【监测点日志】衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, mainTrackStatingMontor.HangerNo);
        //            return;
        //        }
        //        var current = NewCacheService.Instance.GetHangerCurrentFlow(mainTrackStatingMontor.HangerNo); //dicCurrentHangerProductingFlowModelCache[mainTrackStatingMontor.HangerNo];


        //        //var dic = RedisTypeFactory.GetDictionary<string, MainTrackStatingMontorModel>(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION);
        //        //var mtKey = string.Format("{0}:{1}:{2}", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, mainTrackStatingMontor.HangerNo);
        //        //if (!dic.ContainsKey(mtKey))
        //        //{
        //        //    mainTrackStatingMontor.Times = 1;
        //        //    RedisTypeFactory.GetDictionary<string, MainTrackStatingMontorModel>(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION).Add(mtKey, mainTrackStatingMontor);
        //        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 第一次监测!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        //    montorLog.Info(logMessage);
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    //第二次监测点
        //        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 第二次监测!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        //    montorLog.Info(logMessage);

        //        //1.找出生产到第几道工序，然后计算下一站点,给下一站点发送分配命令
        //        var hangerNo = mainTrackStatingMontor.HangerNo.ToString();
        //        var vcHFCList = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //        if (!vcHFCList.ContainsKey(hangerNo))
        //        {
        //            var ex = new ApplicationException(string.Format("主轨:{0} 衣架:{1} 未找到工艺图!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.HangerNo));
        //            montorLog.Error(ex);
        //            return;
        //        }

        //        var fChartList = vcHFCList[hangerNo];
        //        var isExistReworkNoSucess = fChartList.Where(f => f.FlowType.Value == 1 && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
        //        if (isExistReworkNoSucess)
        //        {

        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->返工衣架处理开始!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //            montorLog.Info(logMessage);
        //            MonitoringReworkHangerHandler(fChartList, current, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo.ToString());
        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->返工衣架处理结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //            montorLog.Info(logMessage);
        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //            montorLog.Info(logMessage);
        //            return;
        //        }

        //        //工序是否完成
        //        var flowIsSuccess = CheckFlowIsSuccess(mainTrackStatingMontor.MainTrackNumber, hangerNo, fChartList);

        //        if (!flowIsSuccess)
        //        {
        //            //var isStorageStatingOutSite = fChartList.Where(f => f.StatingRoleCode.Trim().Equals(StatingType.StatingStorage.Code)
        //            //    && f.MainTrackNumber.Value == mainTrackStatingMontor.MainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.IsStorageStatingOutSite).Count() > 0;
        //            HangerStatingAllocationItem allocationItem = null;
        //            var isStorageStatingOutSiteTag = IsStorageStatingOutSite(mainTrackStatingMontor.MainTrackNumber, hangerNo, fChartList, ref allocationItem);
        //            if (isStorageStatingOutSiteTag)
        //            {
        //                /*
        //                ：如果存储站出战 
        //                1.已分配车缝站且车缝站不满站就再分一次且清除之前的缓存；
        //                2.已分配车缝站且车缝站满站就看其他车缝站是否满站，如果不满站就再分给其他车缝站，且清除之前分的缓存；
        //                3.如果车缝站都满站就分存储站,且存储站不满站就清除之前的缓存。
        //                4.如果所有站都满站，则提示满站异常
        //                */

        //                StoreStatingOutSiteMontorHandler(fChartList, mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo.ToString(), mainTrackStatingMontor.HangerNo, allocationItem);

        //                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //                montorLog.Info(logMessage);
        //                return;
        //            }
        //        }
        //        var nonProductFlowIndex = GetNonProductsProcessFlowChartList(fChartList, hangerNo);//GetNonProductsProcessFlowChartList(fChartList, mainTrackStatingMontor);
        //        var fIndexList = fChartList.Where(f => f.FlowIndex.Value == nonProductFlowIndex && (
        //                (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value && f.IncomeSiteDate == null) || (f.Status.Value == HangerProductFlowChartStaus.Producting.Value && f.OutSiteDate == null && f.IncomeSiteDate != null)
        //            ) && f.FlowIndex.Value != 1 &&
        //        null != f.StatingNo && f.StatingNo.Value != -1 && f.AllocationedDate != null);
        //        //var fChartAllocationedNonInStatingList = fIndexList.OrderBy(o => o.FlowIndex).Select(f => f.FlowIndex);
        //        if (nonProductFlowIndex == -1)
        //        {
        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->衣架已生产完成!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //            montorLog.Info(logMessage);
        //            return;
        //        }
        //        //if (fChartAllocationedNonInStatingList.Count() > 0)
        //        //{

        //        #region 【已分配数据修正】 清除已分配衣架站内明细/清除已分配缓存/站点分配数/衣架工艺图分配信息恢复

        //        //修正删除的站内数及明细、缓存
        //        var hnsAllocationStatingNumCacheUpdate = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
        //        hnsAllocationStatingNumCacheUpdate.Action = 7;
        //        hnsAllocationStatingNumCacheUpdate.HangerNo = hangerNo;
        //        hnsAllocationStatingNumCacheUpdate.MainTrackNumber = current.MainTrackNumber;
        //        hnsAllocationStatingNumCacheUpdate.StatingNo = current.StatingNo;
        //        hnsAllocationStatingNumCacheUpdate.FlowNo = current.FlowNo;
        //        hnsAllocationStatingNumCacheUpdate.FlowIndex = current.FlowIndex;
        //        var hnsAllocationStatingNumCacheUpdateJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnsAllocationStatingNumCacheUpdate);
        //        // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnsAllocationStatingNumCacheUpdateJson);
        //        HangerStatingOrAllocationAction(new RedisChannel(), hnsAllocationStatingNumCacheUpdateJson);

        //        //清除已分配衣架站内明细
        //        List<HangerStatingAllocationItem> allocationedHangerList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo);
        //        //var dicHangerStatingALloListCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
        //        //if (dicHangerStatingALloListCache.ContainsKey(hangerNo))
        //        //{
        //        //    allocationedHangerList = dicHangerStatingALloListCache[hangerNo];
        //        //}

        //        //清除已分配缓存
        //        foreach (var item in fIndexList)
        //        {
        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            montorLog.Info(logMessage);

        //            if(CANTcp.client!=null)
        //                CANTcp.client.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);

        //            if (CANTcpServer.server!=null)
        //            {
        //                CANTcpServer.server.ClearHangerCache(item.MainTrackNumber.Value, item.StatingNo.Value, int.Parse(item.HangerNo), SuspeConstants.XOR);
        //            }

        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配站点:{3}】!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            montorLog.Info(logMessage);

        //            //delete
        //            //if (item.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value)
        //            //{
        //            //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【站点:{3}】开始!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            //    montorLog.Info(logMessage);

        //            //    ////站点分配数-1
        //            //    var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = item.MainTrackNumber.Value, StatingNo = item.StatingNo.Value, AllocationNum = -1 };
        //            //    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
        //            //    UpdateMainTrackStatingAllocationNumAction(new RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));

        //            //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【站点:{3}】结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            //    montorLog.Info(logMessage);
        //            //}

        //            //delete
        //            //if (allocationedHangerList != null && allocationedHangerList.Count > 0)
        //            //{
        //            //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【站点:{3}】已分配衣架站内明细结开始!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            //    montorLog.Info(logMessage);
        //            //    allocationedHangerList.RemoveAll(f => f.MainTrackNumber.Value == item.MainTrackNumber.Value && (
        //            //    (
        //            //        f.IncomeSiteDate == null && f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value) || //未进站
        //            //        (f.IncomeSiteDate != null && f.OutSiteDate == null && f.Status.Value == HangerProductFlowChartStaus.Producting.Value)//已进站手工拿出站
        //            //    ) && null != f.NextSiteNo && short.Parse(f.NextSiteNo) == item.StatingNo.Value && (!"-1".Equals(f.Memo)));
        //            //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【站点:{3}】已分配衣架站内明细结结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, item.StatingNo);
        //            //    montorLog.Info(logMessage);

        //            //}

        //        }
        //        //if (allocationedHangerList != null)
        //        //{
        //        //    SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = allocationedHangerList;
        //        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->站内明细已修正!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        //    montorLog.Info(logMessage);
        //        //}



        //        //更新衣架缓存分配信息
        //        foreach (var fc in fChartList)
        //        {
        //            if (fc.FlowIndex.Value == nonProductFlowIndex)
        //            {

        //                //delete
        //                ////站内数-1
        //                //if (fc.Status.Value == HangerProductFlowChartStaus.Producting.Value)
        //                //{
        //                //    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = fc.MainTrackNumber.Value, StatingNo = fc.StatingNo.Value, OnLineSum = -1 };
        //                //    UpdateMainTrackStatingInNumAction(new RedisChannel(), JsonConvert.SerializeObject(inNumModel));

        //                //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【生产序号:{3} 站点:{4}】衣架站内衣架数据!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, nonProductFlowIndex, fc.StatingNo);
        //                //    montorLog.Info(logMessage);
        //                //}

        //                fc.AllocationedDate = null;
        //                fc.isAllocationed = false;
        //                fc.IncomeSiteDate = null;
        //                fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
        //                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除【生产序号:{3} 站点:{4}】 《衣架工艺图》-->衣架缓存分配信息及站内衣架数据!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, nonProductFlowIndex, fc.StatingNo);
        //                montorLog.Info(logMessage);

        //            }
        //        }
        //        #endregion

        //        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->计算下一站开始:!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        montorLog.Info(logMessage);


        //        //if (fChartAllocationedNonInStatingList.Count() == 0)
        //        //{
        //        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->没找到待分配工序站点信息!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        //    montorLog.Error(logMessage);
        //        //    return;
        //        //}
        //        var nonSucessCurrentFlowIndex = nonProductFlowIndex;//fChartAllocationedNonInStatingList.First();

        //        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->监测点重新分配工序生产顺序号:{3}!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo, nonSucessCurrentFlowIndex);
        //        montorLog.Info(logMessage);

        //        var fChart = fChartList.Where(f => f.FlowIndex.Value == nonSucessCurrentFlowIndex).First();
        //        var nonSucessFlowStatingList = fChartList.Where(f => f.FlowIndex.Value == nonSucessCurrentFlowIndex
        //         && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
        //        && f.Status.Value != HangerProductFlowChartStaus.Successed.Value)
        //                                                 .Select(f => new ProductsProcessOrderModel()
        //                                                 {
        //                                                     StatingNo = f.StatingNo.ToString(),
        //                                                     MainTrackNumber = (int)f.MainTrackNumber,
        //                                                     StatingCapacity = f.StatingCapacity.Value,
        //                                                     Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
        //                                                     ProcessChartId = f.ProcessChartId,
        //                                                     FlowNo = f.FlowNo,
        //                                                     StatingRoleCode = f.StatingRoleCode
        //                                                 }).ToList<ProductsProcessOrderModel>();
        //        if (nonSucessFlowStatingList.Count == 0)
        //        {
        //            //下一道没有可以接收衣架的站
        //            var exx = new NoFoundStatingException(string.Format("【监测点】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackStatingMontor.MainTrackNumber, mainTrackStatingMontor.StatingNo, hangerNo))
        //            {
        //                FlowNo = fChartList.Where(k => k.FlowIndex.Value == nonSucessCurrentFlowIndex)?.First()?.FlowNo
        //            };
        //            montorLog.Error(exx);
        //            return;
        //        }
        //        int outMainTrackNumber = 0;
        //        //var nextStatingNo = OutSiteService.Instance.CalcateStatingNo(nonSucessFlowStatingList, ref outMainTrackNumber, true);
        //        var nextStatingNo = string.Empty;
        //        OutSiteService.Instance.AllocationNextProcessFlowStating(nonSucessFlowStatingList, ref outMainTrackNumber, ref nextStatingNo, true);
        //        if (string.IsNullOrEmpty(nextStatingNo))
        //        {
        //            logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->衣架已生产完成!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //            montorLog.Info(logMessage);
        //            return;
        //        }
              
        //        //记录衣架分配

        //        if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo)) //dicHangerStatingALloListCache.ContainsKey(hangerNo))
        //        {
        //                var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingALloListCache[hangerNo];
        //            var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
        //            nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
        //            nextHangerStatingAllocationItem.FlowIndex = (short)nonSucessCurrentFlowIndex;
        //            nextHangerStatingAllocationItem.SiteNo = null;
        //            nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
        //            nextHangerStatingAllocationItem.HangerNo = hangerNo;
        //            nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
        //            nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
        //            nextHangerStatingAllocationItem.FlowNo = fChart?.FlowNo;
        //            nextHangerStatingAllocationItem.FlowChartd = fChart?.ProcessChartId;
        //            nextHangerStatingAllocationItem.ProductsId = fChart?.ProductsId;
        //            nextHangerStatingAllocationItem.ProcessFlowCode = fChart?.FlowCode;
        //            nextHangerStatingAllocationItem.ProcessFlowName = fChart?.FlowName;
        //            nextHangerStatingAllocationItem.ProcessFlowId = fChart?.FlowId;
        //            nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
        //            nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
        //            nextHangerStatingAllocationItem.Memo = "监测点衣架重新分配";
        //            nextHangerStatingAllocationItem.isMonitoringAllocation = true;
        //            nextHangerStatingAllocationItem.LastFlowIndex = nonSucessCurrentFlowIndex;
        //            nextHangerStatingAllocationItem.IsStatingStorageOutStating = CANProductsService.Instance.IsStoreStating(fChart);

        //            dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
        //            //  SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
        //            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);

        //            var tenMainTrackNumber = outMainTrackNumber.ToString();
        //            var susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送开始!", tenMainTrackNumber, nextStatingNo);
        //            tcpLogInfo.Info(susAllocatingMessage);

        //            if (CANTcp.client != null)
        //                CANTcp.client.AllocationHangerToNextStating(tenMainTrackNumber, nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);
        //            if (null != CANTcpServer.server)
        //                CANTcpServer.server.AllocationHangerToNextStating(tenMainTrackNumber, nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);

        //            susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenMainTrackNumber, nextStatingNo);
        //            montorLog.Info(susAllocatingMessage);


        //            //记录衣架分配
        //            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
        //            SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

        //            //再次分配修正工艺图分配日期和状态
        //            //更新衣架缓存分配信息
        //            foreach (var fc in fChartList)
        //            {
        //                if (fc.FlowIndex.Value == nonProductFlowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(nextStatingNo))
        //                {
        //                    fc.AllocationedDate = DateTime.Now;
        //                    fc.isAllocationed = true;
        //                    break;
        //                }
        //            }
        //            //发布衣架状态
        //            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
        //            chpf.HangerNo = hangerNo;
        //            chpf.MainTrackNumber = outMainTrackNumber;
        //            chpf.StatingNo = int.Parse(string.IsNullOrEmpty(nextStatingNo) ? "-1" : nextStatingNo);
        //            chpf.FlowNo = fChart?.FlowNo;
        //            chpf.FlowIndex = nonProductFlowIndex;
        //            chpf.FlowType = null == fChart?.FlowType ? 0 : fChart.FlowType.Value;
        //            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
        //            SusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

        //            //修正删除的站内数及明细、缓存
        //            var hnsAllocationUpdate = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
        //            hnsAllocationUpdate.Action = 0;
        //            hnsAllocationUpdate.HangerNo = hangerNo;
        //            hnsAllocationUpdate.MainTrackNumber = outMainTrackNumber;
        //            hnsAllocationUpdate.StatingNo = int.Parse(nextStatingNo);
        //            hnsAllocationUpdate.FlowNo = fChart.FlowNo;
        //            hnsAllocationUpdate.FlowIndex = fChart.FlowIndex.Value;
        //            hnsAllocationUpdate.HangerProductFlowChartModel = fChart;
        //            var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnsAllocationUpdate);
        //        //    SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
        //            //HangerStatingOrAllocationAction(new RedisChannel(), hnssocDeleteStatingJson);
        //            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);

        //            //【衣架生产履历】下一站分配Cache写入
        //            var nextStatingHPResume = new HangerProductsChartResumeModel()
        //            {
        //                HangerNo = hangerNo,
        //                StatingNo = nextStatingNo,
        //                MainTrackNumber = outMainTrackNumber,
        //                HangerProductFlowChart = fChart,
        //                Action = 1,
        //                NextStatingNo = nextStatingNo
        //            };
        //            var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
        //            //  SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
        //            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
        //        }

        //        //更新衣架工艺图
        //        SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = fChartList;

        //        //清除监测点数据
        //        // RedisTypeFactory.GetDictionary<string, MainTrackStatingMontorModel>(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION)[mtKey] = null;

        //        //logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除监测临时数据!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        //montorLog.Info(logMessage);
        //        // }
        //        //  }
        //    }
        //    catch (Exception ex)
        //    {
        //        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->监测点分配错误!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        montorLog.Error(logMessage, ex);
        //    }
        //    finally
        //    {
        //        logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
        //        montorLog.Info(logMessage);
        //    }

        //}

        //private void MonitoringReworkHangerHandler(List<HangerProductFlowChartModel> fChartList, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current,
        // int monitoringMainTrackNumber, string monitoringStatingNo)
        //{
        //    // return;
        //    var hangerNo = current.HangerNo;
        //    var mainTrackNumber = current.MainTrackNumber;
        //    //是否是未过桥接站而下位机丢失分配缓存等信息
        //    var dicBridgeReowrkNextStatingCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM);
        //    if (dicBridgeReowrkNextStatingCache.ContainsKey(hangerNo))
        //    {
        //        var reworkModel = dicBridgeReowrkNextStatingCache[hangerNo];
        //        int outMainTrackNumber = 0;
        //        string nextStatingNo = null;
        //        //桥接站需要再分配
        //        OutSiteService.Instance.AllocationNextProcessFlowStating(reworkModel.NextStatingList, ref outMainTrackNumber, ref nextStatingNo);
        //        var mMessage = string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接站分配计算结束】 主轨:{0} 监测站点:{1} 下一站主轨{1}:站点【{2}】 ",
        //                monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo);
        //        tcpLogInfo.InfoFormat(mMessage);
        //        if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
        //        {
        //            var ex = new FlowNotFoundException(string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接站分配计算异常】 主轨:{0} 监测站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ",
        //                monitoringMainTrackNumber, monitoringStatingNo, current.MainTrackNumber, current.StatingNo));
        //            tcpLogError.Error(ex);
        //            throw ex;
        //        }
        //        //需要桥接
        //        if (mainTrackNumber != outMainTrackNumber)
        //        {
        //            MonitorStatingReworkHangerBridgeHandler(current, monitoringMainTrackNumber, monitoringStatingNo, hangerNo, mainTrackNumber, reworkModel, outMainTrackNumber, nextStatingNo);
        //            //var ex = new ApplicationException(string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->衣架返工工序及疵点代码】 主轨:{0} 监测站点:{1} 桥接处理异常! 下一站主轨:{2} 站点:{3} ", monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo));
        //            //tcpLogError.Error(ex);
        //            // throw ex;
        //            return;
        //        }

        //        return;
        //    }
        //    //已过监测点下位机断电
        //    MonitorStatingReworkHangerNonRequireBridgeHandler(fChartList, current,
        //     monitoringMainTrackNumber, monitoringStatingNo);
        //}
        
        ///// <summary>
        ///// 返工衣架已过桥接站，下位机断电
        ///// </summary>
        ///// <param name="fChartList"></param>
        ///// <param name="current"></param>
        ///// <param name="monitoringMainTrackNumber"></param>
        ///// <param name="monitoringStatingNo"></param>
        //private void MonitorStatingReworkHangerNonRequireBridgeHandler(List<HangerProductFlowChartModel> fChartList, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current,
        //    int monitoringMainTrackNumber, string monitoringStatingNo)
        //{
        //    var statingList = fChartList.Where(k => k.FlowIndex.Value == current.FlowIndex && k.StatingNo != null
        //    && k.StatingNo.Value == current.StatingNo
        //   && k.StatingNo.Value != -1 && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
        //   && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)).Select(f => new ProductsProcessOrderModel()
        //   {
        //       StatingNo = f.StatingNo.ToString(),
        //       MainTrackNumber = (int)f.MainTrackNumber,
        //       StatingCapacity = f.StatingCapacity.Value,
        //       Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
        //       ProcessChartId = f.ProcessChartId,
        //       FlowNo = f.FlowNo,
        //       StatingRoleCode = f.StatingRoleCode
        //   }).ToList<ProductsProcessOrderModel>();
        //    var reworkFlow = fChartList.Where(f => f.FlowNo.Equals(current.FlowNo)).First();
        //    int outMainTrackNumber = 0;
        //    string nextStatingNo = null;
        //    //桥接站需要再分配
        //    var flowIndex = current.FlowIndex;
        //    var statingNo = current.StatingNo;
        //    var hangerNo = current.HangerNo;
        //    var mainTrackNumber = current.MainTrackNumber;
        //    OutSiteService.Instance.AllocationNextProcessFlowStating(statingList, ref outMainTrackNumber, ref nextStatingNo);

        //    var mMessage = string.Format("【监测点-->返工衣架过桥接下位机断电返工衣架处理-->下一站分配计算完成】 主轨:{0} 监测站点:{1} 下一站主轨{1}:站点【{2}】 ",
        //                monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo);
        //    tcpLogInfo.InfoFormat(mMessage);
        //    if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
        //    {
        //        var ex = new FlowNotFoundException(string.Format("【监测点-->返工衣架过桥接下位机断电返工衣架处理-->桥接站分配计算异常】 主轨:{0} 监测站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ",
        //            monitoringMainTrackNumber, monitoringStatingNo, current.MainTrackNumber, current.StatingNo));
        //        tcpLogError.Error(ex);
        //        throw ex;
        //    }
        //    var dicHangerAllocationListList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); // SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo];
        //    var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
        //    nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
        //    nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
        //    nextHangerStatingAllocationItem.SiteNo = statingNo.ToString();
        //    nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
        //    nextHangerStatingAllocationItem.HangerNo = hangerNo.ToString();
        //    nextHangerStatingAllocationItem.HangerType = 1;
        //    nextHangerStatingAllocationItem.IsReworkSourceStating = true;
        //    nextHangerStatingAllocationItem.IsReturnWorkFlow = true;
        //    nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
        //    nextHangerStatingAllocationItem.OutMainTrackNumber = mainTrackNumber;
        //    nextHangerStatingAllocationItem.FlowNo = reworkFlow.FlowNo;
        //    nextHangerStatingAllocationItem.ProcessFlowCode = reworkFlow.FlowCode;
        //    nextHangerStatingAllocationItem.ProcessFlowName = reworkFlow.FlowName;
        //    nextHangerStatingAllocationItem.ProcessFlowId = reworkFlow.FlowId;
        //    nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
        //    nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
        //    nextHangerStatingAllocationItem.LastFlowIndex = -1;
        //    nextHangerStatingAllocationItem.isMonitoringAllocation = true;
        //    nextHangerStatingAllocationItem.IsStatingStorageOutStating = CANProductsService.Instance.IsStoreStating(reworkFlow);

        //    dicHangerAllocationListList.Add(nextHangerStatingAllocationItem);
        //    // SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()] = dicHangerAllocationListList;
        //    NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerAllocationListList);

        //    var tenMainTrackNumber = outMainTrackNumber;//HexHelper.TenToHexString2Len(outMainTrackNumber);
        //    CANTcpServer.server.AllocationHangerToNextStating(tenMainTrackNumber.ToString(), nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), null);
        //    var susAllocatingMessage = string.Format("【监测点-->返工衣架过桥接下位机断电返工衣架处理-->主轨:{0} 监测站点:{1} 衣架往主轨【{2}】 站点【{3}】 分配指令已发送成功!", monitoringMainTrackNumber, monitoringStatingNo, tenMainTrackNumber, nextStatingNo);
        //    tcpLogInfo.Info(susAllocatingMessage);


        //    //发布 待生产衣架信息
        //    var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
        //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);
        //}
        ///// <summary>
        /////未过桥接站而下位机丢失分配缓存等信息,监测点对返工衣架的处理
        ///// </summary>
        ///// <param name="current"></param>
        ///// <param name="monitoringMainTrackNumber"></param>
        ///// <param name="monitoringStatingNo"></param>
        ///// <param name="hangerNo"></param>
        ///// <param name="mainTrackNumber"></param>
        ///// <param name="reworkModel"></param>
        ///// <param name="outMainTrackNumber"></param>
        ///// <param name="nextStatingNo"></param>
        //private void MonitorStatingReworkHangerBridgeHandler(Domain.Cus.CurrentHangerProductingFlowModel current, int monitoringMainTrackNumber, string monitoringStatingNo, string hangerNo, int mainTrackNumber, ReworkModel reworkModel, int outMainTrackNumber, string nextStatingNo)
        //{
        //    string bridgeType = string.Empty;
        //    SuspeSys.Domain.BridgeSet bridge = null;
        //    var sourceRewokFlowChart = reworkModel.SourceRewokFlowChart;
        //    //var nStatingNo = int.Parse(nextStatingNo);
        //    bool isBridge = CANProductsService.Instance.IsBridgeByOutSiteRequestAction(mainTrackNumber, current.StatingNo, outMainTrackNumber, nextStatingNo, int.Parse(hangerNo)
        //        , ref bridgeType, ref bridge);
        //    if (isBridge)
        //    {
        //        if (bridgeType == BridgeType.Bridge_Stating_Non_Flow_Chart_ALL || bridgeType == BridgeType.Bridge_Stating_One_In_Flow_Chart
        //            || bridgeType == BridgeType.Bridge_Stating_IN_Flow_Chart_ALL)
        //        {
        //            var tenBridgeMaintrackNumber = bridge.AMainTrackNumber.Value.ToString();
        //            var tenBridgeStatingNo = bridge.ASiteNo.Value;
        //            var aMainTracknumberBridgeStatingIsInFlowChart = false;

        //            //处理分配关系
        //            CANProductsService.Instance.BridgeAllocationRelation(mainTrackNumber, int.Parse(monitoringStatingNo), outMainTrackNumber, nextStatingNo, int.Parse(hangerNo),
        //                tenBridgeStatingNo.ToString(), aMainTracknumberBridgeStatingIsInFlowChart,true);
        //            var hexHangerNo = HexHelper.TenToHexString10Len(hangerNo);
        //            //给下一站分配衣架
        //            CANTcpServer.server.AllocationHangerToNextStating(tenBridgeMaintrackNumber, tenBridgeStatingNo.ToString(), hexHangerNo);

        //            var susAllocatingMessage = string.Format("【监测点-->未过桥接下位机断电返工衣架处理-->桥接分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", tenBridgeMaintrackNumber, bridge.ASiteNo.Value);
        //            var info = susAllocatingMessage;
        //            tcpLogInfo.Info(susAllocatingMessage);

        //            //更新返工衣架工艺图到缓存
        //            //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = sourceRewok;
        //            //清除桥接站出战缓存
        //            Products.SusCache.SusCacheBootstarp.Instance.ClearBridgeStatingHangerOutSiteItem(hangerNo.ToString());
        //            //清除桥接站进站缓存
        //            Products.SusCache.SusCacheBootstarp.Instance.ClearBridgeStatingHangerInSiteItem(hangerNo.ToString());

        //            // //站内数处理
        //            // var hpResume = new HangerProductsChartResumeModel()
        //            // {
        //            //     HangerNo = hangerNo.ToString(),
        //            //     StatingNo =current.StatingNo.ToString(),
        //            //     MainTrackNumber = mainTrackNumber,
        //            //     HangerProductFlowChart = sourceRewokFlowChart,
        //            //     //HangerProductFlowChartList = new List<HangerProductFlowChartModel>() { nextReworkFlowChart },
        //            //     ReworkFlowNos = string.Join(",", reworkModel.ReworkFlowNoList),
        //            //     Action = 4
        //            // };
        //            // var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
        //            // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);

        //            // //修正删除的站内数及明细、缓存
        //            // var dicCurrentHangerProductingFlowModelCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
        //            //// var current = dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];
        //            // var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
        //            // hnssocDeleteStating.Action = 6;
        //            // hnssocDeleteStating.HangerNo = hangerNo.ToString();
        //            // hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
        //            // hnssocDeleteStating.StatingNo = current.StatingNo;
        //            // hnssocDeleteStating.FlowNo = current.FlowNo;
        //            // hnssocDeleteStating.FlowIndex = current.FlowIndex;
        //            // //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
        //            // var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
        //            // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
        //            return;
        //        }
        //        var exBridgeApp = new ApplicationException(string.Format("【监测点-->衣架返工-->衣架返工工序及疵点代码】 主轨:{0} 监测站点:{1}桥接类型未找到! 下一站主轨:{2} 站点:{3}", monitoringMainTrackNumber, monitoringStatingNo, outMainTrackNumber, nextStatingNo));
        //        tcpLogError.Error(exBridgeApp);
        //        //throw exBridgeApp;

        //    }
        //}


        //private bool IsStorageStatingOutSite(int mainTrackNumber, string hangerNo, List<HangerProductFlowChartModel> fChartList, ref HangerStatingAllocationItem allocationItem)
        //{
        //    //var dicHangerStatingAllocationItem = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
        //    if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingAllocationItem.ContainsKey(hangerNo))
        //    {
        //            var allocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingAllocationItem[hangerNo];
        //        var lastHangerStatingAllocationItemList = allocationList.Where(f => f.Status.Value != 1 && ((f.Memo != null && f.Memo.Equals("-1")) || f.Memo == null) && f.AllocatingStatingDate != null);
        //        if (lastHangerStatingAllocationItemList.Count() > 0)
        //        {
        //            var lastHangerStatingAllocationItem = lastHangerStatingAllocationItemList.OrderByDescending(f => f.AllocatingStatingDate).First();
        //            var isStatingStorageStatingOutSite = CANProductsService.Instance.IsStatingStorage(lastHangerStatingAllocationItem.MainTrackNumber.Value, int.Parse(lastHangerStatingAllocationItem.SiteNo));
        //            allocationItem = lastHangerStatingAllocationItem;
        //            return isStatingStorageStatingOutSite;
        //        }
        //    }
        //    return false;
        //}

        //private void StoreStatingOutSiteMontorHandler(List<HangerProductFlowChartModel> fChartList, int mainTrackNumber, string statingNo, string hangerNo, HangerStatingAllocationItem allocationItem)
        //{
        //    /*
        //              ：如果存储站出战 
        //              1.已分配车缝站且车缝站不满站就再分一次且清除之前的缓存；
        //              2.已分配车缝站且车缝站满站就看其他车缝站是否满站，如果不满站就再分给其他车缝站，且清除之前分的缓存；
        //              3.如果车缝站都满站就分存储站,且存储站不满站就清除之前的缓存。
        //              4.如果所有站都满站，则提示满站异常
        //              */
        //    //1
        //    var nextFlowIndexList = fChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(allocationItem.SiteNo)).Select(f => f.FlowIndex).Distinct();
        //    if (nextFlowIndexList.Count() == 0)
        //    {
        //        var exx = new ApplicationException(string.Format("【监测点存储站出战】主轨{0} 站点:{1} 衣架:{2} 找不到分配的站点信息!", mainTrackNumber, statingNo, hangerNo));
        //        montorLog.Error(exx);
        //        return;
        //    }
        //    if (nextFlowIndexList.Count() != 1)
        //    {
        //        var exx = new ApplicationException(string.Format("【监测点存储站出战】主轨{0} 站点:{1} 衣架:{2} 工序大于2个!", mainTrackNumber, statingNo, hangerNo));
        //        montorLog.Error(exx);
        //        return;
        //    }
        //    var flowIndex = nextFlowIndexList.First();
        //    //找下一站点
        //    var nextFlowStatlist = fChartList.Where(k => k.FlowIndex.Value == flowIndex
        //    && k.StatingNo != null && k.StatingNo.Value != -1
        //    && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
        //    && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
        //    && null != k.StatingRoleCode
        //    //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
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
        //    if (nextFlowStatlist.Count == 0)
        //    {
        //        //下一道没有可以接收衣架的站
        //        var exx = new NoFoundStatingException(string.Format("【监测点存储站出战】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNumber, statingNo, hangerNo))
        //        {
        //            FlowNo = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First().FlowNo
        //        };
        //        montorLog.Error(exx);
        //        return;
        //    }
        //    int outMainTrackNumber = 0;
        //    string nextStatingNo = null;
        //    //【获取下一站】
        //    OutSiteService.Instance.AllocationNextProcessFlowStating(nextFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
        //    if (string.IsNullOrEmpty(nextStatingNo))
        //    {
        //        var exx = new NoFoundStatingException(string.Format("【监测点存储站出战】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNumber, statingNo, hangerNo))
        //        {
        //            FlowNo = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First().FlowNo
        //        };
        //        montorLog.Error(exx);
        //        return;
        //    }

        //    //---已分配缓存清除，站内数修正，衣架工艺图站点分配状态恢复
        //    #region 已分配缓存及站内数修正
        //    var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【主轨:{3} 站点:{4}】开始!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
        //    montorLog.Info(logMessage);

        //    ////站点分配数-1
        //    var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = allocationItem.MainTrackNumber.Value, StatingNo = int.Parse(allocationItem.NextSiteNo), AllocationNum = -1 };
        //    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
        //    UpdateMainTrackStatingAllocationNumAction(new RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));

        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->修正在线数【主轨:{3} 站点:{4}】结束!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
        //    montorLog.Info(logMessage);

        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存开始【已分配主轨:{3} 站点:{4}】!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
        //    montorLog.Info(logMessage);
        //    if(CANTcp.client!=null)
        //        CANTcp.client.ClearHangerCache(allocationItem.MainTrackNumber.Value, int.Parse(allocationItem.NextSiteNo), int.Parse(allocationItem.HangerNo), SuspeConstants.XOR);
        //    if(null!= CANTcpServer.server)
        //        CANTcpServer.server.ClearHangerCache(allocationItem.MainTrackNumber.Value, int.Parse(allocationItem.NextSiteNo), int.Parse(allocationItem.HangerNo), SuspeConstants.XOR);
        //    logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->清除已分配缓存结束【已分配主轨:{3} 站点:{4}】!", hangerNo, mainTrackNumber, statingNo, allocationItem.MainTrackNumber, allocationItem.NextSiteNo);
        //    montorLog.Info(logMessage);


        //    //---清除已分配缓存


        //    //修正：如果本次分配和上次不同，则修正上次的工艺图状态为未分配?????/lucifer/2018年10月15日 21:09:09
        //    if (null != allocationItem.NextSiteNo && !allocationItem.NextSiteNo.Equals(nextStatingNo))
        //    {
        //        foreach (var fc in fChartList)
        //        {
        //            if (fc.FlowIndex.Value == flowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(allocationItem.NextSiteNo) && allocationItem.MainTrackNumber.Value == fc.MainTrackNumber.Value)
        //            {
        //                fc.AllocationedDate = null;
        //                fc.isAllocationed = false;
        //                break;
        //            }
        //        }
        //    }
        //    #endregion

        //    var fChart = fChartList.Where(k => k.FlowIndex.Value == flowIndex).First();
           
        //    //记录衣架分配
        //   // var dicHangerStatingAllocationItem = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
        //    if (NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo))//dicHangerStatingAllocationItem.ContainsKey(hangerNo))
        //    {
        //        var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //dicHangerStatingAllocationItem[hangerNo];
        //        var nextHangerStatingAllocationItem = new HangerStatingAllocationItem();
        //        nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
        //        nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
        //        nextHangerStatingAllocationItem.SiteNo = null;
        //        nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
        //        nextHangerStatingAllocationItem.HangerNo = hangerNo;
        //        nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
        //        nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
        //        nextHangerStatingAllocationItem.FlowNo = fChart?.FlowNo;
        //        nextHangerStatingAllocationItem.FlowChartd = fChart?.ProcessChartId;
        //        nextHangerStatingAllocationItem.ProductsId = fChart?.ProductsId;
        //        nextHangerStatingAllocationItem.ProcessFlowCode = fChart?.FlowCode;
        //        nextHangerStatingAllocationItem.ProcessFlowName = fChart?.FlowName;
        //        nextHangerStatingAllocationItem.ProcessFlowId = fChart?.FlowId;
        //        nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
        //        nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
        //        nextHangerStatingAllocationItem.Memo = "监测点衣架重新分配";
        //        nextHangerStatingAllocationItem.isMonitoringAllocation = true;
        //        dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
        //        //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
        //        NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo,dicHangerStatingALloList);

        //        var hexOutID = HexHelper.TenToHexString2Len(outMainTrackNumber);
        //        var susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送开始!", hexOutID, nextStatingNo);
        //        tcpLogInfo.Info(susAllocatingMessage);

        //        if (CANTcp.client != null)
        //            CANTcp.client.AllocationHangerToNextStating(hexOutID, nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);
        //        if (CANTcpServer.server != null)
        //            CANTcpServer.server.AllocationHangerToNextStating(hexOutID, nextStatingNo, HexHelper.TenToHexString10Len(hangerNo), SuspeConstants.XOR);

        //        susAllocatingMessage = string.Format("【监测点重新分配消息】 衣架往主轨【{0}】 站点【{1}】 分配指令已发送成功!", hexOutID, nextStatingNo);
        //        montorLog.Info(susAllocatingMessage);

        //        //记录衣架分配
        //        var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
        //        SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

        //        //再次分配修正工艺图分配日期和状态
        //        //更新衣架缓存分配信息

        //        foreach (var fc in fChartList)
        //        {
        //            if (fc.FlowIndex.Value == flowIndex && null != fc.StatingNo && !string.IsNullOrEmpty(nextStatingNo) && fc.StatingNo.Value == short.Parse(nextStatingNo) && fc.MainTrackNumber.Value == outMainTrackNumber)
        //            {
        //                fc.AllocationedDate = DateTime.Now;
        //                fc.isAllocationed = true;
        //                break;
        //            }
        //        }
        //        //发布衣架状态
        //        var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
        //        chpf.HangerNo = hangerNo;
        //        chpf.MainTrackNumber = outMainTrackNumber;
        //        chpf.StatingNo = int.Parse(string.IsNullOrEmpty(nextStatingNo) ? "-1" : nextStatingNo);
        //        chpf.FlowNo = fChart?.FlowNo;
        //        chpf.FlowIndex = flowIndex.Value;
        //        chpf.FlowType = null == fChart?.FlowType ? 0 : fChart.FlowType.Value;
        //        var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
        //        SusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

        //        //修正站点缓存
        //        var hnssocAll = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
        //        hnssocAll.Action = 0;
        //        hnssocAll.HangerNo = hangerNo;
        //        hnssocAll.MainTrackNumber = outMainTrackNumber;
        //        hnssocAll.StatingNo = int.Parse(nextStatingNo);
        //        hnssocAll.FlowNo = fChart.FlowNo;
        //        hnssocAll.FlowIndex = fChart.FlowIndex.Value;
        //        hnssocAll.HangerProductFlowChartModel = fChart;
        //        var hnssocAllJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocAll);
        //        // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocAllJson);
        //        NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocAllJson);
        //    }

        //    //更新衣架工艺图
        //    SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = fChartList;
        //}

        //private bool CheckFlowIsSuccess(int mainTrackNumber, string hangerNo, List<HangerProductFlowChartModel> fChartList)
        //{
        //    // return true;
        //    var isNonFlowSuccess = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0;
        //    if (isNonFlowSuccess) return false;
        //    var successedFlowIndexList = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1 && f.Status.Value == HangerProductFlowChartStaus.Successed.Value)
        //        .Select(k => k.FlowIndex.Value).Distinct().ToList();
        //    var allFlowIndexList = fChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo != null && f.StatingNo.Value != -1)
        //        .Select(k => k.FlowIndex.Value).Distinct().ToList();
        //    if (successedFlowIndexList.Count > 0 && allFlowIndexList.Count > 0)
        //    {
        //        return successedFlowIndexList.Max() == allFlowIndexList.Max();
        //    }
        //    return isNonFlowSuccess;

        //}

        #endregion

        ///// <summary>
        ///// 在线数更新
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        //private void UpdateMainTrackStatingOnlineNumAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var dic = RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM);
        //    var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
        //    var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);
        //    if (!dic.ContainsKey(key))
        //    {
        //        RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM).Add(key, mts.OnLineSum);
        //    }
        //    else
        //    {
        //        var num = dic[key];
        //        if (num == 0)
        //        {
        //            if (mts.OnLineSum > 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num + 1;
        //            }
        //        }
        //        else
        //        {
        //            if (mts.OnLineSum < 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num - 1;
        //            }
        //            else
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num + 1;
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// 站内数维护
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        //private void UpdateMainTrackStatingInNumAction(RedisChannel arg1, RedisValue arg2)
        //{

        //    var dic = RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
        //    var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
        //    var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);

        //    tcpLogInfo.InfoFormat("【站内数维护】站内数维护:站点主轨{0}", key);
        //    tcpLogInfo.InfoFormat("【站内数维护】站内数维护:内容{0}", JsonConvert.SerializeObject(mts));

        //    if (!dic.ContainsKey(key))
        //    {
        //        if (mts.OnLineSum > 0)
        //            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM).Add(key, 1);
        //    }
        //    else
        //    {
        //        var num = dic[key];
        //        if (num == 0)
        //        {
        //            if (mts.OnLineSum > 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num + 1;
        //            }
        //        }
        //        else
        //        {
        //            if (mts.OnLineSum < 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num - 1;
        //            }
        //            else
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num + 1;
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// 分配数维护
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        //private void UpdateMainTrackStatingAllocationNumAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var dic = RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);
        //    var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
        //    var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);
        //    if (!dic.ContainsKey(key))
        //    {
        //        if (mts.AllocationNum > 0)
        //            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM).Add(key, 1);
        //    }
        //    else
        //    {
        //        var num = dic[key];
        //        if (num == 0)
        //        {
        //            if (mts.AllocationNum > 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num + 1;
        //            }
        //        }
        //        else
        //        {
        //            if (mts.AllocationNum < 0)
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num - 1;
        //            }
        //            else
        //            {
        //                RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num + 1;
        //            }
        //        }
        //    }
        //}

        //private void MainTrackStatingStatusAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var mainTrackStatingInfo = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
        //    var mainStatingKey = string.Format("{0}:{1}", mainTrackStatingInfo.MainTrackNumber, mainTrackStatingInfo.StatingNo);
        //    var dic = RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
        //    if (dic.Keys.Contains(mainStatingKey))
        //    {
        //        RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS)[mainStatingKey] = mainTrackStatingInfo;
        //    }
        //    else
        //    {
        //        dic.Add(mainStatingKey, mainTrackStatingInfo);
        //    }
        //}

        //void SetGroupNo(HangerOutSiteResult hos) {
        //    if (null== hos.HangerProductFlowChart) {
        //        tcpLogError.ErrorFormat("主轨:{0} 站点:{1} 没有工艺图!", hos.MainTrackNumber, hos.StatingNo);
        //        return;
        //    }
        //    var dicStatingCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
        //    var key = string.Format("{0}:{1}", hos.HangerProductFlowChart.MainTrackNumber.Value, hos.HangerProductFlowChart.StatingNo.Value);
        //    if (!dicStatingCache.ContainsKey(key)) {
        //        tcpLogError.ErrorFormat("主轨:{0} 站点:{1} 没有找到组!", hos.HangerProductFlowChart.MainTrackNumber.Value, hos.HangerProductFlowChart.StatingNo.Value);
        //    }
        //    hos.HangerProductFlowChart.GroupNo = dicStatingCache[key].GroupNO?.Trim();
        //    //return "";
        //}
       
        ///// <summary>
        /////出战处理处理:
        /////1.衣架工艺图数据写入db;
        ////2.站点衣架生产明细记录db;
        ////3.衣架出战衣架产品明细写入db
        ////4.存储站不记录产量
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        //public void HangerOutSiteAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var outSiteResult = JsonConvert.DeserializeObject<HangerOutSiteResult>(arg2);
        //    SetGroupNo(outSiteResult);
        //     //HangerProductItem hpItem = null;
        //     //var dicHangerProductItemList = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
        //     //if (dicHangerProductItemList.ContainsKey(outSiteResult.HangerNo))
        //     //{
        //     //    var hanerProductItemList = dicHangerProductItemList[outSiteResult.HangerNo];
        //     //    foreach (var hp in hanerProductItemList)
        //     //    {
        //     //        if (hp.SiteNo.Equals(outSiteResult.StatingNo) && hp.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber)
        //     //        {
        //     //            hpItem = hp;
        //     //            var hangerProductItem = BeanUitls<HangerProductItem, HangerProductItemModel>.Mapper(hp);
        //     //            //HangerProductItemDao.Instance.Add(hangerProductItem);
        //     //            DapperHelp.Add(hangerProductItem);
        //     //        }
        //     //    }
        //     //}
        //     //lucifer/2018年10月12日--
        //     //站点衣架缓存
        //     var statingHangerKey = string.Format("{0}:{1}", outSiteResult.MainTrackNumber, int.Parse(outSiteResult.StatingNo));
        //    var dicHangerProductItemListExt = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT);
        //    if (!dicHangerProductItemListExt.ContainsKey(statingHangerKey))
        //    {
        //        var outHangerProductsItemModelList = new List<HangerProductItemModel>();
        //        var outStatingHangerProductItem = BeanUitls<HangerProductItemModel, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);

        //        outStatingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
        //        outStatingHangerProductItem.SizeNum = string.IsNullOrEmpty(outSiteResult.HangerProductFlowChart.Num) ? 0 : int.Parse(outSiteResult.HangerProductFlowChart.Num);
        //        outStatingHangerProductItem.ProcessFlowName = outSiteResult.HangerProductFlowChart.FlowName;
        //        outStatingHangerProductItem.ProcessFlowId = outSiteResult.HangerProductFlowChart.FlowId;
        //        outStatingHangerProductItem.ProcessFlowCode = outSiteResult.HangerProductFlowChart.FlowCode;
        //        outStatingHangerProductItem.FlowChartd = outSiteResult.HangerProductFlowChart.ProcessChartId;
        //        outStatingHangerProductItem.IsReturnWorkFlow = (null != outSiteResult.HangerProductFlowChart.FlowType && outSiteResult.HangerProductFlowChart.FlowType.Value == 1);
        //        outStatingHangerProductItem.OutSiteDate = DateTime.Now;
        //        outHangerProductsItemModelList.Add(outStatingHangerProductItem);
        //        SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT).Add(statingHangerKey, outHangerProductsItemModelList);
        //    }
        //    else
        //    {
        //        var outHangerProductsItemModelList = dicHangerProductItemListExt[statingHangerKey];
        //        var outStatingHangerProductItem = BeanUitls<HangerProductItemModel, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
        //        outStatingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
        //        outStatingHangerProductItem.SizeNum = string.IsNullOrEmpty(outSiteResult.HangerProductFlowChart.Num) ? 0 : int.Parse(outSiteResult.HangerProductFlowChart.Num);
        //        outStatingHangerProductItem.ProcessFlowName = outSiteResult.HangerProductFlowChart.FlowName;
        //        outStatingHangerProductItem.ProcessFlowId = outSiteResult.HangerProductFlowChart.FlowId;
        //        outStatingHangerProductItem.ProcessFlowCode = outSiteResult.HangerProductFlowChart.FlowCode;
        //        outStatingHangerProductItem.FlowChartd = outSiteResult.HangerProductFlowChart.ProcessChartId;
        //        outStatingHangerProductItem.IsReturnWorkFlow = (null != outSiteResult.HangerProductFlowChart.FlowType && outSiteResult.HangerProductFlowChart.FlowType.Value == 1);
        //        outStatingHangerProductItem.OutSiteDate = DateTime.Now;
        //        outHangerProductsItemModelList.Add(outStatingHangerProductItem);
        //        SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT)[statingHangerKey] = outHangerProductsItemModelList;
        //    }

        //    var hanerProductItemListExt = dicHangerProductItemListExt[statingHangerKey];
        //    foreach (var hp in hanerProductItemListExt)
        //    {
        //        if (hp.SiteNo.Equals(outSiteResult.StatingNo) && hp.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber)
        //        {
        //            // hpItem = hp;
        //            var hangerProductItem = BeanUitls<HangerProductItem, HangerProductItemModel>.Mapper(hp);
        //            //HangerProductItemDao.Instance.Add(hangerProductItem);
        //            DapperHelp.Add(hangerProductItem);
        //        }
        //    }
        //    //lucifer/2018年10月12日--

        //    var pQueryService = new ProductsQueryServiceImpl();
        //    //pQueryService.CheckStatingIsLogin(StatingNo);
        //    var statingEmList = pQueryService.GetEmployeeLoginInfoList(outSiteResult.StatingNo, outSiteResult.MainTrackNumber);
        //    var emStatingInfo = statingEmList[0];

        //    var listDisc = new List<string>();
        //    HangerProductFlowChartModel hProductFlowChart = null;
        //    var dicHangerProductChartList = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //    if (dicHangerProductChartList.ContainsKey(outSiteResult.HangerNo))
        //    {
        //        var hangerProductChartList = dicHangerProductChartList[outSiteResult.HangerNo];
        //        hProductFlowChart = outSiteResult.HangerProductFlowChart;
        //        //存储站不记录产量
        //        var statingRole = hProductFlowChart.StatingRoleCode?.Trim();
        //        var storeStatingRoleCode = StatingType.StatingStorage.Code?.Trim();
        //        var isStoreStating = storeStatingRoleCode.Equals(statingRole);
        //        if (!isStoreStating)
        //        {
        //            hProductFlowChart.EmployeeName = emStatingInfo.RealName;
        //            hProductFlowChart.CardNo = emStatingInfo.CardNo;
        //            var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hProductFlowChart);
        //            DapperHelp.Add(inHangerFlowChart);
        //            RecordEmployeeProductOut(hProductFlowChart);

        //            MergeProcessFlow(hProductFlowChart, hangerProductChartList);

        //            var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
        //            statingHangerProductItem.Id = null;
        //            statingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
        //            statingHangerProductItem.IsReturnWorkFlow = null == hProductFlowChart?.FlowType ? false : (hProductFlowChart?.FlowType == 1 ? true : false);
        //            statingHangerProductItem.IsReworkSourceStating = hProductFlowChart?.IsReworkSourceStating;
        //            statingHangerProductItem.FlowChartd = hProductFlowChart.ProcessChartId;
        //            statingHangerProductItem.ProcessFlowId = hProductFlowChart.FlowId;
        //            statingHangerProductItem.ProcessFlowCode = hProductFlowChart.FlowCode;
        //            statingHangerProductItem.ProcessFlowName = hProductFlowChart.FlowName;
        //            statingHangerProductItem.Id = DapperHelp.Add(statingHangerProductItem);

        //            tcpLogInfo.Info("【非合并工序-->记录站点衣架生产记录】");
        //        }

        //        //foreach (var hpc in hangerProductChartList)
        //        //{
        //        //    if (hpc.StatingNo.Value == short.Parse(outSiteResult.StatingNo) && hpc.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber && hpc.Status.Value == HangerProductFlowChartStaus.Successed.Value)
        //        //    {
        //        //        var dics = string.Format("{0}{1}{2}", hpc.StatingNo.Value, hpc.Status.Value, hpc.MainTrackNumber.Value);
        //        //        if (!listDisc.Contains(dics))
        //        //        {
        //        //            hpc.EmployeeName = emStatingInfo.RealName;
        //        //            hpc.CardNo = emStatingInfo.CardNo;
        //        //            hProductFlowChart = hpc;
        //        //            var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChart>.Mapper(hProductFlowChart);
        //        //            //HangerProductFlowChartDao.Instance.Add(inHangerFlowChart);
        //        //            DapperHelp.Add(inHangerFlowChart);
        //        //            MergeProcessFlow(hProductFlowChart, hangerProductChartList);
        //        //            listDisc.Add(dics);
        //        //        }
        //        //    }
        //        //}
        //    }

        //    ////更新衣架工序生产明细的衣架出站时间
        //    //var sql = new StringBuilder("select top 1 * from HangerProductItem where HangerNo=? and SiteNo=? and MainTrackNumber=?");
        //    //var obj = QueryForObject<HangerProductItem>(sql, null, false, outSiteResult.HangerNo, outSiteResult.StatingNo, outSiteResult.MainTrackNumber);
        //    //if (null == obj)
        //    //{
        //    //    var ex = new NoFoundOnlineProductsException(string.Format("【更新衣架工序生产明细的衣架出站时间】 出错:找不到生产明细;主轨:{0} 站点:{1} 衣架号:{2}", outSiteResult.HangerNo, outSiteResult.StatingNo, outSiteResult.MainTrackNumber));
        //    //    tcpLogError.Error(ex);
        //    //    throw ex;
        //    //}
        //    //obj.OutSiteDate = DateTime.Now;
        //    //obj.IsSucessedFlow = true;
        //    //HangerProductItemDao.Instance.Update(obj);
        //    //log.Info("【更新衣架工序生产明细的衣架出站时间】");

        //    //if (null == outSiteResult.HangerProductFlowChart)
        //    //{
        //    //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", outSiteResult.MainTrackNumber, outSiteResult.HangerNo, outSiteResult.StatingNo));
        //    //    errorLog.Error("【衣架出站】", ex);
        //    //    return;
        //    //}
        //    //outSiteResult.HangerProductFlowChart.OutSiteDate = DateTime.Now;
        //    //outSiteResult.HangerProductFlowChart.FlowRealyProductStatingNo = short.Parse(outSiteResult.StatingNo);
        //    //outSiteResult.HangerProductFlowChart.IsFlowSucess = true;
        //    //outSiteResult.HangerProductFlowChart.Status = 2;//生产完成
        //    //HangerProductFlowChartDao.Instance.Update(outSiteResult.HangerProductFlowChart);

        //    //if (null != hpItem)
        //    //{
        //    //记录站点衣架生产记录
        //    // var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductItem>.Mapper(hpItem);

        //    //var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
        //    //statingHangerProductItem.Id = null;
        //    //statingHangerProductItem.IsReturnWorkFlow = null == hProductFlowChart?.FlowType ? false : (hProductFlowChart?.FlowType == 1 ? true : false);
        //    //statingHangerProductItem.IsReworkSourceStating = hProductFlowChart?.IsReworkSourceStating;
        //    //statingHangerProductItem.Id = StatingHangerProductItemDao.Instance.Insert(statingHangerProductItem);
        //    //log.Info("【记录站点衣架生产记录】");

        //    /* lucifer/2018年10月13日

        //    //检查工序是否完成，若完成则转移数据
        //    //注释于2018年9月24日 20:36:40
        //    var isFlowSucess = false;//CANProductsService.Instance.CheckHangerProcessChartIsSuccessed(outSiteResult.HangerNo);//CheckFlowIsSuccessed(obj.FlowChartd, (short)obj.FlowIndex);
        //    if (isFlowSucess)
        //    {

        //        var sucessMessage = string.Format("主轨:{0} 最后一站:{1} 衣架号:{2} 已生产完成!", outSiteResult.MainTrackNumber, outSiteResult.StatingNo, outSiteResult.HangerNo);
        //        tcpLogInfo.Info(sucessMessage);

        //        //var sqlWaitGen = new StringBuilder(@"select * from WaitProcessOrderHanger where HangerNo=?");
        //        //var data = QueryForObject<WaitProcessOrderHanger>(sqlWaitGen, null, false, outSiteResult.HangerNo);
        //        var sqlWaitGen = new StringBuilder(@"select * from WaitProcessOrderHanger where HangerNo=@HangerNo");
        //        var data = DapperHelp.FirstOrDefault<WaitProcessOrderHanger>(sqlWaitGen.ToString(), new { HangerNo = outSiteResult.HangerNo });

        //        var thread = new Thread(CANProductsService.Instance.CopySucessData);
        //        thread.Start(data);

        //        //记录员工产量

        //        //var refpModelSucess = new RecordEmployeeFlowProductionModel() { IsEndFlow = true, MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
        //        //var threadRecordEmployeeFlowProductionSucess = new Thread(new ThreadStart(refpModelSucess.RecordEmployeeFlowProduction));
        //        //threadRecordEmployeeFlowProductionSucess.Start();
        //        //记录员工产量
        //        RecordEmployeeProductOut(outSiteResult.StatingNo, statingHangerProductItem.Id, outSiteResult.MainTrackNumber, outSiteResult.HangerNo, true);
        //        return;
        //    }
        //    ////记录员工产量
        //    //var refpModel = new RecordEmployeeFlowProductionModel() { MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
        //    //var threadRecordEmployeeFlowProduction = new Thread(new ThreadStart(refpModel.RecordEmployeeFlowProduction));
        //    //threadRecordEmployeeFlowProduction.Start();

        //    //记录员工产量
        //    RecordEmployeeProductOut(outSiteResult.StatingNo, statingHangerProductItem.Id, outSiteResult.MainTrackNumber, outSiteResult.HangerNo, false);
        //}

        //*/

        //    ////更新站点出站时间
        //    //var sqlHangerStatingAll = new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=? and SiteNo=?");
        //    //var hangerStatingAllocationItem = QueryForObject<HangerStatingAllocationItem>(sqlHangerStatingAll, null, false, outSiteResult.HangerNo, outSiteResult.StatingNo);
        //    //if (null != hangerStatingAllocationItem)
        //    //{
        //    //    hangerStatingAllocationItem.OutSiteDate = DateTime.Now;
        //    //    hangerStatingAllocationItem.Status = 1;//更新完成
        //    //    HangerStatingAllocationItemDao.Instance.Update(hangerStatingAllocationItem);
        //    //}


        //    ////记录员工产量
        //    //var refpModel = new RecordEmployeeFlowProductionModel() { MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
        //    //var threadRecordEmployeeFlowProduction = new Thread(new ThreadStart(refpModel.RecordEmployeeFlowProduction));
        //    //threadRecordEmployeeFlowProduction.Start();

        //    //var wp = SusRedisClient.RedisTypeFactory.GetDictionary<string, WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER)[outSiteResult.HangerNo];
        //    //wp.ProcessFlowId = outSiteResult.pfcFlowRelation?.ProcessFlow?.Id;
        //    //wp.FlowNo = outSiteResult.pfcFlowRelation.FlowNo;
        //    //wp.ProcessFlowCode = outSiteResult.pfcFlowRelation.FlowCode;
        //    //wp.ProcessFlowName = outSiteResult.pfcFlowRelation.FlowName;
        //    //WaitProcessOrderHangerDao.Instance.Update(wp);

        //    //var hAllocationItem = BeanUitls<HangerStatingAllocationItem, WaitProcessOrderHanger>.Mapper(wp);
        //    //hAllocationItem.AllocatingStatingDate = DateTime.Now;
        //    //hAllocationItem.SiteNo = outSiteResult.StatingNo;
        //    ////hAllocationItem.HsaiNdex = pIndex;
        //    //hAllocationItem.Status = 0;
        //    //hAllocationItem.HangerType = outSiteResult.HangerProductFlowChart?.FlowType;
        //    //hAllocationItem.FlowNo = outSiteResult.HangerProductFlowChart.FlowNo;
        //    //hAllocationItem.MainTrackNumber = (short)outSiteResult.MainTrackNumber;
        //    //hAllocationItem.NextSiteNo = outSiteResult.NextStatingNo;
        //    //HangerStatingAllocationItemDao.Instance.Insert(hAllocationItem);
        //}
       
        ///// <summary>
        ///// 将合并工序标记为生产完成!
        ///// </summary>
        ///// <param name="currentHangerProductFlowChart"></param>
        ///// <param name="hangerProcessFlowChartList"></param>
        //private void MergeProcessFlow(HangerProductFlowChartModel currentHangerProductFlowChart, List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        //{
        //    try
        //    {
        //        //过滤已完成的同工序（连续返工）
        //        var filterFlowNoList = new List<string>();
        //        var meregeProcessFlowChartList = hangerProcessFlowChartList.Where(f =>
        //        (null != f.IsMergeForward && f.IsMergeForward.Value)
        //        && (currentHangerProductFlowChart.ProcessFlowChartFlowRelationId.Equals(f.MergeProcessFlowChartFlowRelationId))
        //        && currentHangerProductFlowChart.FlowType.Value == f.FlowType.Value
        //        && currentHangerProductFlowChart.StatingNo.Value != f.StatingNo.Value
        //        && f.StatingNo.Value == -1
        //        );
        //        foreach (var hpfc in meregeProcessFlowChartList)
        //        {
        //            if (!filterFlowNoList.Contains(hpfc.FlowNo?.Trim()))
        //            {
        //                filterFlowNoList.Add(hpfc.FlowNo?.Trim());
        //                hpfc.ProcessOrderNo = currentHangerProductFlowChart.ProcessOrderNo;
        //                hpfc.PColor = currentHangerProductFlowChart.PColor;
        //                hpfc.PSize = currentHangerProductFlowChart.PSize;
        //                hpfc.Po = currentHangerProductFlowChart.Po;
        //                hpfc.StatingCapacity = currentHangerProductFlowChart.StatingCapacity;

        //                hpfc.MainTrackNumber = currentHangerProductFlowChart.MainTrackNumber;
        //                hpfc.IncomeSiteDate = currentHangerProductFlowChart.IncomeSiteDate;
        //                hpfc.CompareDate = currentHangerProductFlowChart.CompareDate;
        //                hpfc.OutSiteDate = currentHangerProductFlowChart.OutSiteDate;
        //                hpfc.StatingNo = currentHangerProductFlowChart.StatingNo;
        //                hpfc.StatingId = currentHangerProductFlowChart.StatingId;
        //                hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
        //                hpfc.FlowType = currentHangerProductFlowChart.FlowType;
        //                hpfc.IsFlowSucess = true;
        //                hpfc.EmployeeName = currentHangerProductFlowChart.EmployeeName;
        //                hpfc.CardNo = currentHangerProductFlowChart.CardNo;
        //                var meHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hpfc);
        //                DapperHelp.Add(meHangerFlowChart);

        //                RecordEmployeeProductOut(hpfc);

        //                var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(hpfc);
        //                statingHangerProductItem.Id = null;
        //                statingHangerProductItem.SiteNo = currentHangerProductFlowChart.StatingNo?.ToString();
        //                statingHangerProductItem.IsReturnWorkFlow = null == currentHangerProductFlowChart?.FlowType ? false : (currentHangerProductFlowChart?.FlowType == 1 ? true : false);
        //                statingHangerProductItem.IsReworkSourceStating = currentHangerProductFlowChart?.IsReworkSourceStating;
        //                statingHangerProductItem.FlowChartd = currentHangerProductFlowChart.ProcessChartId;
        //                statingHangerProductItem.ProcessFlowId = currentHangerProductFlowChart.FlowId;
        //                statingHangerProductItem.ProcessFlowCode = currentHangerProductFlowChart.FlowCode;
        //                statingHangerProductItem.ProcessFlowName = currentHangerProductFlowChart.FlowName;
        //                statingHangerProductItem.Id = DapperHelp.Add(statingHangerProductItem);
        //                tcpLogInfo.Info("【合并工序--->记录站点衣架生产记录】");
        //            }
        //            ////记录分配工序
        //            ////var meregeProcessFlow = new HangerStatingAllocationItem();
        //            //var meregeProcessFlow = BeanUitls<HangerStatingAllocationItem, HangerProductFlowChart>.Mapper(meHangerFlowChart);
        //            //meregeProcessFlow.Memo = "合并工序";
        //            //meregeProcessFlow.Status = 1;
        //            //meregeProcessFlow.HangerType = 0;
        //            //meregeProcessFlow.IsSucessedFlow = true;
        //            //meregeProcessFlow.SiteNo = meHangerFlowChart.StatingNo?.ToString();
        //            //DapperHelp.Add(meHangerFlowChart);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        redisLog.ErrorFormat("合并工序产量记录异常:{0}", ex);
        //    }
        //}

        //void RecordEmployeeProductOut(HangerProductFlowChartModel hpfModole, bool isEndFlow = false)
        //{

        //    var pQueryService = new ProductsQueryServiceImpl();
        //    //pQueryService.CheckStatingIsLogin(StatingNo);
        //    var statingEmList = pQueryService.GetEmployeeLoginInfoList(hpfModole.StatingNo.Value.ToString(),hpfModole.MainTrackNumber.Value);
        //    var emStatingInfo = statingEmList.Count > 0 ? statingEmList[0] : null;
        //    //var hsaItem = StatingHangerProductItemDao.Instance.GetById(statingHangerProductItemId);
        //    var efpModel = BeanUitls<EmployeeFlowProduction, HangerProductFlowChartModel>.Mapper(hpfModole);
        //    efpModel.Id = null;
        //    efpModel.SizeNum = null != hpfModole.Num ? int.Parse(hpfModole.Num) : 0;
        //    efpModel.ProcessFlowId = hpfModole.FlowId;
        //    efpModel.ProcessFlowCode = hpfModole.FlowCode;
        //    efpModel.ProcessFlowName = hpfModole.FlowName;
        //    efpModel.HangerType = hpfModole.FlowType;
        //    efpModel.FlowChartd = hpfModole.ProcessChartId;
        //    efpModel.SiteNo = "" + hpfModole.StatingNo.Value;
        //    efpModel.CardNo = hpfModole.CardNo?.Trim();
        //    efpModel.EmployeeId = emStatingInfo?.EmployeeId;
        //    efpModel.EmployeeName = emStatingInfo?.RealName?.Trim();
        //    //EmployeeFlowProductionDao.Instance.Insert(efpModel);
        //    DapperHelp.Add(efpModel);

        //    //var ppChart = CANProductsService.Instance.GetCompleteHangerProductFlowChart(mainTrackNumber, hangerNo, statingNo);
        //    //if (null == ppChart)
        //    //{
        //    //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", mainTrackNumber, hangerNo, statingNo));
        //    //    errorLog.Error("【衣架出站】", ex);
        //    //    return;
        //    //}
        //    //ppChart.EmployeeName = emStatingInfo.RealName;
        //    //ppChart.CardNo = emStatingInfo.CardNo;
        //    ////HangerProductFlowChartDao.Instance.Edit(ppChart);
        //    //DapperHelp.Edit(ppChart);

        //    if (isEndFlow)
        //    {
        //      //  CANProductsService.Instance.CopyHangerProductChart(hpfModole.HangerNo);
        //    }
        //}
        //private void HangerInSiteAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    //var inResult = JsonConvert.DeserializeObject<HangerInSiteResult>(arg2);

        //    ////将生产中的制品工序加1
        //    //var sql = new StringBuilder("select * from WaitProcessOrderHanger where HangerNo=?");
        //    //var waitProcessOrderHanger = QueryForObject<WaitProcessOrderHanger>(sql, null, false, inResult.HangerNo); //WaitProcessOrderHangerDao.Instance.GetAll().Where(f => f.HangerNo.Equals(hangerNo));
        //    //waitProcessOrderHanger.IsIncomeSite = true;
        //    //waitProcessOrderHanger.FlowIndex++;
        //    //WaitProcessOrderHangerDao.Instance.Update(waitProcessOrderHanger);

        //    ////记录衣架生产的工序
        //    //ProductsQueryServiceImpl productsQueryService = new ProductsQueryServiceImpl();
        //    //var pIndex = productsQueryService.GetNextIndex(inResult.HangerNo, "HangerProductItem");
        //    //var hangerProductItem = BeanUitls<HangerProductItem, WaitProcessOrderHanger>.Mapper(waitProcessOrderHanger);
        //    //hangerProductItem.Id = null;
        //    //hangerProductItem.IncomeSiteDate = DateTime.Now;
        //    //hangerProductItem.SiteNo = inResult.StatingNo?.Trim();
        //    //hangerProductItem.HpIndex = pIndex;
        //    //hangerProductItem.MainTrackNumber = short.Parse(inResult.StatingNo);
        //    //hangerProductItem.FlowIndex = waitProcessOrderHanger.FlowIndex;
        //    //HangerProductItemDao.Instance.Save(hangerProductItem);

        //    ////更新站点进站时间
        //    //var sqlHangerStatingAll = new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=? and NextSiteNo=?");
        //    //var hangerStatingAllocationItem = QueryForObject<HangerStatingAllocationItem>(sqlHangerStatingAll, null, false, inResult.HangerNo, inResult.StatingNo);
        //    //hangerStatingAllocationItem.IncomeSiteDate = DateTime.Now;
        //    //hangerStatingAllocationItem.Status = 1;//将分配关系标记为已处理
        //    //HangerStatingAllocationItemDao.Instance.Update(hangerStatingAllocationItem);

        //    ////更新衣架生产工艺图制作站点的信息
        //    //var ppChart = CANProductsService.Instance.GetHangerProductFlowChart(inResult.MainTrackNumber, inResult.HangerNo, inResult.StatingNo);
        //    //if (null == ppChart)
        //    //{
        //    //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", inResult.MainTrackNumber, inResult.HangerNo, inResult.StatingNo));
        //    //    errorLog.Error("【衣架进站】", ex);
        //    //    return;
        //    //}
        //    //ppChart.IncomeSiteDate = DateTime.Now;
        //    //ppChart.Status = 1;
        //    //HangerProductFlowChartDao.Instance.Update(ppChart);
        //}

        ////private void UpdateHangerProductItemAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var hangerProductItem = Newtonsoft.Json.JsonConvert.DeserializeObject<HangerProductItem>(arg2);
        //    //HangerProductItemDao.Instance.Edit(hangerProductItem);
        //    DapperHelp.Edit(hangerProductItem);
        //}

        //private void UpdateWaitProcessOrderHangerAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var wp = Newtonsoft.Json.JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
        //    //WaitProcessOrderHangerDao.Instance.Edit(wp);
        //    DapperHelp.Edit(wp);
        //}

        //private void RecordStatingHangerProductItemAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var statingHangerProductItem = JsonConvert.DeserializeObject<StatingHangerProductItem>(arg2);
        //    //StatingHangerProductItemDao.Instance.Insert(statingHangerProductItem);
        //    DapperHelp.Add(statingHangerProductItem);
        //}
        //private void UpdateHangerProcessFlowChartAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var hangerChart = JsonConvert.DeserializeObject<HangerProductFlowChart>(arg2);
        //    //HangerProductFlowChartDao.Instance.Edit(hangerChart);
        //    DapperHelp.Edit(hangerChart);
        //}

        //private void HangerAllocationAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var hsaItemNext = Newtonsoft.Json.JsonConvert.DeserializeObject<HangerStatingAllocationItem>(arg2);
        //    //HangerStatingAllocationItemDao.Instance.Insert(hsaItemNext);
        //    DapperHelp.Add(hsaItemNext);

        //}

        //private void HangerProcessFlowChartAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var hpfcList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HangerProductFlowChart>>(arg2);
        //    foreach (var hpfc in hpfcList)
        //    {
        //        //HangerProductFlowChartDao.Instance.Insert(hpfc);
        //        DapperHelp.Add(hpfc);
        //    }
        //}

        //private void WaitProcessOrderHangerAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var wp = Newtonsoft.Json.JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
        //    //WaitProcessOrderHangerDao.Instance.Insert(wp);
        //    //log.Info("WaitProcessOrderHangerAction..................");
        //    DapperHelp.Add(wp);
        //    //log.Info("WaitProcessOrderHangerAction..................1111");
        //}

        //private void StatingCapacityEdit(RedisChannel arg1, RedisValue arg2)
        //{
        //    var StatingModel = JsonConvert.DeserializeObject<StatingModel>(arg2);
        //    if (StatingModel != null)
        //    {
        //        var dic = SusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
        //        var key = string.Format("{0}:{1}", StatingModel.MainTrackNumber.Value, StatingModel.StatingNo?.Trim());

        //        //更新数据库
        //        //string sql = "update Stating set Capacity = ? where MainTrackNumber = ? and StatingNo = ?";
        //        //int effCount = ProductionLineSetServiceImpl.Instance.ExecuteUpdate(sql, StatingModel.Capacity, StatingModel.MainTrackNumber, StatingModel.StatingNo);

        //        string sql = "update Stating set Capacity = @Capacity where MainTrackNumber = @MainTrackNumber and StatingNo = @StatingNo";
        //        int effCount = DapperHelp.Execute(sql, StatingModel);
        //        if (effCount > 0)
        //        {
        //            //更新Redis缓存
        //            if (dic.ContainsKey(key))
        //            {
        //                dic[key].Capacity = StatingModel.Capacity;
        //            }
        //            if(CANTcp.client!=null)
        //            //发送成功指令
        //                CANTcp.client.SCMModifyStatingCapacitySuccess(StatingModel.MainTrackNumber.Value, StatingModel.StatingNo, StatingModel.Capacity.Value);
        //            if (null != CANTcpServer.server) {
        //                CANTcpServer.server.SCMModifyStatingCapacitySuccess(StatingModel.MainTrackNumber.Value, StatingModel.StatingNo, StatingModel.Capacity.Value);
        //            }
        //        }
        //    }
        //}

        //private void UpdateStatingTypeAction(RedisChannel arg1, RedisValue arg2)
        //{

        //}

        //private void ProcessStatingAllocationLog(RedisChannel arg1, RedisValue arg2)
        //{
        //    //var SystemLogs = JsonConvert.DeserializeObject<SystemLogs>(arg2);
        //    SystemLogs log = new SystemLogs()
        //    {
        //        LogInfo = arg2,
        //        Name = SystemLogEnum.Allocation.ToString(),
        //    };

        //    SystemLogService.Instance.AddLogs(log);
        //}


        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            //内部异常
            //throw new NotImplementedException();
            log.Error(string.Format("内部错误"), e.Exception);
        }

        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            log.Info("新集群：" + e.NewEndPoint + "旧集群：" + e.OldEndPoint);
        }

        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            log.Info("配置更改：" + e.EndPoint);
        }

        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            log.Info("异常信息：" + e.Message);
        }

        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            log.Info("重连错误" + e.EndPoint);
        }

        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            log.Info("连接异常" + e.EndPoint + "，类型为" + e.FailureType + (e.Exception == null ? "" : ("，异常信息是" + e.Exception.Message)));
        }
    }
}

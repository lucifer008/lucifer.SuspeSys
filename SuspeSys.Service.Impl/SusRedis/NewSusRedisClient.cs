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
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Utils;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Service.Impl.Core.MonitorUpload;
using SuspeSys.AuxiliaryTools;
using SuspeSys.Service.Impl.Core.Cache;
using System.Threading;

namespace SuspeSys.Service.Impl.SusRedis
{
    /// <summary>
    /// Redis
    /// </summary>
    public class NewSusRedisClient : ServiceBase
    {
        public readonly static NewSusRedisClient Instance = new NewSusRedisClient();
        new protected static readonly ILog log = LogManager.GetLogger("RedisLogInfo");
        private static object loObject = new object();
        public static RedisTypeFactory RedisTypeFactory
        {
            get
            {
                bool lockWasTaken = false;
                var temp = loObject;
                try
                {
                    Monitor.Enter(temp, ref lockWasTaken);
                    return redisTypeFactory;
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
            }
        }
        private static RedisTypeFactory redisTypeFactory = null;
        //public static RedisTypeFactory BaseRedisTypeFactory = null;
        public static ConnectionMultiplexer ConnectionMultiplexer = null;
        public static ISubscriber subcriber = null;
        public static readonly string url = System.Configuration.ConfigurationManager.AppSettings["RedisIp"]; //"localhost";
        public static readonly int portNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RedisPort"]);
        private readonly static object locObje = new object();

        //public static NewSusRedisClient Instance2
        //{
        //    get { return new NewSusRedisClient(); }
        //}
        private NewSusRedisClient() { }
        public void Init(bool isServiceStart = false)
        {
            try
            {
                if (null == redisTypeFactory)
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
                    redisTypeFactory = new RedisTypeFactory(ConnectionMultiplexer);
                    //  BaseRedisTypeFactory = new RedisTypeFactory(ConnectionMultiplexer);
                    //var redisDictionary = RedisTypeFactory.GetDictionary<int, HangerProductFlowChart>("HangerProductFlowChart");
                    ////redisDictionary.Add(1, new HangerProductFlowChart() { HangerNo = "123" });
                    ////redisDictionary.Add(2, new HangerProductFlowChart() { HangerNo = "124" });
                    ////redisDictionary.Add(3, new HangerProductFlowChart() { HangerNo = "125" });
                    //foreach (var hpfc in redisDictionary)
                    //{
                    //    Console.WriteLine("HangerNo: {0}", hpfc.Value.HangerNo);
                    //}

                    //订阅消息:消息对所有订阅的客户端都有效
                    subcriber = ConnectionMultiplexer.GetSubscriber();
                    //subcriber.Subscribe(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, (channel, message) => {
                    //    Console.WriteLine((string)message);
                    //});
                    subcriber.Subscribe(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, WaitProcessOrderHangerAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_PROCESS_FLOW_CHART, HangerProcessFlowChartAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, HangerAllocationAction);
                    subcriber.Subscribe(SusRedisConst.UPDATE_HANGER_PROCESS_FLOW_CHART_ACTION, UpdateHangerProcessFlowChartAction);
                    subcriber.Subscribe(SusRedisConst.RECORD_STATING_HANGER_PROCESS_ITEM_ACTION, RecordStatingHangerProductItemAction);
                    subcriber.Subscribe(SusRedisConst.UPDATE_WAIT_PROCESS_ORDER_HANGER_ACTION, UpdateWaitProcessOrderHangerAction);

                    subcriber.Subscribe(SusRedisConst.UPDATE_HANGER_PRODUCT_ITEM_ACTION, UpdateHangerProductItemAction);

                    subcriber.Subscribe(SusRedisConst.HANGER_IN_SITE_ACTION, HangerInSiteAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_OUT_SITE_ACTION, HangerOutSiteAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_RESUME_ACTION, HangerResumeAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_AOLLOCATION_ACTION, HangerAollcationAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_OUT_HANGING_STATION_ACTION, HangingStationOutSiteAction);
                    subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_STATUS, MainTrackStatingStatusAction);
                    //subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, UpdateMainTrackStatingAllocationNumAction);
                    //subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_IN_NUM, UpdateMainTrackStatingInNumAction);
                    subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM, UpdateMainTrackStatingOnlineNumAction);
                    subcriber.Subscribe(SusRedisConst.MAINTRACK_STATING_MONITOR_ACTION, MainTrackStatingMontorUploadAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_REWORK_REQUEST_QUEUE_ACTION, HangerReworkReuestQueueAction);
                    subcriber.Subscribe(SusRedisConst.STATING_EDIT_ACTION, StatingCapacityEdit);
                    // subcriber.Subscribe(SusRedisConst.STATING_EMPLOYEE_ACTION, StatingEmployeeAction);
                    //  subcriber.Subscribe(SusRedisConst.EMPLOYEE_CARD_RELATION_ACTION, EmployeeCardRelationAction);
                    subcriber.Subscribe(SusRedisConst.PRODUCT_SUCCESS_COPY_DATA_ACTION, ProductSuccessCopyDataAction);
                    subcriber.Subscribe(SusRedisConst.STATING_ALLOCATION_SAVE_CHANGE_ACTION, ProcessStatingAllocationLog);
                    // subcriber.Subscribe(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, HangerProductsChartResumeAction);
                    subcriber.Subscribe(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_DB_ACTION, HangerProductResumeDBAction);
                    subcriber.Subscribe(SusRedisConst.PUBLIC_TEST, PublicTestAction);
                    subcriber.Subscribe(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, CurrentHangerProductinAction);
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
        /// 分配数维护
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void UpdateMainTrackStatingAllocationNumAction(RedisChannel arg1, RedisValue arg2)
        {
            lock (locObje)
            {
                var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);
                var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
                var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);
                if (!dic.ContainsKey(key))
                {
                    if (mts.AllocationNum > 0)
                        NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM).Add(key, 1);
                }
                else
                {
                    var num = dic[key];
                    if (num == 0)
                    {
                        if (mts.AllocationNum > 0)
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num + 1;
                        }
                    }
                    else
                    {
                        if (mts.AllocationNum < 0)
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num - 1;
                        }
                        else
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM)[key] = num + 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 非常规站内数及站内数明细，硬件缓存修正
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void HangerStatingOrAllocationAction(RedisChannel arg1, RedisValue arg2)
        {
            lock (locObje)
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
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            hNonStandModel.HangerProductFlowChartModel.HangerStatus = 0;
                            hNonStandModel.HangerProductFlowChartModel.MainTrackNumber = (short)hNonStandModel.MainTrackNumber;
                            hNonStandModel.HangerProductFlowChartModel.StatingNo = (short)hNonStandModel.StatingNo;
                            dicHangerStatingInfo.Add(hangerNo, new List<HangerProductFlowChartModel>() { hNonStandModel.HangerProductFlowChartModel });
                            //return;
                            //站点分配数+1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        else
                        {
                            var chList = dicHangerStatingInfo[hangerNo];
                            var isExist = chList.Where(f => ((null != f.FlowNo && f.FlowNo.Equals(hNonStandModel.FlowNo)) || (string.IsNullOrEmpty(hNonStandModel.FlowNo) && hNonStandModel.IsBridgeAllocation)) && f.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                                && f.StatingNo.Value == hNonStandModel.StatingNo).Count() > 0;
                            if (isExist)
                            {
                                chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                                {
                                    if (((hpfc.FlowNo.Equals(hNonStandModel.FlowNo)) || (string.IsNullOrEmpty(hNonStandModel.FlowNo) && hNonStandModel.IsBridgeAllocation))
                                    && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
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
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                            //站点分配数+1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }

                        #region 注釋掉的代碼
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

                        #endregion
                    }
                    //衣架进站
                    else if (hNonStandModel.Action == 1)
                    {
                        #region 衣架进站
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配进站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        //携带工序的情况处理
                        var onlineNums = chList.Where(hpfc => null != hpfc.FlowNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 0)).Count();
                        for (var index = 0; index < onlineNums; index++)
                        {
                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        //携带工序的情况处理
                        chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            if ((null != hpfc.FlowNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo)) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                            )
                            {
                                hpfc.HangerStatus = 1;
                            }
                        });

                        //不携带工序的情况处理

                        var onlineNumsSpeci = chList.Where(hpfc => string.IsNullOrEmpty(hpfc.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 0)).Count();
                        for (var index = 0; index < onlineNumsSpeci; index++)
                        {
                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        //不携带工序的情况处理
                        chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            if (string.IsNullOrEmpty(hpfc.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0
                            )
                            {
                                hpfc.HangerStatus = 1;
                            }
                        });

                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        ////站点分配数-1
                        //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));

                        ////站内数+1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));


                        //携带工序的情况处理
                        var statingNums = chList.Where(hpfc => null != hpfc.FlowNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();

                        for (var index = 0; index < statingNums; index++)
                        {
                            //站内数+1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }

                        //不携带工序的情况处理
                        var statingNumsSpeci = chList.Where(hpfc => string.IsNullOrEmpty(hpfc.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();

                        for (var index = 0; index < statingNumsSpeci; index++)
                        {
                            //站内数+1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                        #endregion
                    }
                    //桥接不携带工序衣架进站
                    else if (hNonStandModel.Action == 11)
                    {
                        #region 衣架进站
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配进站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            if (null != hpfc.MainTrackNumber && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && null != hpfc.StatingNo
                            && hpfc.StatingNo.Value == hNonStandModel.StatingNo && null != hpfc.FlowIndex && hpfc.FlowIndex.Value == -1 && hpfc.HangerStatus == 0
                            )
                            {
                                hpfc.HangerStatus = 1;
                            }
                        });
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        //站点分配数-1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        //站内数+1
                        var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        #endregion
                    }
                    //衣架出战
                    else if (hNonStandModel.Action == 2)
                    {
                        #region 衣架出战
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }

                        var chList = dicHangerStatingInfo[hangerNo];
                        var onlineNums = chList.Where(hpfc => null != hpfc.FlowNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo)
                        && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0).Count();
                        for (var index = 0; index < onlineNums; index++)
                        {
                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        var statingNums = chList.Where(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();

                        for (var index = 0; index < statingNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                        chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            if (null != hpfc.FlowNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                            )
                            {
                                hpfc.HangerStatus = 2;
                            }
                        });
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        #endregion
                    }
                    //站点删除
                    else if (hNonStandModel.Action == 3)
                    {
                        #region 站点删除
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        chList.Remove(chList.Where(p => p.FlowNo.Equals(hNonStandModel.FlowNo) && p.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && p.StatingNo.Value == hNonStandModel.StatingNo).FirstOrDefault());
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                        #endregion
                    }
                    //工序删除
                    else if (hNonStandModel.Action == 4)
                    {
                        #region 工序删除
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
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
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        });
                        chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        #endregion
                    }
                    //衣架返工
                    else if (hNonStandModel.Action == 6)
                    {
                        #region 衣架返工
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        var statingNums = chList.Where(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();
                        var onlineNums = chList.Where(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 0)).Count();
                        for (var index = 0; index < onlineNums; index++)
                        {
                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        for (var index = 0; index < statingNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }

                        chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        {
                            if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && hpfc.StatingNo.Value == hNonStandModel.StatingNo
                            )
                            {
                                hpfc.HangerStatus = 4;
                            }
                        });
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));

                        #endregion
                    }
                    //过监测点已分配或者已进站修正
                    else if (hNonStandModel.Action == 7)
                    {
                        #region 过监测点已分配或者已进站修正
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
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

                                //CANTcp.client.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                                CANTcpServer.server.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);

                                logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存结束【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                                montorLog.Info(logMessage);

                                //站点分配数-1
                                var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, AllocationNum = -1 };
                                // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                                UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                            }
                            else if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.HangerStatus == 1)
                            {
                                hpfc.HangerStatus = 6;
                                var logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存开始【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                                montorLog.Info(logMessage);

                                //CANTcp.client.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                                CANTcpServer.server.ClearHangerCache(hpfc.MainTrackNumber.Value, hpfc.StatingNo.Value, int.Parse(hpfc.HangerNo), SuspeConstants.XOR);
                                logMessage = string.Format("【监测点修正】衣架号:{0} 主轨:{1} 监测-->清除已分配缓存结束【已分配站点:{2}】!", hpfc?.HangerNo, hpfc?.MainTrackNumber, hpfc?.StatingNo);
                                montorLog.Info(logMessage);

                                //站内数-1
                                var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, OnLineSum = -1 };
                                // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                                UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                            }
                        });
                        chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo) && (data.HangerStatus == 5 || data.HangerStatus == 6));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                        #endregion
                    }
                    //工序移动及站点移动
                    else if (hNonStandModel.Action == 8)
                    {
                        #region 工序移动及站点移动--->站点删除
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【工序移动及站点移动】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        //chList.Where(data => data.FlowNo.Equals(hNonStandModel.FlowNo)).ForEach(delegate (HangerProductFlowChartModel hpc)
                        //{
                        //    //站内数-1
                        //    var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpc.MainTrackNumber.Value, StatingNo = hpc.StatingNo.Value, OnLineSum = -1 };
                        //    NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        //});
                        chList.Remove(chList.Where(p => p.FlowNo.Equals(hNonStandModel.FlowNo) && p.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                            && p.StatingNo.Value == hNonStandModel.StatingNo).FirstOrDefault());
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                        #endregion
                    }
                    //桥接站出战逆向桥接站内数修正(逆向站点无携带工序)
                    else if (hNonStandModel.Action == 9)
                    {
                        #region 出战--->桥接站出战逆向桥接站内数修正(逆向站点无携带工序)
                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        var chList = dicHangerStatingInfo[hangerNo];
                        var statingNums = chList.Where(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();

                        for (var index = 0; index < statingNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                        #endregion

                    }
                    else if (hNonStandModel.Action == 10)//桥接出战逆向桥接站内数修正(逆向站点携带工序)
                    {
                        #region 出战--->桥接出战逆向桥接站内数修正(逆向站点携带工序)
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));

                        var chList = dicHangerStatingInfo[hangerNo];

                        var statingNums = chList.Where(hpfc => hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && (hpfc.HangerStatus == 1)).Count();

                        for (var index = 0; index < statingNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                        //chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                        //{
                        //    if (hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                        //    && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.FlowIndex.Value == -1
                        //    )
                        //    {
                        //        hpfc.HangerStatus = 1;
                        //    }
                        //});
                        //SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;
                        #endregion
                    }
                    //桥接不携带工序出战
                    else if (hNonStandModel.Action == 12)
                    {
                        #region 衣架出战--->桥接不携带工序出战
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        // var chList = dicHangerStatingInfo[hangerNo];
                        var incomeNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 1);
                        var onlineNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0);
                        // chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        for (var index = 0; index < incomeNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }

                        for (var index = 0; index < onlineNums; index++)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            //// NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        #endregion
                    }

                    #region 注釋掉的
                    ////桥接携带工序出战，反向工序未完成且是桥接携带工序,清除站内数
                    //else if (hNonStandModel.Action == 15)
                    //{
                    //    #region 衣架出战
                    //    var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                    //    if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                    //    {
                    //        tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                    //        return;
                    //    }
                    //    var chList = dicHangerStatingInfo[hangerNo];
                    //    chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                    //    {
                    //        if (hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                    //        && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.FlowNo.Equals(hNonStandModel.FlowNo) && (hpfc.HangerStatus == 0 || hpfc.HangerStatus == 1)
                    //        )
                    //        {
                    //            hpfc.HangerStatus = 2;
                    //        }
                    //    });
                    //    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                    //    ////站内数-1
                    //    //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                    //    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    //    #endregion
                    //}
                    #endregion

                    //桥接携带工序且已完成，后又分配
                    else if (hNonStandModel.Action == 13)
                    {
                        #region 衣架分配--->桥接携带工序且已完成，后又分配
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        hNonStandModel.HangerProductFlowChartModel.MainTrackNumber = (short)hNonStandModel.MainTrackNumber;
                        hNonStandModel.HangerProductFlowChartModel.StatingNo = (short)hNonStandModel.StatingNo;
                        hNonStandModel.HangerProductFlowChartModel.AllocationedDate = DateTime.Now; ;
                        hNonStandModel.HangerProductFlowChartModel.HangerStatus = 0;
                        chList.Add(hNonStandModel.HangerProductFlowChartModel);

                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        //站点分配数+1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        #endregion
                    }
                    //桥接站出战 清除桥接反向站内数
                    else if (hNonStandModel.Action == 14)
                    {
                        #region 衣架出战-->桥接站出战 清除桥接反向站内数
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        var incomeNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 1);
                        var onlineNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0);
                        // chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        for (var index = 0; index < incomeNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }

                        for (var index = 0; index < onlineNums; index++)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        #endregion
                    }
                    //桥接站出战 清除桥接反向站内数
                    else if (hNonStandModel.Action == 15)
                    {
                        #region 衣架出战-->桥接站出战 清除桥接反向站内数
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配出站!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        //清除站内数及分配数
                        var incomeNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 1);
                        var onlineNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0);
                        // chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        ////站内数-1
                        //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                        //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                        for (var index = 0; index < incomeNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }
                        for (var index = 0; index < onlineNums; index++)
                        {
                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        #endregion
                    }
                    else if (hNonStandModel.Action == -2)//衣架回流分配
                    {
                        //衣架回流分配

                        #region 衣架回流分配
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            var hangerStatingInfoList = dicHangerStatingInfo[hangerNo];
                            var hStatingInfo = BeanUitls<HangerProductFlowChartModel, HangerProductFlowChartModel>.Mapper(hangerStatingInfoList[0]);
                            hStatingInfo.HangerStatus = 0;
                            hStatingInfo.MainTrackNumber = (short)mainTrackNumber;
                            hStatingInfo.StatingNo = (short)statingNo;
                            hStatingInfo.FlowIndex = -2;
                            hStatingInfo.FlowNo = string.Empty;
                            hStatingInfo.FlowCode = string.Empty;
                            hStatingInfo.FlowName = "挂片";
                            hangerStatingInfoList.Add(hStatingInfo);
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = hangerStatingInfoList;
                            //return;
                            //站点分配数+1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        #endregion

                    }
                    else if (hNonStandModel.Action == -3)//衣架回流进站
                    {
                        //衣架回流进站

                        #region 衣架回流进站
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                            {
                                tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配进站!", hangerNo, mainTrackNumber, statingNo);
                                return;
                            }
                            var allocatCount = 0;
                            var chList = dicHangerStatingInfo[hangerNo];
                            chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                            {
                                if (hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                                && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.FlowIndex.Value == -2 && hpfc.HangerStatus == 0)
                                {
                                    hpfc.HangerStatus = 1;
                                    allocatCount++;
                                }
                            });

                            for (var item = 0; item < allocatCount; item++)
                            {
                                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                                //站点分配数-1
                                var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                                //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                                UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                                //站内数+1
                                var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = 1 };
                                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                                UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                            }

                        }
                        #endregion
                    }
                    else if (hNonStandModel.Action == -4)
                    {
                        //衣架回流进站

                        #region 衣架回流未进站监测点再分配
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                            {
                                tcpLogInfo.InfoFormat("【站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配进站!", hangerNo, mainTrackNumber, statingNo);
                                return;
                            }
                            var allocatCount = 0;
                            var chList = dicHangerStatingInfo[hangerNo];
                            chList.ForEach(delegate (HangerProductFlowChartModel hpfc)
                            {
                                if (hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                                && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.FlowIndex.Value == -2 && hpfc.HangerStatus == 0)
                                {
                                    //hpfc.HangerStatus = 1;
                                    allocatCount++;
                                }
                            });
                            //移除分配明细
                            chList.RemoveAll(f => f.MainTrackNumber.Value == hNonStandModel.MainTrackNumber
                                && f.StatingNo.Value == hNonStandModel.StatingNo && f.FlowIndex.Value == -2 && f.HangerStatus == 0);
                            for (var item = 0; item < allocatCount; item++)
                            {
                                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                                //站点分配数-1
                                var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                                //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                                UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));

                            }

                        }
                        #endregion
                    }
                    //F2指定分配清除当前站内数
                    else if (hNonStandModel.Action == 16)
                    {
                        #region F2指定分配清除当前站内数
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【F2指定站内数及明细修正】 衣架:{0} 主轨:{1} 站点:{2}  站内数修正!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        var incomeNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 1);
                        var onlineNums = chList.RemoveAll(hpfc => hpfc.MainTrackNumber.Value == hNonStandModel.MainTrackNumber && hpfc.StatingNo.Value == hNonStandModel.StatingNo && hpfc.HangerStatus == 0);
                        // chList.RemoveAll(data => data.FlowNo.Equals(hNonStandModel.FlowNo));
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        for (var index = 0; index < incomeNums; index++)
                        {
                            //站内数-1
                            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, OnLineSum = -1 };
                            //  NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                            UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
                        }

                        for (var index = 0; index < onlineNums; index++)
                        {

                            //站点分配数-1
                            var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = -1 };
                            // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                            UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        }
                        #endregion
                    }
                    //F2指定分配
                    else if (hNonStandModel.Action == 17)
                    {
                        #region 衣架分配--->F2指定分配
                        var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
                        if (!dicHangerStatingInfo.ContainsKey(hangerNo))
                        {
                            tcpLogInfo.InfoFormat("【F2指定分配】 衣架:{0} 主轨:{1} 站点:{2}  衣架未分配!", hangerNo, mainTrackNumber, statingNo);
                            return;
                        }
                        var chList = dicHangerStatingInfo[hangerNo];
                        hNonStandModel.HangerProductFlowChartModel.MainTrackNumber = (short)hNonStandModel.MainTrackNumber;
                        hNonStandModel.HangerProductFlowChartModel.StatingNo = (short)hNonStandModel.StatingNo;
                        hNonStandModel.HangerProductFlowChartModel.AllocationedDate = DateTime.Now;
                        hNonStandModel.HangerProductFlowChartModel.HangerStatus = 0;
                        hNonStandModel.HangerProductFlowChartModel.FlowNo = hNonStandModel.FlowNo;
                        hNonStandModel.HangerProductFlowChartModel.FlowIndex = hNonStandModel.FlowIndex;

                        chList.Add(hNonStandModel.HangerProductFlowChartModel);

                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION)[hangerNo] = chList;

                        //站点分配数+1
                        var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = statingNo, AllocationNum = 1 };
                        // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                        UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
                        #endregion
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
        }

        //private static readonly object locObj = new object();

        /// <summary>
        /// 维护衣架状态
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void CurrentHangerProductinAction(RedisChannel arg1, RedisValue arg2)
        {
            //lock(locObj)
            tcpLogInfo.InfoFormat("【维护衣架状态】 开始【{0}】", arg2);
            var currentHangerProductingFlowModel = JsonConvert.DeserializeObject<SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(arg2);
            // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(currentHangerProductingFlowModel.HangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(currentHangerProductingFlowModel.HangerNo))
            {
                //SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW).Add(currentHangerProductingFlowModel.HangerNo, currentHangerProductingFlowModel);
                NewCacheService.Instance.AddHangerCurrentFlowToRedis(currentHangerProductingFlowModel.HangerNo, currentHangerProductingFlowModel);
                tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", arg2);
                return;
            }

            // SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[currentHangerProductingFlowModel.HangerNo] = currentHangerProductingFlowModel;
            NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(currentHangerProductingFlowModel.HangerNo, currentHangerProductingFlowModel);
            tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", arg2);
        }

        private void PublicTestAction(RedisChannel arg1, RedisValue arg2)
        {
            redisLog.InfoFormat("Server-->PublicTestAction-->{0}", arg2.ToString());
        }

        #region//衣架生产轨迹记录
        ProductsQueryServiceImpl pQueryService = new ProductsQueryServiceImpl();
        /// <summary>
        /// 衣架生产履历
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void HangerProductsChartResumeAction(RedisChannel arg1, RedisValue arg2)
        {
            lock (locObje)
            {
                if (0 == arg2)
                {
                    return;
                }
                HangerProductFlowChartModel recordHangerResume = null;
                var hangerProductsChartResumeModel = JsonConvert.DeserializeObject<HangerProductsChartResumeModel>(arg2);
                var hpFlowChart = hangerProductsChartResumeModel.HangerProductFlowChart;
                try
                {
                    tcpLogInfo.InfoFormat("【衣架生产轨迹记录】 开始");
                    var hangerNo = hangerProductsChartResumeModel.HangerNo?.Trim();
                    var dicHangerProductsChartResumeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME);
                    if (!dicHangerProductsChartResumeCache.ContainsKey(hangerProductsChartResumeModel.HangerNo) && null != hpFlowChart)
                    {
                        var hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);
                        var hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                        hpFlowChart.InsertDateTime = DateTime.Now;
                        hpFlowChart.UpdateDateTime = DateTime.Now;
                        hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                        hpFlowChart.CardNo = hEmStatingInfo?.Code;
                        hpFlowChart.EmployeeNo = hEmStatingInfo?.Code;
                        hpFlowChart.CheckResult = "完成";
                        recordHangerResume = hpFlowChart;
                        var hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);

                        //履历入库
                        PublishHangerResume(hpFlowChart, HangerResumeStaus.HangingPiece);
                        return;
                    }

                    var hProductsChartResumeList = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo];
                    var action = hangerProductsChartResumeModel.Action;
                    switch (action)
                    {
                        case 0://挂片
                            var hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);

                            var hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;

                            if (dicHangerProductsChartResumeCache.ContainsKey(hangerProductsChartResumeModel.HangerNo))
                            {
                                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Remove(hangerNo);
                            }
                            hpFlowChart.InsertDateTime = DateTime.Now;
                            hpFlowChart.UpdateDateTime = DateTime.Now;
                            hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                            hpFlowChart.CardNo = hEmStatingInfo?.Code;
                            hpFlowChart.EmployeeNo = hEmStatingInfo?.Code;
                            hpFlowChart.CheckResult = "完成";
                            recordHangerResume = hpFlowChart;
                            var hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);

                            //履历入库
                            PublishHangerResume(hpFlowChart, HangerResumeStaus.HangingPiece);
                            break;
                        case -1://重复挂片
                            hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);

                            hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                            hpFlowChart.EmployeeName = hEmStatingInfo?.RealName;
                            hpFlowChart.CardNo = hEmStatingInfo?.Code;
                            hpFlowChart.InsertDateTime = DateTime.Now;
                            hpFlowChart.UpdateDateTime = DateTime.Now;
                            hpFlowChart.CheckResult = "完成";
                            recordHangerResume = hpFlowChart;
                            hpcResumeList = new List<HangerProductFlowChartModel>() { hpFlowChart };
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Remove(hangerNo);
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME).Add(hangerNo, hpcResumeList);

                            //履历入库
                            PublishHangerResume(hpFlowChart, HangerResumeStaus.RepeatHangingPeiece);
                            break;
                        case 1://分配
                            if (null == hpFlowChart)
                            {
                                tcpLogInfo.InfoFormat("【衣架生产轨迹记录】 衣架生产完成.");
                                return;
                            }
                            //清除同站，同工序已分配的衣架
                            int rows1 = hProductsChartResumeList.RemoveAll(f => f.StatingNo != null
                             && -1 != f.StatingNo.Value
                             && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                             && f.StatingNo.Value == int.Parse(hangerProductsChartResumeModel.NextStatingNo)
                             && null != f.FlowNo
                             && f.FlowNo.Equals(hpFlowChart.FlowNo));
                            //清除监测点重复分配记录
                            int rows2 = hProductsChartResumeList.RemoveAll(f => f.StatingNo != null && -1 != f.StatingNo.Value && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && null != f.FlowNo && f.FlowNo.Equals(hpFlowChart.FlowNo));

                            hpFlowChart.InsertDateTime = DateTime.Now;
                            hpFlowChart.UpdateDateTime = DateTime.Now;
                            hpFlowChart.isAllocationed = true;
                            hpFlowChart.AllocationedDate = DateTime.Now;
                            hpFlowChart.StatingNo = short.Parse(hangerProductsChartResumeModel.NextStatingNo);
                            hProductsChartResumeList.Add(hpFlowChart);
                            recordHangerResume = hpFlowChart;
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;

                            //履历入库
                            PublishHangerResume(hpFlowChart, HangerResumeStaus.AllocationStating);

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

                                //履历入库
                                PublishHangerResume(hpfc, HangerResumeStaus.IncomeStating);

                            }
                            );
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;


                            break;
                        case 3://出战

                            //pQueryService.CheckStatingIsLogin(StatingNo);
                            var statingEmList = pQueryService.GetEmployeeLoginInfoList(hpFlowChart.StatingNo.Value.ToString(), hpFlowChart.MainTrackNumber.Value);

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
                            && f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value
                            && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                            && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                            {
                                //分配数-1
                                var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = hpfc.MainTrackNumber.Value, StatingNo = hpfc.StatingNo.Value, AllocationNum = -1 };
                                // NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));
                                UpdateMainTrackStatingAllocationNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(outAllocationNumModel));
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
                                   hpfc.CardNo = emStatingInfo?.Code;
                                   hpfc.EmployeeNo = emStatingInfo?.Code;
                                   if (null != hpfc.FlowType && hpfc.FlowType.Value == 1)
                                   {
                                       hpfc.CheckResult = "返工完成";
                                       hpfc.CheckInfo = (hpfc.ReworkEmployeeNo + '/' + hpfc.ReworkEmployeeName);
                                       hpfc.ReworkDate1 = hpfc.ReworkDate.Value.ToString("yyyy-MM-dd HH:mm");
                                   }
                                   else
                                   {
                                       hpfc.CheckResult = "完成";
                                   }
                                   if (string.IsNullOrEmpty(hpfc.HangerNo))
                                   {
                                       hpfc.HangerNo = hpFlowChart.HangerNo;
                                       hpfc.FlowCode = hpFlowChart.FlowCode;
                                       hpfc.FlowName = hpFlowChart.FlowName;
                                   }
                                   if (string.IsNullOrEmpty(hpfc.GroupNo))
                                   {
                                       hpfc.GroupNo = hpFlowChart.GroupNo;
                                   }

                                   //履历入库
                                   PublishHangerResume(hpfc, HangerResumeStaus.OutSiteStating);
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
                                hpfc.CardNo = emStatingInfo?.Code;
                                hpfc.EmployeeNo = emStatingInfo?.Code;
                                hpfc.StatingNo = Convert.ToInt16(hangerProductsChartResumeModel.StatingNo);
                                hProductsChartResumeList.Add(hpfc);
                                recordHangerResume = hpfc;

                                var reList = hProductsChartResumeList.Where(f => f.StatingNo != null
                                && -1 != f.StatingNo.Value
                                && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                                && f.StatingNo.Value != hpFlowChart.StatingNo.Value
                                && f.FlowNo.Equals(hpFlowChart.FlowNo)).ToList();
                                foreach (var t in reList)
                                {
                                    //清除硬件缓存
                                    var clearHangerNoCache = string.Format("【同工序不同站出站】 正在清除主轨:【{0}】站点{1} 衣架【{2}】的站点缓存...", t.MainTrackNumber.Value, t.StatingNo.Value, hangerNo);
                                    //CANTcp.client.ClearHangerCache(t.MainTrackNumber.Value, t.StatingNo.Value, int.Parse(hangerNo), SuspeConstants.XOR);
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
                                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                                    HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
                                }
                                //清除衣架轨迹日志
                                hProductsChartResumeList.RemoveAll(f => f.StatingNo != null
                            && -1 != f.StatingNo.Value
                            && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                            && f.StatingNo.Value != int.Parse(hangerProductsChartResumeModel.StatingNo)
                            && f.FlowNo.Equals(hpFlowChart.FlowNo));

                            }

                            //处理合并工序
                            var hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);// SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo];
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
                                hpfc.CardNo = emStatingInfo?.Code;
                                hpfc.EmployeeNo = emStatingInfo?.Code;
                                hProductsChartResumeList.Add(hpfc);

                                //履历入库
                                PublishHangerResume(hpfc, HangerResumeStaus.MeregeOutSiteStating);
                            });
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                            break;
                        case 4://返工
                               //移除发起站点
                            hStatingEmList = pQueryService.GetEmployeeLoginInfoList(hangerProductsChartResumeModel.StatingNo?.Trim(), hangerProductsChartResumeModel.MainTrackNumber);
                            var isBridgeStating = CANProductsService.Instance.IsBridge(hangerProductsChartResumeModel.MainTrackNumber, short.Parse(hangerProductsChartResumeModel.StatingNo));
                            hEmStatingInfo = hStatingEmList.Count > 0 ? hStatingEmList[0] : null;
                            hProductsChartResumeList.Where(f =>
                                  f.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo)
                                  && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                                  && !isBridgeStating
                                  && f.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber).ForEach(delegate (HangerProductFlowChartModel hpf)
                                  {
                                      hpf.CompareDate = DateTime.Now;
                                      hpf.CheckResult = "操作返工";
                                      hpf.CheckInfo = string.Format("工序{0} 返工", hangerProductsChartResumeModel.ReworkFlowNos);
                                      hpf.EmployeeName = hEmStatingInfo?.RealName;
                                      hpf.CardNo = hEmStatingInfo?.Code;
                                      hpf.EmployeeNo = hEmStatingInfo?.Code;
                                      hpf.IsReworkSourceStating = true;
                                      hpf.UpdateDateTime = DateTime.Now;
                                      hpf.Status = HangerProductFlowChartStaus.Successed.Value;


                                      //履历入库
                                      PublishHangerResume(hpf, HangerResumeStaus.Rework);
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

                                        //履历入库
                                        PublishHangerResume(hpfc, HangerResumeStaus.Rework);
                                    }
                                    );
                            }

                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                            break;
                        case 5://站点删除

                            var statingDeleList = hProductsChartResumeList.Where(data => data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                              && data.StatingNo.Value == int.Parse(hangerProductsChartResumeModel.StatingNo)
                              && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);

                            hProductsChartResumeList.RemoveAll(data => data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                            && data.StatingNo.Value == int.Parse(hangerProductsChartResumeModel.StatingNo)
                            && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);

                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;

                            foreach (var h in statingDeleList)
                            {
                                //履历入库
                                PublishHangerResume(h, HangerResumeStaus.StatingDelete);
                            }
                            break;
                        case 6://工序删除
                            var delFlowList = hProductsChartResumeList.Where(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                             && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);

                            hProductsChartResumeList.RemoveAll(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                           && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;

                            foreach (var h in delFlowList)
                            {
                                //履历入库
                                PublishHangerResume(h, HangerResumeStaus.FlowDeleted);
                            }
                            break;
                        case 7://工序及站点移动
                            var flowMoveOrStatingMoveList = hProductsChartResumeList.Where(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                             && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);

                            hProductsChartResumeList.RemoveAll(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                           && data.Status.Value != HangerProductFlowChartStaus.Successed.Value);
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                            foreach (var h in flowMoveOrStatingMoveList)
                            {
                                //履历入库
                                PublishHangerResume(h, HangerResumeStaus.FlowMoveOrStatingMove);
                            }
                            break;
                        case 8://桥接不携带工序进站
                            hProductsChartResumeList.Where(f => f.StatingNo != null
                           && -1 != f.StatingNo.Value
                           && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                           && f.StatingNo.Value == hpFlowChart.StatingNo.Value
                           && f.MainTrackNumber.Value == hpFlowChart.MainTrackNumber.Value).ToList().ForEach(delegate (HangerProductFlowChartModel hpfc)
                           {
                               hpfc.Status = HangerProductFlowChartStaus.Producting.Value;
                               hpfc.IncomeSiteDate = DateTime.Now;
                               hpfc.UpdateDateTime = DateTime.Now;


                               //履历入库
                               PublishHangerResume(hpfc, HangerResumeStaus.BridgeNonFlowIncomeStating);
                           }
                           );
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                            break;
                        case 9://桥接携带工序且工序已完成，再次分配
                            hpFlowChart.InsertDateTime = DateTime.Now;
                            hpFlowChart.UpdateDateTime = DateTime.Now;
                            hpFlowChart.isAllocationed = true;
                            hpFlowChart.AllocationedDate = DateTime.Now;
                            hpFlowChart.StatingNo = short.Parse(hangerProductsChartResumeModel.NextStatingNo);
                            hProductsChartResumeList.Add(hpFlowChart);
                            recordHangerResume = hpFlowChart;
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;

                            //履历入库
                            PublishHangerResume(hpFlowChart, HangerResumeStaus.BridgeCarryFlowSucessedAginAollocation);
                            break;
                        case 12://F2指定站出战衣架轨迹修正(F2指定发起源头站)
                            var f2SourceResuList = hProductsChartResumeList.Where(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                           && data.Status.Value != HangerProductFlowChartStaus.Successed.Value && data.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber
                           && data.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo));

                            hProductsChartResumeList.RemoveAll(data => null != data.FlowNo && data.FlowNo.Equals(hangerProductsChartResumeModel.FlowNo)
                          && data.Status.Value != HangerProductFlowChartStaus.Successed.Value && data.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber
                          && data.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo));
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;

                            foreach (var f2 in f2SourceResuList)
                            {
                                PublishHangerResume(f2, HangerResumeStaus.F2SourceHangerOutSiteCorrorHangerResume);
                            }
                            break;
                        case 13://F2指定站出战衣架轨迹修正
                            var f2CorrectResumeList = hProductsChartResumeList.Where(data => string.IsNullOrEmpty(data.FlowNo)
                           && data.Status.Value != HangerProductFlowChartStaus.Successed.Value && data.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber
                           && data.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo));

                            hProductsChartResumeList.RemoveAll(data => string.IsNullOrEmpty(data.FlowNo)
                          && data.Status.Value != HangerProductFlowChartStaus.Successed.Value && data.MainTrackNumber.Value == hangerProductsChartResumeModel.MainTrackNumber
                          && data.StatingNo.Value == short.Parse(hangerProductsChartResumeModel.StatingNo));
                            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[hangerNo] = hProductsChartResumeList;
                            foreach (var f2 in f2CorrectResumeList)
                            {
                                PublishHangerResume(f2, HangerResumeStaus.F2HangerOutSiteCorrorHangerResume);
                            }
                            break;
                    }

                }
                catch (Exception ex)
                {
                    tcpLogError.Error(ex);
                }
                finally
                {
                    //var recordHangerResumeJson = string.Empty;
                    //if (null != recordHangerResume) recordHangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(recordHangerResume);
                    tcpLogInfo.InfoFormat($"【衣架生产轨迹记录】 结束");
                }

            }

        }
        void PublishHangerResume(HangerProductFlowChartModel hangerResume, HangerResumeStaus hangerResumeStaus)
        {
            try
            {
                var hangerResumeModel = new HangerResume();
                hangerResumeModel.BatchNo = hangerResume.BatchNo;
                hangerResumeModel.MainTrackNumber = hangerResume.MainTrackNumber + "";
                hangerResumeModel.ProductsId = hangerResume.ProductsId + "";
                hangerResumeModel.GroupNo = hangerResume.GroupNo + "";
                hangerResumeModel.Unit = hangerResume.Unit + "";
                hangerResumeModel.HangerNo = Convert.ToInt64(hangerResume.HangerNo);
                hangerResumeModel.StyleNo = hangerResume.StyleNo + "";
                hangerResumeModel.IsHangerSucess = hangerResume.IsHangerSucess;
                hangerResumeModel.Po = hangerResume.Po;
                hangerResumeModel.ProcessOrderNo = hangerResume.ProcessOrderNo;
                hangerResumeModel.ProcessChartId = hangerResume.ProcessChartId;
                hangerResumeModel.FlowIndex = hangerResume.FlowIndex;
                hangerResumeModel.FlowId = hangerResume.FlowId;
                hangerResumeModel.FlowNo = hangerResume.FlowNo;
                hangerResumeModel.FlowCode = hangerResume.FlowCode;
                hangerResumeModel.FlowName = hangerResume.FlowName;
                hangerResumeModel.StatingId = hangerResume.StatingId;
                hangerResumeModel.StatingNo = hangerResume.StatingNo + "";
                hangerResumeModel.StatingCapacity = hangerResume.StatingCapacity;
                hangerResumeModel.NextStatingNo = hangerResume.NextStatingNo;
                hangerResumeModel.FlowRealyProductStatingNo = hangerResume.FlowRealyProductStatingNo;
                hangerResumeModel.Status = hangerResume.Status + "";
                hangerResumeModel.FlowType = hangerResume.FlowType;
                hangerResumeModel.IsFlowSucess = hangerResume.IsFlowSucess;
                hangerResumeModel.IsReworkSourceStating = hangerResume.IsReworkSourceStating;
                hangerResumeModel.DefectCode = hangerResume.DefectCode;
                hangerResumeModel.DefcectName = hangerResume.DefcectName;
                hangerResumeModel.PColor = hangerResume.PColor;
                hangerResumeModel.PSize = hangerResume.PSize;
                hangerResumeModel.EmployeeName = hangerResume.EmployeeName;
                hangerResumeModel.CardNo2 = hangerResume.CardNo;
                hangerResumeModel.EmployeeNo = hangerResume.EmployeeNo;
                hangerResumeModel.IncomeSiteDate = hangerResume.IncomeSiteDate;
                hangerResumeModel.CompareDate = hangerResume.CompareDate;
                hangerResumeModel.OutSiteDate = hangerResume.OutSiteDate;
                hangerResumeModel.ReworkEmployeeNo = hangerResume.ReworkEmployeeNo;
                hangerResumeModel.ReworkEmployeeName = hangerResume.ReworkEmployeeName;
                hangerResumeModel.ReworkMaintrackNumber = hangerResume.ReworkMaintrackNumber;
                hangerResumeModel.ReworkStatingNo = hangerResume.ReworkStatingNo;
                hangerResumeModel.Memo = hangerResume.Memo;
                hangerResumeModel.CheckResult = hangerResume.CheckResult;
                hangerResumeModel.CheckInfo = hangerResume.CheckInfo;
                hangerResumeModel.StanardHours = hangerResume.StanardHours;
                hangerResumeModel.StandardPrice = hangerResume.StandardPrice;
                hangerResumeModel.HangerStatus = hangerResumeStaus.Value;
                hangerResumeModel.HangerStatusDesc = hangerResumeStaus.Desption;
                var resultHangeResme = hangerResumeModel;// BeanUitls<HangerResume, HangerProductFlowChartModel>.Mapper(hangerResume);
                var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(resultHangeResme);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_DB_ACTION, allocationJson);
            }
            catch (Exception ex)
            {
                redisLog.Error("【衣架履历入库】异常", ex);
            }
        }
        /// <summary>
        /// 衣架生产履历入库
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void HangerProductResumeDBAction(RedisChannel arg1, RedisValue arg2)
        {
            try
            {
                var hangerResume = JsonConvert.DeserializeObject<HangerResume>(arg2);
                DapperHelp.Add(hangerResume);
            }
            catch (Exception ex)
            {
                redisLog.Error("【衣架履历入库】异常", ex);
            }
        }

        #endregion

        #region //生产完成数据转移
        private void ProductSuccessCopyDataAction(RedisChannel arg1, RedisValue arg2)
        {
            var wp = JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
            CANProductsService.Instance.CopySucessDataExt(wp);
        }
        #endregion

        /// <summary>
        /// 记录挂片信息(衣架挂片工序明细)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>

        public void HangingStationOutSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            var hangerProductFlowChartModel = JsonConvert.DeserializeObject<HangerProductFlowChartModel>(arg2);
            var statingEmList = new ProductsQueryServiceImpl().GetEmployeeLoginInfoList(hangerProductFlowChartModel.StatingNo.Value.ToString(), hangerProductFlowChartModel.MainTrackNumber.Value);
            if (statingEmList.Count != 0)
            {
                hangerProductFlowChartModel.CardNo = statingEmList.First().CardNo?.Trim();
                hangerProductFlowChartModel.EmployeeName = statingEmList.First().RealName?.Trim();
                hangerProductFlowChartModel.OutSiteDate = DateTime.Now;
                hangerProductFlowChartModel.EmployeeNo = statingEmList.First().Code?.Trim();
            }
            var pQueryService = new ProductsQueryServiceImpl();
            //pQueryService.CheckStatingIsLogin(StatingNo);
            //var statingEmList = pQueryService.GetEmployeeLoginInfoList(hangerProductFlowChartModel.StatingNo+"", hangerProductFlowChartModel.MainTrackNumber.Value);
            //var emStatingInfo = statingEmList[0];
            var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hangerProductFlowChartModel);
            //HangerProductFlowChartDao.Instance.Add(inHangerFlowChart);
            DapperHelp.Add(inHangerFlowChart);
            tcpLogInfo.Info("【挂片站站点挂片产量记录----------------】");

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

        //private void EmployeeCardRelationAction(RedisChannel arg1, RedisValue arg2)
        //{
        //    var employeeCardRelactionList = EmployeeCardRelationDao.Instance.GetAll();

        //}

        ////员工登录相关
        //private void StatingEmployeeAction(RedisChannel arg1, RedisValue arg2)
        //{

        //}

        private void HangerReworkReuestQueueAction(RedisChannel arg1, RedisValue arg2)
        {
            var hrrq = JsonConvert.DeserializeObject<HangerReworkRequestQueue>(arg2);
            //HangerReworkRequestQueueDao.Instance.Insert(hrrq);
            DapperHelp.Add(hrrq);
        }

        //private int GetNonProductsProcessFlowChartList(List<HangerProductFlowChartModel> hfcList, MainTrackStatingMontorModel montor)
        //{
        //    var nextFlowList = hfcList.Where(f => f.MainTrackNumber == short.Parse(montor.MainTrackNumber.ToString())
        //    && f.StatingNo != null && f.StatingNo.Value != -1
        //     && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
        //     && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //     && ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //     || ((hfcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        //     ).Select(f => f.FlowIndex);
        //    //var flowIndexNumList = new List<int>();
        //    //var result = new List<HangerProductFlowChartModel>();
        //    //var flowIndexList = hfcList.Where(k => k.FlowIndex.Value != 1 && k.MergeProcessFlowChartFlowRelationId == null).Select(f => f.FlowIndex.Value).ToList<int>().OrderBy(f => f).Distinct();
        //    //foreach (var fIndex in flowIndexList)
        //    //{
        //    //    ////没有生产的工序
        //    //    //var productSuccessCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0).Count();
        //    //    ////工序下的站点都没生产，或者手动拿出站放到主轨的情况
        //    //    //var isExiStatingNo = hfcList.Where(f => f.FlowIndex.Value == fIndex && null != f.StatingNo && f.StatingNo.Value != 0 &&
        //    //    //    (
        //    //    //        (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value) || (f.Status.Value == HangerProductFlowChartStaus.Producting.Value && f.OutSiteDate == null)
        //    //    //    )
        //    //    //).Select(f => f.StatingNo).Count() >= 0;
        //    //    //if (productSuccessCount > 0 && isExiStatingNo)
        //    //    //{
        //    //    //    return fIndex;
        //    //    //}

        //    //    var nonProductSuccessReworkCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && (f.Status.Value == 0 || f.Status.Value == 1) && null != f.StatingNo && f.StatingNo.Value != 0 && f.FlowType.Value == 1).Count();
        //    //    //存在未完成的返工
        //    //    if (nonProductSuccessReworkCount > 0)
        //    //    {
        //    //        if (!flowIndexNumList.Contains(fIndex))
        //    //        {
        //    //            flowIndexNumList.Add(fIndex);
        //    //            continue;
        //    //        }
        //    //    }
        //    //    if (nonProductSuccessReworkCount == 0)
        //    //    {
        //    //        var successFlowCount = hfcList.Where(f => f.FlowIndex.Value == fIndex && f.Status.Value == 2 && null != f.StatingNo && f.StatingNo.Value != 0).Count() > 0;
        //    //        if (!successFlowCount)
        //    //        {
        //    //            if (!flowIndexNumList.Contains(fIndex))
        //    //            {
        //    //                flowIndexNumList.Add(fIndex);
        //    //                continue;
        //    //            }
        //    //        }

        //    //    }
        //    //}
        //    //if (flowIndexNumList.Count > 0)
        //    //{
        //    //    return flowIndexNumList.Min();
        //    //}
        //    if (nextFlowList.Count() > 0)
        //    {
        //        return nextFlowList.Distinct().Min().Value;
        //    }
        //    return -1;
        //}

        #region 监测点
        /// <summary>
        /// 监测点
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void MainTrackStatingMontorUploadAction(RedisChannel arg1, RedisValue arg2)
        {
            var mainTrackStatingMontor = JsonConvert.DeserializeObject<MainTrackStatingMontorModel>(arg2);
            var logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->开始...", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
            montorLog.Info(logMessage);
            try
            {
                MontorHandler.Instance.Process(mainTrackStatingMontor);
            }
            catch (Exception ex)
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->监测点分配错误!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Error(logMessage, ex);
            }
            finally
            {
                logMessage = string.Format("【监测点日志】衣架号:{0} 主轨:{1} 站点:{2} 监测-->分配结束!", mainTrackStatingMontor?.HangerNo, mainTrackStatingMontor?.MainTrackNumber, mainTrackStatingMontor?.StatingNo);
                montorLog.Info(logMessage);
                MainThreadTools.Instance.ResumeThread();
            }

        }

        #endregion

        /// <summary>
        /// 在线数更新
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void UpdateMainTrackStatingOnlineNumAction(RedisChannel arg1, RedisValue arg2)
        {
            lock (locObje)
            {
                var dic = RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM);
                var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
                var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);
                if (!dic.ContainsKey(key))
                {
                    RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM).Add(key, mts.OnLineSum);
                }
                else
                {
                    var num = dic[key];
                    if (num == 0)
                    {
                        if (mts.OnLineSum > 0)
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num + 1;
                        }
                    }
                    else
                    {
                        if (mts.OnLineSum < 0)
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num - 1;
                        }
                        else
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM)[key] = num + 1;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 站内数维护
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void UpdateMainTrackStatingInNumAction(RedisChannel arg1, RedisValue arg2)
        {
            lock (locObje)
            {
                var dic = RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
                var mts = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
                var key = string.Format("{0}:{1}", mts.MainTrackNumber, mts.StatingNo);

                tcpLogInfo.InfoFormat("【站内数维护】:站点主轨{0}", key);
                tcpLogInfo.InfoFormat("【站内数维护】:内容{0}", JsonConvert.SerializeObject(mts));

                if (!dic.ContainsKey(key))
                {
                    if (mts.OnLineSum > 0)
                        RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM).Add(key, 1);
                }
                else
                {
                    var num = dic[key];
                    if (num == 0)
                    {
                        if (mts.OnLineSum > 0)
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num + 1;
                        }
                    }
                    else
                    {
                        if (mts.OnLineSum < 0)
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num - 1;
                        }
                        else
                        {
                            RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[key] = num + 1;
                        }
                    }
                }
            }
        }

        private void MainTrackStatingStatusAction(RedisChannel arg1, RedisValue arg2)
        {
            var mainTrackStatingInfo = JsonConvert.DeserializeObject<MainTrackStatingCacheModel>(arg2);
            var mainStatingKey = string.Format("{0}:{1}", mainTrackStatingInfo.MainTrackNumber, mainTrackStatingInfo.StatingNo);
            var dic = RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
            if (dic.Keys.Contains(mainStatingKey))
            {
                RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS)[mainStatingKey] = mainTrackStatingInfo;
            }
            else
            {
                dic.Add(mainStatingKey, mainTrackStatingInfo);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void HangerAollcationAction(RedisChannel arg1, RedisValue arg2)
        {
            try
            {
                var hangerStatingAllocationItem = JsonConvert.DeserializeObject<HangerStatingAllocationItem>(arg2);
                var statingCache = NewCacheService.Instance.GetStatingCache(hangerStatingAllocationItem.MainTrackNumber.Value, int.Parse(hangerStatingAllocationItem.SiteNo));
                hangerStatingAllocationItem.GroupNo = statingCache.GroupNO;
                DapperHelp.Add(hangerStatingAllocationItem);
            }
            catch (Exception ex)
            {
                redisLog.Error("【衣架分配入库】异常", ex);
            }
        }

        private void HangerResumeAction(RedisChannel arg1, RedisValue arg2)
        {
            try { } catch (Exception ex) { redisLog.Error("【衣架履历入库】异常", ex); }
        }

        void SetGroupNo(HangerOutSiteResult hos)
        {
            if (null == hos.HangerProductFlowChart)
            {
                tcpLogError.ErrorFormat("主轨:{0} 站点:{1} 没有工艺图!", hos.MainTrackNumber, hos.StatingNo);
                return;
            }
            var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format("{0}:{1}", hos.HangerProductFlowChart.MainTrackNumber.Value, hos.HangerProductFlowChart.StatingNo.Value);
            if (!dicStatingCache.ContainsKey(key))
            {
                tcpLogError.ErrorFormat("主轨:{0} 站点:{1} 没有找到组!", hos.HangerProductFlowChart.MainTrackNumber.Value, hos.HangerProductFlowChart.StatingNo.Value);
            }
            hos.HangerProductFlowChart.GroupNo = dicStatingCache[key].GroupNO?.Trim();
            //return "";
        }
        //出战处理处理:
        //1.衣架工艺图数据写入db;
        //2.站点衣架生产明细记录db;
        //3.衣架出战衣架产品明细写入db
        //4.存储站不记录产量
        public void HangerOutSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            var outSiteResult = JsonConvert.DeserializeObject<HangerOutSiteResult>(arg2);
            SetGroupNo(outSiteResult);
            //HangerProductItem hpItem = null;
            //var dicHangerProductItemList = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
            //if (dicHangerProductItemList.ContainsKey(outSiteResult.HangerNo))
            //{
            //    var hanerProductItemList = dicHangerProductItemList[outSiteResult.HangerNo];
            //    foreach (var hp in hanerProductItemList)
            //    {
            //        if (hp.SiteNo.Equals(outSiteResult.StatingNo) && hp.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber)
            //        {
            //            hpItem = hp;
            //            var hangerProductItem = BeanUitls<HangerProductItem, HangerProductItemModel>.Mapper(hp);
            //            //HangerProductItemDao.Instance.Add(hangerProductItem);
            //            DapperHelp.Add(hangerProductItem);
            //        }
            //    }
            //}
            //lucifer/2018年10月12日--
            //站点衣架缓存
            var statingHangerKey = string.Format("{0}:{1}", outSiteResult.MainTrackNumber, int.Parse(outSiteResult.StatingNo));
            var dicHangerProductItemListExt = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT);
            if (!dicHangerProductItemListExt.ContainsKey(statingHangerKey))
            {
                var outHangerProductsItemModelList = new List<HangerProductItemModel>();
                var outStatingHangerProductItem = BeanUitls<HangerProductItemModel, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);

                outStatingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
                outStatingHangerProductItem.SizeNum = string.IsNullOrEmpty(outSiteResult.HangerProductFlowChart.Num) ? 0 : int.Parse(outSiteResult.HangerProductFlowChart.Num);
                outStatingHangerProductItem.ProcessFlowName = outSiteResult.HangerProductFlowChart.FlowName;
                outStatingHangerProductItem.ProcessFlowId = outSiteResult.HangerProductFlowChart.FlowId;
                outStatingHangerProductItem.ProcessFlowCode = outSiteResult.HangerProductFlowChart.FlowCode;
                outStatingHangerProductItem.FlowChartd = outSiteResult.HangerProductFlowChart.ProcessChartId;
                outStatingHangerProductItem.IsReturnWorkFlow = (null != outSiteResult.HangerProductFlowChart.FlowType && outSiteResult.HangerProductFlowChart.FlowType.Value == 1);
                outStatingHangerProductItem.OutSiteDate = DateTime.Now;
                outHangerProductsItemModelList.Add(outStatingHangerProductItem);
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT).Add(statingHangerKey, outHangerProductsItemModelList);
            }
            else
            {
                var outHangerProductsItemModelList = dicHangerProductItemListExt[statingHangerKey];
                var outStatingHangerProductItem = BeanUitls<HangerProductItemModel, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
                outStatingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
                outStatingHangerProductItem.SizeNum = string.IsNullOrEmpty(outSiteResult.HangerProductFlowChart.Num) ? 0 : int.Parse(outSiteResult.HangerProductFlowChart.Num);
                outStatingHangerProductItem.ProcessFlowName = outSiteResult.HangerProductFlowChart.FlowName;
                outStatingHangerProductItem.ProcessFlowId = outSiteResult.HangerProductFlowChart.FlowId;
                outStatingHangerProductItem.ProcessFlowCode = outSiteResult.HangerProductFlowChart.FlowCode;
                outStatingHangerProductItem.FlowChartd = outSiteResult.HangerProductFlowChart.ProcessChartId;
                outStatingHangerProductItem.IsReturnWorkFlow = (null != outSiteResult.HangerProductFlowChart.FlowType && outSiteResult.HangerProductFlowChart.FlowType.Value == 1);
                outStatingHangerProductItem.OutSiteDate = DateTime.Now;
                outHangerProductsItemModelList.Add(outStatingHangerProductItem);
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT)[statingHangerKey] = outHangerProductsItemModelList;
            }

            var hanerProductItemListExt = dicHangerProductItemListExt[statingHangerKey];
            foreach (var hp in hanerProductItemListExt)
            {
                if (hp.SiteNo.Equals(outSiteResult.StatingNo) && hp.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber)
                {
                    // hpItem = hp;
                    var hangerProductItem = BeanUitls<HangerProductItem, HangerProductItemModel>.Mapper(hp);
                    //HangerProductItemDao.Instance.Add(hangerProductItem);
                    DapperHelp.Add(hangerProductItem);
                }
            }
            //lucifer/2018年10月12日--

            var pQueryService = new ProductsQueryServiceImpl();
            //pQueryService.CheckStatingIsLogin(StatingNo);
            var statingEmList = pQueryService.GetEmployeeLoginInfoList(outSiteResult.StatingNo, outSiteResult.MainTrackNumber);
            var emStatingInfo = statingEmList[0];

            var listDisc = new List<string>();
            HangerProductFlowChartModel hProductFlowChart = null;
            var dicHangerProductChartList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (dicHangerProductChartList.ContainsKey(outSiteResult.HangerNo))
            {
                var hangerProductChartList = dicHangerProductChartList[outSiteResult.HangerNo];
                hProductFlowChart = outSiteResult.HangerProductFlowChart;
                //存储站不记录产量
                var statingRole = hProductFlowChart.StatingRoleCode?.Trim();
                var storeStatingRoleCode = StatingType.StatingStorage.Code?.Trim();
                var isStoreStating = storeStatingRoleCode.Equals(statingRole);
                if (!isStoreStating)
                {
                    hProductFlowChart.EmployeeName = emStatingInfo.RealName;
                    hProductFlowChart.CardNo = emStatingInfo.CardNo;
                    hProductFlowChart.EmployeeNo = emStatingInfo.Code?.Trim();
                    var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hProductFlowChart);
                    DapperHelp.Add(inHangerFlowChart);
                    RecordEmployeeProductOut(hProductFlowChart);

                    MergeProcessFlow(hProductFlowChart, hangerProductChartList);

                    var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
                    statingHangerProductItem.Id = null;
                    statingHangerProductItem.SiteNo = outSiteResult.HangerProductFlowChart.StatingNo?.ToString();
                    statingHangerProductItem.IsReturnWorkFlow = null == hProductFlowChart?.FlowType ? false : (hProductFlowChart?.FlowType == 1 ? true : false);
                    statingHangerProductItem.IsReworkSourceStating = hProductFlowChart?.IsReworkSourceStating;
                    statingHangerProductItem.FlowChartd = hProductFlowChart.ProcessChartId;
                    statingHangerProductItem.ProcessFlowId = hProductFlowChart.FlowId;
                    statingHangerProductItem.ProcessFlowCode = hProductFlowChart.FlowCode;
                    statingHangerProductItem.ProcessFlowName = hProductFlowChart.FlowName;
                    statingHangerProductItem.Id = DapperHelp.Add(statingHangerProductItem);

                    tcpLogInfo.Info("【出战产量记录】【非合并工序-->记录站点衣架生产记录】");
                }

                //foreach (var hpc in hangerProductChartList)
                //{
                //    if (hpc.StatingNo.Value == short.Parse(outSiteResult.StatingNo) && hpc.MainTrackNumber.Value == (short)outSiteResult.MainTrackNumber && hpc.Status.Value == HangerProductFlowChartStaus.Successed.Value)
                //    {
                //        var dics = string.Format("{0}{1}{2}", hpc.StatingNo.Value, hpc.Status.Value, hpc.MainTrackNumber.Value);
                //        if (!listDisc.Contains(dics))
                //        {
                //            hpc.EmployeeName = emStatingInfo.RealName;
                //            hpc.CardNo = emStatingInfo.CardNo;
                //            hProductFlowChart = hpc;
                //            var inHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChart>.Mapper(hProductFlowChart);
                //            //HangerProductFlowChartDao.Instance.Add(inHangerFlowChart);
                //            DapperHelp.Add(inHangerFlowChart);
                //            MergeProcessFlow(hProductFlowChart, hangerProductChartList);
                //            listDisc.Add(dics);
                //        }
                //    }
                //}
            }

            ////更新衣架工序生产明细的衣架出站时间
            //var sql = new StringBuilder("select top 1 * from HangerProductItem where HangerNo=? and SiteNo=? and MainTrackNumber=?");
            //var obj = QueryForObject<HangerProductItem>(sql, null, false, outSiteResult.HangerNo, outSiteResult.StatingNo, outSiteResult.MainTrackNumber);
            //if (null == obj)
            //{
            //    var ex = new NoFoundOnlineProductsException(string.Format("【更新衣架工序生产明细的衣架出站时间】 出错:找不到生产明细;主轨:{0} 站点:{1} 衣架号:{2}", outSiteResult.HangerNo, outSiteResult.StatingNo, outSiteResult.MainTrackNumber));
            //    tcpLogError.Error(ex);
            //    throw ex;
            //}
            //obj.OutSiteDate = DateTime.Now;
            //obj.IsSucessedFlow = true;
            //HangerProductItemDao.Instance.Update(obj);
            //log.Info("【更新衣架工序生产明细的衣架出站时间】");

            //if (null == outSiteResult.HangerProductFlowChart)
            //{
            //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", outSiteResult.MainTrackNumber, outSiteResult.HangerNo, outSiteResult.StatingNo));
            //    errorLog.Error("【衣架出站】", ex);
            //    return;
            //}
            //outSiteResult.HangerProductFlowChart.OutSiteDate = DateTime.Now;
            //outSiteResult.HangerProductFlowChart.FlowRealyProductStatingNo = short.Parse(outSiteResult.StatingNo);
            //outSiteResult.HangerProductFlowChart.IsFlowSucess = true;
            //outSiteResult.HangerProductFlowChart.Status = 2;//生产完成
            //HangerProductFlowChartDao.Instance.Update(outSiteResult.HangerProductFlowChart);

            //if (null != hpItem)
            //{
            //记录站点衣架生产记录
            // var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductItem>.Mapper(hpItem);

            //var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(outSiteResult.HangerProductFlowChart);
            //statingHangerProductItem.Id = null;
            //statingHangerProductItem.IsReturnWorkFlow = null == hProductFlowChart?.FlowType ? false : (hProductFlowChart?.FlowType == 1 ? true : false);
            //statingHangerProductItem.IsReworkSourceStating = hProductFlowChart?.IsReworkSourceStating;
            //statingHangerProductItem.Id = StatingHangerProductItemDao.Instance.Insert(statingHangerProductItem);
            //log.Info("【记录站点衣架生产记录】");

            /* lucifer/2018年10月13日

            //检查工序是否完成，若完成则转移数据
            //注释于2018年9月24日 20:36:40
            var isFlowSucess = false;//CANProductsService.Instance.CheckHangerProcessChartIsSuccessed(outSiteResult.HangerNo);//CheckFlowIsSuccessed(obj.FlowChartd, (short)obj.FlowIndex);
            if (isFlowSucess)
            {

                var sucessMessage = string.Format("主轨:{0} 最后一站:{1} 衣架号:{2} 已生产完成!", outSiteResult.MainTrackNumber, outSiteResult.StatingNo, outSiteResult.HangerNo);
                tcpLogInfo.Info(sucessMessage);

                //var sqlWaitGen = new StringBuilder(@"select * from WaitProcessOrderHanger where HangerNo=?");
                //var data = QueryForObject<WaitProcessOrderHanger>(sqlWaitGen, null, false, outSiteResult.HangerNo);
                var sqlWaitGen = new StringBuilder(@"select * from WaitProcessOrderHanger where HangerNo=@HangerNo");
                var data = DapperHelp.FirstOrDefault<WaitProcessOrderHanger>(sqlWaitGen.ToString(), new { HangerNo = outSiteResult.HangerNo });

                var thread = new Thread(CANProductsService.Instance.CopySucessData);
                thread.Start(data);

                //记录员工产量

                //var refpModelSucess = new RecordEmployeeFlowProductionModel() { IsEndFlow = true, MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
                //var threadRecordEmployeeFlowProductionSucess = new Thread(new ThreadStart(refpModelSucess.RecordEmployeeFlowProduction));
                //threadRecordEmployeeFlowProductionSucess.Start();
                //记录员工产量
                RecordEmployeeProductOut(outSiteResult.StatingNo, statingHangerProductItem.Id, outSiteResult.MainTrackNumber, outSiteResult.HangerNo, true);
                return;
            }
            ////记录员工产量
            //var refpModel = new RecordEmployeeFlowProductionModel() { MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
            //var threadRecordEmployeeFlowProduction = new Thread(new ThreadStart(refpModel.RecordEmployeeFlowProduction));
            //threadRecordEmployeeFlowProduction.Start();

            //记录员工产量
            RecordEmployeeProductOut(outSiteResult.StatingNo, statingHangerProductItem.Id, outSiteResult.MainTrackNumber, outSiteResult.HangerNo, false);
        }

        */

            ////更新站点出站时间
            //var sqlHangerStatingAll = new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=? and SiteNo=?");
            //var hangerStatingAllocationItem = QueryForObject<HangerStatingAllocationItem>(sqlHangerStatingAll, null, false, outSiteResult.HangerNo, outSiteResult.StatingNo);
            //if (null != hangerStatingAllocationItem)
            //{
            //    hangerStatingAllocationItem.OutSiteDate = DateTime.Now;
            //    hangerStatingAllocationItem.Status = 1;//更新完成
            //    HangerStatingAllocationItemDao.Instance.Update(hangerStatingAllocationItem);
            //}


            ////记录员工产量
            //var refpModel = new RecordEmployeeFlowProductionModel() { MainTrackNumber = outSiteResult.MainTrackNumber, HangerNo = outSiteResult.HangerNo, StatingNo = outSiteResult.StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
            //var threadRecordEmployeeFlowProduction = new Thread(new ThreadStart(refpModel.RecordEmployeeFlowProduction));
            //threadRecordEmployeeFlowProduction.Start();

            //var wp = SusRedisClient.RedisTypeFactory.GetDictionary<string, WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER)[outSiteResult.HangerNo];
            //wp.ProcessFlowId = outSiteResult.pfcFlowRelation?.ProcessFlow?.Id;
            //wp.FlowNo = outSiteResult.pfcFlowRelation.FlowNo;
            //wp.ProcessFlowCode = outSiteResult.pfcFlowRelation.FlowCode;
            //wp.ProcessFlowName = outSiteResult.pfcFlowRelation.FlowName;
            //WaitProcessOrderHangerDao.Instance.Update(wp);

            //var hAllocationItem = BeanUitls<HangerStatingAllocationItem, WaitProcessOrderHanger>.Mapper(wp);
            //hAllocationItem.AllocatingStatingDate = DateTime.Now;
            //hAllocationItem.SiteNo = outSiteResult.StatingNo;
            ////hAllocationItem.HsaiNdex = pIndex;
            //hAllocationItem.Status = 0;
            //hAllocationItem.HangerType = outSiteResult.HangerProductFlowChart?.FlowType;
            //hAllocationItem.FlowNo = outSiteResult.HangerProductFlowChart.FlowNo;
            //hAllocationItem.MainTrackNumber = (short)outSiteResult.MainTrackNumber;
            //hAllocationItem.NextSiteNo = outSiteResult.NextStatingNo;
            //HangerStatingAllocationItemDao.Instance.Insert(hAllocationItem);
        }
        /// <summary>
        /// 将合并工序标记为生产完成!
        /// </summary>
        /// <param name="currentHangerProductFlowChart"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        private void MergeProcessFlow(HangerProductFlowChartModel currentHangerProductFlowChart, List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        {
            try
            {
                //过滤已完成的同工序（连续返工）
                var filterFlowNoList = new List<string>();
                var meregeProcessFlowChartList = hangerProcessFlowChartList.Where(f =>
                (null != f.IsMergeForward && f.IsMergeForward.Value)
                && (currentHangerProductFlowChart.ProcessFlowChartFlowRelationId.Equals(f.MergeProcessFlowChartFlowRelationId))
                && currentHangerProductFlowChart.FlowType.Value == f.FlowType.Value
                && currentHangerProductFlowChart.StatingNo.Value != f.StatingNo.Value
                && f.StatingNo.Value == -1
                );
                foreach (var hpfc in meregeProcessFlowChartList)
                {
                    if (!filterFlowNoList.Contains(hpfc.FlowNo?.Trim()))
                    {
                        filterFlowNoList.Add(hpfc.FlowNo?.Trim());
                        hpfc.ProcessOrderNo = currentHangerProductFlowChart.ProcessOrderNo;
                        hpfc.PColor = currentHangerProductFlowChart.PColor;
                        hpfc.PSize = currentHangerProductFlowChart.PSize;
                        hpfc.Po = currentHangerProductFlowChart.Po;
                        hpfc.StatingCapacity = currentHangerProductFlowChart.StatingCapacity;

                        hpfc.MainTrackNumber = currentHangerProductFlowChart.MainTrackNumber;
                        hpfc.IncomeSiteDate = currentHangerProductFlowChart.IncomeSiteDate;
                        hpfc.CompareDate = currentHangerProductFlowChart.CompareDate;
                        hpfc.OutSiteDate = currentHangerProductFlowChart.OutSiteDate;
                        hpfc.StatingNo = currentHangerProductFlowChart.StatingNo;
                        hpfc.StatingId = currentHangerProductFlowChart.StatingId;
                        hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
                        hpfc.FlowType = currentHangerProductFlowChart.FlowType;
                        hpfc.IsFlowSucess = true;
                        hpfc.EmployeeName = currentHangerProductFlowChart.EmployeeName;
                        hpfc.CardNo = currentHangerProductFlowChart.CardNo;
                        hpfc.EmployeeNo = currentHangerProductFlowChart.EmployeeNo;
                        var meHangerFlowChart = BeanUitls<HangerProductFlowChart, HangerProductFlowChartModel>.Mapper(hpfc);
                        DapperHelp.Add(meHangerFlowChart);

                        RecordEmployeeProductOut(hpfc);

                        var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductFlowChartModel>.Mapper(hpfc);
                        statingHangerProductItem.Id = null;
                        statingHangerProductItem.SiteNo = currentHangerProductFlowChart.StatingNo?.ToString();
                        statingHangerProductItem.IsReturnWorkFlow = null == currentHangerProductFlowChart?.FlowType ? false : (currentHangerProductFlowChart?.FlowType == 1 ? true : false);
                        statingHangerProductItem.IsReworkSourceStating = currentHangerProductFlowChart?.IsReworkSourceStating;
                        statingHangerProductItem.FlowChartd = currentHangerProductFlowChart.ProcessChartId;
                        statingHangerProductItem.ProcessFlowId = currentHangerProductFlowChart.FlowId;
                        statingHangerProductItem.ProcessFlowCode = currentHangerProductFlowChart.FlowCode;
                        statingHangerProductItem.ProcessFlowName = currentHangerProductFlowChart.FlowName;
                        statingHangerProductItem.Id = DapperHelp.Add(statingHangerProductItem);
                        tcpLogInfo.Info("【出战产量记录】【合并工序--->记录站点衣架生产记录】");
                    }
                    ////记录分配工序
                    ////var meregeProcessFlow = new HangerStatingAllocationItem();
                    //var meregeProcessFlow = BeanUitls<HangerStatingAllocationItem, HangerProductFlowChart>.Mapper(meHangerFlowChart);
                    //meregeProcessFlow.Memo = "合并工序";
                    //meregeProcessFlow.Status = 1;
                    //meregeProcessFlow.HangerType = 0;
                    //meregeProcessFlow.IsSucessedFlow = true;
                    //meregeProcessFlow.SiteNo = meHangerFlowChart.StatingNo?.ToString();
                    //DapperHelp.Add(meHangerFlowChart);
                }
            }
            catch (Exception ex)
            {
                redisLog.ErrorFormat("【出战产量记录】合并工序产量记录异常:{0}", ex);
            }
        }
        void RecordEmployeeProductOut(HangerProductFlowChartModel hpfModole, bool isEndFlow = false)
        {

            var pQueryService = new ProductsQueryServiceImpl();
            //pQueryService.CheckStatingIsLogin(StatingNo);
            var statingEmList = pQueryService.GetEmployeeLoginInfoList(hpfModole.StatingNo.Value.ToString(), hpfModole.MainTrackNumber.Value);
            var emStatingInfo = statingEmList.Count > 0 ? statingEmList[0] : null;
            //var hsaItem = StatingHangerProductItemDao.Instance.GetById(statingHangerProductItemId);
            var efpModel = BeanUitls<EmployeeFlowProduction, HangerProductFlowChartModel>.Mapper(hpfModole);
            efpModel.Id = null;
            efpModel.SizeNum = null != hpfModole.Num ? int.Parse(hpfModole.Num) : 0;
            efpModel.ProcessFlowId = hpfModole.FlowId;
            efpModel.ProcessFlowCode = hpfModole.FlowCode;
            efpModel.ProcessFlowName = hpfModole.FlowName;
            efpModel.HangerType = hpfModole.FlowType;
            efpModel.FlowChartd = hpfModole.ProcessChartId;
            efpModel.SiteNo = "" + hpfModole.StatingNo.Value;
            efpModel.CardNo = hpfModole.CardNo?.Trim();
            efpModel.EmployeeId = emStatingInfo?.EmployeeId;
            efpModel.EmployeeName = emStatingInfo?.RealName?.Trim();
            //EmployeeFlowProductionDao.Instance.Insert(efpModel);
            DapperHelp.Add(efpModel);

            //var ppChart = CANProductsService.Instance.GetCompleteHangerProductFlowChart(mainTrackNumber, hangerNo, statingNo);
            //if (null == ppChart)
            //{
            //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", mainTrackNumber, hangerNo, statingNo));
            //    errorLog.Error("【衣架出站】", ex);
            //    return;
            //}
            //ppChart.EmployeeName = emStatingInfo.RealName;
            //ppChart.CardNo = emStatingInfo.CardNo;
            ////HangerProductFlowChartDao.Instance.Edit(ppChart);
            //DapperHelp.Edit(ppChart);

            if (isEndFlow)
            {
                /// CANProductsService.Instance.CopyHangerProductChart(hpfModole.HangerNo);
            }
        }
        private void HangerInSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            //var inResult = JsonConvert.DeserializeObject<HangerInSiteResult>(arg2);

            ////将生产中的制品工序加1
            //var sql = new StringBuilder("select * from WaitProcessOrderHanger where HangerNo=?");
            //var waitProcessOrderHanger = QueryForObject<WaitProcessOrderHanger>(sql, null, false, inResult.HangerNo); //WaitProcessOrderHangerDao.Instance.GetAll().Where(f => f.HangerNo.Equals(hangerNo));
            //waitProcessOrderHanger.IsIncomeSite = true;
            //waitProcessOrderHanger.FlowIndex++;
            //WaitProcessOrderHangerDao.Instance.Update(waitProcessOrderHanger);

            ////记录衣架生产的工序
            //ProductsQueryServiceImpl productsQueryService = new ProductsQueryServiceImpl();
            //var pIndex = productsQueryService.GetNextIndex(inResult.HangerNo, "HangerProductItem");
            //var hangerProductItem = BeanUitls<HangerProductItem, WaitProcessOrderHanger>.Mapper(waitProcessOrderHanger);
            //hangerProductItem.Id = null;
            //hangerProductItem.IncomeSiteDate = DateTime.Now;
            //hangerProductItem.SiteNo = inResult.StatingNo?.Trim();
            //hangerProductItem.HpIndex = pIndex;
            //hangerProductItem.MainTrackNumber = short.Parse(inResult.StatingNo);
            //hangerProductItem.FlowIndex = waitProcessOrderHanger.FlowIndex;
            //HangerProductItemDao.Instance.Save(hangerProductItem);

            ////更新站点进站时间
            //var sqlHangerStatingAll = new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=? and NextSiteNo=?");
            //var hangerStatingAllocationItem = QueryForObject<HangerStatingAllocationItem>(sqlHangerStatingAll, null, false, inResult.HangerNo, inResult.StatingNo);
            //hangerStatingAllocationItem.IncomeSiteDate = DateTime.Now;
            //hangerStatingAllocationItem.Status = 1;//将分配关系标记为已处理
            //HangerStatingAllocationItemDao.Instance.Update(hangerStatingAllocationItem);

            ////更新衣架生产工艺图制作站点的信息
            //var ppChart = CANProductsService.Instance.GetHangerProductFlowChart(inResult.MainTrackNumber, inResult.HangerNo, inResult.StatingNo);
            //if (null == ppChart)
            //{
            //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", inResult.MainTrackNumber, inResult.HangerNo, inResult.StatingNo));
            //    errorLog.Error("【衣架进站】", ex);
            //    return;
            //}
            //ppChart.IncomeSiteDate = DateTime.Now;
            //ppChart.Status = 1;
            //HangerProductFlowChartDao.Instance.Update(ppChart);
        }

        private void UpdateHangerProductItemAction(RedisChannel arg1, RedisValue arg2)
        {
            var hangerProductItem = Newtonsoft.Json.JsonConvert.DeserializeObject<HangerProductItem>(arg2);
            //HangerProductItemDao.Instance.Edit(hangerProductItem);
            DapperHelp.Edit(hangerProductItem);
        }

        private void UpdateWaitProcessOrderHangerAction(RedisChannel arg1, RedisValue arg2)
        {
            var wp = Newtonsoft.Json.JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
            //WaitProcessOrderHangerDao.Instance.Edit(wp);
            DapperHelp.Edit(wp);
        }

        private void RecordStatingHangerProductItemAction(RedisChannel arg1, RedisValue arg2)
        {
            var statingHangerProductItem = JsonConvert.DeserializeObject<StatingHangerProductItem>(arg2);
            //StatingHangerProductItemDao.Instance.Insert(statingHangerProductItem);
            DapperHelp.Add(statingHangerProductItem);
        }
        private void UpdateHangerProcessFlowChartAction(RedisChannel arg1, RedisValue arg2)
        {
            var hangerChart = JsonConvert.DeserializeObject<HangerProductFlowChart>(arg2);
            //HangerProductFlowChartDao.Instance.Edit(hangerChart);
            DapperHelp.Edit(hangerChart);
        }

        private void HangerAllocationAction(RedisChannel arg1, RedisValue arg2)
        {
            //var hsaItemNext = Newtonsoft.Json.JsonConvert.DeserializeObject<HangerStatingAllocationItem>(arg2);
            ////HangerStatingAllocationItemDao.Instance.Insert(hsaItemNext);
            //DapperHelp.Add(hsaItemNext);

        }

        private void HangerProcessFlowChartAction(RedisChannel arg1, RedisValue arg2)
        {
            var hpfcList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HangerProductFlowChart>>(arg2);
            foreach (var hpfc in hpfcList)
            {
                //HangerProductFlowChartDao.Instance.Insert(hpfc);
                DapperHelp.Add(hpfc);
            }
        }

        private void WaitProcessOrderHangerAction(RedisChannel arg1, RedisValue arg2)
        {
            var wp = Newtonsoft.Json.JsonConvert.DeserializeObject<WaitProcessOrderHanger>(arg2);
            //WaitProcessOrderHangerDao.Instance.Insert(wp);
            //log.Info("WaitProcessOrderHangerAction..................");
            DapperHelp.Add(wp);
            //log.Info("WaitProcessOrderHangerAction..................1111");
        }

        private void StatingCapacityEdit(RedisChannel arg1, RedisValue arg2)
        {
            var StatingModel = JsonConvert.DeserializeObject<StatingModel>(arg2);
            if (StatingModel != null)
            {
                var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
                var key = string.Format("{0}:{1}", StatingModel.MainTrackNumber.Value, StatingModel.StatingNo?.Trim());

                //更新数据库
                //string sql = "update Stating set Capacity = ? where MainTrackNumber = ? and StatingNo = ?";
                //int effCount = ProductionLineSetServiceImpl.Instance.ExecuteUpdate(sql, StatingModel.Capacity, StatingModel.MainTrackNumber, StatingModel.StatingNo);

                string sql = "update Stating set Capacity = @Capacity where MainTrackNumber = @MainTrackNumber and StatingNo = @StatingNo";
                int effCount = DapperHelp.Execute(sql, StatingModel);
                if (effCount > 0)
                {
                    //更新Redis缓存
                    if (dic.ContainsKey(key))
                    {
                        dic[key].Capacity = StatingModel.Capacity;
                    }
                    //发送成功指令
                    // CANTcp.client.SCMModifyStatingCapacitySuccess(StatingModel.MainTrackNumber.Value, StatingModel.StatingNo, StatingModel.Capacity.Value);
                    CANTcpServer.server.SCMModifyStatingCapacitySuccess(StatingModel.MainTrackNumber.Value, StatingModel.StatingNo, StatingModel.Capacity.Value);
                }
            }
        }

        //private void UpdateStatingTypeAction(RedisChannel arg1, RedisValue arg2)
        //{

        //}
        ///
        private void ProcessStatingAllocationLog(RedisChannel arg1, RedisValue arg2)
        {
            //var SystemLogs = JsonConvert.DeserializeObject<SystemLogs>(arg2);
            SystemLogs log = new SystemLogs()
            {
                LogInfo = arg2,
                Name = SystemLogEnum.Allocation.ToString(),
            };

            SystemLogService.Instance.AddLogs(log);
        }


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

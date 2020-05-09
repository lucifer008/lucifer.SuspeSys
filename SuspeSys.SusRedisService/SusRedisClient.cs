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

namespace SuspeSys.SusRedisService.SusRedis
{
    /// <summary>
    /// Redis
    /// </summary>
    public class SusRedisClientDecode 
    {
        public readonly static SusRedisClientDecode Instance = new SusRedisClientDecode();
         protected static readonly ILog log = LogManager.GetLogger("RedisLogInfo");
        public static RedisTypeFactory RedisTypeFactory = null;
        //public static RedisTypeFactory BaseRedisTypeFactory = null;
        public static ConnectionMultiplexer ConnectionMultiplexer = null;
        public static ISubscriber subcriber = null;
        public static readonly string url = System.Configuration.ConfigurationManager.AppSettings["RedisIp"]; //"localhost";
        public static readonly int portNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RedisPort"]);
        protected static readonly ILog tcpLogInfo = LogManager.GetLogger("TcpLogInfo");
        protected static readonly ILog tcpLogError = LogManager.GetLogger("TcpErrorInfo");
        protected static readonly ILog tcpLogHardware = LogManager.GetLogger("TcpHardwareInfo");
        protected static readonly ILog errorLog = LogManager.GetLogger("Error");
        protected static readonly ILog timersLog = LogManager.GetLogger("Timers");
        protected static readonly ILog redisLog = LogManager.GetLogger("RedisLogInfo");
        protected static readonly ILog cacheInfo = LogManager.GetLogger("CacheInfo");
        protected static readonly ILog montorLog = LogManager.GetLogger("MontorLogger");
        private SusRedisClientDecode() { }
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

                    //订阅消息:消息对所有订阅的客户端都有效
                    subcriber = ConnectionMultiplexer.GetSubscriber();
                    //subcriber.Subscribe(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, (channel, message) => {
                    //    Console.WriteLine((string)message);
                    //});
                    

                    //subcriber.Subscribe(SusRedisConst.HANGER_IN_SITE_ACTION, HangerInSiteAction);
                    //subcriber.Subscribe(SusRedisConst.HANGER_OUT_SITE_ACTION, HangerOutSiteAction);
                    subcriber.Subscribe(SusRedisConst.PUBLIC_TEST,PublicTestAction);
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

        private void PublicTestAction(RedisChannel arg1, RedisValue arg2)
        {
            redisLog.InfoFormat("CLient->PublicTestAction-->{0}", arg2.ToString());
        }

        private void HangerOutSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            //throw new NotImplementedException();
        }

        private void HangerInSiteAction(RedisChannel arg1, RedisValue arg2)
        {
            //throw new NotImplementedException();
        }

        //衣架生产轨迹记录

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

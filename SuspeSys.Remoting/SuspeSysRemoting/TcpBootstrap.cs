using DevExpress.XtraEditors;
using log4net;
using SuspeSys.Service.Impl;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.SusCache;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Service.Impl.SusTcp;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace SuspeSys.Remoting.SuspeSysRemoting
{
    /// <summary>
    /// tcp:端口必须手动开放，否则会被防火墙拦住
    /// </summary>
    public class TcpBootstrap
    {
        private static readonly ILog log = LogManager.GetLogger("LogLogger");
        private static readonly ILog redisLog = LogManager.GetLogger("RedisLogInfo");
        private static readonly ILog tcpLog = LogManager.GetLogger("TcpLogInfo");
        private static readonly TcpBootstrap instance = new TcpBootstrap();
        private TcpBootstrap() { }

        public static TcpBootstrap Instance
        {
            get { return instance; }
        }
        public static readonly int basePort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SusTcpPort"]);
        static bool isStart = false;
        ListBoxControl lbMessage = null;
        public void RegsiterRemotingByCode(ListBoxControl _lbMessage, bool isServiceStart = false, string baseApplicationPath = null)
        {
            try
            {
                if (null != _lbMessage)
                {
                    lbMessage = _lbMessage;
                }
                if (!isStart)
                {

                    SuspeApplication.Init(baseApplicationPath, isServiceStart);
                    //SuspeApplication.isStartService = isServiceStart;
                    //SuspeApplication.BaseApplicationPath = SusBootstrap.BaseApplicationPath;

                   
                    //TcpServerChannel channel = new TcpServerChannel(basePort);
                    //ChannelServices.RegisterChannel(channel);
                    //RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service.Impl.Products.ProductsServiceImpl), "TcpProductsService", WellKnownObjectMode.SingleCall);
                    BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
                    serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
                    TcpServerChannel channelTestService = new TcpServerChannel("SuspeSys Channel", basePort, serverProvider);
                    ChannelServices.RegisterChannel(channelTestService, false);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service.Impl.Products.ProductsServiceImpl), "TcpProductsService", WellKnownObjectMode.Singleton);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service.Impl.SusTcp.MainTrackServiceImpl), "TcpMainTrackService", WellKnownObjectMode.Singleton);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service.Impl.SusTcp.StatingServiceImpl), "TcpStatingService", WellKnownObjectMode.Singleton);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(ProcessFlowChartCacheServiceImpl), "ProcessFlowChartCacheService", WellKnownObjectMode.Singleton);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(ProductRealtimeInfoServiceImpl), "ProductRealtimeInfoService", WellKnownObjectMode.Singleton);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(ReloadCacheServiceImpl), "ReloadCacheService", WellKnownObjectMode.Singleton);

                    var sucMessage = string.Format("悬挂业务端口{0} 开启成功!", basePort);
                    log.Info(sucMessage);
                    isStart = true;
                    if (!isServiceStart)
                    {
                        _lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
                    }
                    //InitRedis();

                    LoadCache(isServiceStart);

                }
                else
                {
                    LoadCache(isServiceStart);
                    var sucMessage = string.Format("【悬挂业务端口已经开启!】");
                    _lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
                }
            }
            catch (Exception ex)
            {
                var exMessage = string.Format("【悬挂业务端口开启失败,失败原因:{0}】", ex.Message);
                _lbMessage?.Invoke(new EventHandler(this.AddMessage), exMessage, null);
                log.Error("【悬挂业务端口开启失败】", ex);
            }
        }
        //void InitRedis(bool isServiceStart = false)
        //{
        //    var sucMessage = string.Format("正在开启Redis...");
        //    if (!isServiceStart)
        //    {
        //        lbMessage.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
        //    }
        //    redisLog.Info(sucMessage);
        //    SusRedisClient.Instance.Init();
        //    sucMessage = string.Format("Redis开启完成!");
        //    redisLog.Info(sucMessage);
        //    if (!isServiceStart)
        //    {
        //        lbMessage.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
        //    }
        //}
        void LoadCache(bool isServiceStart = false)
        {
            var sucMessage = string.Format("正在加载预处理数据...");
            redisLog.Info(sucMessage);
            if (!isServiceStart)
            {
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
            }
            SusCacheBootstarp.Instance.Init();
            sucMessage = string.Format("预处理数据加载完成!");
            redisLog.Info(sucMessage);
            if (!isServiceStart)
            {
                lbMessage?.Invoke(new EventHandler(this.AddMessage), sucMessage, null);
            }
        }
        void AddMessage(object sender, EventArgs e)
        {
            var index = lbMessage.Items.Count + 1;
            var data = string.Format("{0}--->{1}", index, sender.ToString());
            lbMessage.Items.Add(data);
        }
    }
}

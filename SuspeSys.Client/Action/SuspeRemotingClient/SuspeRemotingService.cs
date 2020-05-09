using SuspeSys.Service.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.SuspeRemotingClient
{
    public class SuspeRemotingService
    {
        public readonly static SuspeRemotingService Instance = new SuspeRemotingService();
        static int port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SussRepmotingPort"]);
        static string remotingTcpURL = string.Format("tcp://{0}:{1}", System.Configuration.ConfigurationManager.AppSettings["SusRepmotingIP"], port);

        static string productServerURL = string.Format("{0}/TcpProductsService", remotingTcpURL);
        static string mainTrackServiceURL = string.Format("{0}/TcpMainTrackService", remotingTcpURL);
        static string statingServiceURL = string.Format("{0}/TcpStatingService", remotingTcpURL);
        static string cacheFlowChartServiceURL = string.Format("{0}/ProcessFlowChartCacheService", remotingTcpURL);
        static string productRealtimeInfoServiceURL = string.Format("{0}/ProductRealtimeInfoService", remotingTcpURL);
        static string reloadCacheURL = string.Format("{0}/ReloadCacheService", remotingTcpURL);
        private SuspeRemotingService() { }
        static TcpClientChannel tcpClientChannel;
        public void Init() {
            // if(ChannelServices.re)
            if (null != tcpClientChannel) {
                ChannelServices.UnregisterChannel(tcpClientChannel);
            }
            tcpClientChannel = new TcpClientChannel();
            ChannelServices.RegisterChannel(tcpClientChannel, false);
            //System.Runtime.Remoting.ChannelServices.RegisterChannel(new TcpChannel());
             pcpProductsService = (SuspeSys.Service.Products.IProductsService)Activator.GetObject(typeof(SuspeSys.Service.Products.IProductsService), productServerURL);
            mainTrackService= (SuspeSys.Service.SusTcp.IMainTrackService)Activator.GetObject(typeof(SuspeSys.Service.SusTcp.IMainTrackService), mainTrackServiceURL);
            statingService = (SuspeSys.Service.SusTcp.IStatingService)Activator.GetObject(typeof(SuspeSys.Service.SusTcp.IStatingService), statingServiceURL);
            processFlowChartCacheService= (IProcessFlowChartCacheService)Activator.GetObject(typeof(IProcessFlowChartCacheService), cacheFlowChartServiceURL);
            productRealtimeInfoService = (IProductRealtimeInfoService)Activator.GetObject(typeof(IProductRealtimeInfoService), productRealtimeInfoServiceURL);
            reloadCacheService = (IReloadCacheService)Activator.GetObject(typeof(IReloadCacheService), reloadCacheURL);
        }
        public static  SuspeSys.Service.Products.IProductsService pcpProductsService;
        public static SuspeSys.Service.SusTcp.IMainTrackService mainTrackService;
        public static SuspeSys.Service.SusTcp.IStatingService statingService;
        public static IProcessFlowChartCacheService processFlowChartCacheService;
        public static IProductRealtimeInfoService productRealtimeInfoService;
        public static IReloadCacheService reloadCacheService;
    }
}

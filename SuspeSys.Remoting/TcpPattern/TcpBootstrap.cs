using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Remoting.TcpPattern
{
    /// <summary>
    /// tcp:端口必须手动开放，否则会被防火墙拦住
    /// </summary>
    public class TcpBootstrap
    {
        private static readonly TcpBootstrap instance = new TcpBootstrap();
        private TcpBootstrap() { }

        public static TcpBootstrap Instance {
            get { return instance; }
        }
       public void RegsiterRemotingByCode()
        {
            int basePort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["remotingPort"]);
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
            TcpServerChannel channelTestService = new TcpServerChannel("TestService Channel", basePort, serverProvider);
            ChannelServices.RegisterChannel(channelTestService, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SuspeSys.Service.Impl.CatService), "CatService", WellKnownObjectMode.Singleton);
        }
    }
}

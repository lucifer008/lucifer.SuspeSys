using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using log4net.Config;
using log4net;
using Sus.Net.Client;
using Sus.Net.Common.Entity;
using Sus.Net.Client.Sockets;
using Sus.Net.Common.Event;
using System.Threading;
using SusNet.Common.Utils;
using SusNet.Common.Message;
using SuspeSys.AuxiliaryTools;
using Sus.Net.Common.Constant;
using SuspeSys.Remoting;
using SuspeSys.Service.Impl.SusRedis;

namespace SuspeSys.StressTestApp
{
   // [TestClass()]
    public class QueryTestBase
    {
        public static ILog log = null;
       // [TestInitialize]
        public void Init()
        {
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            log = LogManager.GetLogger(typeof(QueryTestBase));
            log.Info("info---->hibernate init....");

            Console.WriteLine("Test Init....");

            //跟踪nhibernate执行的sql配置
        //    HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            SusBootstrap.Instance.Start(null, false, null, true);
            //
            SuspeSys.Service.Impl.SusRedis.SusRedisClient.Instance.Init();
            SusRedisClient.Instance.Init();
         
            Service.Impl.SusRedis.NewSusRedisClient.Instance.Init();
        }
     
    }
}

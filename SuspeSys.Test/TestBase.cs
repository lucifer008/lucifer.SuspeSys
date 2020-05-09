using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.SusRedis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Test
{
    public class TestBase
    {
        public static ILog log = null;
        [TestInitialize]
        public void Init()
        {
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            log = LogManager.GetLogger(typeof(TestBase));
            log.Info("info---->hibernate init....");

            Console.WriteLine("Test Init....");

            //跟踪nhibernate执行的sql配置
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            //

            //
            SusRedisClient.Instance.Init();
            Service.Impl.SusRedis.NewSusRedisClient.Instance.Init();
        }
    }
}

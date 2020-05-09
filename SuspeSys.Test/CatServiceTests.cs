using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Tests
{
    [TestClass()]
    public class CatServiceTests
    {
        //private readonly static ILog log = LogManager.GetLogger(typeof(CatServiceTests));
        private static ILog log = null;
        [TestInitialize]
        public void Init()
        {
            
            //var log4netFileInfo = new FileInfo("log4net.cfg.xml");
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            log = LogManager.GetLogger("LogLogger");
            log.Info("info---->hibernate init....");

            //var logg = LogManager.GetLogger("AdoNetAppender");
            //logg.Info("-----------------AdoNetAppender-----------------------------");

            Console.WriteLine("Init...."); 

            ////跟踪nhibernate执行的sql配置
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }
        [TestMethod()]
        public void TestCatTest()
        {
            //for (int i=0;i<1000;i++) {
            //    Thread.Sleep(1000);
            //    CatService.Instance.AddCat();
            //}

            //var d = NHibernate.Dialect.MsSql2012Dialect;
           // CatService.Instance.AddCat();

            //CatService.Instance.TestTransaction();


            //var cats = CatService.Instance.GetCatList();

            //foreach (var item in cats)
            //{
            //    log.Info("---->>>>"+item.Name + " :" + item.Weight);
            //    Console.WriteLine(item.Name + " :" + item.Weight);
            //}
            log.Info("---------------------------success!");
            log.Info("---------------------------success!");
        }
    }
}
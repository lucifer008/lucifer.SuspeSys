using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using System.IO;
using log4net;
using System.Threading;
using SusNet.Common.Utils;

namespace SuspeSys.CoreTest
{
    [TestClass]
    public class UnitTest1
    {
        public static ILog log = null;
        [TestInitialize]
        public void Init() {
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

            log = LogManager.GetLogger(typeof(UnitTest1));
            log.Info("info---->hibernate init....");

            Console.WriteLine("Test Init....");

        }
        [TestMethod]
        public void TestMethod1()
        {
            log.Info("begin------------------------------------------");
            Thread thread = new Thread(TestLogThread);
            thread.Start();
            Thread.CurrentThread.Join(20000);
            //Thread.CurrentThread.Abort(20000);
            log.Info("success------------------------------------------");
        }

        private void TestLogThread()
        {
            for (var index=1;index<=1000;index++) {
                log.Info($"Thread----->{index}");
                Thread.Sleep(1000);
            }
            log.Info("Thread----->完成!");
        }
        [TestMethod]
        public void TestHex()
        {
            var ht = HexHelper.TenToHexString2Len(17);
        }
    }
}

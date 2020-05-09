using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    /// <summary>
    /// log4net 初始化类
    /// </summary>
    public class Log4netHelper
    {
        private static readonly Log4netHelper instance = new Log4netHelper();
        private Log4netHelper() { }
        public static Log4netHelper Instance {
            get { return instance; }
        }
        public void InitLog4net(string fileName=null) {
            if (string.IsNullOrEmpty(fileName)) {
                fileName = string.Format("Config/log4net.cfg.xml");
                var fileInfo = new FileInfo(fileName);
                XmlConfigurator.Configure(fileInfo);
                return;
            }
            XmlConfigurator.Configure(new FileInfo(fileName));
        }
        public ILog GetLog<T>() {
           return LogManager.GetLogger(typeof(T));
        }
    }
}

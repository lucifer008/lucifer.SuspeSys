using log4net.Config;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action
{
    /// <summary>
    /// 初始化化全局配置
    /// </summary>
   public class SuspeApplicationAction
    {
        static SuspeApplicationAction() {
            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);

        }
    }
}

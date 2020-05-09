using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.AuxiliaryTools
{
    public class MainThreadTools : ToolsBase
    {
        public static Thread CurrentThread = null;
        public static readonly MainThreadTools Instance = new MainThreadTools();
        static ManualResetEvent _event = new ManualResetEvent(true);
        private static ILog log = null;
        private MainThreadTools()
        {
            log = LogManager.GetLogger(typeof(MainThreadTools));
        }
        public void SetCurrentThread(Thread thread)
        {
            if (isUnitTest)
            {
                if (null == CurrentThread)
                {
                    CurrentThread = thread;
                    log.Info($"【线程跟踪】主线程已设置,线程ManagedThreadId={thread.ManagedThreadId}");
                }
            }
        }
        public void SuspendAndResumeThreadHandler(object state)
        {
            while (true)
            {
                log.Info($"【线程跟踪】主线程即将被阻止.....,线程ManagedThreadId={CurrentThread.ManagedThreadId}");
                _event.WaitOne();
                //do operations here
            }
        }
        public void SuspendThread()
        {
            if (isUnitTest)
            {
                log.Info($"【线程跟踪】主线程即将被挂起.....,线程ManagedThreadId={CurrentThread.ManagedThreadId}");
                _event.Reset();
            }
        }
        public void ResumeThread()
        {
            if (isUnitTest)
            {
                log.Info($"【线程跟踪】主线程即将被恢复.....,线程ManagedThreadId={CurrentThread.ManagedThreadId}");
                _event.Reset();
            }
        }
        public void Join(int millisecondsTimeout)
        {
            if (isUnitTest)
            {
                log.Info($"【线程跟踪】阻止线程{millisecondsTimeout/1000} 秒...........................,线程ManagedThreadId={CurrentThread.ManagedThreadId}");
                CurrentThread.Join(millisecondsTimeout);
            }
        }
        public void SetLog(ILog _log)
        {
            log = _log;
        }
    }
}

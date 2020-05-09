using Sus.Net.Common.SusBusMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.CallManagement
{
    /// <summary>
    /// 呼叫管理适配器
    /// </summary>
   public class CallManagementAdapter
    {
        private CallManagementAdapter() { }
        public readonly static CallManagementAdapter Instance = new CallManagementAdapter();
        private readonly static object locObj = new object();
        public virtual void DoService(CallManagementStartRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                CallManagementHandler.Instance.Process(request, tcpClient);
            }
        }
        public virtual void DoService(CallStopRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                CallManagementHandler.Instance.Process(request, tcpClient);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Sus.Net.Common.SusBusMessage;

namespace SuspeSys.Service.Impl.Core.CallManagement
{
    /// <summary>
    /// 呼叫管理处理器
    /// </summary>
   public class CallManagementHandler
    {
        public static readonly CallManagementHandler Instance = new CallManagementHandler();
        private CallManagementHandler() { }

        internal void Process(Object request, TcpClient tcpClient)
        {
            if (request is CallManagementStartRequestMessage)
            {
                CallManagementService.Instance.CallManagementAction((CallManagementStartRequestMessage)request);
            }
            if (request is CallStopRequestMessage)
            {
                CallManagementService.Instance.CallStopManagementAction((CallStopRequestMessage)request);
            }
        }
    }
}

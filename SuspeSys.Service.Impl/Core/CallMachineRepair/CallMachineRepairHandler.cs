using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Sus.Net.Common.SusBusMessage;

namespace SuspeSys.Service.Impl.Core.CallMachineRepair
{
    /// <summary>
    /// 呼叫机修处理器
    /// </summary>
    public class CallMachineRepairHandler
    {

        public static readonly CallMachineRepairHandler Instance = new CallMachineRepairHandler();
        private CallMachineRepairHandler() { }

        internal void Process(Object request, TcpClient tcpClient)
        {
            if (request is CallMachineRepairStartRequestMessage) {
                CallMachineRepairService.Instance.CallMachineRepairStartAction((CallMachineRepairStartRequestMessage)request);
            }
            if (request is CallStopRequestMessage)
            {
                CallMachineRepairService.Instance.CallStopMachineRepairAction((CallStopRequestMessage)request);
            }
        }
    }
}

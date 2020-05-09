using Sus.Net.Common.SusBusMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.CallMachineRepair
{
    /// <summary>
    /// 呼叫机修适配器
    /// </summary>
    public class CallMachineRepairAdapter
    {
        private CallMachineRepairAdapter() { }
        public readonly static CallMachineRepairAdapter Instance = new CallMachineRepairAdapter();
        private readonly static object locObj = new object();
        public virtual void DoService(CallMachineRepairStartRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                CallMachineRepairHandler.Instance.Process(request, tcpClient);
            }
        }
        public virtual void DoService(CallStopRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                CallMachineRepairHandler.Instance.Process(request, tcpClient);
            }
        }
    }
}

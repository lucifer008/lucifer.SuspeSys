using Sus.Net.Common.SusBusMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.F2
{
    public class F2Adapter
    {
        private F2Adapter() { }
        public readonly static F2Adapter Instance = new F2Adapter();
        private readonly static object locObj = new object();
        public virtual void DoService(F2AssignHangerNoUploadRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
               F2Handler.Instance.Process(request, tcpClient);
            }
        }
        public virtual void DoService(F2AssignRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                F2Handler.Instance.Process(request, tcpClient);
            }
        }
    }
}

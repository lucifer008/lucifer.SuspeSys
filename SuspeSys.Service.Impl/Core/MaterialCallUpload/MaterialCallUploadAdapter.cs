using Sus.Net.Common.SusBusMessage;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{
    public class MaterialCallUploadAdapter : SusLog
    {
        private MaterialCallUploadAdapter() { }
        public readonly static MaterialCallUploadAdapter Instance = new MaterialCallUploadAdapter();
        private readonly static object locObj = new object();
        public virtual void DoService(MaterialCallUploadRequestMessage request, TcpClient tcpClient = null)
        {
            lock (locObj)
            {
                MaterialCallUploadHandler.Instance.Process(request, tcpClient);
            }
        }
    }
}

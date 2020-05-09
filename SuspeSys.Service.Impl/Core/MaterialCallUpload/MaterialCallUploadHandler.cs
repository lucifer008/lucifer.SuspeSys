using Sus.Net.Common.SusBusMessage;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{
    public class MaterialCallUploadHandler
    {
        public static readonly MaterialCallUploadHandler Instance = new MaterialCallUploadHandler();
        private MaterialCallUploadHandler() { }

        public void Process(MaterialCallUploadRequestMessage request, TcpClient tcpClient = null)
        {
            MaterialCallUploadService.Instance.MaterialCallUploadHandler(request, tcpClient);
                
        }
    }
}

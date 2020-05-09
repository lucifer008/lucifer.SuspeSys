using Sus.Net.Common.SusBusMessage;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{
    public class MaterialCallHandler
    {
        public static readonly MaterialCallHandler Instance = new MaterialCallHandler();
        private MaterialCallHandler() { }

        public void Process(MaterialCallRequestMessage request)
        {
            MaterialCallService.Instance.MaterialCallHandler(request);
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Bridge
{
    public class BridgeAHandler
    {
        private BridgeAHandler() { }
        public readonly static BridgeAHandler Instance = new BridgeAHandler();
    }
}

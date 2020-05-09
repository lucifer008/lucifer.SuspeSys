using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Bridge
{
    public class BridgeBHandler
    {
        private BridgeBHandler() { }
        public readonly static BridgeBHandler Instance = new BridgeBHandler();
    }
}

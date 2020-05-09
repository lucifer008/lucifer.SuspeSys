using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.FaultRepair
{
    public class FaultRepairHandler
    {
        private FaultRepairHandler() { }
        public static readonly FaultRepairHandler Instance = new FaultRepairHandler();
    }
}

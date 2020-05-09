using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.FaultRepair
{
    public class FaultRepairService
    {
        private FaultRepairService() { }
        public static readonly FaultRepairService Instance = new FaultRepairService();

        internal void FaultRepairUploadStartHandler(int mainTrackNuber, int statingNo)
        {
            throw new NotImplementedException();
        }
    }
}

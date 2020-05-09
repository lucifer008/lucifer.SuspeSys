using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    [Serializable]
    public class BridgeCacheModel
    {
        public string HangerNo { get; set; }
        public short MainTrackNumber { get; set; }
        public short StatingNo { get; set; }
    }
}

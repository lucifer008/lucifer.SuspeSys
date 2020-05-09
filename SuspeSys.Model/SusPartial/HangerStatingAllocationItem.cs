using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public partial class HangerStatingAllocationItem
    {
       // public string LineName { get; set; }
        public string StyleNo { get; set; }
        public string Unit { get; set; }
        public bool isMonitoringAllocation = false;
        /// <summary>
        /// 是否是F2指定分配
        /// </summary>
        public bool isF2AssgnAllocation = false;
        public int LastFlowIndex { get; set; }

        public bool IsStatingStorageOutStating { get; set; }
        public int OutMainTrackNumber { get; set; }

        /// <summary>
        /// 是否是桥接站出战分配
        /// </summary>
        public bool IsBridgeStatingOutStatingAllocate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    /// <summary>
    /// 分配比例
    /// </summary>
    public class Allocation
    {
        /// <summary>
        /// Key redisKey
        /// {ProcessChartId}:{FlowIndex}:{this.MainTrackNo}:{this.StatingNo} 
        /// </summary>
        public string Key
        {
            get
            {
                return $"{ProcessChartId}:{FlowNo}:{this.MainTrackNo}:{this.StatingNo}";
            }
        }


        /// <summary>
        /// 工序
        /// </summary>
        public string FlowNo { get; set; }

        /// <summary>
        /// 工序路线图Id
        /// </summary>
        public string ProcessChartId { get; set; }

        /// <summary>
        /// 主轨号
        /// </summary>
        public int MainTrackNo { get; set; } 

        /// <summary>
        /// 站点号
        /// </summary>
        public string StatingNo { get; set; }

        /// <summary>
        /// 分摊比例
        /// </summary>
        public decimal Proportion { get; set; }

        /// <summary>
        /// 已分配数
        /// </summary>
        public decimal HaveProportion { get; set; }

        /// <summary>
        /// 是否可以分配
        /// </summary>
        public bool CanAllocation { get; set; }
    }
}

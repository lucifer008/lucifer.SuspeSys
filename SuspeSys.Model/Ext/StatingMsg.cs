using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    [Serializable]
    public class StatingMsg
    {
        /// <summary>
        /// 是否是添加
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int? Capacity { get; set; }

        /// <summary>
        /// 站点编号
        /// </summary>
        public string StatingNo { get; set; }

        /// <summary>
        /// 站點類型
        /// </summary>
        public string StatingName { get; set; }
        /// <summary>
        /// 主轨
        /// </summary>
        public short MainTrackNumber { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// 负载监(:0正常，1:不可用)
        /// </summary>
        [Description("负载监(:0正常，1:不可用)")]
        public virtual bool IsLoadMonitor { get; set; }
    }
}

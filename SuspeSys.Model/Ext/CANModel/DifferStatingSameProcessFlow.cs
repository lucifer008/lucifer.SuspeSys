using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    /// <summary>
    /// 同工序不同站点
    /// </summary>
    [Serializable]
    public class DifferStatingSameProcessFlow
    {
        /// <summary>
        /// 进站站点
        /// </summary>
        public string IncomeStatingNo { set; get; }
        /// <summary>
        /// 进站主轨
        /// </summary>
        public string IncomeStatingMainTrackNumber { set; get; }

        /// <summary>
        /// 出站站点
        /// </summary>
        public string OutStatingStatingNo { set; get; }

        /// <summary>
        /// 出站主轨
        /// </summary>
        public string OutStatingMainTrackNumber { set; get; }

        /// <summary>
        /// 衣架号
        /// </summary>
        public string HangerNo { set; get; }
    }
}

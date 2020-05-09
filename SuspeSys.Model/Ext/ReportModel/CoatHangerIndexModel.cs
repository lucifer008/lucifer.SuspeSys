using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.ReportModel
{
    [Serializable]
   public class CoatHangerIndexModel: HangerProductFlowChartModel
    {
        /// <summary>
        /// 是否最新分配
        /// </summary>
        public bool IsNewAllocation { set; get; }
        /// <summary>
        /// 是否在站内
        /// </summary>
        public bool IsInStating { set; get; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsSuccess { set; get; }
        /// <summary>
        /// 款式
        /// </summary>
        public string StyleNo { set; get; }
        public string ColorName { set; get; }
        public string SizeName { set; get; }
        public string PieceNum { set; get; }

        public virtual string CheckResult { set; get; }
        public virtual string CheckInfo { set; get; }
        public virtual String ReworkDate1 { set; get; }

        public string GroupStatingName { set; get; }
    }
}

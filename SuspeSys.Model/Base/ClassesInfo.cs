using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{
    using SuspeSys.Domain.Base;
    using System.ComponentModel;

    /// <summary>
    /// 班次信息
    /// </summary>
    [Serializable]
    public class ClassesInfo : MetaData
    {
        public ClassesInfo() { }
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        [Description("班次")]
        public virtual string Num { get; set; }
        /// <summary>
        /// 班次类型(0:正常班次;1.加班班次;2.假日班次)
        /// </summary>
        [Description("班次类型(0:正常班次;1.加班班次;2.假日班次)")]
        public virtual string CType { get; set; }
        /// <summary>
        /// 时段1上班时间
        /// </summary>
        [Description("时段1上班时间")]
        public virtual DateTime? Time1GoToWorkDate { get; set; }
        /// <summary>
        /// 时段1下班时间
        /// </summary>
        [Description("时段1下班时间")]
        public virtual DateTime? Time1GoOffWorkDate { get; set; }
        /// <summary>
        /// 时段2上班时间
        /// </summary>
        [Description("时段2上班时间")]
        public virtual DateTime? Time2GoToWorkDate { get; set; }
        /// <summary>
        /// 时段2下班时间
        /// </summary>
        [Description("时段2下班时间")]
        public virtual DateTime? Time2GoOffWorkDate { get; set; }
        /// <summary>
        /// 时段3上班时间
        /// </summary>
        [Description("时段3上班时间")]
        public virtual DateTime? Time3GoToWorkDate { get; set; }
        /// <summary>
        /// 时段3下班时间
        /// </summary>
        [Description("时段3下班时间")]
        public virtual DateTime? Time3GoOffWorkDate { get; set; }
        public virtual bool? Time3IsOverTime { get; set; }
        public virtual DateTime? OverTimeIn { get; set; }
        public virtual DateTime? OverTimeOut { get; set; }
        /// <summary>
        /// 是否启用(0:启用;1:禁用)
        /// </summary>
        [Description("是否启用(0:启用;1:禁用)")]
        public virtual bool? IsEnabled { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{
    using SuspeSys.Domain.Base;
    using System.ComponentModel;

    /// <summary>
    /// 班次员工
    /// </summary>
    [Serializable]
    public class ClassesEmployee : MetaData
    {
        public virtual string Id { get; set; }
        public virtual Employee Employee { get; set; }


        public virtual ClassesInfo ClassesInfo { get; set; }

        /// <summary>
        /// 出勤日期
        /// </summary>
        [Description("日期")]
        public virtual int Week { get; set; }
        /// <summary>
        /// 出勤日期
        /// </summary>
        [Description("出勤日期")]
        public virtual DateTime? AttendanceDate { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        [Description("生效日期")]
        public virtual DateTime? EffectDate { get; set; }
    }
}

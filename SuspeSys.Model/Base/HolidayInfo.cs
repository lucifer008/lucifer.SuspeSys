using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{
    using SuspeSys.Domain.Base;
    using System.ComponentModel;

    /// <summary>
    /// 假日信息
    /// </summary>
    [Serializable]
    public class HolidayInfo : MetaData
    {
        /// <summary>
        /// 标识Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual double? OrderNo { get; set; }
        public virtual DateTime? HolidayDateTime { get; set; }
        public virtual bool? WorkDay { get; set; }
        public virtual string Memo { get; set; }
    }
}

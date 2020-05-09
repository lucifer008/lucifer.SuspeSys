using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 卡登录信息
    /// </summary>
    [Serializable]
    public partial class CardLoginInfo : MetaData {
        public virtual string Id { get; set; }
        public virtual CardInfo CardInfo { get; set; }
        /// <summary>
        /// 登录主轨
        /// </summary>
        [Description("登录主轨")]
        public virtual int? MainTrackNumber { get; set; }
        /// <summary>
        /// 登录站点(十进制，换算成16进制占1字节)
        /// </summary>
        [Description("登录站点(十进制，换算成16进制占1字节)")]
        public virtual string LoginStatingNo { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [Description("登录时间")]
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 下线时间
        /// </summary>
        [Description("下线时间")]
        public virtual DateTime? LogOutDate { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        [Description("是否在线")]
        public virtual bool? IsOnline { get; set; }
    }
}

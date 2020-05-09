using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 卡片信息(
    ///   1:衣架卡
    ///   2:衣车卡
    ///   3:机修卡
    ///   4:员工卡)
    /// </summary>
    [Serializable]
    public partial class CardInfo : MetaData {
        public CardInfo() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 卡号(换算十六进制占无字节)
        /// </summary>
        [Description("卡号(换算十六进制占无字节)")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 1:衣架卡
        ///   2:衣车卡
        ///   3:机修卡
        ///   4:员工卡
        /// </summary>
        [Description("1:衣架卡\r\n   2:衣车卡\r\n   3:机修卡\r\n   4:员工卡")]
        public virtual short? CardType { get; set; }
        /// <summary>
        /// 卡描述(1:衣架卡
        ///   2:衣车卡
        ///   3:机修卡
        ///   4:员工卡)
        /// </summary>
        [Description("卡描述(1:衣架卡\r\n   2:衣车卡\r\n   3:机修卡\r\n   4:员工卡)")]
        public virtual string CardDescription { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public virtual bool? IsEnabled { get; set; }
        /// <summary>
        /// 是否能多点登录
        /// </summary>
        [Description("是否能多点登录")]
        public virtual bool? IsMultiLogin { get; set; }
    }
}

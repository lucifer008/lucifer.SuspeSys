using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 员工
    /// </summary>
    [Serializable]
    public partial class Employee : MetaData {
        public Employee() { }
        public virtual string Id { get; set; }
        public virtual Department Department { get; set; }
        public virtual Area Area { get; set; }
        public virtual Organizations Organizations { get; set; }
        public virtual WorkType WorkType { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
        /// <summary>
        /// 员工编号(唯一)
        /// </summary>
        [Description("员工编号(唯一)")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public virtual string Password { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Description("员工姓名")]
        public virtual string RealName { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual byte[] HeadImage { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        public virtual byte? Sex { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Description("卡号")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Description("手机号")]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Description("是否有效")]
        public virtual bool? Valid { get; set; }
        /// <summary>
        /// 录用日期
        /// </summary>
        [Description("录用日期")]
        public virtual DateTime? EmploymentDate { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        [Description("住址")]
        public virtual string Address { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        [Description("入职时间")]
        public virtual DateTime? StartingDate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        [Description("离职日期")]
        public virtual DateTime? LeaveDate { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        [Description("银行卡号")]
        public virtual string BankCardNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

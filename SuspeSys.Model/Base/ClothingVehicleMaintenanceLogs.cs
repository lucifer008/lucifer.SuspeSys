using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    /// <summary>
    /// 衣车维修日志
    /// </summary>
    [Serializable]
    public partial class ClothingVehicleMaintenanceLogs : MetaData {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 维修日期
        /// </summary>
        [Description("维修日期")]
        public virtual DateTime? MaintenanceDate { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        public virtual string WorkShiop { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [Description("组别")]
        public virtual string GroupNo { get; set; }
        /// <summary>
        /// 站点
        /// </summary>
        [Description("站点")]
        public virtual string StatingNo { get; set; }
        /// <summary>
        /// 衣车编码
        /// </summary>
        [Description("衣车编码")]
        public virtual string ClothingVehicleNo { get; set; }
        /// <summary>
        /// 衣车卡号
        /// </summary>
        [Description("衣车卡号")]
        public virtual string ClothingVehicleCardNo { get; set; }
        /// <summary>
        /// 报修时间
        /// </summary>
        [Description("报修时间")]
        public virtual DateTime? RepairDate { get; set; }
        /// <summary>
        /// 报修故障号
        /// </summary>
        [Description("报修故障号")]
        public virtual string FaultCode { get; set; }
        /// <summary>
        /// 报修故障名称
        /// </summary>
        [Description("报修故障名称")]
        public virtual string FaultName { get; set; }
        /// <summary>
        /// 报修员工名称
        /// </summary>
        [Description("报修员工名称")]
        public virtual string FaultEmployeeName { get; set; }
        /// <summary>
        /// 报修用时(分)
        /// </summary>
        [Description("报修用时(分)")]
        public virtual long? RepairUseTimes { get; set; }
        /// <summary>
        /// 快速报修
        /// </summary>
        [Description("快速报修")]
        public virtual long? FastRepair { get; set; }
        /// <summary>
        /// 开始报修时间
        /// </summary>
        [Description("开始报修时间")]
        public virtual DateTime? BeginRepairDate { get; set; }
        /// <summary>
        /// 机修工号
        /// </summary>
        [Description("机修工号")]
        public virtual string RepairEmployeeNo { get; set; }
        /// <summary>
        /// 机修姓名
        /// </summary>
        [Description("机修姓名")]
        public virtual string RepairEmployeeName { get; set; }
        /// <summary>
        /// 完成维修时间
        /// </summary>
        [Description("完成维修时间")]
        public virtual DateTime? SuccessRepairDate { get; set; }
        /// <summary>
        /// 维修用时(分)
        /// </summary>
        [Description("维修用时(分)")]
        public virtual long? RepairTimes { get; set; }
        /// <summary>
        /// 维修故障号
        /// </summary>
        [Description("维修故障号")]
        public virtual string RepairFaultCode { get; set; }
        /// <summary>
        /// 维修故障名称
        /// </summary>
        [Description("维修故障名称")]
        public virtual string RepairFaultName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public virtual string Memo { get; set; }
    }
}

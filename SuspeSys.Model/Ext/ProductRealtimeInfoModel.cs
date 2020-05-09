using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    [Serializable]
    public partial class ProductRealtimeInfoModel
    {
        public virtual string GroupNO { set; get; }
        public virtual Int16? MainTrackNumber { set; get; }
        public virtual string StatingNo { set; get; }
        public virtual bool IsReceive { set; get; }
        public virtual bool? IsReceivingHanger { set; get; }
        public virtual string FullSite { set; get; }
        /// <summary>
        /// 故障信息
        /// </summary>
        public virtual string FaultInfo { set; get; }
        public virtual string Code { set; get; }
        public virtual string RealName { set; get; }
        public virtual string ClothingCar { set; get; }
        public virtual string OnlineHangerCount { set; get; }
        public virtual string StatingInCount { set; get; }
        public virtual int? Capacity { set; get; }
        public virtual string ProcessFlowName { set; get; }
        public virtual string OutSiteNo { set; get; }
        public virtual string ReworkSiteNo { set; get; }
        public virtual string StandardHours { set; get; }
        public virtual string RealyHours { set; get; }
        public virtual string SeamsEfficiencySite { set; get; }
        public virtual int? TodayOutAll { set; get; }
        public virtual int? TodayReworkAll { set; get; }
        public virtual string ReworkRate { set; get; }
        public virtual string StandardPartialAll { set; get; }
        public virtual string RealyHoursPartialAll { set; get; }
        public virtual string SeamsEfficiencyAll { set; get; }
        public virtual string FirmwareSN { set; get; }
        public virtual string FirmwareVersion { set; get; }
        public virtual int ReailHours { set; get; }
        public virtual int? OutSiteNoCount { set; get; }
        public virtual decimal TotalStanardHours { set; get; }
        /// <summary>
        /// 1：暂停接收衣架;0:接收衣架
        /// </summary>
        public virtual int SuspendReceive { set; get; }
        /// <summary>
        /// 是否在线
        /// </summary>
        [Description("是否在线")]
        public virtual bool? IsOnline { get; set; }
        public virtual string StatingLoginId { get; set; }
        public virtual string StatingName { get; set; }
        public virtual string StatingId { get; set; }
        public virtual int? TodayReworkCount { get; set; }
    }
}

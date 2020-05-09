using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 工艺路线图制单工序 扩展Model
    /// </summary>
    public class ProcessFlowChartFlowRelationModel : ProcessFlowChartFlowRelation {
        /// <summary>
        /// 工序Id 或者站点Id
        /// </summary>
        public string ProcessflowIdOrStatingId { set; get; }
        public string ParentId { set; get; }
        public string GroupName { set; get; }
        public string StatingNo { set; get; }
        public string GroupNo { set; get; }
        public byte[] MergeForwardPic { set; get; }

        /// <summary>
        /// 是否生效/是否接收衣架
        /// </summary>
        public bool IsEn { get; set; }
        /// <summary>
        /// 是否往前合并
        /// </summary>
        public bool IsMergeForw { get; set; }

        /// <summary>
        /// 是否接受衣架
        /// </summary>
        public bool IsAcceHanger { get; set; }

        public bool IsStating { get; set; }
        /// <summary>
        /// 工序号/比例
        /// </summary>
        //public string FlowNo { get; set; }
        public bool IsChind { get; set; }

        public IList<ProcessFlowStatingItem> ProcessFlowStatingItemList = new List<ProcessFlowStatingItem>();
        public IList<ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList = new List<ProcessFlowChartFlowRelationModel>();
        public IList<ProcessFlowChartGrop> ProcessFlowChartGropList = new List<ProcessFlowChartGrop>();

        /// <summary>
        /// 接收颜色
        /// </summary>
        public virtual string ReceivingColor { get; set; }
        /// <summary>
        /// 接收尺码
        /// </summary>
        public virtual string ReceivingSize { get; set; }
        /// <summary>
        /// 接收PO号码
        /// </summary>
        public virtual string ReceivingPoNumber { get; set; }
        /// <summary>
        /// 是否全部接收尺码
        /// </summary>
        public virtual bool? IsReceivingAllSize { get; set; }
        /// <summary>
        /// 是否全部接收颜色
        /// </summary>
        public virtual bool? IsReceivingAllColor { get; set; }
        /// <summary>
        /// 是否全部接收PO号码
        /// </summary>
        public virtual bool? IsReceivingAllPoNumber { get; set; }

        /// <summary>
        /// 是否是收尾站
        /// </summary>
        public virtual bool? IsEndStating { get; set; }

        /// <summary>
        /// 展示名称
        /// 颜色，尺码，是否收尾站，Po
        /// </summary>
        public virtual string FlowDisplayName
        {
            set { FlowName = value; }
            get
            {
                if (IsChind)
                {
                    string _flowName = string.Format("{0} {1} {2} {3}",
                                !string.IsNullOrEmpty(this.ReceivingColor) ? "颜色:" + this.ReceivingColor : "",
                                !string.IsNullOrEmpty(this.ReceivingSize) ? "尺码:" + this.ReceivingSize : "",
                                this.IsEndStating.HasValue && this.IsEndStating.Value ? "收尾站：是" : "",
                                !string.IsNullOrEmpty(this.ReceivingPoNumber) ? "PO号:" + this.ReceivingPoNumber : "");

                    return _flowName;
                }
                else
                    return FlowName;
            }
        }
    }
}

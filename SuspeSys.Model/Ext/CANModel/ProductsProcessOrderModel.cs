using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    /// <summary>
    /// 生产中的工艺明细
    /// </summary>
    public class ProductsProcessOrderModel
    {
        /// <summary>
        /// 工艺图分配Id（HangerProductFlowChart）
        /// </summary>
        public virtual string Id { set; get; }
        /// <summary>
        /// 工艺图工序索引
        /// </summary>
        public virtual string CraftFlowNo { set; get; }
        /// <summary>
        /// 挂片开始生产工序Id
        /// </summary>
        public virtual string BoltProcessFlowId { set; get; }
        /// <summary>
        /// 挂片开始生产工序代码
        /// </summary>
        public virtual string BoldProcessFlowCode { set; get; }
        /// <summary>
        /// 产出工序Id
        /// </summary>
        public virtual string OutputProcessFlowId { set; get; }
        /// <summary>
        /// 产出工序代码
        /// </summary>
        public virtual string OutProcessFlowCode { set; get; }

        public virtual string ProcessFlowId { set; get; }
        public virtual string FlowNo { set; get; }
        /// <summary>
        /// 工序代码
        /// </summary>
        public virtual string FlowCode { set; get; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public virtual string FlowName { set; get; }
        public virtual int MainTrackNumber { set; get; }
        /// <summary>
        /// 工序站点
        /// </summary>
        public virtual string StatingNo { set; get; }
        /// <summary>
        /// 站内容量
        /// </summary>
        public virtual long StatingCapacity { set; get; }
        public virtual byte? IsReceivingHanger { set; get; }
        public virtual bool? IsReceivingAllSize { set; get; }
        public virtual bool? IsReceivingAllColor { set; get; }
        public virtual string ReceivingColor { set; get; }
        public virtual string ReceivingSize { set; get; }
        public virtual string ReceivingPONumber { set; get; }

        /// <summary>
        /// 分摊比例
        /// </summary>
        public decimal Proportion { get; set; }

        /// <summary>
        /// 是否满站
        /// </summary>
        public bool IsFull { get; set; } = false;

        /// <summary>
        /// 剩余
        /// </summary>
        public long Remainder { get; set; } = 0;

        /// <summary>
        /// 工艺路线图Id
        /// </summary>
        public string ProcessChartId { get; set; }

        /// <summary>
        /// 站点角色
        /// </summary>
        public string StatingRoleCode { get; set; }
        public int FlowIndex { get; set; }
        public List<string> ReworkFlowNoList { get; set; }
    }
    public class SiteInfo {
        public virtual int MainTrackNumber { set; get; }
        public virtual string SiteNo { set; get; }
        public virtual int OnLineCount { set; get; }

    }
}

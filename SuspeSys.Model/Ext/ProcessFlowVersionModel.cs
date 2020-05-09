using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 制单工序版本 扩展Model
    /// </summary>
    public class ProcessFlowVersionModel : ProcessFlowVersion {
        public virtual string ProcessOrderId { set; get; }
        public virtual string POrderNo { set; get; }
        public virtual string StyleCode { set; get; }
        public virtual string StyleId { set; get; }
        public virtual string StyleName { set; get; }

        public IList<ProcessFlowChartModel> ProcessFlowChartModelList = new List<ProcessFlowChartModel>();
    }
}

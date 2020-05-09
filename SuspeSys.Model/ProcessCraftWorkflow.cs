using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    public class ProcessCraftWorkflow {
        public ProcessCraftWorkflow() { }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual Order Order { get; set; }
        /// <summary>
        /// 路线名称
        /// </summary>
        public virtual string LinkName { get; set; }
        /// <summary>
        /// 产品部位
        /// </summary>
        public virtual string ProductPosition { get; set; }
        /// <summary>
        /// 目标产量
        /// </summary>
        public virtual int? TargetNum { get; set; }
        /// <summary>
        /// 产出工序
        /// </summary>
        public virtual string OutputProcessFlowId { get; set; }
        /// <summary>
        /// 挂片开始生产工序
        /// </summary>
        public virtual string BoltProcessFlowId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}

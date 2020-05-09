using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class ProcessOrderExtModel: ProcessOrder
    {
        public virtual string PurchaseOrderNo{set;get;}
        public virtual string Color { set; get; }
        public virtual string SizeDesption { set; get; }
        public virtual int Total { set; get; }
        /// <summary>
        /// 已分配数量
        /// </summary>
        public virtual int AllocationedTotal { set; get; }
        /// <summary>
        /// 未分配数量
        /// </summary>
        public virtual int NonAllocationTotal { set; get; }

        /// <summary>
        /// 任务数量
        /// </summary>
        public virtual int TaskTotal { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual int Unit { set; get; }
        // public virtual string LineName
    }
}

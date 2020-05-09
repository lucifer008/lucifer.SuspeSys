using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    /// <summary>
    /// 制单状态
    ///0:待审核
    ///1:已审核
    ///2:制作完成
    ///3:生产中
    ///4：完成
    /// </summary>
    public class ProcessOrderStatus
    {
        public static readonly ProcessOrderStatus WaitingAudit = new ProcessOrderStatus(0, "待审核");
        public static readonly ProcessOrderStatus Audited = new ProcessOrderStatus(1, "已审核");
        public static readonly ProcessOrderStatus FinishedProduction = new ProcessOrderStatus(2, "制作完成");
        public static readonly ProcessOrderStatus InProduction = new ProcessOrderStatus(3, "生产中");
        public static readonly ProcessOrderStatus Successed = new ProcessOrderStatus(4, "完成");

        private ProcessOrderStatus(byte _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public byte Value { set; get; }
        public string Desption { set; get; }
    }
}

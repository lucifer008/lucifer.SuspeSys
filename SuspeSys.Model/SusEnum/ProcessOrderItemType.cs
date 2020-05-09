using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class ProcessOrderItemType
    {
        public static readonly ProcessOrderItemType Color = new ProcessOrderItemType("0", "颜色");
        public static readonly ProcessOrderItemType Size = new ProcessOrderItemType("1", "尺码");
        private ProcessOrderItemType(string _value,string desption) {
            Value = _value;
            Desption = desption;
        }
        public string Value { set; get; }
        public string Desption { set; get; }
    }
}

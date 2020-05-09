using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class ProcessOrderType
    {
        public static readonly ProcessOrderType Single = new ProcessOrderType(1, "单客户类型制单");
        public static readonly ProcessOrderType Composed = new ProcessOrderType(2, "合成类型制单");
        public static readonly ProcessOrderType Other = new ProcessOrderType(3, "其他制单");
        private ProcessOrderType(byte _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public byte Value { set; get; }
        public string Desption { set; get; }
    }
}

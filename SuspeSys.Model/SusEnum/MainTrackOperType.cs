using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class MainTrackOperType
    {
        public static readonly MainTrackOperType Start = new MainTrackOperType("0", "启动");
        public static readonly MainTrackOperType Emergency = new MainTrackOperType("1", "急停");
        public static readonly MainTrackOperType Stop = new MainTrackOperType("2", "停止");
        private MainTrackOperType(string _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public string Value { set; get; }
        public string Desption { set; get; }
    }
}

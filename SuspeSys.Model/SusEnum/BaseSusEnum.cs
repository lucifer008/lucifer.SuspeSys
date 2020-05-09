using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
   public class BaseSusEnum
    {
        public BaseSusEnum(short _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public short Value { set; get; }
        public string Desption { set; get; }
    }
}

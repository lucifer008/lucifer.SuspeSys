using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEvent
{
    public class SusEventArgs: EventArgs
    {
        public object Tag { set; get; }

    }
}

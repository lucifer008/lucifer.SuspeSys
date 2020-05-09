using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sus.Net.Common.Event;

namespace SuspeSys.Service.Impl.CAN
{
    public class SusThreadShareState
    {
        public MessageEventArgs EventArg { get; internal set; }
        public object Sender { get; internal set; }
    }
}

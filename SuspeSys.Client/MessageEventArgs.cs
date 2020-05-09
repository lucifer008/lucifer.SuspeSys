using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client
{
    public class MessageEventArgs : EventArgs
    {
        public int Status { set; get; }
        public int ProgressValue { set; get; }
        public string StatusText { set; get; }
    }
}

using SusNet.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core
{
    public abstract class Dispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract void DoService(object sender, Sus.Net.Common.Event.MessageEventArgs e);
    }
}

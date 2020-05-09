using Sus.Net.Common.SusBusMessage;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCallStop
{
    public class MaterialCallStopAdapter : SusLog
    {
        private MaterialCallStopAdapter() { }
        public readonly static MaterialCallStopAdapter Instance = new MaterialCallStopAdapter();
        private readonly static object locObj = new object();
        public virtual void DoService(MaterialCallStopRequestMessaage request)
        {
            lock (locObj)
            {
                MaterialCallStopHandler.Instance.Process(request);
            }
        }
    }
}

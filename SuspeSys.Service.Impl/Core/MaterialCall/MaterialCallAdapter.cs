using Sus.Net.Common.SusBusMessage;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCall
{
    public class MaterialCallAdapter : SusLog
    {
        private MaterialCallAdapter() { }
        public readonly static MaterialCallAdapter Instance = new MaterialCallAdapter();
        private readonly static object locObj = new object();
        public virtual void DoService(MaterialCallRequestMessage request)
        {
            lock (locObj)
            {
                MaterialCallHandler.Instance.Process(request);
            }
        }
    }
}

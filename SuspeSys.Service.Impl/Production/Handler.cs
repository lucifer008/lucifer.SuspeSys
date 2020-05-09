using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Production
{
    public abstract class Handler
    {
        protected MessageBody Message;
        protected int MainTrackNumber;
        protected int StatingNo;
        protected int CMD;

        protected string HexMainTrackNumber;
        protected string HexStatingNo;
        protected string HexCMD;
        public Handler(MessageBody message)
        {
            this.Message = message;
            this.MainTrackNumber = HexHelper.HexToTen(message.XID);
            this.StatingNo = HexHelper.HexToTen(message.ID);
            this.CMD = HexHelper.HexToTen(message.CMD);
            this.HexMainTrackNumber = message.XID;
            this.HexStatingNo = message.ID;
            this.HexCMD = message.CMD;
        }
        public abstract void Process();
        public virtual void Process<T>(Func<T> action)
        {
           // T o = default(T);
            action?.Invoke();
        }
    }
}

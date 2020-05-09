using SusNet.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Production.InStating
{
    public class InStatingRequestHandler: Handler
    {
        public InStatingRequestHandler(MessageBody message) :base(message) {
        }
        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SusNet.Common.Message;

namespace SuspeSys.Service.Impl.Production.ProcessComparison
{
    public class ProcessComparisonResponseHandler : Handler
    {
        public ProcessComparisonResponseHandler(MessageBody message) : base(message)
        {
        }

        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}

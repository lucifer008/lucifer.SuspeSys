﻿using SusNet.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Production.OutStating
{
    public class OutStatingRequestHandler : Handler
    {
        public OutStatingRequestHandler(MessageBody message) :base(message) {
        }
        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}

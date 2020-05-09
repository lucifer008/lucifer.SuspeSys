using System;
using System.Collections.Generic;
using System.Text;

namespace SusNet.Common.Message
{
    public interface IMessage
    {
        byte[] Encode();
        string Describe();
    }
}

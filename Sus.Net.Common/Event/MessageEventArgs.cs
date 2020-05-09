using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Sus.Net.Common.Event
{
    public class MessageEventArgs : EventArgs
    {
        public string message { get; set; }

        public MessageEventArgs(string msg)
        {
            this.message = msg;
        }
        public MessageEventArgs(TcpClient tcpClient,string msg)
        {
            this.message = msg;
            this.TcpClient = tcpClient;
        }
        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get;  set; }

        public object Tag { set; get; }
    }
}

using SusNet.Common.Utils;

namespace SuspeSys.Remoting.Action.Tcp
{
    public class TcpAction
    {
        public static void StartTcpPort(int port) {
          ////  SuspeTcpServer.Intance.StartServer(port);
          //  SuspeTcpServer.Intance.ClientConnected += Intance_ClientConnected;
          //  SuspeTcpServer.Intance.ClientDataReceived += Intance_ClientDataReceived;
          //  SuspeTcpServer.Intance.ClientDisconnected += Intance_ClientDisconnected;
        }

        //private static void Intance_ClientDisconnected(object sender, Cowboy.Sockets.TcpClientDisconnectedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        //private static void Intance_ClientDataReceived(object sender, Cowboy.Sockets.TcpClientDataReceivedEventArgs e)
        //{

        //    byte[] dstArray = new byte[6];
        //    ////Buffer.BlockCopy(e.Data, 0, dstArray, e.DataOffset, 6);
        //    var d = e.DataOffset;
        //    for (var i = 0; i < 6; i++)
        //    {
        //        dstArray[i] = e.Data[d + i];
        //    }
        //    var data = HexHelper.byteToHexStr(dstArray);
        //    //SuspeTcpServer.Intance.TcpSocketServer.SendTo(e.Session, Encoding.UTF8.GetBytes(data));
        //    //throw new NotImplementedException();
        //}

        //private static void Intance_ClientConnected(object sender, global::Cowboy.Sockets.TcpClientConnectedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

    }
}

using DevExpress.XtraEditors;
using Suspe.CAN.Action.CAN;
using SuspeSys.Remoting.SuspeSysRemoting;
//using SuspeSys.Remoting.TcpPattern;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Service.Impl.Task;

namespace SuspeSys.Remoting
{
    public class SusBootstrap
    {
        public static SusBootstrap Instance = new SusBootstrap();
        // public static string BaseApplicationPath;
        private SusBootstrap() { }

        public void Start(ListBoxControl _lbMessage, bool isServiceStart = false, string baseApplicationPath = null, bool isServer = false)
        {

            NewSusRedisClient.Instance.Init(isServiceStart);
            //注册remoting
            TcpBootstrap.Instance.RegsiterRemotingByCode(_lbMessage, isServiceStart, baseApplicationPath);
            if (!isServer)
            {
               // CANTcp.Instance.ConnectCAN(_lbMessage, isServiceStart);
                return;
            }

            CANTcpServer.Instance.ConnectCAN(_lbMessage, isServiceStart);

            QuartzAction.Start();
        }
        public void Stop(ListBoxControl _lbMessage, bool isServiceStart = false)
        {
            CANTcpServer.Instance.Stop(_lbMessage, isServiceStart);
        }
        public void Disconnect(ListBoxControl _lbMessage, bool isServiceStart = false)
        {
           // CANTcp.Instance.Disconnect(_lbMessage, isServiceStart);
        }
        public void SendMessageWithCANTest(string maintackNumber)
        {
            CANTcpServer.Instance.SendMessageWithCANTest(maintackNumber);
        }
    }
}

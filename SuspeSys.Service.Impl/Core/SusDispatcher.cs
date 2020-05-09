using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SusNet.Common.Message;
using SusNet.Common.SusBusMessage;
using log4net;
using SuspeSys.Service.Impl.Core.Allocation;
using DevExpress.XtraEditors;
using Sus.Net.Common.Event;
using Sus.Net.Common.SusBusMessage;
using SuspeSys.Service.Impl.Core.MaterialCall;
using System.Net.Sockets;
using SuspeSys.Service.Impl.Core.F2;
using SuspeSys.Service.Impl.Core.MaterialCallStop;
using SuspeSys.Service.Impl.Core.CallManagement;
using SuspeSys.Service.Impl.Core.FaultRepair;

namespace SuspeSys.Service.Impl.Core
{
    public class SusDispatcher //: Dispatcher
    {
        protected static readonly ILog log = LogManager.GetLogger("LogLogger");
        protected static readonly ILog tcpLogInfo = LogManager.GetLogger("TcpLogInfo");
        protected static readonly ILog tcpLogError = LogManager.GetLogger("TcpErrorInfo");
        protected static readonly ILog tcpLogHardware = LogManager.GetLogger("TcpHardwareInfo");
        protected static readonly ILog errorLog = LogManager.GetLogger("Error");
        protected static readonly ILog timersLog = LogManager.GetLogger("Timers");
        protected static readonly ILog redisLog = LogManager.GetLogger("RedisLogInfo");
        protected static readonly ILog cacheInfo = LogManager.GetLogger("CacheInfo");
        protected static readonly ILog montorLog = LogManager.GetLogger("MontorLogger");
        private readonly static SusDispatcher instance = new SusDispatcher();
        public static SusDispatcher Instance
        {
            get
            {
                return instance;
            }
        }


        internal void DoService(object sender, MessageEventArgs e, ListBoxControl lbMessage, EventHandler eventHandler, bool isServiceStart, TcpClient tcpClient = null)
        {
            DoHandler(e.Tag as MessageBody, e, lbMessage, eventHandler, isServiceStart, tcpClient);
        }
        private readonly static object locObj = new object();
        private void DoHandler(MessageBody message, MessageEventArgs e, ListBoxControl lbMessage, EventHandler eventHandler, bool isServiceStart, TcpClient tcpClient = null)
        {
            if ((message as HangerOutStatingRequestMessage) != null)
            {
                lock (locObj)
                {
                    if (!isServiceStart)
                    {
                        lbMessage.Invoke(eventHandler, "OK----", null);
                    }
                    tcpLogInfo.InfoFormat("【出战请求消息】:{0}", message.ToString());
                    OutSiteAdapter.Instance.DoService((HangerOutStatingRequestMessage)message);
                    return;
                }
            }
            else if ((message as MaterialCallRequestMessage) != null)
            {
                //缺料请求
                MaterialCallAdapter.Instance.DoService((MaterialCallRequestMessage)message);
            }
            else if ((message as MaterialCallStopRequestMessaage) != null)
            {
                //终止缺料
                MaterialCallStopAdapter.Instance.DoService((MaterialCallStopRequestMessaage)message);
            }
            else if ((message as MaterialCallUploadRequestMessage) != null)
            {
                //缺料上传
                MaterialCallUploadAdapter.Instance.DoService((MaterialCallUploadRequestMessage)message, tcpClient);
            }
            else if ((message as F2AssignHangerNoUploadRequestMessage) != null)
            {
                //F2指定衣架号上传
                F2Adapter.Instance.DoService((F2AssignHangerNoUploadRequestMessage)message, tcpClient);
            }
            else if ((message as F2AssignRequestMessage) != null)
            {
                //F2指定实施
                F2Adapter.Instance.DoService((F2AssignRequestMessage)message, tcpClient);
            }//呼叫管理呼叫开始
            else if ((message as CallManagementStartRequestMessage) != null)
            {
                CallManagementAdapter.Instance.DoService((message as CallManagementStartRequestMessage), tcpClient);
            }
            //呼叫管理呼叫停止
            else if ((message as CallStopRequestMessage) != null)
            {
                CallManagementAdapter.Instance.DoService((message as CallStopRequestMessage), tcpClient);
            }
            //故障报修-->故障代码下发请求
            else if ((message as FaultRepairUploadStartRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairUploadStartRequestMessage), tcpClient);
            }
            //故障报修-->故障代码报修上传请求
            else if ((message as FaultRepairReqtRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairReqtRequestMessage), tcpClient);
            }
            //故障报修-->中止故障报修
            else if ((message as FaultRepairStopRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairStopRequestMessage), tcpClient);
            }
            //故障报修-->开始故障报修
            else if ((message as FaultRepairStartRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairStartRequestMessage), tcpClient);
            }
            //故障报修-->完成故障报修
            else if ((message as FaultRepairSucessRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairSucessRequestMessage), tcpClient);
            }
            //故障报修-->故障报修类别及故障代码请求
            else if ((message as FaultRepairClothingTypeAndFaultCodeRequestMessage) != null)
            {
                FaultRepairAdapter.Instance.DoService((message as FaultRepairClothingTypeAndFaultCodeRequestMessage), tcpClient);
            }
        }
    }
}

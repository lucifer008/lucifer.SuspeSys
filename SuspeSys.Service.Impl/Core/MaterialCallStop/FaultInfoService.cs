using Suspe.CAN.Action.CAN;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCallStop
{
    public class FaultInfoService
    {
        public static FaultInfoService Instance { get { return new FaultInfoService(); } }

        /// <summary>
        /// 缺料终止
        /// </summary>
        /// <param name="mainTrackNumber">主轨</param>
        /// <param name="statingNo">站点</param>
        /// <param name="materialCode">缺料编码</param>
        /// <param name="faultInfo">缺料名称</param>
        public void MaterialCallStop(int mainTrackNumber, int statingNo, string  materialCode, string faultInfo)
        {
            var CallNoticeStNo = SystemParameterService.Instance.GetHangUpLineOtherValue(Domain.SusEnum.SystemParameterHangUpOther.CallNoticeStNo, mainTrackNumber.ToString(), statingNo.ToString());

            //1、收到中止呼叫的指令时，发送取消呼叫指令，到发起呼叫起始站点
            CANTcpServer.server.MaterialCallStop(mainTrackNumber, CallNoticeStNo);

            //2、实时产线信息，故障信息该站点取消显示该缺料消息

            //2.1【表Stating字段(FaultInfo)】--->清空
            StatingServiceImpl.Instance.UpdateFaultInfo(mainTrackNumber, statingNo, string.Empty);

            //2.2 【KanbanInfo】只改状态
            KanbanInfoServiceImpl.Instance.Cancel(mainTrackNumber, statingNo.ToString(), materialCode);
        }
    }
}
